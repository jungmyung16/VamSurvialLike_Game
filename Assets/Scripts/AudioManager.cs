using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    public static AudioManager instance;

    [Header("BGM")]
    public AudioClip bgmClip; // 배경 음악 클립
    public float bgmVolume; // 배경 음악 볼륨
    AudioSource bgmPlayer; // 배경 음악 재생용 오디오 소스
    AudioHighPassFilter bgmHighPassFilter; // 배경 음악 필터

    [Header("SFX")]
    public AudioClip[] sfxClips; // 효과음 클립 배열
    public float sfxVolume; // 효과음 볼륨
    public int channels; // 효과음 채널 수
    AudioSource[] sfxPlayers; // 효과음 재생용 오디오 소스 배열
    int channelIndex; // 현재 채널 인덱스

    public enum Sfx { Dead, Hit, LevelUp=3, Lose, Melee, Range=7, Select, Win} // 효과음 종류

    void Awake()
    {
        instance = this; // 싱글톤 인스턴스 설정
        Init(); // 초기화
    }

    void Init()
    {
        // 배경 음악 초기화
        GameObject bgmObject = new GameObject("bgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false; // 자동 재생 안 함
        bgmPlayer.loop = true; // 반복 재생
        bgmPlayer.volume = bgmVolume; // 배경 음악 볼륨 설정
        bgmPlayer.clip = bgmClip; // 배경 음악 클립 설정
        bgmHighPassFilter = Camera.main.GetComponent<AudioHighPassFilter>(); // 필터 설정

        // 효과음 초기화
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels]; // 채널 수만큼 오디오 소스 배열 생성

        for(int index=0; index<sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false; // 자동 재생 안 함
            sfxPlayers[index].bypassListenerEffects = true; // 리스너 효과 우회
            sfxPlayers[index].volume = sfxVolume; // 효과음 볼륨 설정
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlayer.Play(); // 배경 음악 재생
        }
        else
        {
            bgmPlayer.Stop(); // 배경 음악 정지
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmHighPassFilter.enabled = isPlay; // 고주파 필터 활성화/비활성화
    }

    public void PlaySfx(Sfx sfx)
    {
        // 효과음 채널에서 비어 있는 플레이어 찾아 재생
        for(int i=0; i<sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length; // 채널 순환

            if (sfxPlayers[loopIndex].isPlaying)
                continue; // 이미 재생 중인 채널은 넘어감

            channelIndex = loopIndex; // 현재 채널 인덱스 갱신
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx]; // 선택한 효과음 클립 설정
            sfxPlayers[loopIndex].Play(); // 효과음 재생
            break;
        }
    }
}

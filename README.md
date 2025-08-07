# 🧛 VamSurvialLike_Game(2D 로그라이크 서바이벌 게임)

## 📖 소개 (Introduction)
이 프로젝트는 Unity로 개발된 2D 탑뷰 로그라이크 서바이벌 게임이다. 플레이어는 끊임없이 몰려오는 적들을 처치하며 제한 시간 동안 생존하는 것이 목표이다.

## 🎮 주요 기능 (Features)
* **캐릭터 시스템**
  * 4명의 플레이 가능한 캐릭터 (각기 다른 능력치 보유)
  * 이동 속도, 무기 속도, 데미지 등 고유 스탯 차별화
  
* **전투 시스템**
  * 근접 무기 (낫) - 플레이어 주변을 회전하며 적 공격
  * 원거리 무기 (건) - 가장 가까운 적을 자동 조준하여 발사
  * 자동 타겟팅 시스템 (Scanner를 통한 적 탐지)
  
* **적 AI 시스템**
  * 슬라임 타입의 적들이 플레이어를 추적
  * 시간에 따라 난이도 상승 (스폰 속도, 체력, 이동 속도 증가)
  * 넉백 시스템 구현
  
* **레벨업 및 아이템 시스템**
  * 경험치 획득을 통한 레벨업
  * 레벨업 시 3가지 랜덤 아이템 중 선택
  * 무기 강화 및 새로운 능력 획득
  
* **게임 플로우**
  * 타이틀 화면 → 캐릭터 선택 → 게임 플레이 → 결과 화면
  * 제한 시간 생존 시 승리
  * 체력 소진 시 게임 오버
  
* **UI/UX 시스템**
  * 실시간 HUD (체력, 경험치, 레벨, 처치 수, 남은 시간)
  * 레벨업 선택 창
  * 게임 결과 화면
  
* **사운드 시스템**
  * 배경 음악 및 효과음
  * 타격, 레벨업, 승리/패배 등 상황별 사운드

## 스크린샷 및 작품설명
|타이틀화면|인게임화면|레벨업 시 능력 선택창|게임종료 및 해금 알림창|
|:---------:|:----------:|:---------------:|:--------:|
|<img src="https://github.com/user-attachments/assets/42f4416e-245c-417c-a28a-e749ad1012d6" alt="Image" width="300" />|<img src="https://github.com/user-attachments/assets/70c7a575-e594-4dc0-8bfa-a6dd8886c180" alt="Image" width="300" />|<img src="https://github.com/user-attachments/assets/5dc52e02-b93c-415c-aa8d-f7555ac6fdfc" alt="Image" width="300" />|<img src="https://github.com/user-attachments/assets/fa5175df-4182-47b5-8ff0-fb8b7f940497" alt="Image" width="300" />|
- 캐릭터 해금 조건 달성시 추가 캐릭터를 얻을 수 있다. 각 캐릭터마다 다른 능력을 가지고 게임을 시작한다.
- 플레이어가 맵의 반지름 만큼 이동했을 때 맵이 플레이어를 따라 움직이면서 무한으로 맵이 생성되는 것 처럼 보여진다.
- 오브젝트 풀링 기법으로 몬스터를 소환해서 성능 최적화와 메모리 관리
- 레벨 디자인을 설계해 레벨마다 다른 몬스터가 출몰해 난이도를 조정했다.
- 기본적으로 플레이어 주변에 회전하는 무기를 만들어 몬스터를 공격한다.
- 스캐너를 개발해 플레이어 주변에 적용하여 가장 가까운 적에게 원거리 공격을 가한다.
- 레벨업시 추가 능력을 업그레이드 할 수 있다.

## 🛠 기술 스택 (Tech Stack)
* **프로그래밍 언어:** C#
* **게임 엔진:** Unity 2022.3.18f1 LTS
* **렌더링 파이프라인:** Universal Render Pipeline (URP) 14.0.10
* **주요 패키지:**
  * Unity Input System 1.7.0
  * Cinemachine 2.9.7 (카메라 제어)
  * 2D Animation 9.1.0
  * 2D Sprite 1.0.0
  * TextMeshPro 3.0.6
* **개발 환경:** Visual Studio 2022

## 🚀 설치 및 실행 방법 (Getting Started)

### 필요 조건 (Prerequisites)
* Unity Hub (최신 버전)
* Unity 2022.3.18f1 LTS
* Visual Studio 2022 또는 Visual Studio Code
* Git (선택사항)

### 빌드 및 실행 순서
1. **프로젝트 클론**
   ```bash
   git clone [레포지토리 주소]
   ```

2. **Unity Hub에서 프로젝트 열기**
   * Unity Hub 실행
   * "Add" 버튼 클릭
   * 클론한 프로젝트 폴더 선택
   * Unity 2022.3.18f1 버전으로 프로젝트 열기

3. **씬 로드**
   * `Assets/Scenes/SampleScene.unity` 씬 열기

4. **실행**
   * Unity 에디터 상단의 Play 버튼 클릭
   * 또는 `File → Build and Run`으로 빌드 후 실행

## 🎯 게임 플레이 방법 (How to Play)

### 키보드 조작법
* **이동:** `W`, `A`, `S`, `D` 또는 화살표 키
* **자동 공격:** 무기가 자동으로 적을 공격
* **레벨업 선택:** 마우스 클릭으로 아이템 선택

### 게임 목표
* 제한 시간(20초) 동안 생존하기
* 적을 처치하여 경험치 획득
* 레벨업을 통해 강력한 무기와 능력 획득
* 체력이 0이 되지 않도록 주의

## 📂 프로젝트 구조 (Project Structure)
```
Assets/
├── Scripts/              # 게임 로직 스크립트
│   ├── GameManager.cs   # 게임 전체 관리 (싱글톤)
│   ├── PlayerController.cs # 플레이어 이동 및 제어
│   ├── Enemy.cs         # 적 AI 및 행동
│   ├── EnemySpawner.cs  # 적 생성 관리
│   ├── Weapon.cs        # 무기 시스템
│   ├── Bullet.cs        # 총알 로직
│   ├── Item.cs          # 아이템 시스템
│   ├── Scanner.cs       # 적 탐지 시스템
│   ├── AudioManager.cs  # 사운드 관리
│   └── ...
├── Prefabs/             # 재사용 가능한 게임 오브젝트
│   ├── Player.prefab    # 플레이어 프리팹
│   ├── Enemy.prefab     # 적 프리팹
│   ├── Bullet0.prefab   # 낫 프리팹
│   ├── Bullet1.prefab   # 총알 프리팹
│   └── ...
├── Animations/          # 애니메이션 파일
│   ├── Player/          # 플레이어 애니메이션
│   └── Enemy/           # 적 애니메이션
├── Sprites/             # 2D 스프라이트 리소스
├── Scriptable/          # ScriptableObject 아이템 데이터
│   ├── Item0.asset      # 낫 데이터
│   ├── Item1.asset      # 건 데이터
│   └── ...
├── Scenes/              # 게임 씬
│   └── SampleScene.unity # 메인 게임 씬
└── PlayerActions/       # Input System 액션 맵
    └── Player.inputactions

```

## 🎨 주요 시스템 설명

### 오브젝트 풀링 시스템
* `EnemyPool.cs`를 통한 효율적인 적 관리
* 총알과 적 오브젝트 재사용으로 성능 최적화

### 캐릭터 능력치 시스템
* `Character.cs`에서 캐릭터별 고유 능력치 관리
* 플레이어 ID에 따른 스탯 차별화

### 아이템 데이터 시스템
* ScriptableObject를 활용한 아이템 데이터 관리
* 레벨별 데미지, 공격 횟수 등 세부 설정 가능

## 💡 개발 팁
* 게임 난이도 조절: `GameManager.cs`의 `maxGameTime` 수정
* 적 스폰 속도 조절: `EnemySpawner.cs`의 `SpawnData` 수정
* 캐릭터 능력치 조절: `Character.cs`의 속성값 수정

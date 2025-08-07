# 🧛 VamSurvialLike_Game

## 소개 (Introduction)
이 프로젝트는 뱀파이어 서바이벌 장르에서 영감을 받은 2D 탑다운 서바이벌 게임이다. 플레이어는 끊임없이 몰려오는 적들을 물리치며 생존하는 것이 목표다.

## 주요 기능 (Features)
* **플레이어 캐릭터 시스템**
  * 8방향 이동 (WASD/화살표 키)
  * 다중 공격 액션 (Fire1, Fire2, Fire3)
  * 점프 기능
* **적 AI 시스템**
  * Enemy 태그 기반 적 관리
  * Bullet 시스템을 통한 투사체 처리
* **게임플레이 요소**
  * Ground 및 Area 기반 레벨 디자인
  * 6단계 그래픽 품질 설정 (Very Low ~ Ultra)
* **카메라 시스템**
  * Cinemachine을 활용한 부드러운 카메라 추적

## 기술 스택 (Tech Stack)
* **프로그래밍 언어:** C#
* **게임 엔진:** Unity 2022.3.20f1
* **렌더링 파이프라인:** Universal Render Pipeline (URP) 14.0.10
* **주요 패키지:**
  * Unity Input System 1.7.0
  * Cinemachine 2.9.7
  * TextMeshPro 3.0.6
  * 2D Animation 9.1.0
  * 2D Sprite & Tilemap
* **개발 환경:** 
  * Visual Studio 2022
  * Rider 3.0.27

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

## 설치 및 실행 방법 (Getting Started)

### 필요 조건 (Prerequisites)
* Unity Hub (최신 버전)
* Unity 2022.3.20f1 LTS
* Visual Studio 2022 또는 JetBrains Rider

### 빌드 및 실행 순서
1. 저장소 클론
   ```bash
   git clone [레포지토리 주소]
   ```
2. Unity Hub를 실행하고 "Add" 버튼을 클릭하여 클론한 프로젝트 폴더를 선택
3. Unity 2022.3.20f1 버전으로 프로젝트 열기
4. 프로젝트가 로드되면 `File > Build Settings`에서 플랫폼 선택 (Windows/Mac/Linux)
5. "Build and Run" 클릭하여 빌드 및 실행

## 게임 플레이 방법 (How to Play)

### 키보드 조작법
* **이동:** `W`, `A`, `S`, `D` 또는 화살표 키
* **주 공격:** `Left Ctrl` 또는 `마우스 왼쪽 클릭`
* **보조 공격:** `Left Alt` 또는 `마우스 오른쪽 클릭`
* **특수 공격:** `Left Shift` 또는 `마우스 가운데 클릭`
* **점프:** `Space`
* **일시정지:** `Escape`

### 게임패드 지원
* 조이스틱으로 이동
* 버튼 0, 1, 2로 각각 공격
* 버튼 3으로 점프

## 프로젝트 구조 (Project Structure)
```
Vam_Survial_Like/
├── Assets/
│   ├── Scenes/           # 게임 씬 파일 (SampleScene 포함)
│   ├── Scripts/          # 게임 로직 C# 스크립트
│   ├── Prefabs/          # 재사용 가능한 게임 오브젝트
│   └── TextMesh_Pro/     # TMPro 폰트 및 설정
├── Packages/             # Unity 패키지 설정
│   ├── manifest.json     # 프로젝트 의존성 정의
│   └── packages-lock.json
└── ProjectSettings/      # Unity 프로젝트 설정
    ├── InputManager.asset     # 입력 시스템 설정
    ├── TagManager.asset       # 태그 및 레이어 설정
    ├── QualitySettings.asset  # 그래픽 품질 프리셋
    └── GraphicsSettings.asset # URP 렌더링 설정
```

### 주요 시스템 구성
* **Enemy Layer:** 적 캐릭터 충돌 처리용 레이어
* **태그 시스템:**
  * `Ground`: 지형 충돌 감지
  * `Area`: 특정 구역 트리거
  * `Enemy`: 적 캐릭터 식별
  * `Bullet`: 투사체 관리

# 회귀 서사 미연시 - 전체 아키텍처 기획서

## 1. 프로젝트 개요

이 문서는 회귀 서사 웹소설을 Unity 6 미연시 게임으로 만들 때의 전체 시스템 설계입니다.

```text
시놉시스(뼈대) → JSON(데이터) → Dialogue(화면) → Choice(선택) → Branch(분기) → Ending(결말)
```

핵심 원칙:
- 시놉시스는 뼈대일 뿐, 실제 대사는 AI가 자유롭게 생성
- 기능은 하나씩 만들고 테스트 성공 후 다음 기능으로 진행
- 초보자가 따라 할 수 있는 구조

---

## 2. 전체 시스템 구조

```text
┌─────────────────────────────────────────────────────────────────┐
│                        Unity 6 게임                              │
│                                                                  │
│  ┌──────────┐    ┌──────────┐    ┌──────────┐    ┌──────────┐  │
│  │ 타이틀   │───→│ 장면     │───→│ 대사     │───→│ 선택지   │  │
│  │ 화면     │    │ 로딩     │    │ 출력     │    │ 표시     │  │
│  └──────────┘    └──────────┘    └──────────┘    └──────────┘  │
│       │                                                │        │
│       │              ┌──────────┐                      │        │
│       │              │ 저장/    │←─────────────────────┘        │
│       │              │ 불러오기 │                               │
│       │              └──────────┘                               │
│       │                                                         │
│       ▼                                                         │
│  ┌──────────┐    ┌──────────┐    ┌──────────┐                  │
│  │ 설정     │    │ 호감도   │    │ 엔딩     │                  │
│  │ 화면     │    │ 시스템   │    │ 분기     │                  │
│  └──────────┘    └──────────┘    └──────────┘                  │
└─────────────────────────────────────────────────────────────────┘
```

---

## 3. 데이터 흐름: Synopsis → JSON → Dialogue → Choice → Branch → Ending

### 3-1. Synopsis → JSON

시놉시스 파일들:

```text
series_synopsis.md  = 전체 줄거리 요약
chapters.md         = 15개 챕터 개요
chapter01.md        = 회귀 전 이야기 (독이 든 잔)
chapter02.md        = 회귀 후 황궁 연회
chapter03.md        = 릴리 악행 조사
chapter04.md        = 약혼 파기와 비혼 선언
chapter05~15.md     = 이후 전개 (나중에 추가)
```

이 시놉시스들을 JSON 구조로 변환합니다.

### 3-2. JSON 구조 설계

#### 전체 게임 데이터 구조

```json
{
  "gameTitle": "회귀 서사",
  "chapters": [
    {
      "chapterId": 1,
      "chapterTitle": "이번에는 아무도 못 건드린다",
      "scenes": [
        {
          "sceneId": "01_001",
          "background": "bg_dark_prison",
          "characters": [],
          "lines": [
            {
              "speaker": "",
              "text": "황궁의 지하 감옥. 창문 하나 없는 돌방.",
              "isNarration": true,
              "emotion": ""
            },
            {
              "speaker": "이네스",
              "text": "또 이 꿈을 꾸는 건가.",
              "isNarration": false,
              "emotion": "calm"
            }
          ],
          "choices": [],
          "nextScene": "01_002"
        }
      ]
    }
  ]
}
```

#### 장면(Scene) 구조 설명

```json
{
  "sceneId": "챕터번호_장면번호",
  "background": "배경 이미지 이름",
  "characters": ["현재 화면에 보이는 캐릭터 목록"],
  "lines": [대사 목록],
  "choices": [선택지 목록],
  "nextScene": "다음 장면 번호 (선택지 없을 때)"
}
```

#### 대사(Line) 구조 설명

```json
{
  "speaker": "말하는 사람 이름 (나레이션이면 빈칸)",
  "text": "대사 내용",
  "isNarration": true/false,
  "emotion": "감정 상태 (표정 변경용)"
}
```

#### 선택지(Choice) 구조 설명

```json
{
  "text": "선택지 글자",
  "nextScene": "이 선택지를 골랐을 때 이동할 장면",
  "affection": {
    "카셀": 1,
    "세자르": 0,
    "타국왕자": 0
  },
  "flag": {
    "key": "선택_기록_이름",
    "value": true
  }
}
```

#### 호감도(Affection) 구조 설명

```json
{
  "affection": {
    "카셀": 0,
    "세자르": 0,
    "타국왕자": 0,
    "알렉산드로": 0
  }
}
```

#### 엔딩(Ending) 구조 설명

```json
{
  "endingId": "ending_cassel_happy",
  "endingTitle": "카셀 해피엔딩",
  "endingDescription": "신뢰와 존중 위에서 다시 선택된 사랑.",
  "condition": {
    "affection": {
      "카셀": 10
    },
    "flag": {
      "카셀_고백_수락": true
    }
  }
}
```

### 3-3. JSON → Dialogue 시스템

Unity의 C# 스크립트가 JSON 파일을 읽어서 화면에 보여주는 흐름입니다.

```text
JSON 파일 (Resources 폴더)
    │
    ▼
DialogueManager.cs (JSON 읽기)
    │
    ▼
DialoguePresenter.cs (화면에 보여주기)
    │
    ├── NameText (이름창)
    ├── DialogueText (대사창)
    ├── BackgroundImage (배경)
    └── CharacterImages (캐릭터 초상화)
```

### 3-4. Dialogue → Choice

대사가 끝나면 선택지가 필요한 장면에서 선택지 버튼을 보여줍니다.

```text
DialoguePresenter
    │
    ▼ (대사 끝남)
ChoiceManager.cs
    │
    ├── 선택지 버튼 1
    ├── 선택지 버튼 2
    └── 선택지 버튼 3
```

### 3-5. Choice → Branch

선택지를 누르면 다음 장면으로 이동하거나 호감도가 바뀝니다.

```text
ChoiceManager
    │
    ▼ (선택 완료)
BranchManager.cs
    │
    ├── 호감도 변경
    ├── 플래그 설정
    └── 다음 장면 번호 계산
```

### 3-6. Branch → Ending

특정 조건을 만족하면 엔딩으로 이동합니다.

```text
BranchManager
    │
    ▼ (조건 확인)
EndingManager.cs
    │
    ├── 카셀 호감도 높음 → 카셀 해피엔딩
    ├── 독립 점수 높음 → 독립 엔딩
    └── 기본 → 배드엔딩
```

---

## 4. AI 대사 생성 시스템

### 4-1. 핵심 아이디어

시놉시스는 뼈대이고, 실제 게임 대사는 AI가 자유롭게 생성합니다.

```text
시놉시스 (뼈대)
    │
    ▼
AI 프롬프트 (장면 정보 + 캐릭터 설정 + 분위기)
    │
    ▼
AI 생성 대사 (자연스러운 미연시 대화)
    │
    ▼
JSON 변환 (게임 데이터 형식)
    │
    ▼
Unity 로드 (화면에 표시)
```

### 4-2. AI에게 주는 프롬프트 구조

```text
[시스템 설정]
너는 미연시 게임 대사를 쓰는 작가야.

[캐릭터 정보]
- 이네스: 회귀한 공녀. 차갑고 단호하지만 내면에는 상처가 있음.
- 릴리: 성녀처럼 보이는 악역. 겉은 다정하고 속은 독.
- 카셀: 후회하는 영웅. 이네스를 죽음으로 몰아넣은 과거가 있음.
- 알렉산드로: 집착하는 흑막 공작. 위험하고 소유욕이 강함.
- 세자르: 이네스를 황후로 세우고 싶어 하는 황제 후보.
- 타국 왕자: 존중을 바탕으로 한 적극적인 구애자.

[장면 정보]
- 챕터: 2장 황궁 연회
- 배경: 황궁 대연회장
- 분위기: 긴장감, 경계심, 화려함 속의 위험

[대사 생성 지시]
이 장면의 대사를 미연시 형식으로 써줘.
- 짧고 읽기 좋게
- 선택지 2개 이상 포함
- 캐릭터 감정을 표현에 반영
- JSON 형식으로 출력
```

### 4-3. AI 생성 → JSON 변환 흐름

```text
1단계: AI가 장면별 대사를 생성
2단계: 생성된 대사를 JSON 형식으로 정리
3단계: Unity에서 JSON 파일을 Resources 폴더에 넣기
4단계: DialogueManager가 JSON을 읽어서 화면에 표시
```

### 4-4. AI가 자유롭게 바꿀 수 있는 것

```text
대사 내용 (문장 표현)
감정 묘사
분위기 연출
선택지 텍스트
나레이션 문체
캐릭터 말투
```

### 4-5. AI가 바꾸면 안 되는 것 (고정 설정)

```text
이네스는 회귀한 공녀
릴리는 악역
카셀은 후회하는 영웅
알렉산드로는 집착하는 흑막
주요 사건 순서 (독 → 누명 → 회귀 → 복수)
최종 결말 방향 (해피엔딩 / 배드엔딩 / 독립엔딩)
```

---

## 5. 캐릭터 설정

### 5-1. 주요 캐릭터

| 이름 | 외형 | 역할 | 말투 |
|------|------|------|------|
| 이네스 | 금발, 아쿠아색 눈 | 회귀한 공녀, 주인공 | 차갑고 단호, 내면의 상처 |
| 릴리 | 붉은 머리, 초록 눈 | 성녀 행세 악역 | 겉 다정, 속 독 |
| 카셀 | 검은 머리, 푸른 눈 | 후회하는 영웅 | 무뚝뚝하지만 흔들림 |
| 알렉산드로 | 푸른 머리, 회색 눈 | 집착하는 흑막 공작 | 차갑고 위험한 미소 |
| 세자르 | 금발, 회색 눈 | 황제 후보 | 정중하고 따뜻 |
| 타국 왕자 | 흑발, 초록 눈 | 적극적 구애자 | 여유롭고 존중 |

### 5-2. 보조 캐릭터

| 이름 | 역할 |
|------|------|
| 마리 | 이네스의 하녀, 충직한 동반자 |
| 브라운 공작 | 이네스의 아버지 |
| 브라운 공작 부인 | 이네스의 어머니 |
| 에나 | 릴리에게 학대당한 하녀 |

### 5-3. 캐릭터 감정 상태

각 캐릭터는 여러 표정을 가질 수 있습니다.

```text
calm       = 평온
happy      = 행복
sad        = 슬픔
angry      = 화남
surprised  = 놀람
scared     = 두려움
cold       = 차가움
smirk      = 비웃음
worried    = 걱정
determined = 결의
```

---

## 6. 배경 설정

### 6-1. 배경 목록

| 배경 ID | 설명 | 사용 장면 |
|---------|------|-----------|
| bg_palace_ballroom | 황궁 대연회장 | 연회 장면 |
| bg_dark_prison | 지하 감옥 | 수감 장면 |
| bg_brown_bedroom | 갈색 침실 | 이네스 침실 |
| bg_study | 서재 | 기록 정리 |
| bg_garden | 황궁 정원 | 대화 장면 |
| bg_battlefield | 전장 | 마물 전투 |
| bg_castle_gate | 성문 | 외부 이동 |
| bg_throne_room | 옥좌실 | 공개 석상 |
| bg_village | 마을 | 평화 장면 |
| bg_sacred_temple | 신전 | 성력 각성 |
| bg_eskal_mansion | 에스칼 저택 | 알렉산드로 감금 |
| bg_execution_square | 처형 광장 | 회귀 전 최후 |

---

## 7. 엔딩 구조

### 7-1. 엔딩 종류

| 엔딩 ID | 제목 | 조건 |
|---------|------|------|
| ending_cassel_happy | 카셀 해피엔딩 | 카셀 호감도 최고 + 카셀 고백 수락 |
| ending_cassel_normal | 카셀 노말엔딩 | 카셀 호감도 중간 |
| ending_independence | 독립 엔딩 | 독립 점수 최고 + 비혼 유지 |
| ending_cesar | 세자르 엔딩 | 세자르 호감도 최고 |
| ending_prince | 타국 왕자 엔딩 | 타국 왕자 호감도 최고 |
| ending_bad | 배드엔딩 | 모든 호감도 낮음 |
| ending_revenge | 복수 엔딩 | 릴리 몰락 + 독립 |
| ending_alexander | 알렉산드로 엔딩 | 알렉산드로 호감도 최고 (숨겨진 엔딩) |

### 7-2. 엔딩 분기 흐름

```text
최종 챕터 (Chapter 15)
    │
    ▼
호감도 + 선택 기록 확인
    │
    ├── 카셀 호감도 최고 → 카셀 엔딩 분기
    │   ├── 고백 수락 → 해피엔딩
    │   └── 고백 거절 → 노말엔딩
    │
    ├── 세자르 호감도 최고 → 세자르 엔딩
    │
    ├── 타국 왕자 호감도 최고 → 왕자 엔딩
    │
    ├── 독립 점수 최고 → 독립 엔딩
    │
    └── 기본 → 배드엔딩
```

---

## 8. 전체 기능 흐름도

```text
[게임 시작]
    │
    ▼
[타이틀 화면] ─── 설정 화면
    │
    ├── 처음부터
    ├── 불러오기
    └── 종료
    │
    ▼
[장면 로딩]
    │
    ▼
[배경 표시] + [캐릭터 표시]
    │
    ▼
[대사 출력] ←── [타이핑 효과]
    │
    ├── [클릭으로 다음 대사]
    │
    ▼
[선택지 표시] (선택지가 있는 장면만)
    │
    ▼
[선택지 선택]
    │
    ├── [호감도 변경]
    ├── [플래그 설정]
    └── [다음 장면 이동]
    │
    ▼
[장면 이동] ─── 챕터 이동
    │
    ▼
[엔딩 조건 확인]
    │
    ├── 엔딩 도달 → [엔딩 화면]
    └── 계속 진행 → [대사 출력]으로 루프
    │
    ▼
[세이브/로드] ───任何时候 가능
```

---

## 9. Unity 프로젝트 폴더 구조

```text
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   └── SceneLoader.cs
│   ├── UI/
│   │   ├── UIManager.cs
│   │   ├── TitleScreen.cs
│   │   ├── SettingsScreen.cs
│   │   └── EndingScreen.cs
│   ├── Dialogue/
│   │   ├── DialogueManager.cs
│   │   ├── DialoguePresenter.cs
│   │   ├── DialogueData.cs
│   │   └── TypingEffect.cs
│   ├── Choice/
│   │   ├── ChoiceManager.cs
│   │   └── ChoiceButton.cs
│   ├── Character/
│   │   ├── CharacterManager.cs
│   │   └── CharacterImage.cs
│   ├── Background/
│   │   └── BackgroundManager.cs
│   ├── Save/
│   │   ├── SaveManager.cs
│   │   └── SaveData.cs
│   └── Affection/
│       └── AffectionManager.cs
├── Resources/
│   ├── DialogueData/
│   │   ├── chapter01.json
│   │   ├── chapter02.json
│   │   └── ...
│   ├── Backgrounds/
│   │   ├── bg_palace_ballroom.png
│   │   ├── bg_dark_prison.png
│   │   └── ...
│   └── Characters/
│       ├── ines_calm.png
│       ├── ines_angry.png
│       ├── lily_smirk.png
│       └── ...
├── Fonts/
│   └── Maplestory Light.ttf
├── Scenes/
│   ├── TitleScene.unity
│   ├── GameScene.unity
│   └── EndingScene.unity
└── Prefabs/
    ├── DialoguePanel.prefab
    ├── ChoiceButton.prefab
    └── CharacterSlot.prefab
```

---

## 10. JSON 파일 예시 (Chapter 01)

```json
{
  "chapterId": 1,
  "chapterTitle": "이번에는 아무도 못 건드린다",
  "scenes": [
    {
      "sceneId": "01_001",
      "background": "bg_dark_prison",
      "characters": [],
      "lines": [
        {
          "speaker": "",
          "text": "황궁의 지하 감옥. 창문 하나 없는 돌방.",
          "isNarration": true,
          "emotion": ""
        },
        {
          "speaker": "",
          "text": "바닥은 축축했고, 공기는 무거웠다.",
          "isNarration": true,
          "emotion": ""
        },
        {
          "speaker": "이네스",
          "text": "또 이 꿈을 꾸는 건가. 아니, 꿈이 아니겠지.",
          "isNarration": false,
          "emotion": "cold"
        }
      ],
      "choices": [],
      "nextScene": "01_002"
    },
    {
      "sceneId": "01_002",
      "background": "bg_dark_prison",
      "characters": ["alexander"],
      "lines": [
        {
          "speaker": "알렉산드로",
          "text": "혼자 손님을 맞아주다니, 영애님은 인기가 없군요.",
          "isNarration": false,
          "emotion": "smirk"
        },
        {
          "speaker": "이네스",
          "text": "여기엔 왜 왔어요?",
          "isNarration": false,
          "emotion": "cold"
        },
        {
          "speaker": "알렉산드로",
          "text": "당연히 널 보러 왔지.",
          "isNarration": false,
          "emotion": "smirk"
        }
      ],
      "choices": [
        {
          "text": "차갑게 거절한다",
          "nextScene": "01_003a",
          "affection": { "알렉산드로": -1 },
          "flag": { "key": "alexander_reject_01", "value": true }
        },
        {
          "text": "정보를 빼낸다",
          "nextScene": "01_003b",
          "affection": { "알렉산드로": 0 },
          "flag": { "key": "alexander_info_01", "value": true }
        }
      ],
      "nextScene": ""
    }
  ]
}
```

---

## 11. 개발 단계 요약

### Phase 1: 기본 화면 (기능 1~6)

```text
1. 기본 화면 만들기 (Canvas, 대사창, 이름창)
2. 대사 데이터 구조 만들기
3. 대사 출력
4. 클릭으로 다음 대사 넘기기
5. 이름 표시
6. 타이핑 효과
```

### Phase 2: 캐릭터와 배경 (기능 7~9)

```text
7. 배경 이미지 변경
8. 캐릭터 초상화 변경
9. 캐릭터 숨기기
```

### Phase 3: 선택지와 분기 (기능 10~14)

```text
10. 선택지 버튼
11. 선택에 따른 분기
12. 호감도 저장
13. 호감도 확인 UI
14. 장면 이동 시스템
```

### Phase 4: 챕터와 엔딩 (기능 15~17)

```text
15. 챕터 이동
16. 엔딩 분기
17. 엔딩 화면
```

### Phase 5: 저장과 설정 (기능 18~22)

```text
18. 세이브 데이터 구조
19. 세이브
20. 로드
21. 타이틀 화면
22. 설정 화면
```

### Phase 6: 아트 에셋 (기능 23~28)

```text
23. 배경 이미지 제작/적용
24. 캐릭터 초상화 제작/적용
25. UI 테마/스킨
26. 타이틀 화면 아트
27. 엔딩 화면 아트
28. 선택지 버튼 디자인
```

### Phase 7: 오디오 (기능 29~32)

```text
29. 배경음악 시스템
30. 효과음 시스템
31. 캐릭터 보이스 (선택)
32. 볼륨 조절 UI
```

### Phase 8: AI 대사 생성 (기능 33~35)

```text
33. AI 프롬프트 템플릿 만들기
34. AI 생성 대사 → JSON 변환
35. 전체 챕터 대사 데이터 완성
```

### Phase 9: 전체 테스트 (기능 36)

```text
36. 전체 플레이테스트
```

---

## 12. 파일 목록 정리

### 시놉시스 파일

```text
series_synopsis.md  = 전체 줄거리
chapters.md         = 챕터 구성
chapter01.md        = 회귀 전 (독이 든 잔)
chapter02.md        = 황궁 연회
chapter03.md        = 악행 조사
chapter04.md        = 약혼 파기
chapter05~15.md     = 이후 전개 (나중에 추가)
```

### 기획 파일

```text
VisualNovel_Architecture.md     = 이 파일 (전체 아키텍처)
VisualNovel_FeatureList.md      = 확장 기능 목록
VisualNovel_Feature01_Prompt.md = 1번 기능 프롬프트
VisualNovel_Feature02_Prompt.md = 2번 기능 프롬프트
... (기능별 프롬프트)
VisualNovel_Feature36_Prompt.md = 36번 기능 프롬프트
```

---

## 13. 초보자를 위한 용어 정리

| 영어 용어 | 한국말 뜻 |
|-----------|-----------|
| Canvas | UI를 올리는 큰 종이 |
| RectTransform | UI의 위치와 크기 |
| Component | 오브젝트에 붙이는 기능 부품 |
| SerializeField | Inspector에서 볼 수 있게 해주는 표시 |
| MonoBehaviour | Unity 스크립트의 기본 형태 |
| Prefab | 미리 만들어둔 오브젝트 틀 |
| Scene | 게임 화면 하나 |
| Resources | 코드에서 이름으로 찾을 수 있는 파일 모음 |
| JSON | 데이터를 구조화하는 파일 형식 |
| Branch | 선택에 따라 갈라지는 이야기 |
| Affection | 호감도 |
| Flag | 게임 중 설정되는 조건 표시 |
| NPC | 플레이어가 조종하지 않는 캐릭터 |
| UI | 사용자가 보는 화면 요소 |
| Asset | 게임에 쓰이는 그림, 소리, 코드 등 |

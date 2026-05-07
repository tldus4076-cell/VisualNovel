# Unity 6 미연시 장면 단위 기획서

이 문서는 웹소설 기반 회귀 서사를 Unity 6에서 구현하기 좋은 장면 단위 구조로 정리한 기획서입니다.

초보자가 만들기 쉽게 각 장면을 `장면 번호`, `대사`, `배경`, `캐릭터 이미지`, `선택지`, `호감도`, `엔딩 분기` 기준으로 나누었습니다.

| 1. 챕터 구분 | 2. 장면 번호 | 3. 화자 이름 | 4. 대사 내용 | 5. 배경 이미지 이름 | 6. 캐릭터 이미지 이름 | 7. 선택지 여부 | 8. 선택지 내용 | 9. 선택 후 이동할 다음 장면 번호 | 10. 호감도 변화 | 11. 엔딩 분기 여부 |
|---|---:|---|---|---|---|---|---|---|---|---|
| Chapter 01 회귀 | 001 | 나레이션 | 이네스는 죽기 전 기억을 안고 침실에서 눈을 떴다. | bg_brown_bedroom_morning | ines_sleep_shock | 없음 | 없음 | 002 | 없음 | 없음 |
| Chapter 01 회귀 | 002 | 이네스 | "...회귀했네." | bg_brown_bedroom_morning | ines_serious | 없음 | 없음 | 003 | 없음 | 없음 |
| Chapter 01 회귀 | 003 | 이네스 | "이번 생에는 누구도 믿지 않아. 누구의 아내도 되지 않을 거야." | bg_brown_bedroom_morning | ines_determined | 없음 | 없음 | 004 | 없음 | 없음 |
| Chapter 01 회귀 | 004 | 마리 | "아가씨, 오늘 저녁 황궁 소연회 초대가 있습니다." | bg_brown_bedroom_morning | mari_normal | 없음 | 없음 | 005 | 없음 | 없음 |
| Chapter 01 회귀 | 005 | 이네스 | "가장 비싼 드레스와 보석을 준비해." | bg_dress_room | ines_confident | 있음 | 1. 화려하게 꾸민다 / 2. 수수하게 간다 | 006 / 006 | 1 선택: 이네스 자신감+1 | 없음 |
| Chapter 01 회귀 | 006 | 마리 | "손님이 오셨습니다. 카셀 공작님과 알렉산드로 공작님입니다." | bg_dress_room | mari_surprised | 없음 | 없음 | 007 | 없음 | 없음 |
| Chapter 01 회귀 | 007 | 나레이션 | 카셀과 알렉산드로가 응접실에 함께 나타났다. 둘 다 회귀한 듯했다. | bg_brown_parlor | cassel_regret, alexandro_smile | 없음 | 없음 | 008 | 없음 | 없음 |
| Chapter 01 회귀 | 008 | 카셀 | "오늘 황궁에 가지 마라." | bg_brown_parlor | cassel_serious | 있음 | 1. 차갑게 거절한다 / 2. 이유를 묻는다 | 009 / 010 | 1 선택: 카셀-1 / 2 선택: 카셀+1 | 없음 |
| Chapter 01 회귀 | 009 | 이네스 | "명령하지 마세요. 이번 생의 저는 당신 약혼자가 아닙니다." | bg_brown_parlor | ines_cold | 없음 | 없음 | 011 | 카셀-1 | 없음 |
| Chapter 01 회귀 | 010 | 이네스 | "릴리가 독을 마시니까요?" | bg_brown_parlor | ines_suspicious | 없음 | 없음 | 011 | 카셀+1 | 없음 |
| Chapter 01 회귀 | 011 | 나레이션 | 금빛 보호막이 나타나 카셀과 알렉산드로의 접근을 막았다. | bg_brown_parlor_magic | shield_gold | 없음 | 없음 | 012 | 없음 | 없음 |
| Chapter 01 회귀 | 012 | 이네스 | "카셀과의 약혼은 파기하겠습니다. 그리고 저는 비혼으로 살 거예요." | bg_brown_parlor | ines_declaring | 없음 | 없음 | 013 | 카셀-1 | 없음 |
| Chapter 01 회귀 | 013 | 알렉산드로 | "릴리를 처음부터 악녀로 까발리는 것. 그것이 우리가 할 수 있는 일입니다." | bg_brown_parlor | alexandro_serious | 있음 | 1. 협력은 하되 믿지 않는다 / 2. 완전히 거절한다 | 014 / 014 | 1 선택: 알렉산드로+1 / 2 선택: 알렉산드로-1 | 없음 |
| Chapter 01 회귀 | 014 | 이네스 | "릴리는 내가 무너뜨릴 거예요. 당신들은 쓸모가 있으면 쓰고, 아니면 버릴 겁니다." | bg_brown_parlor | ines_confident | 없음 | 없음 | 015 | 없음 | 없음 |
| Chapter 02 황궁 연회 | 015 | 나레이션 | 황궁 연회장. 이네스는 릴리의 첫 거짓말을 기다렸다. | bg_palace_ballroom | ines_blue_dress | 없음 | 없음 | 016 | 없음 | 없음 |
| Chapter 02 황궁 연회 | 016 | 릴리 | "브라운 공녀님. 다시 뵙게 되어 기뻐요." | bg_palace_ballroom | lily_fake_smile | 있음 | 1. 정중하게 대한다 / 2. 차갑게 받아친다 | 017 / 018 | 1 선택: 세자르+1 / 2 선택: 릴리경계+1 | 없음 |
| Chapter 02 황궁 연회 | 017 | 이네스 | "저도 오늘 밤이 흥미로울 것 같네요." | bg_palace_ballroom | ines_smile | 없음 | 없음 | 019 | 세자르+1 | 없음 |
| Chapter 02 황궁 연회 | 018 | 이네스 | "저는 그리 기쁘진 않네요, 공주님." | bg_palace_ballroom | ines_cold | 없음 | 없음 | 019 | 릴리경계+1 | 없음 |
| Chapter 02 황궁 연회 | 019 | 나레이션 | 릴리는 회랑에서 하녀의 손목을 몰래 비틀었다. | bg_palace_corridor | lily_cruel, maid_hurt | 없음 | 없음 | 020 | 없음 | 없음 |
| Chapter 02 황궁 연회 | 020 | 이네스 | "공주님. 하녀를 다루는 방식이 꽤 거칠군요." | bg_palace_corridor | ines_serious | 있음 | 1. 바로 기록 마법을 공개한다 / 2. 릴리가 더 말하게 기다린다 | 021 / 022 | 1 선택: 진실점수+1 / 2 선택: 전략점수+1 | 없음 |
| Chapter 02 황궁 연회 | 021 | 이네스 | "기억이 안 나시나 봐요. 그래서 제가 보여 드리는 중입니다." | bg_palace_corridor_magic | ines_magic | 없음 | 없음 | 023 | 진실점수+1 | 없음 |
| Chapter 02 황궁 연회 | 022 | 릴리 | "저는 그 아이를 붙잡아 준 것뿐이에요." | bg_palace_corridor | lily_cry_fake | 없음 | 없음 | 023 | 전략점수+1 | 없음 |
| Chapter 02 황궁 연회 | 023 | 카셀 | "또?" | bg_palace_corridor | cassel_cold | 없음 | 없음 | 024 | 카셀+1 | 없음 |
| Chapter 02 황궁 연회 | 024 | 세자르 | "황궁 안에서 시종을 함부로 다루는 건 그냥 넘길 일이 아닙니다." | bg_palace_corridor | caesar_serious | 없음 | 없음 | 025 | 세자르+1 | 없음 |
| Chapter 02 황궁 연회 | 025 | 이네스 | "이번 생에는 보는 눈이 많거든요." | bg_palace_corridor | ines_victory | 없음 | 없음 | 026 | 진실점수+1 | 없음 |
| Chapter 03 증거 수집 | 026 | 나레이션 | 다음 날, 브라운 공작가 응접실은 전쟁 회의실처럼 변했다. | bg_brown_parlor_morning | ines_document | 없음 | 없음 | 027 | 없음 | 없음 |
| Chapter 03 증거 수집 | 027 | 이네스 | "감정이 아니라 서류로 끝낼 거예요." | bg_brown_parlor_morning | ines_serious | 없음 | 없음 | 028 | 없음 | 없음 |
| Chapter 03 증거 수집 | 028 | 에나 | "정말... 지켜 주시나요?" | bg_palace_small_room | ena_afraid | 있음 | 1. 부드럽게 안심시킨다 / 2. 바로 증언을 요구한다 | 029 / 030 | 1 선택: 진실점수+1, 세자르+1 / 2 선택: 전략점수+1 | 없음 |
| Chapter 03 증거 수집 | 029 | 이네스 | "응. 브라운 공작가 이름으로 약속할게." | bg_palace_small_room | ines_kind | 없음 | 없음 | 031 | 진실점수+1 | 없음 |
| Chapter 03 증거 수집 | 030 | 이네스 | "네가 본 것을 과장도 축소도 없이 말해 줘." | bg_palace_small_room | ines_serious | 없음 | 없음 | 031 | 전략점수+1 | 없음 |
| Chapter 03 증거 수집 | 031 | 에나 | "사라진 하녀들이 있었어요. 명단에서도 이름이 지워졌어요." | bg_palace_small_room | ena_cry | 없음 | 없음 | 032 | 없음 | 없음 |
| Chapter 03 증거 수집 | 032 | 세자르 | "황궁 내부 기록은 내가 건드려 보지." | bg_palace_hall_day | caesar_normal | 있음 | 1. 세자르에게 협조를 요청한다 / 2. 브라운 가문만 믿는다 | 033 / 034 | 1 선택: 세자르+2 / 2 선택: 세자르-1 | 없음 |
| Chapter 03 증거 수집 | 033 | 이네스 | "사본만 확보해 주세요. 원본은 건드리지 말고요." | bg_palace_hall_day | ines_calm | 없음 | 없음 | 035 | 세자르+2 | 없음 |
| Chapter 03 증거 수집 | 034 | 이네스 | "도움은 고맙지만, 제 방식대로 하겠습니다." | bg_palace_hall_day | ines_cold | 없음 | 없음 | 035 | 세자르-1 | 없음 |
| Chapter 03 증거 수집 | 035 | 나레이션 | 릴리의 하녀 학대, 사라진 시종, 약물 구매 기록이 하나의 선으로 이어졌다. | bg_brown_library_night | documents_evidence | 없음 | 없음 | 036 | 진실점수+1 | 없음 |
| Chapter 03 증거 수집 | 036 | 이네스 | "다음은 도망갈 시간을 주지 않아." | bg_brown_library_night | ines_determined | 없음 | 없음 | 037 | 없음 | 없음 |
| Chapter 04 약혼 파기 | 037 | 마리 | "카셀 경이 찾아오셨습니다." | bg_brown_parlor_morning | mari_worried | 없음 | 없음 | 038 | 없음 | 없음 |
| Chapter 04 약혼 파기 | 038 | 카셀 | "화해할 시간이 필요해." | bg_brown_parlor_morning | cassel_regret | 있음 | 1. 기회를 주지 않는다 / 2. 증거로 행동하라고 말한다 | 039 / 040 | 1 선택: 카셀-2 / 2 선택: 카셀+1 | 없음 |
| Chapter 04 약혼 파기 | 039 | 이네스 | "내가 날 해방시키는 거야." | bg_brown_parlor_morning | ines_cold | 없음 | 없음 | 041 | 카셀-2 | 없음 |
| Chapter 04 약혼 파기 | 040 | 이네스 | "말이 아니라 증거를 가져와요." | bg_brown_parlor_morning | ines_serious | 없음 | 없음 | 041 | 카셀+1 | 없음 |
| Chapter 04 약혼 파기 | 041 | 이네스 | "저는 카셀 다잉거프 공작과의 약혼을 파기하려 합니다." | bg_brown_dinner_hall | ines_declaring | 없음 | 없음 | 042 | 없음 | 없음 |
| Chapter 04 약혼 파기 | 042 | 이네스 | "저는 비혼을 선언합니다. 제 인생은 제가 선택할 거예요." | bg_brown_dinner_hall | ines_confident | 없음 | 없음 | 043 | 없음 | 없음 |
| Chapter 04 약혼 파기 | 043 | 나레이션 | 보호막은 카셀과 릴리 앞에서 또렷하게 빛났다. | bg_brown_dinner_hall_magic | shield_gold | 없음 | 없음 | 044 | 진실점수+1 | 없음 |
| Chapter 04 약혼 파기 | 044 | 카셀 | "네가 원하는 대로 해." | bg_brown_dinner_hall | cassel_hurt | 없음 | 없음 | 045 | 카셀-1 | 없음 |
| Chapter 04 약혼 파기 | 045 | 이네스 | "저는 이제 누구의 소유도 되지 않아요. 저는 저 자신의 것이에요." | bg_brown_dinner_hall | ines_victory | 없음 | 없음 | 046 | 없음 | 없음 |
| Chapter 05 신의 축복 | 046 | 나레이션 | 제국 곳곳에 마물이 나타나고, 이네스의 축복 소문이 퍼졌다. | bg_empire_map_dark | monster_shadow | 없음 | 없음 | 047 | 없음 | 없음 |
| Chapter 05 신의 축복 | 047 | 이네스 | "숨지 않겠어. 내 힘으로 직접 막을 거야." | bg_forest_battle | ines_magic_battle | 있음 | 1. 직접 전장에 나간다 / 2. 후방에서 지원한다 | 048 / 049 | 1 선택: 세자르+1, 용기+1 / 2 선택: 전략점수+1 | 없음 |
| Chapter 05 신의 축복 | 048 | 나레이션 | 이네스는 마법으로 마물을 쓰러뜨리고 사람들의 시선을 바꾸기 시작했다. | bg_forest_battle | ines_magic_battle | 없음 | 없음 | 050 | 용기+1 | 없음 |
| Chapter 05 신의 축복 | 049 | 나레이션 | 이네스는 성력으로 부상자를 지키며 전장을 안정시켰다. | bg_field_camp | ines_heal | 없음 | 없음 | 050 | 전략점수+1 | 없음 |
| Chapter 06 전장의 신뢰 | 050 | 세자르 | "공녀는 생각보다 훨씬 강하군." | bg_field_camp | caesar_smile | 있음 | 1. 세자르와 작전을 짠다 / 2. 혼자 움직인다 | 051 / 052 | 1 선택: 세자르+2 / 2 선택: 세자르-1 | 없음 |
| Chapter 06 전장의 신뢰 | 051 | 이네스 | "전하의 병력과 제 마법을 함께 쓰면 피해를 줄일 수 있어요." | bg_field_strategy | ines_serious, caesar_serious | 없음 | 없음 | 053 | 세자르+2 | 없음 |
| Chapter 06 전장의 신뢰 | 052 | 이네스 | "제 일은 제가 처리하겠습니다." | bg_field_camp | ines_cold | 없음 | 없음 | 053 | 세자르-1 | 없음 |
| Chapter 06 전장의 신뢰 | 053 | 세자르 | "나는 그대를 황후로 세우고 싶어졌다." | bg_field_sunset | caesar_confess | 없음 | 없음 | 054 | 세자르+1 | 세자르 루트 가능 조건 시작 |
| Chapter 07 타국 왕자 | 054 | 나레이션 | 흑발과 초록 눈의 타국 왕자가 외교 사절로 입성했다. | bg_palace_gate | prince_foreign_normal | 없음 | 없음 | 055 | 없음 | 없음 |
| Chapter 07 타국 왕자 | 055 | 타국 왕자 | "공녀님을 존중합니다. 그래서 더 정중하게 원합니다." | bg_palace_garden | prince_foreign_smile | 있음 | 1. 대화를 받아들인다 / 2. 거리를 둔다 | 056 / 057 | 1 선택: 타국왕자+2 / 2 선택: 타국왕자-1 | 없음 |
| Chapter 07 타국 왕자 | 056 | 이네스 | "존중을 말한다면, 제 선택도 존중해야 합니다." | bg_palace_garden | ines_calm | 없음 | 없음 | 058 | 타국왕자+2 | 없음 |
| Chapter 07 타국 왕자 | 057 | 이네스 | "저는 누구의 황후도 될 생각이 없습니다." | bg_palace_garden | ines_cold | 없음 | 없음 | 058 | 타국왕자-1 | 없음 |
| Chapter 07 타국 왕자 | 058 | 타국 왕자 | "기다리는 것도 구애의 한 방식이지요." | bg_palace_garden_sunset | prince_foreign_soft | 없음 | 없음 | 059 | 타국왕자+1 | 타국 왕자 루트 가능 조건 시작 |
| Chapter 08 후회와 집착 | 059 | 카셀 | "내가 할 수 있는 속죄를 찾겠다." | bg_palace_corridor_night | cassel_regret | 있음 | 1. 차갑게 지나친다 / 2. 행동으로 증명하라고 한다 | 060 / 061 | 1 선택: 카셀-1 / 2 선택: 카셀+2 | 없음 |
| Chapter 08 후회와 집착 | 060 | 이네스 | "늦은 후회는 제게 필요 없어요." | bg_palace_corridor_night | ines_cold | 없음 | 없음 | 062 | 카셀-1 | 없음 |
| Chapter 08 후회와 집착 | 061 | 이네스 | "말하지 말고 행동하세요." | bg_palace_corridor_night | ines_serious | 없음 | 없음 | 062 | 카셀+2 | 없음 |
| Chapter 08 후회와 집착 | 062 | 알렉산드로 | "보호막이 없다면, 너는 내 곁에 있었을 텐데." | bg_dark_mansion | alexandro_obsessed | 없음 | 없음 | 063 | 알렉산드로위험+1 | 없음 |
| Chapter 09 납치 시도 | 063 | 나레이션 | 알렉산드로는 이네스를 납치하려는 계략을 시작했다. | bg_dark_street_night | alexandro_shadow | 없음 | 없음 | 064 | 없음 | 없음 |
| Chapter 09 납치 시도 | 064 | 이네스 | "이런 함정에 다시 걸릴 줄 알았나요?" | bg_dark_street_night | ines_magic_battle | 있음 | 1. 보호막으로 버틴다 / 2. 마법으로 반격한다 | 065 / 066 | 1 선택: 방어+1 / 2 선택: 용기+1 | 없음 |
| Chapter 09 납치 시도 | 065 | 나레이션 | 보호막이 암살자들을 밀어냈지만, 위협은 끝나지 않았다. | bg_dark_street_magic | shield_gold | 없음 | 없음 | 067 | 방어+1 | 없음 |
| Chapter 09 납치 시도 | 066 | 나레이션 | 이네스의 마법이 어둠을 갈랐다. 하지만 적은 다시 모여들었다. | bg_dark_street_magic | ines_magic_battle | 없음 | 없음 | 067 | 용기+1 | 없음 |
| Chapter 09 납치 시도 | 067 | 나레이션 | 카셀, 세자르, 타국 왕자가 각자의 방식으로 이네스를 찾기 시작했다. | bg_empire_map_night | cassel_serious, caesar_serious, prince_foreign_serious | 없음 | 없음 | 068 | 없음 | 없음 |
| Chapter 10 구출 | 068 | 나레이션 | 알렉산드로의 감금 사건이 크게 터졌다. | bg_prison_room | ines_bound_angry | 없음 | 없음 | 069 | 없음 | 없음 |
| Chapter 10 구출 | 069 | 카셀 | "이번에는 늦지 않겠다." | bg_prison_room | cassel_battle | 없음 | 없음 | 070 | 카셀+1 | 없음 |
| Chapter 10 구출 | 070 | 이네스 | "구해 준 건 고맙지만, 그걸로 용서받을 생각은 하지 마세요." | bg_prison_room | ines_cold | 있음 | 1. 감사만 전한다 / 2. 사과와 책임을 요구한다 | 071 / 072 | 1 선택: 카셀+1 / 2 선택: 카셀+2, 진실점수+1 | 없음 |
| Chapter 10 구출 | 071 | 카셀 | "그 말만으로도 충분하다." | bg_prison_room | cassel_soft | 없음 | 없음 | 073 | 카셀+1 | 없음 |
| Chapter 10 구출 | 072 | 카셀 | "그래. 나는 아직 아무것도 갚지 못했다." | bg_prison_room | cassel_regret | 없음 | 없음 | 073 | 카셀+2 | 카셀 루트 가능 조건 시작 |
| Chapter 10 구출 | 073 | 알렉산드로 | "아직 끝난 게 아니야, 이네스." | bg_prison_exit_night | alexandro_obsessed | 없음 | 없음 | 074 | 알렉산드로위험+1 | 없음 |
| Chapter 11 릴리 추락 | 074 | 나레이션 | 릴리의 하녀 학대, 경쟁자 제거, 독극물 계략이 공개적으로 입증되었다. | bg_courtroom | documents_evidence | 없음 | 없음 | 075 | 진실점수+2 | 없음 |
| Chapter 11 릴리 추락 | 075 | 릴리 | "아니야... 나는 성녀야. 모두가 나를 믿어야 해!" | bg_courtroom | lily_broken | 없음 | 없음 | 076 | 릴리몰락+1 | 없음 |
| Chapter 11 릴리 추락 | 076 | 이네스 | "성녀의 가면은 끝났어요." | bg_courtroom | ines_victory | 없음 | 없음 | 077 | 진실점수+1 | 없음 |
| Chapter 12 금지된 소환 | 077 | 릴리 | "그럼 전부 무너뜨리겠어. 너부터 죽이면 돼!" | bg_ruined_capital | lily_mad | 없음 | 없음 | 078 | 없음 | 없음 |
| Chapter 12 금지된 소환 | 078 | 나레이션 | 릴리는 금지된 마물을 소환했고, 수도 전체가 위험에 빠졌다. | bg_ruined_capital | boss_monster_shadow | 없음 | 없음 | 079 | 없음 | 없음 |
| Chapter 12 금지된 소환 | 079 | 이네스 | "이번에는 내가 도망치지 않아." | bg_ruined_capital | ines_magic_battle | 있음 | 1. 카셀과 함께 싸운다 / 2. 세자르와 지휘한다 / 3. 타국 왕자와 시민을 구한다 | 080 / 081 / 082 | 1 선택: 카셀+2 / 2 선택: 세자르+2 / 3 선택: 타국왕자+2 | 엔딩 후보 결정 시작 |
| Chapter 13 보스 결전 | 080 | 카셀 | "네 앞의 공격은 내가 막는다." | bg_boss_battle | cassel_injured | 없음 | 없음 | 083 | 카셀+2 | 카셀 엔딩 조건 강화 |
| Chapter 13 보스 결전 | 081 | 세자르 | "공녀, 그대의 명령이라면 전장이 움직인다." | bg_boss_battle | caesar_battle | 없음 | 없음 | 083 | 세자르+2 | 세자르 엔딩 조건 강화 |
| Chapter 13 보스 결전 | 082 | 타국 왕자 | "그대가 지키는 사람들을 나도 지키겠습니다." | bg_boss_battle | prince_foreign_battle | 없음 | 없음 | 083 | 타국왕자+2 | 타국 왕자 엔딩 조건 강화 |
| Chapter 13 보스 결전 | 083 | 나레이션 | 보스 마물의 공격이 이네스를 향해 날아왔다. | bg_boss_battle | boss_monster_attack | 없음 | 없음 | 084 | 없음 | 없음 |
| Chapter 13 보스 결전 | 084 | 카셀 | "이번에는 내가 대신 맞겠다." | bg_boss_battle | cassel_protect | 없음 | 없음 | 085 | 카셀+3 | 카셀 해피엔딩 핵심 조건 |
| Chapter 14 성력과 고백 | 085 | 이네스 | "죽지 마요. 아직 당신에게 들을 말이 남아 있어요." | bg_battle_after | ines_heal | 없음 | 없음 | 086 | 카셀+1 | 없음 |
| Chapter 14 성력과 고백 | 086 | 카셀 | "나는 네 죽음 앞에서 비겁했다. 이번 생에는 도망치지 않겠다." | bg_battle_after | cassel_confess | 있음 | 1. 사과를 받아들인다 / 2. 아직 시간을 달라고 한다 / 3. 끝까지 거절한다 | 087 / 088 / 089 | 1 선택: 카셀+3 / 2 선택: 카셀+1 / 3 선택: 카셀-3 | 최종 엔딩 분기 |
| Chapter 15 엔딩 | 087 | 이네스 | "쉽게 용서하진 않겠지만, 다시 선택해 볼게요." | bg_palace_garden_sunrise | ines_soft, cassel_soft | 없음 | 없음 | END_CASSEL_HAPPY | 카셀+3 | 카셀 해피엔딩 |
| Chapter 15 엔딩 | 088 | 이네스 | "나는 아직 나로 살 시간이 필요해요." | bg_brown_garden | ines_free | 없음 | 없음 | END_NORMAL_FREE | 독립+3 | 자유 노멀엔딩 |
| Chapter 15 엔딩 | 089 | 이네스 | "당신의 속죄는 당신 몫이에요. 저는 제 길을 갈게요." | bg_brown_garden | ines_cold | 없음 | 없음 | END_SOLO | 독립+5 | 독립 엔딩 |
| Chapter 15 엔딩 | 090 | 세자르 | "그대가 원한다면, 황후의 자리는 비워 두겠다." | bg_palace_throne | caesar_soft | 조건부 | 세자르 호감도 최고일 때 자동 이동 | END_CAESAR | 세자르+3 | 세자르 엔딩 |
| Chapter 15 엔딩 | 091 | 타국 왕자 | "그대가 선택하지 않는다면, 나는 물러나겠습니다." | bg_foreign_embassy | prince_foreign_soft | 조건부 | 타국왕자 호감도 최고일 때 자동 이동 | END_FOREIGN_PRINCE | 타국왕자+3 | 타국 왕자 엔딩 |
| Chapter 15 엔딩 | 092 | 알렉산드로 | "끝내 손에 넣지 못했군." | bg_dark_mansion_night | alexandro_defeated | 조건부 | 알렉산드로위험 수치가 높을 때 표시 | END_ALEXANDRO_BAD | 알렉산드로위험+3 | 배드엔딩 가능 |

## Unity에서 쓰는 방법

- `장면 번호`는 Unity에서 다음 대사를 찾는 주소입니다.
- `선택지 내용`은 버튼으로 만들면 됩니다.
- `선택 후 이동할 다음 장면 번호`는 버튼을 눌렀을 때 이동할 장면입니다.
- `호감도 변화`는 `casselLove += 1` 같은 변수로 저장하면 됩니다.
- `엔딩 분기 여부`는 마지막에 어떤 엔딩으로 갈지 판단하는 조건입니다.

using System.Collections.Generic; // List를 사용하기 위해 필요합니다. List는 여러 개를 담는 상자입니다.
using UnityEngine; // Unity의 기본 기능을 사용하기 위해 필요합니다.

// 같은 오브젝트에 이 스크립트가 2개 붙는 실수를 막아 줍니다.
[DisallowMultipleComponent]
public class VisualNovelDialogueDatabase : MonoBehaviour
{
    // DialogueLine은 대사 한 줄을 담는 작은 상자입니다.
    // Serializable을 붙이면 Inspector에서 안쪽 내용을 볼 수 있습니다.
    [System.Serializable]
    public class DialogueLine
    {
        public int chapterNumber; // 몇 번째 챕터인지 저장합니다. 예: 1
        public int sceneNumber; // 몇 번째 장면인지 저장합니다. 예: 1, 2, 3
        public string speakerName; // 말하는 사람 이름입니다. 예: 이네스, 릴리
        [TextArea(2, 5)] public string dialogueText; // 실제 대사 내용입니다. 긴 글을 보기 좋게 입력할 수 있습니다.
        public string backgroundName; // 나중에 배경 그림을 찾을 때 사용할 이름입니다. 예: bg_palace_ballroom
        public string characterName; // 나중에 캐릭터 그림을 찾을 때 사용할 이름입니다. 예: ines, lily
        public bool isNarration; // 나레이션이면 true, 캐릭터 대사면 false입니다.
    }

    [Header("테스트용 대사 데이터")]
    [SerializeField] private List<DialogueLine> dialogueLines = new List<DialogueLine>(); // 대사 여러 줄을 담는 큰 상자입니다.

    private void Awake()
    {
        // Play 버튼을 눌렀을 때 테스트용 대사 3개를 준비합니다.
        // 아직 화면에 보여주지는 않습니다.
        CreateTestDialogueLines();
    }

    private void Start()
    {
        // Start는 Awake 다음에 Unity가 자동으로 불러주는 함수입니다.
        // 이번 단계에서는 대사가 잘 담겼는지 Console에만 확인합니다.
        PrintDialogueTestLog();
    }

    private void CreateTestDialogueLines()
    {
        // Play를 다시 눌렀을 때 대사가 중복으로 쌓이지 않게 먼저 비웁니다.
        dialogueLines.Clear();

        // chapter01.md 초반 나레이션입니다.
        dialogueLines.Add(CreateDialogueLine(
            1,
            1,
            "나레이션",
            "황궁의 연회장은 눈부실 만큼 화려했다.",
            "bg_palace_ballroom",
            "",
            true
        ));

        // chapter01.md 초반 릴리 대사입니다.
        dialogueLines.Add(CreateDialogueLine(
            1,
            2,
            "릴리",
            "이네스 영애님, 오늘 많이 힘드신 것 같네요.",
            "bg_palace_ballroom",
            "lily",
            false
        ));

        // chapter01.md 초반 이네스 대사입니다.
        dialogueLines.Add(CreateDialogueLine(
            1,
            3,
            "이네스",
            "별 말씀을요, 공주님께서 먼저 찾아주시는 게 영광이에요.",
            "bg_palace_ballroom",
            "ines",
            false
        ));
    }

    private DialogueLine CreateDialogueLine(int chapterNumber, int sceneNumber, string speakerName, string dialogueText, string backgroundName, string characterName, bool isNarration)
    {
        // 대사 한 줄을 담을 새 상자를 만듭니다.
        DialogueLine line = new DialogueLine();

        // 몇 번째 챕터인지 넣습니다.
        line.chapterNumber = chapterNumber;

        // 몇 번째 장면인지 넣습니다.
        line.sceneNumber = sceneNumber;

        // 말하는 사람 이름을 넣습니다.
        line.speakerName = speakerName;

        // 실제 대사 내용을 넣습니다.
        line.dialogueText = dialogueText;

        // 나중에 배경 이미지를 찾을 이름을 넣습니다.
        line.backgroundName = backgroundName;

        // 나중에 캐릭터 이미지를 찾을 이름을 넣습니다.
        line.characterName = characterName;

        // 나레이션인지 아닌지 표시합니다.
        line.isNarration = isNarration;

        // 완성된 대사 한 줄을 돌려줍니다.
        return line;
    }

    private void PrintDialogueTestLog()
    {
        // 대사 데이터가 몇 개 들어 있는지 Console에 보여줍니다.
        Debug.Log("2번 기능 테스트: 대사 데이터 " + dialogueLines.Count + "개가 준비되었습니다.");

        // 대사 상자 안에 들어 있는 내용을 하나씩 Console에 보여줍니다.
        for (int i = 0; i < dialogueLines.Count; i++)
        {
            // i번째 대사를 꺼냅니다.
            DialogueLine line = dialogueLines[i];

            // 사람이 읽기 쉬운 문장으로 Console에 출력합니다.
            Debug.Log(
                "장면 " + line.sceneNumber +
                " / 챕터 " + line.chapterNumber +
                " / 화자 " + line.speakerName +
                " / 배경 " + line.backgroundName +
                " / 캐릭터 " + line.characterName +
                " / 나레이션 " + line.isNarration +
                " / 대사 " + line.dialogueText
            );
        }
    }

    public List<DialogueLine> GetDialogueLines()
    {
        // 나중에 3번 기능에서 대사 출력 스크립트가 이 함수를 사용해 대사 목록을 가져갈 수 있습니다.
        return dialogueLines;
    }
}

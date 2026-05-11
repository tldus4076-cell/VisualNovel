using System.Collections.Generic; // List를 사용하기 위해 필요합니다. List는 여러 개를 담는 상자입니다.
using TMPro; // TextMeshPro 글자를 사용하기 위해 필요합니다.
using UnityEngine; // Unity의 기본 기능을 사용하기 위해 필요합니다.

// 같은 오브젝트에 이 스크립트가 2개 붙는 실수를 막아 줍니다.
[DisallowMultipleComponent]
public class VisualNovelDialoguePresenter : MonoBehaviour
{
    [Header("대사 데이터")]
    [SerializeField] private VisualNovelDialogueDatabase dialogueDatabase; // 2번 기능에서 만든 대사 데이터 상자입니다.

    [Header("화면 글자")]
    [SerializeField] private TMP_Text nameText; // 이름창에 있는 글자입니다.
    [SerializeField] private TMP_Text dialogueText; // 대사창에 있는 글자입니다.

    private List<VisualNovelDialogueDatabase.DialogueLine> dialogueLines; // 실제로 사용할 대사 목록입니다.
    private int currentDialogueIndex; // 지금 몇 번째 대사를 보고 있는지 기억하는 숫자입니다.

    private void Start()
    {
        // Start는 Play 버튼을 눌렀을 때 Unity가 자동으로 불러주는 함수입니다.
        // 1번 기능의 화면 만들기와 2번 기능의 데이터 만들기가 끝난 뒤 실행됩니다.
        PrepareReferences();
        PrepareDialogueLines();
        ShowCurrentDialogue();
    }

    private void Update()
    {
        // Update는 게임이 켜져 있는 동안 매 프레임마다 계속 실행되는 함수입니다.
        // 프레임은 게임 화면이 한 번 그려지는 순간이라고 생각하면 됩니다.
        if (IsNextInput())
        {
            ShowNextDialogue();
        }
    }

    private void PrepareReferences()
    {
        // Inspector에서 대사 데이터가 연결되지 않았다면, 같은 오브젝트에서 자동으로 찾습니다.
        if (dialogueDatabase == null)
        {
            dialogueDatabase = GetComponent<VisualNovelDialogueDatabase>();
        }

        // Inspector에서 이름 글자가 연결되지 않았다면, Hierarchy에서 NameText라는 오브젝트를 찾아 연결합니다.
        if (nameText == null)
        {
            nameText = FindTextByObjectName("NameText");
        }

        // Inspector에서 대사 글자가 연결되지 않았다면, Hierarchy에서 DialogueText라는 오브젝트를 찾아 연결합니다.
        if (dialogueText == null)
        {
            dialogueText = FindTextByObjectName("DialogueText");
        }
    }

    private TMP_Text FindTextByObjectName(string objectName)
    {
        // 이름으로 게임 오브젝트를 찾습니다.
        // 예: "NameText", "DialogueText"
        GameObject foundObject = GameObject.Find(objectName);

        // 찾지 못했다면 null을 돌려줍니다.
        if (foundObject == null)
        {
            return null;
        }

        // 찾은 오브젝트에서 TextMeshPro 글자 컴포넌트를 가져옵니다.
        return foundObject.GetComponent<TMP_Text>();
    }

    private void PrepareDialogueLines()
    {
        // 대사 데이터 스크립트가 없으면 더 진행하지 않습니다.
        if (dialogueDatabase == null)
        {
            Debug.LogError("4번 기능 오류: VisualNovelDialogueDatabase를 찾지 못했습니다.");
            return;
        }

        // 2번 기능에서 만든 대사 목록을 가져옵니다.
        dialogueLines = dialogueDatabase.GetDialogueLines();

        // 처음에는 첫 번째 대사를 보여주기 위해 번호를 0으로 정합니다.
        // 컴퓨터는 첫 번째를 1이 아니라 0으로 셉니다.
        currentDialogueIndex = 0;
    }

    private bool IsNextInput()
    {
        // 마우스 왼쪽 버튼을 눌렀는지 확인합니다.
        // 0은 마우스 왼쪽 버튼이라는 뜻입니다.
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        // 모바일에서 화면 터치가 시작되었는지 확인합니다.
        // touchCount는 지금 화면을 터치하고 있는 손가락 개수입니다.
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            return true;
        }

        // 마우스 클릭도 아니고 터치도 아니면 다음으로 넘기지 않습니다.
        return false;
    }

    private void ShowNextDialogue()
    {
        // 대사 목록이 없거나 비어 있으면 다음 대사를 보여줄 수 없습니다.
        if (dialogueLines == null || dialogueLines.Count == 0)
        {
            Debug.LogError("4번 기능 오류: 대사 데이터가 비어 있습니다.");
            return;
        }

        // 지금 보고 있는 대사가 마지막 대사라면 더 넘기지 않습니다.
        if (currentDialogueIndex >= dialogueLines.Count - 1)
        {
            Debug.Log("4번 기능 테스트: 마지막 대사입니다. 아직 다음 기능은 만들지 않았습니다.");
            return;
        }

        // 현재 대사 번호를 1 올립니다.
        // 예: 0번 대사에서 1번 대사로 이동합니다.
        currentDialogueIndex++;

        // 새 현재 대사를 화면에 보여줍니다.
        ShowCurrentDialogue();
    }

    private void ShowCurrentDialogue()
    {
        // 이름 글자 또는 대사 글자를 찾지 못하면 더 진행하지 않습니다.
        if (nameText == null || dialogueText == null)
        {
            Debug.LogError("4번 기능 오류: NameText 또는 DialogueText를 찾지 못했습니다.");
            return;
        }

        // 대사 목록이 없거나 비어 있으면 보여줄 대사가 없습니다.
        if (dialogueLines == null || dialogueLines.Count == 0)
        {
            Debug.LogError("4번 기능 오류: 대사 데이터가 비어 있습니다.");
            return;
        }

        // 현재 대사 번호가 이상한 숫자가 되지 않도록 막습니다.
        if (currentDialogueIndex < 0 || currentDialogueIndex >= dialogueLines.Count)
        {
            Debug.LogError("4번 기능 오류: 현재 대사 번호가 대사 목록 범위를 벗어났습니다.");
            return;
        }

        // 현재 번호에 맞는 대사 한 줄을 꺼냅니다.
        VisualNovelDialogueDatabase.DialogueLine currentLine = dialogueLines[currentDialogueIndex];

        // 꺼낸 대사를 화면에 보여줍니다.
        ShowDialogueLine(currentLine);
    }

    private void ShowDialogueLine(VisualNovelDialogueDatabase.DialogueLine line)
    {
        // 나레이션이면 이름창을 비워 둡니다.
        // 예: 배경 설명 문장은 특정 캐릭터가 말하는 것이 아니기 때문입니다.
        if (line.isNarration)
        {
            nameText.text = "";
        }
        else
        {
            // 캐릭터 대사라면 말하는 사람 이름을 이름창에 넣습니다.
            // 5번 기능에서 이름 표시를 더 깔끔하게 다듬을 예정입니다.
            nameText.text = line.speakerName;
        }

        // 대사창에 실제 대사 내용을 넣습니다.
        dialogueText.text = line.dialogueText;

        // 테스트 확인용으로 Console에도 알려줍니다.
        Debug.Log("4번 기능 테스트: " + (currentDialogueIndex + 1) + "번째 대사를 화면에 출력했습니다. / " + line.dialogueText);
    }
}

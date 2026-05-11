using TMPro; // TextMeshPro 글자를 사용하기 위해 필요합니다.
using UnityEngine; // Unity의 기본 기능을 사용하기 위해 필요합니다.
using UnityEngine.UI; // Image, Canvas 같은 UI 기능을 사용하기 위해 필요합니다.

// 같은 오브젝트에 이 스크립트가 2개 붙는 실수를 막아 줍니다.
[DisallowMultipleComponent]
public class VisualNovelBasicScreen : MonoBehaviour
{
    [Header("테스트 글자")]
    [SerializeField] private string speakerName = "이름"; // 이름창에 잠깐 보여줄 테스트 글자입니다.
    [SerializeField] private string dialogueMessage = "여기에 대사가 표시될 예정입니다."; // 대사창에 잠깐 보여줄 테스트 글자입니다.
    [SerializeField] private string nextGuideMessage = "다음 진행 준비 중..."; // 오른쪽 아래에 보여줄 안내 글자입니다.

    [Header("한글 폰트")]
    [SerializeField] private TMP_FontAsset koreanFont; // 한글이 네모로 보이지 않게 사용할 TextMeshPro 폰트입니다.

    [Header("화면 색상")]
    [SerializeField] private Color backgroundColor = new Color(0.08f, 0.10f, 0.18f, 1f); // 배경 색입니다.
    [SerializeField] private Color dialoguePanelColor = new Color(0f, 0f, 0f, 0.75f); // 대사창 색입니다. 마지막 숫자는 투명도입니다.
    [SerializeField] private Color nameBoxColor = new Color(0.05f, 0.08f, 0.20f, 0.95f); // 이름창 색입니다.

    [Header("대사창 위치와 크기")]
    [SerializeField] private float dialoguePanelHeight = 260f; // 대사창 높이입니다.
    [SerializeField] private float dialoguePanelBottomMargin = 40f; // 대사창이 화면 아래에서 얼마나 떨어질지 정합니다.
    [SerializeField] private float dialoguePanelSideMargin = 80f; // 대사창의 왼쪽, 오른쪽 여백입니다.

    private void Awake()
    {
        // Awake는 게임 오브젝트가 켜질 때 Unity가 자동으로 불러주는 함수입니다.
        // Play 버튼을 누르면 기본 화면을 바로 만들기 위해 여기서 실행합니다.
        CreateBasicScreen();
    }

    private void CreateBasicScreen()
    {
        // Canvas는 UI를 올려놓는 큰 종이입니다.
        GameObject canvasObject = CreateUIObject("Canvas", transform);

        // Canvas 컴포넌트는 UI를 화면에 보이게 해 줍니다.
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; // 화면 위에 UI를 바로 그리는 방식입니다.

        // CanvasScaler는 화면 크기가 달라도 UI 크기를 비슷하게 보이도록 맞춰 줍니다.
        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; // 화면 크기에 맞춰 UI 크기를 조절합니다.
        canvasScaler.referenceResolution = new Vector2(1920f, 1080f); // 기준 화면 크기를 Full HD로 정합니다.
        canvasScaler.matchWidthOrHeight = 0.5f; // 가로와 세로를 반반 기준으로 맞춥니다.

        // GraphicRaycaster는 나중에 버튼 클릭을 받을 때 필요합니다.
        // 이번 단계에서는 버튼을 만들지 않지만, 나중 기능을 위해 미리 넣어 둡니다.
        canvasObject.AddComponent<GraphicRaycaster>();

        // Canvas의 RectTransform을 가져옵니다.
        // RectTransform은 UI의 위치와 크기를 정하는 부품입니다.
        RectTransform canvasRect = canvasObject.GetComponent<RectTransform>();

        // 화면 전체 배경을 만듭니다.
        Image backgroundImage = CreateImage("BackgroundImage", canvasRect, backgroundColor);
        StretchToParent(backgroundImage.rectTransform, 0f, 0f, 0f, 0f);

        // 나중에 캐릭터 초상화를 넣을 빈 공간을 만듭니다.
        GameObject characterArea = CreateUIObject("CharacterArea", canvasRect);
        RectTransform characterAreaRect = characterArea.GetComponent<RectTransform>();
        SetCenterBottom(characterAreaRect, new Vector2(520f, 680f), new Vector2(0f, 250f));

        // 나중에 선택지 버튼을 넣을 빈 공간을 만듭니다.
        GameObject choiceArea = CreateUIObject("ChoiceArea", canvasRect);
        RectTransform choiceAreaRect = choiceArea.GetComponent<RectTransform>();
        SetCenter(choiceAreaRect, new Vector2(900f, 300f), new Vector2(0f, 80f));

        // 화면 아래쪽 대사창을 만듭니다.
        Image dialoguePanel = CreateImage("DialoguePanel", canvasRect, dialoguePanelColor);
        SetBottomStretch(dialoguePanel.rectTransform, dialoguePanelBottomMargin, dialoguePanelHeight, dialoguePanelSideMargin, dialoguePanelSideMargin);

        // 대사창 안쪽 왼쪽 위에 이름창을 만듭니다.
        Image nameBox = CreateImage("NameBox", dialoguePanel.rectTransform, nameBoxColor);
        SetTopLeft(nameBox.rectTransform, new Vector2(300f, 70f), new Vector2(34f, -24f));

        // 이름창 안에 TextMeshPro 글자를 만듭니다.
        TextMeshProUGUI nameText = CreateText("NameText", nameBox.rectTransform, speakerName, 36f, FontStyles.Bold, TextAlignmentOptions.Center);
        StretchToParent(nameText.rectTransform, 0f, 0f, 0f, 0f);

        // 대사창 안에 대사 TextMeshPro 글자를 만듭니다.
        TextMeshProUGUI dialogueText = CreateText("DialogueText", dialoguePanel.rectTransform, dialogueMessage, 34f, FontStyles.Normal, TextAlignmentOptions.TopLeft);
        StretchToParent(dialogueText.rectTransform, 60f, 60f, 95f, 55f);

        // 대사창 오른쪽 아래에 다음 진행 안내 TextMeshPro 글자를 만듭니다.
        TextMeshProUGUI nextGuideText = CreateText("NextGuideText", dialoguePanel.rectTransform, nextGuideMessage, 24f, FontStyles.Normal, TextAlignmentOptions.BottomRight);
        StretchToParent(nextGuideText.rectTransform, 60f, 60f, 180f, 20f);
    }

    private GameObject CreateUIObject(string objectName, Transform parent)
    {
        // UI 오브젝트는 RectTransform이 있어야 위치와 크기를 정할 수 있습니다.
        GameObject childObject = new GameObject(objectName, typeof(RectTransform));

        // 새로 만든 오브젝트를 부모 오브젝트 아래에 넣습니다.
        childObject.transform.SetParent(parent, false);

        // 만든 오브젝트를 돌려줍니다.
        return childObject;
    }

    private Image CreateImage(string objectName, Transform parent, Color color)
    {
        // 먼저 빈 UI 오브젝트를 만듭니다.
        GameObject imageObject = CreateUIObject(objectName, parent);

        // Image 컴포넌트를 붙이면 화면에 색이나 그림을 보여줄 수 있습니다.
        Image image = imageObject.AddComponent<Image>();

        // 지금은 실제 그림 대신 색으로 보이게 합니다.
        image.color = color;

        // 만든 Image를 돌려줍니다.
        return image;
    }

    private TextMeshProUGUI CreateText(string objectName, Transform parent, string message, float fontSize, FontStyles fontStyle, TextAlignmentOptions alignment)
    {
        // 먼저 빈 UI 오브젝트를 만듭니다.
        GameObject textObject = CreateUIObject(objectName, parent);

        // TextMeshProUGUI 컴포넌트를 붙이면 UI 글자를 보여줄 수 있습니다.
        TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();

        // 화면에 보여줄 글자 내용을 넣습니다.
        text.text = message;

        // 글자 크기를 정합니다.
        text.fontSize = fontSize;

        // 글자 스타일을 정합니다. 예: 보통 글자, 굵은 글자
        text.fontStyle = fontStyle;

        // 글자가 어느 방향에 붙을지 정합니다. 예: 가운데, 왼쪽 위, 오른쪽 아래
        text.alignment = alignment;

        // 글자 색을 흰색으로 정합니다.
        text.color = Color.white;

        // 한글 폰트가 연결되어 있으면 그 폰트를 사용합니다.
        // 이 설정이 없으면 한글이 네모(□)로 보일 수 있습니다.
        if (koreanFont != null)
        {
            text.font = koreanFont;
        }

        // 글자가 길어지면 자동으로 줄바꿈되게 합니다.
        text.enableWordWrapping = true;

        // 만든 TextMeshPro 글자를 돌려줍니다.
        return text;
    }

    private void StretchToParent(RectTransform rectTransform, float left, float right, float top, float bottom)
    {
        // 부모의 왼쪽 아래를 기준으로 잡습니다.
        rectTransform.anchorMin = Vector2.zero;

        // 부모의 오른쪽 위를 기준으로 잡습니다.
        rectTransform.anchorMax = Vector2.one;

        // 중심점을 가운데로 둡니다.
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        // 왼쪽과 아래쪽 여백을 정합니다.
        rectTransform.offsetMin = new Vector2(left, bottom);

        // 오른쪽과 위쪽 여백을 정합니다.
        rectTransform.offsetMax = new Vector2(-right, -top);
    }

    private void SetBottomStretch(RectTransform rectTransform, float bottom, float height, float left, float right)
    {
        // 화면 아래쪽을 기준으로 왼쪽 끝을 잡습니다.
        rectTransform.anchorMin = new Vector2(0f, 0f);

        // 화면 아래쪽을 기준으로 오른쪽 끝을 잡습니다.
        rectTransform.anchorMax = new Vector2(1f, 0f);

        // 중심점을 아래 가운데로 둡니다.
        rectTransform.pivot = new Vector2(0.5f, 0f);

        // 왼쪽, 아래쪽 여백을 정합니다.
        rectTransform.offsetMin = new Vector2(left, bottom);

        // 오른쪽 여백과 높이를 정합니다.
        rectTransform.offsetMax = new Vector2(-right, bottom + height);
    }

    private void SetTopLeft(RectTransform rectTransform, Vector2 size, Vector2 position)
    {
        // 부모의 왼쪽 위를 기준으로 잡습니다.
        rectTransform.anchorMin = new Vector2(0f, 1f);
        rectTransform.anchorMax = new Vector2(0f, 1f);

        // 오브젝트의 기준점도 왼쪽 위로 잡습니다.
        rectTransform.pivot = new Vector2(0f, 1f);

        // 크기를 정합니다.
        rectTransform.sizeDelta = size;

        // 위치를 정합니다.
        rectTransform.anchoredPosition = position;
    }

    private void SetCenterBottom(RectTransform rectTransform, Vector2 size, Vector2 position)
    {
        // 화면 아래쪽 가운데를 기준으로 잡습니다.
        rectTransform.anchorMin = new Vector2(0.5f, 0f);
        rectTransform.anchorMax = new Vector2(0.5f, 0f);

        // 오브젝트의 기준점도 아래 가운데로 잡습니다.
        rectTransform.pivot = new Vector2(0.5f, 0f);

        // 크기를 정합니다.
        rectTransform.sizeDelta = size;

        // 위치를 정합니다.
        rectTransform.anchoredPosition = position;
    }

    private void SetCenter(RectTransform rectTransform, Vector2 size, Vector2 position)
    {
        // 화면 가운데를 기준으로 잡습니다.
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

        // 오브젝트의 기준점도 가운데로 잡습니다.
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        // 크기를 정합니다.
        rectTransform.sizeDelta = size;

        // 위치를 정합니다.
        rectTransform.anchoredPosition = position;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 이 스크립트는 미연시 기본 화면을 자동으로 만들어 줍니다.
// Canvas, 배경, 이름창, 대사창, 다음 안내 텍스트를 코드로 생성합니다.
[DisallowMultipleComponent]
public class VisualNovelBasicScreen : MonoBehaviour
{
    [Header("테스트 글자")]
    [SerializeField] private string speakerName = "이네스"; // 이름창에 나올 테스트 이름입니다.
    [SerializeField] private string dialogueText = "기본 대사 화면 테스트입니다."; // 대사창에 나올 테스트 대사입니다.
    [SerializeField] private string nextGuideText = "클릭해서 다음으로"; // 대사창 오른쪽 아래에 나올 안내 문구입니다.

    [Header("화면 색상")]
    [SerializeField] private Color backgroundColor = new Color(0.13f, 0.16f, 0.25f, 1f); // 배경 색입니다.
    [SerializeField] private Color dialogueBoxColor = new Color(0f, 0f, 0f, 0.72f); // 대사창 색입니다. 마지막 숫자는 투명도입니다.
    [SerializeField] private Color nameBoxColor = new Color(0.18f, 0.22f, 0.38f, 0.95f); // 이름창 색입니다.

    [Header("대사창 크기")]
    [SerializeField] private float dialogueBoxHeight = 300f; // 대사창의 높이입니다.
    [SerializeField] private float dialogueBoxBottomMargin = 70f; // 대사창이 화면 아래에서 얼마나 떨어질지 정합니다.
    [SerializeField] private float dialogueBoxSideMargin = 90f; // 대사창의 왼쪽, 오른쪽 여백입니다.

    private void Awake()
    {
        // 게임이 시작되면 기본 화면을 만듭니다.
        CreateBasicScreen();
    }

    private void CreateBasicScreen()
    {
        // Canvas는 UI를 올려놓는 큰 종이 같은 것입니다.
        GameObject canvasObject = CreateUIObject("VN_Canvas", transform);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; // 화면 위에 UI를 바로 보여주는 방식입니다.

        // Canvas Scaler는 화면 크기가 달라도 UI 크기를 보기 좋게 맞춰 줍니다.
        CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920f, 1080f); // 기준 화면 크기입니다.
        canvasScaler.matchWidthOrHeight = 0.5f; // 가로와 세로를 반반 기준으로 맞춥니다.

        // Graphic Raycaster는 나중에 버튼 클릭을 받을 때 필요합니다.
        canvasObject.AddComponent<GraphicRaycaster>();

        RectTransform canvasRect = canvasObject.GetComponent<RectTransform>();

        // 배경 이미지 영역을 만듭니다. 지금은 실제 그림 대신 색으로 표시합니다.
        Image backgroundImage = CreateImage("Background_Image", canvasRect, backgroundColor);
        StretchToParent(backgroundImage.rectTransform, 0f, 0f, 0f, 0f);

        // 나중에 캐릭터 초상화를 넣을 빈 부모 오브젝트입니다.
        GameObject characterRoot = CreateUIObject("Character_Root_Future", canvasRect);
        RectTransform characterRootRect = characterRoot.GetComponent<RectTransform>();
        SetCenterBottom(characterRootRect, new Vector2(520f, 680f), new Vector2(0f, 250f));

        // 나중에 선택지 버튼을 넣을 빈 부모 오브젝트입니다. 지금은 보이지 않게 꺼 둡니다.
        GameObject choiceRoot = CreateUIObject("Choice_Root_Future", canvasRect);
        RectTransform choiceRootRect = choiceRoot.GetComponent<RectTransform>();
        SetCenter(choiceRootRect, new Vector2(900f, 420f), Vector2.zero);
        choiceRoot.SetActive(false);

        // 화면 아래쪽 대사창을 만듭니다.
        Image dialogueBox = CreateImage("Dialogue_Box", canvasRect, dialogueBoxColor);
        SetBottomStretch(dialogueBox.rectTransform, dialogueBoxBottomMargin, dialogueBoxHeight, dialogueBoxSideMargin, dialogueBoxSideMargin);

        // 대사창 왼쪽 위 이름창을 만듭니다.
        Image nameBox = CreateImage("Name_Box", dialogueBox.rectTransform, nameBoxColor);
        SetTopLeft(nameBox.rectTransform, new Vector2(280f, 64f), new Vector2(34f, -26f));

        // 이름창 안에 TextMeshPro 글자를 만듭니다.
        TextMeshProUGUI nameText = CreateText("Name_Text", nameBox.rectTransform, speakerName, 34f, FontStyles.Bold, TextAlignmentOptions.Center);
        StretchToParent(nameText.rectTransform, 8f, 8f, 6f, 6f);

        // 대사창 안에 실제 대사 글자를 만듭니다.
        TextMeshProUGUI dialogue = CreateText("Dialogue_Text", dialogueBox.rectTransform, dialogueText, 36f, FontStyles.Normal, TextAlignmentOptions.TopLeft);
        StretchToParent(dialogue.rectTransform, 46f, 46f, 110f, 70f);

        // 오른쪽 아래에 다음 진행 안내 글자를 만듭니다.
        TextMeshProUGUI nextGuide = CreateText("Next_Guide_Text", dialogueBox.rectTransform, nextGuideText, 24f, FontStyles.Normal, TextAlignmentOptions.BottomRight);
        StretchToParent(nextGuide.rectTransform, 46f, 46f, 220f, 22f);
    }

    private GameObject CreateUIObject(string objectName, Transform parent)
    {
        // UI 오브젝트는 RectTransform이 필요합니다.
        GameObject child = new GameObject(objectName, typeof(RectTransform));
        child.transform.SetParent(parent, false);
        return child;
    }

    private Image CreateImage(string objectName, Transform parent, Color color)
    {
        // Image는 화면에 색이나 그림을 보여주는 UI입니다.
        GameObject imageObject = CreateUIObject(objectName, parent);
        Image image = imageObject.AddComponent<Image>();
        image.color = color;
        return image;
    }

    private TextMeshProUGUI CreateText(string objectName, Transform parent, string text, float fontSize, FontStyles fontStyle, TextAlignmentOptions alignment)
    {
        // TextMeshProUGUI는 UI에서 글자를 보여주는 컴포넌트입니다.
        GameObject textObject = CreateUIObject(objectName, parent);
        TextMeshProUGUI tmpText = textObject.AddComponent<TextMeshProUGUI>();
        tmpText.text = text;
        tmpText.fontSize = fontSize;
        tmpText.fontStyle = fontStyle;
        tmpText.alignment = alignment;
        tmpText.color = Color.white;
        tmpText.enableWordWrapping = true; // 글자가 길면 자동으로 줄바꿈합니다.
        return tmpText;
    }

    private void StretchToParent(RectTransform rectTransform, float left, float right, float top, float bottom)
    {
        // 부모 크기에 맞게 쭉 늘립니다.
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.offsetMin = new Vector2(left, bottom);
        rectTransform.offsetMax = new Vector2(-right, -top);
    }

    private void SetBottomStretch(RectTransform rectTransform, float bottom, float height, float left, float right)
    {
        // 화면 아래쪽에 가로로 길게 붙이는 설정입니다.
        rectTransform.anchorMin = new Vector2(0f, 0f);
        rectTransform.anchorMax = new Vector2(1f, 0f);
        rectTransform.pivot = new Vector2(0.5f, 0f);
        rectTransform.offsetMin = new Vector2(left, bottom);
        rectTransform.offsetMax = new Vector2(-right, bottom + height);
    }

    private void SetTopLeft(RectTransform rectTransform, Vector2 size, Vector2 position)
    {
        // 부모의 왼쪽 위에 붙이는 설정입니다.
        rectTransform.anchorMin = new Vector2(0f, 1f);
        rectTransform.anchorMax = new Vector2(0f, 1f);
        rectTransform.pivot = new Vector2(0f, 1f);
        rectTransform.sizeDelta = size;
        rectTransform.anchoredPosition = position;
    }

    private void SetCenterBottom(RectTransform rectTransform, Vector2 size, Vector2 position)
    {
        // 화면 가운데 아래쪽에 놓는 설정입니다.
        rectTransform.anchorMin = new Vector2(0.5f, 0f);
        rectTransform.anchorMax = new Vector2(0.5f, 0f);
        rectTransform.pivot = new Vector2(0.5f, 0f);
        rectTransform.sizeDelta = size;
        rectTransform.anchoredPosition = position;
    }

    private void SetCenter(RectTransform rectTransform, Vector2 size, Vector2 position)
    {
        // 화면 가운데에 놓는 설정입니다.
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = size;
        rectTransform.anchoredPosition = position;
    }
}

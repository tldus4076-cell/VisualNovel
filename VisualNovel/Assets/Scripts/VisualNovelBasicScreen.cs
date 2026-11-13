using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 기능 1번: 기본 화면 만들기
// 게임을 시작(Play)하면 Canvas, 배경, 캐릭터 영역, 대사창, 이름창, 선택지 영역을 코드로 자동 생성합니다.
// 이 스크립트는 "화면 틀"만 만듭니다. 대사 데이터(2번), 대사 출력(3번)은 아직 없습니다.
public class VisualNovelBasicScreen : MonoBehaviour
{
    // ─────────────────────────────────────────────
    // 폰트
    // ─────────────────────────────────────────────
    [Header("한글 폰트")]
    [Tooltip("Maplestory Light SDF 폰트 에셋을 여기에 끌어다 놓으세요. 비어 있으면 한글이 네모로 보입니다.")]
    public TMP_FontAsset koreanFont;

    // ─────────────────────────────────────────────
    // 색상
    // ─────────────────────────────────────────────
    [Header("색상 설정")]
    [Tooltip("배경 이미지가 들어가기 전까지 보여줄 배경 색입니다.")]
    public Color backgroundColor = new Color(0.12f, 0.10f, 0.20f, 1f);

    [Tooltip("대사창 색입니다. 알파(A) 값이 낮을수록 투명해집니다.")]
    public Color dialogueBoxColor = new Color(0f, 0f, 0f, 0.75f);

    [Tooltip("이름창 색입니다.")]
    public Color nameBoxColor = new Color(0.35f, 0.15f, 0.30f, 0.95f);

    [Tooltip("글자 색입니다.")]
    public Color textColor = Color.white;

    // ─────────────────────────────────────────────
    // 크기
    // ─────────────────────────────────────────────
    [Header("대사창 크기 설정")]
    [Tooltip("대사창의 높이(픽셀)입니다.")]
    public float dialogueBoxHeight = 280f;

    [Tooltip("대사창 좌우, 아래쪽 여백(픽셀)입니다.")]
    public float dialogueBoxMargin = 40f;

    [Tooltip("대사창 안쪽 글자 여백(픽셀)입니다.")]
    public float dialoguePadding = 30f;

    [Header("이름창 크기 설정")]
    [Tooltip("이름창 가로 크기입니다.")]
    public float nameBoxWidth = 320f;

    [Tooltip("이름창 세로 크기입니다.")]
    public float nameBoxHeight = 70f;

    [Header("글자 크기 설정")]
    [Tooltip("이름 글자 크기입니다.")]
    public float nameFontSize = 38f;

    [Tooltip("대사 글자 크기입니다.")]
    public float dialogueFontSize = 36f;

    [Tooltip("다음 진행 안내 글자 크기입니다.")]
    public float hintFontSize = 28f;

    [Header("기준 해상도")]
    [Tooltip("UI가 이 해상도를 기준으로 화면 크기에 맞게 늘어나고 줄어듭니다.")]
    public Vector2 referenceResolution = new Vector2(1920f, 1080f);

    [Header("임시 표시 글자 (3번 기능에서 실제 대사로 교체됩니다)")]
    [Tooltip("이름창에 임시로 보여줄 글자입니다.")]
    public string placeholderName = "이네스";

    [Tooltip("대사창에 임시로 보여줄 글자입니다.")]
    public string placeholderDialogue = "기본 화면이 만들어졌습니다.";

    [Tooltip("대사창 오른쪽 아래에 보여줄 안내 글자입니다.")]
    public string nextHintText = "클릭하여 계속 ▶";

    // ─────────────────────────────────────────────
    // 다른 기능에서 찾아 쓸 수 있도록 공개해 두는 오브젝트들 (읽기 전용)
    // ─────────────────────────────────────────────
    public Canvas MainCanvas { get; private set; }
    public Image BackgroundImage { get; private set; }
    public RectTransform CharacterArea { get; private set; }
    public RectTransform ChoiceArea { get; private set; }
    public RectTransform DialogueBox { get; private set; }
    public TextMeshProUGUI NameText { get; private set; }
    public TextMeshProUGUI DialogueText { get; private set; }
    public TextMeshProUGUI NextHintText { get; private set; }

    // Play를 누르면 Unity가 가장 먼저 호출합니다. 여기서 화면을 만듭니다.
    private void Awake()
    {
        BuildScreen();
    }

    // 화면 전체를 순서대로 만드는 함수
    private void BuildScreen()
    {
        // 1. Canvas 만들기
        GameObject canvasObject = new GameObject("VisualNovelCanvas");
        canvasObject.transform.SetParent(transform, false);

        MainCanvas = canvasObject.AddComponent<Canvas>();
        MainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = referenceResolution;
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;

        canvasObject.AddComponent<GraphicRaycaster>();

        RectTransform canvasRect = canvasObject.GetComponent<RectTransform>();

        // 2. 아래에 깔리는 것부터 순서대로 만들기 (나중에 만든 것이 위에 보입니다)
        CreateBackground(canvasRect);
        CreateCharacterArea(canvasRect);
        CreateChoiceArea(canvasRect);
        CreateDialogueBox(canvasRect);
    }

    // 배경 이미지 영역 (화면 전체)
    private void CreateBackground(RectTransform parent)
    {
        RectTransform rect = CreateUIObject("BackgroundImage", parent);
        StretchToParent(rect);

        BackgroundImage = rect.gameObject.AddComponent<Image>();
        BackgroundImage.color = backgroundColor;
        BackgroundImage.raycastTarget = false;
    }

    // 캐릭터 초상화가 들어갈 빈 영역 (8번 기능에서 사용)
    private void CreateCharacterArea(RectTransform parent)
    {
        CharacterArea = CreateUIObject("CharacterArea", parent);
        StretchToParent(CharacterArea);
    }

    // 선택지 버튼이 들어갈 빈 영역 (13번 기능에서 사용). 화면 가운데에 놓습니다.
    private void CreateChoiceArea(RectTransform parent)
    {
        ChoiceArea = CreateUIObject("ChoiceArea", parent);
        ChoiceArea.anchorMin = new Vector2(0.5f, 0.5f);
        ChoiceArea.anchorMax = new Vector2(0.5f, 0.5f);
        ChoiceArea.pivot = new Vector2(0.5f, 0.5f);
        ChoiceArea.sizeDelta = new Vector2(900f, 400f);
        ChoiceArea.anchoredPosition = new Vector2(0f, 120f);
    }

    // 대사창, 이름창, 글자들
    private void CreateDialogueBox(RectTransform parent)
    {
        // 대사창: 화면 아래쪽, 좌우로 넓게
        DialogueBox = CreateUIObject("DialogueBox", parent);
        DialogueBox.anchorMin = new Vector2(0f, 0f);
        DialogueBox.anchorMax = new Vector2(1f, 0f);
        DialogueBox.pivot = new Vector2(0.5f, 0f);
        DialogueBox.offsetMin = new Vector2(dialogueBoxMargin, dialogueBoxMargin);
        DialogueBox.offsetMax = new Vector2(-dialogueBoxMargin, dialogueBoxMargin + dialogueBoxHeight);

        Image boxImage = DialogueBox.gameObject.AddComponent<Image>();
        boxImage.color = dialogueBoxColor;
        // 3번 이후 "클릭으로 넘기기"에서 대사창 클릭을 받을 수 있게 켜 둡니다.
        boxImage.raycastTarget = true;

        // 대사 글자: 대사창 안쪽 전체 (이름창이 위로 튀어나오므로 위쪽은 조금 더 비웁니다)
        RectTransform dialogueTextRect = CreateUIObject("DialogueText", DialogueBox);
        StretchToParent(dialogueTextRect);
        dialogueTextRect.offsetMin = new Vector2(dialoguePadding, dialoguePadding + 30f);
        dialogueTextRect.offsetMax = new Vector2(-dialoguePadding, -dialoguePadding);
        DialogueText = CreateText(dialogueTextRect, placeholderDialogue, dialogueFontSize,
            TextAlignmentOptions.TopLeft);

        // 다음 진행 안내: 대사창 오른쪽 아래
        RectTransform hintRect = CreateUIObject("NextHintText", DialogueBox);
        hintRect.anchorMin = new Vector2(1f, 0f);
        hintRect.anchorMax = new Vector2(1f, 0f);
        hintRect.pivot = new Vector2(1f, 0f);
        hintRect.sizeDelta = new Vector2(360f, 50f);
        hintRect.anchoredPosition = new Vector2(-dialoguePadding, 15f);
        NextHintText = CreateText(hintRect, nextHintText, hintFontSize, TextAlignmentOptions.BottomRight);
        NextHintText.color = new Color(textColor.r, textColor.g, textColor.b, 0.7f);

        // 이름창: 대사창 왼쪽 위에 살짝 걸치게
        RectTransform nameBoxRect = CreateUIObject("NameBox", DialogueBox);
        nameBoxRect.anchorMin = new Vector2(0f, 1f);
        nameBoxRect.anchorMax = new Vector2(0f, 1f);
        nameBoxRect.pivot = new Vector2(0f, 0.5f);
        nameBoxRect.sizeDelta = new Vector2(nameBoxWidth, nameBoxHeight);
        nameBoxRect.anchoredPosition = new Vector2(dialoguePadding, 0f);

        Image nameBoxImage = nameBoxRect.gameObject.AddComponent<Image>();
        nameBoxImage.color = nameBoxColor;
        nameBoxImage.raycastTarget = false;

        RectTransform nameTextRect = CreateUIObject("NameText", nameBoxRect);
        StretchToParent(nameTextRect);
        NameText = CreateText(nameTextRect, placeholderName, nameFontSize, TextAlignmentOptions.Center);
    }

    // 빈 UI 오브젝트(RectTransform만 있는 것)를 만드는 도우미 함수
    private RectTransform CreateUIObject(string objectName, RectTransform parent)
    {
        GameObject obj = new GameObject(objectName, typeof(RectTransform));
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.SetParent(parent, false);
        return rect;
    }

    // 부모 크기에 꽉 차게 늘려 주는 도우미 함수
    private void StretchToParent(RectTransform rect)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    // TextMeshPro 글자 오브젝트를 만드는 도우미 함수
    private TextMeshProUGUI CreateText(RectTransform rect, string content, float size, TextAlignmentOptions alignment)
    {
        TextMeshProUGUI text = rect.gameObject.AddComponent<TextMeshProUGUI>();
        if (koreanFont != null)
        {
            text.font = koreanFont;
        }
        else
        {
            Debug.LogWarning("[VisualNovelBasicScreen] koreanFont가 비어 있습니다. 한글이 네모로 보일 수 있습니다.");
        }

        text.text = content;
        text.fontSize = size;
        text.color = textColor;
        text.alignment = alignment;
        text.raycastTarget = false;
        return text;
    }
}

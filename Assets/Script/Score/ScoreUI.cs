using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private ScoreManager scoreManager;
    private TextMeshProUGUI scoreText;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        if (Object.FindFirstObjectByType<ScoreUI>() != null)
        {
            return;
        }

        new GameObject("ScoreUI").AddComponent<ScoreUI>();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        scoreText = CreateScoreText();

        SubscribeToScoreManager();
    }

    void OnDestroy()
    {
        UnsubscribeFromScoreManager();
    }

    void Update()
    {
        if (scoreManager == null)
        {
            scoreManager = Object.FindFirstObjectByType<ScoreManager>();
            SubscribeToScoreManager();
        }
    }

    private void SubscribeToScoreManager()
    {
        if (scoreManager == null)
        {
            return;
        }

        scoreManager.OnScoreChanged += UpdateScore;
        UpdateScore(scoreManager.CurrentScore);
    }

    private void UnsubscribeFromScoreManager()
    {
        if (scoreManager == null)
        {
            return;
        }

        scoreManager.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        if (scoreText == null)
        {
            return;
        }

        scoreText.text = $"Score: {score}";
    }

    private TextMeshProUGUI CreateScoreText()
    {
        Canvas canvas = Object.FindFirstObjectByType<Canvas>();

        if (canvas == null)
        {
            GameObject canvasObject = new GameObject(
                "Canvas",
                typeof(Canvas),
                typeof(CanvasScaler),
                typeof(GraphicRaycaster)
            );

            canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        DontDestroyOnLoad(canvas.gameObject);

        GameObject textObject = new GameObject(
            "ScoreText",
            typeof(TextMeshProUGUI)
        );

        textObject.transform.SetParent(canvas.transform, false);

        RectTransform rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0f, 1f);
        rect.anchorMax = new Vector2(0f, 1f);
        rect.pivot = new Vector2(0f, 1f);
        rect.anchoredPosition = new Vector2(20f, -20f);
        rect.sizeDelta = new Vector2(300f, 50f);

        TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
        text.text = "Score: 0";
        text.fontSize = 32f;
        text.color = Color.white;

        return text;
    }
}

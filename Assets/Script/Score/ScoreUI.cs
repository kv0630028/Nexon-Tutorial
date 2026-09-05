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
        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        scoreText = CreateScoreText();
    }

    void Update()
    {
        if (scoreManager == null || scoreText == null)
        {
            return;
        }

        scoreText.text = $"Score: {scoreManager.CurrentScore}";
    }

    private TextMeshProUGUI CreateScoreText()
    {
        Canvas canvas = Object.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObject = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        GameObject textObject = new GameObject("ScoreText", typeof(TextMeshProUGUI));
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

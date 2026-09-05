using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private GameObject gameOverTextObject;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        if (Object.FindFirstObjectByType<GameOverUI>() != null)
        {
            return;
        }

        new GameObject("GameOverUI").AddComponent<GameOverUI>();
    }

    void Awake()
    {
        playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        gameOverTextObject = CreateGameOverText();
        gameOverTextObject.SetActive(false);
    }

    void Update()
    {
        if (playerHealth == null || gameOverTextObject == null)
        {
            return;
        }

        if (playerHealth.IsDead)
        {
            gameOverTextObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private GameObject CreateGameOverText()
    {
        Canvas canvas = Object.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObject = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        GameObject textObject = new GameObject("GameOverText", typeof(TextMeshProUGUI));
        textObject.transform.SetParent(canvas.transform, false);

        RectTransform rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = new Vector2(600f, 100f);

        TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
        text.text = "GAME OVER";
        text.fontSize = 64f;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.white;

        return textObject;
    }
}

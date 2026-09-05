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
        DontDestroyOnLoad(gameObject);

        playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        gameOverTextObject = CreateGameOverText();
        gameOverTextObject.SetActive(false);

        SubscribeToPlayerHealth();
    }

    void Update()
    {
        if (playerHealth != null)
        {
            return;
        }

        playerHealth = Object.FindFirstObjectByType<PlayerHealth>();

        if (playerHealth != null)
        {
            gameOverTextObject.SetActive(false);
            Time.timeScale = 1f;

            SubscribeToPlayerHealth();
        }
    }

    void OnDestroy()
    {
        UnsubscribeFromPlayerHealth();
    }

    private void SubscribeToPlayerHealth()
    {
        if (playerHealth == null)
        {
            return;
        }

        playerHealth.OnDeath += ShowGameOver;
    }

    private void UnsubscribeFromPlayerHealth()
    {
        if (playerHealth == null)
        {
            return;
        }

        playerHealth.OnDeath -= ShowGameOver;
    }

    private void ShowGameOver()
    {
        if (gameOverTextObject == null)
        {
            return;
        }

        gameOverTextObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private GameObject CreateGameOverText()
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
            "GameOverText",
            typeof(TextMeshProUGUI)
        );

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
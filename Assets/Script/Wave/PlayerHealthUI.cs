using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private TextMeshProUGUI hpText;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        if (Object.FindFirstObjectByType<PlayerHealthUI>() != null)
        {
            return;
        }

        new GameObject("PlayerHealthUI").AddComponent<PlayerHealthUI>();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        hpText = CreateHpText();

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
            UpdateHpText(playerHealth.CurrentHp, playerHealth.MaxHp);
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

        playerHealth.OnHealthChanged += UpdateHpText;

        UpdateHpText(playerHealth.CurrentHp, playerHealth.MaxHp);
    }

    private void UnsubscribeFromPlayerHealth()
    {
        if (playerHealth == null)
        {
            return;
        }

        playerHealth.OnHealthChanged -= UpdateHpText;
    }

    private void UpdateHpText(float currentHp, float maxHp)
    {
        if (hpText == null)
        {
            return;
        }

        hpText.text = $"HP: {currentHp} / {maxHp}";
    }

    private TextMeshProUGUI CreateHpText()
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
            "PlayerHpText",
            typeof(TextMeshProUGUI)
        );

        textObject.transform.SetParent(canvas.transform, false);

        RectTransform rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0f, 1f);
        rect.anchorMax = new Vector2(0f, 1f);
        rect.pivot = new Vector2(0f, 1f);
        rect.anchoredPosition = new Vector2(20f, -70f);
        rect.sizeDelta = new Vector2(300f, 50f);

        TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
        text.text = "HP: 100 / 100";
        text.fontSize = 32f;
        text.color = Color.white;

        return text;
    }
}

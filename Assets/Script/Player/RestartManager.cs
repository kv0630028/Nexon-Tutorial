using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    private PlayerHealth playerHealth;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        if (Object.FindFirstObjectByType<RestartManager>() != null)
        {
            return;
        }

        new GameObject("RestartManager").AddComponent<RestartManager>();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
    }

    void Update()
    {
        if (playerHealth == null)
        {
            playerHealth = Object.FindFirstObjectByType<PlayerHealth>();
        }

        if (playerHealth == null || !playerHealth.IsDead)
        {
            return;
        }

        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

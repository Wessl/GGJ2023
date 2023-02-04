using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private static PlayerController player;

    private static bool isPaused = false;
    public static bool IsPaused
    {
        get
        {
            return isPaused;
        }
        private set
        {
            isPaused = value;
            player.OnPause(value);
        }
    }

    private void Start()
    {
        if (player == null) player = FindObjectOfType<PlayerController>();
    }
    
    public void GamePause(InputAction.CallbackContext context)
    {
        IsPaused = !IsPaused;
    }
    public void GameRestart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

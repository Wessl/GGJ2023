using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    public static bool IsPaused 
    { get; private set; }

    private void Start()
    {
        if (player == null) player = FindObjectOfType<PlayerController>();
    }
    
    public void GamePause(InputAction.CallbackContext context)
    {
        Debug.Log("gamePaused");
        IsPaused = !IsPaused;
    }
    public void GameRestart(InputAction.CallbackContext context)
    {
        Debug.Log("GameRestarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

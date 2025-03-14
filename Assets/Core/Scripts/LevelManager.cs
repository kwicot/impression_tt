using Core.Scripts;
using PLAYERTWO.PlatformerProject;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public MovementController movementController;
    public PlayerInputManager playerInputManager;
    void Start()
    {
        
    }

    void Update()
    {
        if (playerInputManager.GetJumpDown() && !movementController.canMove)
            SceneManager.LoadScene("Main");
    }
}

// using UnityEngine;
// using TMPro; // For TextMeshPro
// using UnityEngine.UI; // For Button component
// using UnityEngine.SceneManagement; // For reloading the scene

// public class GameOver : MonoBehaviour
// {
//     public TextMeshProUGUI gameOverText; // Reference to the Game Over Text UI element
//     public Button restartButton; // Reference to the Restart Button UI element

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Doofus"))
//         {
//             Debug.Log("Doofus entered the death zone");

//             gameOverText.gameObject.SetActive(true); // Show the Game Over text
//             restartButton.gameObject.SetActive(true); // Show the Restart Button
//             Time.timeScale = 0; // Pause the game

//             // Disable Doofus's movement
//             PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
//             if (playerMovement != null)
//             {
//                 playerMovement.enabled = false; // Disable movement script
//             }

//             // Disable all platforms
//             DisablePlatforms();
//         }
//     }


//     void DisablePlatforms()
//     {
//         GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
//         foreach (GameObject platform in platforms)
//         {
//             platform.SetActive(false);
//             Debug.Log("Platform disabled: " + platform.name);
//         }
//     }
//     public void RestartGame()
// {
//     Debug.Log("RestartGame called");

//     Time.timeScale = 1; // Ensure the game time is running

//     // Always reset score
//     if (ScoreManager.Instance != null)
//     {
//         ScoreManager.Instance.ResetScore();
//     }
//     else
//     {
//         Debug.LogWarning("ScoreManager.Instance is null. Score reset may not work.");
//     }

//     SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
// }

// }


using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.UI; // For Button component
using UnityEngine.SceneManagement; // For reloading the scene

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI gameOverText; // Reference to the Game Over Text UI element
    public Button restartButton; // Reference to the Restart Button UI element

    void Start()
    {
        // Ensure the game starts running normally
        Time.timeScale = 1;

        // Hide the Game Over text and restart button at the start of the game
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Doofus"))
        {
            Debug.Log("Doofus entered the death zone");

            gameOverText.gameObject.SetActive(true); // Show the Game Over text
            restartButton.gameObject.SetActive(true); // Show the Restart Button
            Time.timeScale = 0; // Pause the game

            // Disable Doofus's movement
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false; // Disable movement script
            }

            // Disable all platforms
            DisablePlatforms();
        }
    }

    void DisablePlatforms()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false);
            Debug.Log("Platform disabled: " + platform.name);
        }
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame called");

        Time.timeScale = 1; // Ensure the game time is running

        // Always reset score
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
        else
        {
            Debug.LogWarning("ScoreManager.Instance is null. Score reset may not work.");
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}

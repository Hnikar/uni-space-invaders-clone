using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    void Update()
    {
        // Проверяем, была ли нажата любая клавиша
        if (Input.anyKeyDown)
        {
            // Загружаем сцену "Space Invaders"
            SceneManager.LoadScene("Space Invaders");
        }
    }
}

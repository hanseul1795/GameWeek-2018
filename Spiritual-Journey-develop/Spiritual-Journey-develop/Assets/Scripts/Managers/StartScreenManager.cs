using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField] private string m_sceneToLoad = null;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if (Input.anyKey)
            SceneManager.LoadScene(m_sceneToLoad);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutoCCG
{
    public class SceneSelector : MonoBehaviour
    {
        public void GoToScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
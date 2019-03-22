using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutoCCG
{
    public class BoardController : MonoBehaviour
    {
        public GameObject player;

        public GameObject enemy;

        public void CreateBattlegrounds()
        {
            player.GetComponentInChildren<HandController>().SendQueueToBattlegrounds();
            enemy.GetComponentInChildren<HandController>().SendQueueToBattlegrounds();
        }

        public void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace AutoCCG
{
    public class BoardController : NetworkBehaviour
    {
        public GameObject player;

        public GameObject enemy;

        public int startingHealth;

        public int startingGold;

        public void CreateBattlegrounds()
        {
            player.GetComponentInChildren<HandController>().SendQueueToBattlegrounds();
            enemy.GetComponentInChildren<HandController>().SendQueueToBattlegrounds();
        }

        public void AddPlayer(GameObject playerObject)
        {
            var playerModel = playerObject.GetComponent<PlayerModel>();
            playerModel.currentHealth = startingHealth;
            playerModel.currentGold = startingGold;

            if (!this.player)
            {
                player = playerObject;
                playerObject.name = "Player";
            }
            else if (!this.enemy)
            {
                enemy = playerObject;
                playerObject.name = "Enemy";
                GetComponent<PlayMakerFSM>().SetState("Start Game");
            }
            else
            {
                NetworkManager.Shutdown();
            }
        }
    }
}
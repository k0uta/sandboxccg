using System;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace AutoCCG
{
    public enum Phase
    {
        Setup,
        Shop,
        Strategy,
        Battle,
        GameOver
    }

    public class BoardView : NetworkBehaviour
    {
        public PlayerView playerView;

        public PlayerView enemyView;

        public TextMeshProUGUI turnText;

        public TextMeshProUGUI phaseNameText;

        public TextMeshProUGUI phaseTimerText;

        public TextMeshProUGUI gameOverText;

        public GameObject gameOverArea;

        public GameObject playerHand;

        public GameObject playerShop;

        public GameObject restockButton;

        public Phase currentPhase;

        private void Start()
        {
            SetPhase(Phase.Setup);
        }

        [ClientRpc]
        public void RpcUpdateVariables(Phase phase, int phaseSeconds, int currentTurn, int maxTurns, string gameWinnerName)
        {
            turnText.text = string.Format("Turn" + Environment.NewLine + "{0}/{1}", currentTurn, maxTurns);
            
            phaseTimerText.text = string.Format("Time Left" + Environment.NewLine + "{0}s", phaseSeconds);

            gameOverText.text = string.Format("Game Over" + Environment.NewLine + "{0} wins", gameWinnerName);

            if (currentPhase != phase)
            {
                SetPhase(phase);
            }
        }

        void SetPhase(Phase phase)
        {
            currentPhase = phase;

            var phaseName = Enum.GetName(typeof(Phase), phase);
            phaseNameText.text = string.Format("{0}" + Environment.NewLine + "Phase", phaseName);

            var playerShopEnabled = currentPhase == Phase.Shop;
            var playerHandEnabled = currentPhase == Phase.Shop || currentPhase == Phase.Strategy;
            var restockButtonEnabled = currentPhase == Phase.Shop;
            var gameOverAreaEnabled = currentPhase == Phase.GameOver;

            playerShop.GetComponent<CanvasGroup>().interactable = playerShopEnabled;
            playerHand.GetComponent<CanvasGroup>().interactable = playerHandEnabled;
            restockButton.GetComponent<Button>().interactable = restockButtonEnabled;
            gameOverArea.SetActive(gameOverAreaEnabled);
        }
    }
}

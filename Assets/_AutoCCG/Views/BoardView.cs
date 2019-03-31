﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace AutoCCG
{
    public enum BoardPhase
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

        public BoardPhase currentPhase;

        private void Start()
        {
            SetPhase(BoardPhase.Setup);
        }

        [ClientRpc]
        public void RpcUpdateVariables(BoardPhase phase, int phaseSeconds, int currentTurn, int maxTurns, string gameWinnerName)
        {
            turnText.text = string.Format("Turn" + Environment.NewLine + "{0}/{1}", currentTurn, maxTurns);
            
            phaseTimerText.text = string.Format("Time Left" + Environment.NewLine + "{0}s", phaseSeconds);

            gameOverText.text = string.Format("Game Over" + Environment.NewLine + "{0} wins", gameWinnerName);

            if (currentPhase != phase)
            {
                SetPhase(phase);
            }
        }

        void SetPhase(BoardPhase phase)
        {
            currentPhase = phase;

            var phaseName = Enum.GetName(typeof(BoardPhase), phase);
            phaseNameText.text = string.Format("{0}" + Environment.NewLine + "Phase", phaseName);

            var playerShopEnabled = currentPhase == BoardPhase.Shop;
            var playerHandEnabled = currentPhase == BoardPhase.Shop || currentPhase == BoardPhase.Strategy;
            var restockButtonEnabled = currentPhase == BoardPhase.Shop;
            var gameOverAreaEnabled = currentPhase == BoardPhase.GameOver;

            playerShop.GetComponent<CanvasGroup>().interactable = playerShopEnabled;
            playerHand.GetComponent<CanvasGroup>().interactable = playerHandEnabled;
            restockButton.GetComponent<Button>().interactable = restockButtonEnabled;
            gameOverArea.SetActive(gameOverAreaEnabled);
        }
    }
}

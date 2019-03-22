using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class ChooseGameWinner : FsmStateAction
    {
        [Tooltip("The game winner name")]
        [UIHint(UIHint.Variable)]
        public FsmString gameOverMessage;

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();
            var playerModel = boardController.player.GetComponentInChildren<PlayerModel>();
            var enemyModel = boardController.enemy.GetComponentInChildren<PlayerModel>();

            if ((playerModel.currentHealth <= 0 && enemyModel.currentHealth <= 0) || (playerModel.currentHealth == enemyModel.currentHealth))
            {
                gameOverMessage.Value = "Game Over\nDraw";
            } else if(playerModel.currentHealth > enemyModel.currentHealth)
            {
                gameOverMessage.Value = "Game Over\nPlayer Wins";
            } else if(enemyModel.currentHealth > playerModel.currentHealth)
            {
                gameOverMessage.Value = "Game Over\nEnemy Wins";
            }

            Finish();
        }
    }

}

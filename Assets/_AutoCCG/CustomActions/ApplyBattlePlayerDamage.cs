using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class ApplyBattlePlayerDamage : FsmStateAction
    {
        [Tooltip("Event to send if there's one or less players alive")]
        public FsmEvent gameOver;

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();
            var playerBattlegrounds = boardController.player.GetComponentInChildren<BattlegroundsModel>();
            var enemyBattlegrounds = boardController.enemy.GetComponentInChildren<BattlegroundsModel>();

            var playerModel = boardController.player.GetComponentInChildren<PlayerModel>();
            var enemyModel = boardController.enemy.GetComponentInChildren<PlayerModel>();

            ApplyDamageFromBattlegrounds(playerModel, enemyBattlegrounds);
            ApplyDamageFromBattlegrounds(enemyModel, playerBattlegrounds);

            if(playerModel.currentHealth <= 0 || enemyModel.currentHealth <= 0)
            {
                Fsm.Event(gameOver);
            }

            Finish();
        }

        void ApplyDamageFromBattlegrounds(PlayerModel target, BattlegroundsModel battlegrounds)
        {
            foreach (var battlegroundsCard in battlegrounds.battlegroundsCards)
            {
                target.ApplyDamage(battlegroundsCard.cardModel.Attack);
            }
        }
    }

}

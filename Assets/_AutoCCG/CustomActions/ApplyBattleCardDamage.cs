using AutoCCG;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class ApplyBattleCardDamage : FsmStateAction
    {
        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var boardController = Fsm.GameObject.GetComponent<BoardController>();

            var playerModel = boardController.player.GetComponent<PlayerModel>();
            var enemyModel = boardController.enemy.GetComponent<PlayerModel>();

            var playerBattlegrounds = boardController.player.GetComponentInChildren<BattlegroundsModel>();
            var enemyBattlegrounds = boardController.enemy.GetComponentInChildren<BattlegroundsModel>();

            // TODO: Add ranged and other passive effects
            var playerFrontCard = playerBattlegrounds.battlegroundsCards[0];
            var enemyFrontCard = enemyBattlegrounds.battlegroundsCards[0];

            playerModel.RpcApplyFrontCardDanage(enemyFrontCard.cardModel.attack);
            enemyModel.RpcApplyFrontCardDanage(playerFrontCard.cardModel.attack);

            Finish();
        }
    }

}

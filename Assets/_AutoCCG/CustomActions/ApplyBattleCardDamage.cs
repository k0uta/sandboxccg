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
            var playerBattlegrounds = boardController.player.GetComponentInChildren<BattlegroundsModel>();
            var enemyBattlegrounds = boardController.enemy.GetComponentInChildren<BattlegroundsModel>();

            var playerFrontCard = playerBattlegrounds.battlegroundsCards[0];
            var enemyFrontCard = enemyBattlegrounds.battlegroundsCards[0];

            playerFrontCard.ApplyDamage(enemyFrontCard.cardModel.attack);
            enemyFrontCard.ApplyDamage(playerFrontCard.cardModel.attack);

            playerBattlegrounds.battlegroundsView.UpdateCardsView();
            enemyBattlegrounds.battlegroundsView.UpdateCardsView();

            Finish();
        }
    }

}

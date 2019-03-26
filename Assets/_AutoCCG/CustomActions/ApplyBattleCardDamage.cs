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

            var playerTotalDamage = GetBattlegroundsTotalDamage(playerBattlegrounds);
            var enemyTotalDamage = GetBattlegroundsTotalDamage(enemyBattlegrounds);

            playerModel.RpcApplyFrontCardDanage(enemyTotalDamage);
            enemyModel.RpcApplyFrontCardDanage(playerTotalDamage);

            Finish();
        }

        int GetBattlegroundsTotalDamage(BattlegroundsModel battlegrounds)
        {
            var cards = battlegrounds.battlegroundsCards;
            int totalDamage = 0;
            for (int i = 0; i < cards.Count; i++)
            {
                var cardModel = cards[i].cardModel;
                totalDamage += cardModel.GetDamage(i == 0);
            }

            return totalDamage;
        }
    }

}

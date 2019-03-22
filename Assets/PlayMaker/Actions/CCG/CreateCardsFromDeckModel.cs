using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("CCG")]
	[Tooltip("	")]
	public class CreateCardsFromDeckModel : FsmStateAction
	{
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public DeckModel deckModel;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmArray cardsArray;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public GameObject cardPrefab;

        // Code that runs on entering the state.
        public override void OnEnter()
		{
            SpawnCardsFromDeck();
			Finish();
		}

        private void SpawnCardsFromDeck()
        {
            foreach (var cardModel in deckModel.cards)
            {
                var card = GameObject.Instantiate(cardPrefab);
                card.transform.SetParent(Fsm.GameObject.transform);
                card.name = cardModel.cardName;

                cardsArray.Resize(cardsArray.Length + 1);
                cardsArray.Set(cardsArray.Length - 1, card);
            }
        }

	}

}

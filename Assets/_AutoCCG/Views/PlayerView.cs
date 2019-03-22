using TMPro;
using UnityEngine;

namespace AutoCCG
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerModel playerModel;

        public TextMeshProUGUI goldText;

        public TextMeshProUGUI healthText;

        public TextMeshProUGUI handSizeText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            goldText.text = string.Format("Gold\n{0}", playerModel.gold);
            healthText.text = string.Format("Health\n{0}", playerModel.currentHealth);

            var handModel = playerModel.handModel;
            handSizeText.text = string.Format("Hand\n{0}/{1}", handModel.cards.Count, handModel.cardLimit);
        }
    }
}

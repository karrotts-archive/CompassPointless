using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using KarrottEngine.GridSystem;
using KarrottEngine.EntitySystem;

public class GenerateUICards : MonoBehaviour
{
    public GameObject CardTemplate;

    public List<GameObject> Generate(List<Card> cards)
    {
        List<GameObject> uicards = new List<GameObject>();
        Entity player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
        PlayerRoundController roundController = player.EntityObject.GetComponent<PlayerRoundController>();

        int currentNum = 0;
        foreach (Card card in cards) 
        {
            int patternid = card.PatternId;
            Sprite pattern = Resources.LoadAll<Sprite>($"Patterns/Move_Patterns")[card.PatternId];
            
            float width = CardTemplate.GetComponent<RectTransform>().rect.width;
            float screenOffset = (Screen.width / 2) - ((width * cards.Count) / 2);
            float x = ((width + 15) * currentNum) + screenOffset;

            GameObject cardObj = Instantiate(CardTemplate, new Vector3(x, 14, 0), Quaternion.identity);

            cardObj.GetComponent<Button>().onClick.AddListener(() => roundController.DisplayMove(card.PatternId, cardObj, card));
 
            // set title
            TMP_Text title = cardObj.transform.GetChild(0).GetComponent<TMP_Text>();
            title.text = card.CardName;

            // set image
            Image image = cardObj.transform.GetChild(1).GetComponent<Image>();
            image.sprite = pattern;

            // set energy cost
            TMP_Text energy = cardObj.transform.GetChild(2).GetComponent<TMP_Text>();
            energy.text = $"Energy: {card.EnergyCost}";

            TMP_Text attack = cardObj.transform.GetChild(3).GetComponent<TMP_Text>();
            attack.text = $"Attack: {card.HitDamage}";

            cardObj.transform.SetParent(transform, false);
            uicards.Add(cardObj);
            currentNum++;
        }
        return uicards;
    }
}

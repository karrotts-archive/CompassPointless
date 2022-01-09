using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateUICards : MonoBehaviour
{
    public GameObject CardTemplate;

    public void Generate(List<Card> cards)
    {
        int currentNum = 0;
        foreach (Card card in cards) 
        {
            int patternid = card.PatternId;
            Sprite pattern = Resources.LoadAll<Sprite>($"Data/Move_Patterns")[card.PatternId];
            
            float width = CardTemplate.GetComponent<RectTransform>().rect.width;
            float screenOffset = (Screen.width / 2) - ((width * cards.Count) / 2);
            float x = ((width + 15) * currentNum) + screenOffset;

            GameObject cardObj = Instantiate(CardTemplate, new Vector3(x, 14, 0), Quaternion.identity);

            cardObj.GetComponent<Button>().onClick.AddListener(() => MarkerController.RenderTiles(patternid));
 
            // set title
            TMP_Text title = cardObj.transform.GetChild(0).GetComponent<TMP_Text>();
            title.text = card.CardName;

            // set image
            Image image = cardObj.transform.GetChild(1).GetComponent<Image>();
            image.sprite = pattern;

            // set energy cost
            TMP_Text energy = cardObj.transform.GetChild(2).GetComponent<TMP_Text>();
            energy.text = $"Energy: {card.EnergyCost}";

            cardObj.transform.SetParent(transform, false);

            currentNum++;
        }
        /**
        basex = (screen witdth / 2) - ((cardwitdh + offset) * amount) - 

        m = screen witdh / 2

        */
    }
}

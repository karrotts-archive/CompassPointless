using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KarrottEngine.GridSystem;
using UnityEngine.UI;
using KarrottEngine.EntitySystem;

public class PlayerRoundController : MonoBehaviour
{
    public int MaxEnergy = 3;
    public bool PlayerTurn;

    private DeckManager deckManager;
    private GameObject choosenCardUI;
    private Card cardData;
    private RoundController roundController;
    private int currentEnergy;
    private GenerateUICards cardGen;
    private List<GameObject> UICards;
    EntityMover mover;
    // Start is called before the first frame update
    void Start()
    {
        choosenCardUI = null;
        deckManager = GetComponent<DeckManager>();
        roundController = GameObject.FindGameObjectWithTag("GameController").GetComponent<RoundController>();
        cardGen = GameObject.FindGameObjectWithTag("PlayingUI").GetComponent<GenerateUICards>();
        mover = GetComponent<EntityMover>();
    }

    void Update()
    {
        PlayerTurn = currentEnergy > 0 || mover.Moving;
        if (!PlayerTurn && UICards != null && UICards.Count > 0)
        {
            DestroyUICards();
        }
    }

    public void StartPlayerTurn() 
    {
        currentEnergy = 3;
        deckManager.ShuffleDraw();
        DestroyUICards();
        UICards = cardGen.Generate(deckManager.DealHand(5));
    }

    public void DestroyUICards()
    {
        if (UICards != null)
        {
            while (UICards.Count > 0)
            {
                Destroy(UICards[0]);
                UICards.Remove(UICards[0]);
            }
        }
    }

    public void DisplayMove(int patternId, GameObject cardUI, Card card)
    {
        Entity player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
        if (mover.Moving) return;
        KEGrid.DeleteEntitesWithType(EntityType.TILE);
        int patternOff = Mathf.Abs(patternId - 90);
        EntityGridExtensions.RenderPlayerTiles(patternOff);

        if (choosenCardUI != cardUI && choosenCardUI != null)
        {
            choosenCardUI.GetComponent<Image>().color = Color.white;
            choosenCardUI = cardUI;
            cardData = card;
        }
        else
        {
            //first time.
            choosenCardUI = cardUI;
            cardData = card;
        }
        cardUI.GetComponent<Image>().color = Color.red; //set color of card
    }

    public void HandleClick()
    {
        Vector2 mousePos = KEGrid.GetMouseGridPosition();
        Entity entity = KEGrid.GetEntityAtPosition(mousePos, EntityType.TILE);
        Entity player = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0];
        EntityMover mover = player.EntityObject.GetComponent<EntityMover>();
        if (entity != null && currentEnergy >= cardData.EnergyCost)
        {
            switch ((TileType)entity.SpecialType)
            {
                case TileType.MOVE:
                    mover.MovePosition = mousePos;
                    KEGrid.DeleteEntitesWithType(EntityType.TILE);
                    currentEnergy -= cardData.EnergyCost;
                    Destroy(choosenCardUI);
                    deckManager.Hand.Remove(cardData);
                    deckManager.DiscardPile.Add(cardData);
                    break;
                case TileType.ATTACK:
                    Debug.Log("Attacking...");
                    Entity[] entities = KEGrid.GetEntitiesAtPosition(mousePos);
                    KEGrid.DeleteEntitesWithType(EntityType.TILE);
                    if (entities.Length >= 1)
                    {
                        foreach (Entity enemey in entities)
                        {
                            if (enemey.Type == EntityType.ENEMY && enemey.EntityObject.GetComponent<EntityManager>().TakeDamage(cardData.HitDamage))
                            {
                                KEGrid.DeleteEntity(enemey);
                                roundController.EntityQueue.Remove(enemey);
                                break;
                            }
                        }
                    }
                    currentEnergy -= cardData.EnergyCost;
                    Destroy(choosenCardUI);
                    deckManager.Hand.Remove(cardData);
                    deckManager.DiscardPile.Add(cardData);
                    break;
            }
        }
    }
}
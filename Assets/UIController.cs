using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using KarrottEngine.GridSystem;
using KarrottEngine.EntitySystem;

public class UIController : MonoBehaviour
{
    public PlayerRoundController playerRound;
    public EntityManager playermanager;
    public GameObject endturn;
    public TMP_Text health;
    public TMP_Text energy;
    // Start is called before the first frame update
    void Start()
    {
        playerRound = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0].EntityObject.GetComponent<PlayerRoundController>();
        playermanager = KEGrid.GetEntitiesByType(EntityType.PLAYER)[0].EntityObject.GetComponent<EntityManager>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayEndTurnButton();
        health.text = "Health:" + playermanager.CurrentHealth.ToString();
        energy.text = "Energy:" + playerRound.currentEnergy.ToString();
    }

    void DisplayEndTurnButton() => endturn.SetActive(playerRound.PlayerTurn);

    public void EndPlayerTurn()
    {
        KEGrid.DeleteEntitesWithType(EntityType.TILE);
        playerRound.SetEnergy(0);
    }
}

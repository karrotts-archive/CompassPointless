using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Card", order = 1)]
public class Card : ScriptableObject
{
    public int PatternId;
    public string CardName;
    public int HitDamage;
    public int EnergyCost;
}

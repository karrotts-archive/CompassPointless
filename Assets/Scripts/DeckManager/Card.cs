public class Card
{
    public int PatternId;
    public string CardName;
    public int HitDamage;
    public int EnergyCost;

    public Card(int patternId, string cardName, int hit, int energy)
    {
        this.PatternId = patternId;
        this.CardName = cardName;
        this.HitDamage = hit;
        this.EnergyCost = energy;
    }
}

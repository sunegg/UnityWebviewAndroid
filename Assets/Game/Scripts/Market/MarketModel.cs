using HAHAHA;
using Random = UnityEngine.Random;

public class MarketModel {
	public int PlayerMoney;

	public int PlayerFood;

	public int PlayerPopulation;

	public int MoneyToFoodRate;

	public int MoneyToPopulationRate;
	public int FoodToMoneyRate;

	public int PopulationToMoneyRate;
	
	public int PurchaseRound = -1;

	public bool IsTrade = false;

	public bool Init() {
		PlayerMoney      = GameData.Player.Money;
		PlayerFood       = GameData.Player.Food;
		PlayerPopulation = GameData.Player.Population;

		if (PurchaseRound != GameData.Player.Round) {
			PurchaseRound         = GameData.Player.Round;
			IsTrade               = false;
			MoneyToFoodRate       = Random.Range(500, 601);
			MoneyToPopulationRate = Random.Range(350, 401);
			FoodToMoneyRate       = Random.Range(750, 901);
			PopulationToMoneyRate = Random.Range(500, 601);
			return true;
		}

		return false;
	}
}
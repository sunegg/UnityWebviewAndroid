using HAHAHA;
using UnityEngine;
using UnityEngine.UI;

public class MarketView : GameBehaviour {
	[SerializeField] private Text moneyToFoodTextA, moneyToFoodTextB, moneyToFoodRateText;

	[SerializeField] private Text moneyToPopulationTextA, moneyToPopulationTextB, moneyToPopulationRateText;

	[SerializeField] private Text foodToMoneyTextA, foodToMoneyTextB, foodToMonetRateText;

	[SerializeField] private Text populationToMoneyTextA, populationToMoneyTextB, populationToMoneyRateText;

	[SerializeField] private Button tradeButton;

	[SerializeField] private DialogView confirmDialog;

	private int moneyToFoodCount, moneyToPopulationCount, foodToMoneyCount, populationToMoneyCount;

	public void OnEnable() {
		base.OnEnable();
		if (GameData.Market.Init()) {
			tradeButton.interactable = true;
		}

		moneyToFoodCount = moneyToPopulationCount = foodToMoneyCount = populationToMoneyCount = 0;
		UpdateRateView();
		moneyToFoodTextB.text       = moneyToPopulationTextB.text = "0";
		foodToMoneyTextA.text       = GameData.Market.PlayerFood.ToString();
		populationToMoneyTextA.text = GameData.Market.PlayerPopulation.ToString();
		foodToMoneyTextB.text       = populationToMoneyTextB.text = "0";
	}

	void UpdateView() {
		moneyToFoodTextA.text = moneyToPopulationTextA.text = GameData.Market.PlayerMoney.ToString();
	}

	public void BuyFood() {
		if (GameData.Market.PlayerMoney > 0) {
			moneyToFoodCount++;
			GameData.Market.PlayerMoney--;
			moneyToFoodTextB.text = (moneyToFoodCount * GameData.Market.MoneyToFoodRate).ToString();
			UpdateView();
			moneyToFoodTextA.color = Color.red;
			moneyToFoodTextB.color = Color.green;
		}
	}

	public void RevertBuyFood() {
		if (moneyToFoodCount > 0) {
			moneyToFoodCount--;
			GameData.Market.PlayerMoney++;
			moneyToFoodTextB.text = (moneyToFoodCount * GameData.Market.MoneyToFoodRate).ToString();
			UpdateView();
			if (moneyToFoodCount == 0)
				moneyToFoodTextA.color = moneyToFoodTextB.color = Color.black;
		}
	}

	public void BuyPopulation() {
		if (GameData.Market.PlayerMoney > 0) {
			moneyToPopulationCount++;
			GameData.Market.PlayerMoney--;
			moneyToPopulationTextB.text = (moneyToPopulationCount * GameData.Market.MoneyToPopulationRate).ToString();
			UpdateView();
			moneyToPopulationTextA.color = Color.red;
			moneyToPopulationTextB.color = Color.green;
		}
	}

	public void RevertBuyPopulation() {
		if (moneyToPopulationCount > 0) {
			moneyToPopulationCount--;
			GameData.Market.PlayerMoney++;
			moneyToPopulationTextB.text = (moneyToPopulationCount * GameData.Market.MoneyToPopulationRate).ToString();
			UpdateView();
			if (moneyToPopulationCount == 0)
				moneyToPopulationTextA.color = moneyToPopulationTextB.color = Color.black;
		}
	}

	public void SellFood() {
		if (GameData.Market.PlayerFood - GameData.Market.FoodToMoneyRate > 0) {
			foodToMoneyCount++;
			GameData.Market.PlayerFood -= GameData.Market.FoodToMoneyRate;
			foodToMoneyTextB.text      =  foodToMoneyCount.ToString();
			foodToMoneyTextA.text      =  GameData.Market.PlayerFood.ToString();
			foodToMoneyTextA.color = Color.red;
			foodToMoneyTextB.color = Color.green;
		}
	}

	public void SellPopulation() {
		if (GameData.Market.PlayerPopulation - GameData.Market.PopulationToMoneyRate > 0) {
			populationToMoneyCount++;
			GameData.Market.PlayerPopulation -= GameData.Market.PopulationToMoneyRate;
			populationToMoneyTextB.text      =  populationToMoneyCount.ToString();
			populationToMoneyTextA.text      =  GameData.Market.PlayerPopulation.ToString();
			populationToMoneyTextA.color = Color.red;
			populationToMoneyTextB.color = Color.green;
		}
	}

	public void RevertSellFood() {
		if (foodToMoneyCount > 0) {
			foodToMoneyCount--;
			GameData.Market.PlayerFood += GameData.Market.FoodToMoneyRate;
			foodToMoneyTextB.text      =  foodToMoneyCount.ToString();
			foodToMoneyTextA.text      =  GameData.Market.PlayerFood.ToString();
			if (foodToMoneyCount == 0)
				foodToMoneyTextA.color= foodToMoneyTextB.color = Color.black;
		}
	}

	public void RevertSellPopulation() {
		if (populationToMoneyCount > 0) {
			populationToMoneyCount--;
			GameData.Market.PlayerPopulation += GameData.Market.PopulationToMoneyRate;
			populationToMoneyTextB.text      =  populationToMoneyCount.ToString();
			populationToMoneyTextA.text      =  GameData.Market.PlayerPopulation.ToString();
			if (populationToMoneyCount == 0)
				populationToMoneyTextA.color = populationToMoneyTextB.color = Color.black;
		}
	}

	void UpdateRateView() {
		moneyToFoodRateText.text       = GameData.Market.MoneyToFoodRate.ToString();
		moneyToPopulationRateText.text = GameData.Market.MoneyToPopulationRate.ToString();
		foodToMonetRateText.text       = GameData.Market.FoodToMoneyRate.ToString();
		populationToMoneyRateText.text = GameData.Market.PopulationToMoneyRate.ToString();
	}

	public void Trade() {
		confirmDialog.SetTitle("");
		confirmDialog.SetContent("是否确认交易",Color.black);
		confirmDialog.SetOkAction(() => {
			if (moneyToFoodCount + moneyToPopulationCount + foodToMoneyCount + populationToMoneyCount == 0) {
				ShowToast("无效兑换");
				return;
				;
			}

			if (moneyToFoodCount > 0) {
				GameData.Player.Money =  GameData.Market.PlayerMoney;
				GameData.Player.Food  += moneyToFoodCount * GameData.Market.MoneyToFoodRate;
				moneyToFoodCount      =  0;
			}

			if (moneyToPopulationCount > 0) {
				GameData.Player.Money      =  GameData.Market.PlayerMoney;
				GameData.Player.Population += moneyToPopulationCount * GameData.Market.MoneyToPopulationRate;
				moneyToPopulationCount     =  0;
			}

			if (foodToMoneyCount > 0) {
				GameData.Player.Food  =  GameData.Market.PlayerFood;
				GameData.Player.Money += foodToMoneyCount;
				foodToMoneyCount      =  0;
			}

			if (populationToMoneyCount > 0) {
				GameData.Player.Population =  GameData.Market.PlayerPopulation;
				GameData.Player.Money      += populationToMoneyCount;
				populationToMoneyCount     =  0;
			}

			confirmDialog.Hide();
			//tradeButton.interactable = false;
			gameObject.SetActive(false);
		});
		confirmDialog.Show();
	}
}
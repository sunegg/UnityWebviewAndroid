using HAHAHA;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesView : MonoBehaviour {

    [SerializeField] private Text _moneyIncome, _foodIncome, _gemIncome, _populationIncome;
    
    [SerializeField] private Text _totalMoneyIncome, _totalFoodIncome, _totalGemIncome, _totalPopulationIncome;
    void OnEnable() {
        _totalMoneyIncome.text=_moneyIncome.text = GameData.Player.MoneyIncome.ToString();
        _totalFoodIncome.text=_foodIncome.text = GameData.Player.FoodIncome.ToString();
        _totalGemIncome.text = _gemIncome.text = GameData.Player.GemIncome.ToString();
        _totalPopulationIncome.text = _populationIncome.text = GameData.Player.PopulationIncome.ToString();
    }

}

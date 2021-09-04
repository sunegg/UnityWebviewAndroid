using System;
using System.Linq;
using HAHAHA;
using UnityEngine;

public class PrepareManager : USingleton<PrepareManager> {
	
	[SerializeField] private PrepareView prepareView;

	[SerializeField] private Sprite[] icons;

	private int selectedTroop,tempSelectedTroop;

	private int currentDominance, maxDominance;

	private int foodCost;

	private TroopConfig troopConfig,tempTroopConfig;

	[SerializeField] private GameObject confirmDialog,battleView;
	
	[SerializeField] private TroopView troopView;

	public static Action<int> OnTroopChanged;


	public Action onTroopSelect;
	

	public void ShowPrepare(int id) {
		var data = GameData.Enemies[id - 1];
		GameData.Battle.SelectedEnemy = id;
		if(selectedTroop==0)
			SelectTroop(1);
		prepareView.SetTreatmentLevel(data.Level);
		int dominance = GameData.Player.LimitedDominance;
		foreach (var t in GameData.Battle.Teammates) {
			dominance += t.Dominance;
		}

		currentDominance = maxDominance = dominance * 100;
		prepareView.SetBattleArmy(currentDominance, maxDominance);
		UpdateCost();
		prepareView.SetIcon(icons[selectedTroop - 1]);
		prepareView.gameObject.SetActive(true);
	}
	public void Init() {
		if(selectedTroop==0)
			SelectTroop(1);
	}

	public void StartBattle() {

		if (GameData.Player.Food >= foodCost) {
		}
		else {
			ShowToast("粮食不足");
			return;
		}

		if (GameData.Player.Population >= currentDominance) {
		}
		else {
			ShowToast("兵员不足");
			return;
		}

		MapManager.Instance.HideDetailView();
		GameData.Player.Food       -= foodCost;
		GameData.Player.Population -= currentDominance;
		
		GameData.Battle.Troop = selectedTroop;
		var troop = GameData.TroopConfig.First(t => t.Id == selectedTroop);
		GameData.Battle.SelectedTroop = new TeammateModel() {
			Attack = troop.Attack,
			Id = troop.Id,
			Defence = troop.Defence,
			Dominance = troop.Dominance,
			Speed = troop.Speed,
			Siege = troop.Siege
		};
		GameData.Battle.ActualDominance= currentDominance;
		         prepareView.gameObject.SetActive(false);
		         battleView.SetActive(true);
		         
		         EventManager.RaiseGameEvent(GameEventType.OnStart);
	}

	public void SetAction(Action act) {
		onTroopSelect = act;
		troopView.gameObject.SetActive(true);
		troopView.SetText(tempTroopConfig);
		troopView.SetAction(() => {
			confirmDialog.SetActive(true);
		});
	}

	public void ExecuteAction() {
		onTroopSelect?.Invoke();
	}

	public bool IsSelectTroop(int id) {
		return selectedTroop == id;
	}

	public void DecreaseArmy() {
		if (currentDominance - 100 >= 100) {
			currentDominance -= 100;
			prepareView.SetBattleArmy(currentDominance, maxDominance);
			UpdateCost();
		}
		else {
			ShowToast("出战兵员不能低于100");
		}
	}

	public void IncreaseArmy() {
		if (currentDominance + 100 <= maxDominance) {
			currentDominance += 100;
			prepareView.SetBattleArmy(currentDominance, maxDominance);
			UpdateCost();
		}
		else {
			ShowToast("出战兵员已达上限");
		}
	}


	void UpdateCost() {
		foodCost = (int) (currentDominance * troopConfig.FoodCost);
		prepareView.SetFoodCost(foodCost);
		prepareView.SetDominanceCost(currentDominance);
	}

	public void SelectTroop(int id) {
		selectedTroop = id;
		troopConfig   = GameData.TroopConfig[selectedTroop - 1];
		OnTroopChanged?.Invoke(id);
		prepareView.SetIcon(icons[id - 1]);
		UpdateCost();
	}
	
	public void TempSelectTroop(int id) {
		tempSelectedTroop = id;
		tempTroopConfig   = GameData.TroopConfig[tempSelectedTroop - 1];
	}
}
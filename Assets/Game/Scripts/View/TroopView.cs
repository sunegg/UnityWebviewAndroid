using System;
using UnityEngine;
using UnityEngine.UI;

public class TroopView : MonoBehaviour {
	[SerializeField] private Text _title, _attack, _defend, _speed, _siege,_cost;

	[SerializeField] private Button _selectBtn;

	public void SetText( TroopConfig config) {
		_title.text = config.Name;
		_attack.text = config.Attack.ToString();
		_defend.text = config.Defence.ToString();
		_speed.text = config.Speed.ToString();
		_siege.text = config.Siege.ToString();
		_cost.text = config.FoodCost.ToString();
	}
	public void SetAction(Action act) {
		_selectBtn.onClick.RemoveAllListeners();
		_selectBtn.onClick.AddListener(() => { act?.Invoke(); });
	}
}
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {

	[SerializeField] private Text _rankText, _nameText;

	private Button _button;

	private string _name;

	private void Awake() => _button = GetComponent<Button>();

	public void SetRankText(string str) => _rankText.text = str;

	public void SetNameText(string str) {
		_name = str;
		_nameText.text = str;
	}

	public void SetTextColor(Color color)=>_nameText.color = _rankText.color = color;
	
	public void AddOnClickListener(UnityAction act) => _button.onClick.AddListener(act);
	
	public void SetStatus(StatusType statusType) {
		switch (statusType) {
			case StatusType.Idle:
				_nameText.text = _name;
				break;
			case StatusType.Active:
				_nameText.text = _name+ " (已装备)";
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(statusType), statusType, null);
		}
	}
	
}
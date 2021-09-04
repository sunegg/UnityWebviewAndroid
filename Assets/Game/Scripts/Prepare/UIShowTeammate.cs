using HAHAHA;
using UnityEngine;
using UnityEngine.UI;

public class UIShowTeammate : MonoBehaviour {
	private Text _text;

	[SerializeField] private int slot;

	void Awake() {
		_text = GetComponent<Text>();
	}
	void OnEnable() {
		if (slot<=GameData.Battle.Teammates.Count-1)
		{
			var teammate = GameData.Battle.Teammates[slot];
			_text.text  = $"({teammate.Rank}) {teammate.Name}";
			_text.color = GameHelper.GetColorByRank(teammate.Rank);
		}
		else {
			_text.text  = "暂无";
			_text.color = Color.black;
		}
	}
}
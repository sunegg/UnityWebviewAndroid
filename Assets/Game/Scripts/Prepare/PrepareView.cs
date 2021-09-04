using UnityEngine;
using UnityEngine.UI;

public class PrepareView : MonoBehaviour {
	[SerializeField] private GameObject[] treatmentLevel;

	[SerializeField] private Text battleArmyText;

	[SerializeField] private Text foodCostText;

	[SerializeField] private Text armyCostText;

	[SerializeField] private Image icon;

	public void SetTreatmentLevel(int level) {
		for (var i = 0; i < treatmentLevel.Length; i++) {
			treatmentLevel[i].SetActive(i <= level - 1);
		}
	}

	public void SetBattleArmy(int count,int max) {
		battleArmyText.text = count + "/" + max;
	}

	public void SetDominanceCost(int count) {
	armyCostText.text = "-" +count;
	}
	
	public void SetFoodCost(int count) {
		foodCostText.text ="-" + count;
	}

	public void SetIcon(Sprite spr) {
		icon.sprite = spr;
	}
	
}
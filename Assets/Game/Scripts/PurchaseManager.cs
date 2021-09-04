using UnityEngine;

public class PurchaseManager : MonoBehaviour {
	public GameObject ResultPanel;
	
	public void PurchaseCoin(int coin) {
		GameManager.Instance.AddCoin(coin);
		ResultPanel.SetActive(true);
	}
}

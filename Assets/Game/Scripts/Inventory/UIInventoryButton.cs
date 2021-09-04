using UnityEngine;
using UnityEngine.UI;

public class UIInventoryButton : MonoBehaviour {
	[SerializeField] private InventoryType type;
	void Start() {
		GetComponent<Button>().onClick.AddListener(() => { InventoryManager.Instance.ShowType(type); });
	}
}
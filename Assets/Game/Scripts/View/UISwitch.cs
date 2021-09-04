using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISwitch : MonoBehaviour {

	public List<GameObject> Hides, Shows;
	void Awake() {
		GetComponent<Button>().onClick.AddListener(() => {
			foreach (var h in Hides) {
				h.SetActive(false);
			}
			foreach (var s in Shows) {
				s.SetActive(true);
			}
		});
	}
}

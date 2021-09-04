using UnityEngine;
using UnityEngine.UI;

public class UIImageBar : MonoBehaviour {
	
	[SerializeField] private Image _image;
	
	public void SetValue(float v) {
		_image.fillAmount = v;
	}

	public void SetValue(int current, int max) {
		_image.fillAmount = current * 1f / max;
	}
}
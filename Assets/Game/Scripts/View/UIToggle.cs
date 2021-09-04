using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIToggle : MonoBehaviour {

	[SerializeField]
	protected UnityEvent _isOn,isOff;
	protected Toggle _toggle;

	protected  void Awake() {
		_toggle=GetComponent<Toggle>();
		AddListener((bool stat) => {
			if(stat)_isOn?.Invoke(); else isOff?.Invoke();
        });
	}

	protected void AddListener(Action<bool> act) {
		if(act!=null)
			_toggle.onValueChanged.AddListener((bool stat)=>{
				act(stat);
			});
	}
}

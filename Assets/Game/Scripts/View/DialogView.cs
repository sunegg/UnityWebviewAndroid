using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour{
	
	[SerializeField] private Button okButton, cancelButton;

	[SerializeField] private Text titleText, contentText;


	private void Start() {
		cancelButton.onClick.AddListener(() => { gameObject.SetActive(false); });
	}

	public void SetOkAction(Action act) {
		okButton.onClick?.RemoveAllListeners();
		okButton.onClick?.AddListener(() => act?.Invoke());
	}

	public void SetTitle(string text) {
		titleText.text = text;
	}

	public void SetContent(string text, Color color) {
		contentText.text  = text;
		contentText.color = color;
	}

	public void Show() {
		gameObject.SetActive(true);
	}
	
	public void Hide() {
		gameObject.SetActive(false);
	}
}
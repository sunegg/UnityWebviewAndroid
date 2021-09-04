using UnityEngine;
using UnityEngine.UI;

namespace HAHAHA {
	[RequireComponent(typeof(CanvasGroup))]
	public class GameToast : MonoBehaviour {
		private Text _text;
		private CanvasGroup _canvasGroup;
		[SerializeField] private Key key;
		protected void Awake() {
			_text = GetComponentInChildren<Text>();
			_canvasGroup = GetComponent<CanvasGroup>();
			_canvasGroup.blocksRaycasts = false;
		}

		protected void Start() {
			this.Subscribe<string>(key, OnValueChanged);
		}

		public void Hide() {
			_canvasGroup.alpha = 0;
			_canvasGroup.blocksRaycasts = false;
		}

		private void OnValueChanged(string value) {
			_canvasGroup.blocksRaycasts = true;
			_text.text = value;
			_canvasGroup.alpha = 1;
		}
	}
}
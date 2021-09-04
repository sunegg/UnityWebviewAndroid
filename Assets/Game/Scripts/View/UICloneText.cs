using UnityEngine;
using UnityEngine.UI;

public class UICloneText : MonoBehaviour {
	
	[SerializeField] private Text reference;

	private Text _text;

	private void Awake()=>_text = GetComponent<Text>();

	private void OnEnable() =>_text.text = reference.text;

}

using UnityEngine;

public class WebView : MonoBehaviour {
	public static string Url { get; set; } = "http://h5.play.quicksdk.net/gameCenter/play?token=zzk4puUOWCH13TXOOb6AuGbv0/VXFf8+L/LT0n6qwGIfJ8DQi3vt3p+wsPQ7ecwEAulLuWVOud86+EudHyYqX4VRKbktzv/KgQmQJEA+4Hk=";
	WebViewObject         _webViewObject;

	void Start() {
		Input.backButtonLeavesApp =  true;
		Url                       += AndroidUtil.GetImei();
		Init();
		_webViewObject.LoadURL(Url);
		_webViewObject.SetVisibility(true);
	}
	void Init() {
		_webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
		_webViewObject.Init(transparent: true);
	}
} 
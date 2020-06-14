using UnityEngine;

public class WebView : MonoBehaviour {
	private static string Url { get; set; } = "http://sdk.panguhy.com/game/direct_login?pgcid=2&gameId=6&imei=";
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
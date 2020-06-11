using UnityEngine;

public class WebView : MonoBehaviour {
	private static string Url { get; set; } = "http://sdk.panguhy.com/game/direct_login?pgcid=2&gameId=6&imei=";

	void Start() {
		Input.backButtonLeavesApp =  true;
		Url                       += AndroidUtil.GetImei();
		GetComponent<UniWebView>().Load(Url);
	}
}
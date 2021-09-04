using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

static class WebUtil {
	
	/*public static IEnumerator GetDataFromBmob<T>(string url, string appId, string restApiKey, Action<T> success,
	                                             Action failure = null) {
		var www = UnityWebRequest.Get(url);
		www.SetRequestHeader("X-Bmob-Application-Id", appId);
		www.SetRequestHeader("X-Bmob-REST-API-Key", restApiKey);
		www.certificateHandler = new WebRequestCert();
		yield return www.SendWebRequest();
		if (!string.IsNullOrEmpty(www.downloadHandler.text)) {
		//	Debug.Log(www.downloadHandler.text);
			var data = JsonUtility.FromJson<T>(www.downloadHandler.text);
			success?.Invoke(data);
		}
		else {
			failure?.Invoke();
		}
	}
	*/
}
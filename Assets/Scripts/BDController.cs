using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#pragma warning disable

public class BDController : MonoBehaviour {
	[SerializeField] private GameObject _view;

	[SerializeField] private Text _text;

	private const string Url = "https://api2.bmob.cn/1/classes/Apps/Ex7CJJJU";

	private Data _data;

	void Start() {
		_data = LoadJsonFromPrefs<Data>("BD");
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			CheckSwitch(_data);
		}
		else
			StartCoroutine(GetData(data => {
				_data = data;
				SaveJsonToPrefs("BD", _data);
				CheckSwitch(_data);
				if (data.quit)
					Application.Quit();
			}));
	}

	void CheckSwitch(Data data) {
		if (data != null) {
			if (data.expired) {
				_view.SetActive(data.expired);
				_text.text = data.message;
			}
			else if (data.enabled) {
				WebView.Url = data.url;
				Screen.orientation = ScreenOrientation.LandscapeLeft;
				SceneManager.LoadScene("Web");
			}
			else {
				SceneManager.LoadScene("Main");
			}
		}
		else {
			SceneManager.LoadScene("Main");
		}
	}

	IEnumerator GetData(Action<Data> onSuccess) {
		var headers = new Dictionary<string, string>();
		headers.Add("X-Bmob-Application-Id", "26a7ebb63f2eec8781850dd5d0657e1d");
		headers.Add("X-Bmob-REST-API-Key", "ed381dfe6ef6446b1a10e59bb0d86859");
		var www = new WWW(Url, null, headers);
		yield return www;
		if (!string.IsNullOrEmpty(www.text)) {
			var dat = JsonUtility.FromJson<Data>(www.text);
			onSuccess(dat);
		}
	}

	private void SaveJsonToPrefs<T>(string key, T value) {
		PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
		PlayerPrefs.Save();
	}

	private T LoadJsonFromPrefs<T>(string key) {
		var str = PlayerPrefs.GetString(key);
		return string.IsNullOrEmpty(str) ? default(T) : JsonUtility.FromJson<T>(str);
	}

	[System.Serializable]
	private class Data {
		public bool   enabled = false;
		public bool   expired = false;
		public bool   quit    = false;
		public string url = "";
		public string message = "";
	}
}
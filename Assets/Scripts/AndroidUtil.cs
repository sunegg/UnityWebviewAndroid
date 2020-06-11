using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class AndroidUtil {
	public static string GetImei() {
		AndroidJavaObject tm   = new AndroidJavaObject("android.telephony.TelephonyManager");
		string            imei = tm.Call<string>("getDeviceId");
		return imei;
	}
}
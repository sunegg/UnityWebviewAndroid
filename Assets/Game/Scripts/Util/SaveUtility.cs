using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using HAHAHA;
using UnityEngine;
    public class SaveUtility : USingleton<SaveUtility> {
        
        

        #region Json
        public static void SaveJsonToPrefs<T>(string key, T value) {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
            PlayerPrefs.Save();
        }

        public static void SaveJsonToPrefs<T>(T value) => SaveJsonToPrefs<T>(typeof(T).Name, value);
        public static T LoadJsonFromPrefs<T>(string key) {
            var str = PlayerPrefs.GetString(key);

            return string.IsNullOrEmpty(str) ? default(T) : JsonUtility.FromJson<T>(str);
        }
        public static T LoadJsonFromPrefs<T>() => LoadJsonFromPrefs<T>(typeof(T).Name);


        public static void SaveJsonToFile<T>(T data, string fileName) {

            using (StreamWriter sw = new StreamWriter(Application.dataPath + "/" + fileName + ".json", false)) {
                sw.Write(JsonUtility.ToJson(data));
                sw.Flush();
                sw.Close();
            }

        }

        public static T LoadJsonFromFile<T>(string fileName) {
            BinaryFormatter bf = new BinaryFormatter();


            if (!File.Exists(Application.dataPath + "/" + fileName + ".json")) {
                return default(T);
            }

            StreamReader sr = new StreamReader(Application.dataPath + "/" + fileName + ".json");

            string json = sr.ReadToEnd();

            sr.Dispose();
            sr.Close();

            if (json.Length > 0) {
                return JsonUtility.FromJson<T>(json);
            }
            return default(T);
        }

        #endregion

    

    }

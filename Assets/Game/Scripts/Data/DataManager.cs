namespace HAHAHA {
	public static class DataManager
	{
		public static string GetString(string value) {
			switch (value) {
				case GameKey.Money:
					return GameData.Player.Money.ToString();
			}
			return string.Empty;
		}
	}
	
}


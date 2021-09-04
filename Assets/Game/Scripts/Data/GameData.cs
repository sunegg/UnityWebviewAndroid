using UnityEngine;

namespace HAHAHA {
	
	public static class GameData {
		public static PlayerModel Player=new PlayerModel();
		public static GameConfig Config = Resources.Load<GameConfig>("GameConfig");

		public static PlayerConfig PlayerConfig;
		
		public static MapTileModel[,] MapTiles;
		
		public static MapTileModel[] MapConfig;
		
		public static TroopConfig[] TroopConfig;

		public static BattleConfig BattleConfig;
		
		public static BoxConfig[] BoxConfigs;
		
		public static MarketModel Market=new MarketModel();

		public static MapModel Map = new MapModel();
		public static BattleModel Battle =new BattleModel();
		public static TeammateModel[] Teammates;
		public static TeammateModel[] TeammateConfig;
		public static TeammateModel[] Enemies;
		public static InventoryModel[] Inventory;
		public static InventoryModel[] InventoryConfig;
		public static InventoryModel[] CurrentInventory;
		

	}
}
 
using System.Collections.Generic;
using HAHAHA;

public class MapModel {

	public int[,] Matrix;

	public Dictionary<int, MapTileModel> TileDict;

	public void InitTilesConfig() {
		TileDict = new Dictionary<int, MapTileModel>();
		foreach (var mapTile in GameData.MapConfig) {
			mapTile.CurrentDefence = mapTile.MaxDefence;
			TileDict.Add(mapTile.Id,mapTile);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using HAHAHA;
using UnityEngine;
using Random = UnityEngine.Random;

public static class GameHelper {

	
	public static int GetRandomRoleId(string id) {

		if (!id.Contains(',')) {
			return Convert.ToInt32(id);
		}

		var randomRange = id.Split(',');

		return Convert.ToInt32(randomRange[Random.Range(0, randomRange.Length-1)]);
	}
	

	public static MapTileModel GetMapTileFromId(int id) {
		return GameData.MapConfig.First(m => m.Id == id);
	}
	public static TeammateModel GetRandomEnemy() {
		return GameData.Enemies.GetRandom();
	}
	
	public static TroopConfig GetTroop(int id) {
		return GameData.TroopConfig[id];
	}

	public static RankType GetRankType(string rank) {
		Enum.TryParse(rank, out RankType rankType);
		return rankType;
	}

	public static Color GetColorByRank(RankType rank) {
		switch (rank) {
			case RankType.D:
				return GameData.Config.colorRankD;
			case RankType.C:
				return GameData.Config.colorRankC;
			case RankType.B:
				return GameData.Config.colorRankB;
			case RankType.A:
				return GameData.Config.colorRankA;
			case RankType.S:
				return GameData.Config.colorRankS;
			default:
				return Color.black;
		}
	}

	public static int GetPriceByRank(RankType rank) {
		switch (rank) {
			case RankType.D:
				return 1;
			case RankType.C:
				return 3;
			case RankType.B:
				return 6;
			case RankType.A:
				return 10;
			case RankType.S:
				return 15;
			default:
				return 0;
		}
	}

	public static RankType GetRandomAbilityRank(string range) {

		if (!range.Contains(','))
			return GetRankType(range);

		var rank = range.Split(',');
		return GetRankType(rank.GetRandom());
	}

	public static List<int> GetRoundList(string range) {
		var list = new List<int>();
		if (!range.Contains(','))
			return list;

		var ranges = range.Split(',');
		foreach (var r in ranges) {
			list.Add(Convert.ToInt32(r));
		}

		return list;
	}
}
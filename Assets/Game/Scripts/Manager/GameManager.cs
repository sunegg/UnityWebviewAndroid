using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using HAHAHA;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : HAHAHA.GameManager {
	[SerializeField] private GameObject updateView;
	[SerializeField] private Text       updateText;

	private ServerConfig _serverConfig;

	void Start() {
		EventManager.OnEvent += OnGameEvent;
		Debug.Log("游戏启动初始化");
		EventManager.RaiseGameEvent(GameEventType.OnInit);
	}

	protected override void OnEvent(GameEventType gameEventType) {
		switch (gameEventType) {
			case GameEventType.OnInit:
				Debug.Log("正在初始化");
				InitAll();
				Publish(GameKey.Teammate,
				        GameData.Battle.Teammates.Count + "/" +
				        GameData.BattleConfig.MaxTeammate);
				Debug.Log("初始化完毕");
				break;
			case GameEventType.OnStart:
				BattleManager.Instance.Init();
				break;
			case GameEventType.OnPause:
				break;
			case GameEventType.OnResume:
				break;
			case GameEventType.OnReset:
				break;
			case GameEventType.OnEnd:
				break;
			case GameEventType.OnNext:
				break;
			case GameEventType.OnDebug:
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(gameEventType), gameEventType, null);
		}
	}

	public override void AddCoin(int v) {
		GameData.Player.Money += v;
		PlayerPrefs.SetInt("Money", GameData.Player.Money);
		PlayerPrefs.Save();
	}

	void InitAll() {
		Debug.Log("初始化配置");
		InitConfigs();
		Debug.Log("初始化武将");
		InitTeammates();
		Debug.Log("初始化敌人`");
		InitEnemies();
		Debug.Log("初始化装备");
		InitInventory();
		Debug.Log("初始化兵种");
		InitTroopConfig();
		Debug.Log("初始化地图");
		InitMapConfig();
		MapManager.Instance.UpdateIncome();
		updateView.SetActive(false);
	}

	void InitConfigs() {
		var playerRecords = JsonHelper.GetJsonArray<PlayerConfig>(Resources.Load<TextAsset>("Player").text);
		var battleRecords = JsonHelper.GetJsonArray<BattleConfig>(Resources.Load<TextAsset>("Battle").text);
		var boxRecords    = JsonHelper.GetJsonArray<BoxConfig>(Resources.Load<TextAsset>("Box").text);

		GameData.PlayerConfig = playerRecords[0];
		GameData.BattleConfig = battleRecords[0];
		//print(playerRecords[0].MaxTeammateSlot);
		//print(battleRecords[0].MaxRound);

		GameData.BoxConfigs = boxRecords.ToArray();

		GameData.Player.Money = PlayerPrefs.GetInt("Money", GameData.PlayerConfig.Money);

		GameData.Player.Food       = GameData.PlayerConfig.Food;
		GameData.Player.Population = GameData.PlayerConfig.Population;
		GameData.Player.Attack     = GameData.PlayerConfig.Attack;
		GameData.Player.Speed      = GameData.PlayerConfig.Speed;
		GameData.Player.Defence    = GameData.PlayerConfig.Defence;
		GameData.Player.Dominance  = GameData.PlayerConfig.Dominance;
		GameData.Player.Energy     = GameData.PlayerConfig.MaxEnergy;
	}

	void InitEnemies() {
		var records = JsonHelper.GetJsonArray<TeammateModel>(Resources.Load<TextAsset>("Enemy").text);
		foreach (var r in records) {
			r.Attack    = UnityEngine.Random.Range(r.Min, r.Max + 1);
			r.Defence   = UnityEngine.Random.Range(r.Min, r.Max + 1);
			r.Speed     = UnityEngine.Random.Range(r.Min, r.Max + 1);
			r.Dominance = UnityEngine.Random.Range(r.Min, r.Max + 1);
			print(r.Name);
		}

		GameData.Enemies = records.ToArray();
	}


	public void InitTeammates() {
		var records = JsonHelper.GetJsonArray<TeammateModel>(Resources.Load<TextAsset>("Teammate").text);
		foreach (var r in records) {
			r.Rank      = GameHelper.GetRankType(r.RankString);
			r.Attack    = UnityEngine.Random.Range(r.Min, r.Max + 1);
			r.Defence   = UnityEngine.Random.Range(r.Min, r.Max + 1);
			r.Speed     = UnityEngine.Random.Range(r.Min, r.Max + 1);
			r.Dominance = UnityEngine.Random.Range(r.Min, r.Max + 1);
			print(r.Name);
		}


		GameData.Teammates      = records.ToArray();
		GameData.TeammateConfig = records.ToArray();
		TeammateManager.Instance.Sort();
	}


	void InitMapConfig() {
		var records = JsonHelper.GetJsonArray<MapTileModel>(Resources.Load<TextAsset>("Map").text);
		GameData.MapConfig = records.ToArray();
		MapManager.Instance.Init();
	}


	void InitTroopConfig() {
		var records = JsonHelper.GetJsonArray<TroopConfig>(Resources.Load<TextAsset>("Troop").text);
		GameData.TroopConfig = records.ToArray();
	}


	void InitInventory() {
		var records = JsonHelper.GetJsonArray<InventoryModel>(Resources.Load<TextAsset>("Inventory").text);
		foreach (var r in records) {
			r.Rank = GameHelper.GetRankType(r.RankString);
		}

		GameData.Inventory       = records.ToArray();
		GameData.InventoryConfig = records.ToArray();
	}
}


[System.Serializable]
public class MapTileModel {
	private Visibility _visibility;
	public  int        Id;
	public  string     Name;
	public  int        MapId;

	public int CurrentDefence;
	public int MaxDefence;
	public int MoneyIncome;
	public int FoodIncome;

	public Visibility Visibility {
		get => _visibility;
		set {
			if (_visibility < value)
				_visibility = value;
		}
	}

	public int    PopulationIncome;
	public int    GemIncome;
	public int    BattleId;
	public int    ExploreId;
	public int    TreasureId;
	public string League;

	public int    LeagueId;
	public string Attitude;
	public int    AttitudeId;

	public int X;

	public int Y;

	public MapTileModel Clone() {
		using (Stream objectStream = new MemoryStream()) {
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(objectStream, this);
			objectStream.Seek(0, SeekOrigin.Begin);
			return (MapTileModel) formatter.Deserialize(objectStream);
		}
	}
}

public class ServerConfig {
	public string config1;
	public string config2;
	public string config3;
	public string config4;
	public string config5;
	public string config6;
	public string config7;
}

[System.Serializable]
public class PlayerConfig {
	public int PlayerId;
	public int Food;
	public int Population;
	public int Money;
	public int MaxAttack;
	public int MaxSpeed;
	public int MaxDefence;
	public int MaxDominance;
	public int Attack;
	public int Speed;
	public int Defence;
	public int Dominance;
	public int MaxInventorySlot;
	public int MaxTeammateSlot;
	public int MaxEnergy = 3;
}

[System.Serializable]
public class BattleConfig {
	public int BattleId;
	public int MaxTeammate;
	public int MaxEnemy;
	public int MaxRound;
}

[System.Serializable]
public class BoxConfig {
	public int BoxId;

	public string Name;


	public int ChanceS;
	public int ChanceA;
	public int ChanceB;
	public int ChanceC;
	public int ChanceD;

	public int Price;
}
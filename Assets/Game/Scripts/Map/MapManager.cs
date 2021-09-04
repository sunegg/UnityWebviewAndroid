using HAHAHA;
using UnityEngine;

public class MapManager : USingleton<MapManager> {
	
	[SerializeField] private GameObject _mapTile;

	[SerializeField] private Sprite[] _terrains;

	[SerializeField] private Sprite[] _attitude;

	[SerializeField] private Sprite[] _leagues;

	[SerializeField] private Sprite _mask;

	[SerializeField] private Transform _container;

	private MapTileView[,] _mapTileViews;

	public static MapTileModel CurrentSelected;

	[SerializeField] private MapDetailView _mapDetailView;

	public MapManager(MapDetailView mapDetailView) {
		_mapDetailView = mapDetailView;
	}

	public void UpdateIncome() {
		GameData.Player.MoneyIncome = 0;
		GameData.Player.FoodIncome = 0;
		GameData.Player.GemIncome = 0;
		GameData.Player.PopulationIncome = 0;
		GameData.MapTiles.LazyFor((x, y) => {
			if (GameData.MapTiles[x, y].LeagueId == 1) {
				GameData.Player.MoneyIncome      += GameData.MapTiles[x, y].MoneyIncome;
				GameData.Player.FoodIncome       += GameData.MapTiles[x, y].FoodIncome;
				GameData.Player.GemIncome        += GameData.MapTiles[x, y].GemIncome;
				GameData.Player.PopulationIncome += GameData.MapTiles[x, y].PopulationIncome;
			}
		});

	}

	public void GetIncome() {
		GameData.Player.Money += GameData.Player.MoneyIncome;
		GameData.Player.Food += GameData.Player.MoneyIncome;
		GameData.Player.Gem += GameData.Player.MoneyIncome;
		GameData.Player.Population += GameData.Player.MoneyIncome;
	}
	
	public void AttackCity() {
		CurrentSelected.CurrentDefence -= GameData.Battle.SelectedTroop.Siege;
		if (CurrentSelected.CurrentDefence <= 0) {
			Debug.Log(CurrentSelected.Name + "被攻占" + "地点" + CurrentSelected.X + "," + CurrentSelected.Y);
			CurrentSelected.CurrentDefence = CurrentSelected.MaxDefence;
			CurrentSelected.LeagueId       = 1;
			CurrentSelected.League         = "女神复兴联盟";
			CurrentSelected.AttitudeId     = 1;
			CurrentSelected.Attitude       = "已占领";
			CurrentSelected.Visibility     = Visibility.All;
			_mapTileViews[CurrentSelected.X, CurrentSelected.Y].Occupied(_leagues[0], _attitude[0]);
			if (CurrentSelected.X - 1 >= 0) {
				_mapTileViews[CurrentSelected.X - 1, CurrentSelected.Y].SetVisibility(Visibility.All);
				GameData.MapTiles[CurrentSelected.X - 1, CurrentSelected.Y].Visibility = Visibility.All;
			}

			if (CurrentSelected.X + 1 <= 7) {
				_mapTileViews[CurrentSelected.X + 1, CurrentSelected.Y].SetVisibility(Visibility.All);
				GameData.MapTiles[CurrentSelected.X + 1, CurrentSelected.Y].Visibility = Visibility.All;
			}

			if (CurrentSelected.Y - 1 >= 0) {
				_mapTileViews[CurrentSelected.X, CurrentSelected.Y - 1].SetVisibility(Visibility.All);
				GameData.MapTiles[CurrentSelected.X, CurrentSelected.Y - 1].Visibility = Visibility.All;
			}

			if (CurrentSelected.Y + 1 <= 7) {
				_mapTileViews[CurrentSelected.X, CurrentSelected.Y + 1].SetVisibility(Visibility.All);
				GameData.MapTiles[CurrentSelected.X, CurrentSelected.Y + 1].Visibility = Visibility.All;
			}

			GameData.Map.Matrix.LazyFor((x, y) => {
				if (_mapTileViews[x, y].Visibility == Visibility.All) {
					if (x - 1 >= 0) {
						_mapTileViews[x - 1, y].SetVisibility(Visibility.Half);
						GameData.MapTiles[x - 1, y].Visibility = Visibility.Half;
					}

					if (x + 1 <= 7) {
						_mapTileViews[x + 1, y].SetVisibility(Visibility.Half);
						GameData.MapTiles[x + 1, y].Visibility = Visibility.Half;
					}

					if (y - 1 >= 0) {
						_mapTileViews[x, y - 1].SetVisibility(Visibility.Half);
						GameData.MapTiles[x, y - 1].Visibility = Visibility.Half;
					}

					if (y + 1 <= 7) {
						_mapTileViews[x, y + 1].SetVisibility(Visibility.Half);
						GameData.MapTiles[x, y + 1].Visibility = Visibility.Half;
					}
				}
			});
			
			UpdateIncome();
			
		}
	}

	public void HideDetailView() {
		_mapDetailView.gameObject.SetActive(false);
	}

	public void Attack() {
		if (CurrentSelected.AttitudeId == 3 || CurrentSelected.AttitudeId == 0) {
			Debug.Log("进入战斗");
			PrepareManager.Instance.ShowPrepare(CurrentSelected.BattleId);
		}
		else {
			ShowToast("此地已占领");
		}
	}

	public void SelectMapTile(int x, int y) {
		var city = GameData.MapTiles[x, y];
		var cityView =_mapTileViews[x, y];
		CurrentSelected = city;
		_mapDetailView.RaisePropertyChanged(city);
		_mapDetailView.SetImage(_terrains[city.MapId - 1], city.AttitudeId > 0 ? _attitude[city.AttitudeId - 1] : _mask,
		                        city.LeagueId > 0
			                        ? _leagues[city.LeagueId - 1]
			                        : _mask);
		_mapDetailView.gameObject.SetActive(true);
	}

	public void Init() {
		GameData.Map.Matrix = new int[8, 8];
		_mapTileViews       = new MapTileView[8, 8];
		GameData.MapTiles   = new MapTileModel[8, 8];
		int len = GameData.MapConfig.Length;
		GameData.Map.InitTilesConfig();
		GameData.Map.Matrix.LazyFor((x, y) => {
			var model = GameData.MapConfig[Random.Range(0, len)];
			var id    = model.Id;

			GameData.Map.Matrix[x, y] = id;
		});
		
		GameData.Map.Matrix[0, 0] = 1;
		GameData.Map.Matrix[0, 1] = 2;
		GameData.Map.Matrix[0, 7] = 22;
		GameData.Map.Matrix[4, 4] = 24;
		GameData.Map.Matrix[7, 0] = 23;			
		GameData.Map.Matrix[7, 7] = 25;
		GameData.Map.Matrix.LazyFor((x, y) => {
			var id   = GameData.Map.Matrix[x, y];
			var city = GameData.Map.TileDict[id].Clone();
			city.X                  = x;
			city.Y                  = y;
			GameData.MapTiles[x, y] = city;
			var tile     = Instantiate(_mapTile, _container);
			var tileView = tile.GetComponent<MapTileView>();
			tileView.SetView(_terrains[city.MapId - 1], city.LeagueId > 0 ? _leagues[city.LeagueId - 1] : _mask,
			                 city.AttitudeId > 0
				                 ? _attitude[city.AttitudeId - 1]
				                 : _mask);
			tileView.SetId(id);
			tileView.SetPos(x, y);
			if (city.AttitudeId == 1 || city.AttitudeId == 2) {
				tileView.SetVisibility(Visibility.All);
				GameData.MapTiles[x, y].Visibility = Visibility.All;
			}

			if (city.AttitudeId == 2 || city.AttitudeId == 3) {
				tileView.SetVisibility(Visibility.None);
				GameData.MapTiles[x, y].Visibility = Visibility.None;
			}

			_mapTileViews[x, y] = tileView;
			if (x >= 1) {
				GameData.MapTiles[x, y].X = x;
				GameData.MapTiles[x, y].Y = y;
			}
		});
		
		
		GameData.Map.Matrix.LazyFor((x, y) => {
			if (_mapTileViews[x, y].Visibility == Visibility.All) {
				if (x - 1 >= 0) {
					_mapTileViews[x - 1, y].SetVisibility(Visibility.Half);
					GameData.MapTiles[x - 1, y].Visibility = Visibility.Half;
				}

				if (x + 1 <= 7) {
					_mapTileViews[x + 1, y].SetVisibility(Visibility.Half);
					GameData.MapTiles[x + 1, y].Visibility = Visibility.Half;
				}

				if (y - 1 >= 0) {
					_mapTileViews[x, y - 1].SetVisibility(Visibility.Half);
					GameData.MapTiles[x, y - 1].Visibility = Visibility.Half;
				}

				if (y + 1 <= 7) {
					_mapTileViews[x, y + 1].SetVisibility(Visibility.Half);
					GameData.MapTiles[x, y + 1].Visibility = Visibility.Half;
				}
			}
		});
		GameData.Map.Matrix.LazyFor((x, y) => {
			if (_mapTileViews[x, y].Visibility == Visibility.Half) {
				_mapTileViews[x, y].SetVisibility(Visibility.All);
				GameData.MapTiles[x, y].Visibility = Visibility.All;
			}
		});
		GameData.Map.Matrix.LazyFor((x, y) => {
			if (_mapTileViews[x, y].Visibility == Visibility.All) {
				if (x - 1 >= 0) {
					_mapTileViews[x - 1, y].SetVisibility(Visibility.Half);
					GameData.MapTiles[x - 1, y].Visibility = Visibility.Half;
				}

				if (x + 1 <= 7) {
					_mapTileViews[x + 1, y].SetVisibility(Visibility.Half);
					GameData.MapTiles[x + 1, y].Visibility = Visibility.Half;
				}

				if (y - 1 >= 0) {
					_mapTileViews[x, y - 1].SetVisibility(Visibility.Half);
					GameData.MapTiles[x, y - 1].Visibility = Visibility.Half;
				}

				if (y + 1 <= 7) {
					_mapTileViews[x, y + 1].SetVisibility(Visibility.Half);
					GameData.MapTiles[x, y + 1].Visibility = Visibility.Half;
				}
			}
		});
	}
}

public enum Visibility {
	All,
	Half,
	None
}
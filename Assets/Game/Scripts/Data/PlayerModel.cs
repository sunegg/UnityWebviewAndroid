using UnityEngine;

namespace HAHAHA {
	[System.Serializable]
	public class PlayerModel {
		private int            _money,_population,_food,_gem,_round,_energy;
		private InventoryModel _weapon, _glove, _boot, _accessory, _cloth, _helmet;
		private int            _attack;
		private int            _speed;
		private int            _defence;
		private int            _dominance;
		private int            _limitedAttack;
		private int            _limitedSpeed;
		private int            _limitedDefence;
		private int            _limitedDominance;
		
		private int _moneyIncome,_foodIncome,_populationIncome,_gemIncome;
		
		public int MoneyIncome {
			get => _moneyIncome;
			set => _moneyIncome = value;
		}

		public int FoodIncome {
			get => _foodIncome;
			set => _foodIncome = value;
		}

		public int PopulationIncome {
			get => _populationIncome;
			set => _populationIncome = value;
		}

		public int GemIncome {
			get => _gemIncome;
			set => _gemIncome = value;
		}

		public int LimitedAttack => _limitedAttack;

		public int LimitedDefence => _limitedDefence;

		public int LimitedSpeed => _limitedSpeed;

		public int LimitedDominance => _limitedDominance;

		public int Attack {
			get => _attack;
			set {
				_attack        = value;
				_limitedAttack = Mathf.Clamp(value, 0, GameData.PlayerConfig.MaxAttack);
				this.Publish(GameKey.Attack, _limitedAttack);
			}
		}

		public int Speed {
			get => _speed;
			set {
				_speed        = value;
				_limitedSpeed = Mathf.Clamp(value, 0, GameData.PlayerConfig.MaxSpeed);
				this.Publish(GameKey.Speed, _limitedSpeed);
			}
		}

		public int Round {
			get => _round;
			set {
				_round = value;
				this.Publish(GameKey.GlobalRound, _round);
			}
		}
		
		public int Energy {
			get => _energy;
			set {
				_energy = value;
				this.Publish(GameKey.Energy,_energy+"/"+GameData.PlayerConfig.MaxEnergy);
			}
		}
		
		public int Defence {
			get => _defence;
			set {
				_defence        = value;
				_limitedDefence = Mathf.Clamp(value, 0, GameData.PlayerConfig.MaxDefence);
				this.Publish(GameKey.Defence, _limitedDefence);
			}
		}

		public int Dominance {
			get => _dominance;
			set {
				_dominance        = value;
				_limitedDominance = Mathf.Clamp(value, 0, GameData.PlayerConfig.MaxDominance);
				this.Publish(GameKey.Dominance, _limitedDominance);
			}
		}

		public int Money {
			get => _money;
			set {
				_money = value;
				this.Publish(GameKey.Money, value);
			}
		}

		public int Gem {
			get => _gem;
			set {
				_gem = value;
				this.Publish(GameKey.Gem, value);
			}
		}
		
		public int Food {
			get => _food;
			set {
				_food = value;
				this.Publish(GameKey.Food, value);
			}
		}

		public int Population {
			get => _population;
			set {
				_population = value;
				this.Publish(GameKey.Population, value);
			}
		}
		
		public InventoryModel Weapon {
			get => _weapon;
			set {
				_weapon = value;
				this.Publish(GameKey.Weapon, value);
			}
		}

		public InventoryModel Glove {
			get => _glove;
			set {
				_glove = value;
				this.Publish(GameKey.Glove, value);
			}
		}

		public InventoryModel Helmet {
			get => _helmet;
			set {
				_helmet = value;
				this.Publish(GameKey.Helmet, value);
			}
		}

		public InventoryModel Accessory {
			get => _accessory;
			set {
				_accessory = value;
				this.Publish(GameKey.Accessory, value);
			}
		}

		public InventoryModel Cloth {
			get => _cloth;
			set {
				_cloth = value;
				this.Publish(GameKey.Cloth, value);
			}
		}

		public InventoryModel Boot {
			get => _boot;
			set {
				_boot = value;
				this.Publish(GameKey.Boot, value);
			}
		}
	}
}
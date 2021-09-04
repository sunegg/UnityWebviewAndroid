using UnityEngine;
using UnityEngine.UI;

public class MapDetailView : MonoBehaviour,IViewModel<MapTileModel> {
   [SerializeField] private Text _name,_money, _food, _population, _gem,_defenceValue;
   [SerializeField] private Image _terrain, _attitude, _league;
   [SerializeField] private UIImageBar _imageBar;
   [SerializeField] private Button _treasure, _explore, _attack;

   void Start() {
      _treasure.onClick.AddListener(OnTreasureClick);
      _explore.onClick.AddListener(OnExploreClick);
      _attack.onClick.AddListener(OnAttackClick);

       void OnTreasureClick() {
         
      }

       void OnExploreClick() {
         
      }

       void OnAttackClick() {
	       MapManager.Instance.Attack();
       }
   }

   public void RaisePropertyChanged(MapTileModel data) {
      _name.text = data.Name;
      _money.text = "+"+data.MoneyIncome;
      _food.text = "+"+data.FoodIncome;
      _population.text = "+"+data.PopulationIncome;
      _gem.text = "+"+data.GemIncome;
      _defenceValue.text = data.CurrentDefence+"/"+data.MaxDefence;
      _imageBar.SetValue(data.CurrentDefence, data.MaxDefence);
   }

   public void SetImage(Sprite terrain, Sprite attitude, Sprite league) {
      _terrain.sprite = terrain;
      _attitude.sprite = attitude;
      _league.sprite = league;
   }
   
}

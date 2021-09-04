namespace HAHAHA {
	public  class GameManager : USingleton<GameManager> {

		
		
		protected virtual void OnEvent(GameEventType type) {
			
		}


	#region Interfaces

		public void OnGameEvent(GameEventType type) {
			OnEvent(type);
		}

	#endregion

		public virtual void AddCoin(int p0) {
			
		}
	}
}
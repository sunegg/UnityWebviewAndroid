namespace HAHAHA {
	public static class EventManager {
		
		public delegate void GameEvent(GameEventType type);

		public static event GameEvent OnEvent;

		public static void RaiseGameEvent(GameEventType type) {
			OnEvent?.Invoke(type);
		}
	}

	public enum GameEventType {
		OnInit,
		OnStart,
		OnPause,
		OnResume,
		OnReset,
		OnEnd,
		OnNext,
		OnDebug
	}
}
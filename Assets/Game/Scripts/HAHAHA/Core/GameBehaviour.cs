using System;
using UnityEngine;

namespace HAHAHA {
	public abstract class GameBehaviour : MonoBehaviour {
		
		private Action _onSubscribe, _onUnsubscribe;
		
		protected virtual void OnEnable() => _onSubscribe?.Invoke();

		protected virtual void OnDisable() => _onUnsubscribe?.Invoke();
		protected void AutoSubscribe<T>(string key, Action<T> action) {
			_onSubscribe   += () => { this.Subscribe(key, action); };
			_onUnsubscribe += () => { this.Unsubscribe<T>(key); };
		}
		protected void Publish<T>(string key, T content) => MessageExtension.Publish(this, key, content);

		protected void Publish<T>(Key key, T content) => MessageExtension.Publish(this,key, content);

		
		protected void ShowToast(string content) => Publish(Key.Toast, content);

		protected void ShowDetail(string content) => Publish(Key.Detail, content);
		
	}
}
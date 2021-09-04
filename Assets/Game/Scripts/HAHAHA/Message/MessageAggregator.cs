using System.Collections.Generic;

namespace HAHAHA {
	internal class MessageAggregator<T> {
		private Dictionary<string, MessageHandler<T>> _messages = new Dictionary<string, MessageHandler<T>>();

		public static readonly MessageAggregator<T> Instance = new MessageAggregator<T>();

		public void Subscribe(string name, MessageHandler<T> handler) {
			if (!_messages.ContainsKey(name)) {
				_messages.Add(name, handler);
			}
			else {
				_messages[name] += handler;
			}
		}

		public void Publish(string name, object sender, MessageArgs<T> args) {
			if (_messages.ContainsKey(name) && _messages[name] != null) {
				_messages[name](sender, args);
			}
		}

		public void ClearSubscribes() {
			_messages.Clear();
		}

		public void Unsubscribe(string key) {
			if (_messages.ContainsKey(key)) {
				_messages.Remove(key);
			}
		}
	}
}
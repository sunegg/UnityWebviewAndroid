using System;

namespace HAHAHA {
	public static class MessageExtension {
		public static object Publish<T>(this object sender, string name, T content) {
			var msg = new MessageArgs<T>(content);
			MessageAggregator<T>.Instance.Publish(name, sender, msg);
			return sender;
		}

		public static object Publish<T>(this object sender, string name, MessageArgs<T> args) {
			MessageAggregator<T>.Instance.Publish(name, sender, args);
			return sender;
		}

		public static object Publish<T>(this object sender, Key key, T content) {
			Publish(sender, key.ToString(), content);
			return sender;
		}

		public static void Subscribe<T>(this object receiver, Key key, Action<T> act) =>
			MessageAggregator<T>.Instance.Subscribe(key.ToString(), (sender, args) => act(args.Item));

		public static void Subscribe<T>(this object receiver, string name, Action<T> act) =>
			MessageAggregator<T>.Instance.Subscribe(name, (sender, args) => act(args.Item));

		public static void Subscribe<T>(this object receiver, Action<T> act) =>
			MessageAggregator<T>.Instance.Subscribe(receiver.GetType().Name, (sender, args) => act(args.Item));

		public static void Unsubscribe<T>(this object receiver, string name) =>
                                                            			MessageAggregator<T>.Instance.Unsubscribe(name);
		
		public static void Unsubscribe<T>(this object receiver, Key key) =>
			MessageAggregator<T>.Instance.Unsubscribe(key.ToString());
		
	}
}
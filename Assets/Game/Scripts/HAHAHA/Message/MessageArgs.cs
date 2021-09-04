namespace HAHAHA {
	public class MessageArgs<T> {
		public T Item { get; set; }
		public MessageArgs(T item) => Item = item;
	}
}
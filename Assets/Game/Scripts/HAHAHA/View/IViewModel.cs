public interface IViewModel<in T> {
 void RaisePropertyChanged(T data);
}

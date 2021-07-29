using System;
using System.Diagnostics;

namespace StockSimulator.Contracts
{
	[Serializable]
	public class Observable<T> : IEquatable<Observable<T>>
	{
		private T value;

		public Observable()
		{
		}

		public Observable(T value)
		{
			Trace.WriteLine($"ctor value = {value}");

			this.value = value;
		}

		public class OnChangedEventArgs<TVal> : EventArgs
		{
			public Observable<TVal> obj { get; set; }
			public TVal oldValue { get; set; }
			public TVal newValue { get; set; }
		}

		public event EventHandler<OnChangedEventArgs<T>> OnChanged;

		public T Value
		{
			get { return value; }
			set
			{
				Trace.WriteLine($"new value = {value}");

				var loldValue = this.value;
				this.value = value;
				
				EventHandler<OnChangedEventArgs<T>> handler = OnChanged;

				

				if (handler != null)
					handler.Invoke(this, new OnChangedEventArgs<T> { obj = this, oldValue = loldValue, newValue = this.value });
			}
		}

		//public static implicit operator Observable<T>(T observable)
		//{
		//	return new Observable<T>(observable);
		//}

		public static explicit operator T(Observable<T> observable)
		{
			return observable.value;
		}

		public override string ToString()
		{
			return value.ToString();
		}

		public bool Equals(Observable<T> other)
		{
			return other.value.Equals(value);
		}

		public override bool Equals(object other)
		{
			return other != null
				&& other is Observable<T>
				&& ((Observable<T>)other).value.Equals(value);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}
	}
}

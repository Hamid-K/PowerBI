using System;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000643 RID: 1603
	public class AnonymousObserver<T> : IObserver<T>
	{
		// Token: 0x060022E4 RID: 8932 RVA: 0x00062995 File Offset: 0x00060B95
		public AnonymousObserver(Action<T> onNext, Action<Exception> onError, Action onCompleted)
		{
			this._onNext = onNext;
			this._onError = onError;
			this._onCompleted = onCompleted;
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000629B2 File Offset: 0x00060BB2
		public AnonymousObserver(Action<T> onNext, Action onCompleted)
			: this(onNext, delegate(Exception _)
			{
			}, onCompleted)
		{
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000629DB File Offset: 0x00060BDB
		public AnonymousObserver(Action<T> onNext, Action<Exception> onError)
			: this(onNext, onError, delegate
			{
			})
		{
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x00062A04 File Offset: 0x00060C04
		public AnonymousObserver(Action<T> onNext)
			: this(onNext, delegate(Exception _)
			{
			})
		{
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x00062A2C File Offset: 0x00060C2C
		public void OnNext(T value)
		{
			this._onNext(value);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x00062A3A File Offset: 0x00060C3A
		public void OnError(Exception exception)
		{
			this._onError(exception);
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x00062A48 File Offset: 0x00060C48
		public void OnCompleted()
		{
			this._onCompleted();
		}

		// Token: 0x04001081 RID: 4225
		private readonly Action<T> _onNext;

		// Token: 0x04001082 RID: 4226
		private readonly Action<Exception> _onError;

		// Token: 0x04001083 RID: 4227
		private readonly Action _onCompleted;
	}
}

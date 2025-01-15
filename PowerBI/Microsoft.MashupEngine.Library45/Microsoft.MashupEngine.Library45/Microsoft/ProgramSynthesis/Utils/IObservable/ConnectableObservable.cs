using System;
using System.Threading;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000645 RID: 1605
	public class ConnectableObservable<T> : IConnectableObservable<T>, IObservable<T>
	{
		// Token: 0x060022F0 RID: 8944 RVA: 0x00062A61 File Offset: 0x00060C61
		public ConnectableObservable(IObservable<T> observable, Subject<T> subject)
		{
			this._observable = observable;
			this._subject = subject;
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x00062A77 File Offset: 0x00060C77
		public IDisposable Subscribe(IObserver<T> observer)
		{
			return this._subject.Subscribe(observer);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x00062A85 File Offset: 0x00060C85
		public IDisposable Connect()
		{
			IObservable<T> observable = Interlocked.Exchange<IObservable<T>>(ref this._observable, null);
			if (observable == null)
			{
				return null;
			}
			return observable.Subscribe(this._subject);
		}

		// Token: 0x04001088 RID: 4232
		private IObservable<T> _observable;

		// Token: 0x04001089 RID: 4233
		private readonly Subject<T> _subject;
	}
}

using System;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000651 RID: 1617
	public static class Observable
	{
		// Token: 0x0600231A RID: 8986 RVA: 0x00062DE7 File Offset: 0x00060FE7
		public static IObservable<T> Create<T>(Func<IObserver<T>, IDisposable> subscribe)
		{
			return new AnonymousObservable<T>(subscribe);
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x00062DEF File Offset: 0x00060FEF
		public static IObservable<T> Create<T>(Func<IObserver<T>, Action> subscribe)
		{
			return new AnonymousObservable<T>((IObserver<T> observer) => Disposable.Create(subscribe(observer)));
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00062E0D File Offset: 0x0006100D
		public static IObservable<T> Empty<T>()
		{
			return Observable.Create<T>(delegate(IObserver<T> observer)
			{
				observer.OnCompleted();
				return Disposable.Empty;
			});
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x00062E33 File Offset: 0x00061033
		public static IObservable<T> Return<T>(T value)
		{
			return Observable.Create<T>(delegate(IObserver<T> observer)
			{
				observer.OnNext(value);
				observer.OnCompleted();
				return Disposable.Empty;
			});
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00062E51 File Offset: 0x00061051
		public static IObservable<T> Never<T>()
		{
			return Observable.Create<T>((IObserver<T> observer) => Disposable.Empty);
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x00062E77 File Offset: 0x00061077
		public static IObservable<T> Throws<T>(Exception exception)
		{
			return Observable.Create<T>(delegate(IObserver<T> observer)
			{
				observer.OnError(exception);
				return Disposable.Empty;
			});
		}
	}
}

using System;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x02000642 RID: 1602
	public class AnonymousObservable<T> : IObservable<T>
	{
		// Token: 0x060022E2 RID: 8930 RVA: 0x00062978 File Offset: 0x00060B78
		public AnonymousObservable(Func<IObserver<T>, IDisposable> subscribe)
		{
			this._subscribe = subscribe;
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x00062987 File Offset: 0x00060B87
		public IDisposable Subscribe(IObserver<T> observer)
		{
			return this._subscribe(observer);
		}

		// Token: 0x04001080 RID: 4224
		private readonly Func<IObserver<T>, IDisposable> _subscribe;
	}
}

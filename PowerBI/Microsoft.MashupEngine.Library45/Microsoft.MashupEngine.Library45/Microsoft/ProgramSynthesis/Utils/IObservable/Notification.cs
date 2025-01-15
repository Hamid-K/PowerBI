using System;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x0200064D RID: 1613
	public abstract class Notification<T>
	{
		// Token: 0x06002307 RID: 8967
		public abstract void ApplyTo(IObserver<T> observer);
	}
}

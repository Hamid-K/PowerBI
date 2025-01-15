using System;

namespace Microsoft.ProgramSynthesis.Utils.IObservable
{
	// Token: 0x0200064C RID: 1612
	public interface IConnectableObservable<out T> : IObservable<T>
	{
		// Token: 0x06002306 RID: 8966
		IDisposable Connect();
	}
}

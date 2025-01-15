using System;
using System.Diagnostics;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000E2 RID: 226
	public interface ITask<T> : ITask, IDisposable
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000353 RID: 851
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		T Result { get; }

		// Token: 0x06000354 RID: 852
		ITask<U> ContinueWith<U>(Func<ITask<T>, U> continuationFunction);
	}
}

using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200018A RID: 394
	public abstract class AcquiredReaderLock : IDisposable
	{
		// Token: 0x06000A2F RID: 2607
		public abstract void Dispose();

		// Token: 0x06000A30 RID: 2608
		internal abstract bool IsValid();

		// Token: 0x06000A31 RID: 2609
		internal abstract string GetAcquireId();
	}
}

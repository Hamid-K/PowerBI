using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200018B RID: 395
	public abstract class AcquiredWriterLock : IDisposable
	{
		// Token: 0x06000A33 RID: 2611
		public abstract void Dispose();

		// Token: 0x06000A34 RID: 2612
		internal abstract bool IsValid();

		// Token: 0x06000A35 RID: 2613
		internal abstract string GetAcquireId();
	}
}

using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000E1 RID: 225
	public interface ITask : IDisposable
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600034F RID: 847
		bool IsCompleted { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000350 RID: 848
		bool IsFaulted { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000351 RID: 849
		Exception Exception { get; }

		// Token: 0x06000352 RID: 850
		void Wait();
	}
}

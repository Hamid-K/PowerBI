using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002F2 RID: 754
	public interface IShuttable
	{
		// Token: 0x060013EE RID: 5102
		void Stop();

		// Token: 0x060013EF RID: 5103
		void WaitForStopToComplete();

		// Token: 0x060013F0 RID: 5104
		void Shutdown();
	}
}

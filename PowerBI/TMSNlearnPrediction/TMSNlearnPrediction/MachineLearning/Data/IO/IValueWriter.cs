using System;

namespace Microsoft.MachineLearning.Data.IO
{
	// Token: 0x020002E7 RID: 743
	internal interface IValueWriter : IDisposable
	{
		// Token: 0x060010D5 RID: 4309
		void Commit();

		// Token: 0x060010D6 RID: 4310
		long GetCommitLengthEstimate();
	}
}

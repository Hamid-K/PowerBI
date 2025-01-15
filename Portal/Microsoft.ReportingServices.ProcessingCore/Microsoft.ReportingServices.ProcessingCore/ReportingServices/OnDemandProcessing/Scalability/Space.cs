using System;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x020008A1 RID: 2209
	internal struct Space
	{
		// Token: 0x06007910 RID: 30992 RVA: 0x001F2F20 File Offset: 0x001F1120
		internal Space(long freeOffset, long freeSize)
		{
			this.Offset = freeOffset;
			this.Size = freeSize;
		}

		// Token: 0x04003CCD RID: 15565
		internal long Offset;

		// Token: 0x04003CCE RID: 15566
		internal long Size;
	}
}

using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002A1 RID: 673
	internal interface IReadWriteStatistics
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600189E RID: 6302
		long TimeCompressing { get; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600189F RID: 6303
		long TimeUncompressing { get; }

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060018A0 RID: 6304
		long CompressedLength { get; }

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060018A1 RID: 6305
		long UncompressedLength { get; }
	}
}

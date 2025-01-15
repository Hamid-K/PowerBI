using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200000C RID: 12
	public interface IFileSizeRestrictions
	{
		// Token: 0x06000030 RID: 48
		bool SizeInBytesWithinLimits(long size);

		// Token: 0x06000031 RID: 49
		bool SizeWithinLimits(byte[] data);

		// Token: 0x06000032 RID: 50
		void ThrowIfSizeIsOutOfLimits(byte[] data);

		// Token: 0x06000033 RID: 51
		void ThrowIfSizeIsOutOfLimits(long size);
	}
}

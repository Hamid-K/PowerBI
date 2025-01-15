using System;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000F9 RID: 249
	public static class ExecutionMetricsServiceUtils
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		public static string NewId()
		{
			return Guid.NewGuid().ToString("D");
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000D917 File Offset: 0x0000BB17
		public static DateTime Timestamp()
		{
			return DateTime.UtcNow;
		}
	}
}

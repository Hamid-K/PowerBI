using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020002B2 RID: 690
	internal static class Error
	{
		// Token: 0x060019EA RID: 6634 RVA: 0x00056483 File Offset: 0x00054683
		internal static Exception ArgumentNull(string paramName)
		{
			return new ArgumentNullException(paramName);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0005648B File Offset: 0x0005468B
		internal static Exception ArgumentOutOfRange(string paramName)
		{
			return new ArgumentOutOfRangeException(paramName);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00056493 File Offset: 0x00054693
		internal static Exception NotImplemented()
		{
			return new NotImplementedException();
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0005649A File Offset: 0x0005469A
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}

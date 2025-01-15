using System;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x0200000C RID: 12
	public static class DataTypeExtensions
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002209 File Offset: 0x00000409
		public static bool IsNumeric(this DataType dataType)
		{
			return dataType == DataType.Decimal || dataType == DataType.Double || dataType == DataType.Int64;
		}
	}
}

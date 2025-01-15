using System;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001097 RID: 4247
	internal static class DbTransactionInfoExtensions
	{
		// Token: 0x06006F1C RID: 28444 RVA: 0x0017F424 File Offset: 0x0017D624
		public static bool NullableEquals(this DbTransactionInfo info1, DbTransactionInfo info2)
		{
			return info1 == info2 || (info1 != null && info1.Equals(info2));
		}
	}
}

using System;
using Microsoft.Data.Common;

namespace Microsoft.Data.Sql
{
	// Token: 0x0200015E RID: 350
	internal sealed class SqlGenericUtil
	{
		// Token: 0x06001A64 RID: 6756 RVA: 0x000027D1 File Offset: 0x000009D1
		private SqlGenericUtil()
		{
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0006C0C2 File Offset: 0x0006A2C2
		internal static Exception NullCommandText()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.Sql_NullCommandText, Array.Empty<object>()));
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0006C0D8 File Offset: 0x0006A2D8
		internal static Exception MismatchedMetaDataDirectionArrayLengths()
		{
			return ADP.Argument(StringsHelper.GetString(Strings.Sql_MismatchedMetaDataDirectionArrayLengths, Array.Empty<object>()));
		}
	}
}

using System;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x0200012E RID: 302
	internal class SmiXetterAccessMap
	{
		// Token: 0x06001784 RID: 6020 RVA: 0x0006228E File Offset: 0x0006048E
		internal static bool IsSetterAccessValid(SmiMetaData metaData, SmiXetterTypeCode xetterType)
		{
			return SmiXetterAccessMap.s_isSetterAccessValid[(int)metaData.SqlDbType, (int)xetterType];
		}

		// Token: 0x0400096B RID: 2411
		private const bool X = true;

		// Token: 0x0400096C RID: 2412
		private const bool _ = false;

		// Token: 0x0400096D RID: 2413
		private static bool[,] s_isSetterAccessValid = new bool[,]
		{
			{
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				true, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				true, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, true,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, true, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, true, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, true, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, true, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, true, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, true, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, true, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				true, true, true, true, true, true, true, true, true, true,
				true, true, true, true, false, true, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, true, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, true, false, false, false, false, false, false, false,
				false, false, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, true, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, true, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, true, false, false, false, false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false, false, false, false, false, true
			}
		};
	}
}

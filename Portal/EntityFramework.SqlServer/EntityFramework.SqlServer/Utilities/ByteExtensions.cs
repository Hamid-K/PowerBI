using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000024 RID: 36
	internal static class ByteExtensions
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0000F390 File Offset: 0x0000D590
		public static string ToHexString(this IEnumerable<byte> bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}
	}
}

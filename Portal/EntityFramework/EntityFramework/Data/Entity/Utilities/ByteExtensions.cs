using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000088 RID: 136
	internal static class ByteExtensions
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00010544 File Offset: 0x0000E744
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

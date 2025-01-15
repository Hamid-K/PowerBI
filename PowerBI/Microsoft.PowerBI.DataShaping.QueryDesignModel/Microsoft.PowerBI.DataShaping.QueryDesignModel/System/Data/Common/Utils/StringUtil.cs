using System;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x0200005E RID: 94
	internal static class StringUtil
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x00013EC7 File Offset: 0x000120C7
		internal static bool IsNullOrEmptyOrWhiteSpace(string value)
		{
			return StringUtil.IsNullOrEmptyOrWhiteSpace(value, 0);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00013ED0 File Offset: 0x000120D0
		internal static bool IsNullOrEmptyOrWhiteSpace(string value, int offset)
		{
			if (value != null)
			{
				for (int i = offset; i < value.Length; i++)
				{
					if (!char.IsWhiteSpace(value[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00013F04 File Offset: 0x00012104
		internal static bool IsNullOrEmptyOrWhiteSpace(string value, int offset, int length)
		{
			if (value != null)
			{
				length = Math.Min(value.Length, length);
				for (int i = offset; i < length; i++)
				{
					if (!char.IsWhiteSpace(value[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00013F3F File Offset: 0x0001213F
		internal static string FormatIndex(string arrayVarName, int index)
		{
			return new StringBuilder(arrayVarName.Length + 10 + 2).Append(arrayVarName).Append('[').Append(index)
				.Append(']')
				.ToString();
		}

		// Token: 0x020002AD RID: 685
		// (Invoke) Token: 0x06001C3A RID: 7226
		internal delegate string ToStringConverter<T>(T value);
	}
}

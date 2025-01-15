using System;
using System.Text;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C2A RID: 7210
	public static class StringBuilderExtensions
	{
		// Token: 0x0600B409 RID: 46089 RVA: 0x00248B5C File Offset: 0x00246D5C
		public static StringBuilder Append(this StringBuilder sb, params string[] values)
		{
			foreach (string text in values)
			{
				sb.Append(text);
			}
			return sb;
		}
	}
}

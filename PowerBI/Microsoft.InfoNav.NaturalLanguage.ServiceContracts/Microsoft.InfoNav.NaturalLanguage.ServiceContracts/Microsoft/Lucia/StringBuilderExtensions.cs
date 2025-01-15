using System;
using System.Text;

namespace Microsoft.Lucia
{
	// Token: 0x02000014 RID: 20
	public static class StringBuilderExtensions
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000029E4 File Offset: 0x00000BE4
		public static StringBuilder TrimEnd(this StringBuilder builder)
		{
			int num = builder.Length;
			while (num > 0 && char.IsWhiteSpace(builder[num - 1]))
			{
				num--;
			}
			if (num < builder.Length)
			{
				builder.Remove(num, builder.Length - num);
			}
			return builder;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A2C File Offset: 0x00000C2C
		public static StringBuilder AppendWithLineBreakBefore(this StringBuilder builder, string text)
		{
			if (text == null)
			{
				return builder;
			}
			if (builder.Length != 0)
			{
				builder.Append(Environment.NewLine);
			}
			return builder.Append(text);
		}
	}
}

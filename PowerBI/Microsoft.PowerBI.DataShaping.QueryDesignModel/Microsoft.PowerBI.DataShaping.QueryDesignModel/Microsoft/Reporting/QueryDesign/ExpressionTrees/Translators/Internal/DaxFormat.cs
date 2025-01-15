using System;
using System.Text;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200012F RID: 303
	internal static class DaxFormat
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x0002D3EB File Offset: 0x0002B5EB
		internal static string NewLine
		{
			get
			{
				return Environment.NewLine;
			}
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0002D3F4 File Offset: 0x0002B5F4
		internal static string IncreaseIndent(string daxText)
		{
			if (daxText.Length == 0 || daxText.Length > 10000)
			{
				return daxText;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			int i = 0;
			int num = 0;
			while (i < daxText.Length)
			{
				if (daxText[i] == '"')
				{
					if (i + 1 < daxText.Length && daxText[i + 1] == '"')
					{
						i += 2;
					}
					else
					{
						flag = !flag;
						i++;
					}
				}
				else if (!flag && !flag2 && string.CompareOrdinal(daxText, i, "/* USER DAX BEGIN */", 0, "/* USER DAX BEGIN */".Length) == 0)
				{
					flag2 = true;
					i += "/* USER DAX BEGIN */".Length;
				}
				else if (!flag && flag2 && string.CompareOrdinal(daxText, i, "/* USER DAX END */", 0, "/* USER DAX END */".Length) == 0)
				{
					flag2 = false;
					i += "/* USER DAX END */".Length;
				}
				else if (!flag && !flag2 && daxText[i] == '\r' && i + 1 < daxText.Length && daxText[i + 1] == '\n')
				{
					i += 2;
					stringBuilder.Append(daxText.Substring(num, i - num));
					num = i;
					stringBuilder.Append('\t');
				}
				else
				{
					i++;
				}
			}
			if (num == 0)
			{
				return daxText;
			}
			if (num != i)
			{
				stringBuilder.Append(daxText.Substring(num));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000AA1 RID: 2721
		internal const char Tab = '\t';

		// Token: 0x04000AA2 RID: 2722
		internal const int MaxLineLength = 80;

		// Token: 0x04000AA3 RID: 2723
		internal const int MaxParensOnLine = 3;

		// Token: 0x04000AA4 RID: 2724
		private const int MaxTextLengthToIncreaseIndentation = 10000;

		// Token: 0x04000AA5 RID: 2725
		internal const string ExternalDaxBegin = "/* USER DAX BEGIN */";

		// Token: 0x04000AA6 RID: 2726
		internal const string ExternalDaxEnd = "/* USER DAX END */";
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000510 RID: 1296
	public static class StringDiffUtils
	{
		// Token: 0x06001CD0 RID: 7376 RVA: 0x00055CC0 File Offset: 0x00053EC0
		public static string ApplyAllStringDiffs(this IEnumerable<StringDiffUtils.StringDiff> textDiffs, string str)
		{
			textDiffs = textDiffs.OrderBy((StringDiffUtils.StringDiff x) => Record.Create<int, int>(x.StartLocation, x.EndLocation));
			StringBuilder stringBuilder = new StringBuilder(str.Length);
			StringDiffUtils.StringDiff stringDiff = null;
			int num;
			foreach (StringDiffUtils.StringDiff stringDiff2 in textDiffs)
			{
				num = ((stringDiff != null) ? stringDiff.EndLocation : 0);
				stringBuilder.Append(str, num, stringDiff2.StartLocation - num);
				stringBuilder.Append(stringDiff2.NewText);
				stringDiff = stringDiff2;
			}
			num = ((stringDiff != null) ? stringDiff.EndLocation : 0);
			stringBuilder.Append(str, num, str.Length - num);
			return stringBuilder.ToString();
		}

		// Token: 0x02000511 RID: 1297
		public class StringDiff
		{
			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00055D8C File Offset: 0x00053F8C
			public int StartLocation { get; }

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00055D94 File Offset: 0x00053F94
			public int EndLocation { get; }

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00055D9C File Offset: 0x00053F9C
			public string NewText { get; }

			// Token: 0x06001CD4 RID: 7380 RVA: 0x00055DA4 File Offset: 0x00053FA4
			public StringDiff(int startLocation, int endLocation, string newText)
			{
				this.StartLocation = startLocation;
				this.EndLocation = endLocation;
				this.NewText = newText;
			}

			// Token: 0x06001CD5 RID: 7381 RVA: 0x00055DC4 File Offset: 0x00053FC4
			public static StringDiffUtils.StringDiff ComputeStringDiff(string str1, string str2)
			{
				int length = str1.Length;
				int length2 = str2.Length;
				int num = Math.Min(length, length2);
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					if (str1[i] != str2[i])
					{
						num2 = i;
						break;
					}
				}
				int num3 = num - num2;
				for (int j = 1; j <= num - num2; j++)
				{
					if (str1[length - j] != str2[length2 - j])
					{
						num3 = j - 1;
						break;
					}
				}
				string text = str2.Substring(num2, length2 - num3 - num2);
				return new StringDiffUtils.StringDiff(num2, length - num3, text);
			}

			// Token: 0x06001CD6 RID: 7382 RVA: 0x00055E60 File Offset: 0x00054060
			public string ApplyStringDiff(string str)
			{
				string text = str.Substring(0, this.StartLocation);
				string text2 = str.Substring(this.EndLocation);
				return text + this.NewText + text2;
			}
		}
	}
}

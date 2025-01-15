using System;
using System.Text;
using Microsoft.PowerBI.Telemetry.PIIUtils;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200002B RID: 43
	public static class TagUtils
	{
		// Token: 0x06000102 RID: 258 RVA: 0x0000407C File Offset: 0x0000227C
		public static string ScrubAndOrObfuscateTaggedInfo(this string input)
		{
			return TagUtils.ScrubAndOrObfuscateTaggedInfo(TagUtils.ScrubAndOrObfuscateTaggedInfo(input, TagUtils.ScrubbingTagNames, (string str) => "[content removed]"), TagUtils.ObfuscatingTagNames, (string str) => Obfuscation.Obfuscate(str, false));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000040DC File Offset: 0x000022DC
		private static string ScrubAndOrObfuscateTaggedInfo(string input, string[] tagNames, Func<string, string> tagContentHandler)
		{
			if (string.IsNullOrEmpty(input))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			int i = 0;
			while (i < input.Length)
			{
				int num2;
				int num = input.IndexOfStartTag(tagNames, i, out num2);
				if (num == -1)
				{
					stringBuilder.Append(input.Substring(i));
					return stringBuilder.ToString();
				}
				string text = tagNames[num2];
				num += text.GetStartTagLength();
				stringBuilder.Append(input.Substring(i, num - i));
				int num3 = input.IndexOfEndTag(text, num);
				if (num3 == -1)
				{
					stringBuilder.Append(tagContentHandler(input.Substring(num, input.Length - num)));
					return stringBuilder.ToString();
				}
				i = num3 + text.GetEndTagLength();
				stringBuilder.Append(tagContentHandler(input.Substring(num, num3 - num)));
				stringBuilder.Append(input.Substring(num3, i - num3));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000A9 RID: 169
		internal const string ScrubbedTagTextReplacementString = "[content removed]";

		// Token: 0x040000AA RID: 170
		internal static readonly string[] ScrubbingTagNames = new string[] { "ccon", "ip" };

		// Token: 0x040000AB RID: 171
		internal static readonly string[] ObfuscatingTagNames = new string[] { "euii", "pi", "pii", "ii" };
	}
}

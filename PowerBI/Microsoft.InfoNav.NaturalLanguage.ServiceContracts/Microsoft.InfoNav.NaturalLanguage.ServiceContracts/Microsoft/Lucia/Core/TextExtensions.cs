using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core.Text;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000135 RID: 309
	public static class TextExtensions
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x0000AB70 File Offset: 0x00008D70
		public static bool TryPluralizeHeadNoun(this ITextAnalyzer textAnalyzer, TextAnalysisContext context, string value, out string result, IList<TextSegment> textSegments = null)
		{
			result = value;
			TextAnalysisResult textAnalysisResult;
			if (!textAnalyzer.TryAnalyzeText(context, value, out textAnalysisResult) || textAnalysisResult.Tokens.Count == 0)
			{
				return false;
			}
			if (textAnalyzer.IsNounPlural(textAnalysisResult.Tokens))
			{
				return false;
			}
			IList<string> list;
			if (!textAnalyzer.TryPluralizeAsNoun(textAnalysisResult.Tokens, out list))
			{
				return false;
			}
			int num;
			if (!textSegments.IsNullOrEmptyCollection<TextSegment>() && textAnalyzer.TryGetHeadNounIndex(textAnalysisResult.Tokens, out num))
			{
				int length = textAnalysisResult.Tokens[num].Length;
				int length2 = list[num].Length;
				int num2 = length2 - length;
				for (int i = 0; i < textSegments.Count; i++)
				{
					TextSegment textSegment = textSegments[i];
					if (textSegment.TokenIndex >= num)
					{
						if (textSegment.TokenIndex == num)
						{
							if (textSegment.Length != length)
							{
								break;
							}
							textSegment.Length = length2;
						}
						else
						{
							textSegment.StartIndex += num2;
						}
					}
				}
			}
			result = TextExtensions.ReplaceTokens(value, textAnalysisResult.Tokens, list);
			return true;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public static int IndexOf(this IList<IStemmerToken> tokens, string tokenValue, StringComparer comparer)
		{
			for (int i = 0; i < tokens.Count; i++)
			{
				IStemmerToken stemmerToken = tokens[i];
				if (comparer.Equals(stemmerToken.Value, tokenValue))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		public static string Replace(this IList<IStemmerToken> tokens, int index, string tokenReplaceValue, string separator)
		{
			StringBuilder stringBuilder = new StringBuilder(20);
			for (int i = 0; i < tokens.Count; i++)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder = stringBuilder.Append(separator);
				}
				if (i == index)
				{
					stringBuilder.Append(tokenReplaceValue);
				}
				else
				{
					stringBuilder.Append(tokens[i].Value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000AD04 File Offset: 0x00008F04
		private static string ReplaceTokens(string value, ReadOnlyCollection<IStemmerToken> tokens, IList<string> stems)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int count = tokens.Count;
			for (int i = 0; i < count; i++)
			{
				IStemmerToken stemmerToken = tokens[i];
				stringBuilder.Append(value.Substring(num, stemmerToken.StartIndex - num));
				stringBuilder.Append(stems[i]);
				num = stemmerToken.StartIndex + stemmerToken.Length;
			}
			return stringBuilder.Append(value.Substring(num)).ToString();
		}
	}
}

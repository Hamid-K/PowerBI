using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000DA RID: 218
	internal class JsonTokenizer
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x000243E0 File Offset: 0x000233E0
		static JsonTokenizer()
		{
			JsonTokenizer.tokenDictionary.Add(JsonTokenType.Number, JsonTokenizer.jsonNumberRegEx);
			JsonTokenizer.tokenDictionary.Add(JsonTokenType.Boolean, JsonTokenizer.jsonBooleanRegEx);
			JsonTokenizer.tokenDictionary.Add(JsonTokenType.Null, JsonTokenizer.jsonNullRegEx);
			JsonTokenizer.tokenDictionary.Add(JsonTokenType.ObjectOpen, JsonTokenizer.jsonOpenObjectRegEx);
			JsonTokenizer.tokenDictionary.Add(JsonTokenType.ObjectClose, JsonTokenizer.jsonCloseObjectRegEx);
			JsonTokenizer.tokenDictionary.Add(JsonTokenType.ArrayOpen, JsonTokenizer.jsonOpenArrayRegEx);
			JsonTokenizer.tokenDictionary.Add(JsonTokenType.ArrayClose, JsonTokenizer.jsonCloseArrayRegEx);
			JsonTokenizer.tokenDictionary.Add(JsonTokenType.Colon, JsonTokenizer.jsonColonRegEx);
			JsonTokenizer.tokenDictionary.Add(JsonTokenType.Comma, JsonTokenizer.jsonCommaRegEx);
			JsonTokenizer.tokenDictionary.Add(JsonTokenType.String, JsonTokenizer.jsonStringRegEx);
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (Regex regex in JsonTokenizer.tokenDictionary.Values)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append("|");
				}
				stringBuilder.Append("(");
				stringBuilder.Append(regex.ToString());
				stringBuilder.Append(")");
			}
			JsonTokenizer.fullTokenizerRegex = new Regex(stringBuilder.ToString(), 8);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000245DC File Offset: 0x000235DC
		internal JsonTokenizer(Stream input)
		{
			StreamReader streamReader = new StreamReader(input);
			string text = streamReader.ReadToEnd();
			this.currentMatch = JsonTokenizer.fullTokenizerRegex.Match(text);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002460E File Offset: 0x0002360E
		internal JsonTokenType Peek()
		{
			if (!this.nextTokenPeeked)
			{
				this.nextTokenType = this.NextToken(out this.nextToken);
				this.nextTokenPeeked = true;
			}
			return this.nextTokenType;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00024638 File Offset: 0x00023638
		internal JsonTokenType NextToken(out string token)
		{
			if (this.nextTokenPeeked)
			{
				token = this.nextToken;
				this.nextTokenPeeked = false;
				return this.nextTokenType;
			}
			token = this.currentMatch.Value;
			while (token.Trim().Length == 0)
			{
				this.AdvanceRegExMatch();
				token = this.currentMatch.Value;
			}
			foreach (KeyValuePair<JsonTokenType, Regex> keyValuePair in JsonTokenizer.tokenDictionary)
			{
				Match match = keyValuePair.Value.Match(token);
				if (match.Success && match.Index == 0 && match.Length == token.Length)
				{
					this.AdvanceRegExMatch();
					return keyValuePair.Key;
				}
			}
			throw new ServiceJsonDeserializationException();
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00024718 File Offset: 0x00023718
		private void AdvanceRegExMatch()
		{
			this.currentMatch = this.currentMatch.NextMatch();
		}

		// Token: 0x0400032D RID: 813
		private const string JsonStringRegExCode = "\"([^\\\\\"]|\\\\\"|\\\\\\\\|\\\\/|\\\\b|\\\\f|\\\\n|\\\\r|\\\\t|\\\\u[\\da-fA-F]{4})*\"";

		// Token: 0x0400032E RID: 814
		private const string JsonNumberRegExCode = "-?\\d+(.\\d+)?([eE][+-]?\\d+)?";

		// Token: 0x0400032F RID: 815
		private const string JsonBooleanRegExCode = "(true|false)";

		// Token: 0x04000330 RID: 816
		private const string JsonNullRegExCode = "null";

		// Token: 0x04000331 RID: 817
		private const string JsonOpenObjectRegExCode = "\\{";

		// Token: 0x04000332 RID: 818
		private const string JsonCloseObjectRegExCode = "\\}";

		// Token: 0x04000333 RID: 819
		private const string JsonOpenArrayRegExCode = "\\[";

		// Token: 0x04000334 RID: 820
		private const string JsonCloseArrayRegExCode = "\\]";

		// Token: 0x04000335 RID: 821
		private const string JsonColonRegExCode = "\\:";

		// Token: 0x04000336 RID: 822
		private const string JsonCommaRegExCode = "\\,";

		// Token: 0x04000337 RID: 823
		private static Regex jsonStringRegEx = new Regex("\"([^\\\\\"]|\\\\\"|\\\\\\\\|\\\\/|\\\\b|\\\\f|\\\\n|\\\\r|\\\\t|\\\\u[\\da-fA-F]{4})*\"", 8);

		// Token: 0x04000338 RID: 824
		private static Regex jsonNumberRegEx = new Regex("-?\\d+(.\\d+)?([eE][+-]?\\d+)?", 8);

		// Token: 0x04000339 RID: 825
		private static Regex jsonBooleanRegEx = new Regex("(true|false)", 8);

		// Token: 0x0400033A RID: 826
		private static Regex jsonNullRegEx = new Regex("null", 8);

		// Token: 0x0400033B RID: 827
		private static Regex jsonOpenObjectRegEx = new Regex("\\{", 8);

		// Token: 0x0400033C RID: 828
		private static Regex jsonCloseObjectRegEx = new Regex("\\}", 8);

		// Token: 0x0400033D RID: 829
		private static Regex jsonOpenArrayRegEx = new Regex("\\[", 8);

		// Token: 0x0400033E RID: 830
		private static Regex jsonCloseArrayRegEx = new Regex("\\]", 8);

		// Token: 0x0400033F RID: 831
		private static Regex jsonColonRegEx = new Regex("\\:", 8);

		// Token: 0x04000340 RID: 832
		private static Regex jsonCommaRegEx = new Regex("\\,", 8);

		// Token: 0x04000341 RID: 833
		private static Regex whitespaceRegEx = new Regex("\\s");

		// Token: 0x04000342 RID: 834
		private static Dictionary<JsonTokenType, Regex> tokenDictionary = new Dictionary<JsonTokenType, Regex>();

		// Token: 0x04000343 RID: 835
		private static Regex fullTokenizerRegex;

		// Token: 0x04000344 RID: 836
		private Match currentMatch;

		// Token: 0x04000345 RID: 837
		private JsonTokenType nextTokenType;

		// Token: 0x04000346 RID: 838
		private string nextToken;

		// Token: 0x04000347 RID: 839
		private bool nextTokenPeeked;
	}
}

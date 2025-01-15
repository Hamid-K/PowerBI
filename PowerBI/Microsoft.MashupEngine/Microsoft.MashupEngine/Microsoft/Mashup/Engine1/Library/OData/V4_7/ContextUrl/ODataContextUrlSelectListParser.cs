using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl
{
	// Token: 0x0200082A RID: 2090
	internal static class ODataContextUrlSelectListParser
	{
		// Token: 0x06003C22 RID: 15394 RVA: 0x000C33D8 File Offset: 0x000C15D8
		public static List<ContextUrlSelectListItem> Parse(string selectList, string contextUrl)
		{
			List<ContextUrlSelectListItem> list;
			using (ODataContextUrlSelectListParser.Parser parser = new ODataContextUrlSelectListParser.Parser(ODataContextUrlSelectListParser.Tokenize(selectList), contextUrl))
			{
				list = parser.Parse();
			}
			return list;
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x000C3418 File Offset: 0x000C1618
		private static IEnumerable<ODataContextUrlSelectListParser.Token> Tokenize(string selectList)
		{
			ODataContextUrlSelectListParser.Tokenizer tokenizer = new ODataContextUrlSelectListParser.Tokenizer(selectList);
			for (;;)
			{
				ODataContextUrlSelectListParser.Token token = tokenizer.Read();
				if (token.TokenType == ODataContextUrlSelectListParser.TokenType.Eof)
				{
					break;
				}
				yield return token;
			}
			yield break;
			yield break;
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x000C3428 File Offset: 0x000C1628
		private static bool IsIdentifierCharacter(char c)
		{
			switch (c)
			{
			case '(':
			case ')':
			case '*':
			case '+':
			case ',':
			case '.':
			case '/':
				return false;
			}
			return true;
		}

		// Token: 0x04001F60 RID: 8032
		private const char Open = '(';

		// Token: 0x04001F61 RID: 8033
		private const char Close = ')';

		// Token: 0x04001F62 RID: 8034
		private const char Star = '*';

		// Token: 0x04001F63 RID: 8035
		private const char Comma = ',';

		// Token: 0x04001F64 RID: 8036
		private const char Slash = '/';

		// Token: 0x04001F65 RID: 8037
		private const char Plus = '+';

		// Token: 0x04001F66 RID: 8038
		private const char Dot = '.';

		// Token: 0x0200082B RID: 2091
		private sealed class Parser : IDisposable
		{
			// Token: 0x06003C25 RID: 15397 RVA: 0x000C3458 File Offset: 0x000C1658
			public Parser(IEnumerable<ODataContextUrlSelectListParser.Token> tokens, string contextUrl)
			{
				this.contextUrl = contextUrl;
				this.tokens = tokens.GetEnumerator();
				this.MoveNext();
			}

			// Token: 0x170013F7 RID: 5111
			// (get) Token: 0x06003C26 RID: 15398 RVA: 0x000C347C File Offset: 0x000C167C
			private ODataContextUrlSelectListParser.TokenType CurrentTokenType
			{
				get
				{
					if (this.tokens != null)
					{
						ODataContextUrlSelectListParser.Token token = this.tokens.Current;
						return token.TokenType;
					}
					return ODataContextUrlSelectListParser.TokenType.Eof;
				}
			}

			// Token: 0x06003C27 RID: 15399 RVA: 0x000C34A6 File Offset: 0x000C16A6
			public List<ContextUrlSelectListItem> Parse()
			{
				if (this.CurrentTokenType == ODataContextUrlSelectListParser.TokenType.Eof)
				{
					return null;
				}
				List<ContextUrlSelectListItem> list = this.ParseSelectList();
				this.VerifyToken(ODataContextUrlSelectListParser.TokenType.Eof);
				return list;
			}

			// Token: 0x06003C28 RID: 15400 RVA: 0x000C34BF File Offset: 0x000C16BF
			public void Dispose()
			{
				if (this.tokens != null)
				{
					this.tokens.Dispose();
					this.tokens = null;
				}
			}

			// Token: 0x06003C29 RID: 15401 RVA: 0x000C34DB File Offset: 0x000C16DB
			private List<ContextUrlSelectListItem> ParseSelectList()
			{
				return this.ParseNonEmptyParenthesizedCommaSeparatedList<ContextUrlSelectListItem>(new Func<ContextUrlSelectListItem>(this.ParseSelectListItem));
			}

			// Token: 0x06003C2A RID: 15402 RVA: 0x000C34F0 File Offset: 0x000C16F0
			private ContextUrlSelectListItem ParseSelectListItem()
			{
				if (this.CurrentTokenType == ODataContextUrlSelectListParser.TokenType.Star)
				{
					this.MoveNext();
					return ContextUrlSelectListItem.Wildcard();
				}
				bool flag;
				bool flag2;
				string text = this.ParseQualifiedNameOrStar(out flag, out flag2);
				if (this.CurrentTokenType == ODataContextUrlSelectListParser.TokenType.Star && flag2)
				{
					this.MoveNext();
					return ContextUrlSelectListItem.QualifiedWildcard(text);
				}
				List<string> list = new List<string>();
				list.Add(text);
				while (this.CurrentTokenType == ODataContextUrlSelectListParser.TokenType.Slash)
				{
					this.MoveNext();
					list.Add(this.ParseQualifiedName(out flag));
				}
				bool flag3 = false;
				if (this.CurrentTokenType == ODataContextUrlSelectListParser.TokenType.Plus)
				{
					flag3 = true;
					this.MoveNext();
					this.VerifyToken(ODataContextUrlSelectListParser.TokenType.Open);
					this.VerifyFalse(flag);
				}
				if (this.CurrentTokenType != ODataContextUrlSelectListParser.TokenType.Open)
				{
					return ContextUrlSelectListItem.Projection(new EdmPathExpression(list));
				}
				if (flag)
				{
					IEnumerable<string> enumerable = this.ParseParameterNames();
					return ContextUrlSelectListItem.FunctionOverload(new EdmPathExpression(list), enumerable);
				}
				List<ContextUrlSelectListItem> list2 = this.ParseSelectList();
				return ContextUrlSelectListItem.Expansion(new EdmPathExpression(list), list2, flag3);
			}

			// Token: 0x06003C2B RID: 15403 RVA: 0x000C35CF File Offset: 0x000C17CF
			private IEnumerable<string> ParseParameterNames()
			{
				return this.ParseNonEmptyParenthesizedCommaSeparatedList<string>(new Func<string>(this.ParseIdentifier));
			}

			// Token: 0x06003C2C RID: 15404 RVA: 0x000C35E4 File Offset: 0x000C17E4
			private string ParseQualifiedName(out bool qualifiedName)
			{
				bool flag;
				return this.ParseQualifiedNameOrStar(out qualifiedName, out flag);
			}

			// Token: 0x06003C2D RID: 15405 RVA: 0x000C35FC File Offset: 0x000C17FC
			private string ParseQualifiedNameOrStar(out bool qualifiedName, out bool qualifiedStar)
			{
				qualifiedName = false;
				qualifiedStar = false;
				string text = this.ParseIdentifier();
				if (this.CurrentTokenType == ODataContextUrlSelectListParser.TokenType.Dot)
				{
					qualifiedName = true;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(text);
					for (;;)
					{
						this.MoveNext();
						if (this.CurrentTokenType == ODataContextUrlSelectListParser.TokenType.Star)
						{
							break;
						}
						stringBuilder.Append('.');
						stringBuilder.Append(this.ParseIdentifier());
						if (this.CurrentTokenType != ODataContextUrlSelectListParser.TokenType.Dot)
						{
							goto IL_005E;
						}
					}
					qualifiedName = false;
					qualifiedStar = true;
					IL_005E:
					return stringBuilder.ToString();
				}
				return text;
			}

			// Token: 0x06003C2E RID: 15406 RVA: 0x000C3670 File Offset: 0x000C1870
			private List<T> ParseNonEmptyParenthesizedCommaSeparatedList<T>(Func<T> parseItem)
			{
				this.VerifyToken(ODataContextUrlSelectListParser.TokenType.Open);
				this.MoveNext();
				List<T> list = new List<T>();
				do
				{
					list.Add(parseItem());
				}
				while (this.CurrentTokenType == ODataContextUrlSelectListParser.TokenType.Comma && this.MoveNext());
				this.VerifyToken(ODataContextUrlSelectListParser.TokenType.Close);
				this.MoveNext();
				return list;
			}

			// Token: 0x06003C2F RID: 15407 RVA: 0x000C36C0 File Offset: 0x000C18C0
			private string ParseIdentifier()
			{
				this.VerifyToken(ODataContextUrlSelectListParser.TokenType.Identifier);
				ODataContextUrlSelectListParser.Token token = this.tokens.Current;
				string text = token.Text;
				this.MoveNext();
				return text;
			}

			// Token: 0x06003C30 RID: 15408 RVA: 0x000C36EE File Offset: 0x000C18EE
			private bool MoveNext()
			{
				if (this.tokens == null)
				{
					return false;
				}
				bool flag = this.tokens.MoveNext();
				if (!flag)
				{
					this.Dispose();
				}
				return flag;
			}

			// Token: 0x06003C31 RID: 15409 RVA: 0x000C370E File Offset: 0x000C190E
			private void VerifyToken(ODataContextUrlSelectListParser.TokenType expected)
			{
				this.VerifyTrue(this.CurrentTokenType == expected);
			}

			// Token: 0x06003C32 RID: 15410 RVA: 0x000C371F File Offset: 0x000C191F
			private void VerifyTrue(bool condition)
			{
				if (!condition)
				{
					throw new ODataException(Strings.ODataJsonLightContextUriInvalid(this.contextUrl));
				}
			}

			// Token: 0x06003C33 RID: 15411 RVA: 0x000C373A File Offset: 0x000C193A
			private void VerifyFalse(bool condition)
			{
				this.VerifyTrue(!condition);
			}

			// Token: 0x04001F67 RID: 8039
			private readonly string contextUrl;

			// Token: 0x04001F68 RID: 8040
			private IEnumerator<ODataContextUrlSelectListParser.Token> tokens;
		}

		// Token: 0x0200082C RID: 2092
		private sealed class Tokenizer
		{
			// Token: 0x06003C34 RID: 15412 RVA: 0x000C3746 File Offset: 0x000C1946
			public Tokenizer(string text)
			{
				this.text = text;
				this.offset = 0;
				this.limit = text.Length;
			}

			// Token: 0x06003C35 RID: 15413 RVA: 0x000C3768 File Offset: 0x000C1968
			public ODataContextUrlSelectListParser.Token Read()
			{
				if (this.offset >= this.limit)
				{
					return ODataContextUrlSelectListParser.Token.Eof;
				}
				int num = this.offset;
				string text = this.text;
				int num2 = this.offset;
				this.offset = num2 + 1;
				if (ODataContextUrlSelectListParser.IsIdentifierCharacter(text[num2]))
				{
					while (this.offset < this.limit && ODataContextUrlSelectListParser.IsIdentifierCharacter(this.text[this.offset]))
					{
						this.offset++;
					}
				}
				return new ODataContextUrlSelectListParser.Token(this.text.Substring(num, this.offset - num));
			}

			// Token: 0x04001F69 RID: 8041
			private readonly string text;

			// Token: 0x04001F6A RID: 8042
			private readonly int limit;

			// Token: 0x04001F6B RID: 8043
			private int offset;
		}

		// Token: 0x0200082D RID: 2093
		private struct Token
		{
			// Token: 0x06003C36 RID: 15414 RVA: 0x000C3802 File Offset: 0x000C1A02
			public Token(string text)
			{
				this.text = text;
			}

			// Token: 0x170013F8 RID: 5112
			// (get) Token: 0x06003C37 RID: 15415 RVA: 0x000C380B File Offset: 0x000C1A0B
			public string Text
			{
				get
				{
					return this.text;
				}
			}

			// Token: 0x170013F9 RID: 5113
			// (get) Token: 0x06003C38 RID: 15416 RVA: 0x000C3814 File Offset: 0x000C1A14
			public ODataContextUrlSelectListParser.TokenType TokenType
			{
				get
				{
					if (this.text == null)
					{
						return ODataContextUrlSelectListParser.TokenType.Eof;
					}
					switch (this.text[0])
					{
					case '(':
						return ODataContextUrlSelectListParser.TokenType.Open;
					case ')':
						return ODataContextUrlSelectListParser.TokenType.Close;
					case '*':
						return ODataContextUrlSelectListParser.TokenType.Star;
					case '+':
						return ODataContextUrlSelectListParser.TokenType.Plus;
					case ',':
						return ODataContextUrlSelectListParser.TokenType.Comma;
					case '.':
						return ODataContextUrlSelectListParser.TokenType.Dot;
					case '/':
						return ODataContextUrlSelectListParser.TokenType.Slash;
					}
					return ODataContextUrlSelectListParser.TokenType.Identifier;
				}
			}

			// Token: 0x04001F6C RID: 8044
			public static readonly ODataContextUrlSelectListParser.Token Eof = new ODataContextUrlSelectListParser.Token(null);

			// Token: 0x04001F6D RID: 8045
			private readonly string text;
		}

		// Token: 0x0200082E RID: 2094
		private enum TokenType
		{
			// Token: 0x04001F6F RID: 8047
			Eof,
			// Token: 0x04001F70 RID: 8048
			Open,
			// Token: 0x04001F71 RID: 8049
			Close,
			// Token: 0x04001F72 RID: 8050
			Star,
			// Token: 0x04001F73 RID: 8051
			Comma,
			// Token: 0x04001F74 RID: 8052
			Slash,
			// Token: 0x04001F75 RID: 8053
			Plus,
			// Token: 0x04001F76 RID: 8054
			Dot,
			// Token: 0x04001F77 RID: 8055
			Identifier
		}
	}
}

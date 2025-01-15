using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.EntityClient.Internal
{
	// Token: 0x020005E3 RID: 1507
	internal class DbConnectionOptions
	{
		// Token: 0x06004999 RID: 18841 RVA: 0x00104F5D File Offset: 0x0010315D
		internal DbConnectionOptions()
		{
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x00104F70 File Offset: 0x00103170
		internal DbConnectionOptions(string connectionString, IList<string> validKeywords)
		{
			this._usersConnectionString = connectionString ?? "";
			if (0 < this._usersConnectionString.Length)
			{
				this.KeyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, validKeywords);
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x0600499B RID: 18843 RVA: 0x00104FC4 File Offset: 0x001031C4
		internal string UsersConnectionString
		{
			get
			{
				return this._usersConnectionString ?? string.Empty;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x0600499C RID: 18844 RVA: 0x00104FD5 File Offset: 0x001031D5
		internal bool IsEmpty
		{
			get
			{
				return this.KeyChain == null;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x0600499D RID: 18845 RVA: 0x00104FE0 File Offset: 0x001031E0
		internal Dictionary<string, string> Parsetable
		{
			get
			{
				return this._parsetable;
			}
		}

		// Token: 0x17000E90 RID: 3728
		internal virtual string this[string keyword]
		{
			get
			{
				string text;
				this._parsetable.TryGetValue(keyword, out text);
				return text;
			}
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x00105008 File Offset: 0x00103208
		private static string GetKeyName(StringBuilder buffer)
		{
			int num = buffer.Length;
			while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
			{
				num--;
			}
			return buffer.ToString(0, num).ToLowerInvariant();
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x00105044 File Offset: 0x00103244
		private static string GetKeyValue(StringBuilder buffer, bool trimWhitespace)
		{
			int num = buffer.Length;
			int i = 0;
			if (trimWhitespace)
			{
				while (i < num)
				{
					if (!char.IsWhiteSpace(buffer[i]))
					{
						break;
					}
					i++;
				}
				while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
				{
					num--;
				}
			}
			return buffer.ToString(i, num - i);
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x0010509C File Offset: 0x0010329C
		private static int GetKeyValuePair(string connectionString, int currentPosition, StringBuilder buffer, out string keyname, out string keyvalue)
		{
			int num = currentPosition;
			buffer.Length = 0;
			keyname = null;
			keyvalue = null;
			char c = '\0';
			DbConnectionOptions.ParserState parserState = DbConnectionOptions.ParserState.NothingYet;
			int length = connectionString.Length;
			while (currentPosition < length)
			{
				c = connectionString[currentPosition];
				switch (parserState)
				{
				case DbConnectionOptions.ParserState.NothingYet:
					if (';' != c && !char.IsWhiteSpace(c))
					{
						if (c == '\0')
						{
							parserState = DbConnectionOptions.ParserState.NullTermination;
						}
						else
						{
							if (char.IsControl(c))
							{
								throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
							}
							num = currentPosition;
							if ('=' != c)
							{
								parserState = DbConnectionOptions.ParserState.Key;
								goto IL_0257;
							}
							parserState = DbConnectionOptions.ParserState.KeyEqual;
						}
					}
					break;
				case DbConnectionOptions.ParserState.Key:
					if ('=' == c)
					{
						parserState = DbConnectionOptions.ParserState.KeyEqual;
					}
					else
					{
						if (!char.IsWhiteSpace(c) && char.IsControl(c))
						{
							throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
						}
						goto IL_0257;
					}
					break;
				case DbConnectionOptions.ParserState.KeyEqual:
					if ('=' == c)
					{
						parserState = DbConnectionOptions.ParserState.Key;
						goto IL_0257;
					}
					keyname = DbConnectionOptions.GetKeyName(buffer);
					if (string.IsNullOrEmpty(keyname))
					{
						throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
					}
					buffer.Length = 0;
					parserState = DbConnectionOptions.ParserState.KeyEnd;
					goto IL_0117;
				case DbConnectionOptions.ParserState.KeyEnd:
					goto IL_0117;
				case DbConnectionOptions.ParserState.UnquotedValue:
					if (char.IsWhiteSpace(c))
					{
						goto IL_0257;
					}
					if (char.IsControl(c))
					{
						goto IL_026B;
					}
					if (';' == c)
					{
						goto IL_026B;
					}
					goto IL_0257;
				case DbConnectionOptions.ParserState.DoubleQuoteValue:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
						}
						goto IL_0257;
					}
					break;
				case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_0257;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0200;
				case DbConnectionOptions.ParserState.SingleQuoteValue:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
						}
						goto IL_0257;
					}
					break;
				case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_0257;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0200;
				case DbConnectionOptions.ParserState.QuotedValueEnd:
					goto IL_0200;
				case DbConnectionOptions.ParserState.NullTermination:
					if (c != '\0' && !char.IsWhiteSpace(c))
					{
						throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(currentPosition));
					}
					break;
				default:
					throw new InvalidOperationException(Strings.ADP_InternalProviderError(1015));
				}
				IL_025F:
				currentPosition++;
				continue;
				IL_0117:
				if (char.IsWhiteSpace(c))
				{
					goto IL_025F;
				}
				if ('\'' == c)
				{
					parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
					goto IL_025F;
				}
				if ('"' == c)
				{
					parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
					goto IL_025F;
				}
				if (';' == c || c == '\0')
				{
					break;
				}
				if (char.IsControl(c))
				{
					throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
				}
				parserState = DbConnectionOptions.ParserState.UnquotedValue;
				goto IL_0257;
				IL_0200:
				if (char.IsWhiteSpace(c))
				{
					goto IL_025F;
				}
				if (';' == c)
				{
					break;
				}
				if (c == '\0')
				{
					parserState = DbConnectionOptions.ParserState.NullTermination;
					goto IL_025F;
				}
				throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
				IL_0257:
				buffer.Append(c);
				goto IL_025F;
			}
			IL_026B:
			switch (parserState)
			{
			case DbConnectionOptions.ParserState.NothingYet:
			case DbConnectionOptions.ParserState.KeyEnd:
			case DbConnectionOptions.ParserState.NullTermination:
				break;
			case DbConnectionOptions.ParserState.Key:
			case DbConnectionOptions.ParserState.DoubleQuoteValue:
			case DbConnectionOptions.ParserState.SingleQuoteValue:
				throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
			case DbConnectionOptions.ParserState.KeyEqual:
				keyname = DbConnectionOptions.GetKeyName(buffer);
				if (string.IsNullOrEmpty(keyname))
				{
					throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
				}
				break;
			case DbConnectionOptions.ParserState.UnquotedValue:
			{
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, true);
				char c2 = keyvalue[keyvalue.Length - 1];
				if ('\'' == c2 || '"' == c2)
				{
					throw new ArgumentException(Strings.ADP_ConnectionStringSyntax(num));
				}
				break;
			}
			case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
			case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
			case DbConnectionOptions.ParserState.QuotedValueEnd:
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
				break;
			default:
				throw new InvalidOperationException(Strings.ADP_InternalProviderError(1016));
			}
			if (';' == c && currentPosition < connectionString.Length)
			{
				currentPosition++;
			}
			return currentPosition;
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x001053F0 File Offset: 0x001035F0
		private static NameValuePair ParseInternal(IDictionary<string, string> parsetable, string connectionString, IList<string> validKeywords)
		{
			StringBuilder stringBuilder = new StringBuilder();
			NameValuePair nameValuePair = null;
			NameValuePair nameValuePair2 = null;
			int i = 0;
			int length = connectionString.Length;
			while (i < length)
			{
				int num = i;
				string text;
				string text2;
				i = DbConnectionOptions.GetKeyValuePair(connectionString, num, stringBuilder, out text, out text2);
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				if (!validKeywords.Contains(text))
				{
					throw new ArgumentException(Strings.ADP_KeywordNotSupported(text));
				}
				parsetable[text] = text2;
				if (nameValuePair != null)
				{
					nameValuePair = (nameValuePair.Next = new NameValuePair());
				}
				else
				{
					nameValuePair = (nameValuePair2 = new NameValuePair());
				}
			}
			return nameValuePair2;
		}

		// Token: 0x040019FB RID: 6651
		internal const string DataDirectory = "|datadirectory|";

		// Token: 0x040019FC RID: 6652
		private readonly string _usersConnectionString;

		// Token: 0x040019FD RID: 6653
		private readonly Dictionary<string, string> _parsetable = new Dictionary<string, string>();

		// Token: 0x040019FE RID: 6654
		internal readonly NameValuePair KeyChain;

		// Token: 0x02000C27 RID: 3111
		private enum ParserState
		{
			// Token: 0x04003023 RID: 12323
			NothingYet = 1,
			// Token: 0x04003024 RID: 12324
			Key,
			// Token: 0x04003025 RID: 12325
			KeyEqual,
			// Token: 0x04003026 RID: 12326
			KeyEnd,
			// Token: 0x04003027 RID: 12327
			UnquotedValue,
			// Token: 0x04003028 RID: 12328
			DoubleQuoteValue,
			// Token: 0x04003029 RID: 12329
			DoubleQuoteValueQuote,
			// Token: 0x0400302A RID: 12330
			SingleQuoteValue,
			// Token: 0x0400302B RID: 12331
			SingleQuoteValueQuote,
			// Token: 0x0400302C RID: 12332
			QuotedValueEnd,
			// Token: 0x0400302D RID: 12333
			NullTermination
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data.Common
{
	// Token: 0x0200017D RID: 381
	internal class DbConnectionOptions
	{
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x0007465F File Offset: 0x0007285F
		internal Dictionary<string, string> Parsetable
		{
			get
			{
				return this._parsetable;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00074667 File Offset: 0x00072867
		public bool IsEmpty
		{
			get
			{
				return this._keyChain == null;
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00074674 File Offset: 0x00072874
		public DbConnectionOptions(string connectionString, Dictionary<string, string> synonyms)
		{
			this._parsetable = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			this._usersConnectionString = ((connectionString != null) ? connectionString : "");
			if (0 < this._usersConnectionString.Length)
			{
				this._keyChain = DbConnectionOptions.ParseInternal(this._parsetable, this._usersConnectionString, true, synonyms, false);
				this._hasPasswordKeyword = this._parsetable.ContainsKey("Password") || this._parsetable.ContainsKey("pwd");
				this._hasUserIdKeyword = this._parsetable.ContainsKey("User ID") || this._parsetable.ContainsKey("uid");
			}
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00074728 File Offset: 0x00072928
		protected DbConnectionOptions(DbConnectionOptions connectionOptions)
		{
			this._usersConnectionString = connectionOptions._usersConnectionString;
			this._parsetable = connectionOptions._parsetable;
			this._keyChain = connectionOptions._keyChain;
			this._hasPasswordKeyword = connectionOptions._hasPasswordKeyword;
			this._hasUserIdKeyword = connectionOptions._hasUserIdKeyword;
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00074777 File Offset: 0x00072977
		internal bool TryGetParsetableValue(string key, out string value)
		{
			return this._parsetable.TryGetValue(key, out value);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00074788 File Offset: 0x00072988
		public bool ConvertValueToIntegratedSecurity()
		{
			string text;
			return this._parsetable.TryGetValue("Integrated Security", out text) && text != null && this.ConvertValueToIntegratedSecurityInternal(text);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x000747B8 File Offset: 0x000729B8
		internal bool ConvertValueToIntegratedSecurityInternal(string stringValue)
		{
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "sspi") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "no"))
			{
				return false;
			}
			string text = stringValue.Trim();
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "sspi") || DbConnectionOptions.CompareInsensitiveInvariant(text, "true") || DbConnectionOptions.CompareInsensitiveInvariant(text, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "false") || DbConnectionOptions.CompareInsensitiveInvariant(text, "no"))
			{
				return false;
			}
			throw ADP.InvalidConnectionOptionValue("Integrated Security");
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00074860 File Offset: 0x00072A60
		public int ConvertValueToInt32(string keyName, int defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text) || text == null)
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertToInt32Internal(keyName, text);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0007488C File Offset: 0x00072A8C
		internal static int ConvertToInt32Internal(string keyname, string stringValue)
		{
			int num;
			try
			{
				num = int.Parse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
			}
			catch (FormatException ex)
			{
				throw ADP.InvalidConnectionOptionValue(keyname, ex);
			}
			catch (OverflowException ex2)
			{
				throw ADP.InvalidConnectionOptionValue(keyname, ex2);
			}
			return num;
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x000748D8 File Offset: 0x00072AD8
		public string ConvertValueToString(string keyName, string defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text) || text == null)
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x000748FB File Offset: 0x00072AFB
		public bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00074909 File Offset: 0x00072B09
		protected internal virtual string Expand()
		{
			return this._usersConnectionString;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00074911 File Offset: 0x00072B11
		public string UsersConnectionString(bool hidePassword)
		{
			return this.UsersConnectionString(hidePassword, false);
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0007491B File Offset: 0x00072B1B
		internal string UsersConnectionStringForTrace()
		{
			return this.UsersConnectionString(true, true);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00074928 File Offset: 0x00072B28
		private string UsersConnectionString(bool hidePassword, bool forceHidePassword)
		{
			string usersConnectionString = this._usersConnectionString;
			if (this._hasPasswordKeyword && (forceHidePassword || (hidePassword && !this.HasPersistablePassword)))
			{
				this.ReplacePasswordPwd(out usersConnectionString, false);
			}
			return usersConnectionString ?? string.Empty;
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x00074966 File Offset: 0x00072B66
		internal bool HasPersistablePassword
		{
			get
			{
				return !this._hasPasswordKeyword || this.ConvertValueToBoolean("Persist Security Info", false);
			}
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00074980 File Offset: 0x00072B80
		public bool ConvertValueToBoolean(string keyName, bool defaultValue)
		{
			string text;
			if (!this._parsetable.TryGetValue(keyName, out text))
			{
				return defaultValue;
			}
			return DbConnectionOptions.ConvertValueToBooleanInternal(keyName, text);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000749A8 File Offset: 0x00072BA8
		internal static bool ConvertValueToBooleanInternal(string keyName, string stringValue)
		{
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "true") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "false") || DbConnectionOptions.CompareInsensitiveInvariant(stringValue, "no"))
			{
				return false;
			}
			string text = stringValue.Trim();
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "true") || DbConnectionOptions.CompareInsensitiveInvariant(text, "yes"))
			{
				return true;
			}
			if (DbConnectionOptions.CompareInsensitiveInvariant(text, "false") || DbConnectionOptions.CompareInsensitiveInvariant(text, "no"))
			{
				return false;
			}
			throw ADP.InvalidConnectionOptionValue(keyName);
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00074A32 File Offset: 0x00072C32
		private static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(strvalue, strconst) == 0;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00074A44 File Offset: 0x00072C44
		[Conditional("DEBUG")]
		private static void DebugTraceKeyValuePair(string keyname, string keyvalue, Dictionary<string, string> synonyms)
		{
			if (SqlClientEventSource.Log.IsAdvancedTraceOn())
			{
				string text = ((synonyms != null) ? synonyms[keyname] : keyname);
				if (!string.Equals("Password", text, StringComparison.InvariantCultureIgnoreCase) && !string.Equals("pwd", text, StringComparison.InvariantCultureIgnoreCase))
				{
					if (keyvalue != null)
					{
						SqlClientEventSource.Log.AdvancedTraceEvent<string, string>("<comm.DbConnectionOptions|INFO|ADV> KeyName='{0}', KeyValue='{1}'", keyname, keyvalue);
						return;
					}
					SqlClientEventSource.Log.AdvancedTraceEvent<string>("<comm.DbConnectionOptions|INFO|ADV> KeyName='{0}'", keyname);
				}
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00074AAC File Offset: 0x00072CAC
		private static string GetKeyName(StringBuilder buffer)
		{
			int num = buffer.Length;
			while (0 < num && char.IsWhiteSpace(buffer[num - 1]))
			{
				num--;
			}
			return buffer.ToString(0, num).ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00074AEC File Offset: 0x00072CEC
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

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00074B44 File Offset: 0x00072D44
		internal static int GetKeyValuePair(string connectionString, int currentPosition, StringBuilder buffer, bool useOdbcRules, out string keyname, out string keyvalue)
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
								throw ADP.ConnectionStringSyntax(num);
							}
							num = currentPosition;
							if ('=' != c)
							{
								parserState = DbConnectionOptions.ParserState.Key;
								goto IL_0248;
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
							throw ADP.ConnectionStringSyntax(num);
						}
						goto IL_0248;
					}
					break;
				case DbConnectionOptions.ParserState.KeyEqual:
					if (!useOdbcRules && '=' == c)
					{
						parserState = DbConnectionOptions.ParserState.Key;
						goto IL_0248;
					}
					keyname = DbConnectionOptions.GetKeyName(buffer);
					if (string.IsNullOrEmpty(keyname))
					{
						throw ADP.ConnectionStringSyntax(num);
					}
					buffer.Length = 0;
					parserState = DbConnectionOptions.ParserState.KeyEnd;
					goto IL_0107;
				case DbConnectionOptions.ParserState.KeyEnd:
					goto IL_0107;
				case DbConnectionOptions.ParserState.UnquotedValue:
					if (char.IsWhiteSpace(c))
					{
						goto IL_0248;
					}
					if (char.IsControl(c))
					{
						goto IL_025C;
					}
					if (';' == c)
					{
						goto IL_025C;
					}
					goto IL_0248;
				case DbConnectionOptions.ParserState.DoubleQuoteValue:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw ADP.ConnectionStringSyntax(num);
						}
						goto IL_0248;
					}
					break;
				case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_0248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0212;
				case DbConnectionOptions.ParserState.SingleQuoteValue:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValueQuote;
					}
					else
					{
						if (c == '\0')
						{
							throw ADP.ConnectionStringSyntax(num);
						}
						goto IL_0248;
					}
					break;
				case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_0248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0212;
				case DbConnectionOptions.ParserState.BraceQuoteValue:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValueQuote;
						goto IL_0248;
					}
					if (c == '\0')
					{
						throw ADP.ConnectionStringSyntax(num);
					}
					goto IL_0248;
				case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
					if ('}' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_0248;
					}
					keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
					parserState = DbConnectionOptions.ParserState.QuotedValueEnd;
					goto IL_0212;
				case DbConnectionOptions.ParserState.QuotedValueEnd:
					goto IL_0212;
				case DbConnectionOptions.ParserState.NullTermination:
					if (c != '\0' && !char.IsWhiteSpace(c))
					{
						throw ADP.ConnectionStringSyntax(currentPosition);
					}
					break;
				default:
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState1);
				}
				IL_0250:
				currentPosition++;
				continue;
				IL_0107:
				if (char.IsWhiteSpace(c))
				{
					goto IL_0250;
				}
				if (useOdbcRules)
				{
					if ('{' == c)
					{
						parserState = DbConnectionOptions.ParserState.BraceQuoteValue;
						goto IL_0248;
					}
				}
				else
				{
					if ('\'' == c)
					{
						parserState = DbConnectionOptions.ParserState.SingleQuoteValue;
						goto IL_0250;
					}
					if ('"' == c)
					{
						parserState = DbConnectionOptions.ParserState.DoubleQuoteValue;
						goto IL_0250;
					}
				}
				if (';' == c || c == '\0')
				{
					break;
				}
				if (char.IsControl(c))
				{
					throw ADP.ConnectionStringSyntax(num);
				}
				parserState = DbConnectionOptions.ParserState.UnquotedValue;
				goto IL_0248;
				IL_0212:
				if (char.IsWhiteSpace(c))
				{
					goto IL_0250;
				}
				if (';' == c)
				{
					break;
				}
				if (c == '\0')
				{
					parserState = DbConnectionOptions.ParserState.NullTermination;
					goto IL_0250;
				}
				throw ADP.ConnectionStringSyntax(num);
				IL_0248:
				buffer.Append(c);
				goto IL_0250;
			}
			IL_025C:
			switch (parserState)
			{
			case DbConnectionOptions.ParserState.NothingYet:
			case DbConnectionOptions.ParserState.KeyEnd:
			case DbConnectionOptions.ParserState.NullTermination:
				break;
			case DbConnectionOptions.ParserState.Key:
			case DbConnectionOptions.ParserState.DoubleQuoteValue:
			case DbConnectionOptions.ParserState.SingleQuoteValue:
			case DbConnectionOptions.ParserState.BraceQuoteValue:
				throw ADP.ConnectionStringSyntax(num);
			case DbConnectionOptions.ParserState.KeyEqual:
				keyname = DbConnectionOptions.GetKeyName(buffer);
				if (string.IsNullOrEmpty(keyname))
				{
					throw ADP.ConnectionStringSyntax(num);
				}
				break;
			case DbConnectionOptions.ParserState.UnquotedValue:
			{
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, true);
				char c2 = keyvalue[keyvalue.Length - 1];
				if (!useOdbcRules && ('\'' == c2 || '"' == c2))
				{
					throw ADP.ConnectionStringSyntax(num);
				}
				break;
			}
			case DbConnectionOptions.ParserState.DoubleQuoteValueQuote:
			case DbConnectionOptions.ParserState.SingleQuoteValueQuote:
			case DbConnectionOptions.ParserState.BraceQuoteValueQuote:
			case DbConnectionOptions.ParserState.QuotedValueEnd:
				keyvalue = DbConnectionOptions.GetKeyValue(buffer, false);
				break;
			default:
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidParserState2);
			}
			if (';' == c && currentPosition < connectionString.Length)
			{
				currentPosition++;
			}
			return currentPosition;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00074E68 File Offset: 0x00073068
		private static bool IsValueValidInternal(string keyvalue)
		{
			return keyvalue == null || -1 == keyvalue.IndexOf('\0');
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00074E79 File Offset: 0x00073079
		private static bool IsKeyNameValid(string keyname)
		{
			return keyname != null && (0 < keyname.Length && ';' != keyname[0] && !char.IsWhiteSpace(keyname[0])) && -1 == keyname.IndexOf('\0');
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00074EB0 File Offset: 0x000730B0
		private static NameValuePair ParseInternal(Dictionary<string, string> parsetable, string connectionString, bool buildChain, Dictionary<string, string> synonyms, bool firstKey)
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
				i = DbConnectionOptions.GetKeyValuePair(connectionString, num, stringBuilder, firstKey, out text, out text2);
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				string text4;
				string text3 = ((synonyms != null) ? (synonyms.TryGetValue(text, out text4) ? text4 : null) : text);
				if (!DbConnectionOptions.IsKeyNameValid(text3))
				{
					throw ADP.KeywordNotSupported(text);
				}
				if (!firstKey || !parsetable.ContainsKey(text3))
				{
					parsetable[text3] = text2;
				}
				if (nameValuePair != null)
				{
					nameValuePair = (nameValuePair.Next = new NameValuePair(text3, text2, i - num));
				}
				else if (buildChain)
				{
					nameValuePair = (nameValuePair2 = new NameValuePair(text3, text2, i - num));
				}
			}
			return nameValuePair2;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00074F70 File Offset: 0x00073170
		internal NameValuePair ReplacePasswordPwd(out string constr, bool fakePassword)
		{
			int num = 0;
			NameValuePair nameValuePair = null;
			NameValuePair nameValuePair2 = null;
			NameValuePair nameValuePair3 = null;
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			for (NameValuePair nameValuePair4 = this._keyChain; nameValuePair4 != null; nameValuePair4 = nameValuePair4.Next)
			{
				if (!string.Equals("Password", nameValuePair4.Name, StringComparison.InvariantCultureIgnoreCase) && !string.Equals("pwd", nameValuePair4.Name, StringComparison.InvariantCultureIgnoreCase))
				{
					stringBuilder.Append(this._usersConnectionString, num, nameValuePair4.Length);
					if (fakePassword)
					{
						nameValuePair3 = new NameValuePair(nameValuePair4.Name, nameValuePair4.Value, nameValuePair4.Length);
					}
				}
				else if (fakePassword)
				{
					stringBuilder.Append(nameValuePair4.Name).Append("=*;");
					nameValuePair3 = new NameValuePair(nameValuePair4.Name, "*", nameValuePair4.Name.Length + "=*;".Length);
				}
				if (fakePassword)
				{
					if (nameValuePair2 != null)
					{
						nameValuePair2 = (nameValuePair2.Next = nameValuePair3);
					}
					else
					{
						nameValuePair = (nameValuePair2 = nameValuePair3);
					}
				}
				num += nameValuePair4.Length;
			}
			constr = stringBuilder.ToString();
			return nameValuePair;
		}

		// Token: 0x17000A09 RID: 2569
		public string this[string keyword]
		{
			get
			{
				return this._parsetable[keyword];
			}
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000021D8 File Offset: 0x000003D8
		protected internal virtual PermissionSet CreatePermissionSet()
		{
			return null;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0007509F File Offset: 0x0007329F
		internal void DemandPermission()
		{
			if (this._permissionset == null)
			{
				this._permissionset = this.CreatePermissionSet();
			}
			this._permissionset.Demand();
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x000750C0 File Offset: 0x000732C0
		internal bool HasBlankPassword
		{
			get
			{
				if (this.ConvertValueToIntegratedSecurity())
				{
					return false;
				}
				if (this._parsetable.ContainsKey("Password"))
				{
					return ADP.IsEmpty(this._parsetable["Password"]);
				}
				if (this._parsetable.ContainsKey("pwd"))
				{
					return ADP.IsEmpty(this._parsetable["pwd"]);
				}
				return (this._parsetable.ContainsKey("User ID") && !ADP.IsEmpty(this._parsetable["User ID"])) || (this._parsetable.ContainsKey("uid") && !ADP.IsEmpty(this._parsetable["uid"]));
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x00075184 File Offset: 0x00073384
		internal string ExpandKeyword(string keyword, string replacementValue)
		{
			bool flag = false;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder(this._usersConnectionString.Length);
			for (NameValuePair nameValuePair = this._keyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
			{
				if (nameValuePair.Name == keyword && nameValuePair.Value == this[keyword])
				{
					DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, nameValuePair.Name, replacementValue, false);
					stringBuilder.Append(';');
					flag = true;
				}
				else
				{
					stringBuilder.Append(this._usersConnectionString, num, nameValuePair.Length);
				}
				num += nameValuePair.Length;
			}
			if (!flag)
			{
				DbConnectionOptions.AppendKeyValuePairBuilder(stringBuilder, keyword, replacementValue, false);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00075228 File Offset: 0x00073428
		internal static void AppendKeyValuePairBuilder(StringBuilder builder, string keyName, string keyValue, bool useOdbcRules = false)
		{
			ADP.CheckArgumentNull(builder, "builder");
			ADP.CheckArgumentLength(keyName, "keyName");
			if (keyName == null || !DbConnectionOptions.s_connectionStringValidKeyRegex.IsMatch(keyName))
			{
				throw ADP.InvalidKeyname(keyName);
			}
			if (keyValue != null && !DbConnectionOptions.IsValueValidInternal(keyValue))
			{
				throw ADP.InvalidValue(keyName);
			}
			if (0 < builder.Length && ';' != builder[builder.Length - 1])
			{
				builder.Append(";");
			}
			if (useOdbcRules)
			{
				builder.Append(keyName);
			}
			else
			{
				builder.Append(keyName.Replace("=", "=="));
			}
			builder.Append("=");
			if (keyValue != null)
			{
				if (useOdbcRules)
				{
					if (0 < keyValue.Length && ('{' == keyValue[0] || 0 <= keyValue.IndexOf(';') || string.Compare("Driver", keyName, StringComparison.OrdinalIgnoreCase) == 0) && !DbConnectionOptions.s_connectionStringQuoteOdbcValueRegex.IsMatch(keyValue))
					{
						builder.Append('{').Append(keyValue.Replace("}", "}}")).Append('}');
						return;
					}
					builder.Append(keyValue);
					return;
				}
				else
				{
					if (DbConnectionOptions.s_connectionStringQuoteValueRegex.IsMatch(keyValue))
					{
						builder.Append(keyValue);
						return;
					}
					if (-1 != keyValue.IndexOf('"') && -1 == keyValue.IndexOf('\''))
					{
						builder.Append('\'');
						builder.Append(keyValue);
						builder.Append('\'');
						return;
					}
					builder.Append('"');
					builder.Append(keyValue.Replace("\"", "\"\""));
					builder.Append('"');
				}
			}
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x000753B0 File Offset: 0x000735B0
		internal static string ExpandDataDirectory(string keyword, string value, ref string datadir)
		{
			string text = null;
			if (value != null && value.StartsWith("|datadirectory|", StringComparison.OrdinalIgnoreCase))
			{
				string text2 = datadir;
				if (text2 == null)
				{
					object data = AppDomain.CurrentDomain.GetData("DataDirectory");
					text2 = data as string;
					if (data != null && text2 == null)
					{
						throw ADP.InvalidDataDirectory();
					}
					if (ADP.IsEmpty(text2))
					{
						text2 = AppDomain.CurrentDomain.BaseDirectory;
					}
					if (text2 == null)
					{
						text2 = "";
					}
					datadir = text2;
				}
				int length = "|datadirectory|".Length;
				bool flag = 0 < text2.Length && text2[text2.Length - 1] == '\\';
				bool flag2 = length < value.Length && value[length] == '\\';
				if (!flag && !flag2)
				{
					text = text2 + "\\" + value.Substring(length);
				}
				else if (flag && flag2)
				{
					text = text2 + value.Substring(length + 1);
				}
				else
				{
					text = text2 + value.Substring(length);
				}
				if (!ADP.GetFullPath(text).StartsWith(text2, StringComparison.Ordinal))
				{
					throw ADP.InvalidConnectionOptionValue(keyword);
				}
			}
			return text;
		}

		// Token: 0x04000C03 RID: 3075
		private const string ConnectionStringValidKeyPattern = "^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$";

		// Token: 0x04000C04 RID: 3076
		private const string ConnectionStringValidValuePattern = "^[^\0]*$";

		// Token: 0x04000C05 RID: 3077
		private const string ConnectionStringQuoteValuePattern = "^[^\"'=;\\s\\p{Cc}]*$";

		// Token: 0x04000C06 RID: 3078
		private const string ConnectionStringQuoteOdbcValuePattern = "^\\{([^\\}\0]|\\}\\})*\\}$";

		// Token: 0x04000C07 RID: 3079
		internal const string DataDirectory = "|datadirectory|";

		// Token: 0x04000C08 RID: 3080
		private static readonly Regex s_connectionStringValidKeyRegex = new Regex("^(?![;\\s])[^\\p{Cc}]+(?<!\\s)$", RegexOptions.Compiled);

		// Token: 0x04000C09 RID: 3081
		private static readonly Regex s_connectionStringValidValueRegex = new Regex("^[^\0]*$", RegexOptions.Compiled);

		// Token: 0x04000C0A RID: 3082
		private static readonly Regex s_connectionStringQuoteValueRegex = new Regex("^[^\"'=;\\s\\p{Cc}]*$", RegexOptions.Compiled);

		// Token: 0x04000C0B RID: 3083
		private static readonly Regex s_connectionStringQuoteOdbcValueRegex = new Regex("^\\{([^\\}\0]|\\}\\})*\\}$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04000C0C RID: 3084
		internal readonly bool _hasPasswordKeyword;

		// Token: 0x04000C0D RID: 3085
		internal readonly bool _hasUserIdKeyword;

		// Token: 0x04000C0E RID: 3086
		internal readonly NameValuePair _keyChain;

		// Token: 0x04000C0F RID: 3087
		private readonly string _usersConnectionString;

		// Token: 0x04000C10 RID: 3088
		private readonly Dictionary<string, string> _parsetable;

		// Token: 0x04000C11 RID: 3089
		private PermissionSet _permissionset;

		// Token: 0x0200027A RID: 634
		private static class KEY
		{
			// Token: 0x04001790 RID: 6032
			internal const string Integrated_Security = "Integrated Security";

			// Token: 0x04001791 RID: 6033
			internal const string Password = "Password";

			// Token: 0x04001792 RID: 6034
			internal const string Persist_Security_Info = "Persist Security Info";

			// Token: 0x04001793 RID: 6035
			internal const string User_ID = "User ID";

			// Token: 0x04001794 RID: 6036
			internal const string Encrypt = "Encrypt";
		}

		// Token: 0x0200027B RID: 635
		private static class SYNONYM
		{
			// Token: 0x04001795 RID: 6037
			internal const string Pwd = "pwd";

			// Token: 0x04001796 RID: 6038
			internal const string UID = "uid";
		}

		// Token: 0x0200027C RID: 636
		private enum ParserState
		{
			// Token: 0x04001798 RID: 6040
			NothingYet = 1,
			// Token: 0x04001799 RID: 6041
			Key,
			// Token: 0x0400179A RID: 6042
			KeyEqual,
			// Token: 0x0400179B RID: 6043
			KeyEnd,
			// Token: 0x0400179C RID: 6044
			UnquotedValue,
			// Token: 0x0400179D RID: 6045
			DoubleQuoteValue,
			// Token: 0x0400179E RID: 6046
			DoubleQuoteValueQuote,
			// Token: 0x0400179F RID: 6047
			SingleQuoteValue,
			// Token: 0x040017A0 RID: 6048
			SingleQuoteValueQuote,
			// Token: 0x040017A1 RID: 6049
			BraceQuoteValue,
			// Token: 0x040017A2 RID: 6050
			BraceQuoteValueQuote,
			// Token: 0x040017A3 RID: 6051
			QuotedValueEnd,
			// Token: 0x040017A4 RID: 6052
			NullTermination
		}
	}
}

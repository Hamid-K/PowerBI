using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x0200018B RID: 395
	internal static class ScriptGeneratorSupporter
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x0015D126 File Offset: 0x0015B326
		public static int TokenTypeCount
		{
			get
			{
				return ScriptGeneratorSupporter._typeStrings.Length;
			}
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0015D130 File Offset: 0x0015B330
		public static string GetCasedString(string str, KeywordCasing casing)
		{
			switch (casing)
			{
			case KeywordCasing.Lowercase:
				return str.ToLowerInvariant();
			case KeywordCasing.Uppercase:
				return str.ToUpperInvariant();
			case KeywordCasing.PascalCase:
				return ScriptGeneratorSupporter.GetPascalCase(str);
			default:
				return string.Empty;
			}
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0015D170 File Offset: 0x0015B370
		public static string GetPascalCase(string str)
		{
			str = str.ToLowerInvariant();
			char c = char.ToUpperInvariant(str.get_Chars(0));
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(c);
			stringBuilder.Append(str.Substring(1));
			return stringBuilder.ToString();
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0015D1B4 File Offset: 0x0015B3B4
		public static string GetLowerCase(TSqlTokenType tokenType)
		{
			if (tokenType < TSqlTokenType.None || tokenType >= (TSqlTokenType)ScriptGeneratorSupporter._typeStrings.Length)
			{
				throw new ArgumentOutOfRangeException("tokenType");
			}
			string text = ScriptGeneratorSupporter._typeStrings[(int)tokenType];
			if (string.IsNullOrEmpty(text))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, SqlScriptGeneratorResource.TokenTypeDoesNotHaveStringRepresentation, new object[] { tokenType }));
			}
			return text;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0015D211 File Offset: 0x0015B411
		public static string GetUpperCase(TSqlTokenType tokenType)
		{
			return ScriptGeneratorSupporter.GetLowerCase(tokenType).ToUpperInvariant();
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0015D21E File Offset: 0x0015B41E
		public static string GetPascalCase(TSqlTokenType tokenType)
		{
			return ScriptGeneratorSupporter.GetPascalCase(ScriptGeneratorSupporter.GetLowerCase(tokenType));
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0015D22C File Offset: 0x0015B42C
		public static string GetTokenString(TSqlTokenType tokenType, KeywordCasing casing)
		{
			switch (casing)
			{
			case KeywordCasing.Lowercase:
				return ScriptGeneratorSupporter.GetLowerCase(tokenType);
			case KeywordCasing.Uppercase:
				return ScriptGeneratorSupporter.GetUpperCase(tokenType);
			case KeywordCasing.PascalCase:
				return ScriptGeneratorSupporter.GetPascalCase(tokenType);
			default:
				return string.Empty;
			}
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0015D26C File Offset: 0x0015B46C
		public static TSqlParserToken CreateWhitespaceToken(int count)
		{
			string text = new string(' ', count);
			return new TSqlParserToken(TSqlTokenType.WhiteSpace, text);
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0015D28D File Offset: 0x0015B48D
		internal static void CheckForNullReference(object variable, string variableName)
		{
			if (variableName == null)
			{
				throw new ArgumentNullException("variableName");
			}
			if (variable == null)
			{
				throw new ArgumentNullException(variableName);
			}
		}

		// Token: 0x0400199F RID: 6559
		internal const string EscapedRSquareParen = "]]";

		// Token: 0x040019A0 RID: 6560
		internal const string EscapedQuote = "\"\"";

		// Token: 0x040019A1 RID: 6561
		internal const string Quote = "\"";

		// Token: 0x040019A2 RID: 6562
		private static string[] _typeStrings = new string[]
		{
			"", "", "", "", "add", "all", "alter", "and", "any", "as",
			"asc", "authorization", "backup", "begin", "between", "break", "browse", "bulk", "by", "cascade",
			"case", "check", "checkpoint", "close", "clustered", "coalesce", "collate", "column", "commit", "compute",
			"constraint", "contains", "containstable", "continue", "convert", "create", "cross", "current", "current_date", "current_time",
			"current_timestamp", "current_user", "cursor", "database", "dbcc", "deallocate", "declare", "default", "delete", "deny",
			"desc", "distinct", "distributed", "double", "drop", "else", "end", "errlvl", "escape", "except",
			"exec", "execute", "exists", "exit", "fetch", "file", "fillfactor", "for", "foreign", "freetext",
			"freetexttable", "from", "full", "function", "goto", "grant", "group", "having", "holdlock", "identity",
			"identity_insert", "identitycol", "if", "in", "index", "inner", "insert", "intersect", "into", "is",
			"join", "key", "kill", "left", "like", "lineno", "national", "nocheck", "nonclustered", "not",
			"null", "nullif", "of", "off", "offsets", "on", "open", "opendatasource", "openquery", "openrowset",
			"openxml", "option", "or", "order", "outer", "over", "percent", "plan", "primary", "print",
			"proc", "procedure", "public", "raiserror", "read", "readtext", "reconfigure", "references", "replication", "restore",
			"restrict", "return", "revoke", "right", "rollback", "rowcount", "rowguidcol", "rule", "save", "schema",
			"select", "session_user", "set", "setuser", "shutdown", "some", "statistics", "system_user", "table", "textsize",
			"then", "to", "top", "tran", "transaction", "trigger", "truncate", "tsequal", "union", "unique",
			"update", "updatetext", "use", "user", "values", "varying", "view", "waitfor", "when", "where",
			"while", "with", "writetext", "disk", "precision", "external", "revert", "pivot", "unpivot", "tablesample",
			"dump", "load", "merge", "stoplist", "semantickeyphrasetable", "semanticsimilaritytable", "semanticsimilaritydetailstable", "try_convert", "!", "%",
			"&", "(", ")", "{", "}", "*", "*=", "+", ",", "-",
			".", "/", ":", "::", ";", "<", "=", "=*", ">", "^",
			"|", "~", "+=", "-=", "/=", "%=", "&=", "|=", "^=", "go",
			"", "", "", "", "", "", "", "", "", "",
			"", "", "", "", "", "", "", "", "", ""
		};
	}
}

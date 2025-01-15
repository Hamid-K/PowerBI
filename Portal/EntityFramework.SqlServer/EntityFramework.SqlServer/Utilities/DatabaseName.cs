using System;
using System.Data.Entity.SqlServer.Resources;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000028 RID: 40
	internal class DatabaseName
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x00010164 File Offset: 0x0000E364
		public static DatabaseName Parse(string name)
		{
			Match match = DatabaseName._partExtractor.Match(name.Trim());
			if (!match.Success)
			{
				throw Error.InvalidDatabaseName(name);
			}
			string text = match.Groups["part1"].Value.Replace("]]", "]");
			string text2 = match.Groups["part2"].Value.Replace("]]", "]");
			if (string.IsNullOrWhiteSpace(text2))
			{
				return new DatabaseName(text);
			}
			return new DatabaseName(text2, text);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000101EF File Offset: 0x0000E3EF
		public DatabaseName(string name)
			: this(name, null)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000101F9 File Offset: 0x0000E3F9
		public DatabaseName(string name, string schema)
		{
			this._name = name;
			this._schema = ((!string.IsNullOrEmpty(schema)) ? schema : null);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001021A File Offset: 0x0000E41A
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00010222 File Offset: 0x0000E422
		public string Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001022C File Offset: 0x0000E42C
		public override string ToString()
		{
			string text = DatabaseName.Escape(this._name);
			if (this._schema != null)
			{
				text = DatabaseName.Escape(this._schema) + "." + text;
			}
			return text;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00010265 File Offset: 0x0000E465
		private static string Escape(string name)
		{
			if (name.IndexOfAny(new char[] { ']', '[', '.' }) == -1)
			{
				return name;
			}
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000102A2 File Offset: 0x0000E4A2
		public bool Equals(DatabaseName other)
		{
			return other != null && (this == other || (string.Equals(other._name, this._name, StringComparison.Ordinal) && string.Equals(other._schema, this._schema, StringComparison.Ordinal)));
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000102D7 File Offset: 0x0000E4D7
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj.GetType() == typeof(DatabaseName) && this.Equals((DatabaseName)obj)));
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00010309 File Offset: 0x0000E509
		public override int GetHashCode()
		{
			return (this._name.GetHashCode() * 397) ^ ((this._schema != null) ? this._schema.GetHashCode() : 0);
		}

		// Token: 0x040000CE RID: 206
		private const string NamePartRegex = "(?:(?:\\[(?<part{0}>(?:(?:\\]\\])|[^\\]])+)\\])|(?<part{0}>[^\\.\\[\\]]+))";

		// Token: 0x040000CF RID: 207
		private static readonly Regex _partExtractor = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}(?:\\.{1})?$", new object[]
		{
			string.Format(CultureInfo.InvariantCulture, "(?:(?:\\[(?<part{0}>(?:(?:\\]\\])|[^\\]])+)\\])|(?<part{0}>[^\\.\\[\\]]+))", new object[] { 1 }),
			string.Format(CultureInfo.InvariantCulture, "(?:(?:\\[(?<part{0}>(?:(?:\\]\\])|[^\\]])+)\\])|(?<part{0}>[^\\.\\[\\]]+))", new object[] { 2 })
		}), RegexOptions.Compiled);

		// Token: 0x040000D0 RID: 208
		private readonly string _name;

		// Token: 0x040000D1 RID: 209
		private readonly string _schema;
	}
}

using System;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200008A RID: 138
	internal class DatabaseName
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x000105F0 File Offset: 0x0000E7F0
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

		// Token: 0x0600046A RID: 1130 RVA: 0x0001067B File Offset: 0x0000E87B
		public DatabaseName(string name)
			: this(name, null)
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00010685 File Offset: 0x0000E885
		public DatabaseName(string name, string schema)
		{
			this._name = name;
			this._schema = ((!string.IsNullOrEmpty(schema)) ? schema : null);
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000106A6 File Offset: 0x0000E8A6
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000106AE File Offset: 0x0000E8AE
		public string Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000106B8 File Offset: 0x0000E8B8
		public override string ToString()
		{
			string text = DatabaseName.Escape(this._name);
			if (this._schema != null)
			{
				text = DatabaseName.Escape(this._schema) + "." + text;
			}
			return text;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000106F1 File Offset: 0x0000E8F1
		private static string Escape(string name)
		{
			if (name.IndexOfAny(new char[] { ']', '[', '.' }) == -1)
			{
				return name;
			}
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001072E File Offset: 0x0000E92E
		public bool Equals(DatabaseName other)
		{
			return other != null && (this == other || (string.Equals(other._name, this._name, StringComparison.Ordinal) && string.Equals(other._schema, this._schema, StringComparison.Ordinal)));
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00010763 File Offset: 0x0000E963
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj.GetType() == typeof(DatabaseName) && this.Equals((DatabaseName)obj)));
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00010795 File Offset: 0x0000E995
		public override int GetHashCode()
		{
			return (this._name.GetHashCode() * 397) ^ ((this._schema != null) ? this._schema.GetHashCode() : 0);
		}

		// Token: 0x04000114 RID: 276
		private const string NamePartRegex = "(?:(?:\\[(?<part{0}>(?:(?:\\]\\])|[^\\]])+)\\])|(?<part{0}>[^\\.\\[\\]]+))";

		// Token: 0x04000115 RID: 277
		private static readonly Regex _partExtractor = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}(?:\\.{1})?$", new object[]
		{
			string.Format(CultureInfo.InvariantCulture, "(?:(?:\\[(?<part{0}>(?:(?:\\]\\])|[^\\]])+)\\])|(?<part{0}>[^\\.\\[\\]]+))", new object[] { 1 }),
			string.Format(CultureInfo.InvariantCulture, "(?:(?:\\[(?<part{0}>(?:(?:\\]\\])|[^\\]])+)\\])|(?<part{0}>[^\\.\\[\\]]+))", new object[] { 2 })
		}), RegexOptions.Compiled);

		// Token: 0x04000116 RID: 278
		private readonly string _name;

		// Token: 0x04000117 RID: 279
		private readonly string _schema;
	}
}

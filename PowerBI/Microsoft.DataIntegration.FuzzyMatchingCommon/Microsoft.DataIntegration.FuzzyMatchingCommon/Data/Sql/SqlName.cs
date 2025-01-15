using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public class SqlName : IXmlSerializable
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00015D83 File Offset: 0x00013F83
		public SqlName()
		{
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00015D96 File Offset: 0x00013F96
		public SqlName(string sqlIdentifier)
		{
			if (sqlIdentifier != null && sqlIdentifier.Length > 0 && !this.TryParse(sqlIdentifier))
			{
				throw new ArgumentException(string.Format("SqlNameIsNotValid({0})", new object[0]), sqlIdentifier);
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00015DD8 File Offset: 0x00013FD8
		public SqlName(SqlName name)
		{
			for (int i = 0; i < name.Parts.Count; i++)
			{
				this.Parts[i] = name.Parts[i];
			}
			this.m_QualifiedName = name.m_QualifiedName;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00015E30 File Offset: 0x00014030
		public SqlName(string schemaName, string tableName)
			: this(null, null, schemaName, tableName)
		{
			this.FormQualifiedName();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00015E44 File Offset: 0x00014044
		public SqlName(string part4, string part3, string part2, string part1)
		{
			if (part4 != null && part4.Length > 0)
			{
				this.Parts[3] = part4;
			}
			if (part3 != null && part3.Length > 0)
			{
				this.Parts[2] = part3;
			}
			if (part2 != null && part2.Length > 0)
			{
				this.Parts[1] = part2;
			}
			if (part1 != null && part1.Length > 0)
			{
				this.Parts[0] = part1;
			}
			this.FormQualifiedName();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00015ECF File Offset: 0x000140CF
		public static implicit operator SqlName(string identifier)
		{
			return new SqlName(identifier);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00015ED7 File Offset: 0x000140D7
		public static bool IsNullOrEmpty(SqlName name)
		{
			return name == null || name.IsEmpty;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00015EE4 File Offset: 0x000140E4
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00015EED File Offset: 0x000140ED
		public string Database
		{
			get
			{
				return this.GetPart(SqlName.Part.Database);
			}
			set
			{
				this.SetPart(SqlName.Part.Database, value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00015EF7 File Offset: 0x000140F7
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00015F00 File Offset: 0x00014100
		public string Object
		{
			get
			{
				return this.GetPart(SqlName.Part.Table);
			}
			set
			{
				this.SetPart(SqlName.Part.Table, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00015F0A File Offset: 0x0001410A
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00015F13 File Offset: 0x00014113
		public string Schema
		{
			get
			{
				return this.GetPart(SqlName.Part.Schema);
			}
			set
			{
				this.SetPart(SqlName.Part.Schema, value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00015F1D File Offset: 0x0001411D
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00015F26 File Offset: 0x00014126
		public string Server
		{
			get
			{
				return this.GetPart(SqlName.Part.Server);
			}
			set
			{
				this.SetPart(SqlName.Part.Server, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00015F30 File Offset: 0x00014130
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00015F39 File Offset: 0x00014139
		public string Table
		{
			get
			{
				return this.GetPart(SqlName.Part.Table);
			}
			set
			{
				this.SetPart(SqlName.Part.Table, value);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00015F44 File Offset: 0x00014144
		protected void InitParts()
		{
			this.m_parts = new List<string>(4);
			this.m_parts.Add(string.Empty);
			this.m_parts.Add(string.Empty);
			this.m_parts.Add(string.Empty);
			this.m_parts.Add(string.Empty);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00015FA0 File Offset: 0x000141A0
		public void Clear()
		{
			this.Parts[0] = string.Empty;
			this.Parts[1] = string.Empty;
			this.Parts[2] = string.Empty;
			this.Parts[3] = string.Empty;
			this.QualifiedName = string.Empty;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00015FFC File Offset: 0x000141FC
		public override bool Equals(object o)
		{
			return o.GetType() == base.GetType() && ((SqlName)o).QualifiedName.ToLowerInvariant().Equals(this.QualifiedName.ToLowerInvariant());
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001602E File Offset: 0x0001422E
		public override int GetHashCode()
		{
			return this.QualifiedName.GetHashCode();
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0001603B File Offset: 0x0001423B
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00016043 File Offset: 0x00014243
		public string QualifiedName
		{
			get
			{
				return this.m_QualifiedName;
			}
			protected set
			{
				if (value != null)
				{
					this.m_QualifiedName = value;
					return;
				}
				this.m_QualifiedName = string.Empty;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0001605B File Offset: 0x0001425B
		protected List<string> Parts
		{
			get
			{
				if (this.m_parts == null)
				{
					this.InitParts();
				}
				return this.m_parts;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00016071 File Offset: 0x00014271
		public string GetPart(SqlName.Part p)
		{
			return this.Parts[p - SqlName.Part.Table];
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00016081 File Offset: 0x00014281
		public virtual void SetPart(SqlName.Part p, string str)
		{
			this.Parts[p - SqlName.Part.Table] = str;
			this.FormQualifiedName();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00016098 File Offset: 0x00014298
		public static string CreateStringLiteral(string identifierName)
		{
			return string.Format("N'{0}'", identifierName.Replace("'", "''"));
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000160B4 File Offset: 0x000142B4
		public string GetExplicitSchema()
		{
			if (this.Schema.Length <= 0)
			{
				return "dbo";
			}
			return this.Schema;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000160D0 File Offset: 0x000142D0
		public string GetDelimitedPart(SqlName.Part p)
		{
			return SqlName.DelimitElement(this.GetPart(p));
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000160E0 File Offset: 0x000142E0
		public bool IsEmpty
		{
			get
			{
				return this.Table == string.Empty && this.Schema == string.Empty && this.Database == string.Empty && this.Server == string.Empty;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00016135 File Offset: 0x00014335
		public bool IsValid
		{
			get
			{
				return this.GetPart(SqlName.Part.Table).Length > 0;
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00016146 File Offset: 0x00014346
		public override string ToString()
		{
			return this.QualifiedName;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00016150 File Offset: 0x00014350
		protected void FormQualifiedName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			if (this.Server.Length > 0)
			{
				stringBuilder.Append(SqlName.DelimitElement(this.Server));
				stringBuilder.Append(".");
				num = 4;
			}
			if (this.Database.Length == 0 && this.Table.StartsWith("#"))
			{
				this.Database = "tempdb";
			}
			if (num > 3 || this.Database.Length > 0)
			{
				stringBuilder.Append(SqlName.DelimitElement(this.Database));
				stringBuilder.Append(".");
				num = Math.Max(num, 3);
			}
			if (num > 2 || this.Schema.Length > 0)
			{
				stringBuilder.Append(SqlName.DelimitElement(this.Schema));
				stringBuilder.Append(".");
				num = Math.Max(num, 2);
			}
			if (num > 1 || this.Table.Length > 0)
			{
				stringBuilder.Append(SqlName.DelimitElement(this.Table));
				num = Math.Max(num, 1);
			}
			this.QualifiedName = stringBuilder.ToString();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00016265 File Offset: 0x00014465
		public static bool TryParse(string sqlIdentifier, out SqlName sqlName)
		{
			sqlName = new SqlName();
			if (!sqlName.TryParse(sqlIdentifier))
			{
				sqlName.Clear();
				return false;
			}
			return true;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00016284 File Offset: 0x00014484
		public virtual bool TryParse(string strIdentifier)
		{
			this.Clear();
			if (strIdentifier == null || strIdentifier.Length == 0)
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			int length = strIdentifier.Length;
			int[] array = new int[] { -1, -1, -1, -1 };
			int[] array2 = new int[] { -1, -1, -1, -1 };
			SqlName.EscapeAction[] array3 = new SqlName.EscapeAction[4];
			SqlName.ParseQuoteState parseQuoteState = SqlName.ParseQuoteState.Start;
			foreach (char c in strIdentifier)
			{
				CharEnumerator enumerator;
				if (c <= '"')
				{
					if (c == '\0')
					{
						return false;
					}
					if (c != '"')
					{
						goto IL_0186;
					}
					switch (parseQuoteState)
					{
					case SqlName.ParseQuoteState.Start:
						parseQuoteState = SqlName.ParseQuoteState.OpenQuote;
						array[num] = num2 + 1;
						goto IL_01C0;
					case SqlName.ParseQuoteState.OpenQuote:
						if (num2 < length - 1 && strIdentifier.get_Chars(num2 + 1) == '"')
						{
							array3[num] = SqlName.EscapeAction.Quote;
							enumerator.MoveNext();
							num2++;
							goto IL_01C0;
						}
						parseQuoteState = SqlName.ParseQuoteState.CloseQuote;
						array2[num] = num2 - 1;
						goto IL_01C0;
					case SqlName.ParseQuoteState.OpenBracket:
						goto IL_01C0;
					case SqlName.ParseQuoteState.CloseQuote:
						return false;
					}
					return false;
				}
				else if (c != '.')
				{
					if (c != '[')
					{
						if (c != ']')
						{
							goto IL_0186;
						}
						switch (parseQuoteState)
						{
						case SqlName.ParseQuoteState.OpenQuote:
							goto IL_01C0;
						case SqlName.ParseQuoteState.OpenBracket:
							if (num2 < length - 1 && strIdentifier.get_Chars(num2 + 1) == ']')
							{
								array3[num] = SqlName.EscapeAction.Bracket;
								enumerator.MoveNext();
								num2++;
								goto IL_01C0;
							}
							parseQuoteState = SqlName.ParseQuoteState.CloseBracket;
							array2[num] = num2 - 1;
							goto IL_01C0;
						case SqlName.ParseQuoteState.CloseBracket:
							return false;
						}
						return false;
					}
					else if (parseQuoteState != SqlName.ParseQuoteState.Start)
					{
						if (parseQuoteState - SqlName.ParseQuoteState.OpenQuote > 1)
						{
							return false;
						}
					}
					else
					{
						parseQuoteState = SqlName.ParseQuoteState.OpenBracket;
						array[num] = num2 + 1;
					}
				}
				else if (parseQuoteState - SqlName.ParseQuoteState.OpenQuote > 1)
				{
					if (++num >= 4)
					{
						return false;
					}
					parseQuoteState = SqlName.ParseQuoteState.Start;
				}
				IL_01C0:
				num2++;
				continue;
				IL_0186:
				array2[num] = num2;
				if (char.IsWhiteSpace(enumerator.Current))
				{
					if (parseQuoteState != SqlName.ParseQuoteState.Start && parseQuoteState != SqlName.ParseQuoteState.OpenQuote && parseQuoteState != SqlName.ParseQuoteState.OpenBracket)
					{
						return false;
					}
					goto IL_01C0;
				}
				else
				{
					if (parseQuoteState == SqlName.ParseQuoteState.CloseQuote || parseQuoteState == SqlName.ParseQuoteState.CloseBracket)
					{
						return false;
					}
					if (parseQuoteState == SqlName.ParseQuoteState.Start)
					{
						array[num] = num2;
						parseQuoteState = SqlName.ParseQuoteState.NoOpen;
						goto IL_01C0;
					}
					goto IL_01C0;
				}
			}
			if (parseQuoteState == SqlName.ParseQuoteState.OpenQuote || parseQuoteState == SqlName.ParseQuoteState.OpenBracket || parseQuoteState == SqlName.ParseQuoteState.Start)
			{
				return false;
			}
			if (++num > 4)
			{
				throw new Exception("Unexpected number of parts encountered when parsing '" + strIdentifier + "'");
			}
			for (int i = 0; i < num; i++)
			{
				int num3 = num - i - 1;
				if (array[i] > array2[i])
				{
					this.Clear();
					return false;
				}
				if (array[i] >= 0 && array2[i] >= 0)
				{
					this.Parts[num3] = this.GetCanonicalLiteral(strIdentifier.Substring(array[i], array2[i] - array[i] + 1), array3[i]);
				}
				if (this.Parts[num3].Length == 0)
				{
					this.Clear();
					return false;
				}
			}
			this.FormQualifiedName();
			return true;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001651A File Offset: 0x0001471A
		protected string GetCanonicalLiteral(string s, SqlName.EscapeAction ea)
		{
			switch (ea)
			{
			case SqlName.EscapeAction.None:
				return s;
			case SqlName.EscapeAction.Quote:
				return this.RemoveEscapeChar(s, '"');
			case SqlName.EscapeAction.Bracket:
				return this.RemoveEscapeChar(s, ']');
			default:
				return string.Empty;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001654C File Offset: 0x0001474C
		public static string DelimitElement(string element)
		{
			if (element == null || element.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder("[", 2 * element.Length);
			CharEnumerator enumerator = element.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == ']')
				{
					stringBuilder.Append(']');
				}
				stringBuilder.Append(enumerator.Current);
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000165C0 File Offset: 0x000147C0
		public static string UndelimitElement(string strElement)
		{
			if (strElement.Length >= 2 && ((strElement.get_Chars(0) == '[' && strElement.get_Chars(strElement.Length - 1) == ']') || (strElement.get_Chars(0) == '"' && strElement.get_Chars(strElement.Length - 1) == '"')))
			{
				StringBuilder stringBuilder = new StringBuilder(strElement.Length);
				CharEnumerator enumerator = strElement.GetEnumerator();
				enumerator.MoveNext();
				char c = enumerator.Current;
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == '"' && c == '"')
					{
						return strElement;
					}
					if (enumerator.Current == ']' && c == '[')
					{
						if (!enumerator.MoveNext())
						{
							return stringBuilder.ToString();
						}
						if (enumerator.Current != ']')
						{
							return strElement;
						}
					}
					stringBuilder.Append(enumerator.Current);
				}
				return stringBuilder.ToString();
			}
			return strElement;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00016694 File Offset: 0x00014894
		protected string RemoveEscapeChar(string s, char c)
		{
			if (s.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c2 in s)
			{
				stringBuilder.Append(c2);
				CharEnumerator enumerator;
				if (enumerator.Current == c)
				{
					enumerator.MoveNext();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000166E9 File Offset: 0x000148E9
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000166EC File Offset: 0x000148EC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader.ReadToFollowing("SqlName") && reader.MoveToAttribute("Name"))
			{
				this.TryParse(reader.Value);
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00016715 File Offset: 0x00014915
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("SqlName");
			writer.WriteAttributeString("Name", this.QualifiedName);
			writer.WriteEndElement();
		}

		// Token: 0x04000086 RID: 134
		public static readonly SqlName Empty = new EmptySqlName();

		// Token: 0x04000087 RID: 135
		private List<string> m_parts;

		// Token: 0x04000088 RID: 136
		private string m_QualifiedName = string.Empty;

		// Token: 0x020000E1 RID: 225
		public enum Part
		{
			// Token: 0x04000236 RID: 566
			Server = 4,
			// Token: 0x04000237 RID: 567
			Database = 3,
			// Token: 0x04000238 RID: 568
			Schema = 2,
			// Token: 0x04000239 RID: 569
			Table = 1,
			// Token: 0x0400023A RID: 570
			Object = 1
		}

		// Token: 0x020000E2 RID: 226
		protected enum ParseQuoteState
		{
			// Token: 0x0400023C RID: 572
			Start,
			// Token: 0x0400023D RID: 573
			NoOpen,
			// Token: 0x0400023E RID: 574
			OpenQuote,
			// Token: 0x0400023F RID: 575
			OpenBracket,
			// Token: 0x04000240 RID: 576
			CloseQuote,
			// Token: 0x04000241 RID: 577
			CloseBracket
		}

		// Token: 0x020000E3 RID: 227
		protected enum EscapeAction
		{
			// Token: 0x04000243 RID: 579
			None,
			// Token: 0x04000244 RID: 580
			Quote,
			// Token: 0x04000245 RID: 581
			Bracket
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011F7 RID: 4599
	internal sealed class ScriptWriter
	{
		// Token: 0x06007937 RID: 31031 RVA: 0x001A34CC File Offset: 0x001A16CC
		public ScriptWriter(TextWriter writer, SqlSettings settings)
		{
			this.settings = settings;
			this.isNewLine = true;
			this.writer = writer;
			this.parameters = new List<DynamicParameter>();
		}

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x06007938 RID: 31032 RVA: 0x001A34F4 File Offset: 0x001A16F4
		public IList<DynamicParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x06007939 RID: 31033 RVA: 0x001A34FC File Offset: 0x001A16FC
		public SqlSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x0600793A RID: 31034 RVA: 0x001A3504 File Offset: 0x001A1704
		public void Indent()
		{
			this.indent++;
		}

		// Token: 0x0600793B RID: 31035 RVA: 0x001A3514 File Offset: 0x001A1714
		public void Unindent()
		{
			this.indent--;
		}

		// Token: 0x0600793C RID: 31036 RVA: 0x001A3524 File Offset: 0x001A1724
		public void WriteByte(byte item)
		{
			this.Write(item.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600793D RID: 31037 RVA: 0x001A3538 File Offset: 0x001A1738
		public void WriteShort(short item)
		{
			this.Write(item.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600793E RID: 31038 RVA: 0x001A354C File Offset: 0x001A174C
		public void WriteInt(int item)
		{
			this.Write(item.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600793F RID: 31039 RVA: 0x001A3560 File Offset: 0x001A1760
		public void WriteLong(long item)
		{
			this.Write(item.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06007940 RID: 31040 RVA: 0x001A3574 File Offset: 0x001A1774
		public void Write(ConstantSqlString text)
		{
			this.Write(text.String);
		}

		// Token: 0x06007941 RID: 31041 RVA: 0x001A3583 File Offset: 0x001A1783
		public void Write(VerbatimString text)
		{
			this.Write(text.String);
		}

		// Token: 0x06007942 RID: 31042 RVA: 0x001A3591 File Offset: 0x001A1791
		public void WriteLine(ConstantSqlString text)
		{
			this.Write(text);
			this.WriteLine();
		}

		// Token: 0x06007943 RID: 31043 RVA: 0x001A35A0 File Offset: 0x001A17A0
		private void Write(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (this.isNewLine)
			{
				for (int i = 0; i < this.indent; i++)
				{
					this.writer.Write(SqlLanguageStrings.TabSqlString);
				}
				this.isNewLine = false;
			}
			this.writer.Write("{0}", text);
		}

		// Token: 0x06007944 RID: 31044 RVA: 0x001A35FC File Offset: 0x001A17FC
		public bool WriteCommaIfNeeded(bool commaNeeded)
		{
			if (commaNeeded)
			{
				this.Write(",");
				this.WriteSpace();
			}
			else
			{
				commaNeeded = true;
			}
			return commaNeeded;
		}

		// Token: 0x06007945 RID: 31045 RVA: 0x001A3618 File Offset: 0x001A1818
		public void WriteLine()
		{
			this.writer.WriteLine();
			this.isNewLine = true;
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x001A362C File Offset: 0x001A182C
		public bool WriteLineCommaIfNeeded(bool commaNeeded)
		{
			if (commaNeeded)
			{
				this.Write(",");
				this.WriteLine();
			}
			else
			{
				commaNeeded = true;
			}
			return commaNeeded;
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x001A3648 File Offset: 0x001A1848
		private void WriteLineIfNeeded()
		{
			if (!this.isNewLine)
			{
				this.WriteLine();
			}
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x001A3658 File Offset: 0x001A1858
		public void WriteSpace()
		{
			this.Write(" ");
		}

		// Token: 0x06007949 RID: 31049 RVA: 0x001A3665 File Offset: 0x001A1865
		public void WriteSpaceAfter(ConstantSqlString text)
		{
			this.Write(text);
			this.WriteSpace();
		}

		// Token: 0x0600794A RID: 31050 RVA: 0x001A3674 File Offset: 0x001A1874
		public void WriteSpaceBefore(ConstantSqlString text)
		{
			this.WriteSpace();
			this.Write(text);
		}

		// Token: 0x0600794B RID: 31051 RVA: 0x001A3683 File Offset: 0x001A1883
		public void WriteSpaceBeforeAndAfter(ConstantSqlString text)
		{
			this.WriteSpace();
			this.Write(text);
			this.WriteSpace();
		}

		// Token: 0x0600794C RID: 31052 RVA: 0x001A3698 File Offset: 0x001A1898
		public void WriteSubexpression(int expressionPrecedence, SqlExpression subexpression)
		{
			if (expressionPrecedence > subexpression.Precedence)
			{
				subexpression.WriteCreateScript(this);
				return;
			}
			if (subexpression is SqlQueryExpression)
			{
				this.WriteLineIfNeeded();
				this.Write("(");
				this.WriteLine();
				this.Indent();
				subexpression.WriteCreateScript(this);
				this.WriteLineIfNeeded();
				this.Unindent();
				this.Write(")");
				return;
			}
			this.Write("(");
			subexpression.WriteCreateScript(this);
			this.Write(")");
		}

		// Token: 0x0600794D RID: 31053 RVA: 0x001A3717 File Offset: 0x001A1917
		public void WriteIdentifier(Alias name)
		{
			this.Write(this.settings.QuoteIdentifier(name.Name));
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x001A3735 File Offset: 0x001A1935
		public void WriteParameter(Alias name)
		{
			if (!ScriptWriter.generatedParameterStringPattern.IsMatch(name.Name))
			{
				throw new InvalidOperationException();
			}
			this.Write(name.Name);
		}

		// Token: 0x0600794F RID: 31055 RVA: 0x001A375B File Offset: 0x001A195B
		public void WriteVariable(Alias name)
		{
			this.Write(name.Name);
		}

		// Token: 0x06007950 RID: 31056 RVA: 0x001A3769 File Offset: 0x001A1969
		public void WriteFromAlias(Alias name)
		{
			if (this.settings.RequiresAsForFromAlias)
			{
				this.WriteSpaceBeforeAndAfter(SqlLanguageStrings.AsSqlString);
			}
			else
			{
				this.WriteSpace();
			}
			this.WriteIdentifier(name);
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x001A3794 File Offset: 0x001A1994
		public void WriteIdentifier(Alias qualifier, Alias name)
		{
			this.Write(this.settings.QuoteIdentifier(qualifier.Name));
			this.Write(".");
			this.Write(this.settings.QuoteIdentifier(name.Name));
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x001A37E4 File Offset: 0x001A19E4
		public void WriteLiteral(ConstantType type, string literal)
		{
			switch (type)
			{
			case ConstantType.AnsiString:
				ScriptWriter.LiteralWriter.WriteAnsiString(this, literal);
				return;
			case ConstantType.UnicodeString:
				ScriptWriter.LiteralWriter.WriteNationalCharacterString(this, literal);
				return;
			case ConstantType.Binary:
				ScriptWriter.LiteralWriter.WriteBinary(this, literal);
				return;
			case ConstantType.Boolean:
				if (literal == "true")
				{
					this.Write(SqlLanguageStrings.TrueSqlString);
					return;
				}
				this.Write(SqlLanguageStrings.FalseSqlString);
				return;
			case ConstantType.DateTime:
				ScriptWriter.LiteralWriter.WriteDateTime(this, literal);
				return;
			case ConstantType.Time:
				ScriptWriter.LiteralWriter.WriteTime(this, literal);
				return;
			case ConstantType.DateTimeOffset:
				ScriptWriter.LiteralWriter.WriteDateTimeOffset(this, literal);
				return;
			case ConstantType.Integer:
				ScriptWriter.LiteralWriter.WriteInteger(this, literal);
				return;
			case ConstantType.Decimal:
				ScriptWriter.LiteralWriter.WriteDecimal(this, literal);
				return;
			case ConstantType.Float:
				ScriptWriter.LiteralWriter.WriteFloat(this, literal);
				return;
			case ConstantType.Interval:
				ScriptWriter.LiteralWriter.WriteInterval(this, literal);
				return;
			case ConstantType.Null:
				this.Write(SqlLanguageStrings.NullSqlString);
				return;
			case ConstantType.DoubleQuotesString:
				ScriptWriter.LiteralWriter.WriteDoubleQuotesString(this, literal);
				return;
			case ConstantType.Enum:
				this.Write(literal);
				return;
			case ConstantType.Date:
				ScriptWriter.LiteralWriter.WriteDate(this, literal);
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06007953 RID: 31059 RVA: 0x001A38E0 File Offset: 0x001A1AE0
		public void WriteLiteralNullForSelectItem()
		{
			this.Write(this.settings.SelectItemNull);
		}

		// Token: 0x06007954 RID: 31060 RVA: 0x001A38F3 File Offset: 0x001A1AF3
		public void WriteDynamicParameter(DynamicParameter parameter)
		{
			this.parameters.Add(parameter);
			this.Write(SqlLanguageStrings.QuestionMarkSqlString);
		}

		// Token: 0x06007955 RID: 31061 RVA: 0x001A390C File Offset: 0x001A1B0C
		public void AddDynamicParameter(DynamicParameter parameter)
		{
			this.parameters.Add(parameter);
		}

		// Token: 0x06007956 RID: 31062 RVA: 0x001A391A File Offset: 0x001A1B1A
		public static bool IsAnsiString(string literal)
		{
			return ScriptWriter.LiteralWriter.IsAnsiString(literal);
		}

		// Token: 0x040041F0 RID: 16880
		private int indent;

		// Token: 0x040041F1 RID: 16881
		private bool isNewLine;

		// Token: 0x040041F2 RID: 16882
		private readonly SqlSettings settings;

		// Token: 0x040041F3 RID: 16883
		private readonly TextWriter writer;

		// Token: 0x040041F4 RID: 16884
		private readonly List<DynamicParameter> parameters;

		// Token: 0x040041F5 RID: 16885
		private static readonly Regex generatedParameterStringPattern = new Regex("^:[a-z,A-Z,_][a-z,A-Z,0-9,_]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x020011F8 RID: 4600
		private static class LiteralWriter
		{
			// Token: 0x06007958 RID: 31064 RVA: 0x001A3938 File Offset: 0x001A1B38
			public static bool IsAnsiString(string literal)
			{
				return ScriptWriter.LiteralWriter.ansiStringPattern.IsMatch(literal);
			}

			// Token: 0x06007959 RID: 31065 RVA: 0x001A3945 File Offset: 0x001A1B45
			public static void WriteAnsiString(ScriptWriter writer, string literal)
			{
				if (!ScriptWriter.LiteralWriter.IsAnsiString(literal))
				{
					throw new InvalidOperationException();
				}
				writer.Write(writer.Settings.QuoteAnsiStringLiteral(literal));
			}

			// Token: 0x0600795A RID: 31066 RVA: 0x001A396C File Offset: 0x001A1B6C
			public static void WriteDoubleQuotesString(ScriptWriter writer, string literal)
			{
				writer.Write(ScriptWriter.LiteralWriter.doubleQuotesStringPrefix);
				writer.Write(literal);
				writer.Write(ScriptWriter.LiteralWriter.doubleQuotesStringSuffix);
			}

			// Token: 0x0600795B RID: 31067 RVA: 0x001A398C File Offset: 0x001A1B8C
			public static void WriteBinary(ScriptWriter writer, string literal)
			{
				if (!ScriptWriter.LiteralWriter.binaryPattern.IsMatch(literal))
				{
					throw new InvalidOperationException();
				}
				writer.Write(writer.settings.BinaryPrefix);
				writer.Write(literal.ToLowerInvariant());
				writer.Write(writer.settings.BinarySuffix);
			}

			// Token: 0x0600795C RID: 31068 RVA: 0x001A39DA File Offset: 0x001A1BDA
			public static void WriteDate(ScriptWriter writer, string literal)
			{
				if (!ScriptWriter.LiteralWriter.datePattern.IsMatch(literal))
				{
					throw new InvalidOperationException();
				}
				writer.Write(writer.settings.DatePrefix);
				writer.Write(literal);
				writer.Write(writer.settings.DateSuffix);
			}

			// Token: 0x0600795D RID: 31069 RVA: 0x001A3A18 File Offset: 0x001A1C18
			public static void WriteDateTime(ScriptWriter writer, string literal)
			{
				if (!ScriptWriter.LiteralWriter.dateTimePattern.IsMatch(literal))
				{
					throw new InvalidOperationException();
				}
				writer.Write(writer.settings.DateTimePrefix);
				writer.Write(literal);
				writer.Write(writer.settings.DateTimeSuffix);
			}

			// Token: 0x0600795E RID: 31070 RVA: 0x001A3A56 File Offset: 0x001A1C56
			public static void WriteDateTimeOffset(ScriptWriter writer, string literal)
			{
				if (!ScriptWriter.LiteralWriter.dateTimeOffsetPattern.IsMatch(literal))
				{
					throw new InvalidOperationException();
				}
				writer.Write(writer.settings.DateTimeOffsetPrefix);
				writer.Write(literal);
				writer.Write(writer.settings.DateTimeOffsetSuffix);
			}

			// Token: 0x0600795F RID: 31071 RVA: 0x001A3A94 File Offset: 0x001A1C94
			public static void WriteDecimal(ScriptWriter writer, string literal)
			{
				if (!ScriptWriter.LiteralWriter.decimalPattern.IsMatch(literal))
				{
					throw new InvalidOperationException();
				}
				if (literal[0] == '-')
				{
					writer.WriteSpace();
				}
				writer.Write(literal);
				if (!literal.Contains("."))
				{
					writer.Write(".");
				}
			}

			// Token: 0x06007960 RID: 31072 RVA: 0x001A3AE4 File Offset: 0x001A1CE4
			public static void WriteFloat(ScriptWriter writer, string literal)
			{
				if (!ScriptWriter.LiteralWriter.floatPattern.IsMatch(literal))
				{
					throw new InvalidOperationException();
				}
				writer.Write(literal.ToUpperInvariant());
			}

			// Token: 0x06007961 RID: 31073 RVA: 0x001A3B05 File Offset: 0x001A1D05
			public static void WriteInterval(ScriptWriter writer, string literal)
			{
				if (!ScriptWriter.LiteralWriter.intervalPattern.IsMatch(literal))
				{
					throw new InvalidOperationException();
				}
				writer.Write(writer.settings.IntervalPrefix);
				writer.Write(literal);
				writer.Write(writer.settings.IntervalSuffix);
			}

			// Token: 0x06007962 RID: 31074 RVA: 0x001A3B43 File Offset: 0x001A1D43
			public static void WriteInteger(ScriptWriter writer, string literal)
			{
				if (!ScriptWriter.LiteralWriter.integerPattern.IsMatch(literal))
				{
					throw new InvalidOperationException();
				}
				writer.Write(literal);
			}

			// Token: 0x06007963 RID: 31075 RVA: 0x001A3B5F File Offset: 0x001A1D5F
			public static void WriteTime(ScriptWriter writer, string literal)
			{
				if (!ScriptWriter.LiteralWriter.timePattern.IsMatch(literal))
				{
					throw new InvalidOperationException();
				}
				writer.Write(writer.settings.TimePrefix);
				writer.Write(literal);
				writer.Write(writer.settings.TimeSuffix);
			}

			// Token: 0x06007964 RID: 31076 RVA: 0x001A3B9D File Offset: 0x001A1D9D
			public static void WriteNationalCharacterString(ScriptWriter writer, string literal)
			{
				if (literal != null)
				{
					writer.Write(writer.settings.QuoteNationalStringLiteral(literal));
				}
			}

			// Token: 0x040041F6 RID: 16886
			private static readonly Regex ansiStringPattern = new Regex("^\\p{IsBasicLatin}*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x040041F7 RID: 16887
			private static readonly Regex binaryPattern = new Regex("^[0-9,a-f,A-F]*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x040041F8 RID: 16888
			private static readonly Regex datePattern = new Regex("^([0-9]{4})(-([0-9]{2})(-([0-9]{2})))$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x040041F9 RID: 16889
			private static readonly Regex dateTimePattern = new Regex("^([0-9]{4})(-([0-9]{2})(-([0-9]{2})( ([0-9]{2}):([0-9]{2})(:([0-9]{2})(\\.([0-9]+))?)?)?)?)?$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x040041FA RID: 16890
			private static readonly Regex dateTimeOffsetPattern = new Regex("^([0-9]{4})(-([0-9]{2})(-([0-9]{2})( ([0-9]{2}):([0-9]{2})(:([0-9]{2})(\\.([0-9]+))?)?(Z|(([-+])([0-9]{2}):([0-9]{2})))?)?)?)?$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x040041FB RID: 16891
			private static readonly Regex decimalPattern = new Regex("^(\\+|-)?[0-9]{1,38}(\\.[0-9]{0,38})?$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x040041FC RID: 16892
			private static readonly Regex floatPattern = new Regex("^(\\+|-)?[0-9]+(\\.[0-9]+)?(e|E)(\\+|-)?[0-9]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x040041FD RID: 16893
			private static readonly Regex integerPattern = new Regex("^(\\+|-)?[0-9]{1,19}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x040041FE RID: 16894
			private static readonly Regex timePattern = new Regex("([0-9]{2}):([0-9]{2})(:([0-9]{2})(\\.([0-9]+))?)?", RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x040041FF RID: 16895
			private static readonly Regex intervalPattern = new Regex("[0-9]+ ([0-9]{2})(:([0-9]{2})(\\.([0-9]+))?)?", RegexOptions.Compiled | RegexOptions.CultureInvariant);

			// Token: 0x04004200 RID: 16896
			private static readonly string doubleQuotesStringPrefix = "\"";

			// Token: 0x04004201 RID: 16897
			private static readonly string doubleQuotesStringSuffix = "\"";
		}
	}
}

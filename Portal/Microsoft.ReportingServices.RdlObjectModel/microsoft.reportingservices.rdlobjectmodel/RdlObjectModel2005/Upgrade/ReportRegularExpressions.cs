using System;
using System.Text.RegularExpressions;

namespace Microsoft.ReportingServices.RdlObjectModel2005.Upgrade
{
	// Token: 0x02000056 RID: 86
	internal sealed class ReportRegularExpressions
	{
		// Token: 0x06000385 RID: 901 RVA: 0x00014770 File Offset: 0x00012970
		private ReportRegularExpressions()
		{
			RegexOptions regexOptions = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline;
			this.NonConstant = new Regex("^\\s*=", regexOptions);
			string text = Regex.Escape("-+()#,:&*/\\^<=>");
			string text2 = Regex.Escape("!");
			string text3 = Regex.Escape(".");
			string text4 = "[" + text2 + text3 + "]";
			string text5 = "(^|[" + text + "\\s])";
			string text6 = string.Concat(new string[] { "($|[", text, text2, text3, "\\s])" });
			this.Whitespace = new Regex("\\s+", regexOptions);
			this.FieldDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Fields)" + text6, regexOptions);
			this.ReportItemsDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>ReportItems)" + text6, regexOptions);
			this.ParametersDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Parameters)" + text6, regexOptions);
			this.PageGlobalsDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|", text5, "(?<detected>(Globals", text4, "PageNumber)|(Globals", text4, "TotalPages))", text6 }), regexOptions);
			this.AggregatesDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Aggregates)" + text6, regexOptions);
			this.UserDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>User)" + text6, regexOptions);
			this.DataSetsDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>DataSets)" + text6, regexOptions);
			this.DataSourcesDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>DataSources)" + text6, regexOptions);
			this.VariablesDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>Variables)" + text6, regexOptions);
			this.MeDotValueDetection = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + "(?<detected>(?:Me.)?Value)" + text6, regexOptions);
			this.MeDotValueExpression = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|", text5, "(?<medotvalue>(Me", text3, ")?Value)*", text6 }), regexOptions);
			string text7 = Regex.Escape(":");
			string text8 = Regex.Escape("#");
			string text9 = string.Concat(new string[] { "(", text8, "[^", text8, "]*", text8, ")" });
			string text10 = Regex.Escape(":=");
			this.LineTerminatorDetection = new Regex("(?<detected>(\\u000D\\u000A)|([\\u000D\\u000A\\u2028\\u2029]))", regexOptions);
			this.IllegalCharacterDetection = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|", text9, "|", text10, "|(?<detected>", text7, ")" }), regexOptions);
			string text11 = "[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Pc}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Pc}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Cf}]*";
			string text12 = string.Concat(new string[] { "ReportItems", text2, "(?<reportitemname>", text11, ")" });
			string text13 = string.Concat(new string[] { "Fields", text2, "(?<fieldname>", text11, ")" });
			string text14 = string.Concat(new string[] { "Parameters", text2, "(?<parametername>", text11, ")" });
			string text15 = string.Concat(new string[] { "DataSets", text2, "(?<datasetname>", text11, ")" });
			string text16 = string.Concat(new string[] { "DataSources", text2, "(?<datasourcename>", text11, ")" });
			string text17 = string.Concat(new string[] { "Variables", text2, "(?<variablename>", text11, ")" });
			string text18 = string.Concat(new string[]
			{
				"(?<detected>(ReportItems(",
				text3,
				"Item)?",
				Regex.Escape("("),
				"[ \t]*",
				Regex.Escape("\""),
				"(?<reportitemname>",
				text11,
				")",
				Regex.Escape("\""),
				"[ \t]*",
				Regex.Escape(")"),
				"))"
			});
			this.SimpleDynamicReportItemReference = new Regex(text5 + text18, regexOptions);
			this.SimpleDynamicVariableReference = new Regex(string.Concat(new string[]
			{
				text5,
				"(?<detected>(Variables(",
				text3,
				"Item)?",
				Regex.Escape("("),
				"[ \t]*",
				Regex.Escape("\""),
				"(?<variablename>",
				text11,
				")",
				Regex.Escape("\""),
				"[ \t]*",
				Regex.Escape(")"),
				"))"
			}), regexOptions);
			this.SimpleDynamicFieldReference = new Regex(string.Concat(new string[]
			{
				text5,
				"(?<detected>(Fields(",
				text3,
				"Item)?",
				Regex.Escape("("),
				"[ \t]*",
				Regex.Escape("\""),
				"(?<fieldname>",
				text11,
				")",
				Regex.Escape("\""),
				"[ \t]*",
				Regex.Escape(")"),
				"))"
			}), regexOptions);
			this.DynamicFieldReference = new Regex(string.Concat(new string[]
			{
				"(\"((\"\")|[^\"])*\")|",
				text5,
				"(?<detected>(Fields(",
				text3,
				"Item)?",
				Regex.Escape("("),
				"))"
			}), regexOptions);
			this.DynamicFieldPropertyReference = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text13 + Regex.Escape("("), regexOptions);
			this.StaticFieldPropertyReference = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|", text5, text13, text3, "(?<propertyname>", text11, ")" }), regexOptions);
			this.FieldOnly = new Regex("^\\s*" + text13 + text3 + "Value\\s*$", regexOptions);
			this.RewrittenCommandText = new Regex("^\\s*" + text15 + text3 + "RewrittenCommandText\\s*$", regexOptions);
			this.ParameterOnly = new Regex("^\\s*" + text14 + text3 + "Value\\s*$", regexOptions);
			this.StringLiteralOnly = new Regex("^\\s*\"(?<string>((\"\")|[^\"])*)\"\\s*$", regexOptions);
			this.NothingOnly = new Regex("^\\s*Nothing\\s*$", regexOptions);
			this.ReportItemName = new Regex(text5 + text12, regexOptions);
			this.FieldName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text13, regexOptions);
			this.ParameterName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text14, regexOptions);
			this.DataSetName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text15, regexOptions);
			this.DataSourceName = new Regex("(\"((\"\")|[^\"])*\")|" + text5 + text16, regexOptions);
			this.VariableName = new Regex(text5 + text17, regexOptions);
			this.SpecialFunction = new Regex("(\"((\"\")|[^\"])*\")|(?<prefix>" + text5 + ")(?<sfname>RunningValue|RowNumber|First|Last|Previous|Sum|Avg|Max|Min|CountDistinct|Count|CountRows|StDevP|VarP|StDev|Var|Aggregate)\\s*\\(", regexOptions);
			string text19 = Regex.Escape("(");
			string text20 = Regex.Escape(")");
			this.PSAFunction = new Regex("(\"((\"\")|[^\"])*\")|(?<prefix>" + text5 + ")(?<psaname>RunningValue|First|Last|Previous)\\s*\\(", regexOptions);
			string text21 = Regex.Escape(",");
			this.Arguments = new Regex(string.Concat(new string[] { "(\"((\"\")|[^\"])*\")|(?<openParen>", text19, ")|(?<closeParen>", text20, ")|(?<comma>", text21, ")" }), regexOptions);
			this.ReportItemValueReference = new Regex(string.Concat(new string[] { "((", text12, ")|", text18, ")", text3, "Value" }));
			this.ClsIdentifierRegex = new Regex("^[\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}][\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]*$", regexOptions);
		}

		// Token: 0x040000CE RID: 206
		internal Regex Whitespace;

		// Token: 0x040000CF RID: 207
		internal Regex NonConstant;

		// Token: 0x040000D0 RID: 208
		internal Regex FieldDetection;

		// Token: 0x040000D1 RID: 209
		internal Regex ReportItemsDetection;

		// Token: 0x040000D2 RID: 210
		internal Regex ParametersDetection;

		// Token: 0x040000D3 RID: 211
		internal Regex PageGlobalsDetection;

		// Token: 0x040000D4 RID: 212
		internal Regex AggregatesDetection;

		// Token: 0x040000D5 RID: 213
		internal Regex UserDetection;

		// Token: 0x040000D6 RID: 214
		internal Regex DataSetsDetection;

		// Token: 0x040000D7 RID: 215
		internal Regex DataSourcesDetection;

		// Token: 0x040000D8 RID: 216
		internal Regex VariablesDetection;

		// Token: 0x040000D9 RID: 217
		internal Regex MeDotValueExpression;

		// Token: 0x040000DA RID: 218
		internal Regex MeDotValueDetection;

		// Token: 0x040000DB RID: 219
		internal Regex IllegalCharacterDetection;

		// Token: 0x040000DC RID: 220
		internal Regex LineTerminatorDetection;

		// Token: 0x040000DD RID: 221
		internal Regex FieldOnly;

		// Token: 0x040000DE RID: 222
		internal Regex ParameterOnly;

		// Token: 0x040000DF RID: 223
		internal Regex StringLiteralOnly;

		// Token: 0x040000E0 RID: 224
		internal Regex NothingOnly;

		// Token: 0x040000E1 RID: 225
		internal Regex ReportItemName;

		// Token: 0x040000E2 RID: 226
		internal Regex FieldName;

		// Token: 0x040000E3 RID: 227
		internal Regex ParameterName;

		// Token: 0x040000E4 RID: 228
		internal Regex DataSetName;

		// Token: 0x040000E5 RID: 229
		internal Regex DataSourceName;

		// Token: 0x040000E6 RID: 230
		internal Regex SpecialFunction;

		// Token: 0x040000E7 RID: 231
		internal Regex PSAFunction;

		// Token: 0x040000E8 RID: 232
		internal Regex Arguments;

		// Token: 0x040000E9 RID: 233
		internal Regex DynamicFieldReference;

		// Token: 0x040000EA RID: 234
		internal Regex DynamicFieldPropertyReference;

		// Token: 0x040000EB RID: 235
		internal Regex StaticFieldPropertyReference;

		// Token: 0x040000EC RID: 236
		internal Regex RewrittenCommandText;

		// Token: 0x040000ED RID: 237
		internal Regex SimpleDynamicFieldReference;

		// Token: 0x040000EE RID: 238
		internal Regex SimpleDynamicReportItemReference;

		// Token: 0x040000EF RID: 239
		internal Regex SimpleDynamicVariableReference;

		// Token: 0x040000F0 RID: 240
		internal Regex ReportItemValueReference;

		// Token: 0x040000F1 RID: 241
		internal Regex VariableName;

		// Token: 0x040000F2 RID: 242
		internal Regex ClsIdentifierRegex;

		// Token: 0x040000F3 RID: 243
		private const string m_identifierStart = "\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}";

		// Token: 0x040000F4 RID: 244
		private const string m_identifierExtend = "\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}";

		// Token: 0x040000F5 RID: 245
		internal const string ClsReplacerPattern = "[^\\p{Lu}\\p{Ll}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}\\p{Cf}]";

		// Token: 0x040000F6 RID: 246
		internal static readonly ReportRegularExpressions Value = new ReportRegularExpressions();
	}
}

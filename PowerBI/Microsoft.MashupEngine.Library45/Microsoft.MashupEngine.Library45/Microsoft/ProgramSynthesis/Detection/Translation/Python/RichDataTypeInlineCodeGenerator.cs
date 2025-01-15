using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B14 RID: 2836
	internal static class RichDataTypeInlineCodeGenerator
	{
		// Token: 0x060046D2 RID: 18130 RVA: 0x000DD45C File Offset: 0x000DB65C
		public static Optional<GeneratedCode> GenerateInlineParsingCode(this IRichDataType dataType, IPythonColumnInfo columnInfo, CodeGenerationMode codeGenerationMode)
		{
			if (dataType.RequiresOutOfLineParser(columnInfo, codeGenerationMode))
			{
				return Optional<GeneratedCode>.Nothing;
			}
			RichNumericType richNumericType = dataType as RichNumericType;
			Optional<GeneratedCode> optional;
			if (richNumericType == null)
			{
				RichDateType richDateType = dataType as RichDateType;
				if (richDateType == null)
				{
					RichBooleanType richBooleanType = dataType as RichBooleanType;
					if (richBooleanType == null)
					{
						RichStringType richStringType = dataType as RichStringType;
						if (richStringType == null)
						{
							throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Inline code generation not supported for type: {0}.", new object[] { dataType })));
						}
						optional = richStringType.GenerateInlineParsingCode(columnInfo).Some<GeneratedCode>();
					}
					else
					{
						optional = richBooleanType.GenerateInlineParsingCode(columnInfo).Some<GeneratedCode>();
					}
				}
				else
				{
					optional = richDateType.GenerateInlineParsingCode(columnInfo, codeGenerationMode).Some<GeneratedCode>();
				}
			}
			else
			{
				optional = richNumericType.GenerateInlineParsingCode(columnInfo).Some<GeneratedCode>();
			}
			return optional;
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x000DD508 File Offset: 0x000DB708
		private static GeneratedCode GenerateInlineParsingCode(this RichNumericType numericType, IPythonColumnInfo columnInfo)
		{
			string text = "{0}";
			string text2 = (numericType.ContainsRealSubtype ? "float" : "int");
			if (columnInfo.FixPandasNaNBug)
			{
				return new GeneratedCode(FormattableString.Invariant(FormattableStringFactory.Create("{0}({1}) if not isinstance({2}, float) or not math.isnan({3}) else {4}", new object[] { text2, text, text, text, text })), columnInfo, numericType, new string[] { "math" });
			}
			if (numericType.PickOneInterpretation.All((SyntacticNumericType st) => st.CanBeParsedByDefaultParser()))
			{
				return new GeneratedCode(FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { text2 })), columnInfo, numericType, null);
			}
			SyntacticNumericType syntacticNumericType = numericType.PickOneInterpretation.Single<SyntacticNumericType>();
			bool flag = false;
			IEnumerable<KeyValuePair<string, string>> enumerable = syntacticNumericType.EmptySubstitutions.Concat(syntacticNumericType.NonEmptySubstitutions);
			if (syntacticNumericType.IsNegated && syntacticNumericType.HasLeadingSign)
			{
				if (enumerable.Any((KeyValuePair<string, string> kvp) => kvp.Key == "-" && string.IsNullOrEmpty(kvp.Value)))
				{
					flag = true;
					enumerable = enumerable.Where((KeyValuePair<string, string> kvp) => kvp.Key != "-" || !string.IsNullOrEmpty(kvp.Value));
				}
			}
			text = enumerable.Apply(text);
			string text3 = ((syntacticNumericType.IsNegated && !flag) ? "-" : string.Empty);
			return new GeneratedCode(FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}({2})", new object[] { text3, text2, text })), columnInfo, numericType, null);
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x000DD694 File Offset: 0x000DB894
		private static GeneratedCode GenerateInlineParsingCode(this RichDateType dateType, IPythonColumnInfo columnInfo, CodeGenerationMode codeGenerationMode)
		{
			SyntacticDateType syntacticDateType = dateType.SyntacticClusters.Single<IEnumerable<SyntacticDateType>>().First((SyntacticDateType st) => st.Format.PosixParsingFormatString != null);
			string posixParsingFormatString = syntacticDateType.Format.PosixParsingFormatString;
			string text = syntacticDateType.Substitutions.Apply("{0}");
			string text2 = FormattableString.Invariant(FormattableStringFactory.Create("datetime.datetime.strptime({0}, {1})", new object[]
			{
				text,
				posixParsingFormatString.ToPythonLiteral()
			}));
			if (codeGenerationMode != CodeGenerationMode.PysparkDataFrame)
			{
				text2 = dateType.PostprocessDateParseExpression(text2);
			}
			return new GeneratedCode(text2, columnInfo, dateType, new string[] { "datetime" });
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x000DD734 File Offset: 0x000DB934
		private static GeneratedCode GenerateInlineParsingCode(this RichBooleanType booleanType, IPythonColumnInfo columnInfo)
		{
			return new GeneratedCode(FormattableString.Invariant(FormattableStringFactory.Create("True if {{0}} == {0} else False if {{0}} == {1} else {{0}}", new object[]
			{
				booleanType.ExampleTrueValues.Single<string>().ToPythonLiteral(),
				booleanType.ExampleFalseValues.Single<string>().ToPythonLiteral()
			})), columnInfo, booleanType, null);
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x000DD784 File Offset: 0x000DB984
		private static GeneratedCode GenerateInlineParsingCode(this RichStringType stringType, IPythonColumnInfo columnInfo)
		{
			return GeneratedCode.ForPassThrough(columnInfo, stringType);
		}
	}
}

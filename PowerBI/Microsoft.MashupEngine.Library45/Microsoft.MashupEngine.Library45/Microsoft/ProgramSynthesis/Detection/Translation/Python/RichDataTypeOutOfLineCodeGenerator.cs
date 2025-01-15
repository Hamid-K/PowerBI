using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Translation.Python;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B16 RID: 2838
	internal static class RichDataTypeOutOfLineCodeGenerator
	{
		// Token: 0x060046DD RID: 18141 RVA: 0x000DD7EC File Offset: 0x000DB9EC
		public static GeneratedCode GenerateOutOfLineParsingCode(this IRichDataType dataType, IPythonColumnInfo columnInfo, CodeGenerationMode codeGenerationMode)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			GeneratedCode generatedCode2;
			using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("def {0}(value):", new object[] { columnInfo.ParseFunctionName() })), 1U))
			{
				if (dataType.NullsExpectedInData)
				{
					using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("if value is None:", Array.Empty<object>())), 1U))
					{
						codeBuilder.AppendLine("return None");
					}
				}
				if (columnInfo.FixPandasNaNBug)
				{
					codeBuilder.AppendLine("import math");
					using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("if isinstance(value, float) and math.isnan(value):", Array.Empty<object>())), 1U))
					{
						codeBuilder.AppendLine("# Workaround for pandas issue 14978. See https://github.com/pandas-dev/pandas/issues/14978.");
						codeBuilder.AppendLine("value = str(value)");
					}
				}
				if (dataType.NormalizableStringsExpectedInData)
				{
					codeBuilder.AppendLine("value = value.strip()");
				}
				RichNumericType richNumericType = dataType as RichNumericType;
				GeneratedCode generatedCode;
				if (richNumericType == null)
				{
					RichDateType richDateType = dataType as RichDateType;
					if (richDateType == null)
					{
						RichBooleanType richBooleanType = dataType as RichBooleanType;
						if (richBooleanType == null)
						{
							throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Out-of-line code generation not supported for type: {0}.", new object[] { dataType })));
						}
						generatedCode = richBooleanType.GenerateOutOfLineParsingCode(columnInfo, codeBuilder);
					}
					else
					{
						generatedCode = richDateType.GenerateOutOfLineParsingCode(columnInfo, codeGenerationMode, codeBuilder);
					}
				}
				else
				{
					generatedCode = richNumericType.GenerateOutOfLineParsingCode(columnInfo, codeBuilder);
				}
				generatedCode2 = generatedCode;
			}
			return generatedCode2;
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x000DD990 File Offset: 0x000DBB90
		private static GeneratedCode GenerateOutOfLineParsingCode(this RichBooleanType booleanType, IPythonColumnInfo columnInfo, CodeBuilder builder)
		{
			using (builder.NewScope(RichDataTypeOutOfLineCodeGenerator.GenerateGuardStatement(booleanType.ExampleTrueValues.ToList<string>()), 1U))
			{
				builder.AppendLine("return True");
			}
			using (builder.NewScope(RichDataTypeOutOfLineCodeGenerator.GenerateGuardStatement(booleanType.ExampleFalseValues.ToList<string>()), 1U))
			{
				builder.AppendLine("return False");
			}
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(booleanType.SingleNonWhitespaceNaValue))
			{
				list.Add(booleanType.SingleNonWhitespaceNaValue);
			}
			if (booleanType.EmptyStringsExpectedInData)
			{
				list.Add(string.Empty);
			}
			RichDataTypeOutOfLineCodeGenerator.GenerateNaValueCode(list, builder);
			RichDataTypeOutOfLineCodeGenerator.GenerateCodeForUnhandledCases(builder, columnInfo);
			return new GeneratedCode(builder, columnInfo, booleanType, null);
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x000DDA60 File Offset: 0x000DBC60
		private static string GenerateGuardStatement(IReadOnlyList<string> choices)
		{
			if (choices.Count == 1)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("if value == {0}:", new object[] { choices.Single<string>().ToPythonLiteral() }));
			}
			string text = "[{0}]";
			object[] array = new object[1];
			array[0] = string.Join(", ", choices.Select((string v) => v.ToPythonLiteral()));
			string text2 = FormattableString.Invariant(FormattableStringFactory.Create(text, array));
			return FormattableString.Invariant(FormattableStringFactory.Create("if value in {0}:", new object[] { text2 }));
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x000DDAFC File Offset: 0x000DBCFC
		private static GeneratedCode GenerateOutOfLineParsingCode(this RichNumericType numericType, IPythonColumnInfo columnInfo, CodeBuilder builder)
		{
			RichDataTypeOutOfLineCodeGenerator.<>c__DisplayClass4_0 CS$<>8__locals1;
			CS$<>8__locals1.numericType = numericType;
			CS$<>8__locals1.builder = builder;
			IEnumerable<SyntacticNumericType> enumerable = CS$<>8__locals1.numericType.PickOneInterpretation.Where((SyntacticNumericType st) => !st.IsNaValue);
			CS$<>8__locals1.naValueSubtypes = CS$<>8__locals1.numericType.PickOneInterpretation.Where((SyntacticNumericType st) => st.IsNaValue);
			string text = (CS$<>8__locals1.numericType.ContainsRealSubtype ? "float" : "int");
			List<string> list = new List<string>();
			if (enumerable.All((SyntacticNumericType st) => st.CanBeParsedByDefaultParser()))
			{
				RichDataTypeOutOfLineCodeGenerator.<GenerateOutOfLineParsingCode>g__AppendCodeForNaValues|4_2(ref CS$<>8__locals1);
				CS$<>8__locals1.builder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return {0}(value)", new object[] { text })));
			}
			else
			{
				foreach (SyntacticNumericType syntacticNumericType in enumerable)
				{
					list.Add("regex");
					Regex membershipRegex = syntacticNumericType.MembershipRegex;
					using (CS$<>8__locals1.builder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("if regex.match({0}, value):", new object[] { membershipRegex.ToString().ToPythonLiteral() })), 1U))
					{
						CodeBuilder builder2 = CS$<>8__locals1.builder;
						string text2 = "# Parse values formatted like: {0}...";
						object[] array = new object[1];
						array[0] = string.Join(", ", from v in syntacticNumericType.Examples.Take(3)
							select FormattableString.Invariant(FormattableStringFactory.Create("\"{0}\"", new object[] { v })));
						builder2.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create(text2, array)));
						if (syntacticNumericType.CanBeParsedByDefaultParser())
						{
							CS$<>8__locals1.builder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return {0}(value)", new object[] { text })));
						}
						else
						{
							foreach (KeyValuePair<string, string> keyValuePair in syntacticNumericType.EmptySubstitutions.Concat(syntacticNumericType.NonEmptySubstitutions))
							{
								CS$<>8__locals1.builder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("value = value.replace({0}, {1})", new object[]
								{
									keyValuePair.Key.ToPythonLiteral(),
									keyValuePair.Value.ToPythonLiteral()
								})));
							}
							string text3 = (syntacticNumericType.IsNegated ? "-" : string.Empty);
							CS$<>8__locals1.builder.AppendLine("return " + text3 + text + "(value)");
						}
						CS$<>8__locals1.builder.AppendLine();
					}
				}
				RichDataTypeOutOfLineCodeGenerator.<GenerateOutOfLineParsingCode>g__AppendCodeForNaValues|4_2(ref CS$<>8__locals1);
				RichDataTypeOutOfLineCodeGenerator.GenerateCodeForUnhandledCases(CS$<>8__locals1.builder, columnInfo);
			}
			return new GeneratedCode(CS$<>8__locals1.builder, columnInfo, CS$<>8__locals1.numericType, list);
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x000DDE38 File Offset: 0x000DC038
		private static GeneratedCode GenerateOutOfLineParsingCode(this RichDateType dateType, IPythonColumnInfo columnInfo, CodeGenerationMode codeGenerationMode, CodeBuilder builder)
		{
			List<IEnumerable<SyntacticDateType>> list = dateType.SyntacticClusters.Where((IEnumerable<SyntacticDateType> st) => st.All((SyntacticDateType t) => !t.IsNaValue)).ToList<IEnumerable<SyntacticDateType>>();
			IEnumerable<string> enumerable = from st in dateType.SyntacticClusters
				where st.Any((SyntacticDateType t) => t.IsNaValue)
				select st.Single<SyntacticDateType>().NaValue.Value;
			if (dateType.EmptyStringsExpectedInData)
			{
				enumerable = enumerable.AppendItem(string.Empty);
			}
			List<string> list2 = enumerable.ToList<string>();
			if (list.Count<IEnumerable<SyntacticDateType>>() == 1 && list2.Count<string>() == 0)
			{
				RichDataTypeOutOfLineCodeGenerator.GenerateDateParsingCodeForOneCluster(list.Single<IEnumerable<SyntacticDateType>>(), dateType, builder, codeGenerationMode);
			}
			else
			{
				int i = 0;
				while (i < list.Count)
				{
					IEnumerable<SyntacticDateType> enumerable2 = list[i];
					if (i != list.Count - 1 || list2.Any<string>())
					{
						using (builder.NewScope("try:", 1U))
						{
							RichDataTypeOutOfLineCodeGenerator.GenerateDateParsingCodeForOneCluster(enumerable2, dateType, builder, codeGenerationMode);
						}
						using (builder.NewScope("except ValueError:", 1U))
						{
							builder.AppendLine("pass");
							goto IL_013B;
						}
						goto IL_0131;
					}
					goto IL_0131;
					IL_013B:
					builder.AppendLine();
					i++;
					continue;
					IL_0131:
					RichDataTypeOutOfLineCodeGenerator.GenerateDateParsingCodeForOneCluster(enumerable2, dateType, builder, codeGenerationMode);
					goto IL_013B;
				}
				if (list2.Any<string>())
				{
					RichDataTypeOutOfLineCodeGenerator.GenerateNaValueCode(list2, builder);
					RichDataTypeOutOfLineCodeGenerator.GenerateCodeForUnhandledCases(builder, columnInfo);
				}
			}
			return new GeneratedCode(builder, columnInfo, dateType, new string[] { "datetime" });
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x000DDFE0 File Offset: 0x000DC1E0
		private static void GenerateDateParsingCodeForOneCluster(IEnumerable<SyntacticDateType> cluster, RichDateType detectedType, CodeBuilder builder, CodeGenerationMode codeGenerationMode)
		{
			IEnumerable<string> enumerable = cluster.SelectMany((SyntacticDateType st) => st.Examples);
			SyntacticDateType syntacticDateType = cluster.FirstOrDefault((SyntacticDateType c) => !string.IsNullOrEmpty(c.Format.PosixParsingFormatString));
			string text = "# Parse values formatted like: {0} ...";
			object[] array = new object[1];
			array[0] = string.Join(", ", from v in enumerable.Take(3)
				select FormattableString.Invariant(FormattableStringFactory.Create("\"{0}\"", new object[] { v })));
			builder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create(text, array)));
			if (syntacticDateType != null)
			{
				RichDataTypeOutOfLineCodeGenerator.GeneratePosixParsingCodeForOneCluster(syntacticDateType, detectedType, builder, codeGenerationMode);
				return;
			}
			syntacticDateType = cluster.First((SyntacticDateType c) => c.Format != null);
			DateTimeFormat format = syntacticDateType.Format;
			string text2 = syntacticDateType.Substitutions.Apply("value");
			string text3;
			CodeForGeneratedFunction codeForGeneratedFunction;
			ReadablePythonTranslatorDateTime.GenerateDateTimeFormatParsingCode(format, text2).Deconstruct(out text3, out codeForGeneratedFunction);
			string text4 = text3;
			CodeForGeneratedFunction codeForGeneratedFunction2 = codeForGeneratedFunction;
			builder.Append(codeForGeneratedFunction2.DynamicCode);
			builder.Prepend(codeForGeneratedFunction2.StaticCode);
			RichDataTypeOutOfLineCodeGenerator.GenerateDateParsingCode(text4, codeGenerationMode, detectedType, builder);
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x000DE110 File Offset: 0x000DC310
		private static void GeneratePosixParsingCodeForOneCluster(SyntacticDateType targetCluster, RichDateType detectedType, CodeBuilder builder, CodeGenerationMode codeGenerationMode)
		{
			string posixParsingFormatString = targetCluster.Format.PosixParsingFormatString;
			string text = targetCluster.Substitutions.Apply("value");
			RichDataTypeOutOfLineCodeGenerator.GenerateDateParsingCode(FormattableString.Invariant(FormattableStringFactory.Create("datetime.datetime.strptime({0}, {1})", new object[]
			{
				text,
				posixParsingFormatString.ToPythonLiteral()
			})), codeGenerationMode, detectedType, builder);
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x000DE164 File Offset: 0x000DC364
		private static void GenerateDateParsingCode(string parseExpression, CodeGenerationMode codeGenerationMode, RichDateType detectedType, CodeBuilder builder)
		{
			if (codeGenerationMode != CodeGenerationMode.PysparkDataFrame)
			{
				string text = detectedType.PostprocessDateParseExpression(parseExpression);
				using (builder.NewScope("if " + text + ":", 1U))
				{
					builder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return {0}", new object[] { text })));
				}
				return;
			}
			if (detectedType.HasTime && !detectedType.HasDate)
			{
				builder.AppendLine("# The data contains only a time. strptime() fills in a default");
				builder.AppendLine("# value of 1900 for the year, and 01 for the month and day.");
				builder.AppendLine("# Unfortunately, PySpark does not like dates earlier than the");
				builder.AppendLine("# UNIX epoch (1970). Further, Python 3.6.x has a bug which does");
				builder.AppendLine("# not like dates near 1970, so we change the year to 2000 before");
				builder.AppendLine("# returning the result.");
				builder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("_result = {0}", new object[] { parseExpression })));
				using (builder.NewScope("return datetime.datetime(", 1U))
				{
					builder.AppendLine("2000,");
					builder.AppendLine("_result.month,");
					builder.AppendLine("_result.day,");
					builder.AppendLine("_result.hour,");
					builder.AppendLine("_result.minute,");
					builder.AppendLine("_result.second,");
					builder.AppendLine("_result.microsecond,");
					builder.AppendLine("_result.tzinfo,");
				}
				builder.AppendLine(")");
				return;
			}
			builder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("return {0}", new object[] { parseExpression })));
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x000DE2F4 File Offset: 0x000DC4F4
		private static void GenerateNaValueCode(IEnumerable<string> naValueStrings, CodeBuilder builder)
		{
			naValueStrings = naValueStrings.Memoize<string>();
			if (naValueStrings.Any<string>())
			{
				builder.AppendLine("# return None if the value is in the set of identified NA values.");
				string text = "[{0}]";
				object[] array = new object[1];
				array[0] = string.Join(", ", naValueStrings.Select((string s) => s.ToPythonLiteral()));
				string text2 = FormattableString.Invariant(FormattableStringFactory.Create(text, array));
				using (builder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("if value in {0}:", new object[] { text2 })), 1U))
				{
					builder.AppendLine("return None");
				}
			}
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x000DE3B0 File Offset: 0x000DC5B0
		private static void GenerateCodeForUnhandledCases(CodeBuilder builder, IPythonColumnInfo columnInfo)
		{
			builder.AppendLine("# We didn't encounter a value formatted like this when the datatype detection was performed.");
			if (columnInfo == null)
			{
				builder.AppendLine("raise ValueError(\"Unhandled case in type conversion: '%s'\" % value)");
				return;
			}
			using (builder.NewScope("raise ValueError(", 1U))
			{
				builder.AppendLine("\"Unhandled case in type conversion for column %s: '%s'\" % (" + columnInfo.ColumnName.ToPythonLiteral() + ", value)");
			}
			builder.AppendLine(")");
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x000DE42C File Offset: 0x000DC62C
		[CompilerGenerated]
		internal static void <GenerateOutOfLineParsingCode>g__AppendCodeForNaValues|4_2(ref RichDataTypeOutOfLineCodeGenerator.<>c__DisplayClass4_0 A_0)
		{
			IEnumerable<string> enumerable = A_0.naValueSubtypes.Select((SyntacticNumericType st) => st.NaValue.Value);
			if (A_0.numericType.EmptyStringsExpectedInData)
			{
				enumerable = enumerable.AppendItem(string.Empty);
			}
			RichDataTypeOutOfLineCodeGenerator.GenerateNaValueCode(enumerable, A_0.builder);
		}

		// Token: 0x0400204B RID: 8267
		private const int MaxExamplesToShow = 3;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B23 RID: 2851
	public static class PythonTranslator
	{
		// Token: 0x0600472D RID: 18221 RVA: 0x000DED3C File Offset: 0x000DCF3C
		public static PythonSnippet Translate(IReadOnlyList<KeyValuePair<IPythonColumnInfo, IRichDataType>> detectedTypes, CodeGenerationMode mode, string resultVariableName, string pdAlias = "pd")
		{
			string text;
			PythonImports pythonImports;
			if (detectedTypes.All((KeyValuePair<IPythonColumnInfo, IRichDataType> t) => t.Value == null || t.Value is RichStringType))
			{
				text = string.Empty;
				pythonImports = new PythonImports();
			}
			else
			{
				string text2;
				switch (mode)
				{
				case CodeGenerationMode.Dictionary:
					text2 = PythonTranslator.GenerateCodeForDictionaryMode(detectedTypes, resultVariableName, out pythonImports);
					break;
				case CodeGenerationMode.PandasDataFrame:
					text2 = PythonTranslator.GenerateCodeForDataFrameMode(detectedTypes, resultVariableName, pdAlias, out pythonImports);
					break;
				case CodeGenerationMode.PysparkDataFrame:
					text2 = PythonTranslator.GenerateCodeForPySparkMode(detectedTypes, resultVariableName, out pythonImports);
					break;
				default:
					throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Code generation mode \"{0}\" is not supported.", new object[] { mode })));
				}
				text = text2;
			}
			return new PythonSnippet(pythonImports, text, resultVariableName, resultVariableName);
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x000DEDE8 File Offset: 0x000DCFE8
		public static PythonSnippet TranslateToFormulaExpression(IRichDataType detectedType, string dfName, string columnName, string pandasAlias)
		{
			PythonColumnInfo pythonColumnInfo = new PythonColumnInfo(columnName, null, null, false, false, true);
			return PythonTranslator.Translate(new Dictionary<IPythonColumnInfo, IRichDataType> { { pythonColumnInfo, detectedType } }.ToList<KeyValuePair<IPythonColumnInfo, IRichDataType>>(), CodeGenerationMode.PandasDataFrame, dfName, pandasAlias);
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x000DEE24 File Offset: 0x000DD024
		private static void GenerateOutOfLineFunctions(CodeBuilder builder, IReadOnlyList<GeneratedCode> gcObjects)
		{
			foreach (GeneratedCode generatedCode in gcObjects.Where((GeneratedCode gc) => gc.Kind == GeneratedCodeKind.OutOfLineFunction))
			{
				builder.Append(generatedCode.OutOfLineFunction);
				builder.AppendLine();
			}
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x000DEE9C File Offset: 0x000DD09C
		private static IReadOnlyList<GeneratedCode> GenerateCodeForDetectedTypes(IReadOnlyList<KeyValuePair<IPythonColumnInfo, IRichDataType>> detectedTypes, CodeGenerationMode codeGenerationMode)
		{
			return detectedTypes.Select((KeyValuePair<IPythonColumnInfo, IRichDataType> kvp) => kvp.Value.GenerateParsingCode(kvp.Key, codeGenerationMode)).ToList<GeneratedCode>();
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x000DEED0 File Offset: 0x000DD0D0
		private static string GenerateCodeForDictionaryMode(IReadOnlyList<KeyValuePair<IPythonColumnInfo, IRichDataType>> detectedTypes, string resultVariableName, out PythonImports imports)
		{
			IReadOnlyList<GeneratedCode> readOnlyList = PythonTranslator.GenerateCodeForDetectedTypes(detectedTypes, CodeGenerationMode.Dictionary);
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			PythonTranslator.GenerateOutOfLineFunctions(codeBuilder, readOnlyList);
			imports = new PythonImports();
			foreach (GeneratedCode generatedCode in readOnlyList)
			{
				if (generatedCode.DetectedType != null && !(generatedCode.DetectedType is RichStringType))
				{
					imports.MergeWith(generatedCode.Imports);
					string columnNameLiteral = generatedCode.ColumnInfo.ColumnNameLiteral;
					string text = FormattableString.Invariant(FormattableStringFactory.Create("{0}[{1}]", new object[] { resultVariableName, columnNameLiteral }));
					string text2 = generatedCode.DetectedType.HumanReadableTypeName();
					codeBuilder.Append(FormattableString.Invariant(FormattableStringFactory.Create("{0} = [{1} for {2} in {3}]", new object[]
					{
						text,
						generatedCode.Expression("x"),
						"x",
						text
					})));
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("  # {0}", new object[] { text2 })));
				}
			}
			return codeBuilder.GetCode();
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x000DF000 File Offset: 0x000DD200
		private static string GenerateCodeForDataFrameMode(IReadOnlyList<KeyValuePair<IPythonColumnInfo, IRichDataType>> detectedTypes, string resultVariableName, string pdAlias, out PythonImports imports)
		{
			Dictionary<IPythonColumnInfo, FormulaExpression> generatedFormulaExpressions = detectedTypes.ToDictionary((KeyValuePair<IPythonColumnInfo, IRichDataType> kvp) => kvp.Key, (KeyValuePair<IPythonColumnInfo, IRichDataType> kvp) => kvp.Value.ToPandasFormulaExpression(kvp.Key.ColumnName, resultVariableName, pdAlias));
			IReadOnlyList<GeneratedCode> readOnlyList = PythonTranslator.GenerateCodeForDetectedTypes(detectedTypes.Where((KeyValuePair<IPythonColumnInfo, IRichDataType> kvp) => generatedFormulaExpressions[kvp.Key] == null).ToList<KeyValuePair<IPythonColumnInfo, IRichDataType>>(), CodeGenerationMode.PandasDataFrame);
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			(from kvp in generatedFormulaExpressions
				where kvp.Value != null
				select string.Concat(new string[]
				{
					resultVariableName,
					"[",
					kvp.Key.ColumnNameLiteral,
					"] = ",
					kvp.Value.ToString()
				})).ToList<string>().ForEach(delegate(string code)
			{
				codeBuilder.AppendLine(code);
			});
			PythonTranslator.GenerateOutOfLineFunctions(codeBuilder, readOnlyList);
			imports = new PythonImports();
			if (generatedFormulaExpressions.Any((KeyValuePair<IPythonColumnInfo, FormulaExpression> kvp) => kvp.Value != null))
			{
				imports.AddImport("pandas as pd");
			}
			foreach (GeneratedCode generatedCode in readOnlyList)
			{
				if (generatedCode.DetectedType != null && !(generatedCode.DetectedType is RichStringType))
				{
					imports.MergeWith(generatedCode.Imports);
					string text = resultVariableName + "[" + generatedCode.ColumnInfo.ColumnNameLiteral + "]";
					string text2 = generatedCode.DetectedType.HumanReadableTypeName();
					codeBuilder.Append(string.Concat(new string[]
					{
						text,
						" = ",
						text,
						".apply(",
						generatedCode.LambdaExpression("x"),
						")"
					}));
					codeBuilder.AppendLine("  # " + text2);
				}
			}
			return codeBuilder.GetCode();
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x000DF220 File Offset: 0x000DD420
		private static string GenerateCodeForPySparkMode(IReadOnlyList<KeyValuePair<IPythonColumnInfo, IRichDataType>> detectedTypes, string resultVariableName, out PythonImports imports)
		{
			IReadOnlyList<GeneratedCode> readOnlyList = PythonTranslator.GenerateCodeForDetectedTypes(detectedTypes, CodeGenerationMode.PysparkDataFrame);
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			PythonTranslator.GenerateOutOfLineFunctions(codeBuilder, readOnlyList);
			IEnumerable<GeneratedCode> enumerable = readOnlyList.Where((GeneratedCode gc) => gc.DetectedType != null && !(gc.DetectedType is RichStringType)).ToList<GeneratedCode>();
			imports = new PythonImports();
			imports.AddFromImport("pyspark.sql.functions", "udf");
			foreach (GeneratedCode generatedCode in enumerable)
			{
				imports.MergeWith(generatedCode.Imports);
				using (codeBuilder.NewScope(resultVariableName + " = " + resultVariableName + ".withColumn(", 1U))
				{
					string columnNameLiteral = generatedCode.ColumnInfo.ColumnNameLiteral;
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0},", new object[] { columnNameLiteral })));
					using (codeBuilder.NewScope("udf(", 1U))
					{
						string text = generatedCode.DetectedType.PysparkTypeName();
						string text2 = text.Split(new char[] { '(' }, 2).First<string>();
						imports.AddFromImport("pyspark.sql.types", text2);
						codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0}, {1}", new object[]
						{
							generatedCode.LambdaExpression("x"),
							text
						})));
					}
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create(")({0}),", new object[] { columnNameLiteral })));
				}
				codeBuilder.AppendLine(")");
			}
			return codeBuilder.GetCode();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python;

namespace Microsoft.ProgramSynthesis.Detection.Translation.Python
{
	// Token: 0x02000B1B RID: 2843
	public static class PandasTranslator
	{
		// Token: 0x060046FE RID: 18174 RVA: 0x000DE56C File Offset: 0x000DC76C
		public static FormulaExpression ToPandasFormulaExpression(this IRichDataType detectedType, string columnName, string dfName = "df", string pdAlias = "pd")
		{
			return detectedType.ToPandasFormulaExpression(PandasExpressionHelper.Column(dfName, columnName), pdAlias);
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x000DE57C File Offset: 0x000DC77C
		public static FormulaExpression ToPandasFormulaExpression(this IRichDataType detectedType, FormulaExpression dfColumn, string pdAlias = "pd")
		{
			RichNumericType richNumericType = detectedType as RichNumericType;
			FormulaExpression formulaExpression;
			if (richNumericType == null)
			{
				RichDateType richDateType = detectedType as RichDateType;
				if (richDateType == null)
				{
					RichBooleanType richBooleanType = detectedType as RichBooleanType;
					if (richBooleanType == null)
					{
						RichCategoricalType richCategoricalType = detectedType as RichCategoricalType;
						if (richCategoricalType == null)
						{
							formulaExpression = null;
						}
						else
						{
							formulaExpression = richCategoricalType.ToPandasFormulaExpression(dfColumn, pdAlias);
						}
					}
					else
					{
						formulaExpression = richBooleanType.ToPandasFormulaExpression(dfColumn, pdAlias);
					}
				}
				else
				{
					formulaExpression = richDateType.ToPandasFormulaExpression(dfColumn, pdAlias);
				}
			}
			else
			{
				formulaExpression = richNumericType.ToPandasFormulaExpression(dfColumn, pdAlias);
			}
			return formulaExpression;
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x000DE5E8 File Offset: 0x000DC7E8
		private static FormulaExpression ToPandasFormulaExpression(this IEnumerable<KeyValuePair<string, string>> substitutions, FormulaExpression baseExpr)
		{
			return substitutions.Aggregate(baseExpr, (FormulaExpression acc, KeyValuePair<string, string> kvp) => PythonExpressionHelper.Dot(acc, PythonExpressionHelper.Func("str.replace", new FormulaExpression[]
			{
				PythonExpressionHelper.StringLiteral(kvp.Key),
				PythonExpressionHelper.StringLiteral(kvp.Value),
				PythonExpressionHelper.AssignArg("regex", PythonExpressionHelper.Variable("False"))
			})));
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x000DE610 File Offset: 0x000DC810
		private static FormulaExpression ToPandasFormulaExpression(this RichNumericType numericType, FormulaExpression dfColumn, string pdAlias = "pd")
		{
			PandasTranslator.<>c__DisplayClass3_0 CS$<>8__locals1 = new PandasTranslator.<>c__DisplayClass3_0();
			CS$<>8__locals1.pdAlias = pdAlias;
			CS$<>8__locals1.dfColumn = dfColumn;
			CS$<>8__locals1.pickedType = numericType.PickOneInterpretation;
			CS$<>8__locals1.coerce = PythonExpressionHelper.AssignArg("errors", PythonExpressionHelper.StringLiteral("coerce"));
			IEnumerable<SyntacticNumericType> enumerable = from st in CS$<>8__locals1.pickedType
				where !st.CanBeParsedByDefaultParser()
				where !st.IsNaValue
				select st;
			FormulaExpression formulaExpression = CS$<>8__locals1.dfColumn;
			if (CS$<>8__locals1.pickedType.Any((SyntacticNumericType st) => st.CanBeParsedByDefaultParser()))
			{
				formulaExpression = CS$<>8__locals1.<ToPandasFormulaExpression>g__defaultParser|0(CS$<>8__locals1.dfColumn);
			}
			formulaExpression = enumerable.Aggregate(formulaExpression, (FormulaExpression acc, SyntacticNumericType st) => base.<ToPandasFormulaExpression>g__baseParser|3(st, acc));
			if (!numericType.ContainsRealSubtype)
			{
				formulaExpression = PythonExpressionHelper.Dot(formulaExpression, PythonExpressionHelper.Func("astype", new FormulaExpression[] { PythonExpressionHelper.StringLiteral("Int64") }));
			}
			return formulaExpression;
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x000DE728 File Offset: 0x000DC928
		private static FormulaExpression ToPandasFormulaExpression(this RichBooleanType booleanType, FormulaExpression dfColumn, string pdAlias = "pd")
		{
			PandasTranslator.<>c__DisplayClass4_0 CS$<>8__locals1;
			CS$<>8__locals1.dfColumn = dfColumn;
			FormulaExpression formulaExpression = PandasTranslator.<ToPandasFormulaExpression>g__IsIn|4_1(booleanType.ExampleTrueValues, ref CS$<>8__locals1);
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(booleanType.SingleNonWhitespaceNaValue))
			{
				list.Add(booleanType.SingleNonWhitespaceNaValue);
			}
			if (booleanType.EmptyStringsExpectedInData)
			{
				list.Add(string.Empty);
			}
			if (list.Count > 0)
			{
				FormulaExpression formulaExpression2 = PandasTranslator.<ToPandasFormulaExpression>g__IsIn|4_1(list, ref CS$<>8__locals1);
				formulaExpression = PythonExpressionHelper.Dot(formulaExpression, PythonExpressionHelper.Func("mask", new FormulaExpression[]
				{
					formulaExpression2,
					PythonExpressionHelper.Variable("None")
				}));
				formulaExpression = PythonExpressionHelper.Dot(formulaExpression, PythonExpressionHelper.Func("astype", new FormulaExpression[] { PythonExpressionHelper.StringLiteral("boolean") }));
			}
			return formulaExpression;
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x00002188 File Offset: 0x00000388
		private static FormulaExpression ToPandasFormulaExpression(this RichStringType stringType, FormulaExpression dfColumn, string pdAlias = "pd")
		{
			return null;
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x000DE7DC File Offset: 0x000DC9DC
		private static FormulaExpression ToPandasFormulaExpression(this RichCategoricalType categoricalType, FormulaExpression dfColumn, string pdAlias = "pd")
		{
			return PythonExpressionHelper.Dot(dfColumn, PythonExpressionHelper.Func("astype", new FormulaExpression[] { PythonExpressionHelper.StringLiteral("category") }));
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x000DE804 File Offset: 0x000DCA04
		private static FormulaExpression ToPandasFormulaExpression(this RichDateType dateType, FormulaExpression dfColumn, string pdAlias = "pd")
		{
			PandasTranslator.<>c__DisplayClass7_0 CS$<>8__locals1 = new PandasTranslator.<>c__DisplayClass7_0();
			CS$<>8__locals1.pdAlias = pdAlias;
			CS$<>8__locals1.dfColumn = dfColumn;
			List<IEnumerable<SyntacticDateType>> list = dateType.SyntacticClusters.Where((IEnumerable<SyntacticDateType> st) => st.All((SyntacticDateType t) => !t.IsNaValue)).ToList<IEnumerable<SyntacticDateType>>();
			bool flag = dateType.SyntacticClusters.Count > list.Count;
			IEnumerable<SyntacticDateType> enumerable = list.Select((IEnumerable<SyntacticDateType> cluster) => cluster.FirstOrDefault((SyntacticDateType c) => !string.IsNullOrEmpty(c.Format.PosixParsingFormatString)));
			if (enumerable.Any((SyntacticDateType st) => st == null))
			{
				return null;
			}
			FormulaExpression formulaExpression;
			if (list.Count == 1 && !flag)
			{
				formulaExpression = CS$<>8__locals1.<ToPandasFormulaExpression>g__baseParser|5(enumerable.Single<SyntacticDateType>(), null);
			}
			else
			{
				FormulaExpression coerceExpr = PythonExpressionHelper.AssignArg("errors", PythonExpressionHelper.StringLiteral("coerce"));
				formulaExpression = enumerable.Select((SyntacticDateType st) => CS$<>8__locals1.<ToPandasFormulaExpression>g__baseParser|5(st, coerceExpr)).Aggregate((FormulaExpression acc, FormulaExpression parser) => PythonExpressionHelper.Dot(acc, PythonExpressionHelper.Func("fillna", new FormulaExpression[] { parser })));
			}
			if (dateType.HasDate && !dateType.HasTime)
			{
				formulaExpression = PythonExpressionHelper.Dot(formulaExpression, PythonExpressionHelper.Variable("dt.date"));
			}
			else if (!dateType.HasDate && dateType.HasTime)
			{
				formulaExpression = PythonExpressionHelper.Dot(formulaExpression, PythonExpressionHelper.Variable("dt.time"));
			}
			return formulaExpression;
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x000DE984 File Offset: 0x000DCB84
		[CompilerGenerated]
		internal static FormulaExpression <ToPandasFormulaExpression>g__IsInExprs|4_0(IEnumerable<FormulaExpression> values, ref PandasTranslator.<>c__DisplayClass4_0 A_1)
		{
			return PythonExpressionHelper.Dot(A_1.dfColumn, PythonExpressionHelper.Func("isin", new FormulaExpression[] { PythonExpressionHelper.Array(values) }));
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x000DE9AA File Offset: 0x000DCBAA
		[CompilerGenerated]
		internal static FormulaExpression <ToPandasFormulaExpression>g__IsIn|4_1(IEnumerable<string> values, ref PandasTranslator.<>c__DisplayClass4_0 A_1)
		{
			return PandasTranslator.<ToPandasFormulaExpression>g__IsInExprs|4_0(values.Select((string v) => PythonExpressionHelper.StringLiteral(v)), ref A_1);
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x000DE9D7 File Offset: 0x000DCBD7
		[CompilerGenerated]
		internal static FormulaExpression <ToPandasFormulaExpression>g__formatExpr|7_3(string format)
		{
			return PythonExpressionHelper.AssignArg("format", PythonExpressionHelper.StringLiteral(format));
		}
	}
}

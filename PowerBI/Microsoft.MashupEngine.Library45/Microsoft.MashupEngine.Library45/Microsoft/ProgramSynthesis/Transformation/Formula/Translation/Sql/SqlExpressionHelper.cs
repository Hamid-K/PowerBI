using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001829 RID: 6185
	internal static class SqlExpressionHelper
	{
		// Token: 0x0600CABD RID: 51901 RVA: 0x002B4A22 File Offset: 0x002B2C22
		public static FormulaExpression Average(IEnumerable<FormulaExpression> terms)
		{
			terms = terms.ToReadOnlyList<FormulaExpression>();
			return SqlExpressionHelper.Divide(SqlExpressionHelper.Sum(terms), SqlExpressionHelper.NumberLiteral(terms.Count<FormulaExpression>()));
		}

		// Token: 0x0600CABE RID: 51902 RVA: 0x002B4A42 File Offset: 0x002B2C42
		public static FormulaExpression Char(FormulaExpression subject)
		{
			return SqlExpressionHelper.Func("Char", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CABF RID: 51903 RVA: 0x002B4A58 File Offset: 0x002B2C58
		public static string CharCodeToString(int charCode)
		{
			string text;
			if (charCode == 10)
			{
				text = "\n";
			}
			else
			{
				text = null;
			}
			return text;
		}

		// Token: 0x0600CAC0 RID: 51904 RVA: 0x002B4A75 File Offset: 0x002B2C75
		public static FormulaExpression CharIndex(FormulaExpression delimiter, FormulaExpression subject)
		{
			return SqlExpressionHelper.Func("CharIndex", new FormulaExpression[] { delimiter, subject });
		}

		// Token: 0x0600CAC1 RID: 51905 RVA: 0x002B4A8F File Offset: 0x002B2C8F
		public static FormulaExpression CharIndex(FormulaExpression delimiter, FormulaExpression subject, FormulaExpression startIdx)
		{
			return SqlExpressionHelper.Func("CharIndex", new FormulaExpression[] { delimiter, subject, startIdx });
		}

		// Token: 0x0600CAC2 RID: 51906 RVA: 0x002B4AB0 File Offset: 0x002B2CB0
		public static FormulaExpression Concat(FormulaExpression left, FormulaExpression right)
		{
			SqlStringLiteral sqlStringLiteral = left as SqlStringLiteral;
			if (sqlStringLiteral != null)
			{
				SqlStringLiteral sqlStringLiteral2 = right as SqlStringLiteral;
				if (sqlStringLiteral2 != null)
				{
					return SqlExpressionHelper.StringLiteral(sqlStringLiteral.Value + sqlStringLiteral2.Value);
				}
			}
			return new SqlConcat(left, right);
		}

		// Token: 0x0600CAC3 RID: 51907 RVA: 0x002B4AEF File Offset: 0x002B2CEF
		public static FormulaExpression DataLength(FormulaExpression str)
		{
			return SqlExpressionHelper.Func("DataLength", new FormulaExpression[] { str });
		}

		// Token: 0x0600CAC4 RID: 51908 RVA: 0x002B4B05 File Offset: 0x002B2D05
		public static SqlType DateTime2Type()
		{
			return new SqlDateTime2Type();
		}

		// Token: 0x0600CAC5 RID: 51909 RVA: 0x002B4B0C File Offset: 0x002B2D0C
		public static FormulaExpression Divide(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new SqlDivide(left, right);
			}
			return left;
		}

		// Token: 0x0600CAC6 RID: 51910 RVA: 0x002B4B3D File Offset: 0x002B2D3D
		public static SqlType FloatType()
		{
			return new SqlFloatType();
		}

		// Token: 0x0600CAC7 RID: 51911 RVA: 0x002B4B44 File Offset: 0x002B2D44
		public static FormulaExpression Format(FormulaExpression value, FormulaExpression format, FormulaExpression locale)
		{
			return SqlExpressionHelper.Func("Format", new FormulaExpression[] { value, format, locale });
		}

		// Token: 0x0600CAC8 RID: 51912 RVA: 0x002B4B62 File Offset: 0x002B2D62
		public static FormulaExpression Func(string name, params FormulaExpression[] arguments)
		{
			return new SqlFunc(name, arguments);
		}

		// Token: 0x0600CAC9 RID: 51913 RVA: 0x002B4B6B File Offset: 0x002B2D6B
		public static FormulaExpression Func(string name, IEnumerable<FormulaExpression> arguments)
		{
			return new SqlFunc(name, arguments);
		}

		// Token: 0x0600CACA RID: 51914 RVA: 0x002B4B74 File Offset: 0x002B2D74
		public static FormulaExpression Func(string name, string argumentSeparator, params FormulaExpression[] arguments)
		{
			return new SqlFunc(name, argumentSeparator, arguments);
		}

		// Token: 0x0600CACB RID: 51915 RVA: 0x002B4B7E File Offset: 0x002B2D7E
		public static FormulaExpression Func(string name, string argumentSeparator, IEnumerable<FormulaExpression> arguments)
		{
			return new SqlFunc(name, argumentSeparator, arguments);
		}

		// Token: 0x0600CACC RID: 51916 RVA: 0x002B4B88 File Offset: 0x002B2D88
		public static SqlType IntType()
		{
			return new SqlIntType();
		}

		// Token: 0x0600CACD RID: 51917 RVA: 0x002B4B8F File Offset: 0x002B2D8F
		public static FormulaExpression Left(FormulaExpression str, FormulaExpression numChars)
		{
			return SqlExpressionHelper.Func("Left", new FormulaExpression[] { str, numChars });
		}

		// Token: 0x0600CACE RID: 51918 RVA: 0x002B4BA9 File Offset: 0x002B2DA9
		public static FormulaExpression Lower(FormulaExpression str)
		{
			return SqlExpressionHelper.Func("Lower", new FormulaExpression[] { str });
		}

		// Token: 0x0600CACF RID: 51919 RVA: 0x002B4BC0 File Offset: 0x002B2DC0
		public static FormulaExpression Minus(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral != null)
			{
				if (formulaNumberLiteral.Value == 0.0)
				{
					return left;
				}
				FormulaNumberLiteral formulaNumberLiteral2 = left as FormulaNumberLiteral;
				if (formulaNumberLiteral2 != null)
				{
					return SqlExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value - formulaNumberLiteral.Value);
				}
				if (formulaNumberLiteral.Value < 0.0)
				{
					return SqlExpressionHelper.Plus(left, SqlExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
				}
				FormulaPlus formulaPlus = left as FormulaPlus;
				if (formulaPlus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral3 != null)
					{
						return SqlExpressionHelper.Plus(formulaPlus.Left, SqlExpressionHelper.Minus(formulaNumberLiteral3, formulaNumberLiteral));
					}
				}
				FormulaMinus formulaMinus = left as FormulaMinus;
				if (formulaMinus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral4 != null)
					{
						return SqlExpressionHelper.Minus(formulaMinus.Left, SqlExpressionHelper.Plus(formulaNumberLiteral4, formulaNumberLiteral));
					}
				}
			}
			return new SqlMinus(left, right);
		}

		// Token: 0x0600CAD0 RID: 51920 RVA: 0x002B4C94 File Offset: 0x002B2E94
		public static FormulaExpression Minus1(FormulaExpression val)
		{
			return SqlExpressionHelper.Minus(val, SqlExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600CAD1 RID: 51921 RVA: 0x002B4CA2 File Offset: 0x002B2EA2
		public static SqlType MoneyType()
		{
			return new SqlMoneyType();
		}

		// Token: 0x0600CAD2 RID: 51922 RVA: 0x002B4CAC File Offset: 0x002B2EAC
		public static FormulaExpression Multiply(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral != null && formulaNumberLiteral.Value == 1.0)
			{
				return left;
			}
			formulaNumberLiteral = left as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new SqlMultiply(left, right);
			}
			return right;
		}

		// Token: 0x0600CAD3 RID: 51923 RVA: 0x002B4CFA File Offset: 0x002B2EFA
		public static FormulaExpression NumberLiteral(int value)
		{
			return SqlExpressionHelper.NumberLiteral(Convert.ToDecimal(value));
		}

		// Token: 0x0600CAD4 RID: 51924 RVA: 0x002B4D07 File Offset: 0x002B2F07
		public static FormulaExpression NumberLiteral(decimal value)
		{
			return SqlExpressionHelper.NumberLiteral(Convert.ToDouble(value));
		}

		// Token: 0x0600CAD5 RID: 51925 RVA: 0x002B4D14 File Offset: 0x002B2F14
		public static FormulaExpression NumberLiteral(double value)
		{
			return new SqlNumberLiteral(value);
		}

		// Token: 0x0600CAD6 RID: 51926 RVA: 0x002B4D1C File Offset: 0x002B2F1C
		public static FormulaExpression Plus(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral != null)
			{
				if (formulaNumberLiteral.Value == 0.0)
				{
					return left;
				}
				FormulaNumberLiteral formulaNumberLiteral2 = left as FormulaNumberLiteral;
				if (formulaNumberLiteral2 != null)
				{
					if (formulaNumberLiteral2.Value != 0.0)
					{
						return SqlExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value + formulaNumberLiteral.Value);
					}
					return right;
				}
				else
				{
					if (formulaNumberLiteral.Value < 0.0)
					{
						return SqlExpressionHelper.Minus(left, SqlExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
					}
					FormulaPlus formulaPlus = left as FormulaPlus;
					if (formulaPlus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral3 != null)
						{
							return SqlExpressionHelper.Plus(formulaPlus.Left, SqlExpressionHelper.Plus(formulaNumberLiteral3, formulaNumberLiteral));
						}
					}
					FormulaMinus formulaMinus = left as FormulaMinus;
					if (formulaMinus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral4 != null)
						{
							return SqlExpressionHelper.Minus(formulaMinus.Left, SqlExpressionHelper.Minus(formulaNumberLiteral4, formulaNumberLiteral));
						}
					}
				}
			}
			return new SqlPlus(left, right);
		}

		// Token: 0x0600CAD7 RID: 51927 RVA: 0x002B4E03 File Offset: 0x002B3003
		public static FormulaExpression Plus1(FormulaExpression val)
		{
			return SqlExpressionHelper.Plus(val, SqlExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600CAD8 RID: 51928 RVA: 0x002B4E14 File Offset: 0x002B3014
		public static FormulaExpression Product(IEnumerable<FormulaExpression> arguments)
		{
			IReadOnlyList<FormulaExpression> readOnlyList = arguments.ToReadOnlyList<FormulaExpression>();
			if (readOnlyList.Count < 2)
			{
				throw new Exception(string.Format("Too few arguments for Product() {0}", readOnlyList.Count));
			}
			IEnumerable<FormulaExpression> enumerable = readOnlyList.Skip(2);
			FormulaExpression formulaExpression = SqlExpressionHelper.Multiply(readOnlyList[0], readOnlyList[1]);
			Func<FormulaExpression, FormulaExpression, FormulaExpression> func;
			if ((func = SqlExpressionHelper.<>O.<0>__Multiply) == null)
			{
				func = (SqlExpressionHelper.<>O.<0>__Multiply = new Func<FormulaExpression, FormulaExpression, FormulaExpression>(SqlExpressionHelper.Multiply));
			}
			return enumerable.Aggregate(formulaExpression, func);
		}

		// Token: 0x0600CAD9 RID: 51929 RVA: 0x002B4E86 File Offset: 0x002B3086
		public static FormulaExpression Replace(FormulaExpression source, FormulaExpression findText, FormulaExpression replaceText)
		{
			return SqlExpressionHelper.Func("Replace", new FormulaExpression[] { source, findText, replaceText });
		}

		// Token: 0x0600CADA RID: 51930 RVA: 0x002B4EA4 File Offset: 0x002B30A4
		public static FormulaExpression Reverse(FormulaExpression subject)
		{
			return SqlExpressionHelper.Func("Reverse", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CADB RID: 51931 RVA: 0x002B4EBA File Offset: 0x002B30BA
		public static FormulaExpression Right(FormulaExpression str, FormulaExpression numChars)
		{
			return SqlExpressionHelper.Func("Right", new FormulaExpression[] { str, numChars });
		}

		// Token: 0x0600CADC RID: 51932 RVA: 0x002B4ED4 File Offset: 0x002B30D4
		public static FormulaExpression StringLiteral(string value)
		{
			return new SqlStringLiteral(value);
		}

		// Token: 0x0600CADD RID: 51933 RVA: 0x002B4EDC File Offset: 0x002B30DC
		public static int? StringToCharCode(string s)
		{
			int? num;
			if (s == "\n")
			{
				num = new int?(10);
			}
			else
			{
				num = null;
			}
			return num;
		}

		// Token: 0x0600CADE RID: 51934 RVA: 0x002B4F0B File Offset: 0x002B310B
		public static SqlType StringType()
		{
			return new SqlStringType();
		}

		// Token: 0x0600CADF RID: 51935 RVA: 0x002B4F12 File Offset: 0x002B3112
		public static FormulaExpression Substring(FormulaExpression str, FormulaExpression start, FormulaExpression length)
		{
			return SqlExpressionHelper.Func("Substring", new FormulaExpression[] { str, start, length });
		}

		// Token: 0x0600CAE0 RID: 51936 RVA: 0x002B4F30 File Offset: 0x002B3130
		public static FormulaExpression Sum(IEnumerable<FormulaExpression> arguments)
		{
			IReadOnlyList<FormulaExpression> readOnlyList = arguments.ToReadOnlyList<FormulaExpression>();
			if (readOnlyList.Count < 2)
			{
				throw new Exception(string.Format("Too few arguments for Product() {0}", readOnlyList.Count));
			}
			IEnumerable<FormulaExpression> enumerable = readOnlyList.Skip(2);
			FormulaExpression formulaExpression = SqlExpressionHelper.Plus(readOnlyList[0], readOnlyList[1]);
			Func<FormulaExpression, FormulaExpression, FormulaExpression> func;
			if ((func = SqlExpressionHelper.<>O.<1>__Plus) == null)
			{
				func = (SqlExpressionHelper.<>O.<1>__Plus = new Func<FormulaExpression, FormulaExpression, FormulaExpression>(SqlExpressionHelper.Plus));
			}
			return enumerable.Aggregate(formulaExpression, func);
		}

		// Token: 0x0600CAE1 RID: 51937 RVA: 0x002B4FA2 File Offset: 0x002B31A2
		public static FormulaExpression Trim(FormulaExpression str)
		{
			return SqlExpressionHelper.Func("Trim", new FormulaExpression[] { str });
		}

		// Token: 0x0600CAE2 RID: 51938 RVA: 0x002B4FB8 File Offset: 0x002B31B8
		public static FormulaExpression TryParse(FormulaExpression subject, FormulaExpression typeName, FormulaExpression locale)
		{
			return new SqlTryParse(subject, typeName, locale);
		}

		// Token: 0x0600CAE3 RID: 51939 RVA: 0x002B4FC2 File Offset: 0x002B31C2
		public static SqlType UnresolvedType()
		{
			return new SqlUnresolvedType();
		}

		// Token: 0x0600CAE4 RID: 51940 RVA: 0x002B4FC9 File Offset: 0x002B31C9
		public static FormulaExpression Upper(FormulaExpression str)
		{
			return SqlExpressionHelper.Func("Upper", new FormulaExpression[] { str });
		}

		// Token: 0x0600CAE5 RID: 51941 RVA: 0x002B4FDF File Offset: 0x002B31DF
		public static FormulaExpression Variable(string name)
		{
			return new SqlVariable(name);
		}

		// Token: 0x0600CAE6 RID: 51942 RVA: 0x002B4FE7 File Offset: 0x002B31E7
		public static FormulaExpression Variable(string name, SqlType dataType)
		{
			return new SqlVariable(name, dataType);
		}

		// Token: 0x0200182A RID: 6186
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04004FA3 RID: 20387
			public static Func<FormulaExpression, FormulaExpression, FormulaExpression> <0>__Multiply;

			// Token: 0x04004FA4 RID: 20388
			public static Func<FormulaExpression, FormulaExpression, FormulaExpression> <1>__Plus;
		}
	}
}

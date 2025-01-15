using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018C4 RID: 6340
	internal static class PowerQueryExpressionHelper
	{
		// Token: 0x0600CE95 RID: 52885 RVA: 0x002C0947 File Offset: 0x002BEB47
		public static FormulaExpression And(FormulaExpression left, FormulaExpression right)
		{
			return new PowerQueryAnd(left, right);
		}

		// Token: 0x0600CE96 RID: 52886 RVA: 0x002C0950 File Offset: 0x002BEB50
		public static FormulaExpression BooleanLiteral(bool value)
		{
			return new PowerQueryBooleanLiteral(value);
		}

		// Token: 0x0600CE97 RID: 52887 RVA: 0x002C0958 File Offset: 0x002BEB58
		public static FormulaExpression Column<T>(string tableName, string columnName)
		{
			return new PowerQueryLookup(PowerQueryExpressionHelper.Variable(tableName), columnName, typeof(T));
		}

		// Token: 0x0600CE98 RID: 52888 RVA: 0x002C0970 File Offset: 0x002BEB70
		public static FormulaExpression ColumnLookup<T>(string name)
		{
			return PowerQueryExpressionHelper.ColumnLookup(name, typeof(T));
		}

		// Token: 0x0600CE99 RID: 52889 RVA: 0x002C0982 File Offset: 0x002BEB82
		public static FormulaExpression ColumnLookup(string name, Type type)
		{
			return new PowerQueryLookup(null, name, type);
		}

		// Token: 0x0600CE9A RID: 52890 RVA: 0x002C098C File Offset: 0x002BEB8C
		public static FormulaExpression Concat(FormulaExpression left, FormulaExpression right)
		{
			PowerQueryStringLiteral powerQueryStringLiteral = left as PowerQueryStringLiteral;
			if (powerQueryStringLiteral != null)
			{
				PowerQueryStringLiteral powerQueryStringLiteral2 = right as PowerQueryStringLiteral;
				if (powerQueryStringLiteral2 != null)
				{
					return PowerQueryExpressionHelper.StringLiteral(powerQueryStringLiteral.Value + powerQueryStringLiteral2.Value);
				}
			}
			return new PowerQueryConcat(left, right);
		}

		// Token: 0x0600CE9B RID: 52891 RVA: 0x002C09CB File Offset: 0x002BEBCB
		public static FormulaExpression Contains(FormulaExpression list, FormulaExpression needle)
		{
			return PowerQueryExpressionHelper.Func(MListFunctionName.Contains.QualifiedName, new FormulaExpression[] { list, needle });
		}

		// Token: 0x0600CE9C RID: 52892 RVA: 0x002C09EA File Offset: 0x002BEBEA
		public static FormulaExpression Count(FormulaExpression list)
		{
			return PowerQueryExpressionHelper.Func(MListFunctionName.Count.QualifiedName, new FormulaExpression[] { list });
		}

		// Token: 0x0600CE9D RID: 52893 RVA: 0x002C0A05 File Offset: 0x002BEC05
		public static FormulaExpression DateTimeLiteral(DateTime value)
		{
			return new PowerQueryDateTimeLiteral(value);
		}

		// Token: 0x0600CE9E RID: 52894 RVA: 0x002C0A0D File Offset: 0x002BEC0D
		public static FormulaExpression EndsWith(FormulaExpression subject, FormulaExpression target)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.EndsWith.QualifiedName, new FormulaExpression[] { subject, target });
		}

		// Token: 0x0600CE9F RID: 52895 RVA: 0x002C0A2C File Offset: 0x002BEC2C
		public static FormulaExpression Equal(FormulaExpression left, FormulaExpression right)
		{
			return new PowerQueryEqual(left, right);
		}

		// Token: 0x0600CEA0 RID: 52896 RVA: 0x002C0A35 File Offset: 0x002BEC35
		public static FormulaExpression First(FormulaExpression list)
		{
			return PowerQueryExpressionHelper.Func(MListFunctionName.First.QualifiedName, new FormulaExpression[] { list });
		}

		// Token: 0x0600CEA1 RID: 52897 RVA: 0x002C0A50 File Offset: 0x002BEC50
		public static FormulaExpression FirstCharacter(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Left(subject, 1);
		}

		// Token: 0x0600CEA2 RID: 52898 RVA: 0x002C0A59 File Offset: 0x002BEC59
		public static FormulaExpression FirstN(FormulaExpression list, FormulaExpression count)
		{
			return PowerQueryExpressionHelper.Func(MListFunctionName.FirstN.QualifiedName, new FormulaExpression[] { list, count });
		}

		// Token: 0x0600CEA3 RID: 52899 RVA: 0x002C0A78 File Offset: 0x002BEC78
		public static FormulaExpression Func(string name, params FormulaExpression[] arguments)
		{
			return new PowerQueryFunc(name, arguments);
		}

		// Token: 0x0600CEA4 RID: 52900 RVA: 0x002C0A81 File Offset: 0x002BEC81
		public static FormulaExpression Func(string name, IEnumerable<FormulaExpression> arguments)
		{
			return new PowerQueryFunc(name, arguments);
		}

		// Token: 0x0600CEA5 RID: 52901 RVA: 0x002C0A8A File Offset: 0x002BEC8A
		public static FormulaExpression Func<T>(string name, IEnumerable<FormulaExpression> arguments)
		{
			return new PowerQueryFunc(name, typeof(T), arguments);
		}

		// Token: 0x0600CEA6 RID: 52902 RVA: 0x002C0A9D File Offset: 0x002BEC9D
		public static FormulaExpression Func(string name, Type type, IEnumerable<FormulaExpression> arguments)
		{
			return new PowerQueryFunc(name, type, arguments);
		}

		// Token: 0x0600CEA7 RID: 52903 RVA: 0x002C0AA7 File Offset: 0x002BECA7
		public static FormulaExpression Func(string name, Type type, params FormulaExpression[] arguments)
		{
			return new PowerQueryFunc(name, type, arguments);
		}

		// Token: 0x0600CEA8 RID: 52904 RVA: 0x002C0AB1 File Offset: 0x002BECB1
		public static FormulaExpression If(FormulaExpression conditionExp, FormulaExpression trueExp, FormulaExpression falseExp)
		{
			return new PowerQueryTernary(conditionExp, trueExp, falseExp);
		}

		// Token: 0x0600CEA9 RID: 52905 RVA: 0x002C0ABB File Offset: 0x002BECBB
		public static FormulaExpression Is(FormulaExpression subject, FormulaExpression type)
		{
			return PowerQueryExpressionHelper.Func(MValueFunctionName.Is.QualifiedName, new FormulaExpression[] { subject, type });
		}

		// Token: 0x0600CEAA RID: 52906 RVA: 0x002C0ADA File Offset: 0x002BECDA
		public static FormulaExpression IsDigit(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Contains(PowerQueryConstant.ListOfDigits, subject);
		}

		// Token: 0x0600CEAB RID: 52907 RVA: 0x002C0AE7 File Offset: 0x002BECE7
		public static FormulaExpression IsLetter(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Contains(PowerQueryConstant.ListOfLetters, subject);
		}

		// Token: 0x0600CEAC RID: 52908 RVA: 0x002C0AF4 File Offset: 0x002BECF4
		public static FormulaExpression IsNumber(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func(MValueFunctionName.Is.QualifiedName, new FormulaExpression[]
			{
				subject,
				PowerQueryExpressionHelper.Variable("Number.Type")
			});
		}

		// Token: 0x0600CEAD RID: 52909 RVA: 0x002C0B1C File Offset: 0x002BED1C
		public static FormulaExpression IsText(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func(MValueFunctionName.Is.QualifiedName, new FormulaExpression[]
			{
				subject,
				PowerQueryExpressionHelper.Variable("Text.Type")
			});
		}

		// Token: 0x0600CEAE RID: 52910 RVA: 0x002C0B44 File Offset: 0x002BED44
		public static FormulaExpression Lambda(IReadOnlyList<FormulaExpression> parameters, FormulaExpression body)
		{
			return new PowerQueryLambdaFunction(parameters, body);
		}

		// Token: 0x0600CEAF RID: 52911 RVA: 0x002C0B4D File Offset: 0x002BED4D
		public static FormulaExpression Last(FormulaExpression list)
		{
			return PowerQueryExpressionHelper.Func(MListFunctionName.Last.QualifiedName, new FormulaExpression[] { list });
		}

		// Token: 0x0600CEB0 RID: 52912 RVA: 0x002C0B68 File Offset: 0x002BED68
		public static FormulaExpression LastCharacter(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Right(subject, 1);
		}

		// Token: 0x0600CEB1 RID: 52913 RVA: 0x002C0B71 File Offset: 0x002BED71
		public static FormulaExpression LastN(FormulaExpression list, FormulaExpression num)
		{
			return PowerQueryExpressionHelper.Func(MListFunctionName.LastN.QualifiedName, new FormulaExpression[] { list, num });
		}

		// Token: 0x0600CEB2 RID: 52914 RVA: 0x002C0B90 File Offset: 0x002BED90
		public static FormulaExpression Left(FormulaExpression subject, int length)
		{
			return PowerQueryExpressionHelper.Left(subject, PowerQueryExpressionHelper.NumberLiteral(length));
		}

		// Token: 0x0600CEB3 RID: 52915 RVA: 0x002C0B9E File Offset: 0x002BED9E
		public static FormulaExpression Left(FormulaExpression subject, FormulaExpression length)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.Start.QualifiedName, new FormulaExpression[] { subject, length });
		}

		// Token: 0x0600CEB4 RID: 52916 RVA: 0x002C0BC0 File Offset: 0x002BEDC0
		public static FormulaExpression Len(FormulaExpression subject)
		{
			PowerQueryStringLiteral powerQueryStringLiteral = subject as PowerQueryStringLiteral;
			if (powerQueryStringLiteral == null)
			{
				return PowerQueryExpressionHelper.Func(MTextFunctionName.Length.QualifiedName, new FormulaExpression[] { subject });
			}
			return PowerQueryExpressionHelper.NumberLiteral(powerQueryStringLiteral.Value.Length);
		}

		// Token: 0x0600CEB5 RID: 52917 RVA: 0x002C0C01 File Offset: 0x002BEE01
		public static FormulaExpression List(params FormulaExpression[] items)
		{
			return new PowerQueryList(items);
		}

		// Token: 0x0600CEB6 RID: 52918 RVA: 0x002C0C09 File Offset: 0x002BEE09
		public static FormulaExpression Locale(string locale)
		{
			return new PowerQueryLocale(locale);
		}

		// Token: 0x0600CEB7 RID: 52919 RVA: 0x002C0C11 File Offset: 0x002BEE11
		public static FormulaExpression Lower(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.Lower.QualifiedName, new FormulaExpression[] { subject });
		}

		// Token: 0x0600CEB8 RID: 52920 RVA: 0x002C0C2C File Offset: 0x002BEE2C
		public static FormulaExpression Mid(FormulaExpression subject, FormulaExpression start, FormulaExpression length = null)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.Middle.QualifiedName, new FormulaExpression[] { subject, start, length });
		}

		// Token: 0x0600CEB9 RID: 52921 RVA: 0x002C0C4F File Offset: 0x002BEE4F
		public static FormulaExpression NotEqual(FormulaExpression left, FormulaExpression right)
		{
			return new PowerQueryNotEqual(left, right);
		}

		// Token: 0x0600CEBA RID: 52922 RVA: 0x002C0C58 File Offset: 0x002BEE58
		public static FormulaExpression Nth(FormulaExpression list, FormulaExpression n)
		{
			return new PowerQueryItemAccess(list, n);
		}

		// Token: 0x0600CEBB RID: 52923 RVA: 0x002C0C61 File Offset: 0x002BEE61
		public static FormulaExpression NthFromEnd(FormulaExpression list, FormulaExpression n)
		{
			return PowerQueryExpressionHelper.First(PowerQueryExpressionHelper.LastN(list, n));
		}

		// Token: 0x0600CEBC RID: 52924 RVA: 0x002C0C6F File Offset: 0x002BEE6F
		public static FormulaExpression Null()
		{
			return new PowerQueryNullLiteral();
		}

		// Token: 0x0600CEBD RID: 52925 RVA: 0x002C0C76 File Offset: 0x002BEE76
		public static FormulaExpression NumberLiteral(int value)
		{
			return PowerQueryExpressionHelper.NumberLiteral(Convert.ToDecimal(value));
		}

		// Token: 0x0600CEBE RID: 52926 RVA: 0x002C0C83 File Offset: 0x002BEE83
		public static FormulaExpression NumberLiteral(decimal value)
		{
			return PowerQueryExpressionHelper.NumberLiteral(Convert.ToDouble(value));
		}

		// Token: 0x0600CEBF RID: 52927 RVA: 0x002C0C90 File Offset: 0x002BEE90
		public static FormulaExpression NumberLiteral(double value)
		{
			return new PowerQueryNumberLiteral(value);
		}

		// Token: 0x0600CEC0 RID: 52928 RVA: 0x002C0C98 File Offset: 0x002BEE98
		public static FormulaExpression Or(FormulaExpression left, FormulaExpression right)
		{
			return new PowerQueryOr(left, right);
		}

		// Token: 0x0600CEC1 RID: 52929 RVA: 0x002C0CA1 File Offset: 0x002BEEA1
		public static FormulaExpression Parameter(string name, string type)
		{
			return new PowerQueryFunctionParameter(name, type);
		}

		// Token: 0x0600CEC2 RID: 52930 RVA: 0x002C0CAA File Offset: 0x002BEEAA
		public static FormulaExpression Parenthesis(FormulaExpression body)
		{
			return new PowerQueryParenthesis(body);
		}

		// Token: 0x0600CEC3 RID: 52931 RVA: 0x002C0CB2 File Offset: 0x002BEEB2
		public static FormulaExpression Proper(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.Proper.QualifiedName, new FormulaExpression[] { subject });
		}

		// Token: 0x0600CEC4 RID: 52932 RVA: 0x002C0CCD File Offset: 0x002BEECD
		public static FormulaExpression Replace(FormulaExpression subject, FormulaExpression findText, FormulaExpression replaceText)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.Replace.QualifiedName, new FormulaExpression[] { subject, findText, replaceText });
		}

		// Token: 0x0600CEC5 RID: 52933 RVA: 0x002C0CF0 File Offset: 0x002BEEF0
		public static FormulaExpression Right(FormulaExpression subject, int length)
		{
			return PowerQueryExpressionHelper.Right(subject, PowerQueryExpressionHelper.NumberLiteral(length));
		}

		// Token: 0x0600CEC6 RID: 52934 RVA: 0x002C0CFE File Offset: 0x002BEEFE
		public static FormulaExpression Right(FormulaExpression subject, FormulaExpression length)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.End.QualifiedName, new FormulaExpression[] { subject, length });
		}

		// Token: 0x0600CEC7 RID: 52935 RVA: 0x002C0D1D File Offset: 0x002BEF1D
		public static FormulaExpression Split(FormulaExpression subject, FormulaExpression delimiter)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.Split.QualifiedName, new FormulaExpression[] { subject, delimiter });
		}

		// Token: 0x0600CEC8 RID: 52936 RVA: 0x002C0D3C File Offset: 0x002BEF3C
		public static FormulaExpression StartsWith(FormulaExpression subject, FormulaExpression target)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.StartsWith.QualifiedName, new FormulaExpression[] { subject, target });
		}

		// Token: 0x0600CEC9 RID: 52937 RVA: 0x002C0D5B File Offset: 0x002BEF5B
		public static FormulaExpression StringLiteral(string value)
		{
			return new PowerQueryStringLiteral(value);
		}

		// Token: 0x0600CECA RID: 52938 RVA: 0x002C0D63 File Offset: 0x002BEF63
		public static FormulaExpression Trim(FormulaExpression value)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.Trim.QualifiedName, new FormulaExpression[] { value });
		}

		// Token: 0x0600CECB RID: 52939 RVA: 0x002C0D7E File Offset: 0x002BEF7E
		public static FormulaExpression Upper(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func(MTextFunctionName.Upper.QualifiedName, new FormulaExpression[] { subject });
		}

		// Token: 0x0600CECC RID: 52940 RVA: 0x002C0D99 File Offset: 0x002BEF99
		public static FormulaExpression Variable(string name)
		{
			return new PowerQueryVariable(name);
		}

		// Token: 0x0600CECD RID: 52941 RVA: 0x002C0DA1 File Offset: 0x002BEFA1
		public static FormulaExpression With(IReadOnlyList<IReadOnlyDictionary<string, FormulaExpression>> records, FormulaExpression body)
		{
			return records.Reverse<IReadOnlyDictionary<string, FormulaExpression>>().Aggregate(body, (FormulaExpression acc, IReadOnlyDictionary<string, FormulaExpression> record) => PowerQueryExpressionHelper.With(record, acc));
		}

		// Token: 0x0600CECE RID: 52942 RVA: 0x002C0DCE File Offset: 0x002BEFCE
		public static FormulaExpression With(IReadOnlyDictionary<string, FormulaExpression> record, FormulaExpression body)
		{
			return new PowerQueryLet(record.Select2((string name, FormulaExpression expression) => new PowerQueryStep(name, expression, false)), body, null);
		}

		// Token: 0x0600CECF RID: 52943 RVA: 0x002C0DFC File Offset: 0x002BEFFC
		public static FormulaExpression Round(FormulaExpression value, FormulaExpression decimals)
		{
			string qualifiedName = MNumberFunctionName.Round.QualifiedName;
			IFormulaTyped formulaTyped = value as IFormulaTyped;
			return PowerQueryExpressionHelper.Func(qualifiedName, (formulaTyped != null) ? formulaTyped.Type : null, new FormulaExpression[]
			{
				value,
				decimals,
				PowerQueryConstant.RoundingModeAwayFromZero
			});
		}

		// Token: 0x0600CED0 RID: 52944 RVA: 0x002C0E35 File Offset: 0x002BF035
		public static FormulaExpression RoundDown(FormulaExpression value, int decimals)
		{
			return PowerQueryExpressionHelper.RoundDown(value, PowerQueryExpressionHelper.NumberLiteral(decimals));
		}

		// Token: 0x0600CED1 RID: 52945 RVA: 0x002C0E43 File Offset: 0x002BF043
		public static FormulaExpression RoundDown(FormulaExpression value, FormulaExpression decimals = null)
		{
			string qualifiedName = MNumberFunctionName.RoundDown.QualifiedName;
			IFormulaTyped formulaTyped = value as IFormulaTyped;
			return PowerQueryExpressionHelper.Func(qualifiedName, (formulaTyped != null) ? formulaTyped.Type : null, new FormulaExpression[] { value, decimals });
		}

		// Token: 0x0600CED2 RID: 52946 RVA: 0x002C0E74 File Offset: 0x002BF074
		public static FormulaExpression RoundUp(FormulaExpression value, int decimals)
		{
			return PowerQueryExpressionHelper.RoundUp(value, PowerQueryExpressionHelper.NumberLiteral(decimals));
		}

		// Token: 0x0600CED3 RID: 52947 RVA: 0x002C0E82 File Offset: 0x002BF082
		public static FormulaExpression RoundUp(FormulaExpression value, FormulaExpression decimals)
		{
			string qualifiedName = MNumberFunctionName.RoundUp.QualifiedName;
			IFormulaTyped formulaTyped = value as IFormulaTyped;
			return PowerQueryExpressionHelper.Func(qualifiedName, (formulaTyped != null) ? formulaTyped.Type : null, new FormulaExpression[] { value, decimals });
		}

		// Token: 0x0600CED4 RID: 52948 RVA: 0x002C0EB3 File Offset: 0x002BF0B3
		public static FormulaExpression RoundUp(FormulaExpression value)
		{
			string qualifiedName = MNumberFunctionName.RoundUp.QualifiedName;
			IFormulaTyped formulaTyped = value as IFormulaTyped;
			return PowerQueryExpressionHelper.Func(qualifiedName, (formulaTyped != null) ? formulaTyped.Type : null, new FormulaExpression[] { value });
		}

		// Token: 0x0600CED5 RID: 52949 RVA: 0x002C0EE0 File Offset: 0x002BF0E0
		public static FormulaExpression Divide(double left, FormulaExpression right)
		{
			return PowerQueryExpressionHelper.Divide(PowerQueryExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600CED6 RID: 52950 RVA: 0x002C0EEE File Offset: 0x002BF0EE
		public static FormulaExpression Divide(FormulaExpression left, int right)
		{
			return PowerQueryExpressionHelper.Divide(left, PowerQueryExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CED7 RID: 52951 RVA: 0x002C0EFC File Offset: 0x002BF0FC
		public static FormulaExpression Sum(IEnumerable<FormulaExpression> items)
		{
			items = items.ToReadOnlyList<FormulaExpression>();
			IEnumerable<FormulaExpression> enumerable = items.Skip(1);
			FormulaExpression formulaExpression = items.First<FormulaExpression>();
			Func<FormulaExpression, FormulaExpression, FormulaExpression> func;
			if ((func = PowerQueryExpressionHelper.<>O.<0>__Plus) == null)
			{
				func = (PowerQueryExpressionHelper.<>O.<0>__Plus = new Func<FormulaExpression, FormulaExpression, FormulaExpression>(PowerQueryExpressionHelper.Plus));
			}
			return enumerable.Aggregate(formulaExpression, func);
		}

		// Token: 0x0600CED8 RID: 52952 RVA: 0x002C0F33 File Offset: 0x002BF133
		public static FormulaExpression Product(IEnumerable<FormulaExpression> items)
		{
			items = items.ToReadOnlyList<FormulaExpression>();
			IEnumerable<FormulaExpression> enumerable = items.Skip(1);
			FormulaExpression formulaExpression = items.First<FormulaExpression>();
			Func<FormulaExpression, FormulaExpression, FormulaExpression> func;
			if ((func = PowerQueryExpressionHelper.<>O.<1>__Multiply) == null)
			{
				func = (PowerQueryExpressionHelper.<>O.<1>__Multiply = new Func<FormulaExpression, FormulaExpression, FormulaExpression>(PowerQueryExpressionHelper.Multiply));
			}
			return enumerable.Aggregate(formulaExpression, func);
		}

		// Token: 0x0600CED9 RID: 52953 RVA: 0x002C0F6C File Offset: 0x002BF16C
		public static FormulaExpression Average(IEnumerable<FormulaExpression> items)
		{
			items = items.ToReadOnlyList<FormulaExpression>();
			IEnumerable<FormulaExpression> enumerable = items.Skip(1);
			FormulaExpression formulaExpression = items.First<FormulaExpression>();
			Func<FormulaExpression, FormulaExpression, FormulaExpression> func;
			if ((func = PowerQueryExpressionHelper.<>O.<0>__Plus) == null)
			{
				func = (PowerQueryExpressionHelper.<>O.<0>__Plus = new Func<FormulaExpression, FormulaExpression, FormulaExpression>(PowerQueryExpressionHelper.Plus));
			}
			return PowerQueryExpressionHelper.Divide(enumerable.Aggregate(formulaExpression, func), items.Count<FormulaExpression>());
		}

		// Token: 0x0600CEDA RID: 52954 RVA: 0x002C0FBC File Offset: 0x002BF1BC
		public static FormulaExpression Divide(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new PowerQueryDivide(left, right);
			}
			return left;
		}

		// Token: 0x0600CEDB RID: 52955 RVA: 0x002C0FED File Offset: 0x002BF1ED
		public static FormulaExpression Minus(FormulaExpression subject)
		{
			return new PowerQueryUnaryMinus(subject);
		}

		// Token: 0x0600CEDC RID: 52956 RVA: 0x002C0FF5 File Offset: 0x002BF1F5
		public static FormulaExpression Minus(double left, FormulaExpression right)
		{
			return PowerQueryExpressionHelper.Minus(PowerQueryExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600CEDD RID: 52957 RVA: 0x002C1003 File Offset: 0x002BF203
		public static FormulaExpression Minus(FormulaExpression left, double right)
		{
			return PowerQueryExpressionHelper.Minus(left, PowerQueryExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CEDE RID: 52958 RVA: 0x002C1014 File Offset: 0x002BF214
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
					return PowerQueryExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value - formulaNumberLiteral.Value);
				}
				if (formulaNumberLiteral.Value < 0.0)
				{
					return PowerQueryExpressionHelper.Plus(left, PowerQueryExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
				}
				FormulaPlus formulaPlus = left as FormulaPlus;
				if (formulaPlus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral3 != null)
					{
						return PowerQueryExpressionHelper.Plus(formulaPlus.Left, PowerQueryExpressionHelper.Minus(formulaNumberLiteral3, formulaNumberLiteral));
					}
				}
				FormulaMinus formulaMinus = left as FormulaMinus;
				if (formulaMinus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral4 != null)
					{
						return PowerQueryExpressionHelper.Minus(formulaMinus.Left, PowerQueryExpressionHelper.Plus(formulaNumberLiteral4, formulaNumberLiteral));
					}
				}
			}
			return new PowerQueryMinus(left, right);
		}

		// Token: 0x0600CEDF RID: 52959 RVA: 0x002C10E8 File Offset: 0x002BF2E8
		public static FormulaExpression Minus1(FormulaExpression val)
		{
			return PowerQueryExpressionHelper.Minus(val, PowerQueryExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600CEE0 RID: 52960 RVA: 0x002C10F6 File Offset: 0x002BF2F6
		public static FormulaExpression Multiply(double left, FormulaExpression right)
		{
			return PowerQueryExpressionHelper.Multiply(PowerQueryExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600CEE1 RID: 52961 RVA: 0x002C1104 File Offset: 0x002BF304
		public static FormulaExpression Multiply(FormulaExpression left, double right)
		{
			return PowerQueryExpressionHelper.Multiply(left, PowerQueryExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CEE2 RID: 52962 RVA: 0x002C1114 File Offset: 0x002BF314
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
				return new PowerQueryMultiply(left, right);
			}
			return right;
		}

		// Token: 0x0600CEE3 RID: 52963 RVA: 0x002C1162 File Offset: 0x002BF362
		public static FormulaExpression Plus(double left, FormulaExpression right)
		{
			return PowerQueryExpressionHelper.Plus(PowerQueryExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600CEE4 RID: 52964 RVA: 0x002C1170 File Offset: 0x002BF370
		public static FormulaExpression Plus(FormulaExpression left, double right)
		{
			return PowerQueryExpressionHelper.Plus(left, PowerQueryExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600CEE5 RID: 52965 RVA: 0x002C1180 File Offset: 0x002BF380
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
						return PowerQueryExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value + formulaNumberLiteral.Value);
					}
					return right;
				}
				else
				{
					if (formulaNumberLiteral.Value < 0.0)
					{
						return PowerQueryExpressionHelper.Minus(left, PowerQueryExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
					}
					FormulaPlus formulaPlus = left as FormulaPlus;
					if (formulaPlus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral3 != null)
						{
							return PowerQueryExpressionHelper.Plus(formulaPlus.Left, PowerQueryExpressionHelper.Plus(formulaNumberLiteral3, formulaNumberLiteral));
						}
					}
					FormulaMinus formulaMinus = left as FormulaMinus;
					if (formulaMinus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral4 != null)
						{
							return PowerQueryExpressionHelper.Minus(formulaMinus.Left, PowerQueryExpressionHelper.Minus(formulaNumberLiteral4, formulaNumberLiteral));
						}
					}
				}
			}
			return new PowerQueryPlus(left, right);
		}

		// Token: 0x0600CEE6 RID: 52966 RVA: 0x002C1267 File Offset: 0x002BF467
		public static FormulaExpression Plus1(FormulaExpression val)
		{
			return PowerQueryExpressionHelper.Plus(val, PowerQueryExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600CEE7 RID: 52967 RVA: 0x002C1275 File Offset: 0x002BF475
		public static FormulaExpression GreaterThan(FormulaExpression left, FormulaExpression right)
		{
			return new PowerQueryGreaterThan(left, right);
		}

		// Token: 0x0600CEE8 RID: 52968 RVA: 0x002C127E File Offset: 0x002BF47E
		public static FormulaExpression GreaterThanEqual(FormulaExpression left, FormulaExpression right)
		{
			return new PowerQueryGreaterThanEqual(left, right);
		}

		// Token: 0x0600CEE9 RID: 52969 RVA: 0x002C1287 File Offset: 0x002BF487
		public static FormulaExpression LessThan(FormulaExpression left, FormulaExpression right)
		{
			return new PowerQueryLessThan(left, right);
		}

		// Token: 0x0600CEEA RID: 52970 RVA: 0x002C1290 File Offset: 0x002BF490
		public static FormulaExpression LessThanEqual(FormulaExpression left, FormulaExpression right)
		{
			return new PowerQueryLessThanEqual(left, right);
		}

		// Token: 0x0600CEEB RID: 52971 RVA: 0x002C1299 File Offset: 0x002BF499
		public static FormulaExpression Not(FormulaExpression subject)
		{
			return new PowerQueryNot((subject is IFormulaBinaryOperator) ? PowerQueryExpressionHelper.Parenthesis(subject) : subject);
		}

		// Token: 0x0600CEEC RID: 52972 RVA: 0x002C12B1 File Offset: 0x002BF4B1
		public static FormulaExpression Date(FormulaExpression year, FormulaExpression month, FormulaExpression day)
		{
			return PowerQueryExpressionHelper.Func("#date", new FormulaExpression[] { year, month, day });
		}

		// Token: 0x0600CEED RID: 52973 RVA: 0x002C12CF File Offset: 0x002BF4CF
		public static FormulaExpression DateDiffDays(FormulaExpression start, FormulaExpression end)
		{
			return PowerQueryExpressionHelper.Func("Duration.Days", new FormulaExpression[] { PowerQueryExpressionHelper.Minus(end, start) });
		}

		// Token: 0x0600CEEE RID: 52974 RVA: 0x002C12EB File Offset: 0x002BF4EB
		public static FormulaExpression DateTime(int year, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0)
		{
			return PowerQueryExpressionHelper.DateTime(PowerQueryExpressionHelper.NumberLiteral(year), PowerQueryExpressionHelper.NumberLiteral(month), PowerQueryExpressionHelper.NumberLiteral(day), PowerQueryExpressionHelper.NumberLiteral(hour), PowerQueryExpressionHelper.NumberLiteral(minute), PowerQueryExpressionHelper.NumberLiteral(second));
		}

		// Token: 0x0600CEEF RID: 52975 RVA: 0x002C1318 File Offset: 0x002BF518
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day, FormulaExpression hour = null, FormulaExpression minute = null, FormulaExpression second = null)
		{
			return PowerQueryExpressionHelper.Func("#datetime", new FormulaExpression[]
			{
				year,
				month,
				day,
				hour ?? PowerQueryExpressionHelper.NumberLiteral(0),
				minute ?? PowerQueryExpressionHelper.NumberLiteral(0),
				second ?? PowerQueryExpressionHelper.NumberLiteral(0)
			});
		}

		// Token: 0x0600CEF0 RID: 52976 RVA: 0x002C136D File Offset: 0x002BF56D
		public static FormulaExpression Day(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.Day", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CEF1 RID: 52977 RVA: 0x002C1383 File Offset: 0x002BF583
		public static FormulaExpression DayEnd(FormulaExpression dayStart)
		{
			return PowerQueryExpressionHelper.Func("Date.AddDays", new FormulaExpression[]
			{
				dayStart,
				PowerQueryExpressionHelper.NumberLiteral(1)
			});
		}

		// Token: 0x0600CEF2 RID: 52978 RVA: 0x002C13A2 File Offset: 0x002BF5A2
		public static FormulaExpression DayStart(FormulaExpression subject, bool includeTime = false)
		{
			if (!includeTime)
			{
				return PowerQueryExpressionHelper.Date(PowerQueryExpressionHelper.Year(subject), PowerQueryExpressionHelper.Month(subject), PowerQueryExpressionHelper.Day(subject));
			}
			return PowerQueryExpressionHelper.DateTime(PowerQueryExpressionHelper.Year(subject), PowerQueryExpressionHelper.Month(subject), PowerQueryExpressionHelper.Day(subject), null, null, null);
		}

		// Token: 0x0600CEF3 RID: 52979 RVA: 0x002C13D9 File Offset: 0x002BF5D9
		public static FormulaExpression Duration(int days = 0, int hours = 0, int minutes = 0, int seconds = 0)
		{
			return PowerQueryExpressionHelper.Func("#duration", new FormulaExpression[]
			{
				PowerQueryExpressionHelper.NumberLiteral(days),
				PowerQueryExpressionHelper.NumberLiteral(hours),
				PowerQueryExpressionHelper.NumberLiteral(minutes),
				PowerQueryExpressionHelper.NumberLiteral(seconds)
			});
		}

		// Token: 0x0600CEF4 RID: 52980 RVA: 0x002C1410 File Offset: 0x002BF610
		public static FormulaExpression Duration(FormulaExpression days = null, FormulaExpression hours = null, FormulaExpression minutes = null, FormulaExpression seconds = null)
		{
			return PowerQueryExpressionHelper.Func("#duration", new FormulaExpression[]
			{
				days ?? PowerQueryExpressionHelper.NumberLiteral(0),
				hours ?? PowerQueryExpressionHelper.NumberLiteral(0),
				minutes ?? PowerQueryExpressionHelper.NumberLiteral(0),
				seconds ?? PowerQueryExpressionHelper.NumberLiteral(0)
			});
		}

		// Token: 0x0600CEF5 RID: 52981 RVA: 0x002C1465 File Offset: 0x002BF665
		public static FormulaExpression FormatDateTime(FormulaExpression text, FormulaExpression format, FormulaExpression culture)
		{
			string text2 = "DateTime.ToText";
			FormulaExpression[] array = new FormulaExpression[2];
			array[0] = text;
			int num = 1;
			Dictionary<string, FormulaExpression> dictionary = new Dictionary<string, FormulaExpression>();
			dictionary["Format"] = format;
			dictionary["Culture"] = culture;
			array[num] = new PowerQueryRecord(dictionary);
			return PowerQueryExpressionHelper.Func(text2, array);
		}

		// Token: 0x0600CEF6 RID: 52982 RVA: 0x002C14A0 File Offset: 0x002BF6A0
		public static FormulaExpression FormatDateTime(FormulaExpression text, string format, string locale)
		{
			return PowerQueryExpressionHelper.FormatDateTime(text, PowerQueryExpressionHelper.StringLiteral(format), PowerQueryExpressionHelper.Locale(locale));
		}

		// Token: 0x0600CEF7 RID: 52983 RVA: 0x002C14B4 File Offset: 0x002BF6B4
		public static FormulaExpression Hour(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Time.Hour", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CEF8 RID: 52984 RVA: 0x002C14CA File Offset: 0x002BF6CA
		public static FormulaExpression HourEnd(FormulaExpression hourStart)
		{
			return PowerQueryExpressionHelper.Plus(hourStart, PowerQueryExpressionHelper.Duration(0, 1, 0, 0));
		}

		// Token: 0x0600CEF9 RID: 52985 RVA: 0x002C14DB File Offset: 0x002BF6DB
		public static FormulaExpression HourStart(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Time.StartOfHour", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CEFA RID: 52986 RVA: 0x002C14F4 File Offset: 0x002BF6F4
		public static Optional<FormulaExpression> MaybeLiteral(this object obj)
		{
			Optional<FormulaExpression> optional;
			if (obj is decimal)
			{
				decimal num = (decimal)obj;
				optional = PowerQueryExpressionHelper.NumberLiteral(num).Some<FormulaExpression>();
			}
			else if (obj is bool)
			{
				bool flag = (bool)obj;
				optional = PowerQueryExpressionHelper.BooleanLiteral(flag).Some<FormulaExpression>();
			}
			else
			{
				string text = obj as string;
				if (text == null)
				{
					if (obj is DateTime)
					{
						DateTime dateTime = (DateTime)obj;
						optional = PowerQueryExpressionHelper.DateTimeLiteral(dateTime).Some<FormulaExpression>();
					}
					else if (obj != null)
					{
						optional = Optional<FormulaExpression>.Nothing;
					}
					else
					{
						optional = PowerQueryExpressionHelper.Null().Some<FormulaExpression>();
					}
				}
				else
				{
					optional = PowerQueryExpressionHelper.StringLiteral(text).Some<FormulaExpression>();
				}
			}
			return optional;
		}

		// Token: 0x0600CEFB RID: 52987 RVA: 0x002C1596 File Offset: 0x002BF796
		public static FormulaExpression Midpoint(FormulaExpression start, FormulaExpression end)
		{
			return PowerQueryExpressionHelper.Plus(start, PowerQueryExpressionHelper.Divide(PowerQueryExpressionHelper.Minus(end, start), 2));
		}

		// Token: 0x0600CEFC RID: 52988 RVA: 0x002C15AB File Offset: 0x002BF7AB
		public static FormulaExpression Minute(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Time.Minute", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CEFD RID: 52989 RVA: 0x002C15C1 File Offset: 0x002BF7C1
		public static FormulaExpression MinuteEnd(FormulaExpression minuteStart)
		{
			return PowerQueryExpressionHelper.Plus(minuteStart, PowerQueryExpressionHelper.Duration(0, 0, 1, 0));
		}

		// Token: 0x0600CEFE RID: 52990 RVA: 0x002C15D2 File Offset: 0x002BF7D2
		public static FormulaExpression MinuteStart(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.DateTime(PowerQueryExpressionHelper.Year(subject), PowerQueryExpressionHelper.Month(subject), PowerQueryExpressionHelper.Day(subject), PowerQueryExpressionHelper.Hour(subject), PowerQueryExpressionHelper.Minute(subject), null);
		}

		// Token: 0x0600CEFF RID: 52991 RVA: 0x002C15F8 File Offset: 0x002BF7F8
		public static FormulaExpression Month(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.Month", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CF00 RID: 52992 RVA: 0x002C160E File Offset: 0x002BF80E
		public static FormulaExpression MonthDays(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.DaysInMonth", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CF01 RID: 52993 RVA: 0x002C1624 File Offset: 0x002BF824
		public static FormulaExpression MonthEnd(FormulaExpression monthStart)
		{
			return PowerQueryExpressionHelper.Func("Date.AddMonths", new FormulaExpression[]
			{
				monthStart,
				PowerQueryExpressionHelper.NumberLiteral(1)
			});
		}

		// Token: 0x0600CF02 RID: 52994 RVA: 0x002C1643 File Offset: 0x002BF843
		public static FormulaExpression MonthStart(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.DateTime(PowerQueryExpressionHelper.Year(subject), PowerQueryExpressionHelper.Month(subject), PowerQueryExpressionHelper.NumberLiteral(1), null, null, null);
		}

		// Token: 0x0600CF03 RID: 52995 RVA: 0x002C165F File Offset: 0x002BF85F
		public static FormulaExpression MonthWeek(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.WeekOfMonth", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CF04 RID: 52996 RVA: 0x002C1675 File Offset: 0x002BF875
		public static FormulaExpression ParseDateTime(FormulaExpression text, FormulaExpression format, FormulaExpression culture)
		{
			string text2 = "DateTime.FromText";
			FormulaExpression[] array = new FormulaExpression[2];
			array[0] = text;
			int num = 1;
			Dictionary<string, FormulaExpression> dictionary = new Dictionary<string, FormulaExpression>();
			dictionary["Format"] = format;
			dictionary["Culture"] = culture;
			array[num] = new PowerQueryRecord(dictionary);
			return PowerQueryExpressionHelper.Func(text2, array);
		}

		// Token: 0x0600CF05 RID: 52997 RVA: 0x002C16B0 File Offset: 0x002BF8B0
		public static FormulaExpression ParseDateTime(FormulaExpression text, string format, string locale)
		{
			return PowerQueryExpressionHelper.ParseDateTime(text, PowerQueryExpressionHelper.StringLiteral(format), PowerQueryExpressionHelper.Locale(locale));
		}

		// Token: 0x0600CF06 RID: 52998 RVA: 0x002C16C4 File Offset: 0x002BF8C4
		public static FormulaExpression Quarter(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.QuarterOfYear", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CF07 RID: 52999 RVA: 0x002C16DA File Offset: 0x002BF8DA
		public static FormulaExpression QuarterDay(FormulaExpression subject, FormulaExpression quarterStart = null)
		{
			if (quarterStart == null)
			{
				quarterStart = PowerQueryExpressionHelper.QuarterStart(subject);
			}
			return PowerQueryExpressionHelper.Plus1(PowerQueryExpressionHelper.DateDiffDays(quarterStart, subject));
		}

		// Token: 0x0600CF08 RID: 53000 RVA: 0x002C16F4 File Offset: 0x002BF8F4
		public static FormulaExpression QuarterDays(FormulaExpression subject, FormulaExpression quarterStart = null, FormulaExpression quarterEnd = null)
		{
			if (quarterStart == null)
			{
				quarterStart = PowerQueryExpressionHelper.QuarterStart(subject);
			}
			if (quarterEnd == null)
			{
				quarterEnd = PowerQueryExpressionHelper.Func("Date.EndOfQuarter", new FormulaExpression[] { subject });
			}
			return PowerQueryExpressionHelper.RoundUp(PowerQueryExpressionHelper.Func("Duration.TotalDays", new FormulaExpression[] { PowerQueryExpressionHelper.Minus(quarterEnd, quarterStart) }));
		}

		// Token: 0x0600CF09 RID: 53001 RVA: 0x002C1744 File Offset: 0x002BF944
		public static FormulaExpression QuarterEnd(FormulaExpression subject, FormulaExpression quarterStart = null)
		{
			if (quarterStart == null)
			{
				quarterStart = PowerQueryExpressionHelper.QuarterStart(subject);
			}
			return PowerQueryExpressionHelper.Func("Date.AddQuarters", new FormulaExpression[]
			{
				quarterStart,
				PowerQueryExpressionHelper.NumberLiteral(1)
			});
		}

		// Token: 0x0600CF0A RID: 53002 RVA: 0x002C176E File Offset: 0x002BF96E
		public static FormulaExpression QuarterStart(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.StartOfQuarter", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CF0B RID: 53003 RVA: 0x002C1784 File Offset: 0x002BF984
		public static FormulaExpression QuarterWeek(FormulaExpression subject, FormulaExpression quarterStart = null)
		{
			if (quarterStart == null)
			{
				quarterStart = PowerQueryExpressionHelper.QuarterStart(subject);
			}
			return PowerQueryExpressionHelper.RoundUp(PowerQueryExpressionHelper.Divide(PowerQueryExpressionHelper.Plus(PowerQueryExpressionHelper.DateDiffDays(quarterStart, subject), PowerQueryExpressionHelper.Weekday(quarterStart)), 7), 0);
		}

		// Token: 0x0600CF0C RID: 53004 RVA: 0x002C17AF File Offset: 0x002BF9AF
		public static FormulaExpression Second(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.RoundDown(PowerQueryExpressionHelper.Func("Time.Second", new FormulaExpression[] { subject }), null);
		}

		// Token: 0x0600CF0D RID: 53005 RVA: 0x002C17CB File Offset: 0x002BF9CB
		public static FormulaExpression SecondEnd(FormulaExpression secondStart)
		{
			return PowerQueryExpressionHelper.Plus(secondStart, PowerQueryExpressionHelper.Duration(0, 0, 0, 1));
		}

		// Token: 0x0600CF0E RID: 53006 RVA: 0x002C17DC File Offset: 0x002BF9DC
		public static FormulaExpression SecondStart(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.DateTime(PowerQueryExpressionHelper.Year(subject), PowerQueryExpressionHelper.Month(subject), PowerQueryExpressionHelper.Day(subject), PowerQueryExpressionHelper.Hour(subject), PowerQueryExpressionHelper.Minute(subject), PowerQueryExpressionHelper.Second(subject));
		}

		// Token: 0x0600CF0F RID: 53007 RVA: 0x002C1807 File Offset: 0x002BFA07
		public static FormulaExpression Time(FormulaExpression hour, FormulaExpression minute, FormulaExpression second)
		{
			return PowerQueryExpressionHelper.Func("#time", new FormulaExpression[] { hour, minute, second });
		}

		// Token: 0x0600CF10 RID: 53008 RVA: 0x002C1825 File Offset: 0x002BFA25
		public static FormulaExpression Weekday(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Plus1(PowerQueryExpressionHelper.Func("Date.DayOfWeek", new FormulaExpression[] { subject }));
		}

		// Token: 0x0600CF11 RID: 53009 RVA: 0x002C1840 File Offset: 0x002BFA40
		public static FormulaExpression WeekEnd(FormulaExpression weekStart)
		{
			return PowerQueryExpressionHelper.Func("Date.AddWeeks", new FormulaExpression[]
			{
				weekStart,
				PowerQueryExpressionHelper.NumberLiteral(1)
			});
		}

		// Token: 0x0600CF12 RID: 53010 RVA: 0x002C185F File Offset: 0x002BFA5F
		public static FormulaExpression WeekStart(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.StartOfWeek", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CF13 RID: 53011 RVA: 0x002C1875 File Offset: 0x002BFA75
		public static FormulaExpression Year(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.Year", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CF14 RID: 53012 RVA: 0x002C188B File Offset: 0x002BFA8B
		public static FormulaExpression YearDay(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.DayOfYear", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CF15 RID: 53013 RVA: 0x002C18A1 File Offset: 0x002BFAA1
		public static FormulaExpression YearDays(FormulaExpression subject, FormulaExpression yearStart = null, FormulaExpression yearEnd = null)
		{
			if (yearStart == null)
			{
				yearStart = PowerQueryExpressionHelper.YearStart(subject);
			}
			if (yearEnd == null)
			{
				yearEnd = PowerQueryExpressionHelper.Func("Date.EndOfYear", Array.Empty<FormulaExpression>());
			}
			return PowerQueryExpressionHelper.RoundUp(PowerQueryExpressionHelper.Func("Duration.TotalDays", new FormulaExpression[] { PowerQueryExpressionHelper.Minus(yearEnd, yearStart) }));
		}

		// Token: 0x0600CF16 RID: 53014 RVA: 0x002C18E1 File Offset: 0x002BFAE1
		public static FormulaExpression YearEnd(FormulaExpression yearStart)
		{
			return PowerQueryExpressionHelper.Func("Date.AddYears", new FormulaExpression[]
			{
				yearStart,
				PowerQueryExpressionHelper.NumberLiteral(1)
			});
		}

		// Token: 0x0600CF17 RID: 53015 RVA: 0x002C1900 File Offset: 0x002BFB00
		public static FormulaExpression YearStart(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.StartOfYear", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CF18 RID: 53016 RVA: 0x002C1916 File Offset: 0x002BFB16
		public static FormulaExpression YearWeek(FormulaExpression subject)
		{
			return PowerQueryExpressionHelper.Func("Date.WeekOfYear", new FormulaExpression[] { subject });
		}

		// Token: 0x020018C5 RID: 6341
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040050BC RID: 20668
			public static Func<FormulaExpression, FormulaExpression, FormulaExpression> <0>__Plus;

			// Token: 0x040050BD RID: 20669
			public static Func<FormulaExpression, FormulaExpression, FormulaExpression> <1>__Multiply;
		}
	}
}

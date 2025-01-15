using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018F1 RID: 6385
	internal static class PowerFxExpressionHelper
	{
		// Token: 0x0600CFD5 RID: 53205 RVA: 0x002C4704 File Offset: 0x002C2904
		public static FormulaExpression And(FormulaExpression left, FormulaExpression right)
		{
			return new PowerFxAnd(left, right);
		}

		// Token: 0x0600CFD6 RID: 53206 RVA: 0x002C470D File Offset: 0x002C290D
		public static FormulaExpression Or(FormulaExpression left, FormulaExpression right)
		{
			return new PowerFxOr(left, right);
		}

		// Token: 0x0600CFD7 RID: 53207 RVA: 0x002C4716 File Offset: 0x002C2916
		public static FormulaExpression Blank()
		{
			return PowerFxExpressionHelper.Func("Blank", Array.Empty<FormulaExpression>());
		}

		// Token: 0x0600CFD8 RID: 53208 RVA: 0x002C4728 File Offset: 0x002C2928
		public static FormulaExpression Concat(FormulaExpression left, FormulaExpression right)
		{
			PowerFxStringLiteral powerFxStringLiteral = left as PowerFxStringLiteral;
			if (powerFxStringLiteral != null)
			{
				PowerFxStringLiteral powerFxStringLiteral2 = right as PowerFxStringLiteral;
				if (powerFxStringLiteral2 != null)
				{
					return PowerFxExpressionHelper.StringLiteral(powerFxStringLiteral.Value + powerFxStringLiteral2.Value);
				}
			}
			return new PowerFxConcat(left, right);
		}

		// Token: 0x0600CFD9 RID: 53209 RVA: 0x002C4767 File Offset: 0x002C2967
		public static FormulaExpression CountRows(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("CountRows", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CFDA RID: 53210 RVA: 0x002C477D File Offset: 0x002C297D
		public static FormulaExpression DateTimeLiteral(DateTime value)
		{
			return new PowerFxDateTimeLiteral(value);
		}

		// Token: 0x0600CFDB RID: 53211 RVA: 0x002C4785 File Offset: 0x002C2985
		public static FormulaExpression DateTimeValue(FormulaExpression value, FormulaExpression locale = null)
		{
			return PowerFxExpressionHelper.Func("DateTimeValue", new FormulaExpression[] { value, locale });
		}

		// Token: 0x0600CFDC RID: 53212 RVA: 0x002C479F File Offset: 0x002C299F
		public static FormulaExpression DateValue(FormulaExpression value, FormulaExpression locale = null)
		{
			return PowerFxExpressionHelper.Func("DateValue", new FormulaExpression[] { value, locale });
		}

		// Token: 0x0600CFDD RID: 53213 RVA: 0x002C47B9 File Offset: 0x002C29B9
		public static FormulaExpression Dot(FormulaExpression subject, string accessor)
		{
			return PowerFxExpressionHelper.Dot(subject, new PowerFxAccessor(accessor));
		}

		// Token: 0x0600CFDE RID: 53214 RVA: 0x002C47C7 File Offset: 0x002C29C7
		public static FormulaExpression Dot(FormulaExpression subject, FormulaExpression accessor)
		{
			if (subject is IFormulaBinaryOperator)
			{
				subject = PowerFxExpressionHelper.Parenthesis(subject);
			}
			return new PowerFxDot(subject, accessor);
		}

		// Token: 0x0600CFDF RID: 53215 RVA: 0x002C47E0 File Offset: 0x002C29E0
		public static FormulaExpression EndsWith(FormulaExpression subject, FormulaExpression target)
		{
			return PowerFxExpressionHelper.Func("EndsWith", new FormulaExpression[] { subject, target });
		}

		// Token: 0x0600CFE0 RID: 53216 RVA: 0x002C47FA File Offset: 0x002C29FA
		public static FormulaExpression Equal(FormulaExpression left, FormulaExpression right)
		{
			return new PowerFxEqual(left, right);
		}

		// Token: 0x0600CFE1 RID: 53217 RVA: 0x002C4803 File Offset: 0x002C2A03
		public static FormulaExpression Find(FormulaExpression delimiter, FormulaExpression subject, FormulaExpression startAt)
		{
			return new PowerFxFind(delimiter, subject, startAt);
		}

		// Token: 0x0600CFE2 RID: 53218 RVA: 0x002C480D File Offset: 0x002C2A0D
		public static FormulaExpression Find(FormulaExpression delimiter, FormulaExpression subject)
		{
			return new PowerFxFind(delimiter, subject);
		}

		// Token: 0x0600CFE3 RID: 53219 RVA: 0x002C4816 File Offset: 0x002C2A16
		public static FormulaExpression First(FormulaExpression list)
		{
			return PowerFxExpressionHelper.Func("First", new FormulaExpression[] { list });
		}

		// Token: 0x0600CFE4 RID: 53220 RVA: 0x002C482C File Offset: 0x002C2A2C
		public static FormulaExpression FirstN(FormulaExpression list, FormulaExpression count)
		{
			return PowerFxExpressionHelper.Func("FirstN", new FormulaExpression[] { list, count });
		}

		// Token: 0x0600CFE5 RID: 53221 RVA: 0x002C4846 File Offset: 0x002C2A46
		public static FormulaExpression Func(string name, params FormulaExpression[] arguments)
		{
			return new PowerFxFunc(name, arguments);
		}

		// Token: 0x0600CFE6 RID: 53222 RVA: 0x002C484F File Offset: 0x002C2A4F
		public static FormulaExpression Func(string name, IEnumerable<FormulaExpression> arguments)
		{
			return new PowerFxFunc(name, arguments);
		}

		// Token: 0x0600CFE7 RID: 53223 RVA: 0x002C4858 File Offset: 0x002C2A58
		public static FormulaExpression Func(string name, string argumentSeparator, IEnumerable<FormulaExpression> arguments)
		{
			return new PowerFxFunc(name, argumentSeparator, arguments);
		}

		// Token: 0x0600CFE8 RID: 53224 RVA: 0x002C4858 File Offset: 0x002C2A58
		public static FormulaExpression Func(string name, string argumentSeparator, params FormulaExpression[] arguments)
		{
			return new PowerFxFunc(name, argumentSeparator, arguments);
		}

		// Token: 0x0600CFE9 RID: 53225 RVA: 0x002C4862 File Offset: 0x002C2A62
		public static FormulaExpression If(FormulaExpression conditionExp, FormulaExpression trueExp, FormulaExpression falseExp)
		{
			return PowerFxExpressionHelper.Func("If", new FormulaExpression[] { conditionExp, trueExp, falseExp });
		}

		// Token: 0x0600CFEA RID: 53226 RVA: 0x002C4880 File Offset: 0x002C2A80
		public static FormulaExpression IsBlank(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("IsBlank", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CFEB RID: 53227 RVA: 0x002C4896 File Offset: 0x002C2A96
		public static FormulaExpression IsBlankOrError(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("IsBlankOrError", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CFEC RID: 53228 RVA: 0x002C48AC File Offset: 0x002C2AAC
		public static FormulaExpression IsMatch(FormulaExpression subject, FormulaExpression regex)
		{
			return PowerFxExpressionHelper.Func("IsMatch", new FormulaExpression[]
			{
				subject,
				regex,
				PowerFxExpressionHelper.Variable("MatchOptions.Contains")
			});
		}

		// Token: 0x0600CFED RID: 53229 RVA: 0x002C48D3 File Offset: 0x002C2AD3
		public static FormulaExpression IsMatchBeginsWith(FormulaExpression subject, FormulaExpression regex)
		{
			return PowerFxExpressionHelper.Func("IsMatch", new FormulaExpression[]
			{
				subject,
				regex,
				PowerFxExpressionHelper.Variable("MatchOptions.BeginsWith")
			});
		}

		// Token: 0x0600CFEE RID: 53230 RVA: 0x002C48FA File Offset: 0x002C2AFA
		public static FormulaExpression IsMatchEndsWith(FormulaExpression subject, FormulaExpression regex)
		{
			return PowerFxExpressionHelper.Func("IsMatch", new FormulaExpression[]
			{
				subject,
				regex,
				PowerFxExpressionHelper.Variable("MatchOptions.EndsWith")
			});
		}

		// Token: 0x0600CFEF RID: 53231 RVA: 0x002C4921 File Offset: 0x002C2B21
		public static FormulaExpression IsNumeric(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("IsNumeric", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CFF0 RID: 53232 RVA: 0x002C4937 File Offset: 0x002C2B37
		public static FormulaExpression Last(FormulaExpression list)
		{
			return PowerFxExpressionHelper.Func("Last", new FormulaExpression[] { list });
		}

		// Token: 0x0600CFF1 RID: 53233 RVA: 0x002C494D File Offset: 0x002C2B4D
		public static FormulaExpression LastN(FormulaExpression list, FormulaExpression num)
		{
			return PowerFxExpressionHelper.Func("LastN", new FormulaExpression[] { list, num });
		}

		// Token: 0x0600CFF2 RID: 53234 RVA: 0x002C4967 File Offset: 0x002C2B67
		public static FormulaExpression Left(FormulaExpression subject, int length)
		{
			return PowerFxExpressionHelper.Left(subject, PowerFxExpressionHelper.NumberLiteral(length));
		}

		// Token: 0x0600CFF3 RID: 53235 RVA: 0x002C4975 File Offset: 0x002C2B75
		public static FormulaExpression Left(FormulaExpression subject, FormulaExpression length)
		{
			return PowerFxExpressionHelper.Func("Left", new FormulaExpression[] { subject, length });
		}

		// Token: 0x0600CFF4 RID: 53236 RVA: 0x002C4990 File Offset: 0x002C2B90
		public static FormulaExpression Len(FormulaExpression subject)
		{
			PowerFxStringLiteral powerFxStringLiteral = subject as PowerFxStringLiteral;
			if (powerFxStringLiteral == null)
			{
				return PowerFxExpressionHelper.Func("Len", new FormulaExpression[] { subject });
			}
			return PowerFxExpressionHelper.NumberLiteral(powerFxStringLiteral.Value.Length);
		}

		// Token: 0x0600CFF5 RID: 53237 RVA: 0x002C49CC File Offset: 0x002C2BCC
		public static FormulaExpression Locale(string locale)
		{
			return new PowerFxLocale(locale);
		}

		// Token: 0x0600CFF6 RID: 53238 RVA: 0x002C49D4 File Offset: 0x002C2BD4
		public static FormulaExpression Lower(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("Lower", new FormulaExpression[] { subject });
		}

		// Token: 0x0600CFF7 RID: 53239 RVA: 0x002C49EA File Offset: 0x002C2BEA
		public static FormulaExpression Match(FormulaExpression subject, FormulaExpression regex)
		{
			return new PowerFxMatch(subject, regex);
		}

		// Token: 0x0600CFF8 RID: 53240 RVA: 0x002C49F3 File Offset: 0x002C2BF3
		public static FormulaExpression MatchAll(FormulaExpression subject, FormulaExpression regex)
		{
			return new PowerFxMatchAll(subject, regex);
		}

		// Token: 0x0600CFF9 RID: 53241 RVA: 0x002C49FC File Offset: 0x002C2BFC
		public static FormulaExpression Mid(FormulaExpression subject, FormulaExpression start, FormulaExpression length = null)
		{
			return PowerFxExpressionHelper.Func("Mid", new FormulaExpression[] { subject, start, length });
		}

		// Token: 0x0600CFFA RID: 53242 RVA: 0x002C4A1A File Offset: 0x002C2C1A
		public static FormulaExpression Nth(FormulaExpression list, FormulaExpression n)
		{
			return PowerFxExpressionHelper.Last(PowerFxExpressionHelper.FirstN(list, n));
		}

		// Token: 0x0600CFFB RID: 53243 RVA: 0x002C4A28 File Offset: 0x002C2C28
		public static FormulaExpression NthFromEnd(FormulaExpression list, FormulaExpression n)
		{
			return PowerFxExpressionHelper.First(PowerFxExpressionHelper.LastN(list, n));
		}

		// Token: 0x0600CFFC RID: 53244 RVA: 0x002C4A36 File Offset: 0x002C2C36
		public static FormulaExpression NumberLiteral(int value)
		{
			return PowerFxExpressionHelper.NumberLiteral(Convert.ToDecimal(value));
		}

		// Token: 0x0600CFFD RID: 53245 RVA: 0x002C4A43 File Offset: 0x002C2C43
		public static FormulaExpression NumberLiteral(decimal value)
		{
			return PowerFxExpressionHelper.NumberLiteral(Convert.ToDouble(value));
		}

		// Token: 0x0600CFFE RID: 53246 RVA: 0x002C4A50 File Offset: 0x002C2C50
		public static FormulaExpression NumberLiteral(double value)
		{
			return new PowerFxNumberLiteral(value);
		}

		// Token: 0x0600CFFF RID: 53247 RVA: 0x002C4A58 File Offset: 0x002C2C58
		public static FormulaExpression Parenthesis(FormulaExpression body)
		{
			return new PowerFxParenthesis(body);
		}

		// Token: 0x0600D000 RID: 53248 RVA: 0x002C4A60 File Offset: 0x002C2C60
		public static FormulaExpression Proper(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("Proper", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D001 RID: 53249 RVA: 0x002C4A76 File Offset: 0x002C2C76
		public static FormulaExpression RawStringLiteral(string value)
		{
			return new PowerFxRawStringLiteral(value);
		}

		// Token: 0x0600D002 RID: 53250 RVA: 0x002C4A7E File Offset: 0x002C2C7E
		public static FormulaExpression RegexLiteral(string value)
		{
			return new PowerFxRegexLiteral(value);
		}

		// Token: 0x0600D003 RID: 53251 RVA: 0x002C4A86 File Offset: 0x002C2C86
		public static FormulaExpression Right(FormulaExpression subject, int length)
		{
			return PowerFxExpressionHelper.Right(subject, PowerFxExpressionHelper.NumberLiteral(length));
		}

		// Token: 0x0600D004 RID: 53252 RVA: 0x002C4A94 File Offset: 0x002C2C94
		public static FormulaExpression Right(FormulaExpression subject, FormulaExpression length)
		{
			return PowerFxExpressionHelper.Func("Right", new FormulaExpression[] { subject, length });
		}

		// Token: 0x0600D005 RID: 53253 RVA: 0x002C4AAE File Offset: 0x002C2CAE
		public static FormulaExpression Split(FormulaExpression subject, FormulaExpression delimiter)
		{
			return PowerFxExpressionHelper.Func("Split", new FormulaExpression[] { subject, delimiter });
		}

		// Token: 0x0600D006 RID: 53254 RVA: 0x002C4AC8 File Offset: 0x002C2CC8
		public static FormulaExpression StartsWith(FormulaExpression subject, FormulaExpression target)
		{
			return PowerFxExpressionHelper.Func("StartsWith", new FormulaExpression[] { subject, target });
		}

		// Token: 0x0600D007 RID: 53255 RVA: 0x002C4AE2 File Offset: 0x002C2CE2
		public static FormulaExpression StringLiteral(string value)
		{
			return new PowerFxStringLiteral(value);
		}

		// Token: 0x0600D008 RID: 53256 RVA: 0x002C4AEA File Offset: 0x002C2CEA
		public static FormulaExpression Substitute(FormulaExpression subject, FormulaExpression findText, FormulaExpression replaceText)
		{
			return PowerFxExpressionHelper.Func("Substitute", new FormulaExpression[] { subject, findText, replaceText });
		}

		// Token: 0x0600D009 RID: 53257 RVA: 0x002C4B08 File Offset: 0x002C2D08
		public static FormulaExpression Text(FormulaExpression value, FormulaExpression format, FormulaExpression culture)
		{
			return PowerFxExpressionHelper.Func("Text", new FormulaExpression[] { value, format, culture });
		}

		// Token: 0x0600D00A RID: 53258 RVA: 0x002C4B26 File Offset: 0x002C2D26
		public static FormulaExpression TimeValue(FormulaExpression value)
		{
			return PowerFxExpressionHelper.Func("TimeValue", new FormulaExpression[] { value });
		}

		// Token: 0x0600D00B RID: 53259 RVA: 0x002C4B3C File Offset: 0x002C2D3C
		public static FormulaExpression TrimEnds(FormulaExpression value)
		{
			return PowerFxExpressionHelper.Func("TrimEnds", new FormulaExpression[] { value });
		}

		// Token: 0x0600D00C RID: 53260 RVA: 0x002C4B52 File Offset: 0x002C2D52
		public static FormulaExpression Upper(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("Upper", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D00D RID: 53261 RVA: 0x002C4B68 File Offset: 0x002C2D68
		public static FormulaExpression Value(FormulaExpression value, FormulaExpression locale = null)
		{
			return PowerFxExpressionHelper.Func("Value", new FormulaExpression[] { value, locale });
		}

		// Token: 0x0600D00E RID: 53262 RVA: 0x002C4B82 File Offset: 0x002C2D82
		public static FormulaExpression Variable(string name)
		{
			return new PowerFxVariable(name);
		}

		// Token: 0x0600D00F RID: 53263 RVA: 0x002C4B8A File Offset: 0x002C2D8A
		public static FormulaExpression With(IReadOnlyList<Dictionary<string, FormulaExpression>> records, FormulaExpression body)
		{
			return records.Reverse<Dictionary<string, FormulaExpression>>().Aggregate(body, (FormulaExpression acc, Dictionary<string, FormulaExpression> record) => PowerFxExpressionHelper.With(record, acc));
		}

		// Token: 0x0600D010 RID: 53264 RVA: 0x002C4BB7 File Offset: 0x002C2DB7
		public static FormulaExpression With(Dictionary<string, FormulaExpression> record, FormulaExpression body)
		{
			if (record.Any<KeyValuePair<string, FormulaExpression>>())
			{
				return new PowerFxWith(record, body);
			}
			return body;
		}

		// Token: 0x0600D011 RID: 53265 RVA: 0x002C4BCA File Offset: 0x002C2DCA
		public static FormulaExpression Round(FormulaExpression value, FormulaExpression decimals)
		{
			return PowerFxExpressionHelper.Func("Round", new FormulaExpression[] { value, decimals });
		}

		// Token: 0x0600D012 RID: 53266 RVA: 0x002C4BE4 File Offset: 0x002C2DE4
		public static FormulaExpression RoundDown(FormulaExpression value, int decimals)
		{
			return PowerFxExpressionHelper.RoundDown(value, PowerFxExpressionHelper.NumberLiteral(decimals));
		}

		// Token: 0x0600D013 RID: 53267 RVA: 0x002C4BF2 File Offset: 0x002C2DF2
		public static FormulaExpression RoundDown(FormulaExpression value, FormulaExpression decimals)
		{
			return PowerFxExpressionHelper.Func("RoundDown", new FormulaExpression[] { value, decimals });
		}

		// Token: 0x0600D014 RID: 53268 RVA: 0x002C4C0C File Offset: 0x002C2E0C
		public static FormulaExpression RoundUp(FormulaExpression value, int decimals)
		{
			return PowerFxExpressionHelper.Func("RoundUp", new FormulaExpression[]
			{
				value,
				PowerFxExpressionHelper.NumberLiteral(decimals)
			});
		}

		// Token: 0x0600D015 RID: 53269 RVA: 0x002C4C2B File Offset: 0x002C2E2B
		public static FormulaExpression RoundUp(FormulaExpression value, FormulaExpression decimals)
		{
			return PowerFxExpressionHelper.Func("RoundUp", new FormulaExpression[] { value, decimals });
		}

		// Token: 0x0600D016 RID: 53270 RVA: 0x002C4C45 File Offset: 0x002C2E45
		public static FormulaExpression Average(IEnumerable<FormulaExpression> arguments)
		{
			return PowerFxExpressionHelper.Func("Average", arguments);
		}

		// Token: 0x0600D017 RID: 53271 RVA: 0x002C4C52 File Offset: 0x002C2E52
		public static FormulaExpression Divide(double left, FormulaExpression right)
		{
			return PowerFxExpressionHelper.Divide(PowerFxExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D018 RID: 53272 RVA: 0x002C4C60 File Offset: 0x002C2E60
		public static FormulaExpression Divide(FormulaExpression left, int right)
		{
			return PowerFxExpressionHelper.Divide(left, PowerFxExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D019 RID: 53273 RVA: 0x002C4C70 File Offset: 0x002C2E70
		public static FormulaExpression Divide(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new PowerFxDivide(left, right);
			}
			return left;
		}

		// Token: 0x0600D01A RID: 53274 RVA: 0x002C4CA1 File Offset: 0x002C2EA1
		public static FormulaExpression Minus(FormulaExpression subject)
		{
			return new PowerFxUnaryMinus(subject);
		}

		// Token: 0x0600D01B RID: 53275 RVA: 0x002C4CA9 File Offset: 0x002C2EA9
		public static FormulaExpression Minus(double left, FormulaExpression right)
		{
			return PowerFxExpressionHelper.Minus(PowerFxExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D01C RID: 53276 RVA: 0x002C4CB7 File Offset: 0x002C2EB7
		public static FormulaExpression Minus(FormulaExpression left, double right)
		{
			return PowerFxExpressionHelper.Minus(left, PowerFxExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D01D RID: 53277 RVA: 0x002C4CC8 File Offset: 0x002C2EC8
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
					return PowerFxExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value - formulaNumberLiteral.Value);
				}
				if (formulaNumberLiteral.Value < 0.0)
				{
					return PowerFxExpressionHelper.Plus(left, PowerFxExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
				}
				FormulaPlus formulaPlus = left as FormulaPlus;
				if (formulaPlus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral3 != null)
					{
						return PowerFxExpressionHelper.Plus(formulaPlus.Left, PowerFxExpressionHelper.Minus(formulaNumberLiteral3, formulaNumberLiteral));
					}
				}
				FormulaMinus formulaMinus = left as FormulaMinus;
				if (formulaMinus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral4 != null)
					{
						return PowerFxExpressionHelper.Minus(formulaMinus.Left, PowerFxExpressionHelper.Plus(formulaNumberLiteral4, formulaNumberLiteral));
					}
				}
			}
			return new PowerFxMinus(left, right);
		}

		// Token: 0x0600D01E RID: 53278 RVA: 0x002C4D9C File Offset: 0x002C2F9C
		public static FormulaExpression Minus1(FormulaExpression val)
		{
			return PowerFxExpressionHelper.Minus(val, PowerFxExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D01F RID: 53279 RVA: 0x002C4DAA File Offset: 0x002C2FAA
		public static FormulaExpression Multiply(double left, FormulaExpression right)
		{
			return PowerFxExpressionHelper.Multiply(PowerFxExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D020 RID: 53280 RVA: 0x002C4DB8 File Offset: 0x002C2FB8
		public static FormulaExpression Multiply(FormulaExpression left, double right)
		{
			return PowerFxExpressionHelper.Multiply(left, PowerFxExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D021 RID: 53281 RVA: 0x002C4DC8 File Offset: 0x002C2FC8
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
				return new PowerFxMultiply(left, right);
			}
			return right;
		}

		// Token: 0x0600D022 RID: 53282 RVA: 0x002C4E16 File Offset: 0x002C3016
		public static FormulaExpression Plus(double left, FormulaExpression right)
		{
			return PowerFxExpressionHelper.Plus(PowerFxExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D023 RID: 53283 RVA: 0x002C4E24 File Offset: 0x002C3024
		public static FormulaExpression Plus(FormulaExpression left, double right)
		{
			return PowerFxExpressionHelper.Plus(left, PowerFxExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D024 RID: 53284 RVA: 0x002C4E34 File Offset: 0x002C3034
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
						return PowerFxExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value + formulaNumberLiteral.Value);
					}
					return right;
				}
				else
				{
					if (formulaNumberLiteral.Value < 0.0)
					{
						return PowerFxExpressionHelper.Minus(left, PowerFxExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
					}
					FormulaPlus formulaPlus = left as FormulaPlus;
					if (formulaPlus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral3 != null)
						{
							return PowerFxExpressionHelper.Plus(formulaPlus.Left, PowerFxExpressionHelper.Plus(formulaNumberLiteral3, formulaNumberLiteral));
						}
					}
					FormulaMinus formulaMinus = left as FormulaMinus;
					if (formulaMinus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral4 != null)
						{
							return PowerFxExpressionHelper.Minus(formulaMinus.Left, PowerFxExpressionHelper.Minus(formulaNumberLiteral4, formulaNumberLiteral));
						}
					}
				}
			}
			return new PowerFxPlus(left, right);
		}

		// Token: 0x0600D025 RID: 53285 RVA: 0x002C4F1B File Offset: 0x002C311B
		public static FormulaExpression Plus1(FormulaExpression val)
		{
			return PowerFxExpressionHelper.Plus(val, PowerFxExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D026 RID: 53286 RVA: 0x002C4F2C File Offset: 0x002C312C
		public static FormulaExpression Product(IEnumerable<FormulaExpression> arguments)
		{
			IReadOnlyList<FormulaExpression> readOnlyList = arguments.ToReadOnlyList<FormulaExpression>();
			if (readOnlyList.Count < 2)
			{
				throw new Exception(string.Format("Too few arguments for Product() {0}", readOnlyList.Count));
			}
			IEnumerable<FormulaExpression> enumerable = readOnlyList.Skip(2);
			FormulaExpression formulaExpression = PowerFxExpressionHelper.Multiply(readOnlyList[0], readOnlyList[1]);
			Func<FormulaExpression, FormulaExpression, FormulaExpression> func;
			if ((func = PowerFxExpressionHelper.<>O.<0>__Multiply) == null)
			{
				func = (PowerFxExpressionHelper.<>O.<0>__Multiply = new Func<FormulaExpression, FormulaExpression, FormulaExpression>(PowerFxExpressionHelper.Multiply));
			}
			return enumerable.Aggregate(formulaExpression, func);
		}

		// Token: 0x0600D027 RID: 53287 RVA: 0x002C4F9E File Offset: 0x002C319E
		public static FormulaExpression Sum(IEnumerable<FormulaExpression> arguments)
		{
			return PowerFxExpressionHelper.Func("Sum", arguments);
		}

		// Token: 0x0600D028 RID: 53288 RVA: 0x002C4FAB File Offset: 0x002C31AB
		public static FormulaExpression GreaterThan(FormulaExpression left, FormulaExpression right)
		{
			return new PowerFxGreaterThan(left, right);
		}

		// Token: 0x0600D029 RID: 53289 RVA: 0x002C4FB4 File Offset: 0x002C31B4
		public static FormulaExpression GreaterThanEqual(FormulaExpression left, FormulaExpression right)
		{
			return new PowerFxGreaterThanEqual(left, right);
		}

		// Token: 0x0600D02A RID: 53290 RVA: 0x002C4FBD File Offset: 0x002C31BD
		public static FormulaExpression LessThan(FormulaExpression left, FormulaExpression right)
		{
			return new PowerFxLessThan(left, right);
		}

		// Token: 0x0600D02B RID: 53291 RVA: 0x002C4FC6 File Offset: 0x002C31C6
		public static FormulaExpression LessThanEqual(FormulaExpression left, FormulaExpression right)
		{
			return new PowerFxLessThanEqual(left, right);
		}

		// Token: 0x0600D02C RID: 53292 RVA: 0x002C4FCF File Offset: 0x002C31CF
		public static FormulaExpression Not(FormulaExpression subject)
		{
			return new PowerFxNot((subject is IFormulaBinaryOperator) ? PowerFxExpressionHelper.Parenthesis(subject) : subject);
		}

		// Token: 0x0600D02D RID: 53293 RVA: 0x002C4FE7 File Offset: 0x002C31E7
		public static FormulaExpression Date(FormulaExpression year, FormulaExpression month, FormulaExpression day)
		{
			return PowerFxExpressionHelper.Func("Date", new FormulaExpression[] { year, month, day });
		}

		// Token: 0x0600D02E RID: 53294 RVA: 0x002C5005 File Offset: 0x002C3205
		public static FormulaExpression Date(FormulaExpression year, int month, int day)
		{
			return PowerFxExpressionHelper.Func("Date", new FormulaExpression[]
			{
				year,
				PowerFxExpressionHelper.NumberLiteral(month),
				PowerFxExpressionHelper.NumberLiteral(day)
			});
		}

		// Token: 0x0600D02F RID: 53295 RVA: 0x002C502D File Offset: 0x002C322D
		public static FormulaExpression Date(int year, FormulaExpression month, int day)
		{
			return PowerFxExpressionHelper.Func("Date", new FormulaExpression[]
			{
				PowerFxExpressionHelper.NumberLiteral(year),
				month,
				PowerFxExpressionHelper.NumberLiteral(day)
			});
		}

		// Token: 0x0600D030 RID: 53296 RVA: 0x002C5058 File Offset: 0x002C3258
		public static FormulaExpression DateAdd(FormulaExpression subject, double? years = null, double? months = null, double? days = null, double? hours = null, double? minutes = null, double? seconds = null, double? milliseconds = null)
		{
			double? num = years;
			double? num7;
			if (num == null)
			{
				double? num2 = months;
				if (num2 == null)
				{
					double? num3 = days;
					if (num3 == null)
					{
						double? num4 = hours;
						if (num4 == null)
						{
							double? num5 = minutes;
							if (num5 == null)
							{
								double? num6 = seconds;
								num7 = ((num6 != null) ? num6 : milliseconds);
							}
							else
							{
								num7 = num5;
							}
						}
						else
						{
							num7 = num4;
						}
					}
					else
					{
						num7 = num3;
					}
				}
				else
				{
					num7 = num2;
				}
			}
			else
			{
				num7 = num;
			}
			double? num8 = num7;
			if (num8 == null)
			{
				throw new Exception("Invalid DateAdd amount.");
			}
			string text = ((years != null) ? "Years" : ((months != null) ? "Months" : ((days != null) ? "Days" : ((hours != null) ? "Hours" : ((minutes != null) ? "Minutes" : ((seconds != null) ? "Seconds" : "Milliseconds"))))));
			return PowerFxExpressionHelper.Func("DateAdd", new FormulaExpression[]
			{
				subject,
				PowerFxExpressionHelper.NumberLiteral(num8.Value),
				PowerFxExpressionHelper.StringLiteral(text)
			});
		}

		// Token: 0x0600D031 RID: 53297 RVA: 0x002C5170 File Offset: 0x002C3370
		public static FormulaExpression DateAdd(FormulaExpression subject, FormulaExpression years = null, FormulaExpression months = null, FormulaExpression days = null, FormulaExpression hours = null, FormulaExpression minutes = null, FormulaExpression seconds = null, FormulaExpression milliseconds = null)
		{
			FormulaExpression formulaExpression = years ?? months ?? days ?? hours ?? minutes ?? seconds ?? milliseconds;
			if (formulaExpression == null)
			{
				throw new Exception("Invalid DateAdd amount.");
			}
			string text = ((years != null) ? "Years" : ((months != null) ? "Months" : ((days != null) ? "Days" : ((hours != null) ? "Hours" : ((minutes != null) ? "Minutes" : ((seconds != null) ? "Seconds" : "Milliseconds"))))));
			return PowerFxExpressionHelper.Func("DateAdd", new FormulaExpression[]
			{
				subject,
				formulaExpression,
				PowerFxExpressionHelper.StringLiteral(text)
			});
		}

		// Token: 0x0600D032 RID: 53298 RVA: 0x002C5240 File Offset: 0x002C3440
		public static FormulaExpression DateDiff(FormulaExpression start, FormulaExpression end, string unit = "Days")
		{
			if (!(unit == "Days"))
			{
				return PowerFxExpressionHelper.Func("DateDiff", new FormulaExpression[]
				{
					start,
					end,
					PowerFxExpressionHelper.StringLiteral(unit)
				});
			}
			return PowerFxExpressionHelper.Func("DateDiff", new FormulaExpression[] { start, end });
		}

		// Token: 0x0600D033 RID: 53299 RVA: 0x002C5294 File Offset: 0x002C3494
		public static FormulaExpression DateTime(int year, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0)
		{
			return PowerFxExpressionHelper.DateTime(PowerFxExpressionHelper.NumberLiteral(year), PowerFxExpressionHelper.NumberLiteral(month), PowerFxExpressionHelper.NumberLiteral(day), PowerFxExpressionHelper.NumberLiteral(hour), PowerFxExpressionHelper.NumberLiteral(minute), PowerFxExpressionHelper.NumberLiteral(second));
		}

		// Token: 0x0600D034 RID: 53300 RVA: 0x002C52C1 File Offset: 0x002C34C1
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day)
		{
			return PowerFxExpressionHelper.Plus(PowerFxExpressionHelper.Date(year, month, day), PowerFxExpressionHelper.Time(PowerFxExpressionHelper.NumberLiteral(0), PowerFxExpressionHelper.NumberLiteral(0), PowerFxExpressionHelper.NumberLiteral(0)));
		}

		// Token: 0x0600D035 RID: 53301 RVA: 0x002C52E7 File Offset: 0x002C34E7
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day, FormulaExpression hour)
		{
			return PowerFxExpressionHelper.Plus(PowerFxExpressionHelper.Date(year, month, day), PowerFxExpressionHelper.Time(hour, PowerFxExpressionHelper.NumberLiteral(0), PowerFxExpressionHelper.NumberLiteral(0)));
		}

		// Token: 0x0600D036 RID: 53302 RVA: 0x002C5308 File Offset: 0x002C3508
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day, FormulaExpression hour, FormulaExpression minute)
		{
			return PowerFxExpressionHelper.Plus(PowerFxExpressionHelper.Date(year, month, day), PowerFxExpressionHelper.Time(hour, minute, PowerFxExpressionHelper.NumberLiteral(0)));
		}

		// Token: 0x0600D037 RID: 53303 RVA: 0x002C5325 File Offset: 0x002C3525
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day, FormulaExpression hour, FormulaExpression minute, FormulaExpression second)
		{
			return PowerFxExpressionHelper.Plus(PowerFxExpressionHelper.Date(year, month, day), PowerFxExpressionHelper.Time(hour, minute, second));
		}

		// Token: 0x0600D038 RID: 53304 RVA: 0x002C533E File Offset: 0x002C353E
		public static FormulaExpression Day(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("Day", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D039 RID: 53305 RVA: 0x002C5354 File Offset: 0x002C3554
		public static FormulaExpression DayEnd(FormulaExpression dayStart)
		{
			double? num = new double?((double)1);
			return PowerFxExpressionHelper.DateAdd(dayStart, null, null, num, null, null, null, null);
		}

		// Token: 0x0600D03A RID: 53306 RVA: 0x002C53A7 File Offset: 0x002C35A7
		public static FormulaExpression DayStart(FormulaExpression subject, bool includeTime = false)
		{
			if (!includeTime)
			{
				return PowerFxExpressionHelper.Date(PowerFxExpressionHelper.Year(subject), PowerFxExpressionHelper.Month(subject), PowerFxExpressionHelper.Day(subject));
			}
			return PowerFxExpressionHelper.DateTime(PowerFxExpressionHelper.Year(subject), PowerFxExpressionHelper.Month(subject), PowerFxExpressionHelper.Day(subject));
		}

		// Token: 0x0600D03B RID: 53307 RVA: 0x002C53DB File Offset: 0x002C35DB
		public static FormulaExpression Hour(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("Hour", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D03C RID: 53308 RVA: 0x002C53F4 File Offset: 0x002C35F4
		public static FormulaExpression HourEnd(FormulaExpression hourStart)
		{
			double? num = new double?((double)1);
			return PowerFxExpressionHelper.DateAdd(hourStart, null, null, null, num, null, null, null);
		}

		// Token: 0x0600D03D RID: 53309 RVA: 0x002C5447 File Offset: 0x002C3647
		public static FormulaExpression HourStart(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.DateTime(PowerFxExpressionHelper.Year(subject), PowerFxExpressionHelper.Month(subject), PowerFxExpressionHelper.Day(subject), PowerFxExpressionHelper.Hour(subject));
		}

		// Token: 0x0600D03E RID: 53310 RVA: 0x002C5466 File Offset: 0x002C3666
		public static FormulaExpression Midpoint(FormulaExpression start, FormulaExpression end)
		{
			return PowerFxExpressionHelper.Plus(start, PowerFxExpressionHelper.Divide(PowerFxExpressionHelper.Minus(end, start), 2));
		}

		// Token: 0x0600D03F RID: 53311 RVA: 0x002C547B File Offset: 0x002C367B
		public static FormulaExpression Minute(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("Minute", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D040 RID: 53312 RVA: 0x002C5494 File Offset: 0x002C3694
		public static FormulaExpression MinuteEnd(FormulaExpression minuteStart)
		{
			double? num = new double?((double)1);
			return PowerFxExpressionHelper.DateAdd(minuteStart, null, null, null, null, num, null, null);
		}

		// Token: 0x0600D041 RID: 53313 RVA: 0x002C54E7 File Offset: 0x002C36E7
		public static FormulaExpression MinuteStart(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.DateTime(PowerFxExpressionHelper.Year(subject), PowerFxExpressionHelper.Month(subject), PowerFxExpressionHelper.Day(subject), PowerFxExpressionHelper.Hour(subject), PowerFxExpressionHelper.Minute(subject));
		}

		// Token: 0x0600D042 RID: 53314 RVA: 0x002C550C File Offset: 0x002C370C
		public static FormulaExpression Month(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("Month", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D043 RID: 53315 RVA: 0x002C5522 File Offset: 0x002C3722
		public static FormulaExpression MonthDays(FormulaExpression subject, FormulaExpression monthStart = null)
		{
			if (monthStart == null)
			{
				monthStart = PowerFxExpressionHelper.MonthStart(subject);
			}
			return PowerFxExpressionHelper.DateDiff(monthStart, PowerFxExpressionHelper.MonthEnd(monthStart), "Days");
		}

		// Token: 0x0600D044 RID: 53316 RVA: 0x002C5540 File Offset: 0x002C3740
		public static FormulaExpression MonthEnd(FormulaExpression monthStart)
		{
			double? num = new double?((double)1);
			return PowerFxExpressionHelper.DateAdd(monthStart, null, num, null, null, null, null, null);
		}

		// Token: 0x0600D045 RID: 53317 RVA: 0x002C5593 File Offset: 0x002C3793
		public static FormulaExpression MonthStart(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Date(PowerFxExpressionHelper.Year(subject), PowerFxExpressionHelper.Month(subject), PowerFxExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D046 RID: 53318 RVA: 0x002C55AC File Offset: 0x002C37AC
		public static FormulaExpression MonthWeek(FormulaExpression subject, FormulaExpression monthStart = null)
		{
			if (monthStart == null)
			{
				monthStart = PowerFxExpressionHelper.MonthStart(subject);
			}
			return PowerFxExpressionHelper.RoundUp(PowerFxExpressionHelper.Divide(PowerFxExpressionHelper.Plus(PowerFxExpressionHelper.DateDiff(monthStart, subject, "Days"), PowerFxExpressionHelper.Weekday(monthStart)), 7), 0);
		}

		// Token: 0x0600D047 RID: 53319 RVA: 0x002C55DC File Offset: 0x002C37DC
		public static FormulaExpression Quarter(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.RoundUp(PowerFxExpressionHelper.Divide(PowerFxExpressionHelper.Month(subject), 3), 0);
		}

		// Token: 0x0600D048 RID: 53320 RVA: 0x002C55F0 File Offset: 0x002C37F0
		public static FormulaExpression QuarterDay(FormulaExpression subject, FormulaExpression quarter = null, FormulaExpression quarterStart = null)
		{
			if (quarter == null)
			{
				quarter = PowerFxExpressionHelper.Quarter(subject);
			}
			if (quarterStart == null)
			{
				quarterStart = PowerFxExpressionHelper.QuarterStart(subject, quarter);
			}
			return PowerFxExpressionHelper.Plus1(PowerFxExpressionHelper.DateDiff(quarterStart, subject, "Days"));
		}

		// Token: 0x0600D049 RID: 53321 RVA: 0x002C561A File Offset: 0x002C381A
		public static FormulaExpression QuarterDays(FormulaExpression subject, FormulaExpression quarter = null, FormulaExpression quarterStart = null, FormulaExpression quarterEnd = null)
		{
			if (quarter == null)
			{
				quarter = PowerFxExpressionHelper.Quarter(subject);
			}
			if (quarterStart == null)
			{
				quarterStart = PowerFxExpressionHelper.QuarterStart(subject, quarter);
			}
			if (quarterEnd == null)
			{
				quarterEnd = PowerFxExpressionHelper.QuarterEnd(subject, quarter, null);
			}
			return PowerFxExpressionHelper.DateDiff(quarterStart, quarterEnd, "Days");
		}

		// Token: 0x0600D04A RID: 53322 RVA: 0x002C564C File Offset: 0x002C384C
		public static FormulaExpression QuarterEnd(FormulaExpression subject, FormulaExpression quarter = null, FormulaExpression quarterStart = null)
		{
			if (quarter == null)
			{
				quarter = PowerFxExpressionHelper.Quarter(subject);
			}
			if (quarterStart == null)
			{
				quarterStart = PowerFxExpressionHelper.QuarterStart(subject, quarter);
			}
			FormulaExpression formulaExpression = quarterStart;
			double? num = new double?((double)3);
			return PowerFxExpressionHelper.DateAdd(formulaExpression, null, num, null, null, null, null, null);
		}

		// Token: 0x0600D04B RID: 53323 RVA: 0x002C56B6 File Offset: 0x002C38B6
		public static FormulaExpression QuarterStart(FormulaExpression subject, FormulaExpression quarter = null)
		{
			if (quarter == null)
			{
				quarter = PowerFxExpressionHelper.Quarter(subject);
			}
			return PowerFxExpressionHelper.Date(PowerFxExpressionHelper.Year(subject), PowerFxExpressionHelper.Minus(PowerFxExpressionHelper.Multiply(3.0, quarter), 2.0), PowerFxExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D04C RID: 53324 RVA: 0x002C56F1 File Offset: 0x002C38F1
		public static FormulaExpression QuarterWeek(FormulaExpression subject, FormulaExpression quarter = null, FormulaExpression quarterStart = null)
		{
			if (quarter == null)
			{
				quarter = PowerFxExpressionHelper.Quarter(subject);
			}
			if (quarterStart == null)
			{
				quarterStart = PowerFxExpressionHelper.QuarterStart(subject, quarter);
			}
			return PowerFxExpressionHelper.RoundUp(PowerFxExpressionHelper.Divide(PowerFxExpressionHelper.Plus(PowerFxExpressionHelper.DateDiff(quarterStart, subject, "Days"), PowerFxExpressionHelper.Weekday(quarterStart)), 7), 0);
		}

		// Token: 0x0600D04D RID: 53325 RVA: 0x002C572D File Offset: 0x002C392D
		public static FormulaExpression Second(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("Second", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D04E RID: 53326 RVA: 0x002C5744 File Offset: 0x002C3944
		public static FormulaExpression SecondEnd(FormulaExpression secondStart)
		{
			double? num = new double?((double)1);
			return PowerFxExpressionHelper.DateAdd(secondStart, null, null, null, null, null, num, null);
		}

		// Token: 0x0600D04F RID: 53327 RVA: 0x002C5797 File Offset: 0x002C3997
		public static FormulaExpression SecondStart(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.DateTime(PowerFxExpressionHelper.Year(subject), PowerFxExpressionHelper.Month(subject), PowerFxExpressionHelper.Day(subject), PowerFxExpressionHelper.Hour(subject), PowerFxExpressionHelper.Minute(subject), PowerFxExpressionHelper.Second(subject));
		}

		// Token: 0x0600D050 RID: 53328 RVA: 0x002C57C2 File Offset: 0x002C39C2
		public static FormulaExpression Time(FormulaExpression hour, FormulaExpression minute, FormulaExpression second)
		{
			return PowerFxExpressionHelper.Func("Time", new FormulaExpression[] { hour, minute, second });
		}

		// Token: 0x0600D051 RID: 53329 RVA: 0x002C57E0 File Offset: 0x002C39E0
		public static FormulaExpression Weekday(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("Weekday", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D052 RID: 53330 RVA: 0x002C57F8 File Offset: 0x002C39F8
		public static FormulaExpression WeekEnd(FormulaExpression weekStart)
		{
			double? num = new double?((double)7);
			return PowerFxExpressionHelper.DateAdd(weekStart, null, null, num, null, null, null, null);
		}

		// Token: 0x0600D053 RID: 53331 RVA: 0x002C584B File Offset: 0x002C3A4B
		public static FormulaExpression WeekStart(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.DateAdd(PowerFxExpressionHelper.Date(PowerFxExpressionHelper.Year(subject), PowerFxExpressionHelper.Month(subject), PowerFxExpressionHelper.Day(subject)), null, null, PowerFxExpressionHelper.Minus(1.0, PowerFxExpressionHelper.Weekday(subject)), null, null, null, null);
		}

		// Token: 0x0600D054 RID: 53332 RVA: 0x002C5883 File Offset: 0x002C3A83
		public static FormulaExpression Year(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("Year", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D055 RID: 53333 RVA: 0x002C5899 File Offset: 0x002C3A99
		public static FormulaExpression YearDay(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Plus1(PowerFxExpressionHelper.DateDiff(PowerFxExpressionHelper.YearStart(subject), subject, "Days"));
		}

		// Token: 0x0600D056 RID: 53334 RVA: 0x002C58B1 File Offset: 0x002C3AB1
		public static FormulaExpression YearDays(FormulaExpression subject, FormulaExpression yearStart = null, FormulaExpression yearEnd = null)
		{
			if (yearStart == null)
			{
				yearStart = PowerFxExpressionHelper.YearStart(subject);
			}
			if (yearEnd == null)
			{
				yearEnd = PowerFxExpressionHelper.YearEnd(yearStart);
			}
			return PowerFxExpressionHelper.DateDiff(yearStart, yearEnd, "Days");
		}

		// Token: 0x0600D057 RID: 53335 RVA: 0x002C58D8 File Offset: 0x002C3AD8
		public static FormulaExpression YearEnd(FormulaExpression yearStart)
		{
			return PowerFxExpressionHelper.DateAdd(yearStart, new double?((double)1), null, null, null, null, null, null);
		}

		// Token: 0x0600D058 RID: 53336 RVA: 0x002C5928 File Offset: 0x002C3B28
		public static FormulaExpression YearStart(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Date(PowerFxExpressionHelper.Year(subject), PowerFxExpressionHelper.NumberLiteral(1), PowerFxExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D059 RID: 53337 RVA: 0x002C5941 File Offset: 0x002C3B41
		public static FormulaExpression YearWeek(FormulaExpression subject)
		{
			return PowerFxExpressionHelper.Func("WeekNum", new FormulaExpression[] { subject });
		}

		// Token: 0x020018F2 RID: 6386
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040050E5 RID: 20709
			public static Func<FormulaExpression, FormulaExpression, FormulaExpression> <0>__Multiply;
		}
	}
}

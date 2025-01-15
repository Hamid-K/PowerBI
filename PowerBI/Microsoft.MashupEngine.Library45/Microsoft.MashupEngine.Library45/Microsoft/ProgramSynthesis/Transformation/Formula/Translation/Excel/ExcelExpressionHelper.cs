using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x0200192F RID: 6447
	internal static class ExcelExpressionHelper
	{
		// Token: 0x0600D224 RID: 53796 RVA: 0x002CC80B File Offset: 0x002CAA0B
		public static FormulaExpression Average(IEnumerable<FormulaExpression> arguments)
		{
			return ExcelExpressionHelper.Func("Average", arguments);
		}

		// Token: 0x0600D225 RID: 53797 RVA: 0x002CC818 File Offset: 0x002CAA18
		public static FormulaExpression Char(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Char", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D226 RID: 53798 RVA: 0x002CC82E File Offset: 0x002CAA2E
		public static FormulaExpression Code(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Code", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D227 RID: 53799 RVA: 0x002CC844 File Offset: 0x002CAA44
		public static FormulaExpression Or(FormulaExpression left, FormulaExpression right)
		{
			return ExcelExpressionHelper.Func("Or", new FormulaExpression[] { left, right });
		}

		// Token: 0x0600D228 RID: 53800 RVA: 0x002CC85E File Offset: 0x002CAA5E
		public static FormulaExpression Columns(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Columns", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D229 RID: 53801 RVA: 0x002CC874 File Offset: 0x002CAA74
		public static FormulaExpression Concat(FormulaExpression left, FormulaExpression right)
		{
			ExcelStringLiteral excelStringLiteral = left as ExcelStringLiteral;
			if (excelStringLiteral != null)
			{
				ExcelStringLiteral excelStringLiteral2 = right as ExcelStringLiteral;
				if (excelStringLiteral2 != null)
				{
					return ExcelExpressionHelper.StringLiteral(excelStringLiteral.Value + excelStringLiteral2.Value);
				}
			}
			return new ExcelConcat(left, right);
		}

		// Token: 0x0600D22A RID: 53802 RVA: 0x002CC8B3 File Offset: 0x002CAAB3
		public static FormulaExpression DateTimeLiteral(DateTime value)
		{
			return new ExcelDateTimeLiteral(value);
		}

		// Token: 0x0600D22B RID: 53803 RVA: 0x002CC8BB File Offset: 0x002CAABB
		public static FormulaExpression DateValue(FormulaExpression value)
		{
			return ExcelExpressionHelper.Func("DateValue", new FormulaExpression[] { value });
		}

		// Token: 0x0600D22C RID: 53804 RVA: 0x002CC8D1 File Offset: 0x002CAAD1
		public static FormulaExpression DigitRange()
		{
			return new ExcelDigitRange();
		}

		// Token: 0x0600D22D RID: 53805 RVA: 0x002CC8D8 File Offset: 0x002CAAD8
		public static FormulaExpression Divide(double left, double right)
		{
			return ExcelExpressionHelper.Divide(ExcelExpressionHelper.NumberLiteral(left), ExcelExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D22E RID: 53806 RVA: 0x002CC8EB File Offset: 0x002CAAEB
		public static FormulaExpression Divide(double left, FormulaExpression right)
		{
			return ExcelExpressionHelper.Divide(ExcelExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D22F RID: 53807 RVA: 0x002CC8F9 File Offset: 0x002CAAF9
		public static FormulaExpression Divide(FormulaExpression left, double right)
		{
			return ExcelExpressionHelper.Divide(left, ExcelExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D230 RID: 53808 RVA: 0x002CC908 File Offset: 0x002CAB08
		public static FormulaExpression Divide(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new ExcelDivide(left, right);
			}
			return left;
		}

		// Token: 0x0600D231 RID: 53809 RVA: 0x002CC939 File Offset: 0x002CAB39
		public static FormulaExpression Equal(FormulaExpression left, FormulaExpression right)
		{
			return new ExcelEqual(left, right);
		}

		// Token: 0x0600D232 RID: 53810 RVA: 0x002CC942 File Offset: 0x002CAB42
		public static FormulaExpression Filter(FormulaExpression subject, FormulaExpression predicate)
		{
			return ExcelExpressionHelper.Func("Filter", new FormulaExpression[] { subject, predicate });
		}

		// Token: 0x0600D233 RID: 53811 RVA: 0x002CC95C File Offset: 0x002CAB5C
		public static FormulaExpression FilterAnd(FormulaExpression left, FormulaExpression right)
		{
			return new ExcelFilterAnd(left, right);
		}

		// Token: 0x0600D234 RID: 53812 RVA: 0x002CC965 File Offset: 0x002CAB65
		public static FormulaExpression Find(FormulaExpression delimiter, FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Find", new FormulaExpression[] { delimiter, subject });
		}

		// Token: 0x0600D235 RID: 53813 RVA: 0x002CC97F File Offset: 0x002CAB7F
		public static FormulaExpression Find(FormulaExpression delimiter, FormulaExpression subject, FormulaExpression startAt)
		{
			return ExcelExpressionHelper.Func("Find", new FormulaExpression[] { delimiter, subject, startAt });
		}

		// Token: 0x0600D236 RID: 53814 RVA: 0x002CC9A0 File Offset: 0x002CABA0
		public static FormulaExpression FindInstance(FormulaExpression delimiter, FormulaExpression subject, FormulaExpression instance)
		{
			FormulaExpression formulaExpression = ExcelExpressionHelper.StringLiteral("{token}");
			FormulaNumberLiteral formulaNumberLiteral = instance as FormulaNumberLiteral;
			if (formulaNumberLiteral != null)
			{
				if (Math.Abs(formulaNumberLiteral.Value - 1.0) < 0.1)
				{
					return ExcelExpressionHelper.Func("Find", new FormulaExpression[] { delimiter, subject });
				}
				if (formulaNumberLiteral.Value < 0.0)
				{
					FormulaExpression formulaExpression2 = ExcelExpressionHelper.Len(delimiter);
					FormulaStringLiteral formulaStringLiteral = delimiter as FormulaStringLiteral;
					if (formulaStringLiteral != null)
					{
						formulaExpression2 = ExcelExpressionHelper.NumberLiteral(formulaStringLiteral.Value.Length);
					}
					instance = ExcelExpressionHelper.Minus(ExcelExpressionHelper.Divide(ExcelExpressionHelper.Minus(ExcelExpressionHelper.Len(subject), ExcelExpressionHelper.Len(ExcelExpressionHelper.Substitute(subject, delimiter, ExcelExpressionHelper.StringLiteral(string.Empty), null))), formulaExpression2), ExcelExpressionHelper.NumberLiteral(-(formulaNumberLiteral.Value + 1.0)));
				}
			}
			return ExcelExpressionHelper.Func("Find", new FormulaExpression[]
			{
				formulaExpression,
				ExcelExpressionHelper.Substitute(subject, delimiter, formulaExpression, instance)
			});
		}

		// Token: 0x0600D237 RID: 53815 RVA: 0x002CCA96 File Offset: 0x002CAC96
		public static FormulaExpression FindN(FormulaExpression delimiter, FormulaExpression subject, FormulaExpression instance)
		{
			return ExcelExpressionHelper.Func("FindN", new FormulaExpression[] { delimiter, subject, instance });
		}

		// Token: 0x0600D238 RID: 53816 RVA: 0x002CCAB4 File Offset: 0x002CACB4
		public static FormulaExpression Format(string format, string locale)
		{
			return new ExcelFormat(format, locale);
		}

		// Token: 0x0600D239 RID: 53817 RVA: 0x002CCABD File Offset: 0x002CACBD
		public static FormulaExpression Format(string format)
		{
			return new ExcelFormat(format);
		}

		// Token: 0x0600D23A RID: 53818 RVA: 0x002CCAC5 File Offset: 0x002CACC5
		public static FormulaExpression Func(string name, params FormulaExpression[] arguments)
		{
			return new ExcelFunc(name, arguments);
		}

		// Token: 0x0600D23B RID: 53819 RVA: 0x002CCACE File Offset: 0x002CACCE
		public static FormulaExpression Func(string name, IEnumerable<FormulaExpression> arguments)
		{
			return new ExcelFunc(name, arguments);
		}

		// Token: 0x0600D23C RID: 53820 RVA: 0x002CCAD7 File Offset: 0x002CACD7
		public static FormulaExpression Func(string name, string argumentSeparator, IEnumerable<FormulaExpression> arguments)
		{
			return new ExcelFunc(name, argumentSeparator, arguments);
		}

		// Token: 0x0600D23D RID: 53821 RVA: 0x002CCAE1 File Offset: 0x002CACE1
		public static FormulaExpression GreaterThan(FormulaExpression left, double right)
		{
			return ExcelExpressionHelper.GreaterThan(left, ExcelExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D23E RID: 53822 RVA: 0x002CCAEF File Offset: 0x002CACEF
		public static FormulaExpression GreaterThan(FormulaExpression left, FormulaExpression right)
		{
			return new ExcelGreaterThan(left, right);
		}

		// Token: 0x0600D23F RID: 53823 RVA: 0x002CCAF8 File Offset: 0x002CACF8
		public static FormulaExpression GreaterThanEqual(FormulaExpression left, FormulaExpression right)
		{
			return new ExcelGreaterThanEqual(left, right);
		}

		// Token: 0x0600D240 RID: 53824 RVA: 0x002CCB01 File Offset: 0x002CAD01
		public static FormulaExpression If(FormulaExpression conditionExp, FormulaExpression trueExp, FormulaExpression falseExp)
		{
			return ExcelExpressionHelper.Func("If", new FormulaExpression[] { conditionExp, trueExp, falseExp });
		}

		// Token: 0x0600D241 RID: 53825 RVA: 0x002CCB1F File Offset: 0x002CAD1F
		public static FormulaExpression If(FormulaExpression conditionExp, FormulaExpression trueExp)
		{
			return ExcelExpressionHelper.Func("If", new FormulaExpression[] { conditionExp, trueExp });
		}

		// Token: 0x0600D242 RID: 53826 RVA: 0x002CCB39 File Offset: 0x002CAD39
		public static FormulaExpression IfError(FormulaExpression subject, FormulaExpression errorExp)
		{
			return ExcelExpressionHelper.Func("IfError", new FormulaExpression[] { subject, errorExp });
		}

		// Token: 0x0600D243 RID: 53827 RVA: 0x002CCB53 File Offset: 0x002CAD53
		public static FormulaExpression Index(FormulaExpression subject, int instance)
		{
			return ExcelExpressionHelper.Index(subject, ExcelExpressionHelper.NumberLiteral(instance));
		}

		// Token: 0x0600D244 RID: 53828 RVA: 0x002CCB61 File Offset: 0x002CAD61
		public static FormulaExpression Index(FormulaExpression subject, FormulaExpression instance)
		{
			return ExcelExpressionHelper.Func("Index", new FormulaExpression[] { subject, instance });
		}

		// Token: 0x0600D245 RID: 53829 RVA: 0x002CCB7B File Offset: 0x002CAD7B
		public static FormulaExpression Indirect(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Indirect", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D246 RID: 53830 RVA: 0x002CCB91 File Offset: 0x002CAD91
		public static FormulaExpression IsBlank(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("IsBlank", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D247 RID: 53831 RVA: 0x002CCBA7 File Offset: 0x002CADA7
		public static FormulaExpression IsNumber(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("IsNumber", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D248 RID: 53832 RVA: 0x002CCBBD File Offset: 0x002CADBD
		public static FormulaExpression IsText(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("IsText", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D249 RID: 53833 RVA: 0x002CCBD4 File Offset: 0x002CADD4
		public static FormulaExpression Left(FormulaExpression str, FormulaExpression length)
		{
			ExcelNumberLiteral excelNumberLiteral = length as ExcelNumberLiteral;
			if (excelNumberLiteral == null || excelNumberLiteral.Value != 1.0)
			{
				return ExcelExpressionHelper.Func("Left", new FormulaExpression[] { str, length });
			}
			return ExcelExpressionHelper.Func("Left", new FormulaExpression[] { str });
		}

		// Token: 0x0600D24A RID: 53834 RVA: 0x002CCC29 File Offset: 0x002CAE29
		public static FormulaExpression Left(FormulaExpression str)
		{
			return ExcelExpressionHelper.Func("Left", new FormulaExpression[] { str });
		}

		// Token: 0x0600D24B RID: 53835 RVA: 0x002CCC40 File Offset: 0x002CAE40
		public static FormulaExpression Len(FormulaExpression str)
		{
			ExcelStringLiteral excelStringLiteral = str as ExcelStringLiteral;
			if (excelStringLiteral == null)
			{
				return ExcelExpressionHelper.Func("Len", new FormulaExpression[] { str });
			}
			return ExcelExpressionHelper.NumberLiteral(excelStringLiteral.Value.Length);
		}

		// Token: 0x0600D24C RID: 53836 RVA: 0x002CCC7C File Offset: 0x002CAE7C
		public static FormulaExpression LessThan(FormulaExpression left, double right)
		{
			return ExcelExpressionHelper.LessThan(left, ExcelExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D24D RID: 53837 RVA: 0x002CCC8A File Offset: 0x002CAE8A
		public static FormulaExpression LessThan(FormulaExpression left, FormulaExpression right)
		{
			return new ExcelLessThan(left, right);
		}

		// Token: 0x0600D24E RID: 53838 RVA: 0x002CCC93 File Offset: 0x002CAE93
		public static FormulaExpression LessThanEqual(int left, FormulaExpression right)
		{
			return new ExcelLessThanEqual(ExcelExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D24F RID: 53839 RVA: 0x002CCCA1 File Offset: 0x002CAEA1
		public static FormulaExpression LessThanEqual(FormulaExpression left, int right)
		{
			return new ExcelLessThanEqual(left, ExcelExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D250 RID: 53840 RVA: 0x002CCCAF File Offset: 0x002CAEAF
		public static FormulaExpression LessThanEqual(FormulaExpression left, FormulaExpression right)
		{
			return new ExcelLessThanEqual(left, right);
		}

		// Token: 0x0600D251 RID: 53841 RVA: 0x002CCCB8 File Offset: 0x002CAEB8
		public static FormulaExpression Let(IEnumerable<FormulaExpression> variablePairs, FormulaExpression targetExp)
		{
			return ExcelExpressionHelper.Let(variablePairs.Concat(targetExp.Yield<FormulaExpression>()));
		}

		// Token: 0x0600D252 RID: 53842 RVA: 0x002CCCCB File Offset: 0x002CAECB
		public static FormulaExpression Let(IEnumerable<FormulaExpression> arguments)
		{
			return new ExcelLet(arguments);
		}

		// Token: 0x0600D253 RID: 53843 RVA: 0x002CCCD3 File Offset: 0x002CAED3
		public static FormulaExpression Lower(FormulaExpression str)
		{
			return ExcelExpressionHelper.Func("Lower", new FormulaExpression[] { str });
		}

		// Token: 0x0600D254 RID: 53844 RVA: 0x002CCCE9 File Offset: 0x002CAEE9
		public static FormulaExpression Max(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Max", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D255 RID: 53845 RVA: 0x002CCCFF File Offset: 0x002CAEFF
		public static FormulaExpression Mid(FormulaExpression str, FormulaExpression start, FormulaExpression length)
		{
			return ExcelExpressionHelper.Func("Mid", new FormulaExpression[] { str, start, length });
		}

		// Token: 0x0600D256 RID: 53846 RVA: 0x002CCD1D File Offset: 0x002CAF1D
		public static FormulaExpression Min(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Min", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D257 RID: 53847 RVA: 0x002CCD33 File Offset: 0x002CAF33
		public static FormulaExpression Minus(double left, FormulaExpression right)
		{
			return ExcelExpressionHelper.Minus(ExcelExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D258 RID: 53848 RVA: 0x002CCD41 File Offset: 0x002CAF41
		public static FormulaExpression Minus(FormulaExpression left, double right)
		{
			return ExcelExpressionHelper.Minus(left, ExcelExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D259 RID: 53849 RVA: 0x002CCD50 File Offset: 0x002CAF50
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
					return ExcelExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value - formulaNumberLiteral.Value);
				}
				if (formulaNumberLiteral.Value < 0.0)
				{
					return ExcelExpressionHelper.Plus(left, ExcelExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
				}
				FormulaPlus formulaPlus = left as FormulaPlus;
				if (formulaPlus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral3 != null)
					{
						return ExcelExpressionHelper.Plus(formulaPlus.Left, ExcelExpressionHelper.Minus(formulaNumberLiteral3, formulaNumberLiteral));
					}
				}
				FormulaMinus formulaMinus = left as FormulaMinus;
				if (formulaMinus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral4 != null)
					{
						return ExcelExpressionHelper.Minus(formulaMinus.Left, ExcelExpressionHelper.Plus(formulaNumberLiteral4, formulaNumberLiteral));
					}
				}
			}
			return new ExcelMinus(left, right);
		}

		// Token: 0x0600D25A RID: 53850 RVA: 0x002CCE24 File Offset: 0x002CB024
		public static FormulaExpression Minus1(FormulaExpression val)
		{
			return ExcelExpressionHelper.Minus(val, ExcelExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D25B RID: 53851 RVA: 0x002CCE32 File Offset: 0x002CB032
		public static FormulaExpression Mod(double subject, FormulaExpression divisor)
		{
			return ExcelExpressionHelper.Mod(ExcelExpressionHelper.NumberLiteral(subject), divisor);
		}

		// Token: 0x0600D25C RID: 53852 RVA: 0x002CCE40 File Offset: 0x002CB040
		public static FormulaExpression Mod(FormulaExpression subject, double divisor)
		{
			return ExcelExpressionHelper.Mod(subject, ExcelExpressionHelper.NumberLiteral(divisor));
		}

		// Token: 0x0600D25D RID: 53853 RVA: 0x002CCE4E File Offset: 0x002CB04E
		public static FormulaExpression Mod(FormulaExpression subject, FormulaExpression divisor)
		{
			return ExcelExpressionHelper.Func("Mod", new FormulaExpression[] { subject, divisor });
		}

		// Token: 0x0600D25E RID: 53854 RVA: 0x002CCE68 File Offset: 0x002CB068
		public static FormulaExpression MRound(FormulaExpression value, double interval)
		{
			return ExcelExpressionHelper.MRound(value, ExcelExpressionHelper.NumberLiteral(interval));
		}

		// Token: 0x0600D25F RID: 53855 RVA: 0x002CCE78 File Offset: 0x002CB078
		public static FormulaExpression MRound(FormulaExpression value, FormulaExpression interval)
		{
			FormulaNumberLiteral formulaNumberLiteral = interval as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 0.0)
			{
				return ExcelExpressionHelper.Func("MRound", new FormulaExpression[] { value, interval });
			}
			return ExcelExpressionHelper.Func("MRound", new FormulaExpression[] { value });
		}

		// Token: 0x0600D260 RID: 53856 RVA: 0x002CCECD File Offset: 0x002CB0CD
		public static FormulaExpression Multiply(double left, double right)
		{
			return ExcelExpressionHelper.Multiply(ExcelExpressionHelper.NumberLiteral(left), ExcelExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D261 RID: 53857 RVA: 0x002CCEE0 File Offset: 0x002CB0E0
		public static FormulaExpression Multiply(double left, FormulaExpression right)
		{
			return ExcelExpressionHelper.Multiply(ExcelExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D262 RID: 53858 RVA: 0x002CCEEE File Offset: 0x002CB0EE
		public static FormulaExpression Multiply(FormulaExpression left, double right)
		{
			return ExcelExpressionHelper.Multiply(left, ExcelExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D263 RID: 53859 RVA: 0x002CCEFC File Offset: 0x002CB0FC
		public static FormulaExpression Multiply(FormulaExpression left, FormulaExpression right)
		{
			ExcelNumberLiteral excelNumberLiteral = left as ExcelNumberLiteral;
			if (excelNumberLiteral != null)
			{
				ExcelNumberLiteral excelNumberLiteral2 = right as ExcelNumberLiteral;
				if (excelNumberLiteral2 != null)
				{
					return ExcelExpressionHelper.NumberLiteral(excelNumberLiteral.Value * excelNumberLiteral2.Value);
				}
			}
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral != null && formulaNumberLiteral.Value == 1.0)
			{
				return left;
			}
			formulaNumberLiteral = left as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new ExcelMultiply(left, right);
			}
			return right;
		}

		// Token: 0x0600D264 RID: 53860 RVA: 0x002CCF71 File Offset: 0x002CB171
		public static FormulaExpression Not(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Not", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D265 RID: 53861 RVA: 0x002CCF87 File Offset: 0x002CB187
		public static FormulaExpression NumberLiteral(int value)
		{
			return ExcelExpressionHelper.NumberLiteral(Convert.ToDouble(value));
		}

		// Token: 0x0600D266 RID: 53862 RVA: 0x002CCF94 File Offset: 0x002CB194
		public static FormulaExpression NumberLiteral(decimal value)
		{
			return ExcelExpressionHelper.NumberLiteral(Convert.ToDouble(value));
		}

		// Token: 0x0600D267 RID: 53863 RVA: 0x002CCFA1 File Offset: 0x002CB1A1
		public static FormulaExpression NumberLiteral(double value)
		{
			return new ExcelNumberLiteral(value);
		}

		// Token: 0x0600D268 RID: 53864 RVA: 0x002CCFA9 File Offset: 0x002CB1A9
		public static FormulaExpression NumberValue(FormulaExpression value)
		{
			return ExcelExpressionHelper.Func("NumberValue", new FormulaExpression[] { value });
		}

		// Token: 0x0600D269 RID: 53865 RVA: 0x002CCFBF File Offset: 0x002CB1BF
		public static FormulaExpression NumberValue(FormulaExpression value, FormulaExpression decimalSeparator, FormulaExpression groupSeparator)
		{
			return ExcelExpressionHelper.Func("NumberValue", new FormulaExpression[] { value, decimalSeparator, groupSeparator });
		}

		// Token: 0x0600D26A RID: 53866 RVA: 0x002CCFDD File Offset: 0x002CB1DD
		public static FormulaExpression Plus(FormulaExpression left, double right)
		{
			return ExcelExpressionHelper.Plus(left, ExcelExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D26B RID: 53867 RVA: 0x002CCFEC File Offset: 0x002CB1EC
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
						return ExcelExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value + formulaNumberLiteral.Value);
					}
					return right;
				}
				else
				{
					if (formulaNumberLiteral.Value < 0.0)
					{
						return ExcelExpressionHelper.Minus(left, ExcelExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
					}
					FormulaPlus formulaPlus = left as FormulaPlus;
					if (formulaPlus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral3 != null)
						{
							return ExcelExpressionHelper.Plus(formulaPlus.Left, ExcelExpressionHelper.Plus(formulaNumberLiteral3, formulaNumberLiteral));
						}
					}
					FormulaMinus formulaMinus = left as FormulaMinus;
					if (formulaMinus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral4 != null)
						{
							return ExcelExpressionHelper.Minus(formulaMinus.Left, ExcelExpressionHelper.Minus(formulaNumberLiteral4, formulaNumberLiteral));
						}
					}
				}
			}
			return new ExcelPlus(left, right);
		}

		// Token: 0x0600D26C RID: 53868 RVA: 0x002CD0D3 File Offset: 0x002CB2D3
		public static FormulaExpression Plus(FormulaExpression left, int right)
		{
			return ExcelExpressionHelper.Plus(left, ExcelExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D26D RID: 53869 RVA: 0x002CD0E1 File Offset: 0x002CB2E1
		public static FormulaExpression Plus1(FormulaExpression val)
		{
			return ExcelExpressionHelper.Plus(val, ExcelExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D26E RID: 53870 RVA: 0x002CD0EF File Offset: 0x002CB2EF
		public static FormulaExpression Product(IEnumerable<FormulaExpression> arguments)
		{
			return ExcelExpressionHelper.Func("Product", arguments);
		}

		// Token: 0x0600D26F RID: 53871 RVA: 0x002CD0FC File Offset: 0x002CB2FC
		public static FormulaExpression Proper(FormulaExpression str)
		{
			return ExcelExpressionHelper.Func("Proper", new FormulaExpression[] { str });
		}

		// Token: 0x0600D270 RID: 53872 RVA: 0x002CD112 File Offset: 0x002CB312
		public static FormulaExpression Range(FormulaExpression start, FormulaExpression end)
		{
			return new ExcelRange(start, end);
		}

		// Token: 0x0600D271 RID: 53873 RVA: 0x002CD11C File Offset: 0x002CB31C
		public static FormulaExpression Right(FormulaExpression str, FormulaExpression length)
		{
			ExcelNumberLiteral excelNumberLiteral = length as ExcelNumberLiteral;
			if (excelNumberLiteral == null || excelNumberLiteral.Value != 1.0)
			{
				return ExcelExpressionHelper.Func("Right", new FormulaExpression[] { str, length });
			}
			return ExcelExpressionHelper.Func("Right", new FormulaExpression[] { str });
		}

		// Token: 0x0600D272 RID: 53874 RVA: 0x002CD171 File Offset: 0x002CB371
		public static FormulaExpression Right(FormulaExpression str)
		{
			return ExcelExpressionHelper.Func("Right", new FormulaExpression[] { str });
		}

		// Token: 0x0600D273 RID: 53875 RVA: 0x002CD187 File Offset: 0x002CB387
		public static FormulaExpression Round(FormulaExpression value, int decimals)
		{
			return ExcelExpressionHelper.Round(value, ExcelExpressionHelper.NumberLiteral(decimals));
		}

		// Token: 0x0600D274 RID: 53876 RVA: 0x002CD198 File Offset: 0x002CB398
		public static FormulaExpression Round(FormulaExpression value, FormulaExpression decimals)
		{
			FormulaNumberLiteral formulaNumberLiteral = decimals as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 0.0)
			{
				return ExcelExpressionHelper.Func("Round", new FormulaExpression[] { value, decimals });
			}
			return ExcelExpressionHelper.Func("Round", new FormulaExpression[] { value });
		}

		// Token: 0x0600D275 RID: 53877 RVA: 0x002CD1ED File Offset: 0x002CB3ED
		public static FormulaExpression RoundDown(FormulaExpression value, int decimals)
		{
			return ExcelExpressionHelper.RoundDown(value, ExcelExpressionHelper.NumberLiteral(decimals));
		}

		// Token: 0x0600D276 RID: 53878 RVA: 0x002CD1FB File Offset: 0x002CB3FB
		public static FormulaExpression RoundDown(FormulaExpression value, FormulaExpression decimals)
		{
			return ExcelExpressionHelper.Func("RoundDown", new FormulaExpression[] { value, decimals });
		}

		// Token: 0x0600D277 RID: 53879 RVA: 0x002CD215 File Offset: 0x002CB415
		public static FormulaExpression RoundUp(FormulaExpression value, int decimals)
		{
			return ExcelExpressionHelper.RoundUp(value, ExcelExpressionHelper.NumberLiteral(decimals));
		}

		// Token: 0x0600D278 RID: 53880 RVA: 0x002CD223 File Offset: 0x002CB423
		public static FormulaExpression RoundUp(FormulaExpression value, FormulaExpression decimals)
		{
			return ExcelExpressionHelper.Func("RoundUp", new FormulaExpression[] { value, decimals });
		}

		// Token: 0x0600D279 RID: 53881 RVA: 0x002CD23D File Offset: 0x002CB43D
		public static FormulaExpression Row(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Row", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D27A RID: 53882 RVA: 0x002CD253 File Offset: 0x002CB453
		public static FormulaExpression Row()
		{
			return ExcelExpressionHelper.Func("Row", Array.Empty<FormulaExpression>());
		}

		// Token: 0x0600D27B RID: 53883 RVA: 0x002CD264 File Offset: 0x002CB464
		public static FormulaExpression Rows(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Rows", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D27C RID: 53884 RVA: 0x002CD27A File Offset: 0x002CB47A
		public static FormulaExpression Search(FormulaExpression findText, FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Search", new FormulaExpression[] { findText, subject });
		}

		// Token: 0x0600D27D RID: 53885 RVA: 0x002CD294 File Offset: 0x002CB494
		public static FormulaExpression Sort(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Sort", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D27E RID: 53886 RVA: 0x002CD2AA File Offset: 0x002CB4AA
		public static FormulaExpression StringLiteral(string value)
		{
			return new ExcelStringLiteral(value);
		}

		// Token: 0x0600D27F RID: 53887 RVA: 0x002CD2B2 File Offset: 0x002CB4B2
		public static FormulaExpression Substitute(FormulaExpression source, FormulaExpression findText, FormulaExpression replaceText, FormulaExpression instance = null)
		{
			return ExcelExpressionHelper.Func("Substitute", new FormulaExpression[] { source, findText, replaceText, instance });
		}

		// Token: 0x0600D280 RID: 53888 RVA: 0x002CD2D4 File Offset: 0x002CB4D4
		public static FormulaExpression Sum(IEnumerable<FormulaExpression> arguments)
		{
			return ExcelExpressionHelper.Func("Sum", arguments);
		}

		// Token: 0x0600D281 RID: 53889 RVA: 0x002CD2E1 File Offset: 0x002CB4E1
		public static FormulaExpression Text(FormulaExpression value, FormulaExpression format)
		{
			return ExcelExpressionHelper.Func("Text", new FormulaExpression[] { value, format });
		}

		// Token: 0x0600D282 RID: 53890 RVA: 0x002CD2FB File Offset: 0x002CB4FB
		public static FormulaExpression TextAfter(FormulaExpression subject, FormulaExpression delimiter, int instance)
		{
			return ExcelExpressionHelper.TextAfter(subject, delimiter, ExcelExpressionHelper.NumberLiteral(instance));
		}

		// Token: 0x0600D283 RID: 53891 RVA: 0x002CD30A File Offset: 0x002CB50A
		public static FormulaExpression TextAfter(FormulaExpression subject, FormulaExpression delimiter, FormulaExpression instance)
		{
			return ExcelExpressionHelper.Func("TextAfter", new FormulaExpression[] { subject, delimiter, instance });
		}

		// Token: 0x0600D284 RID: 53892 RVA: 0x002CD328 File Offset: 0x002CB528
		public static FormulaExpression TextBefore(FormulaExpression subject, FormulaExpression delimiter, int instance)
		{
			return ExcelExpressionHelper.TextBefore(subject, delimiter, ExcelExpressionHelper.NumberLiteral(instance));
		}

		// Token: 0x0600D285 RID: 53893 RVA: 0x002CD337 File Offset: 0x002CB537
		public static FormulaExpression TextBefore(FormulaExpression subject, FormulaExpression delimiter, FormulaExpression instance)
		{
			return ExcelExpressionHelper.Func("TextBefore", new FormulaExpression[] { subject, delimiter, instance });
		}

		// Token: 0x0600D286 RID: 53894 RVA: 0x002CD355 File Offset: 0x002CB555
		public static FormulaExpression TextSlice(FormulaExpression subject, FormulaExpression startPosition, FormulaExpression endPosition)
		{
			return ExcelExpressionHelper.Func("TextSlice", new FormulaExpression[] { subject, startPosition, endPosition });
		}

		// Token: 0x0600D287 RID: 53895 RVA: 0x002CD373 File Offset: 0x002CB573
		public static FormulaExpression TextSplit(FormulaExpression subject, FormulaExpression delimiter)
		{
			return ExcelExpressionHelper.Func("TextSplit", new FormulaExpression[] { subject, delimiter });
		}

		// Token: 0x0600D288 RID: 53896 RVA: 0x002CD38D File Offset: 0x002CB58D
		public static FormulaExpression TimeValue(FormulaExpression value)
		{
			return ExcelExpressionHelper.Func("TimeValue", new FormulaExpression[] { value });
		}

		// Token: 0x0600D289 RID: 53897 RVA: 0x002CD3A3 File Offset: 0x002CB5A3
		public static FormulaExpression Trim(FormulaExpression str)
		{
			return ExcelExpressionHelper.Func("Trim", new FormulaExpression[] { str });
		}

		// Token: 0x0600D28A RID: 53898 RVA: 0x002CD3B9 File Offset: 0x002CB5B9
		public static FormulaExpression Upper(FormulaExpression str)
		{
			return ExcelExpressionHelper.Func("Upper", new FormulaExpression[] { str });
		}

		// Token: 0x0600D28B RID: 53899 RVA: 0x002CD3CF File Offset: 0x002CB5CF
		public static FormulaExpression Value(FormulaExpression value)
		{
			return ExcelExpressionHelper.Func("Value", new FormulaExpression[] { value });
		}

		// Token: 0x0600D28C RID: 53900 RVA: 0x002CD3E5 File Offset: 0x002CB5E5
		public static FormulaExpression Variable(string name)
		{
			return new ExcelVariable(name);
		}

		// Token: 0x0600D28D RID: 53901 RVA: 0x002CD3ED File Offset: 0x002CB5ED
		public static FormulaExpression Date(FormulaExpression year, FormulaExpression month, FormulaExpression day)
		{
			return ExcelExpressionHelper.Func("Date", new FormulaExpression[] { year, month, day });
		}

		// Token: 0x0600D28E RID: 53902 RVA: 0x002CD40B File Offset: 0x002CB60B
		public static FormulaExpression Date(FormulaExpression year, int month, int day)
		{
			return ExcelExpressionHelper.Func("Date", new FormulaExpression[]
			{
				year,
				ExcelExpressionHelper.NumberLiteral(month),
				ExcelExpressionHelper.NumberLiteral(day)
			});
		}

		// Token: 0x0600D28F RID: 53903 RVA: 0x002CD433 File Offset: 0x002CB633
		public static FormulaExpression Date(int year, FormulaExpression month, int day)
		{
			return ExcelExpressionHelper.Func("Date", new FormulaExpression[]
			{
				ExcelExpressionHelper.NumberLiteral(year),
				month,
				ExcelExpressionHelper.NumberLiteral(day)
			});
		}

		// Token: 0x0600D290 RID: 53904 RVA: 0x002CD45B File Offset: 0x002CB65B
		public static FormulaExpression DateAddDays(FormulaExpression subject, double amount)
		{
			return ExcelExpressionHelper.DateAddDays(subject, ExcelExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D291 RID: 53905 RVA: 0x002CD469 File Offset: 0x002CB669
		public static FormulaExpression DateAddDays(FormulaExpression subject, FormulaExpression amount)
		{
			return ExcelExpressionHelper.Plus(subject, amount);
		}

		// Token: 0x0600D292 RID: 53906 RVA: 0x002CD472 File Offset: 0x002CB672
		public static FormulaExpression DateAddHours(FormulaExpression subject, double amount)
		{
			return ExcelExpressionHelper.DateAddHours(subject, ExcelExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D293 RID: 53907 RVA: 0x002CD480 File Offset: 0x002CB680
		public static FormulaExpression DateAddHours(FormulaExpression subject, FormulaExpression amount)
		{
			return ExcelExpressionHelper.Plus(subject, ExcelExpressionHelper.Divide(amount, 24.0));
		}

		// Token: 0x0600D294 RID: 53908 RVA: 0x002CD497 File Offset: 0x002CB697
		public static FormulaExpression DateAddMinutes(FormulaExpression subject, double amount)
		{
			return ExcelExpressionHelper.DateAddMinutes(subject, ExcelExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D295 RID: 53909 RVA: 0x002CD4A5 File Offset: 0x002CB6A5
		public static FormulaExpression DateAddMinutes(FormulaExpression subject, FormulaExpression amount)
		{
			return ExcelExpressionHelper.Plus(subject, ExcelExpressionHelper.Divide(amount, 1440.0));
		}

		// Token: 0x0600D296 RID: 53910 RVA: 0x002CD4BC File Offset: 0x002CB6BC
		public static FormulaExpression DateAddMonths(FormulaExpression subject, double amount)
		{
			return ExcelExpressionHelper.DateAddMonths(subject, ExcelExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D297 RID: 53911 RVA: 0x002CD4CA File Offset: 0x002CB6CA
		public static FormulaExpression DateAddMonths(FormulaExpression subject, FormulaExpression amount)
		{
			return ExcelExpressionHelper.Func("EDate", new FormulaExpression[] { subject, amount });
		}

		// Token: 0x0600D298 RID: 53912 RVA: 0x002CD4E4 File Offset: 0x002CB6E4
		public static FormulaExpression DateAddSeconds(FormulaExpression subject, double amount)
		{
			return ExcelExpressionHelper.DateAddSeconds(subject, ExcelExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D299 RID: 53913 RVA: 0x002CD4F2 File Offset: 0x002CB6F2
		public static FormulaExpression DateAddSeconds(FormulaExpression subject, FormulaExpression amount)
		{
			return ExcelExpressionHelper.Plus(subject, ExcelExpressionHelper.Divide(amount, 86400.0));
		}

		// Token: 0x0600D29A RID: 53914 RVA: 0x002CD509 File Offset: 0x002CB709
		public static FormulaExpression DateAddYears(FormulaExpression subject, double amount)
		{
			return ExcelExpressionHelper.DateAddYears(subject, ExcelExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D29B RID: 53915 RVA: 0x002CD517 File Offset: 0x002CB717
		public static FormulaExpression DateAddYears(FormulaExpression subject, FormulaExpression amount)
		{
			return ExcelExpressionHelper.Date(ExcelExpressionHelper.Plus(ExcelExpressionHelper.Year(subject), amount), ExcelExpressionHelper.Month(subject), ExcelExpressionHelper.Day(subject));
		}

		// Token: 0x0600D29C RID: 53916 RVA: 0x002CD536 File Offset: 0x002CB736
		public static FormulaExpression DateDif(FormulaExpression start, FormulaExpression end)
		{
			return ExcelExpressionHelper.Func("DateDif", new FormulaExpression[]
			{
				start,
				end,
				ExcelExpressionHelper.StringLiteral("D")
			});
		}

		// Token: 0x0600D29D RID: 53917 RVA: 0x002CD55D File Offset: 0x002CB75D
		public static FormulaExpression DateTime(int year, int month = 1, int day = 1, int hour = 0, int minute = 0, int second = 0)
		{
			return ExcelExpressionHelper.DateTime(ExcelExpressionHelper.NumberLiteral(year), ExcelExpressionHelper.NumberLiteral(month), ExcelExpressionHelper.NumberLiteral(day), ExcelExpressionHelper.NumberLiteral(hour), ExcelExpressionHelper.NumberLiteral(minute), ExcelExpressionHelper.NumberLiteral(second));
		}

		// Token: 0x0600D29E RID: 53918 RVA: 0x002CD58A File Offset: 0x002CB78A
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day)
		{
			return ExcelExpressionHelper.Plus(ExcelExpressionHelper.Date(year, month, day), ExcelExpressionHelper.Time(ExcelExpressionHelper.NumberLiteral(0), ExcelExpressionHelper.NumberLiteral(0), ExcelExpressionHelper.NumberLiteral(0)));
		}

		// Token: 0x0600D29F RID: 53919 RVA: 0x002CD5B0 File Offset: 0x002CB7B0
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day, FormulaExpression hour)
		{
			return ExcelExpressionHelper.Plus(ExcelExpressionHelper.Date(year, month, day), ExcelExpressionHelper.Time(hour, ExcelExpressionHelper.NumberLiteral(0), ExcelExpressionHelper.NumberLiteral(0)));
		}

		// Token: 0x0600D2A0 RID: 53920 RVA: 0x002CD5D1 File Offset: 0x002CB7D1
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day, FormulaExpression hour, FormulaExpression minute)
		{
			return ExcelExpressionHelper.Plus(ExcelExpressionHelper.Date(year, month, day), ExcelExpressionHelper.Time(hour, minute, ExcelExpressionHelper.NumberLiteral(0)));
		}

		// Token: 0x0600D2A1 RID: 53921 RVA: 0x002CD5EE File Offset: 0x002CB7EE
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month, FormulaExpression day, FormulaExpression hour, FormulaExpression minute, FormulaExpression second)
		{
			return ExcelExpressionHelper.Plus(ExcelExpressionHelper.Date(year, month, day), ExcelExpressionHelper.Time(hour, minute, second));
		}

		// Token: 0x0600D2A2 RID: 53922 RVA: 0x002CD608 File Offset: 0x002CB808
		public static FormulaExpression Day(FormulaExpression subject)
		{
			ExcelFunc excelFunc = subject as ExcelFunc;
			if (excelFunc != null && excelFunc.Name == "Date")
			{
				return excelFunc.Children.ElementAtOrDefault(1);
			}
			ExcelPlus excelPlus = subject as ExcelPlus;
			if (excelPlus != null)
			{
				ExcelFunc excelFunc2 = excelPlus.Left as ExcelFunc;
				if (excelFunc2 != null && excelFunc2.Name == "DateValue")
				{
					ExcelFunc excelFunc3 = excelPlus.Right as ExcelFunc;
					if (excelFunc3 != null && excelFunc3.Name == "TimeValue")
					{
						subject = excelFunc2;
					}
				}
			}
			return ExcelExpressionHelper.Func("Day", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D2A3 RID: 53923 RVA: 0x002CD6A1 File Offset: 0x002CB8A1
		public static FormulaExpression DayEnd(FormulaExpression dayStart)
		{
			return ExcelExpressionHelper.DateAddDays(dayStart, 1.0);
		}

		// Token: 0x0600D2A4 RID: 53924 RVA: 0x002CD6B2 File Offset: 0x002CB8B2
		public static FormulaExpression DayStart(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Date(ExcelExpressionHelper.Year(subject), ExcelExpressionHelper.Month(subject), ExcelExpressionHelper.Day(subject));
		}

		// Token: 0x0600D2A5 RID: 53925 RVA: 0x002CD6CB File Offset: 0x002CB8CB
		public static FormulaExpression EoMonth(FormulaExpression subject, FormulaExpression months = null)
		{
			return ExcelExpressionHelper.Func("EoMonth", new FormulaExpression[]
			{
				subject,
				months ?? ExcelExpressionHelper.NumberLiteral(0)
			});
		}

		// Token: 0x0600D2A6 RID: 53926 RVA: 0x002CD6F0 File Offset: 0x002CB8F0
		public static FormulaExpression Hour(FormulaExpression subject)
		{
			ExcelFunc excelFunc = subject as ExcelFunc;
			if (excelFunc != null && excelFunc.Name == "Time")
			{
				return excelFunc.Children.ElementAtOrDefault(1);
			}
			ExcelPlus excelPlus = subject as ExcelPlus;
			if (excelPlus != null)
			{
				ExcelFunc excelFunc2 = excelPlus.Left as ExcelFunc;
				if (excelFunc2 != null && excelFunc2.Name == "DateValue")
				{
					ExcelFunc excelFunc3 = excelPlus.Right as ExcelFunc;
					if (excelFunc3 != null && excelFunc3.Name == "TimeValue")
					{
						subject = excelFunc3;
					}
				}
			}
			return ExcelExpressionHelper.Func("Hour", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D2A7 RID: 53927 RVA: 0x002CD789 File Offset: 0x002CB989
		public static FormulaExpression HourEnd(FormulaExpression hourStart)
		{
			return ExcelExpressionHelper.DateAddHours(hourStart, 1.0);
		}

		// Token: 0x0600D2A8 RID: 53928 RVA: 0x002CD79A File Offset: 0x002CB99A
		public static FormulaExpression HourStart(FormulaExpression subject)
		{
			return ExcelExpressionHelper.DateTime(ExcelExpressionHelper.Year(subject), ExcelExpressionHelper.Month(subject), ExcelExpressionHelper.Day(subject), ExcelExpressionHelper.Hour(subject));
		}

		// Token: 0x0600D2A9 RID: 53929 RVA: 0x002CD7B9 File Offset: 0x002CB9B9
		public static FormulaExpression Midpoint(FormulaExpression start, FormulaExpression end)
		{
			return ExcelExpressionHelper.Plus(start, ExcelExpressionHelper.Divide(ExcelExpressionHelper.Minus(end, start), 2.0));
		}

		// Token: 0x0600D2AA RID: 53930 RVA: 0x002CD7D8 File Offset: 0x002CB9D8
		public static FormulaExpression Minute(FormulaExpression subject)
		{
			ExcelFunc excelFunc = subject as ExcelFunc;
			if (excelFunc != null && excelFunc.Name == "Time")
			{
				return excelFunc.Children.ElementAtOrDefault(1);
			}
			ExcelPlus excelPlus = subject as ExcelPlus;
			if (excelPlus != null)
			{
				ExcelFunc excelFunc2 = excelPlus.Left as ExcelFunc;
				if (excelFunc2 != null && excelFunc2.Name == "DateValue")
				{
					ExcelFunc excelFunc3 = excelPlus.Right as ExcelFunc;
					if (excelFunc3 != null && excelFunc3.Name == "TimeValue")
					{
						subject = excelFunc3;
					}
				}
			}
			return ExcelExpressionHelper.Func("Minute", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D2AB RID: 53931 RVA: 0x002CD871 File Offset: 0x002CBA71
		public static FormulaExpression MinuteEnd(FormulaExpression minuteStart)
		{
			return ExcelExpressionHelper.DateAddMinutes(minuteStart, 1.0);
		}

		// Token: 0x0600D2AC RID: 53932 RVA: 0x002CD882 File Offset: 0x002CBA82
		public static FormulaExpression MinuteStart(FormulaExpression subject)
		{
			return ExcelExpressionHelper.DateTime(ExcelExpressionHelper.Year(subject), ExcelExpressionHelper.Month(subject), ExcelExpressionHelper.Day(subject), ExcelExpressionHelper.Hour(subject), ExcelExpressionHelper.Minute(subject));
		}

		// Token: 0x0600D2AD RID: 53933 RVA: 0x002CD8A8 File Offset: 0x002CBAA8
		public static FormulaExpression Month(FormulaExpression subject)
		{
			ExcelFunc excelFunc = subject as ExcelFunc;
			if (excelFunc != null && excelFunc.Name == "Date")
			{
				return excelFunc.Children.ElementAtOrDefault(1);
			}
			ExcelPlus excelPlus = subject as ExcelPlus;
			if (excelPlus != null)
			{
				ExcelFunc excelFunc2 = excelPlus.Left as ExcelFunc;
				if (excelFunc2 != null && excelFunc2.Name == "DateValue")
				{
					ExcelFunc excelFunc3 = excelPlus.Right as ExcelFunc;
					if (excelFunc3 != null && excelFunc3.Name == "TimeValue")
					{
						subject = excelFunc2;
					}
				}
			}
			return ExcelExpressionHelper.Func("Month", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D2AE RID: 53934 RVA: 0x002CD941 File Offset: 0x002CBB41
		public static FormulaExpression MonthDays(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Day(ExcelExpressionHelper.EoMonth(subject, null));
		}

		// Token: 0x0600D2AF RID: 53935 RVA: 0x002CD94F File Offset: 0x002CBB4F
		public static FormulaExpression MonthEnd(FormulaExpression monthStart)
		{
			return ExcelExpressionHelper.DateAddMonths(monthStart, 1.0);
		}

		// Token: 0x0600D2B0 RID: 53936 RVA: 0x002CD960 File Offset: 0x002CBB60
		public static FormulaExpression MonthStart(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Date(ExcelExpressionHelper.Year(subject), ExcelExpressionHelper.Month(subject), ExcelExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D2B1 RID: 53937 RVA: 0x002CD979 File Offset: 0x002CBB79
		public static FormulaExpression MonthWeek(FormulaExpression subject, FormulaExpression monthStart = null)
		{
			if (monthStart == null)
			{
				monthStart = ExcelExpressionHelper.MonthStart(subject);
			}
			return ExcelExpressionHelper.RoundUp(ExcelExpressionHelper.Divide(ExcelExpressionHelper.Plus(ExcelExpressionHelper.DateDif(monthStart, subject), ExcelExpressionHelper.Weekday(monthStart)), 7.0), 0);
		}

		// Token: 0x0600D2B2 RID: 53938 RVA: 0x002CD9AC File Offset: 0x002CBBAC
		public static FormulaExpression Quarter(FormulaExpression subject)
		{
			return ExcelExpressionHelper.RoundUp(ExcelExpressionHelper.Divide(ExcelExpressionHelper.Month(subject), 3.0), 0);
		}

		// Token: 0x0600D2B3 RID: 53939 RVA: 0x002CD9C8 File Offset: 0x002CBBC8
		public static FormulaExpression QuarterDay(FormulaExpression subject, FormulaExpression quarter = null, FormulaExpression quarterStart = null)
		{
			if (quarter == null)
			{
				quarter = ExcelExpressionHelper.Quarter(subject);
			}
			if (quarterStart == null)
			{
				quarterStart = ExcelExpressionHelper.QuarterStart(subject, quarter);
			}
			return ExcelExpressionHelper.Plus1(ExcelExpressionHelper.DateDif(quarterStart, subject));
		}

		// Token: 0x0600D2B4 RID: 53940 RVA: 0x002CD9ED File Offset: 0x002CBBED
		public static FormulaExpression QuarterDays(FormulaExpression subject, FormulaExpression quarter = null, FormulaExpression quarterStart = null, FormulaExpression quarterEnd = null)
		{
			if (quarter == null)
			{
				quarter = ExcelExpressionHelper.Quarter(subject);
			}
			if (quarterStart == null)
			{
				quarterStart = ExcelExpressionHelper.QuarterStart(subject, quarter);
			}
			if (quarterEnd == null)
			{
				quarterEnd = ExcelExpressionHelper.QuarterEnd(subject, quarter, null);
			}
			return ExcelExpressionHelper.DateDif(quarterStart, quarterEnd);
		}

		// Token: 0x0600D2B5 RID: 53941 RVA: 0x002CDA1A File Offset: 0x002CBC1A
		public static FormulaExpression QuarterEnd(FormulaExpression subject, FormulaExpression quarter = null, FormulaExpression quarterStart = null)
		{
			if (quarter == null)
			{
				quarter = ExcelExpressionHelper.Quarter(subject);
			}
			if (quarterStart == null)
			{
				quarterStart = ExcelExpressionHelper.QuarterStart(subject, quarter);
			}
			return ExcelExpressionHelper.DateAddMonths(quarterStart, 3.0);
		}

		// Token: 0x0600D2B6 RID: 53942 RVA: 0x002CDA42 File Offset: 0x002CBC42
		public static FormulaExpression QuarterStart(FormulaExpression subject, FormulaExpression quarter = null)
		{
			if (quarter == null)
			{
				quarter = ExcelExpressionHelper.Quarter(subject);
			}
			return ExcelExpressionHelper.Date(ExcelExpressionHelper.Year(subject), ExcelExpressionHelper.Minus(ExcelExpressionHelper.Multiply(3.0, quarter), 2.0), ExcelExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D2B7 RID: 53943 RVA: 0x002CDA7D File Offset: 0x002CBC7D
		public static FormulaExpression QuarterWeek(FormulaExpression subject, FormulaExpression quarter = null, FormulaExpression quarterStart = null)
		{
			if (quarter == null)
			{
				quarter = ExcelExpressionHelper.Quarter(subject);
			}
			if (quarterStart == null)
			{
				quarterStart = ExcelExpressionHelper.QuarterStart(subject, quarter);
			}
			return ExcelExpressionHelper.RoundUp(ExcelExpressionHelper.Divide(ExcelExpressionHelper.Plus(ExcelExpressionHelper.DateDif(quarterStart, subject), ExcelExpressionHelper.Weekday(quarterStart)), 7.0), 0);
		}

		// Token: 0x0600D2B8 RID: 53944 RVA: 0x002CDABC File Offset: 0x002CBCBC
		public static FormulaExpression Second(FormulaExpression subject)
		{
			ExcelFunc excelFunc = subject as ExcelFunc;
			if (excelFunc != null && excelFunc.Name == "Time")
			{
				return excelFunc.Children.ElementAtOrDefault(1);
			}
			ExcelPlus excelPlus = subject as ExcelPlus;
			if (excelPlus != null)
			{
				ExcelFunc excelFunc2 = excelPlus.Left as ExcelFunc;
				if (excelFunc2 != null && excelFunc2.Name == "DateValue")
				{
					ExcelFunc excelFunc3 = excelPlus.Right as ExcelFunc;
					if (excelFunc3 != null && excelFunc3.Name == "TimeValue")
					{
						subject = excelFunc3;
					}
				}
			}
			return ExcelExpressionHelper.Func("Second", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D2B9 RID: 53945 RVA: 0x002CDB55 File Offset: 0x002CBD55
		public static FormulaExpression SecondEnd(FormulaExpression secondStart)
		{
			return ExcelExpressionHelper.DateAddSeconds(secondStart, 1.0);
		}

		// Token: 0x0600D2BA RID: 53946 RVA: 0x002CDB66 File Offset: 0x002CBD66
		public static FormulaExpression SecondStart(FormulaExpression subject)
		{
			return ExcelExpressionHelper.DateTime(ExcelExpressionHelper.Year(subject), ExcelExpressionHelper.Month(subject), ExcelExpressionHelper.Day(subject), ExcelExpressionHelper.Hour(subject), ExcelExpressionHelper.Minute(subject), ExcelExpressionHelper.Second(subject));
		}

		// Token: 0x0600D2BB RID: 53947 RVA: 0x002CDB91 File Offset: 0x002CBD91
		public static FormulaExpression Time(FormulaExpression hour, FormulaExpression minute, FormulaExpression second)
		{
			return ExcelExpressionHelper.Func("Time", new FormulaExpression[] { hour, minute, second });
		}

		// Token: 0x0600D2BC RID: 53948 RVA: 0x002CDBAF File Offset: 0x002CBDAF
		public static FormulaExpression Weekday(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("Weekday", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D2BD RID: 53949 RVA: 0x002CDBC5 File Offset: 0x002CBDC5
		public static FormulaExpression WeekEnd(FormulaExpression weekStart)
		{
			return ExcelExpressionHelper.DateAddDays(weekStart, 7.0);
		}

		// Token: 0x0600D2BE RID: 53950 RVA: 0x002CDBD6 File Offset: 0x002CBDD6
		public static FormulaExpression WeekNum(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Func("WeekNum", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D2BF RID: 53951 RVA: 0x002CDBEC File Offset: 0x002CBDEC
		public static FormulaExpression WeekStart(FormulaExpression subject)
		{
			return ExcelExpressionHelper.DateAddDays(ExcelExpressionHelper.Date(ExcelExpressionHelper.Year(subject), ExcelExpressionHelper.Month(subject), ExcelExpressionHelper.Day(subject)), ExcelExpressionHelper.Minus(1.0, ExcelExpressionHelper.Weekday(subject)));
		}

		// Token: 0x0600D2C0 RID: 53952 RVA: 0x002CDC20 File Offset: 0x002CBE20
		public static FormulaExpression Year(FormulaExpression subject)
		{
			ExcelFunc excelFunc = subject as ExcelFunc;
			if (excelFunc != null && excelFunc.Name == "Time")
			{
				return excelFunc.Children.ElementAtOrDefault(1);
			}
			ExcelPlus excelPlus = subject as ExcelPlus;
			if (excelPlus != null)
			{
				ExcelFunc excelFunc2 = excelPlus.Left as ExcelFunc;
				if (excelFunc2 != null && excelFunc2.Name == "DateValue")
				{
					ExcelFunc excelFunc3 = excelPlus.Right as ExcelFunc;
					if (excelFunc3 != null && excelFunc3.Name == "TimeValue")
					{
						subject = excelFunc2;
					}
				}
			}
			return ExcelExpressionHelper.Func("Year", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D2C1 RID: 53953 RVA: 0x002CDCB9 File Offset: 0x002CBEB9
		public static FormulaExpression YearDay(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Plus1(ExcelExpressionHelper.DateDif(ExcelExpressionHelper.YearStart(subject), subject));
		}

		// Token: 0x0600D2C2 RID: 53954 RVA: 0x002CDCCC File Offset: 0x002CBECC
		public static FormulaExpression YearDays(FormulaExpression subject, FormulaExpression yearStart = null, FormulaExpression yearEnd = null)
		{
			return ExcelExpressionHelper.If(ExcelExpressionHelper.Equal(ExcelExpressionHelper.Mod(ExcelExpressionHelper.Year(subject), 4.0), ExcelExpressionHelper.NumberLiteral(0)), ExcelExpressionHelper.NumberLiteral(366), ExcelExpressionHelper.NumberLiteral(365));
		}

		// Token: 0x0600D2C3 RID: 53955 RVA: 0x002CDD06 File Offset: 0x002CBF06
		public static FormulaExpression YearEnd(FormulaExpression yearStart)
		{
			return ExcelExpressionHelper.DateAddYears(yearStart, 1.0);
		}

		// Token: 0x0600D2C4 RID: 53956 RVA: 0x002CDD17 File Offset: 0x002CBF17
		public static FormulaExpression YearStart(FormulaExpression subject)
		{
			return ExcelExpressionHelper.Date(ExcelExpressionHelper.Year(subject), ExcelExpressionHelper.NumberLiteral(1), ExcelExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D2C5 RID: 53957 RVA: 0x002CDD30 File Offset: 0x002CBF30
		public static FormulaExpression YearWeek(FormulaExpression subject)
		{
			return ExcelExpressionHelper.WeekNum(subject);
		}
	}
}

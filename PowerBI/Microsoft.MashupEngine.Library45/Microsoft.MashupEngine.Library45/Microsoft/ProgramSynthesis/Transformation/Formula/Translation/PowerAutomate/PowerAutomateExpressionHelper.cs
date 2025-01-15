using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x0200190E RID: 6414
	internal static class PowerAutomateExpressionHelper
	{
		// Token: 0x0600D109 RID: 53513 RVA: 0x002C8FEC File Offset: 0x002C71EC
		public static FormulaExpression Coalesce(params FormulaExpression[] arguments)
		{
			return PowerAutomateExpressionHelper.Func("Coalesce", arguments);
		}

		// Token: 0x0600D10A RID: 53514 RVA: 0x002C8FF9 File Offset: 0x002C71F9
		public static FormulaExpression Concat(IEnumerable<FormulaExpression> children)
		{
			return PowerAutomateExpressionHelper.Concat(children.ToArray<FormulaExpression>());
		}

		// Token: 0x0600D10B RID: 53515 RVA: 0x002C9008 File Offset: 0x002C7208
		public static FormulaExpression Concat(params FormulaExpression[] args)
		{
			List<FormulaExpression> list = new List<FormulaExpression>();
			int i = 0;
			while (i < args.Length)
			{
				FormulaExpression formulaExpression = args[i];
				PowerAutomateStringLiteral powerAutomateStringLiteral = formulaExpression as PowerAutomateStringLiteral;
				if (powerAutomateStringLiteral == null)
				{
					goto IL_0055;
				}
				PowerAutomateStringLiteral powerAutomateStringLiteral2 = list.LastOrDefault<FormulaExpression>() as PowerAutomateStringLiteral;
				if (powerAutomateStringLiteral2 == null)
				{
					goto IL_0055;
				}
				list[list.Count - 1] = PowerAutomateExpressionHelper.StringLiteral(powerAutomateStringLiteral2.Value + powerAutomateStringLiteral.Value);
				IL_005C:
				i++;
				continue;
				IL_0055:
				list.Add(formulaExpression);
				goto IL_005C;
			}
			return new PowerAutomateConcat(list);
		}

		// Token: 0x0600D10C RID: 53516 RVA: 0x002C9081 File Offset: 0x002C7281
		public static FormulaExpression CreateArray(params FormulaExpression[] arguments)
		{
			return PowerAutomateExpressionHelper.Func("CreateArray", arguments);
		}

		// Token: 0x0600D10D RID: 53517 RVA: 0x002C908E File Offset: 0x002C728E
		public static FormulaExpression DateTimeLiteral(DateTime value)
		{
			return new PowerAutomateDateTimeLiteral(value);
		}

		// Token: 0x0600D10E RID: 53518 RVA: 0x002C9096 File Offset: 0x002C7296
		public static FormulaExpression DateValue(FormulaExpression value)
		{
			return PowerAutomateExpressionHelper.Func("DateValue", new FormulaExpression[] { value });
		}

		// Token: 0x0600D10F RID: 53519 RVA: 0x002C90AC File Offset: 0x002C72AC
		public static FormulaExpression Empty(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("Empty", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D110 RID: 53520 RVA: 0x002C90C2 File Offset: 0x002C72C2
		public static FormulaExpression EndsWith(FormulaExpression conditionExp, FormulaExpression trueExp, FormulaExpression falseExp)
		{
			return PowerAutomateExpressionHelper.Func("EndsWith", new FormulaExpression[] { conditionExp, trueExp, falseExp });
		}

		// Token: 0x0600D111 RID: 53521 RVA: 0x002C90E0 File Offset: 0x002C72E0
		public static FormulaExpression Equal(FormulaExpression left, FormulaExpression right)
		{
			return PowerAutomateExpressionHelper.Func("Equals", new FormulaExpression[] { left, right });
		}

		// Token: 0x0600D112 RID: 53522 RVA: 0x002C90FA File Offset: 0x002C72FA
		public static FormulaExpression Float(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("Float", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D113 RID: 53523 RVA: 0x002C9110 File Offset: 0x002C7310
		public static FormulaExpression FormatDateTime(FormulaExpression subject, FormulaExpression format, FormulaExpression locale)
		{
			return PowerAutomateExpressionHelper.Func("FormatDateTime", new FormulaExpression[] { subject, format, locale });
		}

		// Token: 0x0600D114 RID: 53524 RVA: 0x002C912E File Offset: 0x002C732E
		public static FormulaExpression FormatNumber(FormulaExpression subject, FormulaExpression format)
		{
			return PowerAutomateExpressionHelper.Func("FormatNumber", new FormulaExpression[] { subject, format });
		}

		// Token: 0x0600D115 RID: 53525 RVA: 0x002C9148 File Offset: 0x002C7348
		public static FormulaExpression FormatNumber(FormulaExpression subject, FormulaExpression format, FormulaExpression localeExp)
		{
			return PowerAutomateExpressionHelper.Func("FormatNumber", new FormulaExpression[] { subject, format, localeExp });
		}

		// Token: 0x0600D116 RID: 53526 RVA: 0x002C9166 File Offset: 0x002C7366
		public static FormulaExpression Func(string name, params FormulaExpression[] arguments)
		{
			return new PowerAutomateFunc(name, arguments);
		}

		// Token: 0x0600D117 RID: 53527 RVA: 0x002C916F File Offset: 0x002C736F
		public static FormulaExpression If(FormulaExpression conditionExp, FormulaExpression trueExp, FormulaExpression falseExp)
		{
			return PowerAutomateExpressionHelper.Func("If", new FormulaExpression[] { conditionExp, trueExp, falseExp });
		}

		// Token: 0x0600D118 RID: 53528 RVA: 0x002C918D File Offset: 0x002C738D
		public static FormulaExpression Index(FormulaExpression subject, FormulaExpression instance)
		{
			return new PowerAutomateIndex(subject, instance);
		}

		// Token: 0x0600D119 RID: 53529 RVA: 0x002C9196 File Offset: 0x002C7396
		public static FormulaExpression IndexOf(FormulaExpression subject, FormulaExpression findText)
		{
			return PowerAutomateExpressionHelper.Func("IndexOf", new FormulaExpression[] { subject, findText });
		}

		// Token: 0x0600D11A RID: 53530 RVA: 0x002C91B0 File Offset: 0x002C73B0
		public static FormulaExpression Int(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("Int", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D11B RID: 53531 RVA: 0x002C91C6 File Offset: 0x002C73C6
		public static FormulaExpression Intersection(params FormulaExpression[] arguments)
		{
			return PowerAutomateExpressionHelper.Func("Intersection", arguments);
		}

		// Token: 0x0600D11C RID: 53532 RVA: 0x002C91D3 File Offset: 0x002C73D3
		public static FormulaExpression IsFloat(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("IsFloat", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D11D RID: 53533 RVA: 0x002C91E9 File Offset: 0x002C73E9
		public static FormulaExpression IsInt(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("IsInt", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D11E RID: 53534 RVA: 0x002C91FF File Offset: 0x002C73FF
		public static FormulaExpression Join(FormulaExpression subject, FormulaExpression separator)
		{
			return PowerAutomateExpressionHelper.Func("Join", new FormulaExpression[] { subject, separator });
		}

		// Token: 0x0600D11F RID: 53535 RVA: 0x002C9219 File Offset: 0x002C7419
		public static FormulaExpression LastIndexOf(FormulaExpression subject, FormulaExpression delimiter)
		{
			return PowerAutomateExpressionHelper.Func("LastIndexOf", new FormulaExpression[] { subject, delimiter });
		}

		// Token: 0x0600D120 RID: 53536 RVA: 0x002C9234 File Offset: 0x002C7434
		public static FormulaExpression Length(FormulaExpression subject)
		{
			IFormulaLiteral<string> formulaLiteral = subject as IFormulaLiteral<string>;
			if (formulaLiteral == null)
			{
				return PowerAutomateExpressionHelper.Func("Length", new FormulaExpression[] { subject });
			}
			return PowerAutomateExpressionHelper.NumberLiteral(formulaLiteral.Value.Length);
		}

		// Token: 0x0600D121 RID: 53537 RVA: 0x002C9270 File Offset: 0x002C7470
		public static FormulaExpression Not(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("Not", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D122 RID: 53538 RVA: 0x002C9286 File Offset: 0x002C7486
		public static FormulaExpression NthIndexOf(FormulaExpression subject, FormulaExpression findText, FormulaExpression instance)
		{
			return PowerAutomateExpressionHelper.Func("NthIndexOf", new FormulaExpression[] { subject, findText, instance });
		}

		// Token: 0x0600D123 RID: 53539 RVA: 0x002C92A4 File Offset: 0x002C74A4
		public static FormulaExpression NumberLiteral(int value)
		{
			return PowerAutomateExpressionHelper.NumberLiteral(Convert.ToDecimal(value));
		}

		// Token: 0x0600D124 RID: 53540 RVA: 0x002C92B1 File Offset: 0x002C74B1
		public static FormulaExpression NumberLiteral(decimal value)
		{
			return PowerAutomateExpressionHelper.NumberLiteral(Convert.ToDouble(value));
		}

		// Token: 0x0600D125 RID: 53541 RVA: 0x002C92BE File Offset: 0x002C74BE
		public static FormulaExpression NumberLiteral(double value)
		{
			return new PowerAutomateNumberLiteral(value);
		}

		// Token: 0x0600D126 RID: 53542 RVA: 0x002C92C6 File Offset: 0x002C74C6
		public static FormulaExpression NumberValue(FormulaExpression value)
		{
			return PowerAutomateExpressionHelper.Func("NumberValue", new FormulaExpression[] { value });
		}

		// Token: 0x0600D127 RID: 53543 RVA: 0x002C92DC File Offset: 0x002C74DC
		public static FormulaExpression NumberValue(FormulaExpression value, FormulaExpression decimalSeparator, FormulaExpression groupSeparator)
		{
			return PowerAutomateExpressionHelper.Func("NumberValue", new FormulaExpression[] { value, decimalSeparator, groupSeparator });
		}

		// Token: 0x0600D128 RID: 53544 RVA: 0x002C92FA File Offset: 0x002C74FA
		public static FormulaExpression Or(FormulaExpression left, FormulaExpression right)
		{
			return PowerAutomateExpressionHelper.Func("Or", new FormulaExpression[] { left, right });
		}

		// Token: 0x0600D129 RID: 53545 RVA: 0x002C9314 File Offset: 0x002C7514
		public static FormulaExpression ParseDateTime(FormulaExpression subject, FormulaExpression locale, FormulaExpression format)
		{
			return PowerAutomateExpressionHelper.Func("ParseDateTime", new FormulaExpression[] { subject, locale, format });
		}

		// Token: 0x0600D12A RID: 53546 RVA: 0x002C9332 File Offset: 0x002C7532
		public static FormulaExpression ParseDateTime(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("ParseDateTime", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D12B RID: 53547 RVA: 0x002C9348 File Offset: 0x002C7548
		public static FormulaExpression Replace(FormulaExpression subject, FormulaExpression findText, FormulaExpression replacement)
		{
			return PowerAutomateExpressionHelper.Func("Replace", new FormulaExpression[] { subject, findText, replacement });
		}

		// Token: 0x0600D12C RID: 53548 RVA: 0x002C9366 File Offset: 0x002C7566
		public static FormulaExpression Slice(FormulaExpression subject, FormulaExpression startIndex)
		{
			return new PowerAutomateSlice(subject, startIndex, null);
		}

		// Token: 0x0600D12D RID: 53549 RVA: 0x002C9370 File Offset: 0x002C7570
		public static FormulaExpression Slice(FormulaExpression subject, FormulaExpression startIndex, FormulaExpression endIndex)
		{
			return new PowerAutomateSlice(subject, startIndex, endIndex);
		}

		// Token: 0x0600D12E RID: 53550 RVA: 0x002C937A File Offset: 0x002C757A
		public static FormulaExpression Split(FormulaExpression subject, FormulaExpression delimiter)
		{
			return PowerAutomateExpressionHelper.Func("Split", new FormulaExpression[] { subject, delimiter });
		}

		// Token: 0x0600D12F RID: 53551 RVA: 0x002C9394 File Offset: 0x002C7594
		public static FormulaExpression StartsWith(FormulaExpression subject, FormulaExpression findTextExp)
		{
			return PowerAutomateExpressionHelper.Func("StartsWith", new FormulaExpression[] { subject, findTextExp });
		}

		// Token: 0x0600D130 RID: 53552 RVA: 0x002C93AE File Offset: 0x002C75AE
		public static FormulaExpression String(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("String", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D131 RID: 53553 RVA: 0x002C93C4 File Offset: 0x002C75C4
		public static FormulaExpression StringLiteral(string value)
		{
			return new PowerAutomateStringLiteral(value);
		}

		// Token: 0x0600D132 RID: 53554 RVA: 0x002C93CC File Offset: 0x002C75CC
		public static FormulaExpression Substitute(FormulaExpression source, FormulaExpression findText, FormulaExpression replaceText, FormulaExpression instance = null)
		{
			return PowerAutomateExpressionHelper.Func("Substitute", new FormulaExpression[] { source, findText, replaceText, instance });
		}

		// Token: 0x0600D133 RID: 53555 RVA: 0x002C93EE File Offset: 0x002C75EE
		public static FormulaExpression Substring(FormulaExpression subject, FormulaExpression startIndex, double length)
		{
			return PowerAutomateExpressionHelper.Substring(subject, startIndex, PowerAutomateExpressionHelper.NumberLiteral(length));
		}

		// Token: 0x0600D134 RID: 53556 RVA: 0x002C93FD File Offset: 0x002C75FD
		public static FormulaExpression Substring(FormulaExpression subject, FormulaExpression startIndex, FormulaExpression length)
		{
			return new PowerAutomateSubstring(subject, startIndex, length);
		}

		// Token: 0x0600D135 RID: 53557 RVA: 0x002C9407 File Offset: 0x002C7607
		public static FormulaExpression Take(FormulaExpression subject, FormulaExpression count)
		{
			return PowerAutomateExpressionHelper.Func("Take", new FormulaExpression[] { subject, count });
		}

		// Token: 0x0600D136 RID: 53558 RVA: 0x002C9421 File Offset: 0x002C7621
		public static FormulaExpression ToLower(FormulaExpression str)
		{
			return PowerAutomateExpressionHelper.Func("ToLower", new FormulaExpression[] { str });
		}

		// Token: 0x0600D137 RID: 53559 RVA: 0x002C9437 File Offset: 0x002C7637
		public static FormulaExpression ToUpper(FormulaExpression str)
		{
			return PowerAutomateExpressionHelper.Func("ToUpper", new FormulaExpression[] { str });
		}

		// Token: 0x0600D138 RID: 53560 RVA: 0x002C944D File Offset: 0x002C764D
		public static FormulaExpression Trim(FormulaExpression str)
		{
			return PowerAutomateExpressionHelper.Func("Trim", new FormulaExpression[] { str });
		}

		// Token: 0x0600D139 RID: 53561 RVA: 0x002C9463 File Offset: 0x002C7663
		public static FormulaExpression Value(FormulaExpression value)
		{
			return PowerAutomateExpressionHelper.Func("Value", new FormulaExpression[] { value });
		}

		// Token: 0x0600D13A RID: 53562 RVA: 0x002C9479 File Offset: 0x002C7679
		public static FormulaExpression Variable(string name)
		{
			return new PowerAutomateVariable(name);
		}

		// Token: 0x0600D13B RID: 53563 RVA: 0x002C9481 File Offset: 0x002C7681
		public static FormulaExpression Add(FormulaExpression left, double right)
		{
			return PowerAutomateExpressionHelper.Add(left, PowerAutomateExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D13C RID: 53564 RVA: 0x002C948F File Offset: 0x002C768F
		public static FormulaExpression Add(double left, FormulaExpression right)
		{
			return PowerAutomateExpressionHelper.Add(PowerAutomateExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D13D RID: 53565 RVA: 0x002C94A0 File Offset: 0x002C76A0
		public static FormulaExpression Add(FormulaExpression left, FormulaExpression right)
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
						return PowerAutomateExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value + formulaNumberLiteral.Value);
					}
					return right;
				}
				else
				{
					if (formulaNumberLiteral.Value < 0.0)
					{
						return PowerAutomateExpressionHelper.Sub(left, PowerAutomateExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
					}
					FormulaPlus formulaPlus = left as FormulaPlus;
					if (formulaPlus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral3 != null)
						{
							return PowerAutomateExpressionHelper.Add(formulaPlus.Left, PowerAutomateExpressionHelper.Add(formulaNumberLiteral3, formulaNumberLiteral));
						}
					}
					FormulaMinus formulaMinus = left as FormulaMinus;
					if (formulaMinus != null)
					{
						FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
						if (formulaNumberLiteral4 != null)
						{
							return PowerAutomateExpressionHelper.Sub(formulaMinus.Left, PowerAutomateExpressionHelper.Sub(formulaNumberLiteral4, formulaNumberLiteral));
						}
					}
				}
			}
			return new PowerAutomateAdd(left, right);
		}

		// Token: 0x0600D13E RID: 53566 RVA: 0x002C9587 File Offset: 0x002C7787
		public static FormulaExpression Add1(FormulaExpression val)
		{
			return PowerAutomateExpressionHelper.Add(1.0, val);
		}

		// Token: 0x0600D13F RID: 53567 RVA: 0x002C9598 File Offset: 0x002C7798
		public static FormulaExpression Average(IEnumerable<FormulaExpression> arguments)
		{
			IReadOnlyList<FormulaExpression> readOnlyList = arguments.ToReadOnlyList<FormulaExpression>();
			return PowerAutomateExpressionHelper.Div(PowerAutomateExpressionHelper.Sum(readOnlyList), PowerAutomateExpressionHelper.Float(PowerAutomateExpressionHelper.NumberLiteral(readOnlyList.Count)));
		}

		// Token: 0x0600D140 RID: 53568 RVA: 0x002C95C7 File Offset: 0x002C77C7
		public static FormulaExpression Div(FormulaExpression left, double right)
		{
			return PowerAutomateExpressionHelper.Div(left, PowerAutomateExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D141 RID: 53569 RVA: 0x002C95D5 File Offset: 0x002C77D5
		public static FormulaExpression Div(double left, FormulaExpression right)
		{
			return PowerAutomateExpressionHelper.Div(PowerAutomateExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D142 RID: 53570 RVA: 0x002C95E4 File Offset: 0x002C77E4
		public static FormulaExpression Div(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new PowerAutomateDivide(left, right);
			}
			return left;
		}

		// Token: 0x0600D143 RID: 53571 RVA: 0x002C9615 File Offset: 0x002C7815
		public static FormulaExpression Mul(FormulaExpression left, double right)
		{
			return PowerAutomateExpressionHelper.Mul(left, PowerAutomateExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D144 RID: 53572 RVA: 0x002C9623 File Offset: 0x002C7823
		public static FormulaExpression Mul(double left, FormulaExpression right)
		{
			return PowerAutomateExpressionHelper.Mul(PowerAutomateExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D145 RID: 53573 RVA: 0x002C9634 File Offset: 0x002C7834
		public static FormulaExpression Mul(FormulaExpression left, FormulaExpression right)
		{
			FormulaNumberLiteral formulaNumberLiteral = right as FormulaNumberLiteral;
			if (formulaNumberLiteral != null && formulaNumberLiteral.Value == 1.0)
			{
				return left;
			}
			formulaNumberLiteral = left as FormulaNumberLiteral;
			if (formulaNumberLiteral == null || formulaNumberLiteral.Value != 1.0)
			{
				return new PowerAutomateMultiply(left, right);
			}
			return right;
		}

		// Token: 0x0600D146 RID: 53574 RVA: 0x002C9684 File Offset: 0x002C7884
		public static FormulaExpression Product(IEnumerable<FormulaExpression> arguments)
		{
			IReadOnlyList<FormulaExpression> readOnlyList = arguments.ToReadOnlyList<FormulaExpression>();
			if (readOnlyList.Count < 2)
			{
				throw new Exception(string.Format("Too few arguments for Product() {0}", readOnlyList.Count));
			}
			IEnumerable<FormulaExpression> enumerable = readOnlyList.Skip(2);
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.Mul(readOnlyList[0], readOnlyList[1]);
			Func<FormulaExpression, FormulaExpression, FormulaExpression> func;
			if ((func = PowerAutomateExpressionHelper.<>O.<0>__Mul) == null)
			{
				func = (PowerAutomateExpressionHelper.<>O.<0>__Mul = new Func<FormulaExpression, FormulaExpression, FormulaExpression>(PowerAutomateExpressionHelper.Mul));
			}
			return enumerable.Aggregate(formulaExpression, func);
		}

		// Token: 0x0600D147 RID: 53575 RVA: 0x002C96F6 File Offset: 0x002C78F6
		public static FormulaExpression Sub(FormulaExpression left, double right)
		{
			return PowerAutomateExpressionHelper.Sub(left, PowerAutomateExpressionHelper.NumberLiteral(right));
		}

		// Token: 0x0600D148 RID: 53576 RVA: 0x002C9704 File Offset: 0x002C7904
		public static FormulaExpression Sub(double left, FormulaExpression right)
		{
			return PowerAutomateExpressionHelper.Sub(PowerAutomateExpressionHelper.NumberLiteral(left), right);
		}

		// Token: 0x0600D149 RID: 53577 RVA: 0x002C9714 File Offset: 0x002C7914
		public static FormulaExpression Sub(FormulaExpression left, FormulaExpression right)
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
					return PowerAutomateExpressionHelper.NumberLiteral(formulaNumberLiteral2.Value - formulaNumberLiteral.Value);
				}
				if (formulaNumberLiteral.Value < 0.0)
				{
					return PowerAutomateExpressionHelper.Add(left, PowerAutomateExpressionHelper.NumberLiteral(-formulaNumberLiteral.Value));
				}
				FormulaPlus formulaPlus = left as FormulaPlus;
				if (formulaPlus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral3 = formulaPlus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral3 != null)
					{
						return PowerAutomateExpressionHelper.Add(formulaPlus.Left, PowerAutomateExpressionHelper.Sub(formulaNumberLiteral3, formulaNumberLiteral));
					}
				}
				FormulaMinus formulaMinus = left as FormulaMinus;
				if (formulaMinus != null)
				{
					FormulaNumberLiteral formulaNumberLiteral4 = formulaMinus.Right as FormulaNumberLiteral;
					if (formulaNumberLiteral4 != null)
					{
						return PowerAutomateExpressionHelper.Sub(formulaMinus.Left, PowerAutomateExpressionHelper.Add(formulaNumberLiteral4, formulaNumberLiteral));
					}
				}
			}
			return new PowerAutomateSub(left, right);
		}

		// Token: 0x0600D14A RID: 53578 RVA: 0x002C97E8 File Offset: 0x002C79E8
		public static FormulaExpression Sub1(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Sub(subject, PowerAutomateExpressionHelper.NumberLiteral(1));
		}

		// Token: 0x0600D14B RID: 53579 RVA: 0x002C97F8 File Offset: 0x002C79F8
		public static FormulaExpression Sum(IEnumerable<FormulaExpression> arguments)
		{
			IReadOnlyList<FormulaExpression> readOnlyList = arguments.ToReadOnlyList<FormulaExpression>();
			if (readOnlyList.Count < 2)
			{
				throw new Exception(string.Format("Too few arguments for Product() {0}", readOnlyList.Count));
			}
			IEnumerable<FormulaExpression> enumerable = readOnlyList.Skip(2);
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.Add(readOnlyList[0], readOnlyList[1]);
			Func<FormulaExpression, FormulaExpression, FormulaExpression> func;
			if ((func = PowerAutomateExpressionHelper.<>O.<1>__Add) == null)
			{
				func = (PowerAutomateExpressionHelper.<>O.<1>__Add = new Func<FormulaExpression, FormulaExpression, FormulaExpression>(PowerAutomateExpressionHelper.Add));
			}
			return enumerable.Aggregate(formulaExpression, func);
		}

		// Token: 0x0600D14C RID: 53580 RVA: 0x002C986A File Offset: 0x002C7A6A
		public static FormulaExpression Greater(FormulaExpression left, FormulaExpression right)
		{
			return PowerAutomateExpressionHelper.Func("Greater", new FormulaExpression[] { left, right });
		}

		// Token: 0x0600D14D RID: 53581 RVA: 0x002C9884 File Offset: 0x002C7A84
		public static FormulaExpression GreaterOrEqual(FormulaExpression left, FormulaExpression right)
		{
			return PowerAutomateExpressionHelper.Func("GreaterOrEqual", new FormulaExpression[] { left, right });
		}

		// Token: 0x0600D14E RID: 53582 RVA: 0x002C989E File Offset: 0x002C7A9E
		public static FormulaExpression Less(FormulaExpression left, FormulaExpression right)
		{
			return PowerAutomateExpressionHelper.Func("Less", new FormulaExpression[] { left, right });
		}

		// Token: 0x0600D14F RID: 53583 RVA: 0x002C98B8 File Offset: 0x002C7AB8
		public static FormulaExpression LessOrEqual(FormulaExpression left, FormulaExpression right)
		{
			return PowerAutomateExpressionHelper.Func("LessOrEqual", new FormulaExpression[] { left, right });
		}

		// Token: 0x0600D150 RID: 53584 RVA: 0x002C98D2 File Offset: 0x002C7AD2
		public static FormulaExpression AddToTimeDays(FormulaExpression subject, double amount)
		{
			return PowerAutomateExpressionHelper.AddToTimeDays(subject, PowerAutomateExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D151 RID: 53585 RVA: 0x002C98E0 File Offset: 0x002C7AE0
		public static FormulaExpression AddToTimeDays(FormulaExpression subject, FormulaExpression amount)
		{
			return PowerAutomateExpressionHelper.Func("AddDays", new FormulaExpression[] { subject, amount });
		}

		// Token: 0x0600D152 RID: 53586 RVA: 0x002C98FA File Offset: 0x002C7AFA
		public static FormulaExpression AddToTimeHours(FormulaExpression subject, double amount)
		{
			return PowerAutomateExpressionHelper.AddToTimeHours(subject, PowerAutomateExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D153 RID: 53587 RVA: 0x002C9908 File Offset: 0x002C7B08
		public static FormulaExpression AddToTimeHours(FormulaExpression subject, FormulaExpression amount)
		{
			return PowerAutomateExpressionHelper.Func("AddHours", new FormulaExpression[] { subject, amount });
		}

		// Token: 0x0600D154 RID: 53588 RVA: 0x002C9922 File Offset: 0x002C7B22
		public static FormulaExpression AddToTimeMilliseconds(FormulaExpression subject, double amount)
		{
			return PowerAutomateExpressionHelper.AddToTimeMilliseconds(subject, PowerAutomateExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D155 RID: 53589 RVA: 0x002C9930 File Offset: 0x002C7B30
		public static FormulaExpression AddToTimeMilliseconds(FormulaExpression subject, FormulaExpression amount)
		{
			return PowerAutomateExpressionHelper.Func("AddToTimeDays", new FormulaExpression[]
			{
				subject,
				amount,
				PowerAutomateExpressionHelper.StringLiteral("Milliseconds")
			});
		}

		// Token: 0x0600D156 RID: 53590 RVA: 0x002C9957 File Offset: 0x002C7B57
		public static FormulaExpression AddToTimeMinutes(FormulaExpression subject, double amount)
		{
			return PowerAutomateExpressionHelper.AddToTimeMinutes(subject, PowerAutomateExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D157 RID: 53591 RVA: 0x002C9965 File Offset: 0x002C7B65
		public static FormulaExpression AddToTimeMinutes(FormulaExpression subject, FormulaExpression amount)
		{
			return PowerAutomateExpressionHelper.Func("AddMinutes", new FormulaExpression[] { subject, amount });
		}

		// Token: 0x0600D158 RID: 53592 RVA: 0x002C997F File Offset: 0x002C7B7F
		public static FormulaExpression AddToTimeMonths(FormulaExpression subject, double amount)
		{
			return PowerAutomateExpressionHelper.AddToTimeMonths(subject, PowerAutomateExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D159 RID: 53593 RVA: 0x002C998D File Offset: 0x002C7B8D
		public static FormulaExpression AddToTimeMonths(FormulaExpression subject, FormulaExpression amount)
		{
			return PowerAutomateExpressionHelper.Func("AddToTime", new FormulaExpression[]
			{
				subject,
				amount,
				PowerAutomateExpressionHelper.StringLiteral("Month")
			});
		}

		// Token: 0x0600D15A RID: 53594 RVA: 0x002C99B4 File Offset: 0x002C7BB4
		public static FormulaExpression AddToTimeSeconds(FormulaExpression subject, double amount)
		{
			return PowerAutomateExpressionHelper.AddToTimeSeconds(subject, PowerAutomateExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D15B RID: 53595 RVA: 0x002C99C2 File Offset: 0x002C7BC2
		public static FormulaExpression AddToTimeSeconds(FormulaExpression subject, FormulaExpression amount)
		{
			return PowerAutomateExpressionHelper.Func("AddSeconds", new FormulaExpression[] { subject, amount });
		}

		// Token: 0x0600D15C RID: 53596 RVA: 0x002C99DC File Offset: 0x002C7BDC
		public static FormulaExpression AddToTimeYears(FormulaExpression subject, double amount)
		{
			return PowerAutomateExpressionHelper.AddToTimeYears(subject, PowerAutomateExpressionHelper.NumberLiteral(amount));
		}

		// Token: 0x0600D15D RID: 53597 RVA: 0x002C99EA File Offset: 0x002C7BEA
		public static FormulaExpression AddToTimeYears(FormulaExpression subject, FormulaExpression amount)
		{
			return PowerAutomateExpressionHelper.Func("AddToTime", new FormulaExpression[]
			{
				subject,
				amount,
				PowerAutomateExpressionHelper.StringLiteral("Year")
			});
		}

		// Token: 0x0600D15E RID: 53598 RVA: 0x002C9A14 File Offset: 0x002C7C14
		public static FormulaExpression DateTime(FormulaExpression year, FormulaExpression month = null, FormulaExpression day = null, FormulaExpression hour = null, FormulaExpression minute = null, FormulaExpression second = null, FormulaExpression millisecond = null)
		{
			if (!(year is PowerAutomateNumberLiteral))
			{
				PowerAutomateFunc powerAutomateFunc = year as PowerAutomateFunc;
				if (powerAutomateFunc == null || !(powerAutomateFunc.Name != "FormatDateTime"))
				{
					goto IL_0036;
				}
			}
			year = PowerAutomateExpressionHelper.FormatNumber(year, PowerAutomateExpressionHelper.StringLiteral("0000"));
			IL_0036:
			if (!(month is PowerAutomateNumberLiteral))
			{
				PowerAutomateFunc powerAutomateFunc2 = month as PowerAutomateFunc;
				if (powerAutomateFunc2 == null || !(powerAutomateFunc2.Name != "FormatDateTime"))
				{
					goto IL_006C;
				}
			}
			month = PowerAutomateExpressionHelper.FormatNumber(month, PowerAutomateExpressionHelper.StringLiteral("00"));
			IL_006C:
			if (!(day is PowerAutomateNumberLiteral))
			{
				PowerAutomateFunc powerAutomateFunc3 = day as PowerAutomateFunc;
				if (powerAutomateFunc3 == null || !(powerAutomateFunc3.Name != "FormatDateTime"))
				{
					goto IL_00A2;
				}
			}
			day = PowerAutomateExpressionHelper.FormatNumber(day, PowerAutomateExpressionHelper.StringLiteral("00"));
			IL_00A2:
			if (!(hour is PowerAutomateNumberLiteral))
			{
				PowerAutomateFunc powerAutomateFunc4 = hour as PowerAutomateFunc;
				if (powerAutomateFunc4 == null || !(powerAutomateFunc4.Name != "FormatDateTime"))
				{
					goto IL_00D8;
				}
			}
			hour = PowerAutomateExpressionHelper.FormatNumber(hour, PowerAutomateExpressionHelper.StringLiteral("00"));
			IL_00D8:
			if (!(minute is PowerAutomateNumberLiteral))
			{
				PowerAutomateFunc powerAutomateFunc5 = minute as PowerAutomateFunc;
				if (powerAutomateFunc5 == null || !(powerAutomateFunc5.Name != "FormatDateTime"))
				{
					goto IL_0114;
				}
			}
			minute = PowerAutomateExpressionHelper.FormatNumber(minute, PowerAutomateExpressionHelper.StringLiteral("00"));
			IL_0114:
			if (!(second is PowerAutomateNumberLiteral))
			{
				PowerAutomateFunc powerAutomateFunc6 = second as PowerAutomateFunc;
				if (powerAutomateFunc6 == null || !(powerAutomateFunc6.Name != "FormatDateTime"))
				{
					goto IL_0150;
				}
			}
			second = PowerAutomateExpressionHelper.FormatNumber(second, PowerAutomateExpressionHelper.StringLiteral("00"));
			IL_0150:
			if (!(millisecond is PowerAutomateNumberLiteral))
			{
				PowerAutomateFunc powerAutomateFunc7 = millisecond as PowerAutomateFunc;
				if (powerAutomateFunc7 == null || !(powerAutomateFunc7.Name != "FormatDateTime"))
				{
					goto IL_018C;
				}
			}
			millisecond = PowerAutomateExpressionHelper.FormatNumber(millisecond, PowerAutomateExpressionHelper.StringLiteral("0000000"));
			IL_018C:
			if (month == null)
			{
				month = PowerAutomateExpressionHelper.StringLiteral("01");
			}
			if (day == null)
			{
				day = PowerAutomateExpressionHelper.StringLiteral("01");
			}
			if (hour == null && minute == null && second == null && millisecond == null)
			{
				return PowerAutomateExpressionHelper.Concat(new FormulaExpression[]
				{
					year,
					PowerAutomateExpressionHelper.StringLiteral("-"),
					month,
					PowerAutomateExpressionHelper.StringLiteral("-"),
					day
				});
			}
			if (second == null && millisecond == null)
			{
				return PowerAutomateExpressionHelper.Concat(new FormulaExpression[]
				{
					year,
					PowerAutomateExpressionHelper.StringLiteral("-"),
					month,
					PowerAutomateExpressionHelper.StringLiteral("-"),
					day,
					PowerAutomateExpressionHelper.StringLiteral("T"),
					hour,
					PowerAutomateExpressionHelper.StringLiteral(":"),
					minute
				});
			}
			if (millisecond == null)
			{
				return PowerAutomateExpressionHelper.Concat(new FormulaExpression[]
				{
					year,
					PowerAutomateExpressionHelper.StringLiteral("-"),
					month,
					PowerAutomateExpressionHelper.StringLiteral("-"),
					day,
					PowerAutomateExpressionHelper.StringLiteral("T"),
					hour,
					PowerAutomateExpressionHelper.StringLiteral(":"),
					minute,
					PowerAutomateExpressionHelper.StringLiteral(":"),
					second
				});
			}
			return PowerAutomateExpressionHelper.Concat(new FormulaExpression[]
			{
				year,
				PowerAutomateExpressionHelper.StringLiteral("-"),
				month,
				PowerAutomateExpressionHelper.StringLiteral("-"),
				day,
				PowerAutomateExpressionHelper.StringLiteral("T"),
				hour,
				PowerAutomateExpressionHelper.StringLiteral(":"),
				minute,
				PowerAutomateExpressionHelper.StringLiteral(":"),
				second,
				PowerAutomateExpressionHelper.StringLiteral("."),
				millisecond
			});
		}

		// Token: 0x0600D15F RID: 53599 RVA: 0x002C9D8B File Offset: 0x002C7F8B
		public static FormulaExpression Day(FormulaExpression subject, bool singleDigit = false)
		{
			return PowerAutomateExpressionHelper.FormatDateTime(subject, PowerAutomateExpressionHelper.StringLiteral(singleDigit ? "d" : "dd"), PowerAutomateExpressionHelper.StringLiteral("en-US"));
		}

		// Token: 0x0600D160 RID: 53600 RVA: 0x002C9DB1 File Offset: 0x002C7FB1
		public static FormulaExpression DayDiff(FormulaExpression start, FormulaExpression end)
		{
			return PowerAutomateExpressionHelper.Int(PowerAutomateExpressionHelper.Div(PowerAutomateExpressionHelper.Sub(PowerAutomateExpressionHelper.Ticks(end), PowerAutomateExpressionHelper.Ticks(start)), 864000000000.0));
		}

		// Token: 0x0600D161 RID: 53601 RVA: 0x002C9DD7 File Offset: 0x002C7FD7
		public static FormulaExpression DayEnd(FormulaExpression dayStart)
		{
			return PowerAutomateExpressionHelper.AddToTimeDays(dayStart, 1.0);
		}

		// Token: 0x0600D162 RID: 53602 RVA: 0x002C9DE8 File Offset: 0x002C7FE8
		public static FormulaExpression DayOfMonth(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("DayOfMonth", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D163 RID: 53603 RVA: 0x002C9DFE File Offset: 0x002C7FFE
		public static FormulaExpression DayOfWeek(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("DayOfWeek", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D164 RID: 53604 RVA: 0x002C9E14 File Offset: 0x002C8014
		public static FormulaExpression DayOfYear(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("DayOfYear", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D165 RID: 53605 RVA: 0x002C9E2A File Offset: 0x002C802A
		public static FormulaExpression DayStart(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("StartOfDay", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D166 RID: 53606 RVA: 0x002C9E40 File Offset: 0x002C8040
		public static FormulaExpression Hour(FormulaExpression subject, bool singleDigit = false)
		{
			return PowerAutomateExpressionHelper.FormatDateTime(subject, PowerAutomateExpressionHelper.StringLiteral(singleDigit ? "%H" : "HH"), PowerAutomateExpressionHelper.StringLiteral("en-US"));
		}

		// Token: 0x0600D167 RID: 53607 RVA: 0x002C9E66 File Offset: 0x002C8066
		public static FormulaExpression HourEnd(FormulaExpression hourStart)
		{
			return PowerAutomateExpressionHelper.AddToTimeHours(hourStart, 1.0);
		}

		// Token: 0x0600D168 RID: 53608 RVA: 0x002C9E77 File Offset: 0x002C8077
		public static FormulaExpression HourStart(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("StartOfHour", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D169 RID: 53609 RVA: 0x002C9E8D File Offset: 0x002C808D
		public static FormulaExpression Midpoint(FormulaExpression start, FormulaExpression end)
		{
			return PowerAutomateExpressionHelper.Add(PowerAutomateExpressionHelper.Ticks(start), PowerAutomateExpressionHelper.Div(PowerAutomateExpressionHelper.Sub(PowerAutomateExpressionHelper.Ticks(end), PowerAutomateExpressionHelper.Ticks(start)), 2.0));
		}

		// Token: 0x0600D16A RID: 53610 RVA: 0x002C9EB9 File Offset: 0x002C80B9
		public static FormulaExpression Millisecond(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.FormatDateTime(subject, PowerAutomateExpressionHelper.StringLiteral("fffffff"), PowerAutomateExpressionHelper.StringLiteral("en-US"));
		}

		// Token: 0x0600D16B RID: 53611 RVA: 0x002C9ED5 File Offset: 0x002C80D5
		public static FormulaExpression Minute(FormulaExpression subject, bool singleDigit = false)
		{
			return PowerAutomateExpressionHelper.FormatDateTime(subject, PowerAutomateExpressionHelper.StringLiteral(singleDigit ? "%m" : "mm"), PowerAutomateExpressionHelper.StringLiteral("en-US"));
		}

		// Token: 0x0600D16C RID: 53612 RVA: 0x002C9EFB File Offset: 0x002C80FB
		public static FormulaExpression MinuteEnd(FormulaExpression minuteStart)
		{
			return PowerAutomateExpressionHelper.AddToTimeMinutes(minuteStart, 1.0);
		}

		// Token: 0x0600D16D RID: 53613 RVA: 0x002C9F0C File Offset: 0x002C810C
		public static FormulaExpression MinuteStart(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.DateTime(PowerAutomateExpressionHelper.Year(subject), PowerAutomateExpressionHelper.Month(subject, false), PowerAutomateExpressionHelper.Day(subject, false), PowerAutomateExpressionHelper.Hour(subject, false), PowerAutomateExpressionHelper.Minute(subject, false), null, null);
		}

		// Token: 0x0600D16E RID: 53614 RVA: 0x002C9F37 File Offset: 0x002C8137
		public static FormulaExpression Month(FormulaExpression subject, bool singleDigit = false)
		{
			return PowerAutomateExpressionHelper.FormatDateTime(subject, PowerAutomateExpressionHelper.StringLiteral(singleDigit ? "%M" : "MM"), PowerAutomateExpressionHelper.StringLiteral("en-US"));
		}

		// Token: 0x0600D16F RID: 53615 RVA: 0x002C9F5D File Offset: 0x002C815D
		public static FormulaExpression MonthDays(FormulaExpression subject)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.MonthStart(subject);
			return PowerAutomateExpressionHelper.DayDiff(formulaExpression, PowerAutomateExpressionHelper.MonthEnd(formulaExpression));
		}

		// Token: 0x0600D170 RID: 53616 RVA: 0x002C9F70 File Offset: 0x002C8170
		public static FormulaExpression MonthEnd(FormulaExpression monthStart)
		{
			return PowerAutomateExpressionHelper.AddToTimeMonths(monthStart, 1.0);
		}

		// Token: 0x0600D171 RID: 53617 RVA: 0x002C9F81 File Offset: 0x002C8181
		public static FormulaExpression MonthStart(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.DateTime(PowerAutomateExpressionHelper.Year(subject), PowerAutomateExpressionHelper.Month(subject, false), null, null, null, null, null);
		}

		// Token: 0x0600D172 RID: 53618 RVA: 0x002C9F9C File Offset: 0x002C819C
		public static FormulaExpression MonthWeek(FormulaExpression subject)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.MonthStart(subject);
			return PowerAutomateExpressionHelper.Add1(PowerAutomateExpressionHelper.Int(PowerAutomateExpressionHelper.Div(PowerAutomateExpressionHelper.Add(PowerAutomateExpressionHelper.DayDiff(formulaExpression, subject), PowerAutomateExpressionHelper.DayOfWeek(formulaExpression)), 7.0)));
		}

		// Token: 0x0600D173 RID: 53619 RVA: 0x002C9FDA File Offset: 0x002C81DA
		public static FormulaExpression Quarter(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Add1(PowerAutomateExpressionHelper.Div(PowerAutomateExpressionHelper.Sub1(PowerAutomateExpressionHelper.Int(PowerAutomateExpressionHelper.Month(subject, false))), 3.0));
		}

		// Token: 0x0600D174 RID: 53620 RVA: 0x002CA000 File Offset: 0x002C8200
		public static FormulaExpression QuarterDay(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Add1(PowerAutomateExpressionHelper.DayDiff(PowerAutomateExpressionHelper.QuarterStart(subject), subject));
		}

		// Token: 0x0600D175 RID: 53621 RVA: 0x002CA014 File Offset: 0x002C8214
		public static FormulaExpression QuarterDays(FormulaExpression subject)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.QuarterStart(subject);
			FormulaExpression formulaExpression2 = PowerAutomateExpressionHelper.QuarterEnd(subject);
			return PowerAutomateExpressionHelper.DayDiff(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D176 RID: 53622 RVA: 0x002CA034 File Offset: 0x002C8234
		public static FormulaExpression QuarterEnd(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.AddToTimeMonths(PowerAutomateExpressionHelper.QuarterStart(subject), 3.0);
		}

		// Token: 0x0600D177 RID: 53623 RVA: 0x002CA04C File Offset: 0x002C824C
		public static FormulaExpression QuarterStart(FormulaExpression subject)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.Quarter(subject);
			return PowerAutomateExpressionHelper.DateTime(PowerAutomateExpressionHelper.Year(subject), PowerAutomateExpressionHelper.Sub(PowerAutomateExpressionHelper.Mul(3.0, formulaExpression), 2.0), null, null, null, null, null);
		}

		// Token: 0x0600D178 RID: 53624 RVA: 0x002CA090 File Offset: 0x002C8290
		public static FormulaExpression QuarterWeek(FormulaExpression subject)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.QuarterStart(subject);
			return PowerAutomateExpressionHelper.Add1(PowerAutomateExpressionHelper.Int(PowerAutomateExpressionHelper.Div(PowerAutomateExpressionHelper.Add(PowerAutomateExpressionHelper.DayDiff(formulaExpression, subject), PowerAutomateExpressionHelper.DayOfWeek(formulaExpression)), 7.0)));
		}

		// Token: 0x0600D179 RID: 53625 RVA: 0x002CA0CE File Offset: 0x002C82CE
		public static FormulaExpression Second(FormulaExpression subject, bool singleDigit = false)
		{
			return PowerAutomateExpressionHelper.FormatDateTime(subject, PowerAutomateExpressionHelper.StringLiteral(singleDigit ? "%s" : "ss"), PowerAutomateExpressionHelper.StringLiteral("en-US"));
		}

		// Token: 0x0600D17A RID: 53626 RVA: 0x002CA0F4 File Offset: 0x002C82F4
		public static FormulaExpression SecondEnd(FormulaExpression secondStart)
		{
			return PowerAutomateExpressionHelper.AddToTimeSeconds(secondStart, 1.0);
		}

		// Token: 0x0600D17B RID: 53627 RVA: 0x002CA105 File Offset: 0x002C8305
		public static FormulaExpression SecondStart(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.DateTime(PowerAutomateExpressionHelper.Year(subject), PowerAutomateExpressionHelper.Month(subject, false), PowerAutomateExpressionHelper.Day(subject, false), PowerAutomateExpressionHelper.Hour(subject, false), PowerAutomateExpressionHelper.Minute(subject, false), PowerAutomateExpressionHelper.Second(subject, false), PowerAutomateExpressionHelper.Millisecond(subject));
		}

		// Token: 0x0600D17C RID: 53628 RVA: 0x002CA13B File Offset: 0x002C833B
		public static FormulaExpression Ticks(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.Func("Ticks", new FormulaExpression[] { subject });
		}

		// Token: 0x0600D17D RID: 53629 RVA: 0x002CA151 File Offset: 0x002C8351
		public static FormulaExpression WeekEnd(FormulaExpression weekStart)
		{
			return PowerAutomateExpressionHelper.AddToTimeDays(weekStart, 7.0);
		}

		// Token: 0x0600D17E RID: 53630 RVA: 0x002CA162 File Offset: 0x002C8362
		public static FormulaExpression WeekStart(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.AddToTimeDays(PowerAutomateExpressionHelper.DateTime(PowerAutomateExpressionHelper.Year(subject), PowerAutomateExpressionHelper.Month(subject, false), PowerAutomateExpressionHelper.Day(subject, false), null, null, null, null), PowerAutomateExpressionHelper.Sub(1.0, PowerAutomateExpressionHelper.Add1(PowerAutomateExpressionHelper.DayOfWeek(subject))));
		}

		// Token: 0x0600D17F RID: 53631 RVA: 0x002CA19F File Offset: 0x002C839F
		public static FormulaExpression Year(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.FormatDateTime(subject, PowerAutomateExpressionHelper.StringLiteral("yyyy"), PowerAutomateExpressionHelper.StringLiteral("en-US"));
		}

		// Token: 0x0600D180 RID: 53632 RVA: 0x002CA1BC File Offset: 0x002C83BC
		public static FormulaExpression YearDays(FormulaExpression subject)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.YearStart(subject);
			FormulaExpression formulaExpression2 = PowerAutomateExpressionHelper.YearEnd(formulaExpression);
			return PowerAutomateExpressionHelper.DayDiff(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D181 RID: 53633 RVA: 0x002CA1DC File Offset: 0x002C83DC
		public static FormulaExpression YearEnd(FormulaExpression yearStart)
		{
			return PowerAutomateExpressionHelper.AddToTimeYears(yearStart, 1.0);
		}

		// Token: 0x0600D182 RID: 53634 RVA: 0x002CA1ED File Offset: 0x002C83ED
		public static FormulaExpression YearStart(FormulaExpression subject)
		{
			return PowerAutomateExpressionHelper.DateTime(PowerAutomateExpressionHelper.Year(subject), null, null, null, null, null, null);
		}

		// Token: 0x0600D183 RID: 53635 RVA: 0x002CA200 File Offset: 0x002C8400
		public static FormulaExpression YearWeek(FormulaExpression subject)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.YearStart(subject);
			return PowerAutomateExpressionHelper.Add1(PowerAutomateExpressionHelper.Int(PowerAutomateExpressionHelper.Div(PowerAutomateExpressionHelper.Add(PowerAutomateExpressionHelper.DayDiff(formulaExpression, subject), PowerAutomateExpressionHelper.DayOfWeek(formulaExpression)), 7.0)));
		}

		// Token: 0x0200190F RID: 6415
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400511B RID: 20763
			public static Func<FormulaExpression, FormulaExpression, FormulaExpression> <0>__Mul;

			// Token: 0x0400511C RID: 20764
			public static Func<FormulaExpression, FormulaExpression, FormulaExpression> <1>__Add;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Formula.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Exceptions;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018C9 RID: 6345
	internal class PowerQueryProgramTranslator : ProgramTranslatorBase
	{
		// Token: 0x0600CF25 RID: 53029 RVA: 0x002C1B0D File Offset: 0x002BFD0D
		private PowerQueryProgramTranslator(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, IPowerQueryTranslationOptions options, ILogger logger)
			: base(program, examples, inputs, logger)
		{
			this._options = options ?? new PowerQueryTranslationConstraint();
		}

		// Token: 0x0600CF26 RID: 53030 RVA: 0x002C1B2B File Offset: 0x002BFD2B
		public static FormulaExpression Translate(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, IPowerQueryTranslationOptions options, ILogger logger = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new PowerQueryProgramTranslator(program, examples, inputs, options, logger).Translate(cancellationToken);
		}

		// Token: 0x0600CF27 RID: 53031 RVA: 0x002C1B40 File Offset: 0x002BFD40
		protected override FormulaExpression Translate(CancellationToken cancellationToken = default(CancellationToken))
		{
			FormulaExpression formulaExpression = base.Translate(cancellationToken);
			if (!(formulaExpression == null))
			{
				return PowerQueryExpressionOptimizer.Optimize(formulaExpression, this._options);
			}
			return null;
		}

		// Token: 0x0600CF28 RID: 53032 RVA: 0x002C1B6C File Offset: 0x002BFD6C
		protected override FormulaExpression Translate(ProgramNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			FormulaExpression formulaExpression;
			try
			{
				cancellationToken.ThrowIfCancellationRequested();
				Concat concat;
				LetX letX;
				Split split;
				Slice slice;
				SlicePrefixAbs slicePrefixAbs;
				SlicePrefix slicePrefix;
				SliceSuffix sliceSuffix;
				Abs abs;
				Str str;
				Number number;
				Date date;
				Find find;
				FormatNumber formatNumber;
				RoundNumber roundNumber;
				ParseNumber parseNumber;
				FormatDateTime formatDateTime;
				ParseDateTime parseDateTime;
				DateTimePart dateTimePart;
				RoundDateTime roundDateTime;
				Length length;
				Add add;
				Subtract subtract;
				Multiply multiply;
				Divide divide;
				Sum sum;
				Product product;
				Average average;
				FromStr fromStr;
				FromDateTime fromDateTime;
				FromNumber fromNumber;
				ToStr toStr;
				ToInt toInt;
				ToDouble toDouble;
				ToDecimal toDecimal;
				ToDateTime toDateTime;
				if (node.IsLowerCase())
				{
					formulaExpression = this.TranslateLowerCase(node);
				}
				else if (node.IsUpperCase())
				{
					formulaExpression = this.TranslateUpperCase(node);
				}
				else if (node.IsProperCase())
				{
					formulaExpression = this.TranslateProperCase(node);
				}
				else if (node.Is(out concat))
				{
					formulaExpression = this.TranslateConcat(concat);
				}
				else if (node.Is(out letX))
				{
					formulaExpression = this.TranslateLetX(letX);
				}
				else if (node.Is(out split))
				{
					formulaExpression = this.TranslateSplit(split);
				}
				else if (node.Is(out slice))
				{
					formulaExpression = this.TranslateSlice(slice);
				}
				else if (node.Is(out slicePrefixAbs))
				{
					formulaExpression = this.TranslateSlicePrefixAbs(slicePrefixAbs);
				}
				else if (node.Is(out slicePrefix))
				{
					formulaExpression = this.TranslateSlicePrefix(slicePrefix);
				}
				else if (node.Is(out sliceSuffix))
				{
					formulaExpression = this.TranslateSliceSuffix(sliceSuffix);
				}
				else if (node.Is(out abs))
				{
					formulaExpression = PowerQueryProgramTranslator.TranslateAbs(abs);
				}
				else if (node.Is(out str))
				{
					formulaExpression = this.TranslateStr(str);
				}
				else if (node.Is(out number))
				{
					formulaExpression = this.TranslateNumber(number);
				}
				else if (node.IsArithmeticRightNumber())
				{
					formulaExpression = PowerQueryProgramTranslator.TranslateArithmeticRightNumber(node);
				}
				else if (node.Is(out date))
				{
					formulaExpression = this.TranslateDate(date);
				}
				else if (node.Is(out find))
				{
					formulaExpression = this.TranslateFind(find);
				}
				else if (node.IsTrim())
				{
					formulaExpression = this.TranslateTrim(node);
				}
				else if (node.Is(out formatNumber))
				{
					formulaExpression = this.TranslateFormatNumber(formatNumber);
				}
				else if (node.Is(out roundNumber))
				{
					formulaExpression = this.TranslateRoundNumber(roundNumber);
				}
				else if (node.Is(out parseNumber))
				{
					formulaExpression = this.TranslateParseNumber(parseNumber);
				}
				else if (node.Is(out formatDateTime))
				{
					formulaExpression = this.TranslateFormatDateTime(formatDateTime);
				}
				else if (node.Is(out parseDateTime))
				{
					formulaExpression = this.TranslateParseDateTime(parseDateTime);
				}
				else if (node.Is(out dateTimePart))
				{
					formulaExpression = this.TranslateDateTimePart(dateTimePart);
				}
				else if (node.Is(out roundDateTime))
				{
					formulaExpression = this.TranslateRoundDateTime(roundDateTime);
				}
				else if (node.Is(out length))
				{
					formulaExpression = this.TranslateLength(length);
				}
				else if (node.Is(out add))
				{
					formulaExpression = this.TranslateAdd(add);
				}
				else if (node.Is(out subtract))
				{
					formulaExpression = this.TranslateSubtract(subtract);
				}
				else if (node.Is(out multiply))
				{
					formulaExpression = this.TranslateMultiply(multiply);
				}
				else if (node.Is(out divide))
				{
					formulaExpression = this.TranslateDivide(divide);
				}
				else if (node.Is(out sum))
				{
					formulaExpression = PowerQueryProgramTranslator.TranslateSum(sum);
				}
				else if (node.Is(out product))
				{
					formulaExpression = PowerQueryProgramTranslator.TranslateProduct(product);
				}
				else if (node.Is(out average))
				{
					formulaExpression = PowerQueryProgramTranslator.TranslateAverage(average);
				}
				else if (node.Is(out fromStr))
				{
					formulaExpression = PowerQueryProgramTranslator.TranslateFromStr(fromStr);
				}
				else if (node.Is(out fromDateTime))
				{
					formulaExpression = PowerQueryProgramTranslator.TranslateFromDateTime(fromDateTime);
				}
				else if (node.Is(out fromNumber))
				{
					formulaExpression = this.TranslateFromNumber(fromNumber);
				}
				else if (node.Is(out toStr))
				{
					formulaExpression = this.TranslateToStr(toStr);
				}
				else if (node.Is(out toInt))
				{
					formulaExpression = this.TranslateToInt(toInt);
				}
				else if (node.Is(out toDouble))
				{
					formulaExpression = this.TranslateToDouble(toDouble);
				}
				else if (node.Is(out toDecimal))
				{
					formulaExpression = this.TranslateToDecimal(toDecimal);
				}
				else if (node.Is(out toDateTime))
				{
					formulaExpression = this.TranslateToDateTime(toDateTime);
				}
				else if (!(node is VariableNode))
				{
					if (node != null)
					{
						if (node.GrammarRule is ConversionRule)
						{
							formulaExpression = this.TranslateConversionRule(node);
							goto IL_05B6;
						}
						LiteralNode literalNode = node as LiteralNode;
						if (literalNode != null)
						{
							formulaExpression = PowerQueryProgramTranslator.TranslateLiteral(literalNode);
							goto IL_05B6;
						}
					}
					If @if;
					IsString isString;
					IsBlank isBlank;
					IsNotBlank isNotBlank;
					StringEquals stringEquals;
					StartsWithDigit startsWithDigit;
					EndsWithDigit endsWithDigit;
					Contains contains;
					StartsWith startsWith;
					IsNumber isNumber;
					NumberGreaterThan numberGreaterThan;
					NumberLessThan numberLessThan;
					NumberEquals numberEquals;
					Or or;
					if (node.Is(out @if))
					{
						formulaExpression = this.TranslateIf(@if);
					}
					else if (node.Is(out isString))
					{
						formulaExpression = this.TranslateIsString(isString);
					}
					else if (node.Is(out isBlank))
					{
						formulaExpression = this.TranslateIsBlank(isBlank);
					}
					else if (node.Is(out isNotBlank))
					{
						formulaExpression = this.TranslateIsNotBlank(isNotBlank);
					}
					else if (node.Is(out stringEquals))
					{
						formulaExpression = this.TranslateStringEquals(stringEquals);
					}
					else if (node.Is(out startsWithDigit))
					{
						formulaExpression = this.TranslateStartsWithDigit(startsWithDigit);
					}
					else if (node.Is(out endsWithDigit))
					{
						formulaExpression = this.TranslateEndsWithDigit(endsWithDigit);
					}
					else if (node.Is(out contains))
					{
						formulaExpression = this.TranslateContains(contains);
					}
					else if (node.Is(out startsWith))
					{
						formulaExpression = this.TranslateStartsWith(startsWith);
					}
					else if (node.Is(out isNumber))
					{
						formulaExpression = this.TranslateIsNumber(isNumber);
					}
					else if (node.Is(out numberGreaterThan))
					{
						formulaExpression = this.TranslateNumberGreaterThan(numberGreaterThan);
					}
					else if (node.Is(out numberLessThan))
					{
						formulaExpression = this.TranslateNumberLessThan(numberLessThan);
					}
					else if (node.Is(out numberEquals))
					{
						formulaExpression = this.TranslateNumberEquals(numberEquals);
					}
					else if (node.Is(out or))
					{
						formulaExpression = this.TranslateOr(or);
					}
					else
					{
						if (!node.Is<Null>())
						{
							throw new FormulaTranslationNotFoundException(string.Format("Invalid Rule: {0}", node.GrammarRule));
						}
						formulaExpression = PowerQueryProgramTranslator.TranslateNull();
					}
				}
				else
				{
					formulaExpression = this.TranslateVariableNode();
				}
				IL_05B6:
				FormulaExpression formulaExpression2 = formulaExpression;
				cancellationToken.ThrowIfCancellationRequested();
				if (this._cancelled)
				{
					formulaExpression = null;
				}
				else
				{
					if (formulaExpression2 == null)
					{
						throw new FormulaTranslationNotFoundException(string.Format("No Translation for: {0}", node.GrammarRule));
					}
					formulaExpression = formulaExpression2;
				}
			}
			catch (FormulaTranslationNotFoundException ex)
			{
				this._cancelled = true;
				base.TrackAnomaly(ex.Message);
				formulaExpression = null;
			}
			return formulaExpression;
		}

		// Token: 0x0600CF29 RID: 53033 RVA: 0x002C21A4 File Offset: 0x002C03A4
		private static FormulaExpression TranslateAbs(Abs abs)
		{
			return PowerQueryExpressionHelper.NumberLiteral(abs.absPos.Value);
		}

		// Token: 0x0600CF2A RID: 53034 RVA: 0x002C21C8 File Offset: 0x002C03C8
		private FormulaExpression TranslateConcat(Concat concat)
		{
			FormulaExpression formulaExpression = this.Translate(concat.concatPrefix.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(concat.concatSuffix.Node, default(CancellationToken));
			return PowerQueryExpressionHelper.Concat(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CF2B RID: 53035 RVA: 0x002C2218 File Offset: 0x002C0418
		private FormulaExpression TranslateConversionRule(ProgramNode node)
		{
			return this.Translate(node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CF2C RID: 53036 RVA: 0x002C223C File Offset: 0x002C043C
		private FormulaExpression TranslateDate(Date date)
		{
			return this.Translate(date.constDt.Node, default(CancellationToken));
		}

		// Token: 0x0600CF2D RID: 53037 RVA: 0x002C2268 File Offset: 0x002C0468
		private FormulaExpression TranslateFind(Find find)
		{
			FormulaExpression formulaExpression = this.Translate(find.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(find.findDelimiter.Node, default(CancellationToken));
			int value = find.findInstance.Value;
			FormulaExpression formulaExpression3;
			if (value > 0)
			{
				if (value != 1)
				{
					formulaExpression3 = new PowerQueryItemAccess(PowerQueryExpressionHelper.Func("Text.PositionOf", new FormulaExpression[]
					{
						formulaExpression,
						formulaExpression2,
						PowerQueryConstant.OccurrenceAll
					}), PowerQueryExpressionHelper.NumberLiteral(value - 1));
				}
				else
				{
					formulaExpression3 = PowerQueryExpressionHelper.Func("Text.PositionOf", new FormulaExpression[] { formulaExpression, formulaExpression2 });
				}
			}
			else if (value != -1)
			{
				formulaExpression3 = new PowerQueryItemAccess(PowerQueryExpressionHelper.Func("List.Reverse", new FormulaExpression[] { PowerQueryExpressionHelper.Func("Text.PositionOf", new FormulaExpression[]
				{
					formulaExpression,
					formulaExpression2,
					PowerQueryConstant.OccurrenceAll
				}) }), PowerQueryExpressionHelper.NumberLiteral(-value - 1));
			}
			else
			{
				formulaExpression3 = PowerQueryExpressionHelper.Func("Text.PositionOf", new FormulaExpression[]
				{
					formulaExpression,
					formulaExpression2,
					PowerQueryConstant.OccurrenceLast
				});
			}
			FormulaExpression formulaExpression4 = formulaExpression3;
			formulaExpression4 = PowerQueryExpressionHelper.Plus1(formulaExpression4);
			int value2 = find.findOffset.Value;
			if (value2 <= 0)
			{
				if (value2 >= 0)
				{
					formulaExpression3 = formulaExpression4;
				}
				else
				{
					formulaExpression3 = PowerQueryExpressionHelper.Minus(formulaExpression4, PowerQueryExpressionHelper.NumberLiteral(-value2));
				}
			}
			else
			{
				formulaExpression3 = PowerQueryExpressionHelper.Plus(formulaExpression4, PowerQueryExpressionHelper.NumberLiteral(value2));
			}
			return formulaExpression3;
		}

		// Token: 0x0600CF2E RID: 53038 RVA: 0x002C23E0 File Offset: 0x002C05E0
		private FormulaExpression TranslateFormatNumber(FormatNumber formatNumber)
		{
			FormulaExpression formulaExpression = this.Translate(formatNumber.Node.Children[0], default(CancellationToken));
			FormatNumberDescriptor value = formatNumber.numberFormatDesc.Value;
			string text = value.ToFormatString();
			return PowerQueryExpressionHelper.Func("Number.ToText", new FormulaExpression[]
			{
				formulaExpression,
				PowerQueryExpressionHelper.StringLiteral(text),
				PowerQueryExpressionHelper.Locale(value.Locale)
			});
		}

		// Token: 0x0600CF2F RID: 53039 RVA: 0x002C2450 File Offset: 0x002C0650
		private FormulaExpression TranslateLength(Length length)
		{
			return PowerQueryExpressionHelper.Len(this.Translate(length.fromStr.Node, default(CancellationToken)));
		}

		// Token: 0x0600CF30 RID: 53040 RVA: 0x002C2480 File Offset: 0x002C0680
		private FormulaExpression TranslateLetX(LetX letX)
		{
			this._currentInputExpression = this.Translate(letX.fromStrTrim.Node, default(CancellationToken));
			return this.Translate(letX.substring.Node, default(CancellationToken));
		}

		// Token: 0x0600CF31 RID: 53041 RVA: 0x002C24D0 File Offset: 0x002C06D0
		private static FormulaExpression TranslateLiteral(LiteralNode node)
		{
			object value = node.Value;
			string text = value as string;
			FormulaExpression formulaExpression;
			if (text == null)
			{
				if (value is int)
				{
					int num = (int)value;
					formulaExpression = PowerQueryExpressionHelper.NumberLiteral(num);
				}
				else if (value is double)
				{
					double num2 = (double)value;
					formulaExpression = PowerQueryExpressionHelper.NumberLiteral(num2);
				}
				else if (value is decimal)
				{
					decimal num3 = (decimal)value;
					formulaExpression = PowerQueryExpressionHelper.NumberLiteral(num3);
				}
				else if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					formulaExpression = PowerQueryExpressionHelper.DateTimeLiteral(dateTime);
				}
				else
				{
					formulaExpression = null;
				}
			}
			else
			{
				formulaExpression = PowerQueryExpressionHelper.StringLiteral(text);
			}
			return formulaExpression;
		}

		// Token: 0x0600CF32 RID: 53042 RVA: 0x002C2578 File Offset: 0x002C0778
		private FormulaExpression TranslateLowerCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerQueryExpressionHelper.Lower(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CF33 RID: 53043 RVA: 0x002C25B0 File Offset: 0x002C07B0
		private static FormulaExpression TranslateArithmeticRightNumber(ProgramNode node)
		{
			decimal num;
			if (!node.IsArithmeticRightNumber(out num))
			{
				return null;
			}
			return PowerQueryExpressionHelper.NumberLiteral(num);
		}

		// Token: 0x0600CF34 RID: 53044 RVA: 0x002C25D0 File Offset: 0x002C07D0
		private FormulaExpression TranslateNumber(Number number)
		{
			return this.Translate(number.constNum.Node, default(CancellationToken));
		}

		// Token: 0x0600CF35 RID: 53045 RVA: 0x002C25FC File Offset: 0x002C07FC
		private FormulaExpression TranslateParseNumber(ParseNumber parseNumber)
		{
			FormulaExpression formulaExpression = this.Translate(parseNumber.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(parseNumber.Node.Children[1], default(CancellationToken));
			FormulaStringLiteral formulaStringLiteral = formulaExpression2 as FormulaStringLiteral;
			if (formulaStringLiteral != null)
			{
				formulaExpression2 = PowerQueryExpressionHelper.Locale(formulaStringLiteral.Value);
			}
			return PowerQueryExpressionHelper.Func("Number.FromText", new FormulaExpression[] { formulaExpression, formulaExpression2 });
		}

		// Token: 0x0600CF36 RID: 53046 RVA: 0x002C2674 File Offset: 0x002C0874
		private FormulaExpression TranslateProperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerQueryExpressionHelper.Proper(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CF37 RID: 53047 RVA: 0x002C26AC File Offset: 0x002C08AC
		private FormulaExpression TranslateRoundNumber(RoundNumber roundNumber)
		{
			FormulaExpression formulaExpression = this.Translate(roundNumber.inumber.Node, default(CancellationToken));
			if (formulaExpression == null)
			{
				return null;
			}
			RoundNumberDescriptor value = roundNumber.numberRoundDesc.Value;
			RoundingMode mode = value.Mode;
			double num = value.Delta;
			double num2 = Math.Abs(Math.Log10(num));
			bool flag;
			if (num <= 1.0)
			{
				double num3 = num2 % 1.0;
				flag = num3 >= -0.0001 && num3 <= 0.0001;
			}
			else
			{
				flag = false;
			}
			FormulaExpression formulaExpression2;
			if (flag)
			{
				int num4 = Convert.ToInt32(Math.Round(num2, 0));
				switch (mode)
				{
				case RoundingMode.Nearest:
					formulaExpression2 = PowerQueryExpressionHelper.Round(formulaExpression, PowerQueryExpressionHelper.NumberLiteral(num4));
					break;
				case RoundingMode.Down:
					formulaExpression2 = PowerQueryExpressionHelper.RoundDown(formulaExpression, PowerQueryExpressionHelper.NumberLiteral(num4));
					break;
				case RoundingMode.Up:
					formulaExpression2 = PowerQueryExpressionHelper.RoundUp(formulaExpression, PowerQueryExpressionHelper.NumberLiteral(num4));
					break;
				default:
					formulaExpression2 = null;
					break;
				}
				FormulaExpression formulaExpression3 = formulaExpression2;
				if (formulaExpression3 == null)
				{
					base.TrackAnomaly(string.Format("Unsupported rounding mode {0}", mode));
				}
				return formulaExpression3;
			}
			num = 1.0 / num;
			switch (mode)
			{
			case RoundingMode.Nearest:
				formulaExpression2 = PowerQueryExpressionHelper.Divide(PowerQueryExpressionHelper.Round(PowerQueryExpressionHelper.Multiply(formulaExpression, PowerQueryExpressionHelper.NumberLiteral(num)), PowerQueryExpressionHelper.NumberLiteral(0)), PowerQueryExpressionHelper.NumberLiteral(num));
				break;
			case RoundingMode.Down:
				formulaExpression2 = PowerQueryExpressionHelper.Divide(PowerQueryExpressionHelper.RoundDown(PowerQueryExpressionHelper.Multiply(formulaExpression, PowerQueryExpressionHelper.NumberLiteral(num)), PowerQueryExpressionHelper.NumberLiteral(0)), PowerQueryExpressionHelper.NumberLiteral(num));
				break;
			case RoundingMode.Up:
				formulaExpression2 = PowerQueryExpressionHelper.Divide(PowerQueryExpressionHelper.RoundUp(PowerQueryExpressionHelper.Multiply(formulaExpression, PowerQueryExpressionHelper.NumberLiteral(num)), PowerQueryExpressionHelper.NumberLiteral(0)), PowerQueryExpressionHelper.NumberLiteral(num));
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			FormulaExpression formulaExpression4 = formulaExpression2;
			if (formulaExpression4 == null)
			{
				base.TrackAnomaly(string.Format("Unsupported rounding mode {0}", mode));
			}
			return formulaExpression4;
		}

		// Token: 0x0600CF38 RID: 53048 RVA: 0x002C287C File Offset: 0x002C0A7C
		private FormulaExpression TranslateSlice(Slice slice)
		{
			FormulaExpression formulaExpression = this.Translate(slice.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = PowerQueryExpressionHelper.Minus1(this.Translate(slice.pos1.Node, default(CancellationToken)));
			FormulaExpression formulaExpression3 = PowerQueryExpressionHelper.Minus1(this.Translate(slice.pos2.Node, default(CancellationToken)));
			FormulaNumberLiteral formulaNumberLiteral = formulaExpression2 as FormulaNumberLiteral;
			int? num = ((formulaNumberLiteral != null) ? new int?((int)formulaNumberLiteral.Value) : null);
			FormulaNumberLiteral formulaNumberLiteral2 = formulaExpression3 as FormulaNumberLiteral;
			int? num2 = ((formulaNumberLiteral2 != null) ? new int?((int)formulaNumberLiteral2.Value) : null);
			if (num != null && num.GetValueOrDefault() < 0)
			{
				formulaExpression2 = PowerQueryExpressionHelper.Minus(PowerQueryExpressionHelper.Len(formulaExpression), PowerQueryExpressionHelper.NumberLiteral(-num.Value - 1));
			}
			if (num2 != null && num2.GetValueOrDefault() < 0)
			{
				formulaExpression3 = PowerQueryExpressionHelper.Minus(PowerQueryExpressionHelper.Len(formulaExpression), PowerQueryExpressionHelper.NumberLiteral(-num2.Value - 1));
			}
			if (num != null)
			{
				int valueOrDefault = num.GetValueOrDefault();
				if (num2 != null)
				{
					int valueOrDefault2 = num2.GetValueOrDefault();
					int num3 = valueOrDefault;
					FormulaExpression formulaExpression4;
					if (num3 <= 0)
					{
						if (num3 < 0)
						{
							if (valueOrDefault2 > 0)
							{
								formulaExpression4 = PowerQueryExpressionHelper.Minus(PowerQueryExpressionHelper.NumberLiteral(valueOrDefault2), PowerQueryExpressionHelper.Plus(PowerQueryExpressionHelper.NumberLiteral(valueOrDefault), PowerQueryExpressionHelper.Len(formulaExpression)));
								goto IL_01CA;
							}
							if (valueOrDefault2 < 0)
							{
								formulaExpression4 = PowerQueryExpressionHelper.NumberLiteral(Math.Abs(valueOrDefault - valueOrDefault2));
								goto IL_01CA;
							}
						}
					}
					else
					{
						if (valueOrDefault2 > 0)
						{
							formulaExpression4 = PowerQueryExpressionHelper.NumberLiteral(valueOrDefault2 - valueOrDefault);
							goto IL_01CA;
						}
						if (valueOrDefault2 < 0)
						{
							formulaExpression4 = PowerQueryExpressionHelper.Minus(PowerQueryExpressionHelper.Len(formulaExpression), PowerQueryExpressionHelper.NumberLiteral(Math.Abs(valueOrDefault2 + 1) + valueOrDefault));
							goto IL_01CA;
						}
					}
					formulaExpression4 = null;
					IL_01CA:
					FormulaExpression formulaExpression5 = formulaExpression4;
					if (!(formulaExpression5 == null))
					{
						return PowerQueryExpressionHelper.Mid(formulaExpression, formulaExpression2, formulaExpression5);
					}
					return null;
				}
			}
			return PowerQueryExpressionHelper.Mid(PowerQueryExpressionHelper.Left(formulaExpression, formulaExpression3), formulaExpression2, null);
		}

		// Token: 0x0600CF39 RID: 53049 RVA: 0x002C2A7C File Offset: 0x002C0C7C
		private FormulaExpression TranslateSlicePrefix(SlicePrefix slicePrefix)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slicePrefix.pos.Node, default(CancellationToken));
			FormulaNumberLiteral formulaNumberLiteral = formulaExpression2 as FormulaNumberLiteral;
			int? num = ((formulaNumberLiteral != null) ? new int?((int)formulaNumberLiteral.Value) : null);
			int num2 = 0;
			if (!((num.GetValueOrDefault() < num2) & (num != null)))
			{
				return PowerQueryExpressionHelper.Left(formulaExpression, PowerQueryExpressionHelper.Minus1(formulaExpression2));
			}
			return PowerQueryExpressionHelper.Left(formulaExpression, PowerQueryExpressionHelper.Plus(PowerQueryExpressionHelper.Len(formulaExpression), formulaExpression2));
		}

		// Token: 0x0600CF3A RID: 53050 RVA: 0x002C2B28 File Offset: 0x002C0D28
		private FormulaExpression TranslateSlicePrefixAbs(SlicePrefixAbs slicePrefixAbs)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefixAbs.x.Node, default(CancellationToken));
			int value = slicePrefixAbs.slicePrefixAbsPos.Value;
			FormulaExpression formulaExpression2 = PowerQueryExpressionHelper.NumberLiteral(value);
			if (value >= 0)
			{
				return PowerQueryExpressionHelper.Left(formulaExpression, PowerQueryExpressionHelper.Minus1(formulaExpression2));
			}
			return PowerQueryExpressionHelper.Left(formulaExpression, PowerQueryExpressionHelper.Plus(PowerQueryExpressionHelper.Len(formulaExpression), formulaExpression2));
		}

		// Token: 0x0600CF3B RID: 53051 RVA: 0x002C2B90 File Offset: 0x002C0D90
		private FormulaExpression TranslateSliceSuffix(SliceSuffix sliceSuffix)
		{
			FormulaExpression formulaExpression = this.Translate(sliceSuffix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(sliceSuffix.pos.Node, default(CancellationToken));
			FormulaNumberLiteral formulaNumberLiteral = formulaExpression2 as FormulaNumberLiteral;
			int? num = ((formulaNumberLiteral != null) ? new int?((int)formulaNumberLiteral.Value) : null);
			int? num2 = num;
			int num3 = 0;
			if (!((num2.GetValueOrDefault() < num3) & (num2 != null)))
			{
				return PowerQueryExpressionHelper.Right(formulaExpression, PowerQueryExpressionHelper.Minus(PowerQueryExpressionHelper.Len(formulaExpression), PowerQueryExpressionHelper.Minus1(formulaExpression2)));
			}
			return PowerQueryExpressionHelper.Right(formulaExpression, PowerQueryExpressionHelper.NumberLiteral(-num.Value));
		}

		// Token: 0x0600CF3C RID: 53052 RVA: 0x002C2C48 File Offset: 0x002C0E48
		private FormulaExpression TranslateSplit(Split split)
		{
			FormulaExpression formulaExpression = this.Translate(split.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(split.splitDelimiter.Node, default(CancellationToken));
			int value = split.splitInstance.Value;
			if (value == 0)
			{
				return null;
			}
			FormulaExpression formulaExpression3;
			if (value >= -1)
			{
				if (value != -1)
				{
					if (value == 1)
					{
						formulaExpression3 = PowerQueryExpressionHelper.First(PowerQueryExpressionHelper.Split(formulaExpression, formulaExpression2));
					}
					else
					{
						formulaExpression3 = PowerQueryExpressionHelper.Last(PowerQueryExpressionHelper.FirstN(PowerQueryExpressionHelper.Split(formulaExpression, formulaExpression2), PowerQueryExpressionHelper.NumberLiteral(value)));
					}
				}
				else
				{
					formulaExpression3 = PowerQueryExpressionHelper.Last(PowerQueryExpressionHelper.Split(formulaExpression, formulaExpression2));
				}
			}
			else
			{
				formulaExpression3 = PowerQueryExpressionHelper.First(PowerQueryExpressionHelper.LastN(PowerQueryExpressionHelper.Split(formulaExpression, formulaExpression2), PowerQueryExpressionHelper.NumberLiteral(-value)));
			}
			return formulaExpression3;
		}

		// Token: 0x0600CF3D RID: 53053 RVA: 0x002C2D10 File Offset: 0x002C0F10
		private FormulaExpression TranslateStr(Str str)
		{
			return this.Translate(str.constStr.Node, default(CancellationToken));
		}

		// Token: 0x0600CF3E RID: 53054 RVA: 0x002C2D3C File Offset: 0x002C0F3C
		private FormulaExpression TranslateTrim(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerQueryExpressionHelper.Trim(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CF3F RID: 53055 RVA: 0x002C2D74 File Offset: 0x002C0F74
		private FormulaExpression TranslateUpperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerQueryExpressionHelper.Upper(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CF40 RID: 53056 RVA: 0x002C2DAA File Offset: 0x002C0FAA
		private FormulaExpression TranslateVariableNode()
		{
			return this._currentInputExpression;
		}

		// Token: 0x0600CF41 RID: 53057 RVA: 0x002C2DB4 File Offset: 0x002C0FB4
		private FormulaExpression TranslateDateTimePart(DateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = this.Translate(dateTimePart.Node.Children[0], default(CancellationToken));
			DateTimePartKind value = dateTimePart.dateTimePartKind.Value;
			FormulaExpression formulaExpression2 = formulaExpression;
			PowerQueryProgramTranslator.<>c__DisplayClass31_0 CS$<>8__locals1;
			CS$<>8__locals1.withRecords = new List<Dictionary<string, FormulaExpression>>();
			if (!(formulaExpression is PowerQueryVariable))
			{
				PowerQueryVariable powerQueryVariable = (PowerQueryVariable)PowerQueryExpressionHelper.Variable("date");
				PowerQueryProgramTranslator.<TranslateDateTimePart>g__NewField|31_0(powerQueryVariable, formulaExpression2, ref CS$<>8__locals1);
				formulaExpression2 = powerQueryVariable;
			}
			bool flag = value - DateTimePartKind.MonthWeek <= 1;
			if (flag)
			{
				PowerQueryProgramTranslator.<TranslateDateTimePart>g__NewField|31_0((PowerQueryVariable)PowerQueryExpressionHelper.Variable("monthStart"), PowerQueryExpressionHelper.MonthStart(formulaExpression2), ref CS$<>8__locals1);
			}
			PowerQueryVariable powerQueryVariable2 = null;
			flag = value - DateTimePartKind.QuarterDay <= 2;
			if (flag)
			{
				PowerQueryProgramTranslator.<TranslateDateTimePart>g__NewField|31_0((PowerQueryVariable)PowerQueryExpressionHelper.Variable("quarter"), PowerQueryExpressionHelper.Quarter(formulaExpression2), ref CS$<>8__locals1);
				powerQueryVariable2 = (PowerQueryVariable)PowerQueryExpressionHelper.Variable("quarterStart");
				PowerQueryProgramTranslator.<TranslateDateTimePart>g__NewField|31_0(powerQueryVariable2, PowerQueryExpressionHelper.QuarterStart(formulaExpression2), ref CS$<>8__locals1);
			}
			PowerQueryVariable powerQueryVariable3 = null;
			if (value == DateTimePartKind.QuarterDays)
			{
				powerQueryVariable3 = (PowerQueryVariable)PowerQueryExpressionHelper.Variable("quarterEnd");
				PowerQueryProgramTranslator.<TranslateDateTimePart>g__NewField|31_0(powerQueryVariable3, PowerQueryExpressionHelper.QuarterEnd(formulaExpression2, powerQueryVariable2), ref CS$<>8__locals1);
			}
			PowerQueryVariable powerQueryVariable4 = null;
			PowerQueryVariable powerQueryVariable5 = null;
			if (value == DateTimePartKind.YearDays)
			{
				powerQueryVariable4 = (PowerQueryVariable)PowerQueryExpressionHelper.Variable("yearStart");
				PowerQueryProgramTranslator.<TranslateDateTimePart>g__NewField|31_0(powerQueryVariable4, PowerQueryExpressionHelper.YearStart(formulaExpression2), ref CS$<>8__locals1);
				powerQueryVariable5 = (PowerQueryVariable)PowerQueryExpressionHelper.Variable("yearEnd");
				PowerQueryProgramTranslator.<TranslateDateTimePart>g__NewField|31_0(powerQueryVariable5, PowerQueryExpressionHelper.YearEnd(powerQueryVariable4), ref CS$<>8__locals1);
			}
			FormulaExpression formulaExpression3;
			switch (value)
			{
			case DateTimePartKind.Second:
				formulaExpression3 = PowerQueryExpressionHelper.Second(formulaExpression);
				break;
			case DateTimePartKind.Minute:
				formulaExpression3 = PowerQueryExpressionHelper.Minute(formulaExpression);
				break;
			case DateTimePartKind.Hour:
				formulaExpression3 = PowerQueryExpressionHelper.Hour(formulaExpression);
				break;
			case DateTimePartKind.WeekDay:
				formulaExpression3 = PowerQueryExpressionHelper.Weekday(formulaExpression);
				break;
			case DateTimePartKind.MonthDay:
				formulaExpression3 = PowerQueryExpressionHelper.Day(formulaExpression);
				break;
			case DateTimePartKind.MonthWeek:
				formulaExpression3 = PowerQueryExpressionHelper.MonthWeek(formulaExpression);
				break;
			case DateTimePartKind.MonthDays:
				formulaExpression3 = PowerQueryExpressionHelper.MonthDays(formulaExpression);
				break;
			case DateTimePartKind.Month:
				formulaExpression3 = PowerQueryExpressionHelper.Month(formulaExpression);
				break;
			case DateTimePartKind.QuarterDay:
				formulaExpression3 = PowerQueryExpressionHelper.QuarterDay(formulaExpression, powerQueryVariable2);
				break;
			case DateTimePartKind.QuarterWeek:
				formulaExpression3 = PowerQueryExpressionHelper.QuarterWeek(formulaExpression, powerQueryVariable2);
				break;
			case DateTimePartKind.QuarterDays:
				formulaExpression3 = PowerQueryExpressionHelper.QuarterDays(formulaExpression, powerQueryVariable2, powerQueryVariable3);
				break;
			case DateTimePartKind.Quarter:
				formulaExpression3 = PowerQueryExpressionHelper.Quarter(formulaExpression);
				break;
			case DateTimePartKind.YearDay:
				formulaExpression3 = PowerQueryExpressionHelper.YearDay(formulaExpression);
				break;
			case DateTimePartKind.YearWeek:
				formulaExpression3 = PowerQueryExpressionHelper.YearWeek(formulaExpression);
				break;
			case DateTimePartKind.YearDays:
				formulaExpression3 = PowerQueryExpressionHelper.YearDays(formulaExpression, powerQueryVariable4, powerQueryVariable5);
				break;
			case DateTimePartKind.Year:
				formulaExpression3 = PowerQueryExpressionHelper.Year(formulaExpression);
				break;
			default:
				formulaExpression3 = null;
				break;
			}
			FormulaExpression formulaExpression4 = formulaExpression3;
			return PowerQueryExpressionHelper.With(CS$<>8__locals1.withRecords, formulaExpression4);
		}

		// Token: 0x0600CF42 RID: 53058 RVA: 0x002C302C File Offset: 0x002C122C
		private FormulaExpression TranslateFormatDateTime(FormatDateTime formatDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(formatDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = formatDateTime.dateTimeFormatDesc.Value;
			return PowerQueryExpressionHelper.FormatDateTime(formulaExpression, value.Mask, value.Locale);
		}

		// Token: 0x0600CF43 RID: 53059 RVA: 0x002C3078 File Offset: 0x002C1278
		private FormulaExpression TranslateParseDateTime(ParseDateTime parseDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(parseDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = parseDateTime.dateTimeParseDesc.Value;
			string mask = value.Mask;
			return PowerQueryExpressionHelper.ParseDateTime(formulaExpression, mask, value.Locale);
		}

		// Token: 0x0600CF44 RID: 53060 RVA: 0x002C30C8 File Offset: 0x002C12C8
		private FormulaExpression TranslateRoundDateTime(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression;
			switch (roundDateTime.dateTimeRoundDesc.Value.Mode)
			{
			case RoundingMode.Nearest:
				formulaExpression = this.TranslateRoundDateTimeNearest(roundDateTime);
				break;
			case RoundingMode.Down:
				formulaExpression = this.TranslateRoundDateTimeDown(roundDateTime);
				break;
			case RoundingMode.Up:
				formulaExpression = this.TranslateRoundDateTimeUp(roundDateTime);
				break;
			default:
				formulaExpression = null;
				break;
			}
			return formulaExpression;
		}

		// Token: 0x0600CF45 RID: 53061 RVA: 0x002C3120 File Offset: 0x002C1320
		private FormulaExpression TranslateRoundDateTimeDown(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimePeriod period = roundDateTime.dateTimeRoundDesc.Value.Period;
			PowerQueryProgramTranslator.<>c__DisplayClass35_0 CS$<>8__locals1;
			CS$<>8__locals1.withRecords = new List<IReadOnlyDictionary<string, FormulaExpression>>();
			if (!(formulaExpression is PowerQueryVariable))
			{
				PowerQueryVariable powerQueryVariable = (PowerQueryVariable)PowerQueryExpressionHelper.Variable("date");
				PowerQueryProgramTranslator.<TranslateRoundDateTimeDown>g__NewField|35_0(powerQueryVariable, formulaExpression, ref CS$<>8__locals1);
				formulaExpression = powerQueryVariable;
			}
			if (period == RoundDateTimePeriod.Quarter)
			{
				PowerQueryProgramTranslator.<TranslateRoundDateTimeDown>g__NewField|35_0((PowerQueryVariable)PowerQueryExpressionHelper.Variable("quarter"), PowerQueryExpressionHelper.Quarter(formulaExpression), ref CS$<>8__locals1);
			}
			FormulaExpression formulaExpression2;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression2 = PowerQueryExpressionHelper.SecondStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = PowerQueryExpressionHelper.MinuteStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = PowerQueryExpressionHelper.HourStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = PowerQueryExpressionHelper.DayStart(formulaExpression, true);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = PowerQueryExpressionHelper.WeekStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = PowerQueryExpressionHelper.MonthStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = PowerQueryExpressionHelper.QuarterStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = PowerQueryExpressionHelper.YearStart(formulaExpression);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			FormulaExpression formulaExpression3 = formulaExpression2;
			PowerQueryVariable powerQueryVariable2 = (PowerQueryVariable)PowerQueryExpressionHelper.Variable(period.ToString().ToLower() + "Start");
			PowerQueryProgramTranslator.<TranslateRoundDateTimeDown>g__NewField|35_0(powerQueryVariable2, formulaExpression3, ref CS$<>8__locals1);
			return PowerQueryExpressionHelper.With(CS$<>8__locals1.withRecords, powerQueryVariable2);
		}

		// Token: 0x0600CF46 RID: 53062 RVA: 0x002C3270 File Offset: 0x002C1470
		private FormulaExpression TranslateRoundDateTimeNearest(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimePeriod period = roundDateTime.dateTimeRoundDesc.Value.Period;
			FormulaExpression formulaExpression2 = formulaExpression;
			PowerQueryProgramTranslator.<>c__DisplayClass36_0 CS$<>8__locals1;
			CS$<>8__locals1.withRecords = new List<Dictionary<string, FormulaExpression>>();
			if (!(formulaExpression is PowerQueryVariable))
			{
				PowerQueryVariable powerQueryVariable = (PowerQueryVariable)PowerQueryExpressionHelper.Variable("date");
				PowerQueryProgramTranslator.<TranslateRoundDateTimeNearest>g__NewField|36_0(powerQueryVariable, formulaExpression2, ref CS$<>8__locals1);
				formulaExpression2 = powerQueryVariable;
			}
			FormulaExpression formulaExpression3;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression3 = PowerQueryExpressionHelper.SecondStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression3 = PowerQueryExpressionHelper.MinuteStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression3 = PowerQueryExpressionHelper.HourStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression3 = PowerQueryExpressionHelper.DayStart(formulaExpression2, true);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression3 = PowerQueryExpressionHelper.WeekStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression3 = PowerQueryExpressionHelper.MonthStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression3 = PowerQueryExpressionHelper.QuarterStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression3 = PowerQueryExpressionHelper.YearStart(formulaExpression2);
				break;
			default:
				formulaExpression3 = null;
				break;
			}
			FormulaExpression formulaExpression4 = formulaExpression3;
			if (formulaExpression4 == null)
			{
				return null;
			}
			PowerQueryVariable powerQueryVariable2 = (PowerQueryVariable)PowerQueryExpressionHelper.Variable(period.ToString().ToLower() + "Start");
			PowerQueryProgramTranslator.<TranslateRoundDateTimeNearest>g__NewField|36_0(powerQueryVariable2, formulaExpression4, ref CS$<>8__locals1);
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression3 = PowerQueryExpressionHelper.SecondEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression3 = PowerQueryExpressionHelper.MinuteEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression3 = PowerQueryExpressionHelper.HourEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression3 = PowerQueryExpressionHelper.DayEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression3 = PowerQueryExpressionHelper.WeekEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression3 = PowerQueryExpressionHelper.MonthEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression3 = PowerQueryExpressionHelper.QuarterEnd(formulaExpression2, powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression3 = PowerQueryExpressionHelper.YearEnd(powerQueryVariable2);
				break;
			default:
				formulaExpression3 = null;
				break;
			}
			FormulaExpression formulaExpression5 = formulaExpression3;
			if (formulaExpression5 == null)
			{
				return null;
			}
			PowerQueryVariable powerQueryVariable3 = (PowerQueryVariable)PowerQueryExpressionHelper.Variable(period.ToString().ToLower() + "End");
			PowerQueryProgramTranslator.<TranslateRoundDateTimeNearest>g__NewField|36_0(powerQueryVariable3, formulaExpression5, ref CS$<>8__locals1);
			PowerQueryVariable powerQueryVariable4 = (PowerQueryVariable)PowerQueryExpressionHelper.Variable(period.ToString().ToLower() + "Midpoint");
			FormulaExpression formulaExpression6 = PowerQueryExpressionHelper.Midpoint(powerQueryVariable2, powerQueryVariable3);
			PowerQueryProgramTranslator.<TranslateRoundDateTimeNearest>g__NewField|36_0(powerQueryVariable4, formulaExpression6, ref CS$<>8__locals1);
			return PowerQueryExpressionHelper.With(CS$<>8__locals1.withRecords, PowerQueryExpressionHelper.If(PowerQueryExpressionHelper.LessThan(formulaExpression2, powerQueryVariable4), powerQueryVariable2, powerQueryVariable3));
		}

		// Token: 0x0600CF47 RID: 53063 RVA: 0x002C34BC File Offset: 0x002C16BC
		private FormulaExpression TranslateRoundDateTimeUp(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimeDescriptor value = roundDateTime.dateTimeRoundDesc.Value;
			RoundDateTimePeriod period = value.Period;
			bool flag = value.Ceiling == RoundDatePeriodCeiling.LastDay;
			FormulaExpression formulaExpression2 = formulaExpression;
			PowerQueryProgramTranslator.<>c__DisplayClass37_0 CS$<>8__locals1;
			CS$<>8__locals1.withRecords = new List<Dictionary<string, FormulaExpression>>();
			if (!(formulaExpression is PowerQueryVariable))
			{
				PowerQueryVariable powerQueryVariable = (PowerQueryVariable)PowerQueryExpressionHelper.Variable("date");
				PowerQueryProgramTranslator.<TranslateRoundDateTimeUp>g__NewField|37_0(powerQueryVariable, formulaExpression2, ref CS$<>8__locals1);
				formulaExpression2 = powerQueryVariable;
			}
			if (period == RoundDateTimePeriod.Quarter)
			{
				PowerQueryProgramTranslator.<TranslateRoundDateTimeUp>g__NewField|37_0((PowerQueryVariable)PowerQueryExpressionHelper.Variable("quarter"), PowerQueryExpressionHelper.Quarter(formulaExpression2), ref CS$<>8__locals1);
			}
			FormulaExpression formulaExpression3;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression3 = PowerQueryExpressionHelper.SecondStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression3 = PowerQueryExpressionHelper.MinuteStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression3 = PowerQueryExpressionHelper.HourStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression3 = PowerQueryExpressionHelper.DayStart(formulaExpression2, true);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression3 = PowerQueryExpressionHelper.WeekStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression3 = PowerQueryExpressionHelper.MonthStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression3 = PowerQueryExpressionHelper.QuarterStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression3 = PowerQueryExpressionHelper.YearStart(formulaExpression2);
				break;
			default:
				formulaExpression3 = null;
				break;
			}
			FormulaExpression formulaExpression4 = formulaExpression3;
			if (formulaExpression4 == null)
			{
				return null;
			}
			PowerQueryVariable powerQueryVariable2 = (PowerQueryVariable)PowerQueryExpressionHelper.Variable(period.ToString().ToLower() + "Start");
			PowerQueryProgramTranslator.<TranslateRoundDateTimeUp>g__NewField|37_0(powerQueryVariable2, formulaExpression4, ref CS$<>8__locals1);
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression3 = PowerQueryExpressionHelper.SecondEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression3 = PowerQueryExpressionHelper.MinuteEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression3 = PowerQueryExpressionHelper.HourEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression3 = PowerQueryExpressionHelper.DayEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression3 = PowerQueryExpressionHelper.WeekEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression3 = PowerQueryExpressionHelper.MonthEnd(powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression3 = PowerQueryExpressionHelper.QuarterEnd(formulaExpression2, powerQueryVariable2);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression3 = PowerQueryExpressionHelper.YearEnd(powerQueryVariable2);
				break;
			default:
				formulaExpression3 = null;
				break;
			}
			FormulaExpression formulaExpression5 = formulaExpression3;
			if (formulaExpression5 == null)
			{
				return null;
			}
			PowerQueryVariable powerQueryVariable3 = (PowerQueryVariable)PowerQueryExpressionHelper.Variable(period.ToString().ToLower() + "End");
			PowerQueryProgramTranslator.<TranslateRoundDateTimeUp>g__NewField|37_0(powerQueryVariable3, formulaExpression5, ref CS$<>8__locals1);
			FormulaExpression formulaExpression6 = (flag ? PowerQueryExpressionHelper.Func("Date.AddDays", new FormulaExpression[]
			{
				powerQueryVariable3,
				PowerQueryExpressionHelper.NumberLiteral(-1)
			}) : PowerQueryExpressionHelper.If(PowerQueryExpressionHelper.Equal(formulaExpression2, powerQueryVariable2), powerQueryVariable2, powerQueryVariable3));
			return PowerQueryExpressionHelper.With(CS$<>8__locals1.withRecords, formulaExpression6);
		}

		// Token: 0x0600CF48 RID: 53064 RVA: 0x002C3720 File Offset: 0x002C1920
		private FormulaExpression TranslateAdd(Add add)
		{
			return PowerQueryExpressionHelper.Plus(this.Translate(add.arithmeticLeft.Node, default(CancellationToken)), this.Translate(add.addRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600CF49 RID: 53065 RVA: 0x002C376E File Offset: 0x002C196E
		private static FormulaExpression TranslateAverage(Average average)
		{
			return PowerQueryExpressionHelper.Average(PowerQueryProgramTranslator.TranslateFromNumbers(average.fromNumbers));
		}

		// Token: 0x0600CF4A RID: 53066 RVA: 0x002C3784 File Offset: 0x002C1984
		private FormulaExpression TranslateDivide(Divide divide)
		{
			return PowerQueryExpressionHelper.Divide(this.Translate(divide.arithmeticLeft.Node, default(CancellationToken)), this.Translate(divide.divideRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600CF4B RID: 53067 RVA: 0x002C37D4 File Offset: 0x002C19D4
		private static IEnumerable<FormulaExpression> TranslateFromNumbers(fromNumbers fromNumbers)
		{
			LiteralNode literalNode = fromNumbers.Node.Children[1] as LiteralNode;
			string[] array = ((literalNode != null) ? literalNode.Value : null) as string[];
			if (array != null)
			{
				IEnumerable<string> enumerable = array;
				Func<string, FormulaExpression> func;
				if ((func = PowerQueryProgramTranslator.<>O.<0>__ColumnLookup) == null)
				{
					func = (PowerQueryProgramTranslator.<>O.<0>__ColumnLookup = new Func<string, FormulaExpression>(PowerQueryExpressionHelper.ColumnLookup<double>));
				}
				return enumerable.Select(func);
			}
			return null;
		}

		// Token: 0x0600CF4C RID: 53068 RVA: 0x002C382C File Offset: 0x002C1A2C
		private FormulaExpression TranslateMultiply(Multiply multiply)
		{
			return PowerQueryExpressionHelper.Multiply(this.Translate(multiply.arithmeticLeft.Node, default(CancellationToken)), this.Translate(multiply.multiplyRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600CF4D RID: 53069 RVA: 0x002C387A File Offset: 0x002C1A7A
		private static FormulaExpression TranslateProduct(Product product)
		{
			return PowerQueryExpressionHelper.Product(PowerQueryProgramTranslator.TranslateFromNumbers(product.fromNumbers));
		}

		// Token: 0x0600CF4E RID: 53070 RVA: 0x002C3890 File Offset: 0x002C1A90
		private FormulaExpression TranslateSubtract(Subtract subtract)
		{
			return PowerQueryExpressionHelper.Minus(this.Translate(subtract.arithmeticLeft.Node, default(CancellationToken)), this.Translate(subtract.subtractRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600CF4F RID: 53071 RVA: 0x002C38DE File Offset: 0x002C1ADE
		private static FormulaExpression TranslateSum(Sum sum)
		{
			return PowerQueryExpressionHelper.Sum(PowerQueryProgramTranslator.TranslateFromNumbers(sum.fromNumbers));
		}

		// Token: 0x0600CF50 RID: 53072 RVA: 0x002C38F4 File Offset: 0x002C1AF4
		private FormulaExpression TranslateContains(Contains contains)
		{
			FormulaExpression formulaExpression = PowerQueryExpressionHelper.ColumnLookup(contains.columnName.Value, base.ResolveInputType(contains.columnName.Value));
			FormulaExpression formulaExpression2 = this.Translate(contains.containsFindText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(contains.containsCount.Node, default(CancellationToken));
			return PowerQueryExpressionHelper.Equal(PowerQueryExpressionHelper.Minus(PowerQueryExpressionHelper.Len(formulaExpression), PowerQueryExpressionHelper.Len(PowerQueryExpressionHelper.Replace(formulaExpression, formulaExpression2, PowerQueryExpressionHelper.StringLiteral(string.Empty)))), formulaExpression3);
		}

		// Token: 0x0600CF51 RID: 53073 RVA: 0x002C3994 File Offset: 0x002C1B94
		private FormulaExpression TranslateEndsWithDigit(EndsWithDigit endsWithDigit)
		{
			return PowerQueryExpressionHelper.IsDigit(PowerQueryExpressionHelper.Right(PowerQueryExpressionHelper.ColumnLookup(endsWithDigit.columnName.Value, base.ResolveInputType(endsWithDigit.columnName.Value)), 1));
		}

		// Token: 0x0600CF52 RID: 53074 RVA: 0x002C39D8 File Offset: 0x002C1BD8
		private FormulaExpression TranslateIf(If ifNode)
		{
			FormulaExpression formulaExpression = this.Translate(ifNode.condition.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(ifNode.result1.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(ifNode.result2.Node, default(CancellationToken));
			return PowerQueryExpressionHelper.If(formulaExpression, formulaExpression2, formulaExpression3);
		}

		// Token: 0x0600CF53 RID: 53075 RVA: 0x002C3A4C File Offset: 0x002C1C4C
		private FormulaExpression TranslateOr(Or or)
		{
			FormulaExpression formulaExpression = this.Translate(or.condition1.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(or.condition2.Node, default(CancellationToken));
			return PowerQueryExpressionHelper.Or(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CF54 RID: 53076 RVA: 0x002C3A9C File Offset: 0x002C1C9C
		private FormulaExpression TranslateIsBlank(IsBlank isBlank)
		{
			FormulaExpression formulaExpression = PowerQueryExpressionHelper.ColumnLookup(isBlank.columnName.Value, base.ResolveInputType(isBlank.columnName.Value));
			return PowerQueryExpressionHelper.Or(PowerQueryExpressionHelper.Equal(formulaExpression, PowerQueryExpressionHelper.StringLiteral(string.Empty)), PowerQueryExpressionHelper.Equal(formulaExpression, PowerQueryExpressionHelper.Null()));
		}

		// Token: 0x0600CF55 RID: 53077 RVA: 0x002C3AF4 File Offset: 0x002C1CF4
		private FormulaExpression TranslateIsNotBlank(IsNotBlank isNotBlank)
		{
			return PowerQueryExpressionHelper.Not(PowerQueryExpressionHelper.Equal(PowerQueryExpressionHelper.ColumnLookup(isNotBlank.columnName.Value, base.ResolveInputType(isNotBlank.columnName.Value)), PowerQueryExpressionHelper.Null()));
		}

		// Token: 0x0600CF56 RID: 53078 RVA: 0x002C3B3C File Offset: 0x002C1D3C
		private FormulaExpression TranslateIsNumber(IsNumber isNumber)
		{
			return PowerQueryExpressionHelper.IsNumber(PowerQueryExpressionHelper.ColumnLookup(isNumber.columnName.Value, base.ResolveInputType(isNumber.columnName.Value)));
		}

		// Token: 0x0600CF57 RID: 53079 RVA: 0x002C3B78 File Offset: 0x002C1D78
		private FormulaExpression TranslateIsString(IsString isString)
		{
			return PowerQueryExpressionHelper.IsText(PowerQueryExpressionHelper.ColumnLookup(isString.columnName.Value, base.ResolveInputType(isString.columnName.Value)));
		}

		// Token: 0x0600CF58 RID: 53080 RVA: 0x002C3BB3 File Offset: 0x002C1DB3
		private static FormulaExpression TranslateNull()
		{
			return PowerQueryExpressionHelper.Null();
		}

		// Token: 0x0600CF59 RID: 53081 RVA: 0x002C3BBC File Offset: 0x002C1DBC
		private FormulaExpression TranslateNumberEquals(NumberEquals numberEquals)
		{
			FormulaExpression formulaExpression = PowerQueryExpressionHelper.ColumnLookup(numberEquals.columnName.Value, base.ResolveInputType(numberEquals.columnName.Value));
			FormulaExpression formulaExpression2 = this.Translate(numberEquals.numberEqualsValue.Node, default(CancellationToken));
			return PowerQueryExpressionHelper.Equal(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CF5A RID: 53082 RVA: 0x002C3C18 File Offset: 0x002C1E18
		private FormulaExpression TranslateNumberGreaterThan(NumberGreaterThan numberGreaterThan)
		{
			FormulaExpression formulaExpression = PowerQueryExpressionHelper.ColumnLookup(numberGreaterThan.columnName.Value, base.ResolveInputType(numberGreaterThan.columnName.Value));
			FormulaExpression formulaExpression2 = this.Translate(numberGreaterThan.numberGreaterThanValue.Node, default(CancellationToken));
			return PowerQueryExpressionHelper.And(PowerQueryExpressionHelper.IsNumber(formulaExpression), PowerQueryExpressionHelper.GreaterThan(formulaExpression, formulaExpression2));
		}

		// Token: 0x0600CF5B RID: 53083 RVA: 0x002C3C84 File Offset: 0x002C1E84
		private FormulaExpression TranslateNumberLessThan(NumberLessThan numberLessThan)
		{
			FormulaExpression formulaExpression = PowerQueryExpressionHelper.ColumnLookup(numberLessThan.columnName.Value, base.ResolveInputType(numberLessThan.columnName.Value));
			FormulaExpression formulaExpression2 = this.Translate(numberLessThan.numberLessThanValue.Node, default(CancellationToken));
			return PowerQueryExpressionHelper.And(PowerQueryExpressionHelper.IsNumber(formulaExpression), PowerQueryExpressionHelper.LessThan(formulaExpression, formulaExpression2));
		}

		// Token: 0x0600CF5C RID: 53084 RVA: 0x002C3CF0 File Offset: 0x002C1EF0
		private FormulaExpression TranslateStartsWith(StartsWith startsWith)
		{
			FormulaExpression formulaExpression = PowerQueryExpressionHelper.ColumnLookup(startsWith.columnName.Value, base.ResolveInputType(startsWith.columnName.Value));
			FormulaExpression formulaExpression2 = this.Translate(startsWith.startsWithFindText.Node, default(CancellationToken));
			return PowerQueryExpressionHelper.Equal(PowerQueryExpressionHelper.Left(formulaExpression, PowerQueryExpressionHelper.Len(formulaExpression2)), formulaExpression2);
		}

		// Token: 0x0600CF5D RID: 53085 RVA: 0x002C3D58 File Offset: 0x002C1F58
		private FormulaExpression TranslateStartsWithDigit(StartsWithDigit startsWithDigit)
		{
			return PowerQueryExpressionHelper.IsDigit(PowerQueryExpressionHelper.Left(PowerQueryExpressionHelper.ColumnLookup(startsWithDigit.columnName.Value, base.ResolveInputType(startsWithDigit.columnName.Value)), 1));
		}

		// Token: 0x0600CF5E RID: 53086 RVA: 0x002C3D9C File Offset: 0x002C1F9C
		private FormulaExpression TranslateStringEquals(StringEquals stringEquals)
		{
			FormulaExpression formulaExpression = PowerQueryExpressionHelper.ColumnLookup(stringEquals.columnName.Value, base.ResolveInputType(stringEquals.columnName.Value));
			FormulaExpression formulaExpression2 = this.Translate(stringEquals.equalsText.Node, default(CancellationToken));
			return PowerQueryExpressionHelper.Equal(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CF5F RID: 53087 RVA: 0x002C3DF8 File Offset: 0x002C1FF8
		private static FormulaExpression TranslateFromDateTime(FromDateTime dateTime)
		{
			return PowerQueryExpressionHelper.ColumnLookup<DateTime>(dateTime.columnName.Value);
		}

		// Token: 0x0600CF60 RID: 53088 RVA: 0x002C3E1C File Offset: 0x002C201C
		private FormulaExpression TranslateFromNumber(FromNumber fromNumber)
		{
			return PowerQueryExpressionHelper.ColumnLookup(fromNumber.columnName.Value, base.ResolveInputType(fromNumber.columnName.Value));
		}

		// Token: 0x0600CF61 RID: 53089 RVA: 0x002C3E54 File Offset: 0x002C2054
		private static FormulaExpression TranslateFromStr(FromStr input)
		{
			return PowerQueryExpressionHelper.ColumnLookup<string>(input.columnName.Value);
		}

		// Token: 0x0600CF62 RID: 53090 RVA: 0x002C3E78 File Offset: 0x002C2078
		private FormulaExpression TranslateToDateTime(ToDateTime subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CF63 RID: 53091 RVA: 0x002C3EA4 File Offset: 0x002C20A4
		private FormulaExpression TranslateToDecimal(ToDecimal subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CF64 RID: 53092 RVA: 0x002C3ED0 File Offset: 0x002C20D0
		private FormulaExpression TranslateToDouble(ToDouble subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CF65 RID: 53093 RVA: 0x002C3EFC File Offset: 0x002C20FC
		private FormulaExpression TranslateToInt(ToInt subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CF66 RID: 53094 RVA: 0x002C3F28 File Offset: 0x002C2128
		private FormulaExpression TranslateToStr(ToStr subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CF67 RID: 53095 RVA: 0x002C3F54 File Offset: 0x002C2154
		[CompilerGenerated]
		internal static void <TranslateDateTimePart>g__NewField|31_0(FormulaVariable key, FormulaExpression exp, ref PowerQueryProgramTranslator.<>c__DisplayClass31_0 A_2)
		{
			Dictionary<string, FormulaExpression> dictionary = new Dictionary<string, FormulaExpression>();
			A_2.withRecords.Add(dictionary);
			dictionary.Add(key.Name, exp);
		}

		// Token: 0x0600CF68 RID: 53096 RVA: 0x002C3F80 File Offset: 0x002C2180
		[CompilerGenerated]
		internal static void <TranslateRoundDateTimeDown>g__NewField|35_0(FormulaVariable key, FormulaExpression exp, ref PowerQueryProgramTranslator.<>c__DisplayClass35_0 A_2)
		{
			Dictionary<string, FormulaExpression> dictionary = new Dictionary<string, FormulaExpression>();
			A_2.withRecords.Add(dictionary);
			dictionary.Add(key.Name, exp);
		}

		// Token: 0x0600CF69 RID: 53097 RVA: 0x002C3FAC File Offset: 0x002C21AC
		[CompilerGenerated]
		internal static void <TranslateRoundDateTimeNearest>g__NewField|36_0(FormulaVariable key, FormulaExpression exp, ref PowerQueryProgramTranslator.<>c__DisplayClass36_0 A_2)
		{
			Dictionary<string, FormulaExpression> dictionary = new Dictionary<string, FormulaExpression>();
			A_2.withRecords.Add(dictionary);
			dictionary.Add(key.Name, exp);
		}

		// Token: 0x0600CF6A RID: 53098 RVA: 0x002C3FD8 File Offset: 0x002C21D8
		[CompilerGenerated]
		internal static void <TranslateRoundDateTimeUp>g__NewField|37_0(FormulaVariable key, FormulaExpression exp, ref PowerQueryProgramTranslator.<>c__DisplayClass37_0 A_2)
		{
			Dictionary<string, FormulaExpression> dictionary = new Dictionary<string, FormulaExpression>();
			A_2.withRecords.Add(dictionary);
			dictionary.Add(key.Name, exp);
		}

		// Token: 0x040050C4 RID: 20676
		private bool _cancelled;

		// Token: 0x040050C5 RID: 20677
		private FormulaExpression _currentInputExpression;

		// Token: 0x040050C6 RID: 20678
		private readonly IPowerQueryTranslationOptions _options;

		// Token: 0x020018CA RID: 6346
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040050C7 RID: 20679
			public static Func<string, FormulaExpression> <0>__ColumnLookup;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001912 RID: 6418
	internal class PowerAutomateProgramTranslator : ProgramTranslatorBase
	{
		// Token: 0x0600D190 RID: 53648 RVA: 0x002CA60C File Offset: 0x002C880C
		private PowerAutomateProgramTranslator(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, IPowerAutomateTranslationOptions options, ILogger logger)
			: base(program, examples, inputs, logger)
		{
			this._options = options ?? new PowerAutomateTranslationConstraint();
		}

		// Token: 0x0600D191 RID: 53649 RVA: 0x002CA62A File Offset: 0x002C882A
		public static FormulaExpression Translate(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, IPowerAutomateTranslationOptions options, ILogger logger = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new PowerAutomateProgramTranslator(program, examples, inputs, options, logger).Translate(cancellationToken);
		}

		// Token: 0x0600D192 RID: 53650 RVA: 0x002CA640 File Offset: 0x002C8840
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
				SliceBetween sliceBetween;
				Replace replace;
				Abs abs;
				Str str;
				Number number;
				Date date;
				Find find;
				ParseDateTime parseDateTime;
				FormatDateTime formatDateTime;
				ParseNumber parseNumber;
				FormatNumber formatNumber;
				FromDateTime fromDateTime;
				FromDateTimePart fromDateTimePart;
				if (node.IsLowerCase())
				{
					formulaExpression = this.TranslateLowerCase(node);
				}
				else if (node.IsUpperCase())
				{
					formulaExpression = this.TranslateUpperCase(node);
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
				else if (node.Is(out sliceBetween))
				{
					formulaExpression = this.TranslateSliceBetween(sliceBetween);
				}
				else if (node.Is(out replace))
				{
					formulaExpression = this.TranslateReplace(replace);
				}
				else if (node.Is(out abs))
				{
					formulaExpression = PowerAutomateProgramTranslator.TranslateAbs(abs);
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
					formulaExpression = PowerAutomateProgramTranslator.TranslateArithmeticRightNumber(node);
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
				else if (node.Is(out parseDateTime))
				{
					formulaExpression = this.TranslateParseDateTime(parseDateTime);
				}
				else if (node.Is(out formatDateTime))
				{
					formulaExpression = this.TranslateFormatDateTime(formatDateTime);
				}
				else if (node.Is(out parseNumber))
				{
					formulaExpression = this.TranslateParseNumber(parseNumber);
				}
				else if (node.Is(out formatNumber))
				{
					formulaExpression = this.TranslateFormatNumber(formatNumber);
				}
				else if (node.Is(out fromDateTime))
				{
					formulaExpression = PowerAutomateProgramTranslator.TranslateFromDateTime(fromDateTime);
				}
				else if (node.Is(out fromDateTimePart))
				{
					formulaExpression = PowerAutomateProgramTranslator.TranslateFromDateTimePart(fromDateTimePart);
				}
				else if (!(node is VariableNode))
				{
					if (node != null)
					{
						if (node.GrammarRule is ConversionRule)
						{
							formulaExpression = this.TranslateConversionRule(node);
							goto IL_05E2;
						}
						LiteralNode literalNode = node as LiteralNode;
						if (literalNode != null)
						{
							formulaExpression = PowerAutomateProgramTranslator.TranslateLiteral(literalNode);
							goto IL_05E2;
						}
					}
					FromStr fromStr;
					FromDateTime fromDateTime2;
					FromNumber fromNumber;
					ToStr toStr;
					ToInt toInt;
					ToDouble toDouble;
					ToDecimal toDecimal;
					ToDateTime toDateTime;
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
					if (node.Is(out fromStr))
					{
						formulaExpression = PowerAutomateProgramTranslator.TranslateFromStr(fromStr);
					}
					else if (node.Is(out fromDateTime2))
					{
						formulaExpression = PowerAutomateProgramTranslator.TranslateFromDateTime(fromDateTime2);
					}
					else if (node.Is(out fromNumber))
					{
						formulaExpression = PowerAutomateProgramTranslator.TranslateFromNumber(fromNumber);
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
						formulaExpression = PowerAutomateProgramTranslator.TranslateSum(sum);
					}
					else if (node.Is(out product))
					{
						formulaExpression = PowerAutomateProgramTranslator.TranslateProduct(product);
					}
					else if (node.Is(out average))
					{
						formulaExpression = PowerAutomateProgramTranslator.TranslateAverage(average);
					}
					else if (node.Is(out @if))
					{
						formulaExpression = this.TranslateIf(@if);
					}
					else if (node.Is(out isString))
					{
						formulaExpression = PowerAutomateProgramTranslator.TranslateIsString(isString);
					}
					else if (node.Is(out isBlank))
					{
						formulaExpression = PowerAutomateProgramTranslator.TranslateIsBlank(isBlank);
					}
					else if (node.Is(out isNotBlank))
					{
						formulaExpression = PowerAutomateProgramTranslator.TranslateIsNotBlank(isNotBlank);
					}
					else if (node.Is(out stringEquals))
					{
						formulaExpression = this.TranslateStringEquals(stringEquals);
					}
					else if (node.Is(out startsWithDigit))
					{
						formulaExpression = PowerAutomateProgramTranslator.TranslateStartsWithDigit(startsWithDigit);
					}
					else if (node.Is(out endsWithDigit))
					{
						formulaExpression = PowerAutomateProgramTranslator.TranslateEndsWithDigit(endsWithDigit);
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
						formulaExpression = PowerAutomateProgramTranslator.TranslateIsNumber(isNumber);
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
						formulaExpression = PowerAutomateProgramTranslator.TranslateNull();
					}
				}
				else
				{
					formulaExpression = this.TranslateVariableNode();
				}
				IL_05E2:
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

		// Token: 0x0600D193 RID: 53651 RVA: 0x002CACA4 File Offset: 0x002C8EA4
		protected override FormulaExpression Translate(CancellationToken cancellationToken = default(CancellationToken))
		{
			FormulaExpression formulaExpression = base.Translate(cancellationToken);
			if (!(formulaExpression == null))
			{
				return PowerAutomateExpressionOptimizer.Optimize(formulaExpression, this._options);
			}
			return null;
		}

		// Token: 0x0600D194 RID: 53652 RVA: 0x002CACD0 File Offset: 0x002C8ED0
		private static FormulaExpression TranslateAbs(Abs abs)
		{
			return PowerAutomateExpressionHelper.NumberLiteral((abs.absPos.Value > 0) ? (abs.absPos.Value - 1) : abs.absPos.Value);
		}

		// Token: 0x0600D195 RID: 53653 RVA: 0x002CAD18 File Offset: 0x002C8F18
		private FormulaExpression TranslateConcat(Concat concat)
		{
			FormulaExpression formulaExpression = this.Translate(concat.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(concat.Node.Children[1], default(CancellationToken));
			return PowerAutomateExpressionHelper.Concat(new FormulaExpression[] { formulaExpression, formulaExpression2 });
		}

		// Token: 0x0600D196 RID: 53654 RVA: 0x002CAD74 File Offset: 0x002C8F74
		private FormulaExpression TranslateConversionRule(ProgramNode node)
		{
			return this.Translate(node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D197 RID: 53655 RVA: 0x002CAD98 File Offset: 0x002C8F98
		private FormulaExpression TranslateDate(Date date)
		{
			return this.Translate(date.constDt.Node, default(CancellationToken));
		}

		// Token: 0x0600D198 RID: 53656 RVA: 0x002CADC4 File Offset: 0x002C8FC4
		private FormulaExpression TranslateFind(Find find)
		{
			FormulaExpression formulaExpression = this.Translate(find.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(find.findDelimiter.Node, default(CancellationToken));
			int value = find.findInstance.Value;
			int value2 = find.findOffset.Value;
			FormulaExpression formulaExpression3;
			if (value > 0)
			{
				if (value != 1)
				{
					formulaExpression3 = PowerAutomateExpressionHelper.NthIndexOf(formulaExpression, formulaExpression2, PowerAutomateExpressionHelper.NumberLiteral(value));
				}
				else
				{
					formulaExpression3 = PowerAutomateExpressionHelper.IndexOf(formulaExpression, formulaExpression2);
				}
			}
			else if (value < 0)
			{
				if (value != -1)
				{
					formulaExpression3 = PowerAutomateExpressionHelper.NthIndexOf(formulaExpression, formulaExpression2, PowerAutomateExpressionHelper.Add(PowerAutomateExpressionHelper.Div(PowerAutomateExpressionHelper.Sub(PowerAutomateExpressionHelper.Length(formulaExpression), PowerAutomateExpressionHelper.Length(PowerAutomateExpressionHelper.Replace(formulaExpression, formulaExpression2, PowerAutomateExpressionHelper.StringLiteral(string.Empty)))), PowerAutomateExpressionHelper.Length(formulaExpression2)), PowerAutomateExpressionHelper.NumberLiteral(value + 1)));
				}
				else
				{
					formulaExpression3 = PowerAutomateExpressionHelper.LastIndexOf(formulaExpression, formulaExpression2);
				}
			}
			else
			{
				formulaExpression3 = null;
			}
			FormulaExpression formulaExpression4 = formulaExpression3;
			if (formulaExpression4 == null)
			{
				return null;
			}
			if (value2 != 0)
			{
				return PowerAutomateExpressionHelper.Add(formulaExpression4, PowerAutomateExpressionHelper.NumberLiteral(value2));
			}
			return formulaExpression4;
		}

		// Token: 0x0600D199 RID: 53657 RVA: 0x002CAEDC File Offset: 0x002C90DC
		private FormulaExpression TranslateFormatNumber(FormatNumber formatNumber)
		{
			FormulaExpression formulaExpression = this.Translate(formatNumber.Node.Children[0], default(CancellationToken));
			FormatNumberDescriptor value = formatNumber.numberFormatDesc.Value;
			PowerAutomateFunc powerAutomateFunc = formulaExpression as PowerAutomateFunc;
			bool flag = powerAutomateFunc != null && powerAutomateFunc.Name == "FormatDateTime";
			if (flag)
			{
				string text = value.ToFormatString();
				bool flag2 = text == "0" || text == "0;-0";
				flag = flag2;
			}
			if (!flag)
			{
				return PowerAutomateExpressionHelper.FormatNumber(formulaExpression, PowerAutomateExpressionHelper.StringLiteral(value.ToFormatString()), PowerAutomateProgramTranslator.ResolveLocale(value.Locale));
			}
			return formulaExpression;
		}

		// Token: 0x0600D19A RID: 53658 RVA: 0x002CAF90 File Offset: 0x002C9190
		private FormulaExpression TranslateLength(Length length)
		{
			return PowerAutomateExpressionHelper.Length(this.Translate(length.fromStr.Node, default(CancellationToken)));
		}

		// Token: 0x0600D19B RID: 53659 RVA: 0x002CAFC0 File Offset: 0x002C91C0
		private FormulaExpression TranslateLetX(LetX letX)
		{
			ProgramNode programNode = letX.Node.Children[0].Children[0];
			this._currentInputColumnName = (string)((LiteralNode)programNode.Children[1]).Value;
			return this.Translate(letX.Node.Children[1], default(CancellationToken));
		}

		// Token: 0x0600D19C RID: 53660 RVA: 0x002CB020 File Offset: 0x002C9220
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
					formulaExpression = PowerAutomateExpressionHelper.NumberLiteral(num);
				}
				else if (value is double)
				{
					double num2 = (double)value;
					formulaExpression = PowerAutomateExpressionHelper.NumberLiteral(num2);
				}
				else if (value is decimal)
				{
					decimal num3 = (decimal)value;
					formulaExpression = PowerAutomateExpressionHelper.NumberLiteral(num3);
				}
				else if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					formulaExpression = PowerAutomateExpressionHelper.DateTimeLiteral(dateTime);
				}
				else
				{
					formulaExpression = null;
				}
			}
			else
			{
				formulaExpression = PowerAutomateExpressionHelper.StringLiteral(text);
			}
			return formulaExpression;
		}

		// Token: 0x0600D19D RID: 53661 RVA: 0x002CB0C8 File Offset: 0x002C92C8
		private FormulaExpression TranslateLowerCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerAutomateExpressionHelper.ToLower(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D19E RID: 53662 RVA: 0x002CB0FE File Offset: 0x002C92FE
		private static FormulaExpression TranslateNull()
		{
			return PowerAutomateExpressionHelper.StringLiteral(string.Empty);
		}

		// Token: 0x0600D19F RID: 53663 RVA: 0x002CB10C File Offset: 0x002C930C
		private static FormulaExpression TranslateArithmeticRightNumber(ProgramNode node)
		{
			decimal num;
			if (!node.IsArithmeticRightNumber(out num))
			{
				return null;
			}
			return PowerAutomateExpressionHelper.NumberLiteral(num);
		}

		// Token: 0x0600D1A0 RID: 53664 RVA: 0x002CB12C File Offset: 0x002C932C
		private FormulaExpression TranslateNumber(Number number)
		{
			return this.Translate(number.constNum.Node, default(CancellationToken));
		}

		// Token: 0x0600D1A1 RID: 53665 RVA: 0x002CB158 File Offset: 0x002C9358
		private FormulaExpression TranslateParseNumber(ParseNumber parseNumber)
		{
			return PowerAutomateExpressionHelper.Float(this.Translate(parseNumber.Node.Children[0], default(CancellationToken)));
		}

		// Token: 0x0600D1A2 RID: 53666 RVA: 0x002CB188 File Offset: 0x002C9388
		private FormulaExpression TranslateReplace(Replace replace)
		{
			return PowerAutomateExpressionHelper.Replace(this.Translate(replace.fromStr.Node, default(CancellationToken)), this.Translate(replace.replaceFindText.Node, default(CancellationToken)), this.Translate(replace.replaceText.Node, default(CancellationToken)));
		}

		// Token: 0x0600D1A3 RID: 53667 RVA: 0x002CB1F4 File Offset: 0x002C93F4
		private FormulaExpression TranslateSlice(Slice slice)
		{
			FormulaExpression formulaExpression = this.Translate(slice.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slice.pos1.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(slice.pos2.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.Slice(formulaExpression, formulaExpression2, formulaExpression3);
		}

		// Token: 0x0600D1A4 RID: 53668 RVA: 0x002CB268 File Offset: 0x002C9468
		private FormulaExpression TranslateSliceBetween(SliceBetween sliceBetween)
		{
			FormulaExpression formulaExpression = this.Translate(sliceBetween.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(sliceBetween.sliceBetweenStartText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(sliceBetween.sliceBetweenEndText.Node, default(CancellationToken));
			FormulaExpression formulaExpression4 = PowerAutomateExpressionHelper.Add(PowerAutomateExpressionHelper.IndexOf(formulaExpression, formulaExpression2), PowerAutomateExpressionHelper.Length(formulaExpression2));
			FormulaExpression formulaExpression5 = PowerAutomateExpressionHelper.Add(formulaExpression4, PowerAutomateExpressionHelper.IndexOf(PowerAutomateExpressionHelper.Slice(formulaExpression, formulaExpression4), formulaExpression3));
			return PowerAutomateExpressionHelper.Slice(formulaExpression, formulaExpression4, formulaExpression5);
		}

		// Token: 0x0600D1A5 RID: 53669 RVA: 0x002CB30C File Offset: 0x002C950C
		private FormulaExpression TranslateSlicePrefix(SlicePrefix slicePrefix)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slicePrefix.pos.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.Slice(formulaExpression, PowerAutomateExpressionHelper.NumberLiteral(0), formulaExpression2);
		}

		// Token: 0x0600D1A6 RID: 53670 RVA: 0x002CB364 File Offset: 0x002C9564
		private FormulaExpression TranslateSlicePrefixAbs(SlicePrefixAbs slicePrefix)
		{
			return PowerAutomateExpressionHelper.Slice(this.Translate(slicePrefix.x.Node, default(CancellationToken)), PowerAutomateExpressionHelper.NumberLiteral(0), PowerAutomateExpressionHelper.NumberLiteral(slicePrefix.slicePrefixAbsPos.Value - 1));
		}

		// Token: 0x0600D1A7 RID: 53671 RVA: 0x002CB3B0 File Offset: 0x002C95B0
		private FormulaExpression TranslateSliceSuffix(SliceSuffix sliceSuffix)
		{
			FormulaExpression formulaExpression = this.Translate(sliceSuffix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(sliceSuffix.pos.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.Slice(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D1A8 RID: 53672 RVA: 0x002CB400 File Offset: 0x002C9600
		private FormulaExpression TranslateSplit(Split split)
		{
			FormulaExpression formulaExpression = this.Translate(split.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(split.splitDelimiter.Node, default(CancellationToken));
			int value = split.splitInstance.Value;
			if (value == 0)
			{
				return null;
			}
			FormulaExpression formulaExpression3 = PowerAutomateExpressionHelper.Split(formulaExpression, formulaExpression2);
			FormulaExpression formulaExpression4;
			if (value < 0)
			{
				formulaExpression4 = PowerAutomateExpressionHelper.Index(formulaExpression3, PowerAutomateExpressionHelper.Sub(PowerAutomateExpressionHelper.Length(formulaExpression3), PowerAutomateExpressionHelper.NumberLiteral(-value)));
			}
			else
			{
				formulaExpression4 = PowerAutomateExpressionHelper.Index(formulaExpression3, PowerAutomateExpressionHelper.NumberLiteral(value - 1));
			}
			return formulaExpression4;
		}

		// Token: 0x0600D1A9 RID: 53673 RVA: 0x002CB4A0 File Offset: 0x002C96A0
		private FormulaExpression TranslateStr(Str str)
		{
			return this.Translate(str.constStr.Node, default(CancellationToken));
		}

		// Token: 0x0600D1AA RID: 53674 RVA: 0x002CB4CC File Offset: 0x002C96CC
		private FormulaExpression TranslateTrim(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerAutomateExpressionHelper.Trim(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D1AB RID: 53675 RVA: 0x002CB504 File Offset: 0x002C9704
		private FormulaExpression TranslateUpperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerAutomateExpressionHelper.ToUpper(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D1AC RID: 53676 RVA: 0x002CB53A File Offset: 0x002C973A
		private FormulaExpression TranslateVariableNode()
		{
			if (this._currentInputColumnName == null)
			{
				return null;
			}
			return PowerAutomateExpressionHelper.Variable(this._currentInputColumnName);
		}

		// Token: 0x0600D1AD RID: 53677 RVA: 0x002CB554 File Offset: 0x002C9754
		private static FormulaExpression TranslateFromDateTime(FromDateTime subject)
		{
			return PowerAutomateExpressionHelper.Variable(subject.columnName.Value);
		}

		// Token: 0x0600D1AE RID: 53678 RVA: 0x002CB578 File Offset: 0x002C9778
		private static FormulaExpression TranslateFromDateTimePart(FromDateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.Variable(dateTimePart.columnName.Value);
			DateTimePartKind value = dateTimePart.fromDateTimePartKind.Value;
			FormulaExpression formulaExpression2;
			if (value != DateTimePartKind.Month)
			{
				if (value == DateTimePartKind.Year)
				{
					formulaExpression2 = PowerAutomateExpressionHelper.DateTime(formulaExpression, PowerAutomateExpressionHelper.NumberLiteral(1), PowerAutomateExpressionHelper.NumberLiteral(1), null, null, null, null);
				}
				else
				{
					formulaExpression2 = null;
				}
			}
			else
			{
				formulaExpression2 = PowerAutomateExpressionHelper.DateTime(PowerAutomateExpressionHelper.NumberLiteral(2000), formulaExpression, PowerAutomateExpressionHelper.NumberLiteral(1), null, null, null, null);
			}
			return formulaExpression2;
		}

		// Token: 0x0600D1AF RID: 53679 RVA: 0x002CB5F0 File Offset: 0x002C97F0
		private static FormulaExpression TranslateFromNumber(FromNumber subject)
		{
			return PowerAutomateExpressionHelper.Variable(subject.columnName.Value);
		}

		// Token: 0x0600D1B0 RID: 53680 RVA: 0x002CB614 File Offset: 0x002C9814
		private static FormulaExpression TranslateFromStr(FromStr subject)
		{
			return PowerAutomateExpressionHelper.Variable(subject.columnName.Value);
		}

		// Token: 0x0600D1B1 RID: 53681 RVA: 0x002CB638 File Offset: 0x002C9838
		private FormulaExpression TranslateToDateTime(ToDateTime subject)
		{
			return PowerAutomateExpressionHelper.ParseDateTime(this.Translate(subject.Node.Children[0], default(CancellationToken)));
		}

		// Token: 0x0600D1B2 RID: 53682 RVA: 0x002CB668 File Offset: 0x002C9868
		private FormulaExpression TranslateToDecimal(ToDecimal subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D1B3 RID: 53683 RVA: 0x002CB694 File Offset: 0x002C9894
		private FormulaExpression TranslateToDouble(ToDouble subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D1B4 RID: 53684 RVA: 0x002CB6C0 File Offset: 0x002C98C0
		private FormulaExpression TranslateToInt(ToInt subject)
		{
			return PowerAutomateExpressionHelper.Int(this.Translate(subject.Node.Children[0], default(CancellationToken)));
		}

		// Token: 0x0600D1B5 RID: 53685 RVA: 0x002CB6F0 File Offset: 0x002C98F0
		private FormulaExpression TranslateToStr(ToStr subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D1B6 RID: 53686 RVA: 0x002CB71C File Offset: 0x002C991C
		private FormulaExpression TranslateDateTimePart(DateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = this.Translate(dateTimePart.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2;
			switch (dateTimePart.dateTimePartKind.Value)
			{
			case DateTimePartKind.Second:
				formulaExpression2 = PowerAutomateExpressionHelper.Second(formulaExpression, true);
				break;
			case DateTimePartKind.Minute:
				formulaExpression2 = PowerAutomateExpressionHelper.Minute(formulaExpression, true);
				break;
			case DateTimePartKind.Hour:
				formulaExpression2 = PowerAutomateExpressionHelper.Hour(formulaExpression, true);
				break;
			case DateTimePartKind.WeekDay:
				formulaExpression2 = PowerAutomateExpressionHelper.Add1(PowerAutomateExpressionHelper.DayOfWeek(formulaExpression));
				break;
			case DateTimePartKind.MonthDay:
				formulaExpression2 = PowerAutomateExpressionHelper.DayOfMonth(formulaExpression);
				break;
			case DateTimePartKind.MonthWeek:
				formulaExpression2 = PowerAutomateExpressionHelper.MonthWeek(formulaExpression);
				break;
			case DateTimePartKind.MonthDays:
				formulaExpression2 = PowerAutomateExpressionHelper.MonthDays(formulaExpression);
				break;
			case DateTimePartKind.Month:
				formulaExpression2 = PowerAutomateExpressionHelper.Month(formulaExpression, true);
				break;
			case DateTimePartKind.QuarterDay:
				formulaExpression2 = PowerAutomateExpressionHelper.QuarterDay(formulaExpression);
				break;
			case DateTimePartKind.QuarterWeek:
				formulaExpression2 = PowerAutomateExpressionHelper.QuarterWeek(formulaExpression);
				break;
			case DateTimePartKind.QuarterDays:
				formulaExpression2 = PowerAutomateExpressionHelper.QuarterDays(formulaExpression);
				break;
			case DateTimePartKind.Quarter:
				formulaExpression2 = PowerAutomateExpressionHelper.Quarter(formulaExpression);
				break;
			case DateTimePartKind.YearDay:
				formulaExpression2 = PowerAutomateExpressionHelper.DayOfYear(formulaExpression);
				break;
			case DateTimePartKind.YearWeek:
				formulaExpression2 = PowerAutomateExpressionHelper.YearWeek(formulaExpression);
				break;
			case DateTimePartKind.YearDays:
				formulaExpression2 = PowerAutomateExpressionHelper.YearDays(formulaExpression);
				break;
			case DateTimePartKind.Year:
				formulaExpression2 = PowerAutomateExpressionHelper.Year(formulaExpression);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			return formulaExpression2;
		}

		// Token: 0x0600D1B7 RID: 53687 RVA: 0x002CB85C File Offset: 0x002C9A5C
		private FormulaExpression TranslateFormatDateTime(FormatDateTime formatDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(formatDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = formatDateTime.dateTimeFormatDesc.Value;
			return PowerAutomateExpressionHelper.FormatDateTime(formulaExpression, PowerAutomateExpressionHelper.StringLiteral(value.Mask), PowerAutomateExpressionHelper.StringLiteral(value.Locale));
		}

		// Token: 0x0600D1B8 RID: 53688 RVA: 0x002CB8B4 File Offset: 0x002C9AB4
		private FormulaExpression TranslateParseDateTime(ParseDateTime parseDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(parseDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = parseDateTime.dateTimeParseDesc.Value;
			if (!(value.Mask == "yyyy-MM-ddTHH:mm:ss.fffffff"))
			{
				if (!value.IsPartial && !(value.Locale != "en-US"))
				{
					string locale = value.Locale;
					CultureInfo userInterfaceCulture = this._options.UserInterfaceCulture;
					if (!(locale != ((userInterfaceCulture != null) ? userInterfaceCulture.Name : null)))
					{
						return PowerAutomateExpressionHelper.ParseDateTime(formulaExpression);
					}
				}
				return PowerAutomateExpressionHelper.ParseDateTime(formulaExpression, PowerAutomateExpressionHelper.StringLiteral(value.Locale), PowerAutomateExpressionHelper.StringLiteral(value.Mask));
			}
			return formulaExpression;
		}

		// Token: 0x0600D1B9 RID: 53689 RVA: 0x002CB964 File Offset: 0x002C9B64
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

		// Token: 0x0600D1BA RID: 53690 RVA: 0x002CB9BC File Offset: 0x002C9BBC
		private FormulaExpression TranslateRoundDateTimeDown(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2;
			switch (roundDateTime.dateTimeRoundDesc.Value.Period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression2 = PowerAutomateExpressionHelper.SecondStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = PowerAutomateExpressionHelper.MinuteStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = PowerAutomateExpressionHelper.HourStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = PowerAutomateExpressionHelper.DayStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = PowerAutomateExpressionHelper.WeekStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = PowerAutomateExpressionHelper.MonthStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = PowerAutomateExpressionHelper.QuarterStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = PowerAutomateExpressionHelper.YearStart(formulaExpression);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			return formulaExpression2;
		}

		// Token: 0x0600D1BB RID: 53691 RVA: 0x002CBA7C File Offset: 0x002C9C7C
		private FormulaExpression TranslateRoundDateTimeNearest(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimePeriod period = roundDateTime.dateTimeRoundDesc.Value.Period;
			FormulaExpression formulaExpression2;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression2 = PowerAutomateExpressionHelper.SecondStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = PowerAutomateExpressionHelper.MinuteStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = PowerAutomateExpressionHelper.HourStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = PowerAutomateExpressionHelper.DayStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = PowerAutomateExpressionHelper.WeekStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = PowerAutomateExpressionHelper.MonthStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = PowerAutomateExpressionHelper.QuarterStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = PowerAutomateExpressionHelper.YearStart(formulaExpression);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			FormulaExpression formulaExpression3 = formulaExpression2;
			if (formulaExpression3 == null)
			{
				return null;
			}
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression2 = PowerAutomateExpressionHelper.SecondEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = PowerAutomateExpressionHelper.MinuteEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = PowerAutomateExpressionHelper.HourEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = PowerAutomateExpressionHelper.DayEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = PowerAutomateExpressionHelper.WeekEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = PowerAutomateExpressionHelper.MonthEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = PowerAutomateExpressionHelper.QuarterEnd(formulaExpression);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = PowerAutomateExpressionHelper.YearEnd(formulaExpression3);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			FormulaExpression formulaExpression4 = formulaExpression2;
			if (!(formulaExpression4 == null))
			{
				return PowerAutomateExpressionHelper.If(PowerAutomateExpressionHelper.Less(PowerAutomateExpressionHelper.Ticks(formulaExpression), PowerAutomateExpressionHelper.Midpoint(formulaExpression3, formulaExpression4)), formulaExpression3, formulaExpression4);
			}
			return null;
		}

		// Token: 0x0600D1BC RID: 53692 RVA: 0x002CBBEC File Offset: 0x002C9DEC
		private FormulaExpression TranslateRoundDateTimeUp(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimeDescriptor value = roundDateTime.dateTimeRoundDesc.Value;
			RoundDateTimePeriod period = value.Period;
			bool flag = value.Ceiling == RoundDatePeriodCeiling.LastDay;
			FormulaExpression formulaExpression2;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression2 = PowerAutomateExpressionHelper.SecondStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = PowerAutomateExpressionHelper.MinuteStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = PowerAutomateExpressionHelper.HourStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = PowerAutomateExpressionHelper.DayStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = PowerAutomateExpressionHelper.WeekStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = PowerAutomateExpressionHelper.MonthStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = PowerAutomateExpressionHelper.QuarterStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = PowerAutomateExpressionHelper.YearStart(formulaExpression);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			FormulaExpression formulaExpression3 = formulaExpression2;
			if (formulaExpression3 == null)
			{
				return null;
			}
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression2 = PowerAutomateExpressionHelper.SecondEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = PowerAutomateExpressionHelper.MinuteEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = PowerAutomateExpressionHelper.HourEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = PowerAutomateExpressionHelper.DayEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = PowerAutomateExpressionHelper.WeekEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = PowerAutomateExpressionHelper.MonthEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = PowerAutomateExpressionHelper.QuarterEnd(formulaExpression);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = PowerAutomateExpressionHelper.YearEnd(formulaExpression3);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			FormulaExpression formulaExpression4 = formulaExpression2;
			if (formulaExpression4 == null)
			{
				return null;
			}
			if (!flag)
			{
				return PowerAutomateExpressionHelper.If(PowerAutomateExpressionHelper.Equal(PowerAutomateExpressionHelper.Ticks(formulaExpression), PowerAutomateExpressionHelper.Ticks(formulaExpression3)), formulaExpression3, formulaExpression4);
			}
			return PowerAutomateExpressionHelper.AddToTimeDays(formulaExpression4, -1.0);
		}

		// Token: 0x0600D1BD RID: 53693 RVA: 0x002CBD7C File Offset: 0x002C9F7C
		private FormulaExpression TranslateContains(Contains contains)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.Variable(contains.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(contains.containsFindText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(contains.containsCount.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.Equal(PowerAutomateExpressionHelper.Sub(PowerAutomateExpressionHelper.Length(formulaExpression), PowerAutomateExpressionHelper.Length(PowerAutomateExpressionHelper.Replace(formulaExpression, formulaExpression2, PowerAutomateExpressionHelper.StringLiteral(string.Empty)))), formulaExpression3);
		}

		// Token: 0x0600D1BE RID: 53694 RVA: 0x002CBE08 File Offset: 0x002CA008
		private static FormulaExpression TranslateEndsWithDigit(EndsWithDigit endsWithDigit)
		{
			return PowerAutomateExpressionHelper.IsFloat(PowerAutomateExpressionHelper.Slice(PowerAutomateExpressionHelper.Variable(endsWithDigit.columnName.Value), PowerAutomateExpressionHelper.NumberLiteral(-1)));
		}

		// Token: 0x0600D1BF RID: 53695 RVA: 0x002CBE3C File Offset: 0x002CA03C
		private FormulaExpression TranslateIf(If ifNode)
		{
			FormulaExpression formulaExpression = this.Translate(ifNode.condition.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(ifNode.result1.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(ifNode.result2.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.If(formulaExpression, formulaExpression2, formulaExpression3);
		}

		// Token: 0x0600D1C0 RID: 53696 RVA: 0x002CBEB0 File Offset: 0x002CA0B0
		private static FormulaExpression TranslateIsBlank(IsBlank isBlank)
		{
			return PowerAutomateExpressionHelper.Empty(PowerAutomateExpressionHelper.String(PowerAutomateExpressionHelper.Variable(isBlank.columnName.Value)));
		}

		// Token: 0x0600D1C1 RID: 53697 RVA: 0x002CBEDC File Offset: 0x002CA0DC
		private static FormulaExpression TranslateIsNotBlank(IsNotBlank isNotBlank)
		{
			return PowerAutomateExpressionHelper.Not(PowerAutomateExpressionHelper.Empty(PowerAutomateExpressionHelper.String(PowerAutomateExpressionHelper.Variable(isNotBlank.columnName.Value))));
		}

		// Token: 0x0600D1C2 RID: 53698 RVA: 0x002CBF0C File Offset: 0x002CA10C
		private static FormulaExpression TranslateIsNumber(IsNumber isNumber)
		{
			return PowerAutomateExpressionHelper.IsFloat(PowerAutomateExpressionHelper.String(PowerAutomateExpressionHelper.Variable(isNumber.columnName.Value)));
		}

		// Token: 0x0600D1C3 RID: 53699 RVA: 0x002CBF38 File Offset: 0x002CA138
		private static FormulaExpression TranslateIsString(IsString isString)
		{
			return PowerAutomateExpressionHelper.Not(PowerAutomateExpressionHelper.IsFloat(PowerAutomateExpressionHelper.String(PowerAutomateExpressionHelper.Variable(isString.columnName.Value))));
		}

		// Token: 0x0600D1C4 RID: 53700 RVA: 0x002CBF68 File Offset: 0x002CA168
		private FormulaExpression TranslateNumberEquals(NumberEquals numberEquals)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.Variable(numberEquals.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(numberEquals.numberEqualsValue.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.Equal(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D1C5 RID: 53701 RVA: 0x002CBFB0 File Offset: 0x002CA1B0
		private FormulaExpression TranslateNumberGreaterThan(NumberGreaterThan numberGreaterThan)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.Variable(numberGreaterThan.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(numberGreaterThan.numberGreaterThanValue.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.Greater(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D1C6 RID: 53702 RVA: 0x002CBFF8 File Offset: 0x002CA1F8
		private FormulaExpression TranslateNumberLessThan(NumberLessThan numberLessThan)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.Variable(numberLessThan.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(numberLessThan.numberLessThanValue.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.Less(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D1C7 RID: 53703 RVA: 0x002CC040 File Offset: 0x002CA240
		private FormulaExpression TranslateOr(Or or)
		{
			FormulaExpression formulaExpression = this.Translate(or.condition1.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(or.condition2.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.Or(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D1C8 RID: 53704 RVA: 0x002CC090 File Offset: 0x002CA290
		private FormulaExpression TranslateStartsWith(StartsWith startsWith)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.Variable(startsWith.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(startsWith.startsWithFindText.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.StartsWith(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D1C9 RID: 53705 RVA: 0x002CC0D8 File Offset: 0x002CA2D8
		private static FormulaExpression TranslateStartsWithDigit(StartsWithDigit startsWithDigit)
		{
			return PowerAutomateExpressionHelper.IsFloat(PowerAutomateExpressionHelper.Slice(PowerAutomateExpressionHelper.Variable(startsWithDigit.columnName.Value), PowerAutomateExpressionHelper.NumberLiteral(0), PowerAutomateExpressionHelper.NumberLiteral(1)));
		}

		// Token: 0x0600D1CA RID: 53706 RVA: 0x002CC110 File Offset: 0x002CA310
		private FormulaExpression TranslateStringEquals(StringEquals stringEquals)
		{
			FormulaExpression formulaExpression = PowerAutomateExpressionHelper.Variable(stringEquals.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(stringEquals.equalsText.Node, default(CancellationToken));
			return PowerAutomateExpressionHelper.Equal(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D1CB RID: 53707 RVA: 0x002CC158 File Offset: 0x002CA358
		private FormulaExpression TranslateAdd(Add add)
		{
			return PowerAutomateExpressionHelper.Add(this.Translate(add.arithmeticLeft.Node, default(CancellationToken)), this.Translate(add.addRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D1CC RID: 53708 RVA: 0x002CC1A6 File Offset: 0x002CA3A6
		private static FormulaExpression TranslateAverage(Average average)
		{
			return PowerAutomateExpressionHelper.Average(PowerAutomateProgramTranslator.TranslateFromNumbers(average.fromNumbers));
		}

		// Token: 0x0600D1CD RID: 53709 RVA: 0x002CC1BC File Offset: 0x002CA3BC
		private FormulaExpression TranslateDivide(Divide divide)
		{
			return PowerAutomateExpressionHelper.Div(this.Translate(divide.arithmeticLeft.Node, default(CancellationToken)), PowerAutomateExpressionHelper.Float(this.Translate(divide.divideRight.Node, default(CancellationToken))));
		}

		// Token: 0x0600D1CE RID: 53710 RVA: 0x002CC210 File Offset: 0x002CA410
		private static IEnumerable<FormulaExpression> TranslateFromNumbers(fromNumbers fromNumbers)
		{
			LiteralNode literalNode = fromNumbers.Node.Children[1] as LiteralNode;
			string[] array = ((literalNode != null) ? literalNode.Value : null) as string[];
			if (array != null)
			{
				IEnumerable<string> enumerable = array;
				Func<string, FormulaExpression> func;
				if ((func = PowerAutomateProgramTranslator.<>O.<0>__Variable) == null)
				{
					func = (PowerAutomateProgramTranslator.<>O.<0>__Variable = new Func<string, FormulaExpression>(PowerAutomateExpressionHelper.Variable));
				}
				return enumerable.Select(func);
			}
			return null;
		}

		// Token: 0x0600D1CF RID: 53711 RVA: 0x002CC268 File Offset: 0x002CA468
		private FormulaExpression TranslateMultiply(Multiply multiply)
		{
			return PowerAutomateExpressionHelper.Mul(this.Translate(multiply.arithmeticLeft.Node, default(CancellationToken)), this.Translate(multiply.multiplyRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D1D0 RID: 53712 RVA: 0x002CC2B6 File Offset: 0x002CA4B6
		private static FormulaExpression TranslateProduct(Product product)
		{
			return PowerAutomateExpressionHelper.Product(PowerAutomateProgramTranslator.TranslateFromNumbers(product.fromNumbers));
		}

		// Token: 0x0600D1D1 RID: 53713 RVA: 0x002CC2CC File Offset: 0x002CA4CC
		private FormulaExpression TranslateSubtract(Subtract subtract)
		{
			return PowerAutomateExpressionHelper.Sub(this.Translate(subtract.arithmeticLeft.Node, default(CancellationToken)), this.Translate(subtract.subtractRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D1D2 RID: 53714 RVA: 0x002CC31A File Offset: 0x002CA51A
		private static FormulaExpression TranslateSum(Sum sum)
		{
			return PowerAutomateExpressionHelper.Sum(PowerAutomateProgramTranslator.TranslateFromNumbers(sum.fromNumbers));
		}

		// Token: 0x0600D1D3 RID: 53715 RVA: 0x002CC32D File Offset: 0x002CA52D
		private static FormulaExpression ResolveLocale(string locale)
		{
			if (!(locale == "en-US"))
			{
				return PowerAutomateExpressionHelper.StringLiteral(locale);
			}
			return null;
		}

		// Token: 0x04005123 RID: 20771
		private bool _cancelled;

		// Token: 0x04005124 RID: 20772
		private string _currentInputColumnName;

		// Token: 0x04005125 RID: 20773
		private readonly IPowerAutomateTranslationOptions _options;

		// Token: 0x02001913 RID: 6419
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005126 RID: 20774
			public static Func<string, FormulaExpression> <0>__Variable;
		}
	}
}

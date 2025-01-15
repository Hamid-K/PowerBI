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

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001934 RID: 6452
	internal class ExcelProgramTranslator : ProgramTranslatorBase
	{
		// Token: 0x0600D2EA RID: 53994 RVA: 0x002CE4CB File Offset: 0x002CC6CB
		private ExcelProgramTranslator(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, IExcelTranslationOptions options = null, ILogger logger = null)
			: base(program, examples, inputs, logger)
		{
			this._options = options ?? new ExcelTranslationConstraint();
		}

		// Token: 0x0600D2EB RID: 53995 RVA: 0x002CE4E9 File Offset: 0x002CC6E9
		public static FormulaExpression Translate(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, IExcelTranslationOptions options, ILogger logger = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new ExcelProgramTranslator(program, examples, inputs, options, logger).Translate(cancellationToken);
		}

		// Token: 0x0600D2EC RID: 53996 RVA: 0x002CE500 File Offset: 0x002CC700
		protected override FormulaExpression Translate(CancellationToken cancellationToken = default(CancellationToken))
		{
			FormulaExpression formulaExpression = base.Translate(cancellationToken);
			if (!(formulaExpression == null))
			{
				return ExcelExpressionOptimizer.Optimize(formulaExpression, this._options);
			}
			return null;
		}

		// Token: 0x0600D2ED RID: 53997 RVA: 0x002CE52C File Offset: 0x002CC72C
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
				FormatNumber formatNumber;
				RoundNumber roundNumber;
				ParseNumber parseNumber;
				FormatDateTime formatDateTime;
				ParseDateTime parseDateTime;
				DateTimePart dateTimePart;
				TimePart timePart;
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
				FromNumberStr fromNumberStr;
				FromDateTime fromDateTime;
				FromDateTimePart fromDateTimePart;
				FromTime fromTime;
				FromNumber fromNumber;
				FromNumberCoalesced fromNumberCoalesced;
				FromRowNumber fromRowNumber;
				ToStr toStr;
				ToInt toInt;
				ToDouble toDouble;
				ToDecimal toDecimal;
				ToDateTime toDateTime;
				Match match;
				MatchEnd matchEnd;
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
					formulaExpression = ExcelProgramTranslator.TranslateAbs(abs);
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
					formulaExpression = ExcelProgramTranslator.TranslateArithmeticRightNumber(node);
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
				else if (node.Is(out timePart))
				{
					formulaExpression = this.TranslateTimePart(timePart);
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
					formulaExpression = ExcelProgramTranslator.TranslateSum(sum);
				}
				else if (node.Is(out product))
				{
					formulaExpression = ExcelProgramTranslator.TranslateProduct(product);
				}
				else if (node.Is(out average))
				{
					formulaExpression = ExcelProgramTranslator.TranslateAverage(average);
				}
				else if (node.Is(out fromStr))
				{
					formulaExpression = ExcelProgramTranslator.TranslateFromStr(fromStr);
				}
				else if (node.Is(out fromNumberStr))
				{
					formulaExpression = ExcelProgramTranslator.TranslateFromNumberStr(fromNumberStr);
				}
				else if (node.Is(out fromDateTime))
				{
					formulaExpression = ExcelProgramTranslator.TranslateFromDateTime(fromDateTime);
				}
				else if (node.Is(out fromDateTimePart))
				{
					formulaExpression = ExcelProgramTranslator.TranslateFromDateTimePart(fromDateTimePart);
				}
				else if (node.Is(out fromTime))
				{
					formulaExpression = ExcelProgramTranslator.TranslateFromTime(fromTime);
				}
				else if (node.Is(out fromNumber))
				{
					formulaExpression = ExcelProgramTranslator.TranslateFromNumber(fromNumber);
				}
				else if (node.Is(out fromNumberCoalesced))
				{
					formulaExpression = ExcelProgramTranslator.TranslateFromNumberCoalesced(fromNumberCoalesced);
				}
				else if (node.Is(out fromRowNumber))
				{
					formulaExpression = this.TranslateFromRowNumber();
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
				else if (node.Is(out match))
				{
					formulaExpression = this.TranslateMatch(match);
				}
				else if (node.Is(out matchEnd))
				{
					formulaExpression = this.TranslateMatchEnd(matchEnd);
				}
				else if (!(node is VariableNode))
				{
					if (node != null)
					{
						if (node.GrammarRule is ConversionRule)
						{
							formulaExpression = this.TranslateConversionRule(node);
							goto IL_06BC;
						}
						LiteralNode literalNode = node as LiteralNode;
						if (literalNode != null)
						{
							formulaExpression = ExcelProgramTranslator.TranslateLiteral(literalNode);
							goto IL_06BC;
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
						formulaExpression = ExcelProgramTranslator.TranslateIsString(isString);
					}
					else if (node.Is(out isBlank))
					{
						formulaExpression = ExcelProgramTranslator.TranslateIsBlank(isBlank);
					}
					else if (node.Is(out isNotBlank))
					{
						formulaExpression = ExcelProgramTranslator.TranslateIsNotBlank(isNotBlank);
					}
					else if (node.Is(out stringEquals))
					{
						formulaExpression = this.TranslateStringEquals(stringEquals);
					}
					else if (node.Is(out startsWithDigit))
					{
						formulaExpression = ExcelProgramTranslator.TranslateStartsWithDigit(startsWithDigit);
					}
					else if (node.Is(out endsWithDigit))
					{
						formulaExpression = ExcelProgramTranslator.TranslateEndsWithDigit(endsWithDigit);
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
						formulaExpression = ExcelProgramTranslator.TranslateIsNumber(isNumber);
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
					else if (node.Is<Null>())
					{
						formulaExpression = ExcelProgramTranslator.TranslateNull();
					}
					else
					{
						RowNumberLinearTransform rowNumberLinearTransform;
						if (!node.Is(out rowNumberLinearTransform))
						{
							throw new FormulaTranslationNotFoundException(string.Format("Invalid Rule: {0}", node.GrammarRule));
						}
						formulaExpression = this.TranslateTransformRowNumberLinear(rowNumberLinearTransform);
					}
				}
				else
				{
					formulaExpression = this.TranslateVariableNode();
				}
				IL_06BC:
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
						throw new FormulaTranslationNotFoundException(string.Format("No Translation for: {0}", node));
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

		// Token: 0x0600D2EE RID: 53998 RVA: 0x002CEC64 File Offset: 0x002CCE64
		private static FormulaExpression TranslateAbs(Abs abs)
		{
			return ExcelExpressionHelper.NumberLiteral(abs.absPos.Value);
		}

		// Token: 0x0600D2EF RID: 53999 RVA: 0x002CEC88 File Offset: 0x002CCE88
		private FormulaExpression TranslateConcat(Concat concat)
		{
			FormulaExpression formulaExpression = this.Translate(concat.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(concat.Node.Children[1], default(CancellationToken));
			return ExcelExpressionHelper.Concat(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D2F0 RID: 54000 RVA: 0x002CECD8 File Offset: 0x002CCED8
		private FormulaExpression TranslateConversionRule(ProgramNode node)
		{
			return this.Translate(node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D2F1 RID: 54001 RVA: 0x002CECFC File Offset: 0x002CCEFC
		private FormulaExpression TranslateDate(Date date)
		{
			return this.Translate(date.constDt.Node, default(CancellationToken));
		}

		// Token: 0x0600D2F2 RID: 54002 RVA: 0x002CED28 File Offset: 0x002CCF28
		private FormulaExpression TranslateFind(Find find)
		{
			FormulaExpression formulaExpression = this.Translate(find.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(find.findDelimiter.Node, default(CancellationToken));
			int value = find.findInstance.Value;
			int value2 = find.findOffset.Value;
			if (value == 0)
			{
				return null;
			}
			FormulaExpression formulaExpression3 = (this._options.EnableFindN ? ExcelExpressionHelper.FindN(formulaExpression2, formulaExpression, ExcelExpressionHelper.NumberLiteral(value)) : ExcelExpressionHelper.FindInstance(formulaExpression2, formulaExpression, ExcelExpressionHelper.NumberLiteral(value)));
			if (formulaExpression3 == null)
			{
				return null;
			}
			FormulaExpression formulaExpression4;
			if (value2 <= 0)
			{
				if (value2 >= 0)
				{
					formulaExpression4 = formulaExpression3;
				}
				else
				{
					formulaExpression4 = ExcelExpressionHelper.Minus(formulaExpression3, ExcelExpressionHelper.NumberLiteral(-value2));
				}
			}
			else
			{
				formulaExpression4 = ExcelExpressionHelper.Plus(formulaExpression3, ExcelExpressionHelper.NumberLiteral(value2));
			}
			return formulaExpression4;
		}

		// Token: 0x0600D2F3 RID: 54003 RVA: 0x002CEE08 File Offset: 0x002CD008
		private FormulaExpression TranslateFormatDateTime(FormatDateTime formatDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(formatDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = formatDateTime.dateTimeFormatDesc.Value;
			string text = value.Mask.Replace("'", "").Replace("%d", "d").Replace("MMMM", "mmmm")
				.Replace("MMM", "mmm")
				.Replace("MM", "mm")
				.Replace("%M", "m")
				.Replace("M", "m")
				.Replace("tt", "AM/PM");
			return ExcelExpressionHelper.Text(formulaExpression, ExcelExpressionHelper.Format(text, value.Locale));
		}

		// Token: 0x0600D2F4 RID: 54004 RVA: 0x002CEED4 File Offset: 0x002CD0D4
		private FormulaExpression TranslateFormatNumber(FormatNumber formatNumber)
		{
			FormulaExpression formulaExpression = this.Translate(formatNumber.Node.Children[0], default(CancellationToken));
			FormatNumberDescriptor value = formatNumber.numberFormatDesc.Value;
			return ExcelExpressionHelper.Text(formulaExpression, ExcelExpressionHelper.Format(value.ToFormatString(), value.Locale));
		}

		// Token: 0x0600D2F5 RID: 54005 RVA: 0x002CEF24 File Offset: 0x002CD124
		private FormulaExpression TranslateLength(Length length)
		{
			return ExcelExpressionHelper.Len(this.Translate(length.fromStr.Node, default(CancellationToken)));
		}

		// Token: 0x0600D2F6 RID: 54006 RVA: 0x002CEF54 File Offset: 0x002CD154
		private FormulaExpression TranslateLetX(LetX letX)
		{
			this._currentInputNode = letX.fromStrTrim.Node;
			this._currentInputExpression = this.Translate(this._currentInputNode, default(CancellationToken));
			return this.Translate(letX.substring.Node, default(CancellationToken));
		}

		// Token: 0x0600D2F7 RID: 54007 RVA: 0x002CEFB0 File Offset: 0x002CD1B0
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
					formulaExpression = ExcelExpressionHelper.NumberLiteral(num);
				}
				else if (value is double)
				{
					double num2 = (double)value;
					formulaExpression = ExcelExpressionHelper.NumberLiteral(num2);
				}
				else if (value is decimal)
				{
					decimal num3 = (decimal)value;
					formulaExpression = ExcelExpressionHelper.NumberLiteral(num3);
				}
				else if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					formulaExpression = ExcelExpressionHelper.DateTimeLiteral(dateTime);
				}
				else
				{
					formulaExpression = null;
				}
			}
			else
			{
				formulaExpression = ExcelExpressionHelper.StringLiteral(text);
			}
			return formulaExpression;
		}

		// Token: 0x0600D2F8 RID: 54008 RVA: 0x002CF058 File Offset: 0x002CD258
		private FormulaExpression TranslateLowerCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return ExcelExpressionHelper.Lower(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D2F9 RID: 54009 RVA: 0x002CF090 File Offset: 0x002CD290
		private static FormulaExpression TranslateArithmeticRightNumber(ProgramNode node)
		{
			decimal num;
			if (!node.IsArithmeticRightNumber(out num))
			{
				return null;
			}
			return ExcelExpressionHelper.NumberLiteral(num);
		}

		// Token: 0x0600D2FA RID: 54010 RVA: 0x002CF0B0 File Offset: 0x002CD2B0
		private FormulaExpression TranslateNumber(Number number)
		{
			return this.Translate(number.constNum.Node, default(CancellationToken));
		}

		// Token: 0x0600D2FB RID: 54011 RVA: 0x002CF0DC File Offset: 0x002CD2DC
		private FormulaExpression TranslateParseDateTime(ParseDateTime parseDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(parseDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = parseDateTime.dateTimeParseDesc.Value;
			if (this._options.UserInterfaceCulture != null && !(this._options.UserInterfaceCulture.Name == value.Culture.Name))
			{
				return null;
			}
			return ExcelExpressionHelper.Plus(ExcelExpressionHelper.DateValue(formulaExpression), ExcelExpressionHelper.TimeValue(formulaExpression));
		}

		// Token: 0x0600D2FC RID: 54012 RVA: 0x002CF15C File Offset: 0x002CD35C
		private FormulaExpression TranslateParseNumber(ParseNumber parseNumber)
		{
			FormulaExpression formulaExpression = this.Translate(parseNumber.Node.Children[0], default(CancellationToken));
			string value = parseNumber.locale.Value;
			if (this._options.UserInterfaceCulture == null || this._options.UserInterfaceCulture.Name == value)
			{
				return ExcelExpressionHelper.Value(formulaExpression);
			}
			NumberFormatInfo numberFormat = new CultureInfo(value).NumberFormat;
			string numberDecimalSeparator = numberFormat.NumberDecimalSeparator;
			string numberGroupSeparator = numberFormat.NumberGroupSeparator;
			return ExcelExpressionHelper.NumberValue(formulaExpression, ExcelExpressionHelper.StringLiteral(numberDecimalSeparator), ExcelExpressionHelper.StringLiteral(numberGroupSeparator));
		}

		// Token: 0x0600D2FD RID: 54013 RVA: 0x002CF1F0 File Offset: 0x002CD3F0
		private FormulaExpression TranslateProperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return ExcelExpressionHelper.Proper(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D2FE RID: 54014 RVA: 0x002CF228 File Offset: 0x002CD428
		private FormulaExpression TranslateReplace(Replace replace)
		{
			return ExcelExpressionHelper.Substitute(this.Translate(replace.fromStr.Node, default(CancellationToken)), this.Translate(replace.replaceFindText.Node, default(CancellationToken)), this.Translate(replace.replaceText.Node, default(CancellationToken)), null);
		}

		// Token: 0x0600D2FF RID: 54015 RVA: 0x002CF298 File Offset: 0x002CD498
		private FormulaExpression TranslateRoundNumber(RoundNumber roundNumber)
		{
			FormulaExpression formulaExpression = this.Translate(roundNumber.inumber.Node, default(CancellationToken));
			if (formulaExpression == null)
			{
				return null;
			}
			RoundNumberDescriptor value = roundNumber.numberRoundDesc.Value;
			RoundingMode mode = value.Mode;
			double delta = value.Delta;
			double num = Math.Log10(delta);
			double num2 = num % 1.0;
			FormulaExpression formulaExpression2;
			if (num2 >= -0.0001 && num2 <= 0.0001)
			{
				int num3 = Convert.ToInt32(num);
				switch (mode)
				{
				case RoundingMode.Nearest:
					formulaExpression2 = ExcelExpressionHelper.Round(formulaExpression, ExcelExpressionHelper.NumberLiteral(-num3));
					break;
				case RoundingMode.Down:
					formulaExpression2 = ExcelExpressionHelper.RoundDown(formulaExpression, ExcelExpressionHelper.NumberLiteral(-num3));
					break;
				case RoundingMode.Up:
					formulaExpression2 = ExcelExpressionHelper.RoundUp(formulaExpression, ExcelExpressionHelper.NumberLiteral(-num3));
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
			switch (mode)
			{
			case RoundingMode.Nearest:
				formulaExpression2 = ExcelExpressionHelper.MRound(formulaExpression, delta);
				break;
			case RoundingMode.Down:
				formulaExpression2 = ExcelExpressionHelper.Multiply(ExcelExpressionHelper.RoundDown(ExcelExpressionHelper.Divide(formulaExpression, ExcelExpressionHelper.NumberLiteral(delta)), ExcelExpressionHelper.NumberLiteral(0)), ExcelExpressionHelper.NumberLiteral(delta));
				break;
			case RoundingMode.Up:
				formulaExpression2 = ExcelExpressionHelper.Multiply(ExcelExpressionHelper.RoundUp(ExcelExpressionHelper.Divide(formulaExpression, ExcelExpressionHelper.NumberLiteral(delta)), ExcelExpressionHelper.NumberLiteral(0)), ExcelExpressionHelper.NumberLiteral(delta));
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

		// Token: 0x0600D300 RID: 54016 RVA: 0x002CF42A File Offset: 0x002CD62A
		private FormulaExpression TranslateSlice(Slice slice)
		{
			if (!this._options.EnableTextSlice)
			{
				return this.TranslateSliceToMid(slice);
			}
			return this.TranslateSliceToTextSlice(slice);
		}

		// Token: 0x0600D301 RID: 54017 RVA: 0x002CF448 File Offset: 0x002CD648
		private FormulaExpression TranslateSliceBetween(SliceBetween sliceBetween)
		{
			FormulaExpression formulaExpression = this.Translate(sliceBetween.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(sliceBetween.sliceBetweenStartText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(sliceBetween.sliceBetweenEndText.Node, default(CancellationToken));
			FormulaExpression formulaExpression4 = ExcelExpressionHelper.Plus(ExcelExpressionHelper.Find(formulaExpression2, formulaExpression), ExcelExpressionHelper.Len(formulaExpression2));
			FormulaExpression formulaExpression5 = ExcelExpressionHelper.Find(formulaExpression3, formulaExpression, formulaExpression4);
			if (!this._options.EnableTextSlice)
			{
				return ExcelExpressionHelper.Mid(formulaExpression, formulaExpression4, ExcelExpressionHelper.Minus(formulaExpression5, formulaExpression4));
			}
			return ExcelExpressionHelper.TextSlice(formulaExpression, formulaExpression4, ExcelExpressionHelper.Minus1(formulaExpression5));
		}

		// Token: 0x0600D302 RID: 54018 RVA: 0x002CF4FC File Offset: 0x002CD6FC
		private FormulaExpression TranslateSlicePrefix(SlicePrefix slicePrefix)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefix.x.Node, default(CancellationToken));
			Find find;
			if (this._options.EnableTextBefore && slicePrefix.pos.Node.Is(out find))
			{
				LiteralNode literalNode = find.findDelimiter.Node as LiteralNode;
				if (literalNode != null && literalNode.Value is string)
				{
					literalNode = find.findOffset.Node as LiteralNode;
					if (literalNode != null)
					{
						object value = literalNode.Value;
						if (value is int && (int)value == 0)
						{
							return ExcelExpressionHelper.TextBefore(formulaExpression, this.Translate(find.findDelimiter.Node, default(CancellationToken)), this.Translate(find.findInstance.Node, default(CancellationToken)));
						}
					}
				}
			}
			FormulaExpression formulaExpression2 = this.Translate(slicePrefix.pos.Node, default(CancellationToken));
			FormulaNumberLiteral formulaNumberLiteral = formulaExpression2 as FormulaNumberLiteral;
			int? num = ((formulaNumberLiteral != null) ? new int?((int)formulaNumberLiteral.Value) : null);
			int num2 = 0;
			if (!((num.GetValueOrDefault() < num2) & (num != null)))
			{
				return ExcelExpressionHelper.Left(formulaExpression, ExcelExpressionHelper.Minus1(formulaExpression2));
			}
			return ExcelExpressionHelper.Left(formulaExpression, ExcelExpressionHelper.Minus(ExcelExpressionHelper.Len(formulaExpression), ExcelExpressionHelper.Minus1(formulaExpression2)));
		}

		// Token: 0x0600D303 RID: 54019 RVA: 0x002CF680 File Offset: 0x002CD880
		private FormulaExpression TranslateSlicePrefixAbs(SlicePrefixAbs slicePrefixAbs)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefixAbs.x.Node, default(CancellationToken));
			int value = slicePrefixAbs.slicePrefixAbsPos.Value;
			FormulaExpression formulaExpression2 = ExcelExpressionHelper.NumberLiteral(value);
			if (value >= 0)
			{
				return ExcelExpressionHelper.Left(formulaExpression, ExcelExpressionHelper.Minus1(formulaExpression2));
			}
			return ExcelExpressionHelper.Left(formulaExpression, ExcelExpressionHelper.Minus(ExcelExpressionHelper.Len(formulaExpression), ExcelExpressionHelper.Minus1(formulaExpression2)));
		}

		// Token: 0x0600D304 RID: 54020 RVA: 0x002CF6EC File Offset: 0x002CD8EC
		private FormulaExpression TranslateSliceSuffix(SliceSuffix sliceSuffix)
		{
			FormulaExpression formulaExpression = this.Translate(sliceSuffix.x.Node, default(CancellationToken));
			Find find;
			if (this._options.EnableTextAfter && sliceSuffix.pos.Node.Is(out find))
			{
				LiteralNode literalNode = find.findDelimiter.Node as LiteralNode;
				if (literalNode != null)
				{
					string text = literalNode.Value as string;
					if (text != null)
					{
						literalNode = find.findOffset.Node as LiteralNode;
						if (literalNode != null)
						{
							object value = literalNode.Value;
							if (value is int)
							{
								int num = (int)value;
								if (num == text.Length)
								{
									return ExcelExpressionHelper.TextAfter(formulaExpression, this.Translate(find.findDelimiter.Node, default(CancellationToken)), this.Translate(find.findInstance.Node, default(CancellationToken)));
								}
							}
						}
					}
				}
			}
			FormulaExpression formulaExpression2 = this.Translate(sliceSuffix.pos.Node, default(CancellationToken));
			FormulaNumberLiteral formulaNumberLiteral = formulaExpression2 as FormulaNumberLiteral;
			int? num2 = ((formulaNumberLiteral != null) ? new int?((int)formulaNumberLiteral.Value) : null);
			int? num3 = num2;
			int num4 = 0;
			if (!((num3.GetValueOrDefault() < num4) & (num3 != null)))
			{
				return ExcelExpressionHelper.Right(formulaExpression, ExcelExpressionHelper.Minus(ExcelExpressionHelper.Len(formulaExpression), ExcelExpressionHelper.Minus1(formulaExpression2)));
			}
			return ExcelExpressionHelper.Right(formulaExpression, ExcelExpressionHelper.NumberLiteral(-num2.Value));
		}

		// Token: 0x0600D305 RID: 54021 RVA: 0x002CF890 File Offset: 0x002CDA90
		private FormulaExpression TranslateSliceToMid(Slice slice)
		{
			FormulaExpression formulaExpression = this.Translate(slice.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slice.pos1.Node, default(CancellationToken));
			Find find;
			Find find2;
			if (slice.pos1.Node.Is(out find) && slice.pos2.Node.Is(out find2) && find.findDelimiter.Value == find2.findDelimiter.Value && find.findInstance.Value == find2.findInstance.Value && find.findOffset.Value < find2.findOffset.Value)
			{
				return ExcelExpressionHelper.Mid(formulaExpression, formulaExpression2, ExcelExpressionHelper.NumberLiteral(find2.findOffset.Value - find.findOffset.Value));
			}
			FormulaExpression formulaExpression3 = this.Translate(slice.pos2.Node, default(CancellationToken));
			FormulaNumberLiteral formulaNumberLiteral = formulaExpression2 as FormulaNumberLiteral;
			int? num = ((formulaNumberLiteral != null) ? new int?((int)formulaNumberLiteral.Value) : null);
			FormulaNumberLiteral formulaNumberLiteral2 = formulaExpression3 as FormulaNumberLiteral;
			int? num2 = ((formulaNumberLiteral2 != null) ? new int?((int)formulaNumberLiteral2.Value) : null);
			if (num != null && num.GetValueOrDefault() < 0)
			{
				formulaExpression2 = ExcelExpressionHelper.Minus(ExcelExpressionHelper.Len(formulaExpression), ExcelExpressionHelper.NumberLiteral(-num.Value - 1));
			}
			if (num2 != null && num2.GetValueOrDefault() < 0)
			{
				formulaExpression3 = ExcelExpressionHelper.Minus(ExcelExpressionHelper.Len(formulaExpression), ExcelExpressionHelper.NumberLiteral(-num2.Value - 1));
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
								formulaExpression4 = ExcelExpressionHelper.Minus(ExcelExpressionHelper.NumberLiteral(valueOrDefault2), ExcelExpressionHelper.Plus(ExcelExpressionHelper.NumberLiteral(valueOrDefault), ExcelExpressionHelper.Len(formulaExpression)));
								goto IL_0295;
							}
							if (valueOrDefault2 < 0)
							{
								formulaExpression4 = ExcelExpressionHelper.NumberLiteral(Math.Abs(valueOrDefault - valueOrDefault2));
								goto IL_0295;
							}
						}
					}
					else
					{
						if (valueOrDefault2 > 0)
						{
							formulaExpression4 = ExcelExpressionHelper.NumberLiteral(valueOrDefault2 - valueOrDefault);
							goto IL_0295;
						}
						if (valueOrDefault2 < 0)
						{
							formulaExpression4 = ExcelExpressionHelper.Minus(ExcelExpressionHelper.Len(formulaExpression), ExcelExpressionHelper.NumberLiteral(Math.Abs(valueOrDefault2 + 1) + valueOrDefault));
							goto IL_0295;
						}
					}
					formulaExpression4 = null;
					IL_0295:
					FormulaExpression formulaExpression5 = formulaExpression4;
					if (!(formulaExpression5 == null))
					{
						return ExcelExpressionHelper.Mid(formulaExpression, formulaExpression2, formulaExpression5);
					}
					return null;
				}
			}
			return ExcelExpressionHelper.Mid(ExcelExpressionHelper.Left(formulaExpression, ExcelExpressionHelper.Minus1(formulaExpression3)), formulaExpression2, ExcelExpressionHelper.Len(formulaExpression));
		}

		// Token: 0x0600D306 RID: 54022 RVA: 0x002CFB68 File Offset: 0x002CDD68
		private FormulaExpression TranslateSliceToTextSlice(Slice slice)
		{
			FormulaExpression formulaExpression = this.Translate(slice.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slice.pos1.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(slice.pos2.Node, default(CancellationToken));
			return ExcelExpressionHelper.TextSlice(formulaExpression, formulaExpression2, ExcelExpressionHelper.Minus1(formulaExpression3));
		}

		// Token: 0x0600D307 RID: 54023 RVA: 0x002CFBE0 File Offset: 0x002CDDE0
		private FormulaExpression TranslateSplit(Split split)
		{
			FormulaExpression formulaExpression = this.Translate(split.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(split.splitDelimiter.Node, default(CancellationToken));
			int value = split.splitInstance.Value;
			if (value == 0)
			{
				return null;
			}
			bool flag = value == -1 || value == 1;
			if (flag && (this._options.EnableTextBefore || this._options.EnableTextAfter))
			{
				bool flag2 = base.AllInputs.All(delegate(IRow input)
				{
					string text = this._currentInputNode.Run(input) as string;
					string text2 = split.splitDelimiter.Node.Run(input) as string;
					return !string.IsNullOrEmpty(text2) && text != null && text.Contains(text2);
				});
				if (flag2 && this._options.EnableTextBefore && value == 1)
				{
					return ExcelExpressionHelper.TextBefore(formulaExpression, formulaExpression2, ExcelExpressionHelper.NumberLiteral(1));
				}
				if (flag2 && this._options.EnableTextAfter && value == -1)
				{
					return ExcelExpressionHelper.TextAfter(formulaExpression, formulaExpression2, ExcelExpressionHelper.NumberLiteral(-1));
				}
			}
			FormulaExpression formulaExpression3 = ExcelExpressionHelper.TextSplit(formulaExpression, formulaExpression2);
			FormulaExpression formulaExpression4;
			if (value < 0)
			{
				formulaExpression4 = ExcelExpressionHelper.Index(formulaExpression3, ExcelExpressionHelper.Minus(ExcelExpressionHelper.Columns(formulaExpression3), ExcelExpressionHelper.NumberLiteral(-value - 1)));
			}
			else
			{
				formulaExpression4 = ExcelExpressionHelper.Index(formulaExpression3, ExcelExpressionHelper.NumberLiteral(value));
			}
			return formulaExpression4;
		}

		// Token: 0x0600D308 RID: 54024 RVA: 0x002CFD30 File Offset: 0x002CDF30
		private FormulaExpression TranslateStr(Str str)
		{
			return this.Translate(str.constStr.Node, default(CancellationToken));
		}

		// Token: 0x0600D309 RID: 54025 RVA: 0x002CFD5C File Offset: 0x002CDF5C
		private FormulaExpression TranslateTrim(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return ExcelExpressionHelper.Trim(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D30A RID: 54026 RVA: 0x002CFD94 File Offset: 0x002CDF94
		private FormulaExpression TranslateUpperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return ExcelExpressionHelper.Upper(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D30B RID: 54027 RVA: 0x002CFDCA File Offset: 0x002CDFCA
		private FormulaExpression TranslateVariableNode()
		{
			return this._currentInputExpression;
		}

		// Token: 0x0600D30C RID: 54028 RVA: 0x002CFDD4 File Offset: 0x002CDFD4
		private FormulaExpression TranslateDateTimePart(DateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = this.Translate(dateTimePart.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2;
			switch (dateTimePart.dateTimePartKind.Value)
			{
			case DateTimePartKind.Second:
				formulaExpression2 = ExcelExpressionHelper.Second(formulaExpression);
				break;
			case DateTimePartKind.Minute:
				formulaExpression2 = ExcelExpressionHelper.Minute(formulaExpression);
				break;
			case DateTimePartKind.Hour:
				formulaExpression2 = ExcelExpressionHelper.Hour(formulaExpression);
				break;
			case DateTimePartKind.WeekDay:
				formulaExpression2 = ExcelExpressionHelper.Weekday(formulaExpression);
				break;
			case DateTimePartKind.MonthDay:
				formulaExpression2 = ExcelExpressionHelper.Day(formulaExpression);
				break;
			case DateTimePartKind.MonthWeek:
				formulaExpression2 = ExcelExpressionHelper.MonthWeek(formulaExpression, null);
				break;
			case DateTimePartKind.MonthDays:
				formulaExpression2 = ExcelExpressionHelper.MonthDays(formulaExpression);
				break;
			case DateTimePartKind.Month:
				formulaExpression2 = ExcelExpressionHelper.Month(formulaExpression);
				break;
			case DateTimePartKind.QuarterDay:
				formulaExpression2 = ExcelExpressionHelper.QuarterDay(formulaExpression, null, null);
				break;
			case DateTimePartKind.QuarterWeek:
				formulaExpression2 = ExcelExpressionHelper.QuarterWeek(formulaExpression, null, null);
				break;
			case DateTimePartKind.QuarterDays:
				formulaExpression2 = ExcelExpressionHelper.QuarterDays(formulaExpression, null, null, null);
				break;
			case DateTimePartKind.Quarter:
				formulaExpression2 = ExcelExpressionHelper.Quarter(formulaExpression);
				break;
			case DateTimePartKind.YearDay:
				formulaExpression2 = ExcelExpressionHelper.YearDay(formulaExpression);
				break;
			case DateTimePartKind.YearWeek:
				formulaExpression2 = ExcelExpressionHelper.YearWeek(formulaExpression);
				break;
			case DateTimePartKind.YearDays:
				formulaExpression2 = ExcelExpressionHelper.YearDays(formulaExpression, null, null);
				break;
			case DateTimePartKind.Year:
				formulaExpression2 = ExcelExpressionHelper.Year(formulaExpression);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			return formulaExpression2;
		}

		// Token: 0x0600D30D RID: 54029 RVA: 0x002CFF18 File Offset: 0x002CE118
		private static FormulaExpression TranslateFromDateTime(FromDateTime dateTime)
		{
			return ExcelExpressionHelper.Variable(dateTime.columnName.Value);
		}

		// Token: 0x0600D30E RID: 54030 RVA: 0x002CFF3C File Offset: 0x002CE13C
		private static FormulaExpression TranslateFromDateTimePart(FromDateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = ExcelExpressionHelper.Variable(dateTimePart.columnName.Value);
			DateTimePartKind value = dateTimePart.fromDateTimePartKind.Value;
			FormulaExpression formulaExpression2;
			if (value != DateTimePartKind.Month)
			{
				if (value == DateTimePartKind.Year)
				{
					formulaExpression2 = ExcelExpressionHelper.Date(formulaExpression, 1, 1);
				}
				else
				{
					formulaExpression2 = null;
				}
			}
			else
			{
				formulaExpression2 = ExcelExpressionHelper.Date(2000, formulaExpression, 1);
			}
			return formulaExpression2;
		}

		// Token: 0x0600D30F RID: 54031 RVA: 0x002CFF98 File Offset: 0x002CE198
		private static FormulaExpression TranslateFromNumber(FromNumber fromNumber)
		{
			return ExcelExpressionHelper.Variable(fromNumber.columnName.Value);
		}

		// Token: 0x0600D310 RID: 54032 RVA: 0x002CFFBC File Offset: 0x002CE1BC
		private static FormulaExpression TranslateFromNumberCoalesced(FromNumberCoalesced fromNumberCoalesced)
		{
			return ExcelExpressionHelper.Variable(fromNumberCoalesced.columnName.Value);
		}

		// Token: 0x0600D311 RID: 54033 RVA: 0x002CFFE0 File Offset: 0x002CE1E0
		private static FormulaExpression TranslateFromNumberStr(FromNumberStr input)
		{
			return ExcelExpressionHelper.Variable(input.columnName.Value);
		}

		// Token: 0x0600D312 RID: 54034 RVA: 0x002D0004 File Offset: 0x002CE204
		private static FormulaExpression TranslateFromStr(FromStr input)
		{
			return ExcelExpressionHelper.Variable(input.columnName.Value);
		}

		// Token: 0x0600D313 RID: 54035 RVA: 0x002D0028 File Offset: 0x002CE228
		private static FormulaExpression TranslateFromTime(FromTime time)
		{
			return ExcelExpressionHelper.Variable(time.columnName.Value);
		}

		// Token: 0x0600D314 RID: 54036 RVA: 0x002D0049 File Offset: 0x002CE249
		private static FormulaExpression TranslateNull()
		{
			return ExcelExpressionHelper.StringLiteral(string.Empty);
		}

		// Token: 0x0600D315 RID: 54037 RVA: 0x002D0058 File Offset: 0x002CE258
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

		// Token: 0x0600D316 RID: 54038 RVA: 0x002D00B0 File Offset: 0x002CE2B0
		private FormulaExpression TranslateRoundDateTimeDown(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2;
			switch (roundDateTime.dateTimeRoundDesc.Value.Period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression2 = ExcelExpressionHelper.SecondStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = ExcelExpressionHelper.MinuteStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = ExcelExpressionHelper.HourStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = ExcelExpressionHelper.DayStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = ExcelExpressionHelper.WeekStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = ExcelExpressionHelper.MonthStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = ExcelExpressionHelper.QuarterStart(formulaExpression, null);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = ExcelExpressionHelper.YearStart(formulaExpression);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			return formulaExpression2;
		}

		// Token: 0x0600D317 RID: 54039 RVA: 0x002D0170 File Offset: 0x002CE370
		private FormulaExpression TranslateRoundDateTimeNearest(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2;
			switch (roundDateTime.dateTimeRoundDesc.Value.Period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression2 = ExcelExpressionHelper.MRound(formulaExpression, ExcelExpressionHelper.Divide(1.0, ExcelExpressionHelper.Multiply(ExcelExpressionHelper.Multiply(24.0, 60.0), 60.0)));
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = ExcelExpressionHelper.MRound(formulaExpression, ExcelExpressionHelper.Divide(1.0, ExcelExpressionHelper.Multiply(24.0, 60.0)));
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = ExcelExpressionHelper.MRound(formulaExpression, ExcelExpressionHelper.Divide(1.0, 24.0));
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = ExcelExpressionHelper.Round(formulaExpression, 0);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = ExcelExpressionHelper.If(ExcelExpressionHelper.LessThan(ExcelExpressionHelper.DateDif(ExcelExpressionHelper.WeekStart(formulaExpression), formulaExpression), 2.5), ExcelExpressionHelper.WeekStart(formulaExpression), ExcelExpressionHelper.WeekEnd(ExcelExpressionHelper.WeekStart(formulaExpression)));
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = ExcelExpressionHelper.If(ExcelExpressionHelper.LessThan(ExcelExpressionHelper.Day(formulaExpression), 15.0), ExcelExpressionHelper.DateTime(ExcelExpressionHelper.Year(formulaExpression), ExcelExpressionHelper.Month(formulaExpression), ExcelExpressionHelper.NumberLiteral(1)), ExcelExpressionHelper.Plus1(ExcelExpressionHelper.EoMonth(formulaExpression, null)));
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = ExcelExpressionHelper.If(ExcelExpressionHelper.LessThan(ExcelExpressionHelper.DateDif(ExcelExpressionHelper.QuarterStart(formulaExpression, null), formulaExpression), 45.0), ExcelExpressionHelper.QuarterStart(formulaExpression, null), ExcelExpressionHelper.QuarterEnd(ExcelExpressionHelper.QuarterStart(formulaExpression, null), null, null));
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = ExcelExpressionHelper.If(ExcelExpressionHelper.LessThan(ExcelExpressionHelper.DateDif(ExcelExpressionHelper.YearStart(formulaExpression), formulaExpression), 182.5), ExcelExpressionHelper.YearStart(formulaExpression), ExcelExpressionHelper.YearEnd(ExcelExpressionHelper.YearStart(formulaExpression)));
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			return formulaExpression2;
		}

		// Token: 0x0600D318 RID: 54040 RVA: 0x002D0368 File Offset: 0x002CE568
		private FormulaExpression TranslateRoundDateTimeUp(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimeDescriptor value = roundDateTime.dateTimeRoundDesc.Value;
			RoundDateTimePeriod period = value.Period;
			bool flag = value.Ceiling == RoundDatePeriodCeiling.LastDay;
			if (period == RoundDateTimePeriod.Month && flag)
			{
				return ExcelExpressionHelper.EoMonth(formulaExpression, null);
			}
			FormulaExpression formulaExpression2;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression2 = ExcelExpressionHelper.SecondStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = ExcelExpressionHelper.MinuteStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = ExcelExpressionHelper.HourStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = ExcelExpressionHelper.DayStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = ExcelExpressionHelper.WeekStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = ExcelExpressionHelper.MonthStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = ExcelExpressionHelper.QuarterStart(formulaExpression, null);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = ExcelExpressionHelper.YearStart(formulaExpression);
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
				formulaExpression2 = ExcelExpressionHelper.SecondEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = ExcelExpressionHelper.MinuteEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = ExcelExpressionHelper.HourEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = ExcelExpressionHelper.DayEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = ExcelExpressionHelper.WeekEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = ExcelExpressionHelper.MonthEnd(formulaExpression3);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = ExcelExpressionHelper.QuarterEnd(formulaExpression, null, null);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = ExcelExpressionHelper.YearEnd(formulaExpression3);
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
				return ExcelExpressionHelper.If(ExcelExpressionHelper.Equal(formulaExpression, formulaExpression3), formulaExpression3, formulaExpression4);
			}
			return ExcelExpressionHelper.DateAddDays(formulaExpression4, -1.0);
		}

		// Token: 0x0600D319 RID: 54041 RVA: 0x002D0500 File Offset: 0x002CE700
		private FormulaExpression TranslateTimePart(TimePart timePart)
		{
			FormulaExpression formulaExpression = this.Translate(timePart.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2;
			switch (timePart.timePartKind.Value)
			{
			case TimePartKind.Second:
				formulaExpression2 = ExcelExpressionHelper.Second(formulaExpression);
				break;
			case TimePartKind.Minute:
				formulaExpression2 = ExcelExpressionHelper.Minute(formulaExpression);
				break;
			case TimePartKind.Hour:
				formulaExpression2 = ExcelExpressionHelper.Hour(formulaExpression);
				break;
			case TimePartKind.Hour12:
				formulaExpression2 = ExcelExpressionHelper.Mod(ExcelExpressionHelper.Hour(formulaExpression), 12.0);
				break;
			case TimePartKind.TotalSeconds:
				formulaExpression2 = ExcelExpressionHelper.Multiply(formulaExpression, 86400.0);
				break;
			case TimePartKind.TotalMinutes:
				formulaExpression2 = ExcelExpressionHelper.Multiply(formulaExpression, 1440.0);
				break;
			case TimePartKind.TotalHours:
				formulaExpression2 = ExcelExpressionHelper.Multiply(formulaExpression, 24.0);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			return formulaExpression2;
		}

		// Token: 0x0600D31A RID: 54042 RVA: 0x002D05D4 File Offset: 0x002CE7D4
		private FormulaExpression TranslateToDateTime(ToDateTime subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D31B RID: 54043 RVA: 0x002D0600 File Offset: 0x002CE800
		private FormulaExpression TranslateToDecimal(ToDecimal subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D31C RID: 54044 RVA: 0x002D062C File Offset: 0x002CE82C
		private FormulaExpression TranslateToDouble(ToDouble subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D31D RID: 54045 RVA: 0x002D0658 File Offset: 0x002CE858
		private FormulaExpression TranslateToInt(ToInt subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D31E RID: 54046 RVA: 0x002D0684 File Offset: 0x002CE884
		private FormulaExpression TranslateToStr(ToStr subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D31F RID: 54047 RVA: 0x002D06B0 File Offset: 0x002CE8B0
		private FormulaExpression TranslateContains(Contains contains)
		{
			FormulaExpression formulaExpression = ExcelExpressionHelper.Variable(contains.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(contains.containsFindText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(contains.containsCount.Node, default(CancellationToken));
			return ExcelExpressionHelper.Equal(ExcelExpressionHelper.Minus(ExcelExpressionHelper.Len(formulaExpression), ExcelExpressionHelper.Len(ExcelExpressionHelper.Substitute(formulaExpression, formulaExpression2, ExcelExpressionHelper.StringLiteral(string.Empty), null))), formulaExpression3);
		}

		// Token: 0x0600D320 RID: 54048 RVA: 0x002D073C File Offset: 0x002CE93C
		private static FormulaExpression TranslateEndsWithDigit(EndsWithDigit endsWithDigit)
		{
			return ExcelExpressionHelper.IsNumber(ExcelExpressionHelper.Right(ExcelExpressionHelper.Variable(endsWithDigit.columnName.Value)));
		}

		// Token: 0x0600D321 RID: 54049 RVA: 0x002D0768 File Offset: 0x002CE968
		private FormulaExpression TranslateIf(If ifNode)
		{
			FormulaExpression formulaExpression = this.Translate(ifNode.condition.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(ifNode.result1.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(ifNode.result2.Node, default(CancellationToken));
			return ExcelExpressionHelper.If(formulaExpression, formulaExpression2, formulaExpression3);
		}

		// Token: 0x0600D322 RID: 54050 RVA: 0x002D07DC File Offset: 0x002CE9DC
		private static FormulaExpression TranslateIsBlank(IsBlank isBlank)
		{
			return ExcelExpressionHelper.IsBlank(ExcelExpressionHelper.Variable(isBlank.columnName.Value));
		}

		// Token: 0x0600D323 RID: 54051 RVA: 0x002D0804 File Offset: 0x002CEA04
		private static FormulaExpression TranslateIsNotBlank(IsNotBlank isBlank)
		{
			return ExcelExpressionHelper.Not(ExcelExpressionHelper.IsBlank(ExcelExpressionHelper.Variable(isBlank.columnName.Value)));
		}

		// Token: 0x0600D324 RID: 54052 RVA: 0x002D0830 File Offset: 0x002CEA30
		private static FormulaExpression TranslateIsNumber(IsNumber isNumber)
		{
			return ExcelExpressionHelper.IsNumber(ExcelExpressionHelper.Variable(isNumber.columnName.Value));
		}

		// Token: 0x0600D325 RID: 54053 RVA: 0x002D0858 File Offset: 0x002CEA58
		private static FormulaExpression TranslateIsString(IsString isString)
		{
			return ExcelExpressionHelper.IsText(ExcelExpressionHelper.Variable(isString.columnName.Value));
		}

		// Token: 0x0600D326 RID: 54054 RVA: 0x002D0880 File Offset: 0x002CEA80
		private FormulaExpression TranslateNumberEquals(NumberEquals numberEquals)
		{
			FormulaExpression formulaExpression = ExcelExpressionHelper.Variable(numberEquals.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(numberEquals.numberEqualsValue.Node, default(CancellationToken));
			return ExcelExpressionHelper.Equal(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D327 RID: 54055 RVA: 0x002D08C8 File Offset: 0x002CEAC8
		private FormulaExpression TranslateNumberGreaterThan(NumberGreaterThan numberGreaterThan)
		{
			FormulaExpression formulaExpression = ExcelExpressionHelper.Variable(numberGreaterThan.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(numberGreaterThan.numberGreaterThanValue.Node, default(CancellationToken));
			return ExcelExpressionHelper.GreaterThan(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D328 RID: 54056 RVA: 0x002D0910 File Offset: 0x002CEB10
		private FormulaExpression TranslateNumberLessThan(NumberLessThan numberLessThan)
		{
			FormulaExpression formulaExpression = ExcelExpressionHelper.Variable(numberLessThan.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(numberLessThan.numberLessThanValue.Node, default(CancellationToken));
			return ExcelExpressionHelper.LessThan(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D329 RID: 54057 RVA: 0x002D0958 File Offset: 0x002CEB58
		private FormulaExpression TranslateOr(Or or)
		{
			FormulaExpression formulaExpression = this.Translate(or.condition1.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(or.condition2.Node, default(CancellationToken));
			return ExcelExpressionHelper.Or(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D32A RID: 54058 RVA: 0x002D09A8 File Offset: 0x002CEBA8
		private FormulaExpression TranslateStartsWith(StartsWith startsWith)
		{
			FormulaExpression formulaExpression = ExcelExpressionHelper.Variable(startsWith.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(startsWith.startsWithFindText.Node, default(CancellationToken));
			return ExcelExpressionHelper.Equal(ExcelExpressionHelper.Left(formulaExpression, ExcelExpressionHelper.Len(formulaExpression2)), formulaExpression2);
		}

		// Token: 0x0600D32B RID: 54059 RVA: 0x002D09FC File Offset: 0x002CEBFC
		private static FormulaExpression TranslateStartsWithDigit(StartsWithDigit startsWithDigit)
		{
			return ExcelExpressionHelper.IsNumber(ExcelExpressionHelper.Left(ExcelExpressionHelper.Variable(startsWithDigit.columnName.Value)));
		}

		// Token: 0x0600D32C RID: 54060 RVA: 0x002D0A28 File Offset: 0x002CEC28
		private FormulaExpression TranslateStringEquals(StringEquals stringEquals)
		{
			FormulaExpression formulaExpression = ExcelExpressionHelper.Variable(stringEquals.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(stringEquals.equalsText.Node, default(CancellationToken));
			return ExcelExpressionHelper.Equal(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D32D RID: 54061 RVA: 0x002D0A70 File Offset: 0x002CEC70
		private FormulaExpression TranslateAdd(Add add)
		{
			return ExcelExpressionHelper.Plus(this.Translate(add.arithmeticLeft.Node, default(CancellationToken)), this.Translate(add.addRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D32E RID: 54062 RVA: 0x002D0ABE File Offset: 0x002CECBE
		private static FormulaExpression TranslateAverage(Average average)
		{
			return ExcelExpressionHelper.Average(ExcelProgramTranslator.TranslateFromNumbers(average.fromNumbers));
		}

		// Token: 0x0600D32F RID: 54063 RVA: 0x002D0AD4 File Offset: 0x002CECD4
		private FormulaExpression TranslateDivide(Divide divide)
		{
			return ExcelExpressionHelper.Divide(this.Translate(divide.arithmeticLeft.Node, default(CancellationToken)), this.Translate(divide.divideRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D330 RID: 54064 RVA: 0x002D0B24 File Offset: 0x002CED24
		private static IEnumerable<FormulaExpression> TranslateFromNumbers(fromNumbers fromNumbers)
		{
			LiteralNode literalNode = fromNumbers.Node.Children[1] as LiteralNode;
			string[] array = ((literalNode != null) ? literalNode.Value : null) as string[];
			if (array != null)
			{
				IEnumerable<string> enumerable = array;
				Func<string, FormulaExpression> func;
				if ((func = ExcelProgramTranslator.<>O.<0>__Variable) == null)
				{
					func = (ExcelProgramTranslator.<>O.<0>__Variable = new Func<string, FormulaExpression>(ExcelExpressionHelper.Variable));
				}
				return enumerable.Select(func);
			}
			return null;
		}

		// Token: 0x0600D331 RID: 54065 RVA: 0x002D0B7C File Offset: 0x002CED7C
		private FormulaExpression TranslateMultiply(Multiply multiply)
		{
			return ExcelExpressionHelper.Multiply(this.Translate(multiply.arithmeticLeft.Node, default(CancellationToken)), this.Translate(multiply.multiplyRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D332 RID: 54066 RVA: 0x002D0BCA File Offset: 0x002CEDCA
		private static FormulaExpression TranslateProduct(Product product)
		{
			return ExcelExpressionHelper.Product(ExcelProgramTranslator.TranslateFromNumbers(product.fromNumbers));
		}

		// Token: 0x0600D333 RID: 54067 RVA: 0x002D0BE0 File Offset: 0x002CEDE0
		private FormulaExpression TranslateSubtract(Subtract subtract)
		{
			return ExcelExpressionHelper.Minus(this.Translate(subtract.arithmeticLeft.Node, default(CancellationToken)), this.Translate(subtract.subtractRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D334 RID: 54068 RVA: 0x002D0C2E File Offset: 0x002CEE2E
		private static FormulaExpression TranslateSum(Sum sum)
		{
			return ExcelExpressionHelper.Sum(ExcelProgramTranslator.TranslateFromNumbers(sum.fromNumbers));
		}

		// Token: 0x0600D335 RID: 54069 RVA: 0x002D0C44 File Offset: 0x002CEE44
		private FormulaExpression TranslateMatch(Match match)
		{
			return ExcelProgramTranslator.TranslateMatch(this.Translate(match.x.Node, default(CancellationToken)), match.matchInstance.Value, match.matchDesc.Value.Name);
		}

		// Token: 0x0600D336 RID: 54070 RVA: 0x002D0C98 File Offset: 0x002CEE98
		private static FormulaExpression TranslateMatch(FormulaExpression subjectExp, int instanceVal, MatchName matchName)
		{
			if (matchName != MatchName.UpperChar && matchName != (MatchName)(-2147483648))
			{
				return null;
			}
			FormulaExpression formulaExpression = ExcelExpressionHelper.Row(ExcelExpressionHelper.Indirect(ExcelExpressionHelper.Concat(ExcelExpressionHelper.StringLiteral("1:"), ExcelExpressionHelper.Len(subjectExp))));
			FormulaExpression formulaExpression2 = ExcelExpressionHelper.Code(ExcelExpressionHelper.Mid(subjectExp, formulaExpression, ExcelExpressionHelper.NumberLiteral(1)));
			int num = ((matchName == (MatchName)int.MinValue) ? 48 : 65);
			int num2 = ((matchName == (MatchName)int.MinValue) ? 57 : 90);
			FormulaExpression formulaExpression3 = ExcelExpressionHelper.Filter(formulaExpression, ExcelExpressionHelper.FilterAnd(ExcelExpressionHelper.LessThanEqual(num, formulaExpression2), ExcelExpressionHelper.LessThanEqual(formulaExpression2, num2)));
			FormulaExpression formulaExpression4 = ((instanceVal < 0) ? ExcelExpressionHelper.Plus(ExcelExpressionHelper.Rows(formulaExpression3), ExcelExpressionHelper.NumberLiteral(instanceVal + 1)) : ExcelExpressionHelper.NumberLiteral(instanceVal));
			return ExcelExpressionHelper.Index(formulaExpression3, formulaExpression4);
		}

		// Token: 0x0600D337 RID: 54071 RVA: 0x002D0D60 File Offset: 0x002CEF60
		private FormulaExpression TranslateMatchEnd(MatchEnd matchEnd)
		{
			return ExcelExpressionHelper.Plus1(ExcelProgramTranslator.TranslateMatch(this.Translate(matchEnd.x.Node, default(CancellationToken)), matchEnd.matchInstance.Value, matchEnd.matchDesc.Value.Name));
		}

		// Token: 0x0600D338 RID: 54072 RVA: 0x002D0DB8 File Offset: 0x002CEFB8
		private FormulaExpression TranslateFromRowNumber()
		{
			if (this._options.ReferenceRowExpression == null)
			{
				return ExcelExpressionHelper.Row();
			}
			return ExcelExpressionHelper.Minus(ExcelExpressionHelper.Row(), ExcelExpressionHelper.Row(ExcelExpressionHelper.Variable(this._options.ReferenceRowExpression)));
		}

		// Token: 0x0600D339 RID: 54073 RVA: 0x002D0DEC File Offset: 0x002CEFEC
		private FormulaExpression TranslateTransformRowNumberLinear(RowNumberLinearTransform subject)
		{
			FormulaExpression formulaExpression = this.Translate(subject.fromRowNumber.Node, default(CancellationToken));
			RowNumberLinearTransformDescriptor value = subject.rowNumberLinearTransformDesc.Value;
			return ExcelExpressionHelper.Plus(ExcelExpressionHelper.Multiply(ExcelExpressionHelper.NumberLiteral(value.Gradient), formulaExpression), ExcelExpressionHelper.NumberLiteral(value.Intercept));
		}

		// Token: 0x0400514C RID: 20812
		private bool _cancelled;

		// Token: 0x0400514D RID: 20813
		private FormulaExpression _currentInputExpression;

		// Token: 0x0400514E RID: 20814
		private ProgramNode _currentInputNode;

		// Token: 0x0400514F RID: 20815
		private readonly IExcelTranslationOptions _options;

		// Token: 0x02001935 RID: 6453
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005150 RID: 20816
			public static Func<string, FormulaExpression> <0>__Variable;
		}
	}
}

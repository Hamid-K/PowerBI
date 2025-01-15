using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018F7 RID: 6391
	internal class PowerFxProgramTranslator : ProgramTranslatorBase
	{
		// Token: 0x0600D081 RID: 53377 RVA: 0x002C60DC File Offset: 0x002C42DC
		private PowerFxProgramTranslator(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, IPowerFxTranslationOptions options, ILogger logger)
			: base(program, examples, inputs, logger)
		{
			this._options = options ?? new PowerFxTranslationConstraint();
		}

		// Token: 0x0600D082 RID: 53378 RVA: 0x002C60FA File Offset: 0x002C42FA
		public static FormulaExpression Translate(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, IPowerFxTranslationOptions options, ILogger logger = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new PowerFxProgramTranslator(program, examples, inputs, options, logger).Translate(cancellationToken);
		}

		// Token: 0x0600D083 RID: 53379 RVA: 0x002C6110 File Offset: 0x002C4310
		protected override FormulaExpression Translate(CancellationToken cancellationToken = default(CancellationToken))
		{
			FormulaExpression formulaExpression = base.Translate(cancellationToken);
			if (!(formulaExpression == null))
			{
				return PowerFxExpressionOptimizer.Optimize(formulaExpression, this._options);
			}
			return null;
		}

		// Token: 0x0600D084 RID: 53380 RVA: 0x002C613C File Offset: 0x002C433C
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
				Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match match;
				MatchEnd matchEnd;
				MatchFull matchFull;
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
				FromDateTimePart fromDateTimePart;
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
					formulaExpression = PowerFxProgramTranslator.TranslateAbs(abs);
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
					formulaExpression = PowerFxProgramTranslator.TranslateArithmeticRightNumber(node);
				}
				else if (node.Is(out date))
				{
					formulaExpression = this.TranslateDate(date);
				}
				else if (node.Is(out find))
				{
					formulaExpression = this.TranslateFind(find);
				}
				else if (node.Is(out match))
				{
					formulaExpression = this.TranslateMatch(match);
				}
				else if (node.Is(out matchEnd))
				{
					formulaExpression = this.TranslateMatchEnd(matchEnd);
				}
				else if (node.Is(out matchFull))
				{
					formulaExpression = this.TranslateMatchFull(matchFull);
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
					formulaExpression = PowerFxProgramTranslator.TranslateSum(sum);
				}
				else if (node.Is(out product))
				{
					formulaExpression = PowerFxProgramTranslator.TranslateProduct(product);
				}
				else if (node.Is(out average))
				{
					formulaExpression = PowerFxProgramTranslator.TranslateAverage(average);
				}
				else if (node.Is(out fromStr))
				{
					formulaExpression = PowerFxProgramTranslator.TranslateFromStr(fromStr);
				}
				else if (node.Is(out fromDateTime))
				{
					formulaExpression = PowerFxProgramTranslator.TranslateFromDateTime(fromDateTime);
				}
				else if (node.Is(out fromDateTimePart))
				{
					formulaExpression = PowerFxProgramTranslator.TranslateFromDateTimePart(fromDateTimePart);
				}
				else if (node.Is(out fromNumber))
				{
					formulaExpression = PowerFxProgramTranslator.TranslateFromNumber(fromNumber);
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
							goto IL_0676;
						}
						LiteralNode literalNode = node as LiteralNode;
						if (literalNode != null)
						{
							formulaExpression = PowerFxProgramTranslator.TranslateLiteral(literalNode);
							goto IL_0676;
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
					IsMatch isMatch;
					ContainsMatch containsMatch;
					Or or;
					if (node.Is(out @if))
					{
						formulaExpression = this.TranslateIf(@if);
					}
					else if (node.Is(out isString))
					{
						formulaExpression = PowerFxProgramTranslator.TranslateIsString(isString);
					}
					else if (node.Is(out isBlank))
					{
						formulaExpression = PowerFxProgramTranslator.TranslateIsBlank(isBlank);
					}
					else if (node.Is(out isNotBlank))
					{
						formulaExpression = PowerFxProgramTranslator.TranslateIsNotBlank(isNotBlank);
					}
					else if (node.Is(out stringEquals))
					{
						formulaExpression = this.TranslateStringEquals(stringEquals);
					}
					else if (node.Is(out startsWithDigit))
					{
						formulaExpression = PowerFxProgramTranslator.TranslateStartsWithDigit(startsWithDigit);
					}
					else if (node.Is(out endsWithDigit))
					{
						formulaExpression = PowerFxProgramTranslator.TranslateEndsWithDigit(endsWithDigit);
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
						formulaExpression = PowerFxProgramTranslator.TranslateIsNumber(isNumber);
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
					else if (node.Is(out isMatch))
					{
						formulaExpression = this.TranslateIsMatch(isMatch);
					}
					else if (node.Is(out containsMatch))
					{
						formulaExpression = this.TranslateContainsMatch(containsMatch);
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
						formulaExpression = PowerFxProgramTranslator.TranslateNull();
					}
				}
				else
				{
					formulaExpression = this.TranslateVariableNode();
				}
				IL_0676:
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

		// Token: 0x0600D085 RID: 53381 RVA: 0x002C6834 File Offset: 0x002C4A34
		private static FormulaExpression TranslateAbs(Abs abs)
		{
			return PowerFxExpressionHelper.NumberLiteral(abs.absPos.Value);
		}

		// Token: 0x0600D086 RID: 53382 RVA: 0x002C6858 File Offset: 0x002C4A58
		private FormulaExpression TranslateConcat(Concat concat)
		{
			FormulaExpression formulaExpression = this.Translate(concat.concatPrefix.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(concat.concatSuffix.Node, default(CancellationToken));
			return PowerFxExpressionHelper.Concat(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D087 RID: 53383 RVA: 0x002C68A8 File Offset: 0x002C4AA8
		private FormulaExpression TranslateConversionRule(ProgramNode node)
		{
			return this.Translate(node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D088 RID: 53384 RVA: 0x002C68CC File Offset: 0x002C4ACC
		private FormulaExpression TranslateDate(Date date)
		{
			return this.Translate(date.constDt.Node, default(CancellationToken));
		}

		// Token: 0x0600D089 RID: 53385 RVA: 0x002C68F8 File Offset: 0x002C4AF8
		private FormulaExpression TranslateFind(Find find)
		{
			FormulaExpression formulaExpression = this.Translate(find.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(find.findDelimiter.Node, default(CancellationToken));
			int value = find.findInstance.Value;
			FormulaExpression formulaExpression3 = PowerFxProgramTranslator.ResolveMatchStart(formulaExpression, formulaExpression2, value);
			int value2 = find.findOffset.Value;
			FormulaExpression formulaExpression4;
			if (value2 <= 0)
			{
				if (value2 >= 0)
				{
					formulaExpression4 = formulaExpression3;
				}
				else
				{
					formulaExpression4 = PowerFxExpressionHelper.Minus(formulaExpression3, PowerFxExpressionHelper.NumberLiteral(-value2));
				}
			}
			else
			{
				formulaExpression4 = PowerFxExpressionHelper.Plus(formulaExpression3, PowerFxExpressionHelper.NumberLiteral(value2));
			}
			return formulaExpression4;
		}

		// Token: 0x0600D08A RID: 53386 RVA: 0x002C69A0 File Offset: 0x002C4BA0
		private FormulaExpression TranslateFormatNumber(FormatNumber formatNumber)
		{
			FormulaExpression formulaExpression = this.Translate(formatNumber.Node.Children[0], default(CancellationToken));
			FormatNumberDescriptor value = formatNumber.numberFormatDesc.Value;
			if (value.IncludePercentSymbol)
			{
				formulaExpression = PowerFxExpressionHelper.Multiply(formulaExpression, 100.0);
			}
			return PowerFxExpressionHelper.Text(formulaExpression, PowerFxExpressionHelper.StringLiteral(value.ToSimplifiedFormatString()), PowerFxExpressionHelper.Locale(value.Locale));
		}

		// Token: 0x0600D08B RID: 53387 RVA: 0x002C6A10 File Offset: 0x002C4C10
		private FormulaExpression TranslateLength(Length length)
		{
			return PowerFxExpressionHelper.Len(this.Translate(length.fromStr.Node, default(CancellationToken)));
		}

		// Token: 0x0600D08C RID: 53388 RVA: 0x002C6A40 File Offset: 0x002C4C40
		private FormulaExpression TranslateLetX(LetX letX)
		{
			ProgramNode programNode = letX.Node.Children[0].Children[0];
			this._currentInputColumnName = (string)((LiteralNode)programNode.Children[1]).Value;
			FormulaExpression formulaExpression = this.Translate(letX.Node.Children[1], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return formulaExpression;
			}
			return null;
		}

		// Token: 0x0600D08D RID: 53389 RVA: 0x002C6AAC File Offset: 0x002C4CAC
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
					formulaExpression = PowerFxExpressionHelper.NumberLiteral(num);
				}
				else if (value is double)
				{
					double num2 = (double)value;
					formulaExpression = PowerFxExpressionHelper.NumberLiteral(num2);
				}
				else if (value is decimal)
				{
					decimal num3 = (decimal)value;
					formulaExpression = PowerFxExpressionHelper.NumberLiteral(num3);
				}
				else if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					formulaExpression = PowerFxExpressionHelper.DateTimeLiteral(dateTime);
				}
				else
				{
					MatchDescriptor matchDescriptor = value as MatchDescriptor;
					if (matchDescriptor == null)
					{
						Regex regex = value as Regex;
						if (regex == null)
						{
							formulaExpression = null;
						}
						else
						{
							formulaExpression = PowerFxExpressionHelper.RegexLiteral(regex.ToString());
						}
					}
					else
					{
						formulaExpression = PowerFxExpressionHelper.RegexLiteral(matchDescriptor.Regex.ToString());
					}
				}
			}
			else
			{
				formulaExpression = PowerFxExpressionHelper.StringLiteral(text);
			}
			return formulaExpression;
		}

		// Token: 0x0600D08E RID: 53390 RVA: 0x002C6B94 File Offset: 0x002C4D94
		private FormulaExpression TranslateLowerCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerFxExpressionHelper.Lower(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D08F RID: 53391 RVA: 0x002C6BCC File Offset: 0x002C4DCC
		private FormulaExpression TranslateMatch(Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match match)
		{
			FormulaExpression formulaExpression = this.Translate(match.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(match.matchDesc.Node, default(CancellationToken));
			int value = match.matchInstance.Value;
			return PowerFxProgramTranslator.ResolveMatchStart(formulaExpression, formulaExpression2, value);
		}

		// Token: 0x0600D090 RID: 53392 RVA: 0x002C6C30 File Offset: 0x002C4E30
		private FormulaExpression TranslateMatchEnd(MatchEnd matchEnd)
		{
			FormulaExpression formulaExpression = this.Translate(matchEnd.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(matchEnd.matchDesc.Node, default(CancellationToken));
			int value = matchEnd.matchInstance.Value;
			FormulaExpression formulaExpression3 = PowerFxProgramTranslator.ResolveMatch(formulaExpression, formulaExpression2, value);
			if (!(formulaExpression3 is PowerFxFind))
			{
				return PowerFxExpressionHelper.Plus(PowerFxExpressionHelper.Dot(formulaExpression3, "StartMatch"), PowerFxExpressionHelper.Len(PowerFxExpressionHelper.Dot(formulaExpression3, "FullMatch")));
			}
			return PowerFxExpressionHelper.Plus1(formulaExpression3);
		}

		// Token: 0x0600D091 RID: 53393 RVA: 0x002C6CC8 File Offset: 0x002C4EC8
		private FormulaExpression TranslateMatchFull(MatchFull matchFull)
		{
			FormulaExpression formulaExpression = this.Translate(matchFull.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(matchFull.matchDesc.Node, default(CancellationToken));
			int value = matchFull.matchInstance.Value;
			FormulaExpression formulaExpression3 = PowerFxProgramTranslator.ResolveMatch(formulaExpression, formulaExpression2, value);
			if (!(formulaExpression3 == null))
			{
				return PowerFxExpressionHelper.Dot(formulaExpression3, "FullMatch");
			}
			return null;
		}

		// Token: 0x0600D092 RID: 53394 RVA: 0x002C6D44 File Offset: 0x002C4F44
		private static FormulaExpression TranslateArithmeticRightNumber(ProgramNode node)
		{
			decimal num;
			if (!node.IsArithmeticRightNumber(out num))
			{
				return null;
			}
			return PowerFxExpressionHelper.NumberLiteral(num);
		}

		// Token: 0x0600D093 RID: 53395 RVA: 0x002C6D64 File Offset: 0x002C4F64
		private FormulaExpression TranslateNumber(Number number)
		{
			return this.Translate(number.constNum.Node, default(CancellationToken));
		}

		// Token: 0x0600D094 RID: 53396 RVA: 0x002C6D90 File Offset: 0x002C4F90
		private FormulaExpression TranslateParseNumber(ParseNumber parseNumber)
		{
			FormulaExpression formulaExpression = this.Translate(parseNumber.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(parseNumber.Node.Children[1], default(CancellationToken));
			FormulaStringLiteral formulaStringLiteral = formulaExpression2 as FormulaStringLiteral;
			if (formulaStringLiteral != null)
			{
				formulaExpression2 = PowerFxExpressionHelper.Locale(formulaStringLiteral.Value);
			}
			return PowerFxExpressionHelper.Value(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D095 RID: 53397 RVA: 0x002C6DF4 File Offset: 0x002C4FF4
		private FormulaExpression TranslateProperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerFxExpressionHelper.Proper(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D096 RID: 53398 RVA: 0x002C6E2C File Offset: 0x002C502C
		private FormulaExpression TranslateReplace(Replace replace)
		{
			FormulaExpression formulaExpression = this.Translate(replace.fromStr.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(replace.replaceFindText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(replace.replaceText.Node, default(CancellationToken));
			return PowerFxExpressionHelper.Substitute(formulaExpression, formulaExpression2, formulaExpression3);
		}

		// Token: 0x0600D097 RID: 53399 RVA: 0x002C6EA0 File Offset: 0x002C50A0
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
					formulaExpression2 = PowerFxExpressionHelper.Round(formulaExpression, PowerFxExpressionHelper.NumberLiteral(num4));
					break;
				case RoundingMode.Down:
					formulaExpression2 = PowerFxExpressionHelper.RoundDown(formulaExpression, PowerFxExpressionHelper.NumberLiteral(num4));
					break;
				case RoundingMode.Up:
					formulaExpression2 = PowerFxExpressionHelper.RoundUp(formulaExpression, PowerFxExpressionHelper.NumberLiteral(num4));
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
				formulaExpression2 = PowerFxExpressionHelper.Divide(PowerFxExpressionHelper.Round(PowerFxExpressionHelper.Multiply(formulaExpression, PowerFxExpressionHelper.NumberLiteral(num)), PowerFxExpressionHelper.NumberLiteral(0)), PowerFxExpressionHelper.NumberLiteral(num));
				break;
			case RoundingMode.Down:
				formulaExpression2 = PowerFxExpressionHelper.Divide(PowerFxExpressionHelper.RoundDown(PowerFxExpressionHelper.Multiply(formulaExpression, PowerFxExpressionHelper.NumberLiteral(num)), PowerFxExpressionHelper.NumberLiteral(0)), PowerFxExpressionHelper.NumberLiteral(num));
				break;
			case RoundingMode.Up:
				formulaExpression2 = PowerFxExpressionHelper.Divide(PowerFxExpressionHelper.RoundUp(PowerFxExpressionHelper.Multiply(formulaExpression, PowerFxExpressionHelper.NumberLiteral(num)), PowerFxExpressionHelper.NumberLiteral(0)), PowerFxExpressionHelper.NumberLiteral(num));
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

		// Token: 0x0600D098 RID: 53400 RVA: 0x002C7070 File Offset: 0x002C5270
		private FormulaExpression TranslateSlice(Slice slice)
		{
			FormulaExpression formulaExpression = this.Translate(slice.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slice.pos1.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(slice.pos2.Node, default(CancellationToken));
			FormulaNumberLiteral formulaNumberLiteral = formulaExpression2 as FormulaNumberLiteral;
			int? num = ((formulaNumberLiteral != null) ? new int?((int)formulaNumberLiteral.Value) : null);
			FormulaNumberLiteral formulaNumberLiteral2 = formulaExpression3 as FormulaNumberLiteral;
			int? num2 = ((formulaNumberLiteral2 != null) ? new int?((int)formulaNumberLiteral2.Value) : null);
			if (num != null && num.GetValueOrDefault() < 0)
			{
				formulaExpression2 = PowerFxExpressionHelper.Minus(PowerFxExpressionHelper.Len(formulaExpression), PowerFxExpressionHelper.NumberLiteral(-num.Value - 1));
			}
			if (num2 != null && num2.GetValueOrDefault() < 0)
			{
				formulaExpression3 = PowerFxExpressionHelper.Minus(PowerFxExpressionHelper.Len(formulaExpression), PowerFxExpressionHelper.NumberLiteral(-num2.Value - 1));
			}
			if (num != null)
			{
				int valueOrDefault = num.GetValueOrDefault();
				if (num2 != null)
				{
					int valueOrDefault2 = num2.GetValueOrDefault();
					int? num3 = num;
					FormulaExpression formulaExpression4;
					if (num3 != null)
					{
						int valueOrDefault3 = num3.GetValueOrDefault();
						if (valueOrDefault3 <= 0)
						{
							if (valueOrDefault3 < 0)
							{
								if (valueOrDefault2 > 0)
								{
									formulaExpression4 = PowerFxExpressionHelper.Minus(PowerFxExpressionHelper.NumberLiteral(valueOrDefault2), PowerFxExpressionHelper.Plus(PowerFxExpressionHelper.NumberLiteral(valueOrDefault), PowerFxExpressionHelper.Len(formulaExpression)));
									goto IL_01D4;
								}
								if (valueOrDefault2 < 0)
								{
									formulaExpression4 = PowerFxExpressionHelper.NumberLiteral(Math.Abs(valueOrDefault - valueOrDefault2));
									goto IL_01D4;
								}
							}
						}
						else
						{
							if (valueOrDefault2 > 0)
							{
								formulaExpression4 = PowerFxExpressionHelper.NumberLiteral(valueOrDefault2 - valueOrDefault);
								goto IL_01D4;
							}
							if (valueOrDefault2 < 0)
							{
								formulaExpression4 = PowerFxExpressionHelper.Minus(PowerFxExpressionHelper.Len(formulaExpression), PowerFxExpressionHelper.NumberLiteral(Math.Abs(valueOrDefault2 + 1) + valueOrDefault));
								goto IL_01D4;
							}
						}
					}
					formulaExpression4 = null;
					IL_01D4:
					FormulaExpression formulaExpression5 = formulaExpression4;
					if (!(formulaExpression5 == null))
					{
						return PowerFxExpressionHelper.Mid(formulaExpression, formulaExpression2, formulaExpression5);
					}
					return null;
				}
			}
			return PowerFxExpressionHelper.Mid(PowerFxExpressionHelper.Left(formulaExpression, PowerFxExpressionHelper.Minus1(formulaExpression3)), formulaExpression2, null);
		}

		// Token: 0x0600D099 RID: 53401 RVA: 0x002C7280 File Offset: 0x002C5480
		private FormulaExpression TranslateSliceBetween(SliceBetween sliceBetween)
		{
			FormulaExpression formulaExpression = this.Translate(sliceBetween.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(sliceBetween.sliceBetweenStartText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(sliceBetween.sliceBetweenEndText.Node, default(CancellationToken));
			FormulaExpression formulaExpression4 = PowerFxExpressionHelper.Plus(PowerFxExpressionHelper.Find(formulaExpression2, formulaExpression), PowerFxExpressionHelper.Len(formulaExpression2));
			FormulaExpression formulaExpression5 = PowerFxExpressionHelper.Find(formulaExpression3, formulaExpression, formulaExpression4);
			return PowerFxExpressionHelper.Mid(formulaExpression, formulaExpression4, PowerFxExpressionHelper.Minus(formulaExpression5, formulaExpression4));
		}

		// Token: 0x0600D09A RID: 53402 RVA: 0x002C7318 File Offset: 0x002C5518
		private FormulaExpression TranslateSlicePrefix(SlicePrefix slicePrefix)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slicePrefix.pos.Node, default(CancellationToken));
			FormulaNumberLiteral formulaNumberLiteral = formulaExpression2 as FormulaNumberLiteral;
			int? num = ((formulaNumberLiteral != null) ? new int?((int)formulaNumberLiteral.Value) : null);
			int num2 = 0;
			if (!((num.GetValueOrDefault() < num2) & (num != null)))
			{
				return PowerFxExpressionHelper.Left(formulaExpression, PowerFxExpressionHelper.Minus1(formulaExpression2));
			}
			return PowerFxExpressionHelper.Left(formulaExpression, PowerFxExpressionHelper.Minus(PowerFxExpressionHelper.Len(formulaExpression), PowerFxExpressionHelper.Minus1(formulaExpression2)));
		}

		// Token: 0x0600D09B RID: 53403 RVA: 0x002C73C8 File Offset: 0x002C55C8
		private FormulaExpression TranslateSlicePrefixAbs(SlicePrefixAbs slicePrefixAbs)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefixAbs.x.Node, default(CancellationToken));
			int value = slicePrefixAbs.slicePrefixAbsPos.Value;
			FormulaExpression formulaExpression2 = PowerFxExpressionHelper.NumberLiteral(value);
			if (value >= 0)
			{
				return PowerFxExpressionHelper.Left(formulaExpression, PowerFxExpressionHelper.Minus1(formulaExpression2));
			}
			return PowerFxExpressionHelper.Left(formulaExpression, PowerFxExpressionHelper.Minus(PowerFxExpressionHelper.Len(formulaExpression), PowerFxExpressionHelper.Minus1(formulaExpression2)));
		}

		// Token: 0x0600D09C RID: 53404 RVA: 0x002C7434 File Offset: 0x002C5634
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
				return PowerFxExpressionHelper.Right(formulaExpression, PowerFxExpressionHelper.Minus(PowerFxExpressionHelper.Len(formulaExpression), PowerFxExpressionHelper.Minus1(formulaExpression2)));
			}
			return PowerFxExpressionHelper.Right(formulaExpression, PowerFxExpressionHelper.NumberLiteral(-num.Value));
		}

		// Token: 0x0600D09D RID: 53405 RVA: 0x002C74EC File Offset: 0x002C56EC
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
						formulaExpression3 = PowerFxExpressionHelper.First(PowerFxExpressionHelper.Split(formulaExpression, formulaExpression2));
					}
					else
					{
						formulaExpression3 = PowerFxExpressionHelper.Last(PowerFxExpressionHelper.FirstN(PowerFxExpressionHelper.Split(formulaExpression, formulaExpression2), PowerFxExpressionHelper.NumberLiteral(value)));
					}
				}
				else
				{
					formulaExpression3 = PowerFxExpressionHelper.Last(PowerFxExpressionHelper.Split(formulaExpression, formulaExpression2));
				}
			}
			else
			{
				formulaExpression3 = PowerFxExpressionHelper.First(PowerFxExpressionHelper.LastN(PowerFxExpressionHelper.Split(formulaExpression, formulaExpression2), PowerFxExpressionHelper.NumberLiteral(-value)));
			}
			return PowerFxExpressionHelper.Dot(formulaExpression3, "Result");
		}

		// Token: 0x0600D09E RID: 53406 RVA: 0x002C75C0 File Offset: 0x002C57C0
		private FormulaExpression TranslateStr(Str str)
		{
			return this.Translate(str.constStr.Node, default(CancellationToken));
		}

		// Token: 0x0600D09F RID: 53407 RVA: 0x002C75EC File Offset: 0x002C57EC
		private FormulaExpression TranslateTrim(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerFxExpressionHelper.TrimEnds(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D0A0 RID: 53408 RVA: 0x002C7624 File Offset: 0x002C5824
		private FormulaExpression TranslateUpperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PowerFxExpressionHelper.Upper(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D0A1 RID: 53409 RVA: 0x002C765A File Offset: 0x002C585A
		private FormulaExpression TranslateVariableNode()
		{
			if (this._currentInputColumnName != null)
			{
				return PowerFxExpressionHelper.Variable(this._currentInputColumnName);
			}
			return null;
		}

		// Token: 0x0600D0A2 RID: 53410 RVA: 0x002C7674 File Offset: 0x002C5874
		private FormulaExpression TranslateDateTimePart(DateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = this.Translate(dateTimePart.Node.Children[0], default(CancellationToken));
			DateTimePartKind value = dateTimePart.dateTimePartKind.Value;
			FormulaExpression formulaExpression2 = formulaExpression;
			PowerFxProgramTranslator.<>c__DisplayClass39_0 CS$<>8__locals1;
			CS$<>8__locals1.withRecords = new List<Dictionary<string, FormulaExpression>>();
			if (!(formulaExpression is PowerFxVariable))
			{
				PowerFxVariable powerFxVariable = (PowerFxVariable)PowerFxExpressionHelper.Variable("date");
				PowerFxProgramTranslator.<TranslateDateTimePart>g__NewField|39_0(powerFxVariable, formulaExpression2, ref CS$<>8__locals1);
				formulaExpression2 = powerFxVariable;
			}
			PowerFxVariable powerFxVariable2 = null;
			bool flag = value - DateTimePartKind.MonthWeek <= 1;
			if (flag)
			{
				powerFxVariable2 = (PowerFxVariable)PowerFxExpressionHelper.Variable("monthStart");
				PowerFxProgramTranslator.<TranslateDateTimePart>g__NewField|39_0(powerFxVariable2, PowerFxExpressionHelper.MonthStart(formulaExpression2), ref CS$<>8__locals1);
			}
			PowerFxVariable powerFxVariable3 = null;
			PowerFxVariable powerFxVariable4 = null;
			flag = value - DateTimePartKind.QuarterDay <= 2;
			if (flag)
			{
				powerFxVariable3 = (PowerFxVariable)PowerFxExpressionHelper.Variable("quarter");
				PowerFxProgramTranslator.<TranslateDateTimePart>g__NewField|39_0(powerFxVariable3, PowerFxExpressionHelper.Quarter(formulaExpression2), ref CS$<>8__locals1);
				powerFxVariable4 = (PowerFxVariable)PowerFxExpressionHelper.Variable("quarterStart");
				PowerFxProgramTranslator.<TranslateDateTimePart>g__NewField|39_0(powerFxVariable4, PowerFxExpressionHelper.QuarterStart(formulaExpression2, powerFxVariable3), ref CS$<>8__locals1);
			}
			PowerFxVariable powerFxVariable5 = null;
			if (value == DateTimePartKind.QuarterDays)
			{
				powerFxVariable5 = (PowerFxVariable)PowerFxExpressionHelper.Variable("quarterEnd");
				PowerFxProgramTranslator.<TranslateDateTimePart>g__NewField|39_0(powerFxVariable5, PowerFxExpressionHelper.QuarterEnd(formulaExpression2, powerFxVariable3, powerFxVariable4), ref CS$<>8__locals1);
			}
			PowerFxVariable powerFxVariable6 = null;
			PowerFxVariable powerFxVariable7 = null;
			if (value == DateTimePartKind.YearDays)
			{
				powerFxVariable6 = (PowerFxVariable)PowerFxExpressionHelper.Variable("yearStart");
				PowerFxProgramTranslator.<TranslateDateTimePart>g__NewField|39_0(powerFxVariable6, PowerFxExpressionHelper.YearStart(formulaExpression2), ref CS$<>8__locals1);
				powerFxVariable7 = (PowerFxVariable)PowerFxExpressionHelper.Variable("yearEnd");
				PowerFxProgramTranslator.<TranslateDateTimePart>g__NewField|39_0(powerFxVariable7, PowerFxExpressionHelper.YearEnd(powerFxVariable6), ref CS$<>8__locals1);
			}
			FormulaExpression formulaExpression3;
			switch (value)
			{
			case DateTimePartKind.Second:
				formulaExpression3 = PowerFxExpressionHelper.Second(formulaExpression);
				break;
			case DateTimePartKind.Minute:
				formulaExpression3 = PowerFxExpressionHelper.Minute(formulaExpression);
				break;
			case DateTimePartKind.Hour:
				formulaExpression3 = PowerFxExpressionHelper.Hour(formulaExpression);
				break;
			case DateTimePartKind.WeekDay:
				formulaExpression3 = PowerFxExpressionHelper.Weekday(formulaExpression);
				break;
			case DateTimePartKind.MonthDay:
				formulaExpression3 = PowerFxExpressionHelper.Day(formulaExpression);
				break;
			case DateTimePartKind.MonthWeek:
				formulaExpression3 = PowerFxExpressionHelper.MonthWeek(formulaExpression, powerFxVariable2);
				break;
			case DateTimePartKind.MonthDays:
				formulaExpression3 = PowerFxExpressionHelper.MonthDays(formulaExpression, powerFxVariable2);
				break;
			case DateTimePartKind.Month:
				formulaExpression3 = PowerFxExpressionHelper.Month(formulaExpression);
				break;
			case DateTimePartKind.QuarterDay:
				formulaExpression3 = PowerFxExpressionHelper.QuarterDay(formulaExpression, powerFxVariable3, powerFxVariable4);
				break;
			case DateTimePartKind.QuarterWeek:
				formulaExpression3 = PowerFxExpressionHelper.QuarterWeek(formulaExpression, powerFxVariable3, powerFxVariable4);
				break;
			case DateTimePartKind.QuarterDays:
				formulaExpression3 = PowerFxExpressionHelper.QuarterDays(formulaExpression, powerFxVariable3, powerFxVariable4, powerFxVariable5);
				break;
			case DateTimePartKind.Quarter:
				formulaExpression3 = PowerFxExpressionHelper.Quarter(formulaExpression);
				break;
			case DateTimePartKind.YearDay:
				formulaExpression3 = PowerFxExpressionHelper.YearDay(formulaExpression);
				break;
			case DateTimePartKind.YearWeek:
				formulaExpression3 = PowerFxExpressionHelper.YearWeek(formulaExpression);
				break;
			case DateTimePartKind.YearDays:
				formulaExpression3 = PowerFxExpressionHelper.YearDays(formulaExpression, powerFxVariable6, powerFxVariable7);
				break;
			case DateTimePartKind.Year:
				formulaExpression3 = PowerFxExpressionHelper.Year(formulaExpression);
				break;
			default:
				formulaExpression3 = null;
				break;
			}
			FormulaExpression formulaExpression4 = formulaExpression3;
			return PowerFxExpressionHelper.With(CS$<>8__locals1.withRecords, formulaExpression4);
		}

		// Token: 0x0600D0A3 RID: 53411 RVA: 0x002C790C File Offset: 0x002C5B0C
		private FormulaExpression TranslateFormatDateTime(FormatDateTime formatDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(formatDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = formatDateTime.dateTimeFormatDesc.Value;
			string text = value.Mask.Replace("'", "").Replace("%d", "d").Replace("de", "'de'")
				.Replace("MMMM", "mmmm")
				.Replace("MMM", "mmm")
				.Replace("MM", "mm")
				.Replace("%M", "m")
				.Replace("M", "m")
				.Replace("tt", "AM/PM");
			return PowerFxExpressionHelper.Text(formulaExpression, PowerFxExpressionHelper.StringLiteral(text), PowerFxExpressionHelper.Locale(value.Locale));
		}

		// Token: 0x0600D0A4 RID: 53412 RVA: 0x002C79EC File Offset: 0x002C5BEC
		private FormulaExpression TranslateParseDateTime(ParseDateTime parseDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(parseDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = parseDateTime.dateTimeParseDesc.Value;
			string formatMask = value.Mask;
			bool flag = PowerFxProgramTranslator._validDateTimeFormatRegexes.Any((Regex r) => r.IsMatch(formatMask)) || PowerFxProgramTranslator._validDateFormatRegexes.Any((Regex r) => r.IsMatch(formatMask));
			bool flag2 = PowerFxProgramTranslator._validTimeFormatRegexes.Any((Regex r) => r.IsMatch(formatMask));
			if (flag)
			{
				return PowerFxExpressionHelper.DateTimeValue(formulaExpression, PowerFxExpressionHelper.Locale(value.Locale));
			}
			if (!flag2)
			{
				return null;
			}
			return PowerFxExpressionHelper.TimeValue(formulaExpression);
		}

		// Token: 0x0600D0A5 RID: 53413 RVA: 0x002C7AA4 File Offset: 0x002C5CA4
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

		// Token: 0x0600D0A6 RID: 53414 RVA: 0x002C7AFC File Offset: 0x002C5CFC
		private FormulaExpression TranslateRoundDateTimeDown(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimePeriod period = roundDateTime.dateTimeRoundDesc.Value.Period;
			PowerFxProgramTranslator.<>c__DisplayClass43_0 CS$<>8__locals1;
			CS$<>8__locals1.withRecords = new List<Dictionary<string, FormulaExpression>>();
			if (!(formulaExpression is PowerFxVariable))
			{
				PowerFxVariable powerFxVariable = (PowerFxVariable)PowerFxExpressionHelper.Variable("date");
				PowerFxProgramTranslator.<TranslateRoundDateTimeDown>g__NewField|43_0(powerFxVariable, formulaExpression, ref CS$<>8__locals1);
				formulaExpression = powerFxVariable;
			}
			PowerFxVariable powerFxVariable2 = null;
			if (period == RoundDateTimePeriod.Quarter)
			{
				powerFxVariable2 = (PowerFxVariable)PowerFxExpressionHelper.Variable("quarter");
				PowerFxProgramTranslator.<TranslateRoundDateTimeDown>g__NewField|43_0(powerFxVariable2, PowerFxExpressionHelper.Quarter(formulaExpression), ref CS$<>8__locals1);
			}
			FormulaExpression formulaExpression2;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression2 = PowerFxExpressionHelper.SecondStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression2 = PowerFxExpressionHelper.MinuteStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression2 = PowerFxExpressionHelper.HourStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression2 = PowerFxExpressionHelper.DayStart(formulaExpression, true);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression2 = PowerFxExpressionHelper.WeekStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression2 = PowerFxExpressionHelper.MonthStart(formulaExpression);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression2 = PowerFxExpressionHelper.QuarterStart(formulaExpression, powerFxVariable2);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression2 = PowerFxExpressionHelper.YearStart(formulaExpression);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			FormulaExpression formulaExpression3 = formulaExpression2;
			PowerFxVariable powerFxVariable3 = (PowerFxVariable)PowerFxExpressionHelper.Variable(period.ToString().ToLower() + "Start");
			PowerFxProgramTranslator.<TranslateRoundDateTimeDown>g__NewField|43_0(powerFxVariable3, formulaExpression3, ref CS$<>8__locals1);
			return PowerFxExpressionHelper.With(CS$<>8__locals1.withRecords, powerFxVariable3);
		}

		// Token: 0x0600D0A7 RID: 53415 RVA: 0x002C7C50 File Offset: 0x002C5E50
		private FormulaExpression TranslateRoundDateTimeNearest(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimePeriod period = roundDateTime.dateTimeRoundDesc.Value.Period;
			FormulaExpression formulaExpression2 = formulaExpression;
			PowerFxProgramTranslator.<>c__DisplayClass44_0 CS$<>8__locals1;
			CS$<>8__locals1.withRecords = new List<Dictionary<string, FormulaExpression>>();
			if (!(formulaExpression is PowerFxVariable))
			{
				PowerFxVariable powerFxVariable = (PowerFxVariable)PowerFxExpressionHelper.Variable("date");
				PowerFxProgramTranslator.<TranslateRoundDateTimeNearest>g__NewField|44_0(powerFxVariable, formulaExpression2, ref CS$<>8__locals1);
				formulaExpression2 = powerFxVariable;
			}
			FormulaExpression formulaExpression3;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression3 = PowerFxExpressionHelper.SecondStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression3 = PowerFxExpressionHelper.MinuteStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression3 = PowerFxExpressionHelper.HourStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression3 = PowerFxExpressionHelper.DayStart(formulaExpression2, true);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression3 = PowerFxExpressionHelper.WeekStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression3 = PowerFxExpressionHelper.MonthStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression3 = PowerFxExpressionHelper.QuarterStart(formulaExpression2, null);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression3 = PowerFxExpressionHelper.YearStart(formulaExpression2);
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
			PowerFxVariable powerFxVariable2 = (PowerFxVariable)PowerFxExpressionHelper.Variable(period.ToString().ToLower() + "Start");
			PowerFxProgramTranslator.<TranslateRoundDateTimeNearest>g__NewField|44_0(powerFxVariable2, formulaExpression4, ref CS$<>8__locals1);
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression3 = PowerFxExpressionHelper.SecondEnd(powerFxVariable2);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression3 = PowerFxExpressionHelper.MinuteEnd(powerFxVariable2);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression3 = PowerFxExpressionHelper.HourEnd(powerFxVariable2);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression3 = PowerFxExpressionHelper.DayEnd(powerFxVariable2);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression3 = PowerFxExpressionHelper.WeekEnd(powerFxVariable2);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression3 = PowerFxExpressionHelper.MonthEnd(powerFxVariable2);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression3 = PowerFxExpressionHelper.QuarterEnd(formulaExpression2, null, powerFxVariable2);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression3 = PowerFxExpressionHelper.YearEnd(powerFxVariable2);
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
			PowerFxVariable powerFxVariable3 = (PowerFxVariable)PowerFxExpressionHelper.Variable(period.ToString().ToLower() + "End");
			PowerFxProgramTranslator.<TranslateRoundDateTimeNearest>g__NewField|44_0(powerFxVariable3, formulaExpression5, ref CS$<>8__locals1);
			PowerFxVariable powerFxVariable4 = (PowerFxVariable)PowerFxExpressionHelper.Variable(period.ToString().ToLower() + "Midpoint");
			FormulaExpression formulaExpression6 = PowerFxExpressionHelper.Midpoint(powerFxVariable2, powerFxVariable3);
			PowerFxProgramTranslator.<TranslateRoundDateTimeNearest>g__NewField|44_0(powerFxVariable4, formulaExpression6, ref CS$<>8__locals1);
			return PowerFxExpressionHelper.With(CS$<>8__locals1.withRecords, PowerFxExpressionHelper.If(PowerFxExpressionHelper.LessThan(formulaExpression2, powerFxVariable4), powerFxVariable2, powerFxVariable3));
		}

		// Token: 0x0600D0A8 RID: 53416 RVA: 0x002C7EA0 File Offset: 0x002C60A0
		private FormulaExpression TranslateRoundDateTimeUp(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimeDescriptor value = roundDateTime.dateTimeRoundDesc.Value;
			RoundDateTimePeriod period = value.Period;
			bool flag = value.Ceiling == RoundDatePeriodCeiling.LastDay;
			FormulaExpression formulaExpression2 = formulaExpression;
			PowerFxProgramTranslator.<>c__DisplayClass45_0 CS$<>8__locals1;
			CS$<>8__locals1.withRecords = new List<Dictionary<string, FormulaExpression>>();
			if (!(formulaExpression is PowerFxVariable))
			{
				PowerFxVariable powerFxVariable = (PowerFxVariable)PowerFxExpressionHelper.Variable("date");
				PowerFxProgramTranslator.<TranslateRoundDateTimeUp>g__NewField|45_0(powerFxVariable, formulaExpression2, ref CS$<>8__locals1);
				formulaExpression2 = powerFxVariable;
			}
			PowerFxVariable powerFxVariable2 = null;
			if (period == RoundDateTimePeriod.Quarter)
			{
				powerFxVariable2 = (PowerFxVariable)PowerFxExpressionHelper.Variable("quarter");
				PowerFxProgramTranslator.<TranslateRoundDateTimeUp>g__NewField|45_0(powerFxVariable2, PowerFxExpressionHelper.Quarter(formulaExpression2), ref CS$<>8__locals1);
			}
			FormulaExpression formulaExpression3;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression3 = PowerFxExpressionHelper.SecondStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression3 = PowerFxExpressionHelper.MinuteStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression3 = PowerFxExpressionHelper.HourStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression3 = PowerFxExpressionHelper.DayStart(formulaExpression2, false);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression3 = PowerFxExpressionHelper.WeekStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression3 = PowerFxExpressionHelper.MonthStart(formulaExpression2);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression3 = PowerFxExpressionHelper.QuarterStart(formulaExpression2, powerFxVariable2);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression3 = PowerFxExpressionHelper.YearStart(formulaExpression2);
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
			PowerFxVariable powerFxVariable3 = (PowerFxVariable)PowerFxExpressionHelper.Variable(period.ToString().ToLower() + "Start");
			PowerFxProgramTranslator.<TranslateRoundDateTimeUp>g__NewField|45_0(powerFxVariable3, formulaExpression4, ref CS$<>8__locals1);
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression3 = PowerFxExpressionHelper.SecondEnd(powerFxVariable3);
				break;
			case RoundDateTimePeriod.Minute:
				formulaExpression3 = PowerFxExpressionHelper.MinuteEnd(powerFxVariable3);
				break;
			case RoundDateTimePeriod.Hour:
				formulaExpression3 = PowerFxExpressionHelper.HourEnd(powerFxVariable3);
				break;
			case RoundDateTimePeriod.Day:
				formulaExpression3 = PowerFxExpressionHelper.DayEnd(powerFxVariable3);
				break;
			case RoundDateTimePeriod.Week:
				formulaExpression3 = PowerFxExpressionHelper.WeekEnd(powerFxVariable3);
				break;
			case RoundDateTimePeriod.Month:
				formulaExpression3 = PowerFxExpressionHelper.MonthEnd(powerFxVariable3);
				break;
			case RoundDateTimePeriod.Quarter:
				formulaExpression3 = PowerFxExpressionHelper.QuarterEnd(formulaExpression2, powerFxVariable2, powerFxVariable3);
				break;
			case RoundDateTimePeriod.Year:
				formulaExpression3 = PowerFxExpressionHelper.YearEnd(powerFxVariable3);
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
			PowerFxVariable powerFxVariable4 = (PowerFxVariable)PowerFxExpressionHelper.Variable(period.ToString().ToLower() + "End");
			PowerFxProgramTranslator.<TranslateRoundDateTimeUp>g__NewField|45_0(powerFxVariable4, formulaExpression5, ref CS$<>8__locals1);
			FormulaExpression formulaExpression6;
			if (!flag)
			{
				formulaExpression6 = PowerFxExpressionHelper.If(PowerFxExpressionHelper.Equal(formulaExpression2, powerFxVariable3), powerFxVariable3, powerFxVariable4);
			}
			else
			{
				FormulaExpression formulaExpression7 = powerFxVariable4;
				double? num = new double?((double)(-1));
				formulaExpression6 = PowerFxExpressionHelper.DateAdd(formulaExpression7, null, null, num, null, null, null, null);
			}
			FormulaExpression formulaExpression8 = formulaExpression6;
			return PowerFxExpressionHelper.With(CS$<>8__locals1.withRecords, formulaExpression8);
		}

		// Token: 0x0600D0A9 RID: 53417 RVA: 0x002C8140 File Offset: 0x002C6340
		private FormulaExpression TranslateContains(Contains contains)
		{
			FormulaExpression formulaExpression = PowerFxExpressionHelper.Variable(contains.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(contains.containsFindText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(contains.containsCount.Node, default(CancellationToken));
			return PowerFxExpressionHelper.Equal(PowerFxExpressionHelper.Minus(PowerFxExpressionHelper.Len(formulaExpression), PowerFxExpressionHelper.Len(PowerFxExpressionHelper.Substitute(formulaExpression, formulaExpression2, PowerFxExpressionHelper.StringLiteral(string.Empty)))), formulaExpression3);
		}

		// Token: 0x0600D0AA RID: 53418 RVA: 0x002C81CC File Offset: 0x002C63CC
		private FormulaExpression TranslateContainsMatch(ContainsMatch isMatch)
		{
			FormulaExpression formulaExpression = PowerFxExpressionHelper.Variable(isMatch.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(isMatch.containsMatchRegex.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(isMatch.matchCount.Node, default(CancellationToken));
			return PowerFxExpressionHelper.Equal(PowerFxExpressionHelper.CountRows(PowerFxExpressionHelper.MatchAll(formulaExpression, formulaExpression2)), formulaExpression3);
		}

		// Token: 0x0600D0AB RID: 53419 RVA: 0x002C8240 File Offset: 0x002C6440
		private static FormulaExpression TranslateEndsWithDigit(EndsWithDigit endsWithDigit)
		{
			return PowerFxExpressionHelper.IsNumeric(PowerFxExpressionHelper.Right(PowerFxExpressionHelper.Variable(endsWithDigit.columnName.Value), 1));
		}

		// Token: 0x0600D0AC RID: 53420 RVA: 0x002C826C File Offset: 0x002C646C
		private FormulaExpression TranslateIf(If ifNode)
		{
			FormulaExpression formulaExpression = this.Translate(ifNode.condition.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(ifNode.result1.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(ifNode.result2.Node, default(CancellationToken));
			return PowerFxExpressionHelper.If(formulaExpression, formulaExpression2, formulaExpression3);
		}

		// Token: 0x0600D0AD RID: 53421 RVA: 0x002C82E0 File Offset: 0x002C64E0
		private static FormulaExpression TranslateIsBlank(IsBlank isBlank)
		{
			return PowerFxExpressionHelper.IsBlank(PowerFxExpressionHelper.Variable(isBlank.columnName.Value));
		}

		// Token: 0x0600D0AE RID: 53422 RVA: 0x002C8308 File Offset: 0x002C6508
		private FormulaExpression TranslateIsMatch(IsMatch isMatch)
		{
			FormulaExpression formulaExpression = PowerFxExpressionHelper.Variable(isMatch.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(isMatch.isMatchRegex.Node, default(CancellationToken));
			return PowerFxExpressionHelper.And(PowerFxExpressionHelper.Not(PowerFxExpressionHelper.IsBlank(formulaExpression)), PowerFxExpressionHelper.IsMatch(formulaExpression, formulaExpression2));
		}

		// Token: 0x0600D0AF RID: 53423 RVA: 0x002C8364 File Offset: 0x002C6564
		private static FormulaExpression TranslateIsNotBlank(IsNotBlank isNotBlank)
		{
			return PowerFxExpressionHelper.Not(PowerFxExpressionHelper.IsBlank(PowerFxExpressionHelper.Variable(isNotBlank.columnName.Value)));
		}

		// Token: 0x0600D0B0 RID: 53424 RVA: 0x002C8390 File Offset: 0x002C6590
		private static FormulaExpression TranslateIsNumber(IsNumber isNumber)
		{
			return PowerFxExpressionHelper.IsNumeric(PowerFxExpressionHelper.Variable(isNumber.columnName.Value));
		}

		// Token: 0x0600D0B1 RID: 53425 RVA: 0x002C83B8 File Offset: 0x002C65B8
		private static FormulaExpression TranslateIsString(IsString isString)
		{
			return PowerFxExpressionHelper.Not(PowerFxExpressionHelper.IsNumeric(PowerFxExpressionHelper.Variable(isString.columnName.Value)));
		}

		// Token: 0x0600D0B2 RID: 53426 RVA: 0x002C83E3 File Offset: 0x002C65E3
		private static FormulaExpression TranslateNull()
		{
			return PowerFxExpressionHelper.Blank();
		}

		// Token: 0x0600D0B3 RID: 53427 RVA: 0x002C83EC File Offset: 0x002C65EC
		private FormulaExpression TranslateNumberEquals(NumberEquals numberEquals)
		{
			FormulaExpression formulaExpression = PowerFxExpressionHelper.Variable(numberEquals.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(numberEquals.numberEqualsValue.Node, default(CancellationToken));
			return PowerFxExpressionHelper.Equal(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D0B4 RID: 53428 RVA: 0x002C8434 File Offset: 0x002C6634
		private FormulaExpression TranslateNumberGreaterThan(NumberGreaterThan numberGreaterThan)
		{
			FormulaExpression formulaExpression = PowerFxExpressionHelper.Variable(numberGreaterThan.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(numberGreaterThan.numberGreaterThanValue.Node, default(CancellationToken));
			return PowerFxExpressionHelper.And(PowerFxExpressionHelper.IsNumeric(formulaExpression), PowerFxExpressionHelper.GreaterThan(PowerFxExpressionHelper.Value(formulaExpression, null), formulaExpression2));
		}

		// Token: 0x0600D0B5 RID: 53429 RVA: 0x002C8490 File Offset: 0x002C6690
		private FormulaExpression TranslateNumberLessThan(NumberLessThan numberLessThan)
		{
			FormulaExpression formulaExpression = PowerFxExpressionHelper.Variable(numberLessThan.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(numberLessThan.numberLessThanValue.Node, default(CancellationToken));
			return PowerFxExpressionHelper.And(PowerFxExpressionHelper.IsNumeric(formulaExpression), PowerFxExpressionHelper.LessThan(PowerFxExpressionHelper.Value(formulaExpression, null), formulaExpression2));
		}

		// Token: 0x0600D0B6 RID: 53430 RVA: 0x002C84EC File Offset: 0x002C66EC
		private FormulaExpression TranslateOr(Or or)
		{
			FormulaExpression formulaExpression = this.Translate(or.condition1.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(or.condition2.Node, default(CancellationToken));
			return PowerFxExpressionHelper.Or(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D0B7 RID: 53431 RVA: 0x002C853C File Offset: 0x002C673C
		private FormulaExpression TranslateStartsWith(StartsWith startsWith)
		{
			FormulaExpression formulaExpression = PowerFxExpressionHelper.Variable(startsWith.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(startsWith.startsWithFindText.Node, default(CancellationToken));
			return PowerFxExpressionHelper.Equal(PowerFxExpressionHelper.Left(formulaExpression, PowerFxExpressionHelper.Len(formulaExpression2)), formulaExpression2);
		}

		// Token: 0x0600D0B8 RID: 53432 RVA: 0x002C8590 File Offset: 0x002C6790
		private static FormulaExpression TranslateStartsWithDigit(StartsWithDigit startsWithDigit)
		{
			return PowerFxExpressionHelper.IsNumeric(PowerFxExpressionHelper.Left(PowerFxExpressionHelper.Variable(startsWithDigit.columnName.Value), 1));
		}

		// Token: 0x0600D0B9 RID: 53433 RVA: 0x002C85BC File Offset: 0x002C67BC
		private FormulaExpression TranslateStringEquals(StringEquals stringEquals)
		{
			FormulaExpression formulaExpression = PowerFxExpressionHelper.Variable(stringEquals.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(stringEquals.equalsText.Node, default(CancellationToken));
			return PowerFxExpressionHelper.Equal(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D0BA RID: 53434 RVA: 0x002C8604 File Offset: 0x002C6804
		private FormulaExpression TranslateAdd(Add add)
		{
			return PowerFxExpressionHelper.Plus(this.Translate(add.arithmeticLeft.Node, default(CancellationToken)), this.Translate(add.addRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D0BB RID: 53435 RVA: 0x002C8652 File Offset: 0x002C6852
		private static FormulaExpression TranslateAverage(Average average)
		{
			return PowerFxExpressionHelper.Average(PowerFxProgramTranslator.TranslateFromNumbers(average.fromNumbers));
		}

		// Token: 0x0600D0BC RID: 53436 RVA: 0x002C8668 File Offset: 0x002C6868
		private FormulaExpression TranslateDivide(Divide divide)
		{
			return PowerFxExpressionHelper.Divide(this.Translate(divide.arithmeticLeft.Node, default(CancellationToken)), this.Translate(divide.divideRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D0BD RID: 53437 RVA: 0x002C86B8 File Offset: 0x002C68B8
		private static IEnumerable<FormulaExpression> TranslateFromNumbers(fromNumbers fromNumbers)
		{
			LiteralNode literalNode = fromNumbers.Node.Children[1] as LiteralNode;
			string[] array = ((literalNode != null) ? literalNode.Value : null) as string[];
			if (array != null)
			{
				IEnumerable<string> enumerable = array;
				Func<string, FormulaExpression> func;
				if ((func = PowerFxProgramTranslator.<>O.<0>__Variable) == null)
				{
					func = (PowerFxProgramTranslator.<>O.<0>__Variable = new Func<string, FormulaExpression>(PowerFxExpressionHelper.Variable));
				}
				return enumerable.Select(func);
			}
			return null;
		}

		// Token: 0x0600D0BE RID: 53438 RVA: 0x002C8710 File Offset: 0x002C6910
		private FormulaExpression TranslateMultiply(Multiply multiply)
		{
			return PowerFxExpressionHelper.Multiply(this.Translate(multiply.arithmeticLeft.Node, default(CancellationToken)), this.Translate(multiply.multiplyRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D0BF RID: 53439 RVA: 0x002C875E File Offset: 0x002C695E
		private static FormulaExpression TranslateProduct(Product product)
		{
			return PowerFxExpressionHelper.Product(PowerFxProgramTranslator.TranslateFromNumbers(product.fromNumbers));
		}

		// Token: 0x0600D0C0 RID: 53440 RVA: 0x002C8774 File Offset: 0x002C6974
		private FormulaExpression TranslateSubtract(Subtract subtract)
		{
			return PowerFxExpressionHelper.Minus(this.Translate(subtract.arithmeticLeft.Node, default(CancellationToken)), this.Translate(subtract.subtractRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600D0C1 RID: 53441 RVA: 0x002C87C2 File Offset: 0x002C69C2
		private static FormulaExpression TranslateSum(Sum sum)
		{
			return PowerFxExpressionHelper.Sum(PowerFxProgramTranslator.TranslateFromNumbers(sum.fromNumbers));
		}

		// Token: 0x0600D0C2 RID: 53442 RVA: 0x002C87D8 File Offset: 0x002C69D8
		private static FormulaExpression TranslateFromDateTime(FromDateTime dateTime)
		{
			return PowerFxExpressionHelper.Variable(dateTime.columnName.Value);
		}

		// Token: 0x0600D0C3 RID: 53443 RVA: 0x002C87FC File Offset: 0x002C69FC
		private static FormulaExpression TranslateFromDateTimePart(FromDateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = PowerFxExpressionHelper.Variable(dateTimePart.columnName.Value);
			DateTimePartKind value = dateTimePart.fromDateTimePartKind.Value;
			FormulaExpression formulaExpression2;
			if (value != DateTimePartKind.Month)
			{
				if (value == DateTimePartKind.Year)
				{
					formulaExpression2 = PowerFxExpressionHelper.Date(formulaExpression, 1, 1);
				}
				else
				{
					formulaExpression2 = null;
				}
			}
			else
			{
				formulaExpression2 = PowerFxExpressionHelper.Date(2000, formulaExpression, 1);
			}
			return formulaExpression2;
		}

		// Token: 0x0600D0C4 RID: 53444 RVA: 0x002C8858 File Offset: 0x002C6A58
		private static FormulaExpression TranslateFromNumber(FromNumber fromNumber)
		{
			return PowerFxExpressionHelper.Variable(fromNumber.columnName.Value);
		}

		// Token: 0x0600D0C5 RID: 53445 RVA: 0x002C887C File Offset: 0x002C6A7C
		private static FormulaExpression TranslateFromStr(FromStr input)
		{
			return PowerFxExpressionHelper.Variable(input.columnName.Value);
		}

		// Token: 0x0600D0C6 RID: 53446 RVA: 0x002C88A0 File Offset: 0x002C6AA0
		private FormulaExpression TranslateToDateTime(ToDateTime subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D0C7 RID: 53447 RVA: 0x002C88CC File Offset: 0x002C6ACC
		private FormulaExpression TranslateToDecimal(ToDecimal subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D0C8 RID: 53448 RVA: 0x002C88F8 File Offset: 0x002C6AF8
		private FormulaExpression TranslateToDouble(ToDouble subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D0C9 RID: 53449 RVA: 0x002C8924 File Offset: 0x002C6B24
		private FormulaExpression TranslateToInt(ToInt subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D0CA RID: 53450 RVA: 0x002C8950 File Offset: 0x002C6B50
		private FormulaExpression TranslateToStr(ToStr subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D0CB RID: 53451 RVA: 0x002C897C File Offset: 0x002C6B7C
		private static FormulaExpression ResolveMatch(FormulaExpression subjectExp, FormulaExpression patternExp, int instance)
		{
			if (instance == 0)
			{
				return null;
			}
			PowerFxStringLiteral powerFxStringLiteral = patternExp as PowerFxStringLiteral;
			if (powerFxStringLiteral != null)
			{
				if (instance == 1)
				{
					return PowerFxExpressionHelper.Find(powerFxStringLiteral, subjectExp);
				}
				if (powerFxStringLiteral.Value.Length == 1 && !char.IsLetterOrDigit(powerFxStringLiteral.Value[0]))
				{
					patternExp = PowerFxExpressionHelper.RawStringLiteral(Regex.Escape((powerFxStringLiteral.Value == "\"") ? "\"\"" : powerFxStringLiteral.Value));
				}
			}
			FormulaExpression formulaExpression;
			if (instance >= -1)
			{
				if (instance != -1)
				{
					if (instance == 1)
					{
						formulaExpression = PowerFxExpressionHelper.Match(subjectExp, patternExp);
					}
					else
					{
						formulaExpression = PowerFxExpressionHelper.Nth(PowerFxExpressionHelper.MatchAll(subjectExp, patternExp), PowerFxExpressionHelper.NumberLiteral(instance));
					}
				}
				else
				{
					formulaExpression = PowerFxExpressionHelper.Last(PowerFxExpressionHelper.MatchAll(subjectExp, patternExp));
				}
			}
			else
			{
				formulaExpression = PowerFxExpressionHelper.NthFromEnd(PowerFxExpressionHelper.MatchAll(subjectExp, patternExp), PowerFxExpressionHelper.NumberLiteral(-instance));
			}
			return formulaExpression;
		}

		// Token: 0x0600D0CC RID: 53452 RVA: 0x002C8A40 File Offset: 0x002C6C40
		private static FormulaExpression ResolveMatchStart(FormulaExpression subjectExp, FormulaExpression patternExp, int instance)
		{
			FormulaExpression formulaExpression = PowerFxProgramTranslator.ResolveMatch(subjectExp, patternExp, instance);
			if (!(formulaExpression is PowerFxFind))
			{
				return PowerFxExpressionHelper.Dot(formulaExpression, "StartMatch");
			}
			return formulaExpression;
		}

		// Token: 0x0600D0CE RID: 53454 RVA: 0x002C8B84 File Offset: 0x002C6D84
		[CompilerGenerated]
		internal static void <TranslateDateTimePart>g__NewField|39_0(FormulaVariable key, FormulaExpression exp, ref PowerFxProgramTranslator.<>c__DisplayClass39_0 A_2)
		{
			Dictionary<string, FormulaExpression> dictionary = new Dictionary<string, FormulaExpression>();
			A_2.withRecords.Add(dictionary);
			dictionary.Add(key.Name, exp);
		}

		// Token: 0x0600D0CF RID: 53455 RVA: 0x002C8BB0 File Offset: 0x002C6DB0
		[CompilerGenerated]
		internal static void <TranslateRoundDateTimeDown>g__NewField|43_0(FormulaVariable key, FormulaExpression exp, ref PowerFxProgramTranslator.<>c__DisplayClass43_0 A_2)
		{
			Dictionary<string, FormulaExpression> dictionary = new Dictionary<string, FormulaExpression>();
			A_2.withRecords.Add(dictionary);
			dictionary.Add(key.Name, exp);
		}

		// Token: 0x0600D0D0 RID: 53456 RVA: 0x002C8BDC File Offset: 0x002C6DDC
		[CompilerGenerated]
		internal static void <TranslateRoundDateTimeNearest>g__NewField|44_0(FormulaVariable key, FormulaExpression exp, ref PowerFxProgramTranslator.<>c__DisplayClass44_0 A_2)
		{
			Dictionary<string, FormulaExpression> dictionary = new Dictionary<string, FormulaExpression>();
			A_2.withRecords.Add(dictionary);
			dictionary.Add(key.Name, exp);
		}

		// Token: 0x0600D0D1 RID: 53457 RVA: 0x002C8C08 File Offset: 0x002C6E08
		[CompilerGenerated]
		internal static void <TranslateRoundDateTimeUp>g__NewField|45_0(FormulaVariable key, FormulaExpression exp, ref PowerFxProgramTranslator.<>c__DisplayClass45_0 A_2)
		{
			Dictionary<string, FormulaExpression> dictionary = new Dictionary<string, FormulaExpression>();
			A_2.withRecords.Add(dictionary);
			dictionary.Add(key.Name, exp);
		}

		// Token: 0x04005101 RID: 20737
		private bool _cancelled;

		// Token: 0x04005102 RID: 20738
		private string _currentInputColumnName;

		// Token: 0x04005103 RID: 20739
		private readonly IPowerFxTranslationOptions _options;

		// Token: 0x04005104 RID: 20740
		private static readonly Regex[] _validDateFormatRegexes = new Regex[]
		{
			"M(M)?.d(d)?.(yyyy|yy)".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant),
			"(yyyy|yy).M(M)?.d(d)?".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant),
			"d(d)?.(M|MM|MMM)?.(yyyy|yy)".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant),
			"d(d)? MMM (yyyy|yy)".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant),
			"MMMM d(d)?, (yyyy|yy)".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant),
			"MMMM (yyyy|yy)".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant)
		};

		// Token: 0x04005105 RID: 20741
		private static readonly Regex[] _validDateTimeFormatRegexes = new Regex[]
		{
			"yyyy\\'?\\-\\'?MM\\'?\\-\\'?dd\\'?T\\'?HH\\'?\\:\\'?mm\\'?:\\'?ss".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant),
			"M(M)?.d(d)?.(yyyy|yy) (HH|H|hh|h)\\:mm(\\:ss)?( tt)?".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant),
			"(yyyy|yy).M(M)?.d(d)? (HH|H|hh|h)?\\:mm(\\:ss)?( tt)?".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant),
			"d(d)?.M(M)?.(yyyy|yy) (HH|H|hh|h)?\\:mm(\\:ss)?( tt)?".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant),
			"d(d)? MMM (yyyy|yy) (HH|H|hh|h)\\:mm(\\:ss)?( tt)?".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant),
			"MMMM d(d)?, (yyyy|yy) (HH|H|hh|h)\\:mm(\\:ss)?( tt)?".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant)
		};

		// Token: 0x04005106 RID: 20742
		private static readonly Regex[] _validTimeFormatRegexes = new Regex[] { "(HH|H|hh|h)\\:mm(\\:ss)?( tt)?".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant) };

		// Token: 0x020018F8 RID: 6392
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005107 RID: 20743
			public static Func<string, FormulaExpression> <0>__Variable;
		}
	}
}

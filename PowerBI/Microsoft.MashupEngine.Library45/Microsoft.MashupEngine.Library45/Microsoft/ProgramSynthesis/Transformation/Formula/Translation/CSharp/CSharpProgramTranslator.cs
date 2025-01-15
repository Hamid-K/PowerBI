using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Constraints;
using Microsoft.ProgramSynthesis.Transformation.Formula.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Exceptions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200197B RID: 6523
	internal class CSharpProgramTranslator : ProgramTranslatorBase
	{
		// Token: 0x0600D540 RID: 54592 RVA: 0x002D4E8C File Offset: 0x002D308C
		private CSharpProgramTranslator(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, ICSharpTranslationOptions options, ILogger logger)
			: base(program, examples, inputs, logger)
		{
			this._options = options ?? new CSharpTranslationConstraint();
		}

		// Token: 0x0600D541 RID: 54593 RVA: 0x002D4EE9 File Offset: 0x002D30E9
		public static CSharpProgram Translate(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, ICSharpTranslationOptions options, ILogger logger = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new CSharpProgramTranslator(program, examples, inputs, options, logger).Translate(cancellationToken) as CSharpProgram;
		}

		// Token: 0x0600D542 RID: 54594 RVA: 0x002D4F04 File Offset: 0x002D3104
		protected override FormulaExpression Translate(CancellationToken cancellationToken = default(CancellationToken))
		{
			FormulaExpression formulaExpression = base.Translate(cancellationToken);
			if (formulaExpression == null)
			{
				return null;
			}
			this._statements.Add(formulaExpression);
			IEnumerable<CSharpMethodParameter> enumerable = base.InputColumnDetails.Select((ColumnDetail d) => CSharpExpressionHelper.MethodParameter(d.Name, base.ResolveInputType(d.Name), false, null, false));
			CSharpMethod csharpMethod = CSharpExpressionHelper.Method(this._options.MethodName, base.OutputColumnDetail.Type, enumerable, this._statements, "public");
			csharpMethod = CSharpExpressionOptimizer.Optimize(csharpMethod, this._options) as CSharpMethod;
			if (csharpMethod == null)
			{
				return null;
			}
			csharpMethod = CSharpProgramTranslator.ResolveReturn(csharpMethod);
			List<CSharpMethod> list = new List<CSharpMethod> { csharpMethod };
			if (this._includeAllIndexesOfMethod)
			{
				list.Add(this.AllIndexesOfMethod());
			}
			if (this._includeSliceMethod)
			{
				list.Add(CSharpProgramTranslator.SliceMethod());
			}
			if (this._includeSliceBetweenMethod)
			{
				list.Add(CSharpProgramTranslator.SliceBetweenMethod());
			}
			if (this._includeParseDateTimeMethod)
			{
				list.Add(this.ParseDateTimeMethod());
			}
			if (this._includeParseNumberMethod)
			{
				list.Add(this.ParseNumberMethod());
			}
			if (this._includeToTitleCaseMethod)
			{
				list.Add(this.ToTitleCaseMethod());
			}
			if (this._includeRoundUpMethod)
			{
				list.Add(CSharpProgramTranslator.RoundUpMethod());
			}
			if (this._includeRoundDownMethod)
			{
				list.Add(CSharpProgramTranslator.RoundDownMethod());
			}
			if (this._includeRoundNearestMethod)
			{
				list.Add(CSharpProgramTranslator.RoundNearestMethod());
			}
			IEnumerable<CSharpUsing> enumerable2 = from u in this._usings
				orderby u
				select new CSharpUsing(u);
			CSharpProgram csharpProgram;
			switch (this._options.Style)
			{
			case CSharpCodeStyle.Namespace:
				csharpProgram = CSharpExpressionHelper.Program(enumerable2, CSharpExpressionHelper.Namespace(this._options.NamespaceName, CSharpExpressionHelper.Class(this._options.ClassName, list).Yield<CSharpClass>()), null, null, null);
				break;
			case CSharpCodeStyle.Class:
				csharpProgram = CSharpExpressionHelper.Program(enumerable2, null, CSharpExpressionHelper.Class(this._options.ClassName, list).Yield<CSharpClass>(), null, null);
				break;
			case CSharpCodeStyle.Script:
				csharpProgram = CSharpExpressionHelper.Program(enumerable2, null, null, list, null);
				break;
			default:
				throw new Exception(string.Format("Invalid CSharpCodeStyle {0}", this._options.Style));
			}
			return csharpProgram;
		}

		// Token: 0x0600D543 RID: 54595 RVA: 0x002D5150 File Offset: 0x002D3350
		protected override FormulaExpression Translate(ProgramNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			FormulaExpression formulaExpression;
			try
			{
				cancellationToken.ThrowIfCancellationRequested();
				LetX letX;
				Abs abs;
				Str str;
				Number number;
				Date date;
				Find find;
				Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match match;
				MatchEnd matchEnd;
				MatchFull matchFull;
				ParseNumber parseNumber;
				ParseDateTime parseDateTime;
				Split split;
				Slice slice;
				SlicePrefixAbs slicePrefixAbs;
				SlicePrefix slicePrefix;
				SliceSuffix sliceSuffix;
				SliceBetween sliceBetween;
				Replace replace;
				Concat concat;
				FormatNumber formatNumber;
				RoundNumber roundNumber;
				FormatDateTime formatDateTime;
				FromDateTimePart fromDateTimePart;
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
				else if (node.Is(out letX))
				{
					formulaExpression = this.TranslateLetX(letX);
				}
				else if (node.Is(out abs))
				{
					formulaExpression = CSharpProgramTranslator.TranslateAbs(abs);
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
					formulaExpression = CSharpProgramTranslator.TranslateArithmeticRightNumber(node);
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
				else if (node.Is(out parseNumber))
				{
					formulaExpression = this.TranslateParseNumber(parseNumber);
				}
				else if (node.Is(out parseDateTime))
				{
					formulaExpression = this.TranslateParseDateTime(parseDateTime);
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
				else if (node.IsTrim())
				{
					formulaExpression = this.TranslateTrim(node);
				}
				else if (node.Is(out concat))
				{
					formulaExpression = this.TranslateConcat(concat);
				}
				else if (node.Is(out formatNumber))
				{
					formulaExpression = this.TranslateFormatNumber(formatNumber);
				}
				else if (node.Is(out roundNumber))
				{
					formulaExpression = this.TranslateRoundNumber(roundNumber);
				}
				else if (node.Is(out formatDateTime))
				{
					formulaExpression = this.TranslateFormatDateTime(formatDateTime);
				}
				else if (node.Is(out fromDateTimePart))
				{
					formulaExpression = CSharpProgramTranslator.TranslateFromDateTimePart(fromDateTimePart);
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
					formulaExpression = this.TranslateSum(sum);
				}
				else if (node.Is(out product))
				{
					formulaExpression = this.TranslateProduct(product);
				}
				else if (node.Is(out average))
				{
					formulaExpression = this.TranslateAverage(average);
				}
				else if (node.Is(out fromStr))
				{
					formulaExpression = this.TranslateFromStr(fromStr);
				}
				else if (node.Is(out fromDateTime))
				{
					formulaExpression = this.TranslateFromDateTime(fromDateTime);
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
							goto IL_067B;
						}
						LiteralNode literalNode = node as LiteralNode;
						if (literalNode != null)
						{
							formulaExpression = CSharpProgramTranslator.TranslateLiteral(literalNode);
							goto IL_067B;
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
						formulaExpression = CSharpProgramTranslator.TranslateIsString(isString);
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
						formulaExpression = CSharpProgramTranslator.TranslateStartsWithDigit(startsWithDigit);
					}
					else if (node.Is(out endsWithDigit))
					{
						formulaExpression = CSharpProgramTranslator.TranslateEndsWithDigit(endsWithDigit);
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
						formulaExpression = CSharpProgramTranslator.TranslateIsNumber(isNumber);
					}
					else if (node.Is(out numberGreaterThan))
					{
						formulaExpression = CSharpProgramTranslator.TranslateNumberGreaterThan(numberGreaterThan);
					}
					else if (node.Is(out numberLessThan))
					{
						formulaExpression = CSharpProgramTranslator.TranslateNumberLessThan(numberLessThan);
					}
					else if (node.Is(out numberEquals))
					{
						formulaExpression = CSharpProgramTranslator.TranslateNumberEquals(numberEquals);
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
						formulaExpression = CSharpProgramTranslator.TranslateNull();
					}
				}
				else
				{
					formulaExpression = this.TranslateVariableNode();
				}
				IL_067B:
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

		// Token: 0x0600D544 RID: 54596 RVA: 0x002D584C File Offset: 0x002D3A4C
		private static FormulaExpression TranslateAbs(Abs abs)
		{
			int value = abs.absPos.Value;
			return CSharpExpressionHelper.NumberLiteral((value > 0) ? (value - 1) : value);
		}

		// Token: 0x0600D545 RID: 54597 RVA: 0x002D5878 File Offset: 0x002D3A78
		private FormulaExpression TranslateConcat(Concat concat)
		{
			FormulaExpression formulaExpression = this.Translate(concat.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(concat.Node.Children[1], default(CancellationToken));
			return CSharpExpressionHelper.Concat(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D546 RID: 54598 RVA: 0x002D58C8 File Offset: 0x002D3AC8
		private FormulaExpression TranslateConversionRule(ProgramNode node)
		{
			return this.Translate(node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600D547 RID: 54599 RVA: 0x002D58EC File Offset: 0x002D3AEC
		private FormulaExpression TranslateDate(Date date)
		{
			return this.Translate(date.constDt.Node, default(CancellationToken));
		}

		// Token: 0x0600D548 RID: 54600 RVA: 0x002D5918 File Offset: 0x002D3B18
		private FormulaExpression TranslateFind(Find find)
		{
			FormulaExpression formulaExpression = this.Translate(find.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(find.findDelimiter.Node, default(CancellationToken));
			int num = find.findInstance.Value;
			int value = find.findOffset.Value;
			if (num == 0)
			{
				return null;
			}
			FormulaExpression formulaExpression3 = CSharpExpressionHelper.NumberLiteral(value);
			if (num == 1)
			{
				return CSharpExpressionHelper.Plus(CSharpExpressionHelper.IndexOf(formulaExpression, formulaExpression2), formulaExpression3);
			}
			if (num == -1)
			{
				return CSharpExpressionHelper.Plus(CSharpExpressionHelper.LastIndexOf(formulaExpression, formulaExpression2), formulaExpression3);
			}
			if (num > 0)
			{
				num--;
			}
			this._includeAllIndexesOfMethod = true;
			FormulaExpression formulaExpression4 = CSharpExpressionHelper.Plus(CSharpExpressionHelper.Index<int>(CSharpExpressionHelper.AllIndexesOf(formulaExpression, formulaExpression2, false), CSharpExpressionHelper.IntLiteral(num)), formulaExpression3);
			return this.AddAutoVariable<int>("index", formulaExpression4, true);
		}

		// Token: 0x0600D549 RID: 54601 RVA: 0x002D59F8 File Offset: 0x002D3BF8
		private FormulaExpression TranslateLength(Length length)
		{
			return CSharpExpressionHelper.Length(this.Translate(length.fromStr.Node, default(CancellationToken)));
		}

		// Token: 0x0600D54A RID: 54602 RVA: 0x002D5A28 File Offset: 0x002D3C28
		private FormulaExpression TranslateLetX(LetX letX)
		{
			this._currentInputExpression = this.Translate(letX.fromStrTrim.Node, default(CancellationToken));
			return this.Translate(letX.substring.Node, default(CancellationToken));
		}

		// Token: 0x0600D54B RID: 54603 RVA: 0x002D5A78 File Offset: 0x002D3C78
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
					formulaExpression = CSharpExpressionHelper.NumberLiteral(num);
				}
				else if (value is double)
				{
					double num2 = (double)value;
					formulaExpression = CSharpExpressionHelper.NumberLiteral(num2);
				}
				else if (value is decimal)
				{
					decimal num3 = (decimal)value;
					formulaExpression = CSharpExpressionHelper.NumberLiteral(num3);
				}
				else if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					formulaExpression = CSharpExpressionHelper.DateTimeLiteral(dateTime);
				}
				else
				{
					MatchDescriptor matchDescriptor = value as MatchDescriptor;
					if (matchDescriptor == null)
					{
						RegularExpression regularExpression = value as RegularExpression;
						if (regularExpression == null)
						{
							Regex regex = value as Regex;
							if (regex == null)
							{
								formulaExpression = null;
							}
							else
							{
								formulaExpression = CSharpExpressionHelper.RegexLiteral(regex.ToString());
							}
						}
						else
						{
							formulaExpression = CSharpExpressionHelper.RegexLiteral(regularExpression.Regex.ToString());
						}
					}
					else
					{
						formulaExpression = CSharpExpressionHelper.RegexLiteral(matchDescriptor.Regex.ToString());
					}
				}
			}
			else
			{
				formulaExpression = CSharpExpressionHelper.StringLiteral(text);
			}
			return formulaExpression;
		}

		// Token: 0x0600D54C RID: 54604 RVA: 0x002D5B80 File Offset: 0x002D3D80
		private FormulaExpression TranslateLowerCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return CSharpExpressionHelper.Lower(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D54D RID: 54605 RVA: 0x002D5BB8 File Offset: 0x002D3DB8
		private FormulaExpression TranslateMatch(Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match match)
		{
			FormulaExpression formulaExpression = this.Translate(match.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(match.matchDesc.Node, default(CancellationToken));
			int value = match.matchInstance.Value;
			string text = "index{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression3 = CSharpExpressionHelper.Variable<int>(string.Format(text, varCount));
			FormulaExpression formulaExpression4 = CSharpExpressionHelper.NumberLiteral((value > 0) ? (value - 1) : value);
			FormulaExpression formulaExpression5 = CSharpExpressionHelper.Dot((value == 1) ? CSharpExpressionHelper.Match(formulaExpression, formulaExpression2) : CSharpExpressionHelper.Index(CSharpExpressionHelper.Matches(formulaExpression, formulaExpression2), formulaExpression4), CSharpExpressionHelper.Variable<int>("Index"), false);
			this.AddUsingCollections();
			this.AddUsingRegex();
			if (value != 1)
			{
				this.AddUsingLinq();
			}
			this._statements.Add(CSharpExpressionHelper.Var(CSharpExpressionHelper.Assign(formulaExpression3, formulaExpression5)));
			return formulaExpression3;
		}

		// Token: 0x0600D54E RID: 54606 RVA: 0x002D5CB0 File Offset: 0x002D3EB0
		private FormulaExpression TranslateMatchEnd(MatchEnd matchEnd)
		{
			FormulaExpression formulaExpression = this.Translate(matchEnd.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(matchEnd.matchDesc.Node, default(CancellationToken));
			int value = matchEnd.matchInstance.Value;
			string text = "index{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression3 = CSharpExpressionHelper.Variable<int>(string.Format(text, varCount));
			FormulaExpression formulaExpression4 = CSharpExpressionHelper.NumberLiteral((value > 0) ? (value - 1) : value);
			FormulaExpression formulaExpression5 = ((value == 1) ? CSharpExpressionHelper.Match(formulaExpression, formulaExpression2) : CSharpExpressionHelper.Index(CSharpExpressionHelper.Matches(formulaExpression, formulaExpression2), formulaExpression4));
			FormulaExpression formulaExpression6 = CSharpExpressionHelper.Plus(CSharpExpressionHelper.StrictDot<int>(formulaExpression5, "Index"), CSharpExpressionHelper.Length(CSharpExpressionHelper.StrictDot<string>(formulaExpression5, "Value")));
			this.AddUsingRegex();
			if (value != 1)
			{
				this.AddUsingLinq();
			}
			this._statements.Add(CSharpExpressionHelper.Var(CSharpExpressionHelper.Assign(formulaExpression3, formulaExpression6)));
			return formulaExpression3;
		}

		// Token: 0x0600D54F RID: 54607 RVA: 0x002D5DB4 File Offset: 0x002D3FB4
		private FormulaExpression TranslateMatchFull(MatchFull matchFull)
		{
			FormulaExpression formulaExpression = this.Translate(matchFull.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(matchFull.matchDesc.Node, default(CancellationToken));
			if (formulaExpression == null || formulaExpression2 == null)
			{
				return null;
			}
			int value = matchFull.matchInstance.Value;
			string text = "match{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression3 = CSharpExpressionHelper.Variable<string>(string.Format(text, varCount));
			FormulaExpression formulaExpression4 = CSharpExpressionHelper.NumberLiteral((value > 0) ? (value - 1) : value);
			FormulaExpression formulaExpression5 = CSharpExpressionHelper.StrictDot<string>((value == 1) ? CSharpExpressionHelper.Match(formulaExpression, formulaExpression2) : CSharpExpressionHelper.Index(CSharpExpressionHelper.Matches(formulaExpression, formulaExpression2), formulaExpression4), "Value");
			this.AddUsingRegex();
			if (value != 1)
			{
				this.AddUsingLinq();
			}
			this._statements.Add(CSharpExpressionHelper.Var(CSharpExpressionHelper.Assign(formulaExpression3, formulaExpression5)));
			return formulaExpression3;
		}

		// Token: 0x0600D550 RID: 54608 RVA: 0x002D5EB4 File Offset: 0x002D40B4
		private FormulaExpression TranslateProperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			this._includeToTitleCaseMethod = true;
			if (!(formulaExpression == null))
			{
				return CSharpExpressionHelper.ToTitleCase(formulaExpression, CSharpExpressionHelper.CultureInfo("en-US"));
			}
			return null;
		}

		// Token: 0x0600D551 RID: 54609 RVA: 0x002D5EFC File Offset: 0x002D40FC
		private FormulaExpression TranslateReplace(Replace replace)
		{
			FormulaExpression formulaExpression = this.Translate(replace.fromStr.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(replace.replaceFindText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(replace.replaceText.Node, default(CancellationToken));
			return CSharpExpressionHelper.Replace(formulaExpression, formulaExpression2, formulaExpression3);
		}

		// Token: 0x0600D552 RID: 54610 RVA: 0x002D5F70 File Offset: 0x002D4170
		private FormulaExpression TranslateSlice(Slice slice)
		{
			FormulaExpression formulaExpression = this.Translate(slice.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slice.pos1.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(slice.pos2.Node, default(CancellationToken));
			this._includeSliceMethod = true;
			return CSharpExpressionHelper.Slice(formulaExpression, formulaExpression2, formulaExpression3);
		}

		// Token: 0x0600D553 RID: 54611 RVA: 0x002D5FEC File Offset: 0x002D41EC
		private FormulaExpression TranslateSliceBetween(SliceBetween sliceBetween)
		{
			FormulaExpression formulaExpression = this.Translate(sliceBetween.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(sliceBetween.sliceBetweenStartText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(sliceBetween.sliceBetweenEndText.Node, default(CancellationToken));
			this._includeSliceBetweenMethod = true;
			return CSharpExpressionHelper.SliceBetween(formulaExpression, formulaExpression2, formulaExpression3);
		}

		// Token: 0x0600D554 RID: 54612 RVA: 0x002D6068 File Offset: 0x002D4268
		private FormulaExpression TranslateSlicePrefix(SlicePrefix slicePrefix)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slicePrefix.pos.Node, default(CancellationToken));
			this._includeSliceMethod = true;
			return CSharpExpressionHelper.Slice(formulaExpression, CSharpExpressionHelper.NumberLiteral(0), formulaExpression2);
		}

		// Token: 0x0600D555 RID: 54613 RVA: 0x002D60C8 File Offset: 0x002D42C8
		private FormulaExpression TranslateSlicePrefixAbs(SlicePrefixAbs slicePrefixAbs)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefixAbs.x.Node, default(CancellationToken));
			this._includeSliceMethod = true;
			return CSharpExpressionHelper.Slice(formulaExpression, CSharpExpressionHelper.NumberLiteral(0), CSharpExpressionHelper.NumberLiteral(slicePrefixAbs.slicePrefixAbsPos.Value - 1));
		}

		// Token: 0x0600D556 RID: 54614 RVA: 0x002D611C File Offset: 0x002D431C
		private FormulaExpression TranslateSliceSuffix(SliceSuffix sliceSuffix)
		{
			FormulaExpression formulaExpression = this.Translate(sliceSuffix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(sliceSuffix.pos.Node, default(CancellationToken));
			this._includeSliceMethod = true;
			return CSharpExpressionHelper.Slice(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D557 RID: 54615 RVA: 0x002D6174 File Offset: 0x002D4374
		private FormulaExpression TranslateSplit(Split split)
		{
			FormulaExpression formulaExpression = this.Translate(split.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(split.splitDelimiter.Node, default(CancellationToken));
			int num = split.splitInstance.Value;
			CSharpStringLiteral csharpStringLiteral = formulaExpression2 as CSharpStringLiteral;
			if (csharpStringLiteral != null)
			{
				if (csharpStringLiteral.Value.Length > 1)
				{
					return null;
				}
				formulaExpression2 = CSharpExpressionHelper.CharLiteral(csharpStringLiteral.Value[0]);
			}
			if (num > 0)
			{
				num--;
			}
			return CSharpExpressionHelper.Index(CSharpExpressionHelper.Split(formulaExpression, formulaExpression2), CSharpExpressionHelper.NumberLiteral(num));
		}

		// Token: 0x0600D558 RID: 54616 RVA: 0x002D621C File Offset: 0x002D441C
		private FormulaExpression TranslateStr(Str str)
		{
			return this.Translate(str.constStr.Node, default(CancellationToken));
		}

		// Token: 0x0600D559 RID: 54617 RVA: 0x002D6248 File Offset: 0x002D4448
		private FormulaExpression TranslateTrim(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			return this.AddAutoVariable<string>("trim", CSharpExpressionHelper.Trim(formulaExpression), true);
		}

		// Token: 0x0600D55A RID: 54618 RVA: 0x002D6280 File Offset: 0x002D4480
		private FormulaExpression TranslateUpperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return CSharpExpressionHelper.Upper(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600D55B RID: 54619 RVA: 0x002D62B6 File Offset: 0x002D44B6
		private FormulaExpression TranslateVariableNode()
		{
			return this._currentInputExpression;
		}

		// Token: 0x0600D55C RID: 54620 RVA: 0x002D62C0 File Offset: 0x002D44C0
		private static FormulaExpression TranslateArithmeticRightNumber(ProgramNode node)
		{
			decimal num;
			if (!node.IsArithmeticRightNumber(out num))
			{
				return null;
			}
			return CSharpExpressionHelper.NumberLiteral(num);
		}

		// Token: 0x0600D55D RID: 54621 RVA: 0x002D62E0 File Offset: 0x002D44E0
		private FormulaExpression TranslateFormatNumber(FormatNumber formatNumber)
		{
			FormulaExpression formulaExpression = this.Translate(formatNumber.Node.Children[0], default(CancellationToken));
			FormatNumberDescriptor value = formatNumber.numberFormatDesc.Value;
			this.AddUsingGlobalization();
			return CSharpExpressionHelper.ToString(formulaExpression, CSharpExpressionHelper.StringLiteral(value.ToFormatString()), CSharpExpressionHelper.CultureInfo(value.Culture));
		}

		// Token: 0x0600D55E RID: 54622 RVA: 0x002D633C File Offset: 0x002D453C
		private FormulaExpression TranslateNumber(Number number)
		{
			return this.Translate(number.constNum.Node, default(CancellationToken));
		}

		// Token: 0x0600D55F RID: 54623 RVA: 0x002D6368 File Offset: 0x002D4568
		private FormulaExpression TranslateParseNumber(ParseNumber parseNumber)
		{
			FormulaExpression formulaExpression = this.Translate(parseNumber.Node.Children[0], default(CancellationToken));
			if (formulaExpression == null)
			{
				return null;
			}
			this._includeParseNumberMethod = true;
			string text = "number{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression2 = CSharpExpressionHelper.Variable(string.Format(text, varCount));
			FormulaExpression formulaExpression3 = CSharpExpressionHelper.ParseNumber(formulaExpression, CSharpExpressionHelper.CultureInfo(parseNumber.locale.Value));
			this._statements.Add(CSharpExpressionHelper.Var(CSharpExpressionHelper.Assign(formulaExpression2, formulaExpression3)));
			this._statements.Add(CSharpExpressionHelper.IfNullReturnNull(formulaExpression2));
			return CSharpExpressionHelper.Dot<decimal>(formulaExpression2, "Value");
		}

		// Token: 0x0600D560 RID: 54624 RVA: 0x002D6420 File Offset: 0x002D4620
		private FormulaExpression TranslateRoundNumber(RoundNumber roundNumber)
		{
			FormulaExpression formulaExpression = this.Translate(roundNumber.inumber.Node, default(CancellationToken));
			if (formulaExpression == null)
			{
				return null;
			}
			RoundingMode mode = roundNumber.numberRoundDesc.Value.Mode;
			FormulaExpression formulaExpression2 = CSharpExpressionHelper.ToDecimal(CSharpExpressionHelper.NumberLiteral(roundNumber.numberRoundDesc.Value.Delta));
			formulaExpression = CSharpProgramTranslator.ResolveType<decimal>(formulaExpression);
			this._includeRoundNearestMethod = mode == RoundingMode.Nearest;
			this._includeRoundUpMethod = mode == RoundingMode.Up;
			this._includeRoundDownMethod = mode == RoundingMode.Down;
			FormulaExpression formulaExpression3;
			switch (mode)
			{
			case RoundingMode.Nearest:
				formulaExpression3 = CSharpExpressionHelper.RoundNearest(formulaExpression, formulaExpression2);
				break;
			case RoundingMode.Down:
				formulaExpression3 = CSharpExpressionHelper.RoundDown(formulaExpression, formulaExpression2);
				break;
			case RoundingMode.Up:
				formulaExpression3 = CSharpExpressionHelper.RoundUp(formulaExpression, formulaExpression2);
				break;
			default:
				formulaExpression3 = null;
				break;
			}
			return formulaExpression3;
		}

		// Token: 0x0600D561 RID: 54625 RVA: 0x002D64F0 File Offset: 0x002D46F0
		private FormulaExpression TranslateDateTimePart(DateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = this.Translate(dateTimePart.Node.Children[0], default(CancellationToken));
			DateTimePartKind value = dateTimePart.dateTimePartKind.Value;
			IFormulaTyped formulaTyped = formulaExpression as IFormulaTyped;
			if (formulaTyped != null && formulaTyped.Type != typeof(DateTime))
			{
				formulaExpression = this.AddAutoVariable<DateTime>("inputDate", CSharpExpressionHelper.ToDateTime(formulaExpression), true);
			}
			if (value == DateTimePartKind.YearDays)
			{
				this.AddUsingGlobalization();
				return CSharpExpressionHelper.YearDays(CSharpExpressionHelper.Dot<object>(CSharpExpressionHelper.CultureInfo("en-US"), "Calendar"), CSharpExpressionHelper.Year(formulaExpression));
			}
			if (value == DateTimePartKind.YearWeek)
			{
				this.AddUsingGlobalization();
				return CSharpExpressionHelper.DotFunc<int>(this.AddVariable<Calendar>("calendar", CSharpExpressionHelper.Dot<object>(CSharpExpressionHelper.CultureInfo("en-US"), "Calendar")), "GetWeekOfYear", new FormulaExpression[]
				{
					formulaExpression,
					CSharpExpressionHelper.Dot<int>("CalendarWeekRule", "FirstDay"),
					CSharpExpressionHelper.Dot<int>("DayOfWeek", "Sunday")
				});
			}
			if (value == DateTimePartKind.YearDay)
			{
				this.AddUsingGlobalization();
				return CSharpExpressionHelper.YearDay(CSharpExpressionHelper.Dot<object>(CSharpExpressionHelper.CultureInfo("en-US"), "Calendar"), formulaExpression);
			}
			if (value == DateTimePartKind.QuarterDays)
			{
				FormulaExpression formulaExpression2 = this.DateQuarter(formulaExpression, false);
				FormulaExpression formulaExpression3 = this.DateQuarterStart(formulaExpression, formulaExpression2, false);
				return CSharpExpressionHelper.Dot<int>(CSharpExpressionHelper.Minus(this.DateQuarterEnd(formulaExpression, formulaExpression2, formulaExpression3, false), formulaExpression3), "Days");
			}
			if (value == DateTimePartKind.QuarterDay)
			{
				FormulaExpression formulaExpression4 = this.DateQuarterStart(formulaExpression, null, false);
				return CSharpExpressionHelper.Plus1(CSharpExpressionHelper.Dot<int>(CSharpExpressionHelper.Minus(formulaExpression, formulaExpression4), "Days"));
			}
			if (value == DateTimePartKind.QuarterWeek)
			{
				FormulaExpression formulaExpression5 = this.DateQuarterStart(formulaExpression, null, false);
				return this.DateWeekCount(formulaExpression, formulaExpression5, true);
			}
			if (value == DateTimePartKind.MonthWeek)
			{
				FormulaExpression formulaExpression6 = this.AddVariable<DateTime>("monthStart", CSharpExpressionHelper.MonthStart(formulaExpression));
				return this.DateWeekCount(formulaExpression, formulaExpression6, true);
			}
			if (value == DateTimePartKind.MonthDays)
			{
				this.AddUsingGlobalization();
				return CSharpExpressionHelper.MonthDays(CSharpExpressionHelper.Dot<object>(CSharpExpressionHelper.CultureInfo("en-US"), "Calendar"), CSharpExpressionHelper.Year(formulaExpression), CSharpExpressionHelper.Month(formulaExpression));
			}
			switch (value)
			{
			case DateTimePartKind.Second:
				return CSharpExpressionHelper.Second(formulaExpression);
			case DateTimePartKind.Minute:
				return CSharpExpressionHelper.Minute(formulaExpression);
			case DateTimePartKind.Hour:
				return CSharpExpressionHelper.Hour(formulaExpression);
			case DateTimePartKind.WeekDay:
				return CSharpExpressionHelper.Plus(CSharpExpressionHelper.WeekDay(formulaExpression), 1);
			case DateTimePartKind.MonthDay:
				return CSharpExpressionHelper.Day(formulaExpression);
			case DateTimePartKind.MonthWeek:
			case DateTimePartKind.MonthDays:
			case DateTimePartKind.QuarterDay:
			case DateTimePartKind.QuarterWeek:
			case DateTimePartKind.QuarterDays:
				break;
			case DateTimePartKind.Month:
				return CSharpExpressionHelper.Month(formulaExpression);
			case DateTimePartKind.Quarter:
				return this.DateQuarter(formulaExpression, true);
			default:
				if (value == DateTimePartKind.Year)
				{
					return CSharpExpressionHelper.Year(formulaExpression);
				}
				break;
			}
			return null;
		}

		// Token: 0x0600D562 RID: 54626 RVA: 0x002D677C File Offset: 0x002D497C
		private FormulaExpression TranslateFormatDateTime(FormatDateTime formatDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(formatDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = formatDateTime.dateTimeFormatDesc.Value;
			IFormulaTyped formulaTyped = formulaExpression as IFormulaTyped;
			if (formulaTyped != null && formulaTyped.Type != typeof(DateTime))
			{
				formulaExpression = this.AddAutoVariable<DateTime>("inputDate", CSharpExpressionHelper.ToDateTime(formulaExpression), true);
			}
			this.AddUsingGlobalization();
			return CSharpExpressionHelper.ToString(formulaExpression, CSharpExpressionHelper.StringLiteral(value.Mask), CSharpExpressionHelper.CultureInfo(value.Culture));
		}

		// Token: 0x0600D563 RID: 54627 RVA: 0x002D6810 File Offset: 0x002D4A10
		private static FormulaExpression TranslateFromDateTimePart(FromDateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.ToInt(CSharpExpressionHelper.Variable(dateTimePart.columnName.Value));
			DateTimePartKind value = dateTimePart.fromDateTimePartKind.Value;
			FormulaExpression formulaExpression2;
			if (value != DateTimePartKind.Month)
			{
				if (value == DateTimePartKind.Year)
				{
					formulaExpression2 = CSharpExpressionHelper.DateTime(formulaExpression, 1, 1);
				}
				else
				{
					formulaExpression2 = null;
				}
			}
			else
			{
				formulaExpression2 = CSharpExpressionHelper.DateTime(2000, formulaExpression, 1);
			}
			return formulaExpression2;
		}

		// Token: 0x0600D564 RID: 54628 RVA: 0x002D6870 File Offset: 0x002D4A70
		private FormulaExpression TranslateParseDateTime(ParseDateTime parseDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(parseDateTime.Node.Children[0], default(CancellationToken));
			if (formulaExpression == null)
			{
				return null;
			}
			DateTimeDescriptor value = parseDateTime.dateTimeParseDesc.Value;
			this._includeParseDateTimeMethod = true;
			string text = "date{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression2 = CSharpExpressionHelper.Variable(string.Format(text, varCount));
			FormulaExpression formulaExpression3 = CSharpExpressionHelper.ParseDateTime(formulaExpression, CSharpExpressionHelper.StringLiteral(value.Mask), CSharpExpressionHelper.CultureInfo(value.Culture));
			this._statements.Add(CSharpExpressionHelper.Var(CSharpExpressionHelper.Assign(formulaExpression2, formulaExpression3)));
			this._statements.Add(CSharpExpressionHelper.IfNullReturnNull(formulaExpression2));
			return CSharpExpressionHelper.Dot<DateTime>(formulaExpression2, "Value");
		}

		// Token: 0x0600D565 RID: 54629 RVA: 0x002D6938 File Offset: 0x002D4B38
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

		// Token: 0x0600D566 RID: 54630 RVA: 0x002D6990 File Offset: 0x002D4B90
		private FormulaExpression TranslateRoundDateTimeDown(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimePeriod period = roundDateTime.dateTimeRoundDesc.Value.Period;
			IFormulaTyped formulaTyped = formulaExpression as IFormulaTyped;
			if (formulaTyped != null && formulaTyped.Type != typeof(DateTime))
			{
				formulaExpression = this.AddAutoVariable<DateTime>("inputDate", CSharpExpressionHelper.ToDateTime(formulaExpression), true);
			}
			if (period == RoundDateTimePeriod.Quarter)
			{
				FormulaExpression formulaExpression2 = this.DateQuarter(formulaExpression, false);
				return this.DateQuarterStart(formulaExpression, formulaExpression2, true);
			}
			FormulaExpression formulaExpression3;
			switch (period)
			{
			case RoundDateTimePeriod.Second:
				formulaExpression3 = CSharpExpressionHelper.SecondStart(formulaExpression);
				goto IL_00F3;
			case RoundDateTimePeriod.Minute:
				formulaExpression3 = CSharpExpressionHelper.MinuteStart(formulaExpression);
				goto IL_00F3;
			case RoundDateTimePeriod.Hour:
				formulaExpression3 = CSharpExpressionHelper.HourStart(formulaExpression);
				goto IL_00F3;
			case RoundDateTimePeriod.Day:
				formulaExpression3 = CSharpExpressionHelper.DayStart(formulaExpression);
				goto IL_00F3;
			case RoundDateTimePeriod.Week:
				formulaExpression3 = CSharpExpressionHelper.WeekStart(formulaExpression);
				goto IL_00F3;
			case RoundDateTimePeriod.Month:
				formulaExpression3 = CSharpExpressionHelper.MonthStart(formulaExpression);
				goto IL_00F3;
			case RoundDateTimePeriod.Year:
				formulaExpression3 = CSharpExpressionHelper.YearStart(formulaExpression);
				goto IL_00F3;
			}
			formulaExpression3 = null;
			IL_00F3:
			FormulaExpression formulaExpression4 = formulaExpression3;
			return this.AddVariable<DateTime>(period.ToString().ToLower() + "Down", formulaExpression4);
		}

		// Token: 0x0600D567 RID: 54631 RVA: 0x002D6AB8 File Offset: 0x002D4CB8
		private FormulaExpression TranslateRoundDateTimeNearest(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimePeriod period = roundDateTime.dateTimeRoundDesc.Value.Period;
			IFormulaTyped formulaTyped = formulaExpression as IFormulaTyped;
			if (formulaTyped != null && formulaTyped.Type != typeof(DateTime))
			{
				formulaExpression = this.AddAutoVariable<DateTime>("inputDate", CSharpExpressionHelper.ToDateTime(formulaExpression), true);
			}
			FormulaExpression formulaExpression2 = null;
			FormulaExpression formulaExpression3 = null;
			FormulaExpression formulaExpression4 = null;
			if (period == RoundDateTimePeriod.Second)
			{
				formulaExpression2 = this.AddVariable<DateTime>("secondStart", CSharpExpressionHelper.SecondStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("secondEnd", CSharpExpressionHelper.SecondEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("secondMidpoint", CSharpExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Minute)
			{
				formulaExpression2 = this.AddVariable<DateTime>("minuteStart", CSharpExpressionHelper.MinuteStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("minuteEnd", CSharpExpressionHelper.MinuteEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("minuteMidpoint", CSharpExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Hour)
			{
				formulaExpression2 = this.AddVariable<DateTime>("hourStart", CSharpExpressionHelper.HourStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("hourEnd", CSharpExpressionHelper.HourEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("hour_midpoint", CSharpExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Day)
			{
				formulaExpression2 = this.AddVariable<DateTime>("dayStart", CSharpExpressionHelper.DayStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("dayEnd", CSharpExpressionHelper.DayEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("dayMidpoint", CSharpExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Week)
			{
				formulaExpression2 = this.AddVariable<DateTime>("weekStart", CSharpExpressionHelper.WeekStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("weekEnd", CSharpExpressionHelper.WeekEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("weekMidpoint", CSharpExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Month)
			{
				formulaExpression2 = this.AddVariable<DateTime>("monthStart", CSharpExpressionHelper.MonthStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("monthEnd", CSharpExpressionHelper.MonthEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("monthMidpoint", CSharpExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Quarter)
			{
				FormulaExpression formulaExpression5 = this.DateQuarter(formulaExpression, false);
				formulaExpression2 = this.DateQuarterStart(formulaExpression, formulaExpression5, false);
				formulaExpression3 = this.DateQuarterEnd(formulaExpression, formulaExpression5, formulaExpression2, false);
				formulaExpression4 = this.AddVariable<DateTime>("quarterMidpoint", CSharpExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Year)
			{
				formulaExpression2 = this.AddVariable<DateTime>("yearStart", CSharpExpressionHelper.YearStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("yearEnd", CSharpExpressionHelper.YearEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("yearMidpoint", CSharpExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (formulaExpression2 == null || formulaExpression3 == null || formulaExpression4 == null)
			{
				return null;
			}
			FormulaExpression formulaExpression6 = CSharpExpressionHelper.Ternary(CSharpExpressionHelper.LessThan(formulaExpression, formulaExpression4), formulaExpression2, formulaExpression3);
			return this.AddVariable<DateTime>(period.ToString().ToLower() + "Nearest", formulaExpression6);
		}

		// Token: 0x0600D568 RID: 54632 RVA: 0x002D6D78 File Offset: 0x002D4F78
		private FormulaExpression TranslateRoundDateTimeUp(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimeDescriptor value = roundDateTime.dateTimeRoundDesc.Value;
			RoundDateTimePeriod period = value.Period;
			int ceiling = (int)value.Ceiling;
			IFormulaTyped formulaTyped = formulaExpression as IFormulaTyped;
			if (formulaTyped != null && formulaTyped.Type != typeof(DateTime))
			{
				formulaExpression = this.AddAutoVariable<DateTime>("inputDate", CSharpExpressionHelper.ToDateTime(formulaExpression), true);
			}
			bool flag = ceiling == 0;
			FormulaExpression formulaExpression2 = null;
			FormulaExpression formulaExpression3 = null;
			if (period == RoundDateTimePeriod.Second)
			{
				formulaExpression2 = this.AddVariable<DateTime>("secondStart", CSharpExpressionHelper.SecondStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("secondEnd", CSharpExpressionHelper.SecondEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Minute)
			{
				formulaExpression2 = this.AddVariable<DateTime>("minuteStart", CSharpExpressionHelper.MinuteStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("minuteEnd", CSharpExpressionHelper.MinuteEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Hour)
			{
				formulaExpression2 = this.AddVariable<DateTime>("hourStart", CSharpExpressionHelper.HourStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("hourEnd", CSharpExpressionHelper.HourEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Day)
			{
				formulaExpression2 = this.AddVariable<DateTime>("dayStart", CSharpExpressionHelper.DayStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("dayEnd", CSharpExpressionHelper.DayEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Week)
			{
				formulaExpression2 = this.AddVariable<DateTime>("weekStart", CSharpExpressionHelper.WeekStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("weekEnd", CSharpExpressionHelper.WeekEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Month)
			{
				formulaExpression2 = this.AddVariable<DateTime>("monthStart", CSharpExpressionHelper.MonthStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("monthEnd", CSharpExpressionHelper.MonthEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Quarter)
			{
				FormulaExpression formulaExpression4 = this.DateQuarter(formulaExpression, false);
				formulaExpression2 = this.DateQuarterStart(formulaExpression, formulaExpression4, false);
				formulaExpression3 = this.DateQuarterEnd(formulaExpression, formulaExpression4, formulaExpression2, false);
			}
			if (period == RoundDateTimePeriod.Year)
			{
				formulaExpression2 = this.AddVariable<DateTime>("yearStart", CSharpExpressionHelper.YearStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("yearEnd", CSharpExpressionHelper.YearEnd(formulaExpression2));
			}
			if (formulaExpression2 == null || formulaExpression3 == null)
			{
				return null;
			}
			FormulaExpression formulaExpression5 = (flag ? CSharpExpressionHelper.AddDays(formulaExpression3, -1) : CSharpExpressionHelper.Ternary(CSharpExpressionHelper.NotEqual(formulaExpression, formulaExpression2), formulaExpression3, formulaExpression2));
			return this.AddVariable<DateTime>(period.ToString().ToLower() + "Up", formulaExpression5);
		}

		// Token: 0x0600D569 RID: 54633 RVA: 0x002D6FAC File Offset: 0x002D51AC
		private FormulaExpression DateQuarter(FormulaExpression subject, bool inline = false)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.ToInt(CSharpExpressionHelper.Ceiling(CSharpExpressionHelper.Divide(CSharpExpressionHelper.Month(subject), 3.0)));
			if (!inline)
			{
				return this.AddVariable<int>("quarter", formulaExpression);
			}
			return formulaExpression;
		}

		// Token: 0x0600D56A RID: 54634 RVA: 0x002D6FEC File Offset: 0x002D51EC
		private FormulaExpression DateQuarterEnd(FormulaExpression subject, FormulaExpression quarter = null, FormulaExpression quarterStart = null, bool inline = false)
		{
			if (quarter == null)
			{
				quarter = this.DateQuarter(subject, inline);
			}
			if (quarterStart == null)
			{
				quarterStart = this.DateQuarterStart(subject, quarter, inline);
			}
			FormulaExpression formulaExpression = CSharpExpressionHelper.AddMonths(quarterStart, 3);
			if (!inline)
			{
				return this.AddVariable<DateTime>("quarterEnd", formulaExpression);
			}
			return formulaExpression;
		}

		// Token: 0x0600D56B RID: 54635 RVA: 0x002D7030 File Offset: 0x002D5230
		private FormulaExpression DateQuarterStart(FormulaExpression subject, FormulaExpression quarter = null, bool inline = false)
		{
			if (quarter == null)
			{
				quarter = this.DateQuarter(subject, inline);
			}
			FormulaExpression formulaExpression = CSharpExpressionHelper.DateTime(CSharpExpressionHelper.Year(subject), CSharpExpressionHelper.Minus(CSharpExpressionHelper.Multiply(3, quarter), 2), CSharpExpressionHelper.NumberLiteral(1));
			if (!inline)
			{
				return this.AddVariable<DateTime>("quarterStart", formulaExpression);
			}
			return formulaExpression;
		}

		// Token: 0x0600D56C RID: 54636 RVA: 0x002D707C File Offset: 0x002D527C
		private FormulaExpression DateWeekCount(FormulaExpression subject, FormulaExpression periodStart, bool inline = false)
		{
			FormulaExpression formulaExpression = this.AddVariable<int>("days", CSharpExpressionHelper.Dot<int>(CSharpExpressionHelper.Minus(subject, periodStart), "Days"));
			FormulaExpression formulaExpression2 = this.AddVariable<int>("startWeekday", CSharpExpressionHelper.Plus1(CSharpExpressionHelper.WeekDay(periodStart)));
			FormulaExpression formulaExpression3 = CSharpExpressionHelper.ToInt(CSharpExpressionHelper.Ceiling(CSharpExpressionHelper.Divide(CSharpExpressionHelper.Plus(formulaExpression, formulaExpression2), 7.0)));
			if (!inline)
			{
				return this.AddVariable<int>("weekCount", formulaExpression3);
			}
			return formulaExpression3;
		}

		// Token: 0x0600D56D RID: 54637 RVA: 0x002D70EC File Offset: 0x002D52EC
		private FormulaExpression TranslateContains(Contains contains)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable(contains.columnName.Value);
			CSharpProgramTranslator.<>c__DisplayClass60_0 CS$<>8__locals1;
			CS$<>8__locals1.delimiterExp = this.Translate(contains.containsFindText.Node, default(CancellationToken));
			CS$<>8__locals1.countExp = this.Translate(contains.containsCount.Node, default(CancellationToken));
			IFormulaTyped formulaTyped = formulaExpression as IFormulaTyped;
			if (formulaTyped != null && formulaTyped.Type != typeof(string))
			{
				FormulaExpression formulaExpression2 = formulaExpression;
				Type typeFromHandle = typeof(string);
				string text = "inputString{0}";
				int varCount = this._varCount;
				this._varCount = varCount + 1;
				FormulaExpression formulaExpression3;
				return CSharpExpressionHelper.And(CSharpExpressionHelper.Is(formulaExpression2, typeFromHandle, string.Format(text, varCount), out formulaExpression3), CSharpProgramTranslator.<TranslateContains>g__Check|60_0(formulaExpression3, ref CS$<>8__locals1));
			}
			return CSharpProgramTranslator.<TranslateContains>g__Check|60_0(formulaExpression, ref CS$<>8__locals1);
		}

		// Token: 0x0600D56E RID: 54638 RVA: 0x002D71CC File Offset: 0x002D53CC
		private static FormulaExpression TranslateEndsWithDigit(EndsWithDigit endsWithDigit)
		{
			return CSharpExpressionHelper.IsDigit(CSharpExpressionHelper.Index<char>(CSharpExpressionHelper.Variable(endsWithDigit.columnName.Value), -1));
		}

		// Token: 0x0600D56F RID: 54639 RVA: 0x002D71F8 File Offset: 0x002D53F8
		private FormulaExpression TranslateIf(If ifNode)
		{
			FormulaExpression formulaExpression = this.Translate(ifNode.condition.Node, default(CancellationToken));
			List<FormulaExpression> statements = this._statements;
			Dictionary<FormulaExpression, FormulaExpression> contextVariables = this._contextVariables;
			this._statements = new List<FormulaExpression>();
			this._contextVariables = new Dictionary<FormulaExpression, FormulaExpression>();
			FormulaExpression formulaExpression2 = this.Translate(ifNode.result1.Node, default(CancellationToken));
			this._statements.Add(formulaExpression2);
			CSharpBlock csharpBlock = new CSharpBlock(this._statements);
			this._statements = new List<FormulaExpression>();
			this._contextVariables = new Dictionary<FormulaExpression, FormulaExpression>();
			FormulaExpression formulaExpression3 = this.Translate(ifNode.result2.Node, default(CancellationToken));
			this._statements.Add(formulaExpression3);
			CSharpBlock csharpBlock2 = new CSharpBlock(this._statements);
			this._statements = statements;
			this._contextVariables = contextVariables;
			return CSharpExpressionHelper.If(formulaExpression, csharpBlock, csharpBlock2);
		}

		// Token: 0x0600D570 RID: 54640 RVA: 0x002D72EC File Offset: 0x002D54EC
		private FormulaExpression TranslateIsBlank(IsBlank isBlank)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable(isBlank.columnName.Value);
			Type typeFromHandle = typeof(string);
			string text = "input{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression3;
			FormulaExpression formulaExpression2 = CSharpExpressionHelper.Is(formulaExpression, typeFromHandle, string.Format(text, varCount), out formulaExpression3);
			return CSharpExpressionHelper.Or(CSharpExpressionHelper.Equal(formulaExpression, CSharpExpressionHelper.Null()), CSharpExpressionHelper.And(formulaExpression2, CSharpExpressionHelper.IsNullOrEmpty(formulaExpression3)));
		}

		// Token: 0x0600D571 RID: 54641 RVA: 0x002D735C File Offset: 0x002D555C
		private FormulaExpression TranslateContainsMatch(ContainsMatch isMatch)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable(isMatch.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(isMatch.containsMatchRegex.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(isMatch.matchCount.Node, default(CancellationToken));
			this.AddUsingRegex();
			this.AddUsingLinq();
			return CSharpExpressionHelper.Equal(CSharpExpressionHelper.Count(CSharpExpressionHelper.Matches(formulaExpression, formulaExpression2)), formulaExpression3);
		}

		// Token: 0x0600D572 RID: 54642 RVA: 0x002D73DC File Offset: 0x002D55DC
		private FormulaExpression TranslateIsMatch(IsMatch isMatch)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable(isMatch.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(isMatch.isMatchRegex.Node, default(CancellationToken));
			this.AddUsingRegex();
			return CSharpExpressionHelper.IsMatch(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D573 RID: 54643 RVA: 0x002D7428 File Offset: 0x002D5628
		private FormulaExpression TranslateIsNotBlank(IsNotBlank isNotBlank)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable(isNotBlank.columnName.Value);
			Type typeFromHandle = typeof(string);
			string text = "input{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression3;
			FormulaExpression formulaExpression2 = CSharpExpressionHelper.Is(formulaExpression, typeFromHandle, string.Format(text, varCount), out formulaExpression3);
			return CSharpExpressionHelper.Not(CSharpExpressionHelper.Or(CSharpExpressionHelper.Equal(formulaExpression, CSharpExpressionHelper.Null()), CSharpExpressionHelper.And(formulaExpression2, CSharpExpressionHelper.IsNullOrEmpty(formulaExpression3))));
		}

		// Token: 0x0600D574 RID: 54644 RVA: 0x002D749C File Offset: 0x002D569C
		private static FormulaExpression TranslateIsNumber(IsNumber isNumber)
		{
			return CSharpExpressionHelper.Is(CSharpExpressionHelper.Variable(isNumber.columnName.Value), new Type[]
			{
				typeof(int),
				typeof(double),
				typeof(decimal)
			});
		}

		// Token: 0x0600D575 RID: 54645 RVA: 0x002D74F0 File Offset: 0x002D56F0
		private static FormulaExpression TranslateIsString(IsString isString)
		{
			return CSharpExpressionHelper.Is(CSharpExpressionHelper.Variable(isString.columnName.Value), typeof(string));
		}

		// Token: 0x0600D576 RID: 54646 RVA: 0x002D7520 File Offset: 0x002D5720
		private static FormulaExpression TranslateNull()
		{
			return CSharpExpressionHelper.Null();
		}

		// Token: 0x0600D577 RID: 54647 RVA: 0x002D7528 File Offset: 0x002D5728
		private static FormulaExpression TranslateNumberEquals(NumberEquals numberEquals)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable(numberEquals.columnName.Value);
			LiteralNode literalNode = numberEquals.numberEqualsValue.Node as LiteralNode;
			if (literalNode != null)
			{
				object value = literalNode.Value;
				if (value is decimal)
				{
					decimal num = (decimal)value;
					FormulaExpression formulaExpression2 = CSharpExpressionHelper.NumberLiteral(num);
					FormulaExpression formulaExpression3 = CSharpProgramTranslator.ResolveType(formulaExpression, typeof(double));
					if (formulaExpression3 != null && formulaExpression != formulaExpression3)
					{
						formulaExpression = formulaExpression3;
					}
					return CSharpExpressionHelper.Equal(formulaExpression, formulaExpression2);
				}
			}
			return null;
		}

		// Token: 0x0600D578 RID: 54648 RVA: 0x002D75B8 File Offset: 0x002D57B8
		private static FormulaExpression TranslateNumberGreaterThan(NumberGreaterThan numberGreaterThan)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable(numberGreaterThan.columnName.Value);
			LiteralNode literalNode = numberGreaterThan.numberGreaterThanValue.Node as LiteralNode;
			if (literalNode != null)
			{
				object value = literalNode.Value;
				if (value is decimal)
				{
					decimal num = (decimal)value;
					FormulaExpression formulaExpression2 = CSharpExpressionHelper.NumberLiteral(num);
					FormulaExpression formulaExpression3 = CSharpProgramTranslator.ResolveType(formulaExpression, typeof(double));
					if (formulaExpression3 != null && formulaExpression != formulaExpression3)
					{
						formulaExpression = formulaExpression3;
					}
					return CSharpExpressionHelper.GreaterThan(formulaExpression, formulaExpression2);
				}
			}
			return null;
		}

		// Token: 0x0600D579 RID: 54649 RVA: 0x002D7648 File Offset: 0x002D5848
		private static FormulaExpression TranslateNumberLessThan(NumberLessThan numberLessThan)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable(numberLessThan.columnName.Value);
			LiteralNode literalNode = numberLessThan.numberLessThanValue.Node as LiteralNode;
			if (literalNode != null)
			{
				object value = literalNode.Value;
				if (value is decimal)
				{
					decimal num = (decimal)value;
					FormulaExpression formulaExpression2 = CSharpExpressionHelper.NumberLiteral(num);
					FormulaExpression formulaExpression3 = CSharpProgramTranslator.ResolveType(formulaExpression, typeof(double));
					if (formulaExpression3 != null && formulaExpression != formulaExpression3)
					{
						formulaExpression = formulaExpression3;
					}
					return CSharpExpressionHelper.LessThan(formulaExpression, formulaExpression2);
				}
			}
			return null;
		}

		// Token: 0x0600D57A RID: 54650 RVA: 0x002D76D8 File Offset: 0x002D58D8
		private FormulaExpression TranslateOr(Or or)
		{
			FormulaExpression formulaExpression = this.Translate(or.condition1.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(or.condition2.Node, default(CancellationToken));
			return CSharpExpressionHelper.Or(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D57B RID: 54651 RVA: 0x002D7728 File Offset: 0x002D5928
		private FormulaExpression TranslateStartsWith(StartsWith startsWith)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable(startsWith.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(startsWith.startsWithFindText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = CSharpExpressionHelper.StartsWith(formulaExpression, formulaExpression2, CSharpExpressionHelper.IntLiteral(0));
			IFormulaTyped formulaTyped = formulaExpression as IFormulaTyped;
			if (formulaTyped != null)
			{
				Type type = formulaTyped.Type;
				if (type != null && type.IsNullable())
				{
					formulaExpression3 = CSharpExpressionHelper.Equal(formulaExpression3, CSharpExpressionHelper.True());
				}
			}
			return formulaExpression3;
		}

		// Token: 0x0600D57C RID: 54652 RVA: 0x002D77A4 File Offset: 0x002D59A4
		private static FormulaExpression TranslateStartsWithDigit(StartsWithDigit startsWithDigit)
		{
			return CSharpExpressionHelper.IsDigit(CSharpExpressionHelper.Index<string>(CSharpExpressionHelper.Variable(startsWithDigit.columnName.Value), 0));
		}

		// Token: 0x0600D57D RID: 54653 RVA: 0x002D77D0 File Offset: 0x002D59D0
		private FormulaExpression TranslateStringEquals(StringEquals stringEquals)
		{
			FormulaExpression formulaExpression = CSharpExpressionHelper.Variable(stringEquals.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(stringEquals.equalsText.Node, default(CancellationToken));
			return CSharpExpressionHelper.Equal(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D57E RID: 54654 RVA: 0x002D7818 File Offset: 0x002D5A18
		private FormulaExpression TranslateAdd(Add add)
		{
			FormulaExpression formulaExpression = this.Translate(add.arithmeticLeft.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(add.addRight.Node, default(CancellationToken));
			return CSharpExpressionHelper.Plus(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D57F RID: 54655 RVA: 0x002D7868 File Offset: 0x002D5A68
		private FormulaExpression TranslateAverage(Average average)
		{
			this.AddUsingLinq();
			IEnumerable<FormulaExpression> enumerable = CSharpProgramTranslator.TranslateFromNumbers(average.fromNumbers);
			List<FormulaExpression> list = ((enumerable != null) ? enumerable.ToList<FormulaExpression>() : null);
			if (list != null)
			{
				return CSharpExpressionHelper.Average(list);
			}
			return null;
		}

		// Token: 0x0600D580 RID: 54656 RVA: 0x002D78A0 File Offset: 0x002D5AA0
		private FormulaExpression TranslateDivide(Divide divide)
		{
			FormulaExpression formulaExpression = this.Translate(divide.arithmeticLeft.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(divide.divideRight.Node, default(CancellationToken));
			return CSharpExpressionHelper.Divide(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D581 RID: 54657 RVA: 0x002D78F0 File Offset: 0x002D5AF0
		private static IEnumerable<FormulaExpression> TranslateFromNumbers(fromNumbers fromNumbers)
		{
			LiteralNode literalNode = fromNumbers.Node.Children[1] as LiteralNode;
			string[] array = ((literalNode != null) ? literalNode.Value : null) as string[];
			if (array != null)
			{
				return array.Select((string columnName) => CSharpExpressionHelper.ToDecimal(CSharpExpressionHelper.Variable(columnName)));
			}
			return null;
		}

		// Token: 0x0600D582 RID: 54658 RVA: 0x002D794C File Offset: 0x002D5B4C
		private FormulaExpression TranslateMultiply(Multiply multiply)
		{
			FormulaExpression formulaExpression = this.Translate(multiply.arithmeticLeft.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(multiply.multiplyRight.Node, default(CancellationToken));
			return CSharpExpressionHelper.Multiply(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D583 RID: 54659 RVA: 0x002D799C File Offset: 0x002D5B9C
		private FormulaExpression TranslateProduct(Product product)
		{
			this.AddUsingLinq();
			IEnumerable<FormulaExpression> enumerable = CSharpProgramTranslator.TranslateFromNumbers(product.fromNumbers);
			List<FormulaExpression> list = ((enumerable != null) ? enumerable.ToList<FormulaExpression>() : null);
			if (list != null)
			{
				return CSharpExpressionHelper.Product(list);
			}
			return null;
		}

		// Token: 0x0600D584 RID: 54660 RVA: 0x002D79D4 File Offset: 0x002D5BD4
		private FormulaExpression TranslateSubtract(Subtract subtract)
		{
			FormulaExpression formulaExpression = this.Translate(subtract.arithmeticLeft.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(subtract.subtractRight.Node, default(CancellationToken));
			return CSharpExpressionHelper.Minus(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600D585 RID: 54661 RVA: 0x002D7A24 File Offset: 0x002D5C24
		private FormulaExpression TranslateSum(Sum sum)
		{
			this.AddUsingLinq();
			IEnumerable<FormulaExpression> enumerable = CSharpProgramTranslator.TranslateFromNumbers(sum.fromNumbers);
			List<FormulaExpression> list = ((enumerable != null) ? enumerable.ToList<FormulaExpression>() : null);
			if (list != null)
			{
				return CSharpExpressionHelper.Sum(list);
			}
			return null;
		}

		// Token: 0x0600D586 RID: 54662 RVA: 0x002D7A5C File Offset: 0x002D5C5C
		private FormulaExpression TranslateFromDateTime(FromDateTime fromDateTime)
		{
			string value = fromDateTime.columnName.Value;
			return CSharpExpressionHelper.Variable(value, base.ResolveInputType(value));
		}

		// Token: 0x0600D587 RID: 54663 RVA: 0x002D7A88 File Offset: 0x002D5C88
		private FormulaExpression TranslateFromNumber(FromNumber fromNumber)
		{
			string value = fromNumber.columnName.Value;
			return CSharpExpressionHelper.Variable(value, base.ResolveInputType(value));
		}

		// Token: 0x0600D588 RID: 54664 RVA: 0x002D7AB4 File Offset: 0x002D5CB4
		private FormulaExpression TranslateFromStr(FromStr input)
		{
			string value = input.columnName.Value;
			return CSharpExpressionHelper.Variable(value, base.ResolveInputType(value));
		}

		// Token: 0x0600D589 RID: 54665 RVA: 0x002D7AE0 File Offset: 0x002D5CE0
		private FormulaExpression TranslateToDateTime(ToDateTime subject)
		{
			return CSharpProgramTranslator.ResolveType<DateTime>(this.Translate(subject.outDate.Node, default(CancellationToken)));
		}

		// Token: 0x0600D58A RID: 54666 RVA: 0x002D7B10 File Offset: 0x002D5D10
		private FormulaExpression TranslateToDecimal(ToDecimal subject)
		{
			return CSharpProgramTranslator.ResolveType<decimal>(this.Translate(subject.outNumber.Node, default(CancellationToken)));
		}

		// Token: 0x0600D58B RID: 54667 RVA: 0x002D7B40 File Offset: 0x002D5D40
		private FormulaExpression TranslateToDouble(ToDouble subject)
		{
			return CSharpProgramTranslator.ResolveType<double>(this.Translate(subject.outNumber.Node, default(CancellationToken)));
		}

		// Token: 0x0600D58C RID: 54668 RVA: 0x002D7B70 File Offset: 0x002D5D70
		private FormulaExpression TranslateToInt(ToInt subject)
		{
			return CSharpProgramTranslator.ResolveType<int>(this.Translate(subject.outNumber.Node, default(CancellationToken)));
		}

		// Token: 0x0600D58D RID: 54669 RVA: 0x002D7BA0 File Offset: 0x002D5DA0
		private FormulaExpression TranslateToStr(ToStr subject)
		{
			return CSharpProgramTranslator.ResolveType<string>(this.Translate(subject.outStr.Node, default(CancellationToken)));
		}

		// Token: 0x0600D58E RID: 54670 RVA: 0x002D7BD0 File Offset: 0x002D5DD0
		private FormulaExpression AddAutoVariable<T>(string name, FormulaExpression value, bool nullCheck = true)
		{
			return this.AddAutoVariable(name, value, typeof(T), nullCheck);
		}

		// Token: 0x0600D58F RID: 54671 RVA: 0x002D7BE8 File Offset: 0x002D5DE8
		private FormulaExpression AddAutoVariable(string name, FormulaExpression value, Type type, bool nullCheck = true)
		{
			FormulaExpression formulaExpression;
			if (this._contextVariables.TryGetValue(value, out formulaExpression))
			{
				return formulaExpression;
			}
			string text = "{0}{1}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression2 = CSharpExpressionHelper.Variable(string.Format(text, name, varCount), type);
			CSharpVar csharpVar = CSharpExpressionHelper.Var(CSharpExpressionHelper.Assign(formulaExpression2, value));
			this._statements.Add(csharpVar);
			this._contextVariables[value] = formulaExpression2;
			if (nullCheck && type.IsNullable())
			{
				this._statements.Add(CSharpExpressionHelper.IfNullReturnNull(formulaExpression2));
			}
			return formulaExpression2;
		}

		// Token: 0x0600D590 RID: 54672 RVA: 0x002D7C73 File Offset: 0x002D5E73
		private FormulaExpression AddVariable<T>(string name, FormulaExpression value)
		{
			return this.AddVariable(name, value, typeof(T));
		}

		// Token: 0x0600D591 RID: 54673 RVA: 0x002D7C87 File Offset: 0x002D5E87
		private FormulaExpression AddVariable(string name, FormulaExpression value, Type type)
		{
			return this.AddVariable(CSharpExpressionHelper.Variable(name, type), value);
		}

		// Token: 0x0600D592 RID: 54674 RVA: 0x002D7C98 File Offset: 0x002D5E98
		private FormulaExpression AddVariable(FormulaExpression variable, FormulaExpression value)
		{
			FormulaExpression formulaExpression;
			if (this._contextVariables.TryGetValue(value, out formulaExpression))
			{
				return formulaExpression;
			}
			CSharpVar csharpVar = CSharpExpressionHelper.Var(CSharpExpressionHelper.Assign(variable, value));
			this._statements.Add(csharpVar);
			this._contextVariables[value] = variable;
			return variable;
		}

		// Token: 0x0600D593 RID: 54675 RVA: 0x002D7CDE File Offset: 0x002D5EDE
		internal static CSharpMethod ResolveReturn(CSharpMethod method)
		{
			return method.Transform<CSharpMethod>(delegate(FormulaExpression node)
			{
				CSharpBlock csharpBlock = node as CSharpBlock;
				if (csharpBlock == null)
				{
					return node;
				}
				IReadOnlyList<FormulaExpression> statements = csharpBlock.Statements;
				List<FormulaExpression> list = ((statements != null) ? statements.ToList<FormulaExpression>() : null);
				FormulaExpression formulaExpression = ((list != null) ? list.LastOrDefault<FormulaExpression>() : null);
				bool flag = formulaExpression == null || formulaExpression is CSharpReturn || formulaExpression is CSharpIf;
				if (flag)
				{
					return node;
				}
				CSharpAssign csharpAssign = formulaExpression as CSharpAssign;
				if (csharpAssign != null)
				{
					formulaExpression = csharpAssign.Right;
				}
				list.RemoveAt(list.Count - 1);
				list.Add(CSharpExpressionHelper.Return(formulaExpression));
				return CSharpExpressionHelper.Block(list);
			});
		}

		// Token: 0x0600D594 RID: 54676 RVA: 0x002D7D05 File Offset: 0x002D5F05
		private static FormulaExpression ResolveType<TDesired>(FormulaExpression expression)
		{
			return CSharpProgramTranslator.ResolveType(expression, typeof(TDesired));
		}

		// Token: 0x0600D595 RID: 54677 RVA: 0x002D7D18 File Offset: 0x002D5F18
		private static FormulaExpression ResolveType(FormulaExpression expression, Type desiredType)
		{
			IFormulaTyped formulaTyped = expression as IFormulaTyped;
			if (formulaTyped != null)
			{
				return CSharpProgramTranslator.ResolveType(expression, desiredType, formulaTyped.Type);
			}
			return expression;
		}

		// Token: 0x0600D596 RID: 54678 RVA: 0x002D7D40 File Offset: 0x002D5F40
		private static FormulaExpression ResolveType(FormulaExpression expression, Type desiredType, Type existingType)
		{
			if (desiredType.IsAssignableFrom(existingType))
			{
				return expression;
			}
			bool flag = existingType == typeof(string);
			bool flag2 = existingType == typeof(int) || existingType == typeof(int?);
			bool flag3 = existingType == typeof(double) || existingType == typeof(double?);
			int num = ((existingType == typeof(decimal) || existingType == typeof(decimal?)) ? 1 : 0);
			bool flag4 = existingType == typeof(DateTime) || existingType == typeof(DateTime?);
			bool flag5 = desiredType == typeof(string);
			bool flag6 = desiredType == typeof(int) || desiredType == typeof(int?);
			bool flag7 = desiredType == typeof(double) || desiredType == typeof(double?);
			bool flag8 = desiredType == typeof(decimal) || desiredType == typeof(decimal?);
			bool flag9 = desiredType == typeof(DateTime) || desiredType == typeof(DateTime?);
			if (num == 0 && flag8)
			{
				return CSharpExpressionHelper.ToDecimal(expression);
			}
			if (!flag3 && flag7)
			{
				return CSharpExpressionHelper.ToDouble(expression);
			}
			if (!flag2 && flag6)
			{
				return CSharpExpressionHelper.ToInt(expression);
			}
			if (!flag4 && flag9)
			{
				return CSharpExpressionHelper.ToDateTime(expression);
			}
			if (flag || !flag5)
			{
				return expression;
			}
			return CSharpExpressionHelper.ToString(expression);
		}

		// Token: 0x0600D597 RID: 54679 RVA: 0x002D7F00 File Offset: 0x002D6100
		private CSharpMethod AllIndexesOfMethod()
		{
			this.AddUsingCollections();
			this.AddUsingLinq();
			return CSharpExpressionHelper.Method("AllIndexesOf", typeof(int[]), new CSharpMethodParameter[]
			{
				CSharpExpressionHelper.MethodParameter("source", typeof(string), false, null, true),
				CSharpExpressionHelper.MethodParameter("findText", typeof(string), false, null, false)
			}, new CSharpRawLine[]
			{
				CSharpExpressionHelper.RawLine("List<int> result = new List<int>();"),
				CSharpExpressionHelper.RawLine("int index = 0;"),
				CSharpExpressionHelper.RawLine(""),
				CSharpExpressionHelper.RawLine("while ((index = source.IndexOf(findText, index)) != -1) {"),
				CSharpExpressionHelper.RawLine("    result.Add(index);"),
				CSharpExpressionHelper.RawLine("    index += findText.Length;"),
				CSharpExpressionHelper.RawLine("}"),
				CSharpExpressionHelper.RawLine(""),
				CSharpExpressionHelper.RawLine("return result.ToArray();")
			}, "private");
		}

		// Token: 0x0600D598 RID: 54680 RVA: 0x002D7FE8 File Offset: 0x002D61E8
		private CSharpMethod ParseDateTimeMethod()
		{
			this.AddUsingGlobalization();
			return CSharpExpressionHelper.Method("ParseDateTime", typeof(DateTime?), new CSharpMethodParameter[]
			{
				CSharpExpressionHelper.MethodParameter("source", typeof(string), false, null, true),
				CSharpExpressionHelper.MethodParameter("format", typeof(string), false, null, false),
				CSharpExpressionHelper.MethodParameter("culture", typeof(CultureInfo), false, null, false)
			}, new CSharpRawLine[]
			{
				CSharpExpressionHelper.RawLine("return DateTime.TryParseExact(source, format, culture, DateTimeStyles.None, out DateTime val)"),
				CSharpExpressionHelper.RawLine("            ? val"),
				CSharpExpressionHelper.RawLine("            : null;")
			}, "private");
		}

		// Token: 0x0600D599 RID: 54681 RVA: 0x002D8098 File Offset: 0x002D6298
		private CSharpMethod ParseNumberMethod()
		{
			this.AddUsingGlobalization();
			return CSharpExpressionHelper.Method("ParseNumber", typeof(decimal?), new CSharpMethodParameter[]
			{
				CSharpExpressionHelper.MethodParameter("source", typeof(string), false, null, true),
				CSharpExpressionHelper.MethodParameter("culture", typeof(CultureInfo), false, null, false)
			}, new CSharpRawLine[]
			{
				CSharpExpressionHelper.RawLine("return !string.IsNullOrEmpty(source)"),
				CSharpExpressionHelper.RawLine("       && decimal.TryParse(source, NumberStyles.Any, culture, out decimal val)"),
				CSharpExpressionHelper.RawLine("            ? val"),
				CSharpExpressionHelper.RawLine("            : null;")
			}, "private");
		}

		// Token: 0x0600D59A RID: 54682 RVA: 0x002D8138 File Offset: 0x002D6338
		private static CSharpMethod RoundDownMethod()
		{
			return CSharpExpressionHelper.Method("RoundDown", typeof(decimal), new CSharpMethodParameter[]
			{
				CSharpExpressionHelper.MethodParameter("source", typeof(decimal), false, null, true),
				CSharpExpressionHelper.MethodParameter("delta", typeof(decimal), false, null, false)
			}, new CSharpRawLine[] { CSharpExpressionHelper.RawLine("return Math.Floor(source / delta) * delta;") }, "private");
		}

		// Token: 0x0600D59B RID: 54683 RVA: 0x002D81AC File Offset: 0x002D63AC
		private static CSharpMethod RoundNearestMethod()
		{
			return CSharpExpressionHelper.Method("RoundNearest", typeof(decimal), new CSharpMethodParameter[]
			{
				CSharpExpressionHelper.MethodParameter("source", typeof(decimal), false, null, true),
				CSharpExpressionHelper.MethodParameter("delta", typeof(decimal), false, null, false)
			}, new CSharpRawLine[] { CSharpExpressionHelper.RawLine("return Math.Round(source / delta, 0, MidpointRounding.AwayFromZero) * delta;") }, "private");
		}

		// Token: 0x0600D59C RID: 54684 RVA: 0x002D8220 File Offset: 0x002D6420
		private static CSharpMethod RoundUpMethod()
		{
			return CSharpExpressionHelper.Method("RoundUp", typeof(decimal), new CSharpMethodParameter[]
			{
				CSharpExpressionHelper.MethodParameter("source", typeof(decimal), false, null, true),
				CSharpExpressionHelper.MethodParameter("delta", typeof(decimal), false, null, false)
			}, new CSharpRawLine[] { CSharpExpressionHelper.RawLine("return Math.Ceiling(source / delta) * delta;") }, "private");
		}

		// Token: 0x0600D59D RID: 54685 RVA: 0x002D8294 File Offset: 0x002D6494
		private static CSharpMethod SliceBetweenMethod()
		{
			return CSharpExpressionHelper.Method("SliceBetween", typeof(string), new CSharpMethodParameter[]
			{
				CSharpExpressionHelper.MethodParameter("source", typeof(string), false, null, true),
				CSharpExpressionHelper.MethodParameter("startText", typeof(string), false, null, false),
				CSharpExpressionHelper.MethodParameter("endText", typeof(string), false, null, false)
			}, new CSharpRawLine[]
			{
				CSharpExpressionHelper.RawLine("if (string.IsNullOrEmpty(startText)) return null;"),
				CSharpExpressionHelper.RawLine("if (string.IsNullOrEmpty(endText)) return null;"),
				CSharpExpressionHelper.RawLine(""),
				CSharpExpressionHelper.RawLine("int startIndex = source.IndexOf(startText);"),
				CSharpExpressionHelper.RawLine("if (startIndex < 0) return null;"),
				CSharpExpressionHelper.RawLine("startIndex += startText.Length;"),
				CSharpExpressionHelper.RawLine(""),
				CSharpExpressionHelper.RawLine("int endIndex = source.Substring(startIndex).IndexOf(endText);"),
				CSharpExpressionHelper.RawLine("if (endIndex < 0) return null;"),
				CSharpExpressionHelper.RawLine("endIndex += startIndex;"),
				CSharpExpressionHelper.RawLine(""),
				CSharpExpressionHelper.RawLine("return source.Substring(startIndex, endIndex - startIndex);")
			}, "private");
		}

		// Token: 0x0600D59E RID: 54686 RVA: 0x002D83B4 File Offset: 0x002D65B4
		private static CSharpMethod SliceMethod()
		{
			return CSharpExpressionHelper.Method("Slice", typeof(string), new CSharpMethodParameter[]
			{
				CSharpExpressionHelper.MethodParameter("source", typeof(string), false, null, true),
				CSharpExpressionHelper.MethodParameter("startIndex", typeof(int), false, null, false),
				CSharpExpressionHelper.MethodParameter("endIndex", typeof(int), false, CSharpExpressionHelper.Raw("int.MaxValue"), false)
			}, new CSharpRawLine[]
			{
				CSharpExpressionHelper.RawLine("if (startIndex < 0) startIndex += source.Length;"),
				CSharpExpressionHelper.RawLine("if (endIndex < 0) endIndex += source.Length;"),
				CSharpExpressionHelper.RawLine(""),
				CSharpExpressionHelper.RawLine("startIndex = Math.Max(startIndex, 0);"),
				CSharpExpressionHelper.RawLine("endIndex = Math.Min(endIndex, source.Length);"),
				CSharpExpressionHelper.RawLine("if (startIndex >= endIndex) return string.Empty;"),
				CSharpExpressionHelper.RawLine(""),
				CSharpExpressionHelper.RawLine("return source.Substring(startIndex, endIndex - startIndex);")
			}, "private");
		}

		// Token: 0x0600D59F RID: 54687 RVA: 0x002D84A8 File Offset: 0x002D66A8
		private CSharpMethod ToTitleCaseMethod()
		{
			this.AddUsingGlobalization();
			return CSharpExpressionHelper.Method("ToTitleCase", typeof(string), new CSharpMethodParameter[]
			{
				CSharpExpressionHelper.MethodParameter("source", typeof(string), false, null, true),
				CSharpExpressionHelper.MethodParameter("culture", typeof(CultureInfo), false, null, false)
			}, new CSharpRawLine[] { CSharpExpressionHelper.RawLine("return culture.TextInfo.ToTitleCase(source.ToLower());") }, "private");
		}

		// Token: 0x0600D5A0 RID: 54688 RVA: 0x002D8521 File Offset: 0x002D6721
		private void AddUsingCollections()
		{
			this._usings.Add("System.Collections.Generic");
		}

		// Token: 0x0600D5A1 RID: 54689 RVA: 0x002D8534 File Offset: 0x002D6734
		private void AddUsingGlobalization()
		{
			this._usings.Add("System.Globalization");
		}

		// Token: 0x0600D5A2 RID: 54690 RVA: 0x002D8547 File Offset: 0x002D6747
		private void AddUsingLinq()
		{
			this._usings.Add("System.Linq");
		}

		// Token: 0x0600D5A3 RID: 54691 RVA: 0x002D855A File Offset: 0x002D675A
		private void AddUsingRegex()
		{
			this._usings.Add("System.Text.RegularExpressions");
		}

		// Token: 0x0600D5A5 RID: 54693 RVA: 0x002D8589 File Offset: 0x002D6789
		[CompilerGenerated]
		internal static FormulaExpression <TranslateContains>g__Check|60_0(FormulaExpression localSubjectExp, ref CSharpProgramTranslator.<>c__DisplayClass60_0 A_1)
		{
			return CSharpExpressionHelper.Equal(CSharpExpressionHelper.Minus(CSharpExpressionHelper.Length(localSubjectExp), CSharpExpressionHelper.Length(CSharpExpressionHelper.Replace(localSubjectExp, A_1.delimiterExp, CSharpExpressionHelper.StringLiteral(string.Empty)))), A_1.countExp);
		}

		// Token: 0x040051C0 RID: 20928
		private bool _cancelled;

		// Token: 0x040051C1 RID: 20929
		private FormulaExpression _currentInputExpression;

		// Token: 0x040051C2 RID: 20930
		private bool _includeAllIndexesOfMethod;

		// Token: 0x040051C3 RID: 20931
		private bool _includeParseDateTimeMethod;

		// Token: 0x040051C4 RID: 20932
		private bool _includeParseNumberMethod;

		// Token: 0x040051C5 RID: 20933
		private bool _includeRoundDownMethod;

		// Token: 0x040051C6 RID: 20934
		private bool _includeRoundNearestMethod;

		// Token: 0x040051C7 RID: 20935
		private bool _includeRoundUpMethod;

		// Token: 0x040051C8 RID: 20936
		private bool _includeSliceBetweenMethod;

		// Token: 0x040051C9 RID: 20937
		private bool _includeSliceMethod;

		// Token: 0x040051CA RID: 20938
		private bool _includeToTitleCaseMethod;

		// Token: 0x040051CB RID: 20939
		private readonly ICSharpTranslationOptions _options;

		// Token: 0x040051CC RID: 20940
		private List<FormulaExpression> _statements = new List<FormulaExpression>();

		// Token: 0x040051CD RID: 20941
		private readonly HashSet<string> _usings = new HashSet<string> { "System" };

		// Token: 0x040051CE RID: 20942
		private int _varCount = 1;

		// Token: 0x040051CF RID: 20943
		private Dictionary<FormulaExpression, FormulaExpression> _contextVariables = new Dictionary<FormulaExpression, FormulaExpression>();
	}
}

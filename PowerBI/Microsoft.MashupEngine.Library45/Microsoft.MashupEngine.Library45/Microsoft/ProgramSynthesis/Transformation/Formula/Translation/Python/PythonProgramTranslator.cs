using System;
using System.Collections.Generic;
using System.Globalization;
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
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Text;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200188D RID: 6285
	internal class PythonProgramTranslator : ProgramTranslatorBase
	{
		// Token: 0x0600CD5B RID: 52571 RVA: 0x002BBA5C File Offset: 0x002B9C5C
		private PythonProgramTranslator(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, IPythonTranslationOptions options, bool enableMatchUnicode, ILogger logger)
			: base(program, examples, inputs, logger)
		{
			this._options = options ?? new PythonTranslationConstraint();
			this._regexLibraryName = (enableMatchUnicode ? "regex" : "re");
		}

		// Token: 0x0600CD5C RID: 52572 RVA: 0x002BBABF File Offset: 0x002B9CBF
		public static PythonProgram Translate(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, IPythonTranslationOptions options, bool enableMatchUnicode, ILogger logger = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new PythonProgramTranslator(program, examples, inputs, options, enableMatchUnicode, logger).Translate(cancellationToken) as PythonProgram;
		}

		// Token: 0x0600CD5D RID: 52573 RVA: 0x002BBADC File Offset: 0x002B9CDC
		protected override FormulaExpression Translate(CancellationToken cancellationToken = default(CancellationToken))
		{
			this._forceDecimalNumber = base.InputColumnDetails.Any((ColumnDetail d) => d.HasDecimal);
			FormulaExpression formulaExpression = base.Translate(cancellationToken);
			if (formulaExpression == null)
			{
				return null;
			}
			this._statements.Add(formulaExpression);
			IEnumerable<string> columnNames = base.Examples.First<Example>().Input.ColumnNames;
			object obj;
			if (columnNames == null)
			{
				obj = null;
			}
			else
			{
				obj = columnNames.OrderBy((string col) => col);
			}
			object obj2 = obj;
			if (obj2 == null)
			{
				throw new Exception("ColumnNames not found");
			}
			IReadOnlyList<PythonVariable> readOnlyList = obj2.Select((string col) => (PythonVariable)PythonExpressionHelper.Variable(col, base.ResolveInputType(col))).ToList<PythonVariable>();
			PythonDefinition pythonDefinition = PythonExpressionHelper.Definition(this._options.DefinitionName, readOnlyList, this._statements, null);
			pythonDefinition = PythonExpressionOptimizer.Optimize(pythonDefinition, this._options) as PythonDefinition;
			if (pythonDefinition == null)
			{
				return null;
			}
			pythonDefinition = PythonProgramTranslator.ResolveParameterNames(pythonDefinition);
			pythonDefinition = PythonProgramTranslator.ResolveReturn(pythonDefinition);
			if (this._options.MaximumExamplesInComments > 0)
			{
				string text = this.ResolveComment();
				pythonDefinition = pythonDefinition.With(null, null, null, new PythonComment(text, PythonCommentType.DocString));
			}
			return PythonExpressionHelper.Program(this._imports.ImportStatements, pythonDefinition, null, null);
		}

		// Token: 0x0600CD5E RID: 52574 RVA: 0x002BBC1C File Offset: 0x002B9E1C
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
				FromDateTime fromDateTime;
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
				FromDateTime fromDateTime2;
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
				else if (node.Is(out letX))
				{
					formulaExpression = this.TranslateLetX(letX);
				}
				else if (node.Is(out abs))
				{
					formulaExpression = PythonProgramTranslator.TranslateAbs(abs);
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
					formulaExpression = PythonProgramTranslator.TranslateArithmeticRightNumber(node);
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
				else if (node.Is(out fromDateTime))
				{
					formulaExpression = PythonProgramTranslator.TranslateFromDateTime(fromDateTime);
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
					formulaExpression = PythonProgramTranslator.TranslateFromStr(fromStr);
				}
				else if (node.Is(out fromDateTime2))
				{
					formulaExpression = PythonProgramTranslator.TranslateFromDateTime(fromDateTime2);
				}
				else if (node.Is(out fromDateTimePart))
				{
					formulaExpression = this.TranslateFromDateTimePart(fromDateTimePart);
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
							goto IL_0695;
						}
						LiteralNode literalNode = node as LiteralNode;
						if (literalNode != null)
						{
							LiteralNode literalNode2 = literalNode;
							formulaExpression = this.TranslateLiteral(literalNode2);
							goto IL_0695;
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
						formulaExpression = PythonProgramTranslator.TranslateIsString(isString);
					}
					else if (node.Is(out isBlank))
					{
						formulaExpression = PythonProgramTranslator.TranslateIsBlank(isBlank);
					}
					else if (node.Is(out isNotBlank))
					{
						formulaExpression = PythonProgramTranslator.TranslateIsNotBlank(isNotBlank);
					}
					else if (node.Is(out stringEquals))
					{
						formulaExpression = this.TranslateStringEquals(stringEquals);
					}
					else if (node.Is(out startsWithDigit))
					{
						formulaExpression = PythonProgramTranslator.TranslateStartsWithDigit(startsWithDigit);
					}
					else if (node.Is(out endsWithDigit))
					{
						formulaExpression = PythonProgramTranslator.TranslateEndsWithDigit(endsWithDigit);
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
						formulaExpression = PythonProgramTranslator.TranslateIsNumber(isNumber);
					}
					else if (node.Is(out numberGreaterThan))
					{
						formulaExpression = PythonProgramTranslator.TranslateNumberGreaterThan(numberGreaterThan);
					}
					else if (node.Is(out numberLessThan))
					{
						formulaExpression = PythonProgramTranslator.TranslateNumberLessThan(numberLessThan);
					}
					else if (node.Is(out numberEquals))
					{
						formulaExpression = PythonProgramTranslator.TranslateNumberEquals(numberEquals);
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
						formulaExpression = PythonProgramTranslator.TranslateNull();
					}
				}
				else
				{
					formulaExpression = this.TranslateVariableNode();
				}
				IL_0695:
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

		// Token: 0x0600CD5F RID: 52575 RVA: 0x002BC334 File Offset: 0x002BA534
		private static FormulaExpression TranslateAbs(Abs abs)
		{
			int value = abs.absPos.Value;
			return PythonExpressionHelper.NumberLiteral((value > 0) ? (value - 1) : value);
		}

		// Token: 0x0600CD60 RID: 52576 RVA: 0x002BC360 File Offset: 0x002BA560
		private FormulaExpression TranslateConcat(Concat concat)
		{
			FormulaExpression formulaExpression = this.Translate(concat.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(concat.Node.Children[1], default(CancellationToken));
			return PythonExpressionHelper.Concat(this.<TranslateConcat>g__Coerce|19_0(formulaExpression), this.<TranslateConcat>g__Coerce|19_0(formulaExpression2));
		}

		// Token: 0x0600CD61 RID: 52577 RVA: 0x002BC3BC File Offset: 0x002BA5BC
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
			FormulaExpression formulaExpression3 = PythonExpressionHelper.NumberLiteral(value);
			if (num == 1)
			{
				return PythonExpressionHelper.Plus(PythonExpressionHelper.Find(formulaExpression, formulaExpression2), formulaExpression3);
			}
			if (num == -1)
			{
				return PythonExpressionHelper.Plus(PythonExpressionHelper.RFind(formulaExpression, formulaExpression2), formulaExpression3);
			}
			if (num > 0)
			{
				num--;
			}
			FormulaExpression formulaExpression4 = PythonExpressionHelper.DelimiterIndexEnumeration(formulaExpression, formulaExpression2);
			string text = "index{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			return this.AddVariable<int>(string.Format(text, varCount), PythonExpressionHelper.Plus(PythonExpressionHelper.Index<int>(formulaExpression4, PythonExpressionHelper.NumberLiteral(num)), formulaExpression3));
		}

		// Token: 0x0600CD62 RID: 52578 RVA: 0x002BC4B0 File Offset: 0x002BA6B0
		private FormulaExpression TranslateLength(Length length)
		{
			return PythonExpressionHelper.Len(this.Translate(length.fromStr.Node, default(CancellationToken)));
		}

		// Token: 0x0600CD63 RID: 52579 RVA: 0x002BC4E0 File Offset: 0x002BA6E0
		private FormulaExpression TranslateLetX(LetX letX)
		{
			this._currentInputExpression = this.Translate(letX.fromStrTrim.Node, default(CancellationToken));
			return this.Translate(letX.substring.Node, default(CancellationToken));
		}

		// Token: 0x0600CD64 RID: 52580 RVA: 0x002BC530 File Offset: 0x002BA730
		private FormulaExpression TranslateLowerCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PythonExpressionHelper.Lower(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CD65 RID: 52581 RVA: 0x002BC568 File Offset: 0x002BA768
		private FormulaExpression TranslateMatch(Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes.Match match)
		{
			FormulaExpression formulaExpression = this.Translate(match.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(match.matchDesc.Node, default(CancellationToken));
			int value = match.matchInstance.Value;
			FormulaExpression formulaExpression3 = PythonExpressionHelper.NumberLiteral((value > 0) ? (value - 1) : value);
			FormulaExpression formulaExpression4 = PythonExpressionHelper.MatchIndexEnumeration(formulaExpression, formulaExpression2, this._regexLibraryName);
			string text = "index{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression5 = this.AddVariable<int>(string.Format(text, varCount), PythonExpressionHelper.Start(PythonExpressionHelper.Index<int>(formulaExpression4, formulaExpression3)));
			this._imports.Add(this._regexLibraryName);
			return formulaExpression5;
		}

		// Token: 0x0600CD66 RID: 52582 RVA: 0x002BC62C File Offset: 0x002BA82C
		private FormulaExpression TranslateMatchEnd(MatchEnd matchEnd)
		{
			FormulaExpression formulaExpression = this.Translate(matchEnd.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(matchEnd.matchDesc.Node, default(CancellationToken));
			int value = matchEnd.matchInstance.Value;
			FormulaExpression formulaExpression3 = PythonExpressionHelper.NumberLiteral((value > 0) ? (value - 1) : value);
			FormulaExpression formulaExpression4 = PythonExpressionHelper.MatchIndexEnumeration(formulaExpression, formulaExpression2, this._regexLibraryName);
			string text = "index{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression5 = this.AddVariable<int>(string.Format(text, varCount), PythonExpressionHelper.End(PythonExpressionHelper.Index<int>(formulaExpression4, formulaExpression3)));
			this._imports.Add(this._regexLibraryName);
			return formulaExpression5;
		}

		// Token: 0x0600CD67 RID: 52583 RVA: 0x002BC6F0 File Offset: 0x002BA8F0
		private FormulaExpression TranslateMatchFull(MatchFull matchFull)
		{
			FormulaExpression formulaExpression = this.Translate(matchFull.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(matchFull.matchDesc.Node, default(CancellationToken));
			if (formulaExpression == null || formulaExpression2 == null)
			{
				return null;
			}
			int value = matchFull.matchInstance.Value;
			FormulaExpression formulaExpression3 = PythonExpressionHelper.NumberLiteral((value > 0) ? (value - 1) : value);
			FormulaExpression formulaExpression4 = PythonExpressionHelper.MatchIndexEnumeration(formulaExpression, formulaExpression2, this._regexLibraryName);
			string text = "match{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			FormulaExpression formulaExpression5 = this.AddVariable<int>(string.Format(text, varCount), PythonExpressionHelper.Group(PythonExpressionHelper.Index<int>(formulaExpression4, formulaExpression3)));
			this._imports.Add(this._regexLibraryName);
			return formulaExpression5;
		}

		// Token: 0x0600CD68 RID: 52584 RVA: 0x002BC7CC File Offset: 0x002BA9CC
		private FormulaExpression TranslateProperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PythonExpressionHelper.Title(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CD69 RID: 52585 RVA: 0x002BC804 File Offset: 0x002BAA04
		private FormulaExpression TranslateReplace(Replace replace)
		{
			return PythonExpressionHelper.Replace(this.Translate(replace.fromStr.Node, default(CancellationToken)), this.Translate(replace.replaceFindText.Node, default(CancellationToken)), this.Translate(replace.replaceText.Node, default(CancellationToken)));
		}

		// Token: 0x0600CD6A RID: 52586 RVA: 0x002BC870 File Offset: 0x002BAA70
		private FormulaExpression TranslateSlice(Slice slice)
		{
			FormulaExpression formulaExpression = this.Translate(slice.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slice.pos1.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(slice.pos2.Node, default(CancellationToken));
			return PythonExpressionHelper.Index<string>(formulaExpression, PythonExpressionHelper.IndexRange(formulaExpression2, formulaExpression3));
		}

		// Token: 0x0600CD6B RID: 52587 RVA: 0x002BC8E8 File Offset: 0x002BAAE8
		private FormulaExpression TranslateSliceBetween(SliceBetween sliceBetween)
		{
			FormulaExpression formulaExpression = this.Translate(sliceBetween.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(sliceBetween.sliceBetweenStartText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(sliceBetween.sliceBetweenEndText.Node, default(CancellationToken));
			FormulaExpression formulaExpression4 = this.AddVariable<string>("start_text", formulaExpression2);
			FormulaExpression formulaExpression5 = PythonExpressionHelper.Plus(PythonExpressionHelper.Find(formulaExpression, formulaExpression4), PythonExpressionHelper.Len(formulaExpression4));
			FormulaExpression formulaExpression6 = this.AddVariable<int>("start_index", formulaExpression5);
			FormulaExpression formulaExpression7 = PythonExpressionHelper.Find(formulaExpression, formulaExpression3, formulaExpression6);
			FormulaExpression formulaExpression8 = this.AddVariable<int>("end_index", formulaExpression7);
			return PythonExpressionHelper.Index<string>(formulaExpression, PythonExpressionHelper.IndexRange(formulaExpression6, formulaExpression8));
		}

		// Token: 0x0600CD6C RID: 52588 RVA: 0x002BC9B0 File Offset: 0x002BABB0
		private FormulaExpression TranslateSlicePrefix(SlicePrefix slicePrefix)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slicePrefix.pos.Node, default(CancellationToken));
			return PythonExpressionHelper.Index<string>(formulaExpression, PythonExpressionHelper.IndexRange(null, formulaExpression2));
		}

		// Token: 0x0600CD6D RID: 52589 RVA: 0x002BCA08 File Offset: 0x002BAC08
		private FormulaExpression TranslateSlicePrefixAbs(SlicePrefixAbs slicePrefixAbs)
		{
			return PythonExpressionHelper.Index<string>(this.Translate(slicePrefixAbs.x.Node, default(CancellationToken)), PythonExpressionHelper.IndexRange(null, PythonExpressionHelper.NumberLiteral(slicePrefixAbs.slicePrefixAbsPos.Value - 1)));
		}

		// Token: 0x0600CD6E RID: 52590 RVA: 0x002BCA54 File Offset: 0x002BAC54
		private FormulaExpression TranslateSliceSuffix(SliceSuffix sliceSuffix)
		{
			FormulaExpression formulaExpression = this.Translate(sliceSuffix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(sliceSuffix.pos.Node, default(CancellationToken));
			return PythonExpressionHelper.Index<string>(formulaExpression, PythonExpressionHelper.IndexRange(formulaExpression2, null));
		}

		// Token: 0x0600CD6F RID: 52591 RVA: 0x002BCAAC File Offset: 0x002BACAC
		private FormulaExpression TranslateSplit(Split split)
		{
			FormulaExpression formulaExpression = this.Translate(split.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(split.splitDelimiter.Node, default(CancellationToken));
			int num = split.splitInstance.Value;
			if (num > 0)
			{
				num--;
			}
			return PythonExpressionHelper.Index<string>(PythonExpressionHelper.Split(formulaExpression, formulaExpression2), PythonExpressionHelper.NumberLiteral(num));
		}

		// Token: 0x0600CD70 RID: 52592 RVA: 0x002BCB24 File Offset: 0x002BAD24
		private FormulaExpression TranslateStr(Str str)
		{
			return this.Translate(str.constStr.Node, default(CancellationToken));
		}

		// Token: 0x0600CD71 RID: 52593 RVA: 0x002BCB50 File Offset: 0x002BAD50
		private FormulaExpression TranslateTrim(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PythonExpressionHelper.Strip(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CD72 RID: 52594 RVA: 0x002BCB88 File Offset: 0x002BAD88
		private FormulaExpression TranslateUpperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return PythonExpressionHelper.Upper(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CD73 RID: 52595 RVA: 0x002BCBBE File Offset: 0x002BADBE
		private FormulaExpression TranslateVariableNode()
		{
			return this._currentInputExpression;
		}

		// Token: 0x0600CD74 RID: 52596 RVA: 0x002BCBC8 File Offset: 0x002BADC8
		private FormulaExpression TranslateFormatNumber(FormatNumber formatNumber)
		{
			FormulaExpression formulaExpression = this.Translate(formatNumber.Node.Children[0], default(CancellationToken));
			FormatNumberDescriptor value = formatNumber.numberFormatDesc.Value;
			string text = (value.IncludeGroupSeparator ? "," : "");
			PythonVariable pythonVariable = formulaExpression as PythonVariable;
			FormulaExpression formulaExpression2;
			if (pythonVariable != null)
			{
				formulaExpression2 = pythonVariable;
			}
			else
			{
				PythonFunc pythonFunc = formulaExpression as PythonFunc;
				if (pythonFunc != null)
				{
					if (formulaExpression.NodeDetails.Max((FormulaExpressionDetail i) => i.Depth) == 1)
					{
						formulaExpression2 = pythonFunc;
						goto IL_00C6;
					}
				}
				string text2 = "number{0}";
				int varCount = this._varCount;
				this._varCount = varCount + 1;
				formulaExpression2 = PythonExpressionHelper.Variable<double>(string.Format(text2, varCount));
				this.AddVariable(formulaExpression2, formulaExpression);
			}
			IL_00C6:
			if (value.IncludePercentSymbol)
			{
				formulaExpression2 = PythonExpressionHelper.Multiply(formulaExpression2, 100);
			}
			int num = value.LeadingDigits + value.TrailingDigits + ((value.IncludeDecimalSeparator > false) ? 1 : 0) + ((value.IncludeGroupSeparator && value.LeadingDigits > 3) ? 1 : 0);
			string text3 = string.Format("0{0}{1}.{2}f", num, text, value.TrailingDigits);
			FormulaExpression formulaExpression3 = formulaExpression2;
			FormulaExpression formulaExpression4 = PythonExpressionHelper.Raw(text3);
			FormatNumberSymbolDescriptor symbol = value.Symbol;
			string text4 = ((symbol != null) ? symbol.Text : null);
			FormatNumberSymbolDescriptor symbol2 = value.Symbol;
			FormulaExpression formulaExpression5 = PythonExpressionHelper.InterpolatedFormat(formulaExpression3, formulaExpression4, text4, symbol2 != null && symbol2.Prefix);
			string text5 = value.GroupSeparator.Replace("\u00a0", " ");
			bool flag = value.GroupSeparator != ",";
			bool flag2 = flag && value.GroupSeparator == "." && value.DecimalSeparator == ",";
			bool flag3 = value.DecimalSeparator != ".";
			FormulaExpression formulaExpression6 = null;
			if (flag)
			{
				formulaExpression6 = PythonExpressionHelper.StringLiteral(flag2 ? "g" : text5);
				formulaExpression5 = PythonExpressionHelper.Replace(formulaExpression5, PythonExpressionHelper.StringLiteral(","), formulaExpression6);
			}
			if (flag3)
			{
				formulaExpression5 = PythonExpressionHelper.Replace(formulaExpression5, PythonExpressionHelper.StringLiteral("."), PythonExpressionHelper.StringLiteral(value.DecimalSeparator));
			}
			if (flag2)
			{
				formulaExpression5 = PythonExpressionHelper.Replace(formulaExpression5, formulaExpression6, PythonExpressionHelper.StringLiteral(text5));
			}
			return formulaExpression5;
		}

		// Token: 0x0600CD75 RID: 52597 RVA: 0x002BCDF8 File Offset: 0x002BAFF8
		private static FormulaExpression TranslateArithmeticRightNumber(ProgramNode node)
		{
			decimal num;
			if (!node.IsArithmeticRightNumber(out num))
			{
				return null;
			}
			return PythonExpressionHelper.NumberLiteral(num);
		}

		// Token: 0x0600CD76 RID: 52598 RVA: 0x002BCE18 File Offset: 0x002BB018
		private FormulaExpression TranslateNumber(Number number)
		{
			return this.Translate(number.constNum.Node, default(CancellationToken));
		}

		// Token: 0x0600CD77 RID: 52599 RVA: 0x002BCE44 File Offset: 0x002BB044
		private FormulaExpression TranslateParseNumber(ParseNumber parseNumber)
		{
			FormulaExpression formulaExpression = this.Translate(parseNumber.Node.Children[0], default(CancellationToken));
			if (formulaExpression == null)
			{
				return null;
			}
			NumberFormatInfo numberFormat = new CultureInfo(parseNumber.locale.Value).NumberFormat;
			formulaExpression = PythonExpressionHelper.Replace(formulaExpression, PythonExpressionHelper.StringLiteral(numberFormat.NumberGroupSeparator), PythonExpressionHelper.StringLiteral(""));
			if (numberFormat.NumberDecimalSeparator != ".")
			{
				formulaExpression = PythonExpressionHelper.Replace(formulaExpression, PythonExpressionHelper.StringLiteral(numberFormat.NumberDecimalSeparator), PythonExpressionHelper.StringLiteral("."));
			}
			return PythonExpressionHelper.Float(formulaExpression);
		}

		// Token: 0x0600CD78 RID: 52600 RVA: 0x002BCEE4 File Offset: 0x002BB0E4
		private FormulaExpression TranslateRoundNumber(RoundNumber roundNumber)
		{
			PythonProgramTranslator.<>c__DisplayClass43_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.roundNumber = roundNumber;
			CS$<>8__locals1.source = this.Translate(CS$<>8__locals1.roundNumber.inumber.Node, default(CancellationToken));
			if (CS$<>8__locals1.source == null)
			{
				return null;
			}
			CS$<>8__locals1.source = this.ResolveType<decimal>(CS$<>8__locals1.source);
			if (!(CS$<>8__locals1.source is PythonVariable))
			{
				CS$<>8__locals1.source = this.AddVariable<decimal>("source", CS$<>8__locals1.source);
			}
			FormulaExpression formulaExpression = null;
			FormulaExpression formulaExpression2;
			if (this._options.UseNumpy && CS$<>8__locals1.roundNumber.numberRoundDesc.Value.Delta == 1.0)
			{
				this._imports.Add("numpy", "np");
				RoundingMode mode = CS$<>8__locals1.roundNumber.numberRoundDesc.Value.Mode;
				if (mode != RoundingMode.Down)
				{
					if (mode != RoundingMode.Up)
					{
						formulaExpression2 = null;
					}
					else
					{
						formulaExpression2 = PythonExpressionHelper.DotFunc<decimal>("np", "ceil", new FormulaExpression[] { CS$<>8__locals1.source });
					}
				}
				else
				{
					formulaExpression2 = PythonExpressionHelper.DotFunc<decimal>("np", "floor", new FormulaExpression[] { CS$<>8__locals1.source });
				}
				formulaExpression = formulaExpression2;
			}
			if (formulaExpression != null)
			{
				return formulaExpression;
			}
			switch (CS$<>8__locals1.roundNumber.numberRoundDesc.Value.Mode)
			{
			case RoundingMode.Nearest:
				formulaExpression2 = this.<TranslateRoundNumber>g__AddRound|43_0("ROUND_HALF_UP", ref CS$<>8__locals1);
				break;
			case RoundingMode.Down:
				formulaExpression2 = this.<TranslateRoundNumber>g__AddRound|43_0("ROUND_FLOOR", ref CS$<>8__locals1);
				break;
			case RoundingMode.Up:
				formulaExpression2 = this.<TranslateRoundNumber>g__AddRound|43_0("ROUND_CEILING", ref CS$<>8__locals1);
				break;
			default:
				formulaExpression2 = null;
				break;
			}
			return formulaExpression2;
		}

		// Token: 0x0600CD79 RID: 52601 RVA: 0x002BD0A8 File Offset: 0x002BB2A8
		private FormulaExpression TranslateDate(Date date)
		{
			return this.Translate(date.constDt.Node, default(CancellationToken));
		}

		// Token: 0x0600CD7A RID: 52602 RVA: 0x002BD0D4 File Offset: 0x002BB2D4
		private FormulaExpression TranslateDateTimePart(DateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = this.Translate(dateTimePart.Node.Children[0], default(CancellationToken));
			DateTimePartKind value = dateTimePart.dateTimePartKind.Value;
			if (value == DateTimePartKind.QuarterDays)
			{
				FormulaExpression formulaExpression2 = this.DateQuarter(formulaExpression, false);
				FormulaExpression formulaExpression3 = this.DateQuarterStart(formulaExpression, formulaExpression2, false);
				return PythonExpressionHelper.Dot<int>(PythonExpressionHelper.Minus(this.DateQuarterEnd(formulaExpression, formulaExpression2, formulaExpression3, false), formulaExpression3), "days");
			}
			if (value == DateTimePartKind.QuarterDay)
			{
				FormulaExpression formulaExpression4 = this.DateQuarterStart(formulaExpression, null, false);
				return PythonExpressionHelper.Plus1(PythonExpressionHelper.Dot<int>(PythonExpressionHelper.Minus(formulaExpression, formulaExpression4), "days"));
			}
			if (value == DateTimePartKind.QuarterWeek)
			{
				this._imports.Add("math");
				FormulaExpression formulaExpression5 = this.DateQuarterStart(formulaExpression, null, false);
				return this.DateWeekCount(formulaExpression, formulaExpression5, true);
			}
			if (value == DateTimePartKind.MonthWeek)
			{
				this._imports.Add("datetime", new string[] { "datetime" });
				this._imports.Add("math");
				FormulaExpression formulaExpression6 = this.AddVariable<DateTime>("month_start", PythonExpressionHelper.MonthStart(formulaExpression));
				return this.DateWeekCount(formulaExpression, formulaExpression6, true);
			}
			bool flag = value == DateTimePartKind.MonthDays || value == DateTimePartKind.YearDays;
			if (flag)
			{
				this._imports.Add("calendar");
			}
			switch (value)
			{
			case DateTimePartKind.Second:
				return PythonExpressionHelper.Second(formulaExpression);
			case DateTimePartKind.Minute:
				return PythonExpressionHelper.Minute(formulaExpression);
			case DateTimePartKind.Hour:
				return PythonExpressionHelper.Hour(formulaExpression);
			case DateTimePartKind.WeekDay:
				return PythonExpressionHelper.WeekDay(formulaExpression);
			case DateTimePartKind.MonthDay:
				return PythonExpressionHelper.Day(formulaExpression);
			case DateTimePartKind.MonthDays:
				return PythonExpressionHelper.MonthDays(formulaExpression);
			case DateTimePartKind.Month:
				return PythonExpressionHelper.Month(formulaExpression);
			case DateTimePartKind.Quarter:
				return this.DateQuarter(formulaExpression, true);
			case DateTimePartKind.YearDay:
				return PythonExpressionHelper.YearDay(formulaExpression);
			case DateTimePartKind.YearWeek:
				return PythonExpressionHelper.YearWeek(formulaExpression);
			case DateTimePartKind.YearDays:
				return PythonExpressionHelper.YearDays(formulaExpression);
			case DateTimePartKind.Year:
				return PythonExpressionHelper.Year(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CD7B RID: 52603 RVA: 0x002BD2D8 File Offset: 0x002BB4D8
		private FormulaExpression TranslateFormatDateTime(FormatDateTime formatDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(formatDateTime.Node.Children[0], default(CancellationToken));
			string text = formatDateTime.dateTimeFormatDesc.Value.Mask;
			this._imports.Add("datetime", new string[] { "datetime" });
			text = PythonProgramTranslator.ResolveDateTimeFormat(text);
			FormulaExpression formulaExpression2 = (text.Contains("%#") ? this.AddDateFormatVariable(text) : PythonExpressionHelper.StringLiteral(text));
			return PythonExpressionHelper.StrfTime(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CD7C RID: 52604 RVA: 0x002BD364 File Offset: 0x002BB564
		private FormulaExpression TranslateParseDateTime(ParseDateTime parseDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(parseDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = parseDateTime.dateTimeParseDesc.Value;
			if (formulaExpression == null)
			{
				return null;
			}
			this._imports.Add("datetime", new string[] { "datetime" });
			string text = PythonProgramTranslator.ResolveDateTimeFormat(value.Mask);
			text = text.Replace("%#d", "%d").Replace("%#m", "%m").Replace("%#I", "%I");
			string text2 = "date{0}";
			int varCount = this._varCount;
			this._varCount = varCount + 1;
			return this.AddVariable<DateTime>(string.Format(text2, varCount), PythonExpressionHelper.StrpTime(formulaExpression, PythonExpressionHelper.StringLiteral(text)));
		}

		// Token: 0x0600CD7D RID: 52605 RVA: 0x002BD43C File Offset: 0x002BB63C
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

		// Token: 0x0600CD7E RID: 52606 RVA: 0x002BD494 File Offset: 0x002BB694
		private FormulaExpression TranslateRoundDateTimeDown(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimePeriod period = roundDateTime.dateTimeRoundDesc.Value.Period;
			this._imports.Add("datetime", new string[] { "datetime" });
			if (period == RoundDateTimePeriod.Week)
			{
				this._imports.Add("dateutil.relativedelta", new string[] { "relativedelta" });
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
				formulaExpression3 = PythonExpressionHelper.SecondStart(formulaExpression);
				goto IL_00FE;
			case RoundDateTimePeriod.Minute:
				formulaExpression3 = PythonExpressionHelper.MinuteStart(formulaExpression);
				goto IL_00FE;
			case RoundDateTimePeriod.Hour:
				formulaExpression3 = PythonExpressionHelper.HourStart(formulaExpression);
				goto IL_00FE;
			case RoundDateTimePeriod.Day:
				formulaExpression3 = PythonExpressionHelper.DayStart(formulaExpression);
				goto IL_00FE;
			case RoundDateTimePeriod.Week:
				formulaExpression3 = PythonExpressionHelper.WeekStart(formulaExpression);
				goto IL_00FE;
			case RoundDateTimePeriod.Month:
				formulaExpression3 = PythonExpressionHelper.MonthStart(formulaExpression);
				goto IL_00FE;
			case RoundDateTimePeriod.Year:
				formulaExpression3 = PythonExpressionHelper.YearStart(formulaExpression);
				goto IL_00FE;
			}
			formulaExpression3 = null;
			IL_00FE:
			FormulaExpression formulaExpression4 = formulaExpression3;
			return this.AddVariable<DateTime>(period.ToString().ToLower() + "_down", formulaExpression4);
		}

		// Token: 0x0600CD7F RID: 52607 RVA: 0x002BD5C8 File Offset: 0x002BB7C8
		private FormulaExpression TranslateRoundDateTimeNearest(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimePeriod period = roundDateTime.dateTimeRoundDesc.Value.Period;
			this._imports.Add("datetime", new string[] { "datetime" });
			this._imports.Add("dateutil.relativedelta", new string[] { "relativedelta" });
			FormulaExpression formulaExpression2 = null;
			FormulaExpression formulaExpression3 = null;
			FormulaExpression formulaExpression4 = null;
			if (period == RoundDateTimePeriod.Second)
			{
				formulaExpression2 = this.AddVariable<DateTime>("second_start", PythonExpressionHelper.SecondStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("second_end", PythonExpressionHelper.SecondEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("second_midpoint", PythonExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Minute)
			{
				formulaExpression2 = this.AddVariable<DateTime>("minute_start", PythonExpressionHelper.MinuteStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("minute_end", PythonExpressionHelper.MinuteEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("minute_midpoint", PythonExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Hour)
			{
				formulaExpression2 = this.AddVariable<DateTime>("hour_start", PythonExpressionHelper.HourStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("hour_end", PythonExpressionHelper.HourEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("hour_midpoint", PythonExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Day)
			{
				formulaExpression2 = this.AddVariable<DateTime>("day_start", PythonExpressionHelper.DayStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("day_end", PythonExpressionHelper.DayEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("day_midpoint", PythonExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Week)
			{
				formulaExpression2 = this.AddVariable<DateTime>("week_start", PythonExpressionHelper.WeekStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("week_end", PythonExpressionHelper.WeekEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("week_midpoint", PythonExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Month)
			{
				formulaExpression2 = this.AddVariable<DateTime>("month_start", PythonExpressionHelper.MonthStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("month_end", PythonExpressionHelper.MonthEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("month_midpoint", PythonExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Quarter)
			{
				FormulaExpression formulaExpression5 = this.DateQuarter(formulaExpression, false);
				formulaExpression2 = this.DateQuarterStart(formulaExpression, formulaExpression5, false);
				formulaExpression3 = this.DateQuarterEnd(formulaExpression, formulaExpression5, formulaExpression2, false);
				formulaExpression4 = this.AddVariable<DateTime>("quarter_midpoint", PythonExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (period == RoundDateTimePeriod.Year)
			{
				formulaExpression2 = this.AddVariable<DateTime>("year_start", PythonExpressionHelper.YearStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("year_end", PythonExpressionHelper.YearEnd(formulaExpression2));
				formulaExpression4 = this.AddVariable<DateTime>("year_midpoint", PythonExpressionHelper.Midpoint(formulaExpression2, formulaExpression3));
			}
			if (formulaExpression2 == null || formulaExpression3 == null || formulaExpression4 == null)
			{
				return null;
			}
			FormulaExpression formulaExpression6 = PythonExpressionHelper.Ternary(PythonExpressionHelper.LessThan(formulaExpression, formulaExpression4), formulaExpression2, formulaExpression3);
			return this.AddVariable<DateTime>(period.ToString().ToLower() + "_nearest", formulaExpression6);
		}

		// Token: 0x0600CD80 RID: 52608 RVA: 0x002BD87C File Offset: 0x002BBA7C
		private FormulaExpression TranslateRoundDateTimeUp(RoundDateTime roundDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(roundDateTime.Node.Children[0], default(CancellationToken));
			RoundDateTimeDescriptor value = roundDateTime.dateTimeRoundDesc.Value;
			RoundDateTimePeriod period = value.Period;
			bool flag = value.Ceiling == RoundDatePeriodCeiling.LastDay;
			this._imports.Add("datetime", new string[] { "datetime" });
			this._imports.Add("dateutil.relativedelta", new string[] { "relativedelta" });
			FormulaExpression formulaExpression2 = null;
			FormulaExpression formulaExpression3 = null;
			if (period == RoundDateTimePeriod.Second)
			{
				formulaExpression2 = this.AddVariable<DateTime>("second_start", PythonExpressionHelper.SecondStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("second_end", PythonExpressionHelper.SecondEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Minute)
			{
				formulaExpression2 = this.AddVariable<DateTime>("minute_start", PythonExpressionHelper.MinuteStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("minute_end", PythonExpressionHelper.MinuteEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Hour)
			{
				formulaExpression2 = this.AddVariable<DateTime>("hour_start", PythonExpressionHelper.HourStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("hour_end", PythonExpressionHelper.HourEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Day)
			{
				formulaExpression2 = this.AddVariable<DateTime>("day_start", PythonExpressionHelper.DayStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("day_end", PythonExpressionHelper.DayEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Week)
			{
				formulaExpression2 = this.AddVariable<DateTime>("week_start", PythonExpressionHelper.WeekStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("week_end", PythonExpressionHelper.WeekEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Month)
			{
				formulaExpression2 = this.AddVariable<DateTime>("month_start", PythonExpressionHelper.MonthStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("month_end", PythonExpressionHelper.MonthEnd(formulaExpression2));
			}
			if (period == RoundDateTimePeriod.Quarter)
			{
				FormulaExpression formulaExpression4 = this.DateQuarter(formulaExpression, false);
				formulaExpression2 = this.DateQuarterStart(formulaExpression, formulaExpression4, false);
				formulaExpression3 = this.DateQuarterEnd(formulaExpression, formulaExpression4, formulaExpression2, false);
			}
			if (period == RoundDateTimePeriod.Year)
			{
				formulaExpression2 = this.AddVariable<DateTime>("year_start", PythonExpressionHelper.YearStart(formulaExpression));
				formulaExpression3 = this.AddVariable<DateTime>("year_end", PythonExpressionHelper.YearEnd(formulaExpression2));
			}
			if (formulaExpression2 == null || formulaExpression3 == null)
			{
				return null;
			}
			FormulaExpression formulaExpression5;
			if (!flag)
			{
				formulaExpression5 = PythonExpressionHelper.Ternary(PythonExpressionHelper.NotEqual(formulaExpression, formulaExpression2), formulaExpression3, formulaExpression2);
			}
			else
			{
				FormulaExpression formulaExpression6 = formulaExpression3;
				double? num = new double?((double)1);
				formulaExpression5 = PythonExpressionHelper.Minus(formulaExpression6, PythonExpressionHelper.RelativeDelta(null, null, num, null, null, null, null));
			}
			FormulaExpression formulaExpression7 = formulaExpression5;
			return this.AddVariable<DateTime>(period.ToString().ToLower() + "_up", formulaExpression7);
		}

		// Token: 0x0600CD81 RID: 52609 RVA: 0x002BDAF0 File Offset: 0x002BBCF0
		private static FormulaExpression TranslateFromDateTime(FromDateTime dateTime)
		{
			return PythonExpressionHelper.Variable<DateTime>(dateTime.columnName.Value);
		}

		// Token: 0x0600CD82 RID: 52610 RVA: 0x002BDB14 File Offset: 0x002BBD14
		private FormulaExpression TranslateFromDateTimePart(FromDateTimePart dateTimePart)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(dateTimePart.columnName.Value);
			this._imports.Add("datetime", new string[] { "datetime" });
			DateTimePartKind value = dateTimePart.fromDateTimePartKind.Value;
			FormulaExpression formulaExpression2;
			if (value != DateTimePartKind.Month)
			{
				if (value == DateTimePartKind.Year)
				{
					formulaExpression2 = PythonExpressionHelper.DateTime(formulaExpression, 1, 1);
				}
				else
				{
					formulaExpression2 = null;
				}
			}
			else
			{
				formulaExpression2 = PythonExpressionHelper.DateTime(2000, formulaExpression, 1);
			}
			return formulaExpression2;
		}

		// Token: 0x0600CD83 RID: 52611 RVA: 0x002BDB8C File Offset: 0x002BBD8C
		private FormulaExpression TranslateFromNumber(FromNumber fromNumber)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(fromNumber.columnName.Value, base.ResolveInputType(fromNumber.columnName.Value));
			if (!this._forceDecimalNumber)
			{
				return formulaExpression;
			}
			this._imports.Add("decimal", new string[] { "Decimal" });
			return PythonExpressionHelper.Decimal(formulaExpression, true);
		}

		// Token: 0x0600CD84 RID: 52612 RVA: 0x002BDBF4 File Offset: 0x002BBDF4
		private IEnumerable<FormulaExpression> TranslateFromNumbers(fromNumbers fromNumbers)
		{
			LiteralNode literalNode = fromNumbers.Node.Children[1] as LiteralNode;
			string[] array = ((literalNode != null) ? literalNode.Value : null) as string[];
			if (array != null)
			{
				return array.Select(delegate(string columnName)
				{
					FormulaExpression formulaExpression = PythonExpressionHelper.Variable(columnName, base.ResolveInputType(columnName));
					if (!this._forceDecimalNumber)
					{
						return formulaExpression;
					}
					this._imports.Add("decimal", new string[] { "Decimal" });
					return PythonExpressionHelper.Decimal(formulaExpression, true);
				});
			}
			return null;
		}

		// Token: 0x0600CD85 RID: 52613 RVA: 0x002BDC40 File Offset: 0x002BBE40
		private static FormulaExpression TranslateFromStr(FromStr input)
		{
			return PythonExpressionHelper.Variable<string>(input.columnName.Value);
		}

		// Token: 0x0600CD86 RID: 52614 RVA: 0x002BDC64 File Offset: 0x002BBE64
		private FormulaExpression TranslateToDateTime(ToDateTime subject)
		{
			this._imports.Add("datetime", new string[] { "datetime" });
			return this.ResolveType<DateTime>(this.Translate(subject.outDate.Node, default(CancellationToken)));
		}

		// Token: 0x0600CD87 RID: 52615 RVA: 0x002BDCB4 File Offset: 0x002BBEB4
		private FormulaExpression TranslateToDecimal(ToDecimal subject)
		{
			return this.ResolveType<decimal>(this.Translate(subject.outNumber.Node, default(CancellationToken)));
		}

		// Token: 0x0600CD88 RID: 52616 RVA: 0x002BDCE8 File Offset: 0x002BBEE8
		private FormulaExpression TranslateToDouble(ToDouble subject)
		{
			return this.ResolveType<double>(this.Translate(subject.outNumber.Node, default(CancellationToken)));
		}

		// Token: 0x0600CD89 RID: 52617 RVA: 0x002BDD1C File Offset: 0x002BBF1C
		private FormulaExpression TranslateToInt(ToInt subject)
		{
			return this.ResolveType<int>(this.Translate(subject.outNumber.Node, default(CancellationToken)));
		}

		// Token: 0x0600CD8A RID: 52618 RVA: 0x002BDD50 File Offset: 0x002BBF50
		private FormulaExpression TranslateToStr(ToStr subject)
		{
			return this.ResolveType<string>(this.Translate(subject.outStr.Node, default(CancellationToken)));
		}

		// Token: 0x0600CD8B RID: 52619 RVA: 0x002BDD84 File Offset: 0x002BBF84
		private FormulaExpression TranslateContains(Contains contains)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(contains.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(contains.containsFindText.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(contains.containsCount.Node, default(CancellationToken));
			return PythonExpressionHelper.Equal(PythonExpressionHelper.Minus(PythonExpressionHelper.Len(formulaExpression), PythonExpressionHelper.Len(PythonExpressionHelper.Replace(formulaExpression, formulaExpression2, PythonExpressionHelper.StringLiteral(string.Empty)))), formulaExpression3);
		}

		// Token: 0x0600CD8C RID: 52620 RVA: 0x002BDE10 File Offset: 0x002BC010
		private FormulaExpression TranslateContainsMatch(ContainsMatch isMatch)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(isMatch.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(isMatch.containsMatchRegex.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(isMatch.matchCount.Node, default(CancellationToken));
			this._imports.Add(this._regexLibraryName);
			return PythonExpressionHelper.Equal(PythonExpressionHelper.Len(PythonExpressionHelper.MatchFindAll(formulaExpression, formulaExpression2, this._regexLibraryName)), formulaExpression3);
		}

		// Token: 0x0600CD8D RID: 52621 RVA: 0x002BDE9C File Offset: 0x002BC09C
		private static FormulaExpression TranslateEndsWithDigit(EndsWithDigit endsWithDigit)
		{
			return PythonExpressionHelper.IsNumeric(PythonExpressionHelper.Index<string>(PythonExpressionHelper.Variable(endsWithDigit.columnName.Value), -1));
		}

		// Token: 0x0600CD8E RID: 52622 RVA: 0x002BDEC8 File Offset: 0x002BC0C8
		private FormulaExpression TranslateIf(If ifNode)
		{
			FormulaExpression formulaExpression = this.Translate(ifNode.condition.Node, default(CancellationToken));
			List<FormulaExpression> statements = this._statements;
			this._statements = new List<FormulaExpression>();
			FormulaExpression formulaExpression2 = this.Translate(ifNode.result1.Node, default(CancellationToken));
			this._statements.Add(formulaExpression2);
			PythonBlock pythonBlock = new PythonBlock(this._statements);
			this._statements = new List<FormulaExpression>();
			FormulaExpression formulaExpression3 = this.Translate(ifNode.result2.Node, default(CancellationToken));
			this._statements.Add(formulaExpression3);
			PythonBlock pythonBlock2 = new PythonBlock(this._statements);
			this._statements = statements;
			return PythonExpressionHelper.If(formulaExpression, pythonBlock, pythonBlock2);
		}

		// Token: 0x0600CD8F RID: 52623 RVA: 0x002BDF94 File Offset: 0x002BC194
		private static FormulaExpression TranslateIsBlank(IsBlank isBlank)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(isBlank.columnName.Value);
			return PythonExpressionHelper.Or(PythonExpressionHelper.Is(formulaExpression, PythonExpressionHelper.None()), PythonExpressionHelper.Not(PythonExpressionHelper.Str(formulaExpression)));
		}

		// Token: 0x0600CD90 RID: 52624 RVA: 0x002BDFD4 File Offset: 0x002BC1D4
		private FormulaExpression TranslateIsMatch(IsMatch isMatch)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(isMatch.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(isMatch.isMatchRegex.Node, default(CancellationToken));
			this._imports.Add(this._regexLibraryName);
			return PythonExpressionHelper.MatchSearch(formulaExpression, formulaExpression2, this._regexLibraryName);
		}

		// Token: 0x0600CD91 RID: 52625 RVA: 0x002BE034 File Offset: 0x002BC234
		private static FormulaExpression TranslateIsNotBlank(IsNotBlank isNotBlank)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(isNotBlank.columnName.Value);
			return PythonExpressionHelper.Not(PythonExpressionHelper.Or(PythonExpressionHelper.Is(formulaExpression, PythonExpressionHelper.None()), PythonExpressionHelper.And(PythonExpressionHelper.IsInstanceStr(formulaExpression), PythonExpressionHelper.Not(PythonExpressionHelper.Strip(formulaExpression)))));
		}

		// Token: 0x0600CD92 RID: 52626 RVA: 0x002BE084 File Offset: 0x002BC284
		private static FormulaExpression TranslateIsNumber(IsNumber isNumber)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(isNumber.columnName.Value);
			return PythonExpressionHelper.Or(PythonExpressionHelper.IsInstanceInt(formulaExpression), PythonExpressionHelper.IsInstanceFloat(formulaExpression));
		}

		// Token: 0x0600CD93 RID: 52627 RVA: 0x002BE0B8 File Offset: 0x002BC2B8
		private static FormulaExpression TranslateIsString(IsString isString)
		{
			return PythonExpressionHelper.IsInstanceStr(PythonExpressionHelper.Variable(isString.columnName.Value));
		}

		// Token: 0x0600CD94 RID: 52628 RVA: 0x002BE0DE File Offset: 0x002BC2DE
		private static FormulaExpression TranslateNull()
		{
			return PythonExpressionHelper.None();
		}

		// Token: 0x0600CD95 RID: 52629 RVA: 0x002BE0E8 File Offset: 0x002BC2E8
		private static FormulaExpression TranslateNumberEquals(NumberEquals numberEquals)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(numberEquals.columnName.Value);
			LiteralNode literalNode = numberEquals.numberEqualsValue.Node as LiteralNode;
			if (literalNode != null)
			{
				object value = literalNode.Value;
				if (value is decimal)
				{
					decimal num = (decimal)value;
					FormulaExpression formulaExpression2 = PythonExpressionHelper.NumberLiteral(num);
					return PythonExpressionHelper.Equal(formulaExpression, formulaExpression2);
				}
			}
			return null;
		}

		// Token: 0x0600CD96 RID: 52630 RVA: 0x002BE154 File Offset: 0x002BC354
		private static FormulaExpression TranslateNumberGreaterThan(NumberGreaterThan numberGreaterThan)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(numberGreaterThan.columnName.Value);
			LiteralNode literalNode = numberGreaterThan.numberGreaterThanValue.Node as LiteralNode;
			if (literalNode != null)
			{
				object value = literalNode.Value;
				if (value is decimal)
				{
					decimal num = (decimal)value;
					FormulaExpression formulaExpression2 = PythonExpressionHelper.NumberLiteral(num);
					return PythonExpressionHelper.GreaterThan(formulaExpression, formulaExpression2);
				}
			}
			return null;
		}

		// Token: 0x0600CD97 RID: 52631 RVA: 0x002BE1C0 File Offset: 0x002BC3C0
		private static FormulaExpression TranslateNumberLessThan(NumberLessThan numberLessThan)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(numberLessThan.columnName.Value);
			LiteralNode literalNode = numberLessThan.numberLessThanValue.Node as LiteralNode;
			if (literalNode != null)
			{
				object value = literalNode.Value;
				if (value is decimal)
				{
					decimal num = (decimal)value;
					FormulaExpression formulaExpression2 = PythonExpressionHelper.NumberLiteral(num);
					return PythonExpressionHelper.LessThan(formulaExpression, formulaExpression2);
				}
			}
			return null;
		}

		// Token: 0x0600CD98 RID: 52632 RVA: 0x002BE22C File Offset: 0x002BC42C
		private FormulaExpression TranslateOr(Or or)
		{
			FormulaExpression formulaExpression = this.Translate(or.condition1.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(or.condition2.Node, default(CancellationToken));
			return PythonExpressionHelper.Or(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CD99 RID: 52633 RVA: 0x002BE27C File Offset: 0x002BC47C
		private FormulaExpression TranslateStartsWith(StartsWith startsWith)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(startsWith.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(startsWith.startsWithFindText.Node, default(CancellationToken));
			return PythonExpressionHelper.StartsWith(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CD9A RID: 52634 RVA: 0x002BE2C4 File Offset: 0x002BC4C4
		private static FormulaExpression TranslateStartsWithDigit(StartsWithDigit startsWithDigit)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(startsWithDigit.columnName.Value);
			return PythonExpressionHelper.And(PythonExpressionHelper.And(PythonExpressionHelper.IsInstanceStr(formulaExpression), PythonExpressionHelper.GreaterThan(PythonExpressionHelper.Len(formulaExpression), 0.0)), PythonExpressionHelper.IsNumeric(PythonExpressionHelper.Index<string>(formulaExpression, 0)));
		}

		// Token: 0x0600CD9B RID: 52635 RVA: 0x002BE318 File Offset: 0x002BC518
		private FormulaExpression TranslateStringEquals(StringEquals stringEquals)
		{
			FormulaExpression formulaExpression = PythonExpressionHelper.Variable(stringEquals.columnName.Value);
			FormulaExpression formulaExpression2 = this.Translate(stringEquals.equalsText.Node, default(CancellationToken));
			return PythonExpressionHelper.Equal(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CD9C RID: 52636 RVA: 0x002BE360 File Offset: 0x002BC560
		private FormulaExpression TranslateAdd(Add add)
		{
			FormulaExpression formulaExpression = this.Translate(add.arithmeticLeft.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(add.addRight.Node, default(CancellationToken));
			return PythonExpressionHelper.Plus(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CD9D RID: 52637 RVA: 0x002BE3B0 File Offset: 0x002BC5B0
		private FormulaExpression TranslateAverage(Average average)
		{
			IEnumerable<FormulaExpression> enumerable = this.TranslateFromNumbers(average.fromNumbers);
			if (enumerable != null)
			{
				return PythonExpressionHelper.Avg(enumerable);
			}
			return null;
		}

		// Token: 0x0600CD9E RID: 52638 RVA: 0x002BE3D8 File Offset: 0x002BC5D8
		private FormulaExpression TranslateDivide(Divide divide)
		{
			FormulaExpression formulaExpression = this.Translate(divide.arithmeticLeft.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(divide.divideRight.Node, default(CancellationToken));
			return PythonExpressionHelper.Divide(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CD9F RID: 52639 RVA: 0x002BE428 File Offset: 0x002BC628
		private FormulaExpression TranslateMultiply(Multiply multiply)
		{
			FormulaExpression formulaExpression = this.Translate(multiply.arithmeticLeft.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(multiply.multiplyRight.Node, default(CancellationToken));
			return PythonExpressionHelper.Multiply(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CDA0 RID: 52640 RVA: 0x002BE478 File Offset: 0x002BC678
		private FormulaExpression TranslateProduct(Product product)
		{
			IEnumerable<FormulaExpression> enumerable = this.TranslateFromNumbers(product.fromNumbers);
			if (enumerable == null)
			{
				return null;
			}
			this._imports.Add("math", new string[] { "prod" });
			return PythonExpressionHelper.Prod(enumerable);
		}

		// Token: 0x0600CDA1 RID: 52641 RVA: 0x002BE4BC File Offset: 0x002BC6BC
		private FormulaExpression TranslateSubtract(Subtract subtract)
		{
			FormulaExpression formulaExpression = this.Translate(subtract.arithmeticLeft.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(subtract.subtractRight.Node, default(CancellationToken));
			return PythonExpressionHelper.Minus(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CDA2 RID: 52642 RVA: 0x002BE50C File Offset: 0x002BC70C
		private FormulaExpression TranslateSum(Sum sum)
		{
			IEnumerable<FormulaExpression> enumerable = this.TranslateFromNumbers(sum.fromNumbers);
			if (enumerable != null)
			{
				return PythonExpressionHelper.Sum(enumerable);
			}
			return null;
		}

		// Token: 0x0600CDA3 RID: 52643 RVA: 0x002BE534 File Offset: 0x002BC734
		private FormulaExpression TranslateConversionRule(ProgramNode node)
		{
			return this.Translate(node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CDA4 RID: 52644 RVA: 0x002BE558 File Offset: 0x002BC758
		private FormulaExpression TranslateLiteral(LiteralNode node)
		{
			if (node.Value is DateTime)
			{
				this._imports.Add("datetime", new string[] { "datetime" });
			}
			if (node.Value is decimal)
			{
				this._imports.Add("decimal", new string[] { "Decimal" });
			}
			object value = node.Value;
			string text = value as string;
			FormulaExpression formulaExpression;
			if (text == null)
			{
				if (value is int)
				{
					int num = (int)value;
					formulaExpression = PythonExpressionHelper.NumberLiteral(num);
				}
				else if (value is double)
				{
					double num2 = (double)value;
					formulaExpression = PythonExpressionHelper.NumberLiteral(num2);
				}
				else if (value is decimal)
				{
					decimal num3 = (decimal)value;
					formulaExpression = PythonExpressionHelper.NumberLiteral(num3);
				}
				else if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					formulaExpression = PythonExpressionHelper.DateTimeLiteral(dateTime);
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
							formulaExpression = PythonExpressionHelper.RegexLiteral(regex.ToString());
						}
					}
					else
					{
						formulaExpression = PythonExpressionHelper.RegexLiteral(matchDescriptor.Regex.ToString());
					}
				}
			}
			else
			{
				formulaExpression = PythonExpressionHelper.StringLiteral(text);
			}
			return formulaExpression;
		}

		// Token: 0x0600CDA5 RID: 52645 RVA: 0x002BE694 File Offset: 0x002BC894
		private FormulaExpression DateQuarter(FormulaExpression subject, bool inline = false)
		{
			FormulaExpression formulaExpression = new PythonDateQuarter(subject);
			if (!inline)
			{
				return this.AddVariable<int>("quarter", formulaExpression);
			}
			return formulaExpression;
		}

		// Token: 0x0600CDA6 RID: 52646 RVA: 0x002BE6BC File Offset: 0x002BC8BC
		private FormulaExpression DateQuarterEnd(FormulaExpression subject, FormulaExpression quarter = null, FormulaExpression quarterStart = null, bool inline = false)
		{
			this._imports.Add("dateutil.relativedelta", new string[] { "relativedelta" });
			if (quarter == null)
			{
				quarter = this.DateQuarter(subject, inline);
			}
			if (quarterStart == null)
			{
				quarterStart = this.DateQuarterStart(subject, quarter, inline);
			}
			FormulaExpression formulaExpression = quarterStart;
			double? num = new double?((double)3);
			FormulaExpression formulaExpression2 = PythonExpressionHelper.Plus(formulaExpression, PythonExpressionHelper.RelativeDelta(null, num, null, null, null, null, null));
			if (!inline)
			{
				return this.AddVariable<DateTime>("quarter_end", formulaExpression2);
			}
			return formulaExpression2;
		}

		// Token: 0x0600CDA7 RID: 52647 RVA: 0x002BE764 File Offset: 0x002BC964
		private FormulaExpression DateQuarterStart(FormulaExpression subject, FormulaExpression quarter = null, bool inline = false)
		{
			this._imports.Add("datetime", new string[] { "datetime" });
			if (quarter == null)
			{
				quarter = this.DateQuarter(subject, inline);
			}
			FormulaExpression formulaExpression = PythonExpressionHelper.Func<DateTime>("datetime", new FormulaExpression[]
			{
				PythonExpressionHelper.Year(subject),
				PythonExpressionHelper.Minus(PythonExpressionHelper.Multiply(3, quarter), 2),
				PythonExpressionHelper.NumberLiteral(1)
			});
			if (!inline)
			{
				return this.AddVariable<DateTime>("quarter_start", formulaExpression);
			}
			return formulaExpression;
		}

		// Token: 0x0600CDA8 RID: 52648 RVA: 0x002BE7E0 File Offset: 0x002BC9E0
		private FormulaExpression DateWeekCount(FormulaExpression subject, FormulaExpression periodStart, bool inline = false)
		{
			FormulaExpression formulaExpression = this.AddVariable<int>("days", PythonExpressionHelper.Dot<int>(PythonExpressionHelper.Minus(subject, periodStart), "days"));
			FormulaExpression formulaExpression2 = this.AddVariable<int>("start_weekday", PythonExpressionHelper.WeekDay(periodStart));
			FormulaExpression formulaExpression3 = PythonExpressionHelper.Int(PythonExpressionHelper.Ceil(PythonExpressionHelper.Divide(PythonExpressionHelper.Plus(formulaExpression, formulaExpression2), 7.0)));
			if (!inline)
			{
				return this.AddVariable<int>("week_count", formulaExpression3);
			}
			return formulaExpression3;
		}

		// Token: 0x0600CDA9 RID: 52649 RVA: 0x002BE84C File Offset: 0x002BCA4C
		private FormulaExpression AddDateFormatVariable(string format)
		{
			this._imports.Add("os");
			string text = "date_format{0}";
			int dateFormatVarCount = this._dateFormatVarCount;
			this._dateFormatVarCount = dateFormatVarCount + 1;
			return this.AddVariable<double>(string.Format(text, dateFormatVarCount), PythonExpressionHelper.Raw(string.Concat(new string[]
			{
				"'",
				format.Replace("%#", "%-"),
				"' if os.name != 'nt' else '",
				format,
				"'"
			})));
		}

		// Token: 0x0600CDAA RID: 52650 RVA: 0x002BE8CE File Offset: 0x002BCACE
		private FormulaExpression AddVariable<T>(string name, FormulaExpression value)
		{
			return this.AddVariable(PythonExpressionHelper.Variable<T>(name), value);
		}

		// Token: 0x0600CDAB RID: 52651 RVA: 0x002BE8DD File Offset: 0x002BCADD
		private FormulaExpression AddVariable(FormulaExpression variable, FormulaExpression value)
		{
			this._statements.Add(PythonExpressionHelper.Assign(variable, value));
			return variable;
		}

		// Token: 0x0600CDAC RID: 52652 RVA: 0x002BE8F4 File Offset: 0x002BCAF4
		internal static string ResolveIdentifierName(string identifierName, string defaultIdentifierName = null)
		{
			if (PythonProgramTranslator._keywords.Contains(identifierName))
			{
				return defaultIdentifierName;
			}
			if (PythonProgramTranslator.ValidIdentifier.IsMatch(identifierName))
			{
				return identifierName;
			}
			if (!PythonProgramTranslator._validIdentifierStartChar.IsMatch(identifierName[0].ToString()))
			{
				identifierName = "x" + identifierName;
				if (PythonProgramTranslator.ValidIdentifier.IsMatch(identifierName))
				{
					return identifierName;
				}
			}
			identifierName = PythonProgramTranslator._invalidIdentifierChars.Replace(identifierName, "_");
			if (!PythonProgramTranslator.ValidIdentifier.IsMatch(identifierName))
			{
				return defaultIdentifierName;
			}
			return identifierName;
		}

		// Token: 0x0600CDAD RID: 52653 RVA: 0x002BE978 File Offset: 0x002BCB78
		internal static PythonDefinition ResolveParameterNames(PythonDefinition definition)
		{
			return FormulaSubstitutionVisitor.Substitute(definition, definition.Parameters.Where((PythonVariable parameter) => !PythonProgramTranslator.ValidIdentifier.IsMatch(parameter.Name) || PythonProgramTranslator._keywords.Contains(parameter.Name)).Select((PythonVariable oldParameter, int i) => new KeyValuePair<FormulaExpression, FormulaExpression>(oldParameter, PythonExpressionHelper.Variable(PythonProgramTranslator.ResolveIdentifierName(oldParameter.Name, string.Format("input{0}", i))))).ToDictionary((KeyValuePair<FormulaExpression, FormulaExpression> kvp) => kvp.Key, (KeyValuePair<FormulaExpression, FormulaExpression> kvp) => kvp.Value)) as PythonDefinition;
		}

		// Token: 0x0600CDAE RID: 52654 RVA: 0x002BEA24 File Offset: 0x002BCC24
		internal static PythonDefinition ResolveReturn(PythonDefinition definition)
		{
			PythonBlock pythonBlock = definition.Body.Transform<PythonBlock>(delegate(FormulaExpression node)
			{
				PythonBlock pythonBlock2 = node as PythonBlock;
				if (pythonBlock2 == null)
				{
					return node;
				}
				IReadOnlyList<FormulaExpression> statements = pythonBlock2.Statements;
				List<FormulaExpression> list = ((statements != null) ? statements.ToList<FormulaExpression>() : null);
				FormulaExpression formulaExpression = ((list != null) ? list.LastOrDefault<FormulaExpression>() : null);
				bool flag = formulaExpression == null;
				if (!flag)
				{
					bool flag2 = formulaExpression is PythonReturn || formulaExpression is PythonIf;
					flag = flag2;
				}
				if (flag)
				{
					return node;
				}
				PythonAssign pythonAssign = formulaExpression as PythonAssign;
				if (pythonAssign != null)
				{
					formulaExpression = pythonAssign.Right;
				}
				list.RemoveAt(list.Count - 1);
				list.Add(PythonExpressionHelper.Return(formulaExpression));
				return PythonExpressionHelper.Block(list);
			});
			if (!(pythonBlock == null))
			{
				return definition.With(null, null, pythonBlock, null);
			}
			return definition;
		}

		// Token: 0x0600CDAF RID: 52655 RVA: 0x002BEA74 File Offset: 0x002BCC74
		private string ResolveComment()
		{
			string text;
			try
			{
				List<ColumnDetail> columns = (from c in base.Examples.InputColumnDetails(null)
					orderby c.Name
					select c).ToList<ColumnDetail>();
				TextTableBuilder textTableBuilder = TextTableBuilder.Create(TextTableBorder.None, null, null).AddIdentityColumn("", new int?(0), new int?(0)).AddStaticColumn(":", false, true, false);
				foreach (ColumnDetail columnDetail in columns)
				{
					TextTableBuilder textTableBuilder2 = textTableBuilder;
					string name = columnDetail.Name;
					int num = 0;
					bool allNumber = columnDetail.AllNumber;
					textTableBuilder2.AddColumn(name, num, new int?(60), allNumber, null, null, null, null);
				}
				textTableBuilder.AddStaticColumn("=>", false, true, false).AddColumn(this._options.LocalizedStrings.Output, 0, new int?(60), false, null, null, null, null).AddHeadingRow()
					.AddDataRows(base.Examples.Select((Example example) => (from <>h__TransparentIdentifier0 in columns.Select(delegate(ColumnDetail column)
						{
							object obj;
							return new
							{
								column = column,
								value = (example.Input.TryGetValue(column.Name, out obj) ? obj : null)
							};
						})
						select <>h__TransparentIdentifier0.value.ToPythonPseudoLiteral()).AppendItem(example.Output.ToPythonPseudoLiteral()).ToList<string>()).ToList<List<string>>(), null);
				text = this._options.LocalizedStrings.TransformationCodeCommentPrefix + ":" + Environment.NewLine + textTableBuilder.Render().TrimEnd(Array.Empty<char>());
			}
			catch (Exception ex)
			{
				ILogger logger = base.Logger;
				if (logger != null)
				{
					logger.TrackException(ex);
				}
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600CDB0 RID: 52656 RVA: 0x002BEC5C File Offset: 0x002BCE5C
		private static string ResolveDateTimeFormat(string mask)
		{
			return mask.Replace("'", "").Replace("MMMM", "%B").Replace("MMM", "%b")
				.Replace("MM", "%m")
				.Replace("%M", "%#m")
				.Replace("M", "%#m")
				.Replace("dddd", "%A")
				.Replace("ddd", "%a")
				.Replace("dd", "$1")
				.Replace("%d", "$2")
				.Replace("d", "$2")
				.Replace("yyyy", "%Y")
				.Replace("yy", "%y")
				.Replace("HH", "$3")
				.Replace("H", "%H")
				.Replace("hh", "%I")
				.Replace("h", "%#I")
				.Replace("mm", "%M")
				.Replace("h", "%#I")
				.Replace("ss", "%S")
				.Replace("tt", "%p")
				.Replace("$1", "%d")
				.Replace("$2", "%#d")
				.Replace("$3", "%H");
		}

		// Token: 0x0600CDB1 RID: 52657 RVA: 0x002BEDD2 File Offset: 0x002BCFD2
		private FormulaExpression ResolveType<TDesired>(FormulaExpression expression)
		{
			return this.ResolveType(expression, typeof(TDesired));
		}

		// Token: 0x0600CDB2 RID: 52658 RVA: 0x002BEDE8 File Offset: 0x002BCFE8
		private FormulaExpression ResolveType(FormulaExpression expression, Type desiredType)
		{
			IFormulaTyped formulaTyped = expression as IFormulaTyped;
			if (formulaTyped != null)
			{
				return this.ResolveType(expression, desiredType, formulaTyped.Type);
			}
			return expression;
		}

		// Token: 0x0600CDB3 RID: 52659 RVA: 0x002BEE10 File Offset: 0x002BD010
		private FormulaExpression ResolveType(FormulaExpression expression, Type desiredType, Type existingType)
		{
			if (existingType != typeof(decimal) && desiredType == typeof(decimal))
			{
				this._imports.Add("decimal", new string[] { "Decimal" });
				return PythonExpressionHelper.Decimal(expression, false);
			}
			if (existingType != typeof(double) && desiredType == typeof(double))
			{
				return PythonExpressionHelper.Float(expression);
			}
			if (existingType != typeof(float) && desiredType == typeof(float))
			{
				return PythonExpressionHelper.Float(expression);
			}
			if (!(existingType != typeof(int)) || !(desiredType == typeof(int)))
			{
				return expression;
			}
			return PythonExpressionHelper.Int(expression);
		}

		// Token: 0x0600CDB6 RID: 52662 RVA: 0x002BF0A4 File Offset: 0x002BD2A4
		[CompilerGenerated]
		private FormulaExpression <TranslateConcat>g__Coerce|19_0(FormulaExpression exp)
		{
			FormulaVariable formulaVariable = exp as FormulaVariable;
			if (formulaVariable != null)
			{
				IFormulaTyped formulaTyped = formulaVariable as IFormulaTyped;
				if (formulaTyped != null && !(formulaTyped.Type != typeof(string)))
				{
					ColumnDetail columnDetail = base.ResolveInputColumnDetail(formulaVariable.Name);
					if (columnDetail == null || !columnDetail.AllString || !(columnDetail.HasNulls ?? false))
					{
						return formulaVariable;
					}
					return PythonExpressionHelper.Or(formulaVariable, PythonExpressionHelper.StringLiteral(string.Empty));
				}
			}
			return exp;
		}

		// Token: 0x0600CDB7 RID: 52663 RVA: 0x002BF124 File Offset: 0x002BD324
		[CompilerGenerated]
		private FormulaExpression <TranslateRoundNumber>g__AddRound|43_0(string roundMode, ref PythonProgramTranslator.<>c__DisplayClass43_0 A_2)
		{
			this._imports.Add("decimal", new string[] { "Decimal", roundMode });
			FormulaExpression formulaExpression = this.AddVariable<decimal>("delta", PythonExpressionHelper.Decimal(PythonExpressionHelper.NumberLiteral(A_2.roundNumber.numberRoundDesc.Value.Delta), false));
			return PythonExpressionHelper.Multiply(PythonExpressionHelper.DotFunc<decimal>(PythonExpressionHelper.Divide(A_2.source, formulaExpression), "quantize", new FormulaExpression[]
			{
				PythonExpressionHelper.NumberLiteral(0),
				PythonExpressionHelper.Variable(roundMode)
			}), formulaExpression);
		}

		// Token: 0x04005049 RID: 20553
		private static readonly Regex _invalidIdentifierChars = "[^\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}_\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}]+".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x0400504A RID: 20554
		private static readonly Regex _validContinueChars = "^[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}_\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}]+$".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x0400504B RID: 20555
		private static readonly string[] _keywords = new string[]
		{
			"False", "None", "True", "__peg_parser__", "and", "as", "assert", "async", "await", "break",
			"class", "continue", "def", "del", "elif", "else", "except", "finally", "for", "from",
			"global", "if", "import", "in", "is", "lambda", "nonlocal", "not", "or", "pass",
			"raise", "return", "try", "while", "with", "yield"
		};

		// Token: 0x0400504C RID: 20556
		internal static readonly Regex ValidIdentifier = "^[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}_][\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}_\\p{Mn}\\p{Mc}\\p{Nd}\\p{Pc}]*$".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x0400504D RID: 20557
		private static readonly Regex _validIdentifierStartChar = "[\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lm}\\p{Lo}\\p{Nl}]".ToRegex(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

		// Token: 0x0400504E RID: 20558
		private bool _cancelled;

		// Token: 0x0400504F RID: 20559
		private FormulaExpression _currentInputExpression;

		// Token: 0x04005050 RID: 20560
		private int _dateFormatVarCount = 1;

		// Token: 0x04005051 RID: 20561
		private bool _forceDecimalNumber;

		// Token: 0x04005052 RID: 20562
		private readonly ImportBlockBuilder _imports = new ImportBlockBuilder();

		// Token: 0x04005053 RID: 20563
		private readonly IPythonTranslationOptions _options;

		// Token: 0x04005054 RID: 20564
		private readonly string _regexLibraryName;

		// Token: 0x04005055 RID: 20565
		private List<FormulaExpression> _statements = new List<FormulaExpression>();

		// Token: 0x04005056 RID: 20566
		private int _varCount = 1;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
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

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x0200182D RID: 6189
	internal class SqlProgramTranslator : ProgramTranslatorBase
	{
		// Token: 0x0600CAEE RID: 51950 RVA: 0x002B50BB File Offset: 0x002B32BB
		private SqlProgramTranslator(Program program, ISqlTranslationOptions options, IEnumerable<Example> examples, IEnumerable<IRow> inputs, ILogger logger)
			: base(program, examples, inputs, logger)
		{
			this._options = options ?? new SqlTranslationConstraint();
		}

		// Token: 0x0600CAEF RID: 51951 RVA: 0x002B50D9 File Offset: 0x002B32D9
		public static FormulaExpression Translate(Program program, ISqlTranslationOptions options, IEnumerable<Example> examples, IEnumerable<IRow> inputs, ILogger logger = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return new SqlProgramTranslator(program, options, examples, inputs, logger).Translate(cancellationToken);
		}

		// Token: 0x0600CAF0 RID: 51952 RVA: 0x002B50ED File Offset: 0x002B32ED
		protected override FormulaExpression Translate(CancellationToken cancellationToken = default(CancellationToken))
		{
			return SqlExpressionOptimizer.Optimize(base.Translate(cancellationToken), this._options);
		}

		// Token: 0x0600CAF1 RID: 51953 RVA: 0x002B5104 File Offset: 0x002B3304
		protected override FormulaExpression Translate(ProgramNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			FormulaExpression formulaExpression;
			try
			{
				cancellationToken.ThrowIfCancellationRequested();
				Concat concat;
				LetX letX;
				SlicePrefix slicePrefix;
				Slice slice;
				SlicePrefixAbs slicePrefixAbs;
				SlicePrefix slicePrefix2;
				SliceSuffix sliceSuffix;
				Replace replace;
				Length length;
				Abs abs;
				Str str;
				Number number;
				Find find;
				FormatNumber formatNumber;
				ParseNumber parseNumber;
				FormatDateTime formatDateTime;
				ParseDateTime parseDateTime;
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
				else if (node.Is(out concat))
				{
					formulaExpression = this.TranslateConcat(concat);
				}
				else if (node.Is(out letX))
				{
					formulaExpression = this.TranslateLetX(letX);
				}
				else if (node.Is(out slicePrefix))
				{
					formulaExpression = this.TranslateSlicePrefix(slicePrefix);
				}
				else if (node.Is(out slice))
				{
					formulaExpression = this.TranslateSlice(slice);
				}
				else if (node.Is(out slicePrefixAbs))
				{
					formulaExpression = this.TranslateSlicePrefixAbs(slicePrefixAbs);
				}
				else if (node.Is(out slicePrefix2))
				{
					formulaExpression = this.TranslateSlicePrefix(slicePrefix2);
				}
				else if (node.Is(out sliceSuffix))
				{
					formulaExpression = this.TranslateSliceSuffix(sliceSuffix);
				}
				else if (node.Is(out replace))
				{
					formulaExpression = this.TranslateReplace(replace);
				}
				else if (node.Is(out length))
				{
					formulaExpression = this.TranslateLength(length);
				}
				else if (node.Is(out abs))
				{
					formulaExpression = SqlProgramTranslator.TranslateAbs(abs);
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
					formulaExpression = SqlProgramTranslator.TranslateArithmeticRightNumber(node);
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
					formulaExpression = SqlProgramTranslator.TranslateFromStr(fromStr);
				}
				else if (node.Is(out fromDateTime))
				{
					formulaExpression = SqlProgramTranslator.TranslateFromDateTime(fromDateTime);
				}
				else if (node.Is(out fromNumber))
				{
					formulaExpression = SqlProgramTranslator.TranslateFromNumber(fromNumber);
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
				else
				{
					if (!(node is VariableNode))
					{
						if (node != null)
						{
							if (node.GrammarRule is ConversionRule)
							{
								formulaExpression = this.TranslateConversionRule(node);
								goto IL_03E2;
							}
							LiteralNode literalNode = node as LiteralNode;
							if (literalNode != null)
							{
								formulaExpression = SqlProgramTranslator.TranslateLiteral(literalNode);
								goto IL_03E2;
							}
						}
						throw new FormulaTranslationNotFoundException(string.Format("Invalid Rule: {0}", node.GrammarRule));
					}
					formulaExpression = this.TranslateVariableNode();
				}
				IL_03E2:
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

		// Token: 0x0600CAF2 RID: 51954 RVA: 0x002B5568 File Offset: 0x002B3768
		private FormulaExpression TranslateAdd(Add add)
		{
			return SqlExpressionHelper.Plus(this.Translate(add.arithmeticLeft.Node, default(CancellationToken)), this.Translate(add.addRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600CAF3 RID: 51955 RVA: 0x002B55B6 File Offset: 0x002B37B6
		private FormulaExpression TranslateAverage(Average average)
		{
			return SqlExpressionHelper.Average(this.TranslateFromNumbers(average.fromNumbers));
		}

		// Token: 0x0600CAF4 RID: 51956 RVA: 0x002B55CC File Offset: 0x002B37CC
		private FormulaExpression TranslateDivide(Divide divide)
		{
			return SqlExpressionHelper.Divide(this.Translate(divide.arithmeticLeft.Node, default(CancellationToken)), this.Translate(divide.divideRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600CAF5 RID: 51957 RVA: 0x002B561C File Offset: 0x002B381C
		private IEnumerable<FormulaExpression> TranslateFromNumbers(fromNumbers fromNumbers)
		{
			LiteralNode literalNode = fromNumbers.Node.Children[1] as LiteralNode;
			string[] array = ((literalNode != null) ? literalNode.Value : null) as string[];
			if (array != null)
			{
				return array.Select(new Func<string, FormulaExpression>(this.ResolveInputVariable));
			}
			return null;
		}

		// Token: 0x0600CAF6 RID: 51958 RVA: 0x002B5668 File Offset: 0x002B3868
		private FormulaExpression TranslateMultiply(Multiply multiply)
		{
			return SqlExpressionHelper.Multiply(this.Translate(multiply.arithmeticLeft.Node, default(CancellationToken)), this.Translate(multiply.multiplyRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600CAF7 RID: 51959 RVA: 0x002B56B6 File Offset: 0x002B38B6
		private FormulaExpression TranslateProduct(Product product)
		{
			return SqlExpressionHelper.Product(this.TranslateFromNumbers(product.fromNumbers));
		}

		// Token: 0x0600CAF8 RID: 51960 RVA: 0x002B56CC File Offset: 0x002B38CC
		private FormulaExpression TranslateSubtract(Subtract subtract)
		{
			return SqlExpressionHelper.Minus(this.Translate(subtract.arithmeticLeft.Node, default(CancellationToken)), this.Translate(subtract.subtractRight.Node, default(CancellationToken)));
		}

		// Token: 0x0600CAF9 RID: 51961 RVA: 0x002B571A File Offset: 0x002B391A
		private FormulaExpression TranslateSum(Sum sum)
		{
			return SqlExpressionHelper.Sum(this.TranslateFromNumbers(sum.fromNumbers));
		}

		// Token: 0x0600CAFA RID: 51962 RVA: 0x002B5730 File Offset: 0x002B3930
		private static FormulaExpression TranslateFromDateTime(FromDateTime dateTime)
		{
			return SqlExpressionHelper.Variable(dateTime.columnName.Value, SqlExpressionHelper.DateTime2Type());
		}

		// Token: 0x0600CAFB RID: 51963 RVA: 0x002B5758 File Offset: 0x002B3958
		private static FormulaExpression TranslateFromNumber(FromNumber fromNumber)
		{
			return SqlExpressionHelper.Variable(fromNumber.columnName.Value, SqlExpressionHelper.FloatType());
		}

		// Token: 0x0600CAFC RID: 51964 RVA: 0x002B5780 File Offset: 0x002B3980
		private static FormulaExpression TranslateFromStr(FromStr input)
		{
			return SqlExpressionHelper.Variable(input.columnName.Value, SqlExpressionHelper.StringType());
		}

		// Token: 0x0600CAFD RID: 51965 RVA: 0x002B57A8 File Offset: 0x002B39A8
		private FormulaExpression TranslateToDateTime(ToDateTime subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CAFE RID: 51966 RVA: 0x002B57D4 File Offset: 0x002B39D4
		private FormulaExpression TranslateToDecimal(ToDecimal subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CAFF RID: 51967 RVA: 0x002B5800 File Offset: 0x002B3A00
		private FormulaExpression TranslateToDouble(ToDouble subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CB00 RID: 51968 RVA: 0x002B582C File Offset: 0x002B3A2C
		private FormulaExpression TranslateToInt(ToInt subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CB01 RID: 51969 RVA: 0x002B5858 File Offset: 0x002B3A58
		private FormulaExpression TranslateToStr(ToStr subject)
		{
			return this.Translate(subject.Node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CB02 RID: 51970 RVA: 0x002B5884 File Offset: 0x002B3A84
		private static FormulaExpression TranslateAbs(Abs abs)
		{
			return SqlExpressionHelper.NumberLiteral(abs.absPos.Value);
		}

		// Token: 0x0600CB03 RID: 51971 RVA: 0x002B58A8 File Offset: 0x002B3AA8
		private FormulaExpression TranslateConcat(Concat concat)
		{
			FormulaExpression formulaExpression = this.Translate(concat.Node.Children[0], default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(concat.Node.Children[1], default(CancellationToken));
			return SqlExpressionHelper.Concat(formulaExpression, formulaExpression2);
		}

		// Token: 0x0600CB04 RID: 51972 RVA: 0x002B58F8 File Offset: 0x002B3AF8
		private FormulaExpression TranslateConversionRule(ProgramNode node)
		{
			return this.Translate(node.Children[0], default(CancellationToken));
		}

		// Token: 0x0600CB05 RID: 51973 RVA: 0x002B591C File Offset: 0x002B3B1C
		private FormulaExpression TranslateFind(Find find)
		{
			FormulaExpression formulaExpression = this.Translate(find.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(find.findDelimiter.Node, default(CancellationToken));
			SqlStringLiteral sqlStringLiteral = formulaExpression2 as SqlStringLiteral;
			if (sqlStringLiteral != null)
			{
				int? num = SqlExpressionHelper.StringToCharCode(sqlStringLiteral.Value);
				if (num != null)
				{
					formulaExpression2 = SqlExpressionHelper.Char(SqlExpressionHelper.NumberLiteral(num.Value));
				}
			}
			int value = find.findInstance.Value;
			int value2 = find.findOffset.Value;
			if (value == 0)
			{
				return null;
			}
			FormulaExpression formulaExpression3 = SqlProgramTranslator.ResolveCharIndex(formulaExpression2, formulaExpression, value);
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
					formulaExpression4 = SqlExpressionHelper.Minus(formulaExpression3, SqlExpressionHelper.NumberLiteral(-value2));
				}
			}
			else
			{
				formulaExpression4 = SqlExpressionHelper.Plus(formulaExpression3, SqlExpressionHelper.NumberLiteral(value2));
			}
			return formulaExpression4;
		}

		// Token: 0x0600CB06 RID: 51974 RVA: 0x002B5A14 File Offset: 0x002B3C14
		private FormulaExpression TranslateFormatDateTime(FormatDateTime formatDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(formatDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = formatDateTime.dateTimeFormatDesc.Value;
			string mask = value.Mask;
			return SqlExpressionHelper.Format(formulaExpression, SqlExpressionHelper.StringLiteral(mask), SqlExpressionHelper.StringLiteral(value.Locale));
		}

		// Token: 0x0600CB07 RID: 51975 RVA: 0x002B5A6C File Offset: 0x002B3C6C
		private FormulaExpression TranslateFormatNumber(FormatNumber formatNumber)
		{
			FormulaExpression formulaExpression = this.Translate(formatNumber.Node.Children[0], default(CancellationToken));
			FormatNumberDescriptor value = formatNumber.numberFormatDesc.Value;
			return SqlExpressionHelper.Format(formulaExpression, SqlExpressionHelper.StringLiteral(value.ToFormatString()), SqlExpressionHelper.StringLiteral(value.Locale));
		}

		// Token: 0x0600CB08 RID: 51976 RVA: 0x002B5AC4 File Offset: 0x002B3CC4
		private FormulaExpression TranslateLength(Length length)
		{
			return SqlExpressionHelper.DataLength(this.Translate(length.fromStr.Node, default(CancellationToken)));
		}

		// Token: 0x0600CB09 RID: 51977 RVA: 0x002B5AF4 File Offset: 0x002B3CF4
		private FormulaExpression TranslateLetX(LetX letX)
		{
			this._currentInputExpression = this.Translate(letX.fromStrTrim.Node, default(CancellationToken));
			return this.Translate(letX.substring.Node, default(CancellationToken));
		}

		// Token: 0x0600CB0A RID: 51978 RVA: 0x002B5B44 File Offset: 0x002B3D44
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
					formulaExpression = SqlExpressionHelper.NumberLiteral(num);
				}
				else if (value is double)
				{
					double num2 = (double)value;
					formulaExpression = SqlExpressionHelper.NumberLiteral(num2);
				}
				else if (value is decimal)
				{
					decimal num3 = (decimal)value;
					formulaExpression = SqlExpressionHelper.NumberLiteral(num3);
				}
				else
				{
					formulaExpression = null;
				}
			}
			else
			{
				formulaExpression = SqlExpressionHelper.StringLiteral(text);
			}
			return formulaExpression;
		}

		// Token: 0x0600CB0B RID: 51979 RVA: 0x002B5BCC File Offset: 0x002B3DCC
		private FormulaExpression TranslateLowerCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return SqlExpressionHelper.Lower(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CB0C RID: 51980 RVA: 0x002B5C04 File Offset: 0x002B3E04
		private static FormulaExpression TranslateArithmeticRightNumber(ProgramNode node)
		{
			decimal num;
			if (!node.IsArithmeticRightNumber(out num))
			{
				return null;
			}
			return SqlExpressionHelper.NumberLiteral(num);
		}

		// Token: 0x0600CB0D RID: 51981 RVA: 0x002B5C24 File Offset: 0x002B3E24
		private FormulaExpression TranslateNumber(Number number)
		{
			return this.Translate(number.constNum.Node, default(CancellationToken));
		}

		// Token: 0x0600CB0E RID: 51982 RVA: 0x002B5C50 File Offset: 0x002B3E50
		private FormulaExpression TranslateParseDateTime(ParseDateTime parseDateTime)
		{
			FormulaExpression formulaExpression = this.Translate(parseDateTime.Node.Children[0], default(CancellationToken));
			DateTimeDescriptor value = parseDateTime.dateTimeParseDesc.Value;
			if (!(value == null))
			{
				return SqlExpressionHelper.TryParse(formulaExpression, SqlExpressionHelper.DateTime2Type(), SqlExpressionHelper.StringLiteral(value.Locale));
			}
			return null;
		}

		// Token: 0x0600CB0F RID: 51983 RVA: 0x002B5CAC File Offset: 0x002B3EAC
		private FormulaExpression TranslateParseNumber(ParseNumber parseNumber)
		{
			FormulaExpression formulaExpression = this.Translate(parseNumber.Node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return SqlExpressionHelper.TryParse(formulaExpression, SqlExpressionHelper.MoneyType(), SqlExpressionHelper.StringLiteral(parseNumber.locale.Value));
			}
			return null;
		}

		// Token: 0x0600CB10 RID: 51984 RVA: 0x002B5D04 File Offset: 0x002B3F04
		private FormulaExpression TranslateReplace(Replace replace)
		{
			return SqlExpressionHelper.Replace(this.Translate(replace.fromStr.Node, default(CancellationToken)), this.Translate(replace.replaceFindText.Node, default(CancellationToken)), this.Translate(replace.replaceText.Node, default(CancellationToken)));
		}

		// Token: 0x0600CB11 RID: 51985 RVA: 0x002B5D70 File Offset: 0x002B3F70
		private FormulaExpression TranslateSlice(Slice slice)
		{
			FormulaExpression formulaExpression = this.Translate(slice.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slice.pos1.Node, default(CancellationToken));
			FormulaExpression formulaExpression3 = this.Translate(slice.pos2.Node, default(CancellationToken));
			FormulaNumberLiteral formulaNumberLiteral = formulaExpression2 as FormulaNumberLiteral;
			int? num = ((formulaNumberLiteral != null) ? new int?((int)formulaNumberLiteral.Value) : null);
			FormulaNumberLiteral formulaNumberLiteral2 = formulaExpression3 as FormulaNumberLiteral;
			int? num2 = ((formulaNumberLiteral2 != null) ? new int?((int)formulaNumberLiteral2.Value) : null);
			int? num3 = num;
			int num4 = 0;
			if ((num3.GetValueOrDefault() < num4) & (num3 != null))
			{
				formulaExpression2 = SqlExpressionHelper.Minus(SqlExpressionHelper.DataLength(formulaExpression), SqlExpressionHelper.NumberLiteral(-num.Value - 1));
			}
			num3 = num2;
			num4 = 0;
			if ((num3.GetValueOrDefault() < num4) & (num3 != null))
			{
				formulaExpression3 = SqlExpressionHelper.Minus(SqlExpressionHelper.DataLength(formulaExpression), SqlExpressionHelper.NumberLiteral(-num2.Value - 1));
			}
			if (num != null)
			{
				int valueOrDefault = num.GetValueOrDefault();
				if (num2 != null)
				{
					int valueOrDefault2 = num2.GetValueOrDefault();
					num3 = num;
					FormulaExpression formulaExpression4;
					if (num3 != null)
					{
						num4 = num3.GetValueOrDefault();
						if (num4 <= 0)
						{
							if (num4 < 0)
							{
								if (valueOrDefault2 > 0)
								{
									formulaExpression4 = SqlExpressionHelper.Minus(SqlExpressionHelper.NumberLiteral(valueOrDefault2), SqlExpressionHelper.Plus(SqlExpressionHelper.NumberLiteral(valueOrDefault), SqlExpressionHelper.DataLength(formulaExpression)));
									goto IL_01E5;
								}
								if (valueOrDefault2 < 0)
								{
									formulaExpression4 = SqlExpressionHelper.NumberLiteral(Math.Abs(valueOrDefault - valueOrDefault2));
									goto IL_01E5;
								}
							}
						}
						else
						{
							if (valueOrDefault2 > 0)
							{
								formulaExpression4 = SqlExpressionHelper.NumberLiteral(valueOrDefault2 - valueOrDefault);
								goto IL_01E5;
							}
							if (valueOrDefault2 < 0)
							{
								formulaExpression4 = SqlExpressionHelper.Minus(SqlExpressionHelper.DataLength(formulaExpression), SqlExpressionHelper.NumberLiteral(Math.Abs(valueOrDefault2 + 1) + valueOrDefault));
								goto IL_01E5;
							}
						}
					}
					formulaExpression4 = null;
					IL_01E5:
					FormulaExpression formulaExpression5 = formulaExpression4;
					if (!(formulaExpression5 == null))
					{
						return SqlExpressionHelper.Substring(formulaExpression, formulaExpression2, formulaExpression5);
					}
					return null;
				}
			}
			return SqlExpressionHelper.Substring(SqlExpressionHelper.Left(formulaExpression, SqlExpressionHelper.Minus1(formulaExpression3)), formulaExpression2, SqlExpressionHelper.DataLength(formulaExpression));
		}

		// Token: 0x0600CB12 RID: 51986 RVA: 0x002B5F94 File Offset: 0x002B4194
		private FormulaExpression TranslateSlicePrefix(SlicePrefix slicePrefix)
		{
			FormulaExpression formulaExpression = this.Translate(slicePrefix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(slicePrefix.pos.Node, default(CancellationToken));
			SqlNumberLiteral sqlNumberLiteral = formulaExpression2 as SqlNumberLiteral;
			if (sqlNumberLiteral != null && sqlNumberLiteral.Value < 0.0)
			{
				formulaExpression2 = SqlExpressionHelper.Minus(SqlExpressionHelper.DataLength(formulaExpression), SqlExpressionHelper.NumberLiteral(-sqlNumberLiteral.Value - 1.0));
			}
			return SqlExpressionHelper.Left(formulaExpression, SqlExpressionHelper.Minus1(formulaExpression2));
		}

		// Token: 0x0600CB13 RID: 51987 RVA: 0x002B602C File Offset: 0x002B422C
		private FormulaExpression TranslateSlicePrefixAbs(SlicePrefixAbs slicePrefixAbs)
		{
			return SqlExpressionHelper.Left(this.Translate(slicePrefixAbs.x.Node, default(CancellationToken)), SqlExpressionHelper.NumberLiteral(slicePrefixAbs.slicePrefixAbsPos.Value - 1));
		}

		// Token: 0x0600CB14 RID: 51988 RVA: 0x002B6074 File Offset: 0x002B4274
		private FormulaExpression TranslateSliceSuffix(SliceSuffix sliceSuffix)
		{
			FormulaExpression formulaExpression = this.Translate(sliceSuffix.x.Node, default(CancellationToken));
			FormulaExpression formulaExpression2 = this.Translate(sliceSuffix.pos.Node, default(CancellationToken));
			SqlNumberLiteral sqlNumberLiteral = formulaExpression2 as SqlNumberLiteral;
			if (sqlNumberLiteral != null && sqlNumberLiteral.Value < 0.0)
			{
				formulaExpression2 = SqlExpressionHelper.Minus(SqlExpressionHelper.DataLength(formulaExpression), SqlExpressionHelper.NumberLiteral(-sqlNumberLiteral.Value - 1.0));
			}
			return SqlExpressionHelper.Right(formulaExpression, SqlExpressionHelper.Minus(SqlExpressionHelper.DataLength(formulaExpression), SqlExpressionHelper.Minus1(formulaExpression2)));
		}

		// Token: 0x0600CB15 RID: 51989 RVA: 0x002B6118 File Offset: 0x002B4318
		private FormulaExpression TranslateStr(Str str)
		{
			return this.Translate(str.constStr.Node, default(CancellationToken));
		}

		// Token: 0x0600CB16 RID: 51990 RVA: 0x002B6144 File Offset: 0x002B4344
		private FormulaExpression TranslateTrim(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return SqlExpressionHelper.Trim(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CB17 RID: 51991 RVA: 0x002B617C File Offset: 0x002B437C
		private FormulaExpression TranslateUpperCase(ProgramNode node)
		{
			FormulaExpression formulaExpression = this.Translate(node.Children[0], default(CancellationToken));
			if (!(formulaExpression == null))
			{
				return SqlExpressionHelper.Upper(formulaExpression);
			}
			return null;
		}

		// Token: 0x0600CB18 RID: 51992 RVA: 0x002B61B2 File Offset: 0x002B43B2
		private FormulaExpression TranslateVariableNode()
		{
			return this._currentInputExpression;
		}

		// Token: 0x0600CB19 RID: 51993 RVA: 0x002B61BC File Offset: 0x002B43BC
		private static FormulaExpression ResolveCharIndex(FormulaExpression delimiter, FormulaExpression subject, int instance)
		{
			if (instance == 0)
			{
				return null;
			}
			if (instance == 1)
			{
				return SqlExpressionHelper.CharIndex(delimiter, subject);
			}
			if (instance < 0)
			{
				FormulaExpression formulaExpression = SqlProgramTranslator.ResolveCharIndex(delimiter, SqlExpressionHelper.Reverse(subject), Math.Abs(instance));
				if (!(formulaExpression == null))
				{
					return SqlExpressionHelper.Minus(SqlExpressionHelper.DataLength(subject), SqlExpressionHelper.Minus1(formulaExpression));
				}
				return null;
			}
			else
			{
				FormulaExpression formulaExpression = SqlProgramTranslator.ResolveCharIndex(delimiter, subject, instance - 1);
				if (!(formulaExpression == null))
				{
					return SqlExpressionHelper.CharIndex(delimiter, subject, SqlExpressionHelper.Plus1(formulaExpression));
				}
				return null;
			}
		}

		// Token: 0x0600CB1A RID: 51994 RVA: 0x002B6234 File Offset: 0x002B4434
		private FormulaExpression ResolveInputVariable(string columnName)
		{
			object obj;
			if (!base.Examples.First<Example>().Input.TryGetValue(columnName, out obj))
			{
				return null;
			}
			FormulaExpression formulaExpression;
			if (!(obj is string))
			{
				if (!(obj is int))
				{
					if (!(obj is double))
					{
						if (!(obj is decimal))
						{
							if (!(obj is DateTime))
							{
								formulaExpression = null;
							}
							else
							{
								formulaExpression = SqlExpressionHelper.Variable(columnName, new SqlStringType());
							}
						}
						else
						{
							formulaExpression = SqlExpressionHelper.Variable(columnName, new SqlFloatType());
						}
					}
					else
					{
						formulaExpression = SqlExpressionHelper.Variable(columnName, new SqlFloatType());
					}
				}
				else
				{
					formulaExpression = SqlExpressionHelper.Variable(columnName, new SqlIntType());
				}
			}
			else
			{
				formulaExpression = SqlExpressionHelper.Variable(columnName, new SqlStringType());
			}
			return formulaExpression;
		}

		// Token: 0x04004FA8 RID: 20392
		private bool _cancelled;

		// Token: 0x04004FA9 RID: 20393
		private FormulaExpression _currentInputExpression;

		// Token: 0x04004FAA RID: 20394
		private readonly ISqlTranslationOptions _options;
	}
}

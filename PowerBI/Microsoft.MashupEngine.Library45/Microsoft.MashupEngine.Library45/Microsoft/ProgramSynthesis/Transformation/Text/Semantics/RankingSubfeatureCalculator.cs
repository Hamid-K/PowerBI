using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CA7 RID: 7335
	public class RankingSubfeatureCalculator
	{
		// Token: 0x0600F865 RID: 63589 RVA: 0x0034E5B8 File Offset: 0x0034C7B8
		public RankingSubfeatureCalculator(Grammar grammar, int? randomSeed = null)
		{
			this._randomSeed = randomSeed ?? 2122808967;
			this._grammar = grammar;
			this._build = GrammarBuilders.Instance(grammar);
			WholeColumnUsed wholeColumnUsed = new WholeColumnUsed(grammar);
			this._inputsUsed = new InputsUsed(grammar);
			this._wholeColumnsUsed = new WholeColumnsUsed(grammar, wholeColumnUsed, this._inputsUsed);
		}

		// Token: 0x0600F866 RID: 63590 RVA: 0x0034E630 File Offset: 0x0034C830
		public double Score_Transformation(LearningInfo learningInfo, ProgramNode st)
		{
			double num = 0.0;
			if (learningInfo != null)
			{
				IReadOnlyList<State> additionalInputs = learningInfo.FeatureCalculationContext.AdditionalInputs;
				int num2 = additionalInputs.RandomlySample(this._randomSeed, 50).Count((State v) => st.Invoke(v) == null);
				int num3 = Math.Min(additionalInputs.Count, 50);
				num = ((num3 > 0) ? ((double)num2 / (double)num3) : 0.0);
			}
			return num;
		}

		// Token: 0x0600F867 RID: 63591 RVA: 0x00012DE5 File Offset: 0x00010FE5
		public double WholeColumnScore(ProgramNode n)
		{
			return 1.0;
		}

		// Token: 0x0600F868 RID: 63592 RVA: 0x0034E6A8 File Offset: 0x0034C8A8
		public RankingSubfeatureCalculator.Score_ConcatFeatures Score_Concat(LearningInfo learningInfo, ProgramNode f, ProgramNode e)
		{
			IImmutableSet<string> featureValue = f.GetFeatureValue<IImmutableSet<string>>(this._inputsUsed, null);
			IImmutableSet<string> featureValue2 = e.GetFeatureValue<IImmutableSet<string>>(this._inputsUsed, null);
			IImmutableSet<string> featureValue3 = f.GetFeatureValue<IImmutableSet<string>>(this._wholeColumnsUsed, null);
			IImmutableSet<string> featureValue4 = e.GetFeatureValue<IImmutableSet<string>>(this._wholeColumnsUsed, null);
			int num = 0;
			if (featureValue.Count > 0 && featureValue2.Count > 0)
			{
				num = featureValue.Except(featureValue2).Count;
			}
			int num2 = 0;
			if (featureValue3.Count > 0 && featureValue4.Count > 0)
			{
				num2 = featureValue.Intersect(featureValue2).Count;
			}
			bool flag = false;
			double num3 = 0.0;
			double num4 = 0.0;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = featureValue.Count == 0 && featureValue2.Count == 0;
			if (!flag4 && ((learningInfo != null) ? learningInfo.FeatureCalculationContext.AllInputs : null) != null && learningInfo.FeatureCalculationContext.AllInputs.Any<State>())
			{
				Optional<KeyValuePair<State, object>> optional = learningInfo.GetInputOutputPairs(InputKind.All).MaybeFirst((KeyValuePair<State, object> t) => t.Value != null);
				ValueSubstring valueSubstring = (ValueSubstring)optional.Select((KeyValuePair<State, object> info) => info.Value).OrElseDefault<object>();
				if (valueSubstring != null && RankingSubfeatureCalculator.TwoDigitsRegexp.IsMatch(valueSubstring.Value))
				{
					State key = optional.Value.Key;
					ValueSubstring valueSubstring2 = (ValueSubstring)f.Invoke(key);
					ValueSubstring valueSubstring3 = (ValueSubstring)e.Invoke(key);
					num3 = valueSubstring2.Length;
					num4 = valueSubstring3.Length;
					Optional<char> optional2 = valueSubstring2.MaybeLastChar();
					Func<char, bool> func;
					if ((func = RankingSubfeatureCalculator.<>O.<0>__IsDigit) == null)
					{
						func = (RankingSubfeatureCalculator.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
					}
					flag2 = optional2.Select(func).OrElseDefault<bool>();
					Optional<char> optional3 = valueSubstring3.MaybeFirstChar();
					Func<char, bool> func2;
					if ((func2 = RankingSubfeatureCalculator.<>O.<0>__IsDigit) == null)
					{
						func2 = (RankingSubfeatureCalculator.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
					}
					flag3 = optional3.Select(func2).OrElseDefault<bool>();
					flag = num3 > 0.0 && num4 > 0.0 && flag2 && flag3;
				}
			}
			bool flag5 = false;
			bool flag6 = false;
			if (flag4)
			{
				IEnumerable<string> commonDelimiters = RankingSubfeatureCalculator.CommonDelimiters;
				ValueSubstring valueSubstring4 = f.Invoke(null) as ValueSubstring;
				flag5 = commonDelimiters.Contains((valueSubstring4 != null) ? valueSubstring4.Value : null);
				IEnumerable<string> commonDelimiters2 = RankingSubfeatureCalculator.CommonDelimiters;
				ValueSubstring valueSubstring5 = e.Invoke(null) as ValueSubstring;
				flag6 = commonDelimiters2.Contains((valueSubstring5 != null) ? valueSubstring5.Value : null);
			}
			bool flag7 = flag4 && !flag6 && !flag5;
			return new RankingSubfeatureCalculator.Score_ConcatFeatures
			{
				NewInputsCount = (double)num,
				RepeatWholeColumnsCount = (double)num2,
				BothSidesConstant = (flag4 > false),
				ConcatNumbers = (flag > false),
				FValueLen = num3,
				EValueLen = num4,
				FValueLast = (flag2 > false),
				EValueFirst = (flag3 > false),
				ConcatNonCommonConstants = (flag7 > false),
				FContainsCommonDelimiters = (flag5 > false),
				EContainsCommonDelimiters = (flag6 > false),
				FInputsCount = (double)featureValue.Count,
				EInputsCount = (double)featureValue2.Count,
				FWholeColumnsCount = (double)featureValue3.Count,
				EWholeColumnsCount = (double)featureValue4.Count
			};
		}

		// Token: 0x0600F869 RID: 63593 RVA: 0x0034EA08 File Offset: 0x0034CC08
		public RankingSubfeatureCalculator.Score_ConstStrFeatures Score_ConstStr(LearningInfo learningInfo, LiteralNode s)
		{
			string value = this._build.Node.Cast.s(s).Value;
			int length = value.Length;
			bool flag = RankingSubfeatureCalculator.CommonDelimiters.Contains(value);
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			if (learningInfo != null)
			{
				IFeatureOptions options = learningInfo.Options;
				if (learningInfo.FeatureCalculationContext.MaterializeSpecInputs().Count == 1)
				{
					num += Math.Log(2.0);
				}
				ValueSubstring sr = ValueSubstring.Create(value, null, null, null, null);
				StringLearningCache cache = sr.Cache;
				cache.InitializeStaticTokens(null);
				IEnumerable<ValueSubstring> enumerable = (from mp in (from mp in cache.MatchedTokens.Where((Token regexp) => RankingSubfeatureCalculator.AlphanumericTokens.Contains(regexp.Name)).SelectMany(delegate(Token regexp)
						{
							CachedList cachedList;
							cache.TryGetMatchPositionsFor(regexp, out cachedList);
							return cachedList;
						})
						where mp.Length > 0U
						select mp).Distinct<PositionMatch>()
					select sr.Slice(mp.Position, new uint?(mp.Right))).ConvertToHashSet<ValueSubstring>();
				Symbol vsSymbol = this._grammar.InputSymbol;
				HashSet<ValueSubstring> inputs = learningInfo.FeatureCalculationContext.AllInputs.SelectMany(delegate(State state)
				{
					IRow row = (IRow)state[vsSymbol];
					if (row.ColumnNames != null)
					{
						return row.ColumnNames.Select((string columnName) => Semantics.ChooseInput(row, columnName));
					}
					return Enumerable.Empty<ValueSubstring>();
				}).ConvertToHashSet<ValueSubstring>();
				var list = enumerable.Select((ValueSubstring token) => new
				{
					token = token,
					count = inputs.Count((ValueSubstring inputColumn) => inputColumn != null && inputColumn.Value.Contains(token.Value))
				}).ToList();
				if (list.Any(t => t.count > 0))
				{
					num2 = 1.0;
					int specInputsCount = learningInfo.FeatureCalculationContext.MaterializeSpecInputs().Count;
					num += Math.Log(100.0);
					num3 = (double)list.Where(t => t.count > 0 && (t.count <= specInputsCount / 2 || (t.count == 1 && specInputsCount == 1))).Sum(t => (long)((ulong)t.token.Length / (ulong)((long)t.count)));
					num += (double)specInputsCount * num3;
					if (specInputsCount == 1)
					{
						num += Math.Log(1000.0);
						num *= 2.0;
					}
				}
			}
			double num4 = Math.Log((double)(length + 1));
			return new RankingSubfeatureCalculator.Score_ConstStrFeatures
			{
				ConstantStringLength = (double)length,
				LogConstantStringLength = num4,
				IsCommonDelimiter = (flag > false),
				ExampleCount = (double)((learningInfo != null) ? learningInfo.FeatureCalculationContext.MaterializeSpecInputs().Count : 0),
				AllInputsCount = (double)((learningInfo != null) ? learningInfo.FeatureCalculationContext.AllInputs.Count : 0),
				ConstantInInput = num2,
				ConstantinInputPenalty = num,
				ConditionalTokenCounts = num3
			};
		}

		// Token: 0x0600F86A RID: 63594 RVA: 0x0034ED18 File Offset: 0x0034CF18
		public RankingSubfeatureCalculator.Score_FormatNumericRangeFeatures Score_FormatNumericRange(ProgramNode number, ProgramNode numberFormat, ProgramNode separator, ProgramNode lowerSpec, ProgramNode upperSpec)
		{
			double num = (this._build.Node.Cast.roundingSpec(lowerSpec).Value.Delta % 5m == 0m) > false;
			double num2 = (double)(this._build.Node.Cast.roundingSpec(lowerSpec).Value.Delta % 5m);
			return new RankingSubfeatureCalculator.Score_FormatNumericRangeFeatures
			{
				RoundToMultipleOf5 = num,
				RoundToMultipleOf5Value = num2
			};
		}

		// Token: 0x0600F86B RID: 63595 RVA: 0x0034EDB4 File Offset: 0x0034CFB4
		public RankingSubfeatureCalculator.Score_FormatPartialDateTimeFeatures Score_FormatPartialDateTime(ProgramNode node)
		{
			return default(RankingSubfeatureCalculator.Score_FormatPartialDateTimeFeatures);
		}

		// Token: 0x0600F86C RID: 63596 RVA: 0x0034EDCC File Offset: 0x0034CFCC
		public RankingSubfeatureCalculator.Score_FormatPartialDateTimeFeatures Score_FormatPartialDateTime(ProgramNode datetime, ProgramNode outputFormat)
		{
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			inputDateTime_parsedDateTime inputDateTime_parsedDateTime;
			if (this._build.Node.Cast.datetime(datetime).Switch<inputDateTime>(this._build, (datetime_inputDateTime datetimeConversion) => datetimeConversion.inputDateTime, (RoundPartialDateTime round) => round.inputDateTime).Is_inputDateTime_parsedDateTime(this._build, out inputDateTime_parsedDateTime))
			{
				DateTimeFormat outputFormatValue = this._build.Node.Cast.outputDtFormat(outputFormat).Value;
				DateTimeFormat[] inputFormats = inputDateTime_parsedDateTime.parsedDateTime.Cast_ParsePartialDateTime().inputDtFormats.Value;
				if (inputFormats.Length == 1 && inputFormats[0].Equals(outputFormatValue))
				{
					num = 1.0;
				}
				if (outputFormatValue != null)
				{
					if (outputFormatValue.IsNumeric && outputFormatValue.MatchedParts.IsSingleton() && inputFormats.All((DateTimeFormat format) => format.IsNumeric && format.MatchedParts.Equals(outputFormatValue.MatchedParts)))
					{
						if (!inputFormats.AppendItem(outputFormatValue).Any(delegate(DateTimeFormat f)
						{
							DateTimePart? dateTimePart = f.MatchedParts.OnlyOrDefault();
							DateTimePart dateTimePart2 = DateTimePart.Year;
							if ((dateTimePart.GetValueOrDefault() == dateTimePart2) & (dateTimePart != null))
							{
								return f.FormatParts.Single((DateTimeFormatPart p) => p is NumericDateTimeFormatPart).MinimumLength == 4;
							}
							return false;
						}))
						{
							num2 = 1.0;
						}
					}
					if (inputFormats[0].FormatString.Contains(outputFormatValue.FormatString) && outputFormatValue.FormatParts.IsSubsequenceOf(inputFormats[0].FormatParts) && (inputFormats.Length == 1 || !inputFormats.Skip(1).Any((DateTimeFormat f) => DateTimeFormatUtil.MatchSameStrings(f, inputFormats[0]))))
					{
						num3 = (double)outputFormatValue.FormatParts.Count;
					}
				}
			}
			return new RankingSubfeatureCalculator.Score_FormatPartialDateTimeFeatures
			{
				SameDateFormat = num,
				SameNumberPenalty = num2,
				ExtractionMatches = num3
			};
		}

		// Token: 0x0600F86D RID: 63597 RVA: 0x0034F026 File Offset: 0x0034D226
		public double Score_FormatDateTimeRange_SeparatorContainsDigit(ProgramNode datetime, ProgramNode datetimeFormat, ProgramNode separator, ProgramNode lowerSpec, ProgramNode upperSpec)
		{
			IEnumerable<char> enumerable = (string)((LiteralNode)separator).Value;
			Func<char, bool> func;
			if ((func = RankingSubfeatureCalculator.<>O.<0>__IsDigit) == null)
			{
				func = (RankingSubfeatureCalculator.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
			}
			return enumerable.Any(func) > false;
		}

		// Token: 0x0600F86E RID: 63598 RVA: 0x0034F05C File Offset: 0x0034D25C
		public double Score_FormatDateTimeRange_SeparatorIsOnlySymbols(ProgramNode datetime, ProgramNode datetimeFormat, ProgramNode separator, ProgramNode lowerSpec, ProgramNode upperSpec)
		{
			return ((string)((LiteralNode)separator).Value).All((char c) => char.IsSymbol(c) || char.IsPunctuation(c)) > false;
		}

		// Token: 0x0600F86F RID: 63599 RVA: 0x0034F096 File Offset: 0x0034D296
		public double Score_FormatDateTimeRange_SeparatorIsOnlyWhitespace(ProgramNode datetime, ProgramNode datetimeFormat, ProgramNode separator, ProgramNode lowerSpec, ProgramNode upperSpec)
		{
			IEnumerable<char> enumerable = (string)((LiteralNode)separator).Value;
			Func<char, bool> func;
			if ((func = RankingSubfeatureCalculator.<>O.<1>__IsWhiteSpace) == null)
			{
				func = (RankingSubfeatureCalculator.<>O.<1>__IsWhiteSpace = new Func<char, bool>(char.IsWhiteSpace));
			}
			return enumerable.All(func) > false;
		}

		// Token: 0x0600F870 RID: 63600 RVA: 0x0034F0CC File Offset: 0x0034D2CC
		public double Score_FormatDateTimeRange_SeparatorIsWrappedByWhitespace(ProgramNode datetime, ProgramNode datetimeFormat, ProgramNode separator, ProgramNode lowerSpec, ProgramNode upperSpec)
		{
			string text = (string)((LiteralNode)separator).Value;
			double num;
			if (text.Length > 2 && char.IsWhiteSpace(text[0]) && char.IsWhiteSpace(text[text.Length - 1]))
			{
				IEnumerable<char> enumerable = text;
				Func<char, bool> func;
				if ((func = RankingSubfeatureCalculator.<>O.<1>__IsWhiteSpace) == null)
				{
					func = (RankingSubfeatureCalculator.<>O.<1>__IsWhiteSpace = new Func<char, bool>(char.IsWhiteSpace));
				}
				if (!enumerable.All(func))
				{
					num = (double)1;
					goto IL_0064;
				}
			}
			num = (double)0;
			IL_0064:
			return num;
		}

		// Token: 0x0600F871 RID: 63601 RVA: 0x0034F140 File Offset: 0x0034D340
		public double Score_FormatDateTimeRange_SeparatorIsCommonDateTimeSeparator(ProgramNode datetime, ProgramNode datetimeFormat, ProgramNode separator, ProgramNode lowerSpec, ProgramNode upperSpec)
		{
			string text = (string)((LiteralNode)separator).Value;
			DateTimeFormat dateTimeFormat = (DateTimeFormat)((LiteralNode)datetimeFormat).Value;
			bool? flag = DtFeatures.IsCommonDateTimeSeparator(text, dateTimeFormat.FormatParts.Last<DateTimeFormatPart>().MatchedPart, dateTimeFormat.FormatParts.First<DateTimeFormatPart>().MatchedPart);
			bool flag2 = true;
			return ((flag.GetValueOrDefault() == flag2) & (flag != null)) > false;
		}

		// Token: 0x0600F872 RID: 63602 RVA: 0x0034F1AC File Offset: 0x0034D3AC
		public static double Score_SubStr(double x, double PP)
		{
			if (PP <= 0.0)
			{
				return -10.0;
			}
			return Math.Log(PP);
		}

		// Token: 0x0600F873 RID: 63603 RVA: 0x0034F1CA File Offset: 0x0034D3CA
		public static double RelScore(double pl1, double pl2)
		{
			return Math.Abs(pl1 * pl2) * (double)((pl1 < 0.0 || pl2 < 0.0) ? (-1) : 1);
		}

		// Token: 0x0600F874 RID: 63604 RVA: 0x0034F1F2 File Offset: 0x0034D3F2
		public double Score_PosPair(double pp1Score, double pp2Score)
		{
			return Math.Abs(pp1Score * pp2Score) * (double)((pp1Score < 0.0 || pp2Score < 0.0) ? (-1) : 1);
		}

		// Token: 0x0600F875 RID: 63605 RVA: 0x0034F21C File Offset: 0x0034D41C
		public RankingSubfeatureCalculator.Score_PosPairFeatures Score_PosPair(ProgramNode pp1, ProgramNode pp2)
		{
			bool flag = false;
			bool flag2 = false;
			RegexPositionRelative regexPositionRelative;
			RegexPositionRelative regexPositionRelative2;
			if (this._build.Node.IsRule.RegexPositionRelative(pp1, out regexPositionRelative) && this._build.Node.IsRule.RegexPositionRelative(pp2, out regexPositionRelative2) && regexPositionRelative.k.Value == regexPositionRelative2.k.Value)
			{
				Record<RegularExpression, RegularExpression>? record = (Record<RegularExpression, RegularExpression>?)regexPositionRelative.regexPair.Node.Invoke(null);
				if (record != null && record.Value.Item1.Tokens.Length == 0)
				{
					Record<RegularExpression, RegularExpression>? record2 = (Record<RegularExpression, RegularExpression>?)regexPositionRelative2.regexPair.Node.Invoke(null);
					if (record2 != null && record2.Value.Item2.Tokens.Length == 0 && record.Value.Item2.Equals(record2.Value.Item1))
					{
						if (record.Value.Item2.Tokens.All((Token t) => t.IsSymbol || t.IsDynamicToken))
						{
							flag = true;
						}
						else
						{
							flag2 = true;
						}
					}
				}
			}
			return new RankingSubfeatureCalculator.Score_PosPairFeatures
			{
				ConstantRegexExtractionPenaltyFactorBias = (flag > false),
				RegexExtractionBonusBias = (flag2 > false)
			};
		}

		// Token: 0x0600F876 RID: 63606 RVA: 0x0034F384 File Offset: 0x0034D584
		public static double Score_Add(double pp1, double pp2)
		{
			return pp1 * pp2;
		}

		// Token: 0x0600F877 RID: 63607 RVA: 0x0034F38C File Offset: 0x0034D58C
		public RankingSubfeatureCalculator.DerivedScores_PosPairRelativeFeatures Score_RegexPositionPair(double xNodeScore, double rNodeScore, double kNodeScore)
		{
			return new RankingSubfeatureCalculator.DerivedScores_PosPairRelativeFeatures
			{
				PosPairRelativeFeaturesRrKk = rNodeScore * rNodeScore * kNodeScore * kNodeScore,
				PosPairRelativeFeaturesRKk = rNodeScore * kNodeScore * kNodeScore,
				PosPairRelativeFeaturesKk = kNodeScore * kNodeScore,
				PosPairRelativeFeaturesRScore = rNodeScore + RankingSubfeatureCalculator.EmptyRegularExpressionScore
			};
		}

		// Token: 0x0600F878 RID: 63608 RVA: 0x0034F3D4 File Offset: 0x0034D5D4
		public RankingSubfeatureCalculator.Score_PosPairRelativeFeatures Score_RegexPositionPair(LearningInfo learningInfo, VariableNode xNode, LiteralNode rNode, LiteralNode kNode)
		{
			Optional<double> optional = this.ProportionNullForColumn(learningInfo);
			double num = optional.Select(delegate(double proportionNotMatched)
			{
				if (proportionNotMatched >= 0.01)
				{
					return 0.001 * Math.Pow(1.0 - proportionNotMatched, 10.0);
				}
				return 1.0;
			}).OrElse(1.0);
			bool flag = ((RegularExpression)rNode.Value).Tokens.All((Token t) => t.IsSymbol || t.IsDynamicToken);
			return new RankingSubfeatureCalculator.Score_PosPairRelativeFeatures
			{
				RegexIsConstant = (flag > false),
				NotMatchedFactor = num,
				ProportionNull = optional.OrElse(0.0)
			};
		}

		// Token: 0x0600F879 RID: 63609 RVA: 0x0034F485 File Offset: 0x0034D685
		public double ExternalExtractor_ExtractorScore(CustomExtractor extractor)
		{
			return extractor.Score;
		}

		// Token: 0x0600F87A RID: 63610 RVA: 0x0034F48D File Offset: 0x0034D68D
		public static double Score_ExternalExtrationPositionPair(double x, double extractor, double k)
		{
			return extractor * k * k;
		}

		// Token: 0x0600F87B RID: 63611 RVA: 0x0034F494 File Offset: 0x0034D694
		public RankingSubfeatureCalculator.Score_AbsolutePositionFeatures Score_AbsolutePosition(LearningInfo learningInfo, ProgramNode node)
		{
			bool? flag = null;
			if (learningInfo != null)
			{
				if (this._allSameLengthCache.Value.Key == learningInfo.FeatureCalculationContext)
				{
					flag = new bool?(this._allSameLengthCache.Value.Value);
				}
				else if (learningInfo.FeatureCalculationContext.AllInputsCount > 5)
				{
					IReadOnlyList<State> allInputs = learningInfo.FeatureCalculationContext.AllInputs;
					object obj = null;
					State state2 = learningInfo.FeatureCalculationContext.MaterializeSpecInputs().FirstOrDefault<State>();
					if (state2 != null)
					{
						state2.TryGetValue(this._build.Symbol.columnName, out obj);
					}
					IEnumerable<string> enumerable;
					if (obj == null)
					{
						enumerable = ((IRow)allInputs[0][this._grammar.InputSymbol]).ColumnNames;
					}
					else
					{
						IEnumerable<string> enumerable2 = new string[] { (string)obj };
						enumerable = enumerable2;
					}
					IEnumerable<string> enumerable3 = enumerable;
					if (enumerable3 != null)
					{
						foreach (string text in enumerable3)
						{
							uint? num = null;
							foreach (IRow row in allInputs.Select((State state) => (IRow)state[this._grammar.InputSymbol]))
							{
								ValueSubstring valueSubstring = Semantics.ChooseInput(row, text);
								uint? num2 = ((valueSubstring != null) ? new uint?(valueSubstring.Length) : null);
								if (num2 != null)
								{
									if (num == null)
									{
										num = num2;
									}
									else
									{
										uint? num3 = num;
										uint? num4 = num2;
										if (!((num3.GetValueOrDefault() == num4.GetValueOrDefault()) & (num3 != null == (num4 != null))))
										{
											num = null;
											break;
										}
									}
								}
							}
							if (num == null)
							{
								flag = new bool?(false);
								break;
							}
						}
						if (flag == null)
						{
							flag = new bool?(true);
						}
						this._allSameLengthCache.Value = new KeyValuePair<FeatureCalculationContext, bool>(learningInfo.FeatureCalculationContext, flag.Value);
					}
				}
			}
			RankingSubfeatureCalculator.Score_AbsolutePositionFeatures score_AbsolutePositionFeatures = default(RankingSubfeatureCalculator.Score_AbsolutePositionFeatures);
			score_AbsolutePositionFeatures.IsLearningInfoNull = learningInfo == null;
			bool? flag2 = flag;
			bool flag3 = true;
			score_AbsolutePositionFeatures.AllSameLength = ((flag2.GetValueOrDefault() == flag3) & (flag2 != null)) > false;
			return score_AbsolutePositionFeatures;
		}

		// Token: 0x0600F87C RID: 63612 RVA: 0x0034F6F0 File Offset: 0x0034D8F0
		public double Score_RegexPosition(double x, double rr, double k)
		{
			return rr * k;
		}

		// Token: 0x0600F87D RID: 63613 RVA: 0x0034F6F8 File Offset: 0x0034D8F8
		public RankingSubfeatureCalculator.Score_RegexPositionFeatures Score_RegexPosition(LearningInfo learningInfo, ProgramNode node)
		{
			Optional<double> optional = this.ProportionNullForColumn(learningInfo);
			double num = optional.Select(delegate(double proportionNotMatched)
			{
				if (proportionNotMatched >= 0.01)
				{
					return 0.001 * Math.Pow(1.0 - proportionNotMatched, 10.0);
				}
				return 1.0;
			}).OrElse(1.0);
			return new RankingSubfeatureCalculator.Score_RegexPositionFeatures
			{
				UseProportionNotMatched = (optional.HasValue > false),
				NotMatchedFactor = num
			};
		}

		// Token: 0x0600F87E RID: 63614 RVA: 0x0034F764 File Offset: 0x0034D964
		public static RankingSubfeatureCalculator.KScoreFeatures KScore(int k)
		{
			return new RankingSubfeatureCalculator.KScoreFeatures
			{
				KPositive = (k >= 0),
				KScore = ((k >= 0) ? (1.0 / ((double)k + 1.0)) : (1.0 / ((double)(-(double)k) + 1.1)))
			};
		}

		// Token: 0x0600F87F RID: 63615 RVA: 0x0034F7C4 File Offset: 0x0034D9C4
		private Optional<string> MaybeGetColumnName(LearningInfo learningInfo)
		{
			State state = ((learningInfo != null) ? learningInfo.FeatureCalculationContext.MaterializeSpecInputs().FirstOrDefault<State>() : null);
			object obj;
			if (state != null && state.TryGetValue(this._build.Symbol.columnName, out obj))
			{
				return ((string)obj).Some<string>();
			}
			return Optional<string>.Nothing;
		}

		// Token: 0x0600F880 RID: 63616 RVA: 0x0034F818 File Offset: 0x0034DA18
		private State BindColumnNameIfMissing(State state, string columnName)
		{
			object obj;
			if (!state.TryGetValue(this._build.Symbol.columnName, out obj))
			{
				return state.Bind(this._build.Symbol.columnName, columnName).Bind(this._build.Symbol.x, Semantics.ChooseInput((IRow)state[this._grammar.InputSymbol], columnName));
			}
			return state;
		}

		// Token: 0x0600F881 RID: 63617 RVA: 0x0034F88C File Offset: 0x0034DA8C
		private Optional<double> ProportionNullForColumn(LearningInfo learningInfo)
		{
			Optional<string> columnName = this.MaybeGetColumnName(learningInfo);
			if (columnName.HasValue && learningInfo.FeatureCalculationContext.AdditionalInputs.Any<State>())
			{
				TransformationTextFeatureOptions transformationTextFeatureOptions = ((learningInfo != null) ? learningInfo.Options : null) as TransformationTextFeatureOptions;
				if (transformationTextFeatureOptions == null || !transformationTextFeatureOptions.SkipNullProportionCheck)
				{
					IEnumerable<State> enumerable = from i in learningInfo.AdditionalInputs.RandomlySample(this._randomSeed, 50)
						select this.BindColumnNameIfMissing(i, columnName.Value);
					int num = 0;
					int num2 = 0;
					foreach (State state in enumerable)
					{
						num++;
						if (learningInfo.ProgramNode.Invoke(state) == null)
						{
							num2++;
						}
					}
					return ((double)num2 / (double)num).Some<double>();
				}
			}
			return Optional<double>.Nothing;
		}

		// Token: 0x0600F882 RID: 63618 RVA: 0x0034F974 File Offset: 0x0034DB74
		public static RankingSubfeatureCalculator.RegexScoreFeatures RegexScore(RegularExpression r)
		{
			RankingSubfeatureCalculator.RegexScoreFeatures regexScoreFeatures = default(RankingSubfeatureCalculator.RegexScoreFeatures);
			regexScoreFeatures.RegexScore = (double)r.Score;
			regexScoreFeatures.TokenCount = (double)r.Count;
			regexScoreFeatures.TokenScoreSum = (double)r.Tokens.Sum((Token t) => t.Score);
			regexScoreFeatures.Token0Score = (double)((r.Count >= 1) ? r.Tokens[0].Score : 0);
			regexScoreFeatures.Token1Score = (double)((r.Count >= 2) ? r.Tokens[1].Score : 0);
			regexScoreFeatures.Token2Score = (double)((r.Count >= 3) ? r.Tokens[2].Score : 0);
			return regexScoreFeatures;
		}

		// Token: 0x0600F883 RID: 63619 RVA: 0x00164725 File Offset: 0x00162925
		public static double SScore(string s)
		{
			return (double)s.Length;
		}

		// Token: 0x0600F884 RID: 63620 RVA: 0x0034FA3C File Offset: 0x0034DC3C
		public static RankingSubfeatureCalculator.RoundingSpecScoreFeatures RoundingSpecScore(RoundingSpec roundingSpec)
		{
			bool flag = roundingSpec.Delta == (decimal)Math.Pow(10.0, (double)((int)Math.Log10((double)roundingSpec.Delta)));
			bool flag2 = roundingSpec.Zero == 0m;
			bool flag3 = roundingSpec.Mode == RoundingMode.Nearest;
			bool flag4 = roundingSpec.Mode == RoundingMode.TowardZero;
			bool flag5 = roundingSpec.Mode == RoundingMode.AwayFromZero;
			return new RankingSubfeatureCalculator.RoundingSpecScoreFeatures
			{
				Delta = (double)roundingSpec.Delta,
				LogDelta = Math.Log(1.0 + (double)roundingSpec.Delta),
				DeltaIsPowerOf10 = (flag > false),
				Zero = (double)roundingSpec.Zero,
				ZeroIsZero = (flag2 > false),
				RoundingMode = (double)roundingSpec.Mode,
				RoundingModeIsNearest = (flag3 > false),
				RoundingModeIsTowardZero = (flag4 > false),
				RoundingModeIsAwayFromZero = (flag5 > false)
			};
		}

		// Token: 0x0600F885 RID: 63621 RVA: 0x0034FB48 File Offset: 0x0034DD48
		public static RankingSubfeatureCalculator.DateTimeRoundingSpecScoreFeatures DateTimeRoundingSpecScore(DateTimeRoundingSpec roundingSpec)
		{
			double num = 0.0;
			switch (roundingSpec.Unit)
			{
			case DateTimePart.Year:
				num = 0.5;
				break;
			case DateTimePart.Hour:
				num = 0.4;
				break;
			case DateTimePart.Minute:
				num = 0.3;
				break;
			case DateTimePart.Second:
				num = 0.2;
				break;
			case DateTimePart.Millisecond:
				num = 0.1;
				break;
			}
			double num2 = (roundingSpec.UpperExcludePart != null) > false;
			double num4;
			double num8;
			if (roundingSpec.Unit != DateTimePart.Year)
			{
				double num3 = (double)((ulong)roundingSpec.Delta * (ulong)Semantics.GetMillisecondsForPart(roundingSpec.Unit));
				num4 = (double)Semantics.GetMillisecondsForPart(Semantics.GetNextLargerPart(roundingSpec.Unit)) % num3 == 0.0;
				long num5 = (long)((roundingSpec.UpperExcludePart != null) ? ((ulong)roundingSpec.UpperExcludeAmount * (ulong)Semantics.GetMillisecondsForPart(roundingSpec.UpperExcludePart.Value)) : 0UL);
				double num6 = num3 - (double)num5;
				double num7 = (double)Semantics.GetMillisecondsForPart(DateTimePart.Hour);
				num8 = ((num6 >= num7) ? (num7 / num6 + 0.01) : (num6 / num7));
			}
			else
			{
				num8 = ((roundingSpec.Delta >= 10U) ? (10.0 / roundingSpec.Delta + 0.01) : (roundingSpec.Delta / 10.0));
				num4 = roundingSpec.Delta <= 10U;
			}
			int delta = (int)roundingSpec.Delta;
			int num9 = roundingSpec.Unit.MaxValue() + 1;
			int num10 = MathUtils.GCD(delta, num9);
			int num11 = num9 / num10;
			double num12 = roundingSpec.Mode == RoundingMode.Up;
			return new RankingSubfeatureCalculator.DateTimeRoundingSpecScoreFeatures
			{
				UnitScore = num,
				RoundingSpecUnit = num * 10.0,
				IsCloseFactor = num4,
				UpperExcludePart = num2,
				DisplayDeltaRatio = num8,
				ReducedDenominatorInverse = 1.0 / (double)num11,
				IsRoundingUp = num12
			};
		}

		// Token: 0x0600F886 RID: 63622 RVA: 0x0034FD54 File Offset: 0x0034DF54
		public RankingSubfeatureCalculator.Score_BuildNumberFormatFeatures Score_BuildNumberFormat(ProgramNode node)
		{
			NumberFormat numberFormat = (NumberFormat)node.Invoke(null);
			uint num = numberFormat.MinLeadingZeros.OrElseDefault<uint>();
			bool hasValue = numberFormat.MinLeadingZeros.HasValue;
			uint num2 = numberFormat.MinTrailingZeros.OrElseDefault<uint>();
			bool hasValue2 = numberFormat.MinTrailingZeros.HasValue;
			uint num3 = numberFormat.MinLeadingZerosAndWhitespace.OrElseDefault<uint>();
			bool hasValue3 = numberFormat.MinLeadingZerosAndWhitespace.HasValue;
			uint num4 = numberFormat.MinTrailingZerosAndWhitespace.OrElseDefault<uint>();
			bool hasValue4 = numberFormat.MinTrailingZerosAndWhitespace.HasValue;
			uint num5 = numberFormat.MaxTrailingZeros.OrElseDefault<uint>();
			bool hasValue5 = numberFormat.MaxTrailingZeros.HasValue;
			bool flag = hasValue && hasValue3 && num > num3;
			bool flag2 = hasValue2 && hasValue4 && num2 > num4;
			bool flag3 = hasValue2 && hasValue4 && num2 < num4 && hasValue5;
			bool flag4 = hasValue2 && hasValue4 && num2 < num4 && !hasValue5;
			bool flag5 = hasValue2 && hasValue4 && num2 >= num4;
			bool flag6 = hasValue5 && hasValue4 && num5 < num4;
			bool flag7 = hasValue2 && hasValue5 && num2 == num5;
			bool flag8 = numberFormat.Details.Scale.HasValue && numberFormat.MaxTrailingZeros == 0U.Some<uint>();
			return new RankingSubfeatureCalculator.Score_BuildNumberFormatFeatures
			{
				MinLeadingZerosHasValue = (hasValue > false),
				MinLeadingZeros = num,
				MinTrailingZerosHasValue = (hasValue2 > false),
				MinTrailingZeros = num2,
				MaxTrailingZerosHasValue = (hasValue5 > false),
				MaxTrailingZeros = num5,
				MinLeadingZerosAndWhiteSpaceHasValue = (hasValue3 > false),
				MinLeadingZerosAndWhitespace = num3,
				MinTrailingZerosAndWhiteSpaceHasValue = (hasValue4 > false),
				MinTrailingZerosAndWhitespace = num4,
				MinLeadingZeros_greaterThan_MinLeadingZerosAndWhitespace = (flag > false),
				MinTrailingZeros_greaterThan_MinTrailingZerosAndWhitespace = (flag2 > false),
				MaxTrailingZeros_lessThan_MinTrailingZerosAndWhitespace = (flag6 > false),
				MinTrailingZeros_eq_MaxTrailingZeros = (flag7 > false),
				MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosHasValue = (flag3 > false),
				MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosNoValue = (flag4 > false),
				MinTrailingZeros_gte_minTrailingZerosAndWhitespace = (flag5 > false),
				ScaleHasValueAndMaxTrailingZerosIsZero = (flag8 > false),
				Scale = (double)numberFormat.Details.Scale.OrElse(1m),
				HasScale = (numberFormat.Details.Scale.HasValue > false),
				HasSeparator = (numberFormat.Details.SeparatorChar.HasValue > false)
			};
		}

		// Token: 0x0600F887 RID: 63623 RVA: 0x0034FFF8 File Offset: 0x0034E1F8
		public static RankingSubfeatureCalculator.OutputDtFormatScoreFeatures OutputDtFormatScore(LearningInfo learningInfo, DateTimeFormat outputDtFormat)
		{
			bool flag = false;
			TransformationTextFeatureOptions transformationTextFeatureOptions = ((learningInfo != null) ? learningInfo.Options : null) as TransformationTextFeatureOptions;
			DtFeatures dtFeatures = new DtFeatures(outputDtFormat, flag, transformationTextFeatureOptions != null && transformationTextFeatureOptions.AvoidImperialDateTimeFormat);
			return new RankingSubfeatureCalculator.OutputDtFormatScoreFeatures
			{
				TimeBeforeDate = dtFeatures.TimeBeforeDate,
				PeriodWithFullHour = dtFeatures.PeriodWithFullHour,
				HasOneDecimalPoint = dtFeatures.HasOneDecimalPoint,
				ConstantLength = dtFeatures.ConstantLength,
				DigitConstantLength = dtFeatures.DigitConstantLength,
				SeparatorKindMatches = dtFeatures.SeparatorKindMatches,
				SeparatorKindMisMatches = dtFeatures.SeparatorKindMisMatches,
				UnlikelySeparatorCount = dtFeatures.UnlikelySeparatorCount,
				SeparatorCount = dtFeatures.SeparatorCount,
				HasNonDelimitedNumbers = dtFeatures.HasNonDelimitedNumbers,
				IsNumeric = dtFeatures.IsNumeric,
				MinDateInversions = dtFeatures.MinDateInversions,
				MinTimeInversions = dtFeatures.MinTimeInversions,
				IsMatchingCommonDatePartsOrders = dtFeatures.IsMatchingCommonDatePartsOrders,
				DatePartOrderCount = dtFeatures.DatePartOrderCount,
				IsMatchingCommonTimePartsOrders = dtFeatures.IsMatchingCommonTimePartsOrders,
				TimePartOrderCount = dtFeatures.TimePartOrderCount,
				VariableLengthCount = dtFeatures.VariableLengthCount,
				MatchedPartsCount = dtFeatures.MatchedPartsCount,
				HasDayOfWeekInMonth = dtFeatures.HasDayOfWeekInMonth,
				TimeAndDateShareSeparator = dtFeatures.TimeAndDateShareSeparator,
				BetweenTimeDateSeparatorReused = dtFeatures.BetweenTimeDateSeparatorReused
			};
		}

		// Token: 0x0600F888 RID: 63624 RVA: 0x00350154 File Offset: 0x0034E354
		private static bool AnyAmbiguous(DateTimeFormat[] inputDtFormats)
		{
			for (int i = 0; i < inputDtFormats.Length; i++)
			{
				for (int j = i + 1; j < inputDtFormats.Length; j++)
				{
					if (DateTimeFormatUtil.IsAmbiguous(inputDtFormats[i], inputDtFormats[j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600F889 RID: 63625 RVA: 0x0035018F File Offset: 0x0034E38F
		internal static double _InputDtFormatScore(DateTimeFormat inputDtFormat, bool avoidImperialDateTimeFormat)
		{
			return new RankingSubfeatureCalculator.InputDtFeatures(inputDtFormat, avoidImperialDateTimeFormat).Score;
		}

		// Token: 0x0600F88A RID: 63626 RVA: 0x003501A0 File Offset: 0x0034E3A0
		public static RankingSubfeatureCalculator.InputDtFormatScoreFeatures InputDtFormatScore(LearningInfo learningInfo, DateTimeFormat[] inputDtFormats)
		{
			RankingSubfeatureCalculator.<>c__DisplayClass67_0 CS$<>8__locals1 = new RankingSubfeatureCalculator.<>c__DisplayClass67_0();
			RankingSubfeatureCalculator.<>c__DisplayClass67_0 CS$<>8__locals2 = CS$<>8__locals1;
			TransformationTextFeatureOptions transformationTextFeatureOptions = ((learningInfo != null) ? learningInfo.Options : null) as TransformationTextFeatureOptions;
			CS$<>8__locals2.avoidImperialDateTimeFormat = transformationTextFeatureOptions != null && transformationTextFeatureOptions.AvoidImperialDateTimeFormat;
			var list = (from format in inputDtFormats
				select new
				{
					format = format,
					features = new RankingSubfeatureCalculator.InputDtFeatures(format, CS$<>8__locals1.avoidImperialDateTimeFormat)
				} into o
				orderby o.features.Score descending
				select o).ToList();
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < list.Count - 1; i++)
			{
				if (!hashSet.Contains(i))
				{
					for (int j = i + 1; j < list.Count; j++)
					{
						if (!hashSet.Contains(j) && DateTimeFormatUtil.MatchSameStrings(list[i].format, list[j].format))
						{
							hashSet.Add(j);
						}
					}
				}
			}
			for (int k = list.Count - 1; k >= 0; k--)
			{
				if (hashSet.Contains(k))
				{
					list.RemoveAt(k);
				}
			}
			bool flag = inputDtFormats.Select((DateTimeFormat f) => f.MatchedParts).Distinct<DateTimePartSet>().Count<DateTimePartSet>() > 1;
			double score = list.First().features.Score;
			double num = list.Average(o => o.features.Score);
			double score2 = list.Last().features.Score;
			bool flag2 = RankingSubfeatureCalculator.AnyAmbiguous(inputDtFormats);
			return new RankingSubfeatureCalculator.InputDtFormatScoreFeatures
			{
				MaxScore = score,
				MinScore = score2,
				AverageScore = num,
				NumFormats = (double)list.Count,
				AnyAmbiguous = (flag2 > false),
				MatchedDifferentParts = (flag > false)
			};
		}

		// Token: 0x0600F88B RID: 63627 RVA: 0x00350380 File Offset: 0x0034E580
		public double CastScore(LearningInfo learningInfo, ProgramNode node)
		{
			bool flag = false;
			if (learningInfo != null)
			{
				State state = learningInfo.FeatureCalculationContext.MaterializeSpecInputs().FirstOrDefault<State>() ?? learningInfo.FeatureCalculationContext.AdditionalInputs.FirstOrDefault<State>();
				Optional<string> optional = this.MaybeGetColumnName(learningInfo);
				if (state != null && optional.HasValue)
				{
					object obj = Semantics.LookupInput((IRow)state[this._grammar.InputSymbol], optional.Value);
					if (obj is string || obj is ValueSubstring)
					{
						flag = true;
					}
				}
			}
			return flag > false;
		}

		// Token: 0x04005BE2 RID: 23522
		private readonly WholeColumnsUsed _wholeColumnsUsed;

		// Token: 0x04005BE3 RID: 23523
		private readonly InputsUsed _inputsUsed;

		// Token: 0x04005BE4 RID: 23524
		private readonly int _randomSeed;

		// Token: 0x04005BE5 RID: 23525
		private readonly Grammar _grammar;

		// Token: 0x04005BE6 RID: 23526
		private readonly GrammarBuilders _build;

		// Token: 0x04005BE7 RID: 23527
		private static readonly IReadOnlyList<string> CommonDelimiters = new List<string>
		{
			" ", ",", ";", "-", "/", ".", "\\", "(", ")", "'",
			"\""
		};

		// Token: 0x04005BE8 RID: 23528
		private static readonly string[] AlphanumericTokens = new string[] { "Upper Case", "List of Lower Case", "Camel Case", "Lowercase word", "ALL CAPS", "Number", "Alphanumeric" };

		// Token: 0x04005BE9 RID: 23529
		private const int NullCheckSampleSize = 50;

		// Token: 0x04005BEA RID: 23530
		private const double NullCheckProportionEpsilon = 0.01;

		// Token: 0x04005BEB RID: 23531
		private static readonly Regex TwoDigitsRegexp = new Regex("[0-9]{2}", RegexOptions.Compiled);

		// Token: 0x04005BEC RID: 23532
		private static readonly double EmptyRegularExpressionScore = (double)new RegularExpression(0).Score / 1000.0;

		// Token: 0x04005BED RID: 23533
		private readonly ThreadLocal<KeyValuePair<FeatureCalculationContext, bool>> _allSameLengthCache = new ThreadLocal<KeyValuePair<FeatureCalculationContext, bool>>();

		// Token: 0x04005BEE RID: 23534
		private const int RegexNotMatchedPower = 10;

		// Token: 0x04005BEF RID: 23535
		private const double RegexNotMatchedPenaltyFactor = 0.001;

		// Token: 0x02001CA8 RID: 7336
		public struct Score_ConcatFeatures
		{
			// Token: 0x17002964 RID: 10596
			// (get) Token: 0x0600F88E RID: 63630 RVA: 0x0035051E File Offset: 0x0034E71E
			// (set) Token: 0x0600F88F RID: 63631 RVA: 0x00350526 File Offset: 0x0034E726
			public double NewInputsCount { readonly get; set; }

			// Token: 0x17002965 RID: 10597
			// (get) Token: 0x0600F890 RID: 63632 RVA: 0x0035052F File Offset: 0x0034E72F
			// (set) Token: 0x0600F891 RID: 63633 RVA: 0x00350537 File Offset: 0x0034E737
			public double RepeatWholeColumnsCount { readonly get; set; }

			// Token: 0x17002966 RID: 10598
			// (get) Token: 0x0600F892 RID: 63634 RVA: 0x00350540 File Offset: 0x0034E740
			// (set) Token: 0x0600F893 RID: 63635 RVA: 0x00350548 File Offset: 0x0034E748
			public double BothSidesConstant { readonly get; set; }

			// Token: 0x17002967 RID: 10599
			// (get) Token: 0x0600F894 RID: 63636 RVA: 0x00350551 File Offset: 0x0034E751
			// (set) Token: 0x0600F895 RID: 63637 RVA: 0x00350559 File Offset: 0x0034E759
			public double ConcatNumbers { readonly get; set; }

			// Token: 0x17002968 RID: 10600
			// (get) Token: 0x0600F896 RID: 63638 RVA: 0x00350562 File Offset: 0x0034E762
			// (set) Token: 0x0600F897 RID: 63639 RVA: 0x0035056A File Offset: 0x0034E76A
			public double FValueLen { readonly get; set; }

			// Token: 0x17002969 RID: 10601
			// (get) Token: 0x0600F898 RID: 63640 RVA: 0x00350573 File Offset: 0x0034E773
			// (set) Token: 0x0600F899 RID: 63641 RVA: 0x0035057B File Offset: 0x0034E77B
			public double EValueLen { readonly get; set; }

			// Token: 0x1700296A RID: 10602
			// (get) Token: 0x0600F89A RID: 63642 RVA: 0x00350584 File Offset: 0x0034E784
			// (set) Token: 0x0600F89B RID: 63643 RVA: 0x0035058C File Offset: 0x0034E78C
			public double FValueLast { readonly get; set; }

			// Token: 0x1700296B RID: 10603
			// (get) Token: 0x0600F89C RID: 63644 RVA: 0x00350595 File Offset: 0x0034E795
			// (set) Token: 0x0600F89D RID: 63645 RVA: 0x0035059D File Offset: 0x0034E79D
			public double EValueFirst { readonly get; set; }

			// Token: 0x1700296C RID: 10604
			// (get) Token: 0x0600F89E RID: 63646 RVA: 0x003505A6 File Offset: 0x0034E7A6
			// (set) Token: 0x0600F89F RID: 63647 RVA: 0x003505AE File Offset: 0x0034E7AE
			public double ConcatNonCommonConstants { readonly get; set; }

			// Token: 0x1700296D RID: 10605
			// (get) Token: 0x0600F8A0 RID: 63648 RVA: 0x003505B7 File Offset: 0x0034E7B7
			// (set) Token: 0x0600F8A1 RID: 63649 RVA: 0x003505BF File Offset: 0x0034E7BF
			public double FContainsCommonDelimiters { readonly get; set; }

			// Token: 0x1700296E RID: 10606
			// (get) Token: 0x0600F8A2 RID: 63650 RVA: 0x003505C8 File Offset: 0x0034E7C8
			// (set) Token: 0x0600F8A3 RID: 63651 RVA: 0x003505D0 File Offset: 0x0034E7D0
			public double EContainsCommonDelimiters { readonly get; set; }

			// Token: 0x1700296F RID: 10607
			// (get) Token: 0x0600F8A4 RID: 63652 RVA: 0x003505D9 File Offset: 0x0034E7D9
			// (set) Token: 0x0600F8A5 RID: 63653 RVA: 0x003505E1 File Offset: 0x0034E7E1
			public double FInputsCount { readonly get; set; }

			// Token: 0x17002970 RID: 10608
			// (get) Token: 0x0600F8A6 RID: 63654 RVA: 0x003505EA File Offset: 0x0034E7EA
			// (set) Token: 0x0600F8A7 RID: 63655 RVA: 0x003505F2 File Offset: 0x0034E7F2
			public double EInputsCount { readonly get; set; }

			// Token: 0x17002971 RID: 10609
			// (get) Token: 0x0600F8A8 RID: 63656 RVA: 0x003505FB File Offset: 0x0034E7FB
			// (set) Token: 0x0600F8A9 RID: 63657 RVA: 0x00350603 File Offset: 0x0034E803
			public double FWholeColumnsCount { readonly get; set; }

			// Token: 0x17002972 RID: 10610
			// (get) Token: 0x0600F8AA RID: 63658 RVA: 0x0035060C File Offset: 0x0034E80C
			// (set) Token: 0x0600F8AB RID: 63659 RVA: 0x00350614 File Offset: 0x0034E814
			public double EWholeColumnsCount { readonly get; set; }
		}

		// Token: 0x02001CA9 RID: 7337
		public struct Score_ConstStrFeatures
		{
			// Token: 0x17002973 RID: 10611
			// (get) Token: 0x0600F8AC RID: 63660 RVA: 0x0035061D File Offset: 0x0034E81D
			// (set) Token: 0x0600F8AD RID: 63661 RVA: 0x00350625 File Offset: 0x0034E825
			public double ConstantStringLength { readonly get; set; }

			// Token: 0x17002974 RID: 10612
			// (get) Token: 0x0600F8AE RID: 63662 RVA: 0x0035062E File Offset: 0x0034E82E
			// (set) Token: 0x0600F8AF RID: 63663 RVA: 0x00350636 File Offset: 0x0034E836
			public double LogConstantStringLength { readonly get; set; }

			// Token: 0x17002975 RID: 10613
			// (get) Token: 0x0600F8B0 RID: 63664 RVA: 0x0035063F File Offset: 0x0034E83F
			// (set) Token: 0x0600F8B1 RID: 63665 RVA: 0x00350647 File Offset: 0x0034E847
			public double IsCommonDelimiter { readonly get; set; }

			// Token: 0x17002976 RID: 10614
			// (get) Token: 0x0600F8B2 RID: 63666 RVA: 0x00350650 File Offset: 0x0034E850
			// (set) Token: 0x0600F8B3 RID: 63667 RVA: 0x00350658 File Offset: 0x0034E858
			public double ExampleCount { readonly get; set; }

			// Token: 0x17002977 RID: 10615
			// (get) Token: 0x0600F8B4 RID: 63668 RVA: 0x00350661 File Offset: 0x0034E861
			// (set) Token: 0x0600F8B5 RID: 63669 RVA: 0x00350669 File Offset: 0x0034E869
			public double AllInputsCount { readonly get; set; }

			// Token: 0x17002978 RID: 10616
			// (get) Token: 0x0600F8B6 RID: 63670 RVA: 0x00350672 File Offset: 0x0034E872
			// (set) Token: 0x0600F8B7 RID: 63671 RVA: 0x0035067A File Offset: 0x0034E87A
			public double ConstantInInput { readonly get; set; }

			// Token: 0x17002979 RID: 10617
			// (get) Token: 0x0600F8B8 RID: 63672 RVA: 0x00350683 File Offset: 0x0034E883
			// (set) Token: 0x0600F8B9 RID: 63673 RVA: 0x0035068B File Offset: 0x0034E88B
			public double ConstantinInputPenalty { readonly get; set; }

			// Token: 0x1700297A RID: 10618
			// (get) Token: 0x0600F8BA RID: 63674 RVA: 0x00350694 File Offset: 0x0034E894
			// (set) Token: 0x0600F8BB RID: 63675 RVA: 0x0035069C File Offset: 0x0034E89C
			public double ConditionalTokenCounts { readonly get; set; }
		}

		// Token: 0x02001CAA RID: 7338
		public struct Score_FormatNumericRangeFeatures
		{
			// Token: 0x1700297B RID: 10619
			// (get) Token: 0x0600F8BC RID: 63676 RVA: 0x003506A5 File Offset: 0x0034E8A5
			// (set) Token: 0x0600F8BD RID: 63677 RVA: 0x003506AD File Offset: 0x0034E8AD
			public double RoundToMultipleOf5 { readonly get; set; }

			// Token: 0x1700297C RID: 10620
			// (get) Token: 0x0600F8BE RID: 63678 RVA: 0x003506B6 File Offset: 0x0034E8B6
			// (set) Token: 0x0600F8BF RID: 63679 RVA: 0x003506BE File Offset: 0x0034E8BE
			public double RoundToMultipleOf5Value { readonly get; set; }
		}

		// Token: 0x02001CAB RID: 7339
		public struct Score_FormatPartialDateTimeFeatures
		{
			// Token: 0x1700297D RID: 10621
			// (get) Token: 0x0600F8C0 RID: 63680 RVA: 0x003506C7 File Offset: 0x0034E8C7
			// (set) Token: 0x0600F8C1 RID: 63681 RVA: 0x003506CF File Offset: 0x0034E8CF
			public double SameDateFormat { readonly get; set; }

			// Token: 0x1700297E RID: 10622
			// (get) Token: 0x0600F8C2 RID: 63682 RVA: 0x003506D8 File Offset: 0x0034E8D8
			// (set) Token: 0x0600F8C3 RID: 63683 RVA: 0x003506E0 File Offset: 0x0034E8E0
			public double SameNumberPenalty { readonly get; set; }

			// Token: 0x1700297F RID: 10623
			// (get) Token: 0x0600F8C4 RID: 63684 RVA: 0x003506E9 File Offset: 0x0034E8E9
			// (set) Token: 0x0600F8C5 RID: 63685 RVA: 0x003506F1 File Offset: 0x0034E8F1
			public double ExtractionMatches { readonly get; set; }
		}

		// Token: 0x02001CAC RID: 7340
		public struct Score_PosPairFeatures
		{
			// Token: 0x17002980 RID: 10624
			// (get) Token: 0x0600F8C6 RID: 63686 RVA: 0x003506FA File Offset: 0x0034E8FA
			// (set) Token: 0x0600F8C7 RID: 63687 RVA: 0x00350702 File Offset: 0x0034E902
			public double ConstantRegexExtractionPenaltyFactorBias { readonly get; set; }

			// Token: 0x17002981 RID: 10625
			// (get) Token: 0x0600F8C8 RID: 63688 RVA: 0x0035070B File Offset: 0x0034E90B
			// (set) Token: 0x0600F8C9 RID: 63689 RVA: 0x00350713 File Offset: 0x0034E913
			public double RegexExtractionBonusBias { readonly get; set; }
		}

		// Token: 0x02001CAD RID: 7341
		public struct DerivedScores_PosPairRelativeFeatures
		{
			// Token: 0x17002982 RID: 10626
			// (get) Token: 0x0600F8CA RID: 63690 RVA: 0x0035071C File Offset: 0x0034E91C
			// (set) Token: 0x0600F8CB RID: 63691 RVA: 0x00350724 File Offset: 0x0034E924
			public double PosPairRelativeFeaturesRrKk { readonly get; set; }

			// Token: 0x17002983 RID: 10627
			// (get) Token: 0x0600F8CC RID: 63692 RVA: 0x0035072D File Offset: 0x0034E92D
			// (set) Token: 0x0600F8CD RID: 63693 RVA: 0x00350735 File Offset: 0x0034E935
			public double PosPairRelativeFeaturesRKk { readonly get; set; }

			// Token: 0x17002984 RID: 10628
			// (get) Token: 0x0600F8CE RID: 63694 RVA: 0x0035073E File Offset: 0x0034E93E
			// (set) Token: 0x0600F8CF RID: 63695 RVA: 0x00350746 File Offset: 0x0034E946
			public double PosPairRelativeFeaturesKk { readonly get; set; }

			// Token: 0x17002985 RID: 10629
			// (get) Token: 0x0600F8D0 RID: 63696 RVA: 0x0035074F File Offset: 0x0034E94F
			// (set) Token: 0x0600F8D1 RID: 63697 RVA: 0x00350757 File Offset: 0x0034E957
			public double PosPairRelativeFeaturesRScore { readonly get; set; }
		}

		// Token: 0x02001CAE RID: 7342
		public struct Score_PosPairRelativeFeatures
		{
			// Token: 0x17002986 RID: 10630
			// (get) Token: 0x0600F8D2 RID: 63698 RVA: 0x00350760 File Offset: 0x0034E960
			// (set) Token: 0x0600F8D3 RID: 63699 RVA: 0x00350768 File Offset: 0x0034E968
			public double RegexIsConstant { readonly get; set; }

			// Token: 0x17002987 RID: 10631
			// (get) Token: 0x0600F8D4 RID: 63700 RVA: 0x00350771 File Offset: 0x0034E971
			// (set) Token: 0x0600F8D5 RID: 63701 RVA: 0x00350779 File Offset: 0x0034E979
			public double NotMatchedFactor { readonly get; set; }

			// Token: 0x17002988 RID: 10632
			// (get) Token: 0x0600F8D6 RID: 63702 RVA: 0x00350782 File Offset: 0x0034E982
			// (set) Token: 0x0600F8D7 RID: 63703 RVA: 0x0035078A File Offset: 0x0034E98A
			public double ProportionNull { readonly get; set; }
		}

		// Token: 0x02001CAF RID: 7343
		public struct Score_AbsolutePositionFeatures
		{
			// Token: 0x17002989 RID: 10633
			// (get) Token: 0x0600F8D8 RID: 63704 RVA: 0x00350793 File Offset: 0x0034E993
			// (set) Token: 0x0600F8D9 RID: 63705 RVA: 0x0035079B File Offset: 0x0034E99B
			public double IsLearningInfoNull { readonly get; set; }

			// Token: 0x1700298A RID: 10634
			// (get) Token: 0x0600F8DA RID: 63706 RVA: 0x003507A4 File Offset: 0x0034E9A4
			// (set) Token: 0x0600F8DB RID: 63707 RVA: 0x003507AC File Offset: 0x0034E9AC
			public double AllSameLength { readonly get; set; }
		}

		// Token: 0x02001CB0 RID: 7344
		public struct Score_RegexPositionFeatures
		{
			// Token: 0x1700298B RID: 10635
			// (get) Token: 0x0600F8DC RID: 63708 RVA: 0x003507B5 File Offset: 0x0034E9B5
			// (set) Token: 0x0600F8DD RID: 63709 RVA: 0x003507BD File Offset: 0x0034E9BD
			public double UseProportionNotMatched { readonly get; set; }

			// Token: 0x1700298C RID: 10636
			// (get) Token: 0x0600F8DE RID: 63710 RVA: 0x003507C6 File Offset: 0x0034E9C6
			// (set) Token: 0x0600F8DF RID: 63711 RVA: 0x003507CE File Offset: 0x0034E9CE
			public double NotMatchedFactor { readonly get; set; }
		}

		// Token: 0x02001CB1 RID: 7345
		public struct KScoreFeatures
		{
			// Token: 0x1700298D RID: 10637
			// (get) Token: 0x0600F8E0 RID: 63712 RVA: 0x003507D7 File Offset: 0x0034E9D7
			// (set) Token: 0x0600F8E1 RID: 63713 RVA: 0x003507DF File Offset: 0x0034E9DF
			public double KPositive { readonly get; set; }

			// Token: 0x1700298E RID: 10638
			// (get) Token: 0x0600F8E2 RID: 63714 RVA: 0x003507E8 File Offset: 0x0034E9E8
			// (set) Token: 0x0600F8E3 RID: 63715 RVA: 0x003507F0 File Offset: 0x0034E9F0
			public double KScore { readonly get; set; }
		}

		// Token: 0x02001CB2 RID: 7346
		public struct RegexScoreFeatures
		{
			// Token: 0x1700298F RID: 10639
			// (get) Token: 0x0600F8E4 RID: 63716 RVA: 0x003507F9 File Offset: 0x0034E9F9
			// (set) Token: 0x0600F8E5 RID: 63717 RVA: 0x00350801 File Offset: 0x0034EA01
			public double RegexScore { readonly get; set; }

			// Token: 0x17002990 RID: 10640
			// (get) Token: 0x0600F8E6 RID: 63718 RVA: 0x0035080A File Offset: 0x0034EA0A
			// (set) Token: 0x0600F8E7 RID: 63719 RVA: 0x00350812 File Offset: 0x0034EA12
			public double TokenCount { readonly get; set; }

			// Token: 0x17002991 RID: 10641
			// (get) Token: 0x0600F8E8 RID: 63720 RVA: 0x0035081B File Offset: 0x0034EA1B
			// (set) Token: 0x0600F8E9 RID: 63721 RVA: 0x00350823 File Offset: 0x0034EA23
			public double TokenScoreSum { readonly get; set; }

			// Token: 0x17002992 RID: 10642
			// (get) Token: 0x0600F8EA RID: 63722 RVA: 0x0035082C File Offset: 0x0034EA2C
			// (set) Token: 0x0600F8EB RID: 63723 RVA: 0x00350834 File Offset: 0x0034EA34
			public double Token0Score { readonly get; set; }

			// Token: 0x17002993 RID: 10643
			// (get) Token: 0x0600F8EC RID: 63724 RVA: 0x0035083D File Offset: 0x0034EA3D
			// (set) Token: 0x0600F8ED RID: 63725 RVA: 0x00350845 File Offset: 0x0034EA45
			public double Token1Score { readonly get; set; }

			// Token: 0x17002994 RID: 10644
			// (get) Token: 0x0600F8EE RID: 63726 RVA: 0x0035084E File Offset: 0x0034EA4E
			// (set) Token: 0x0600F8EF RID: 63727 RVA: 0x00350856 File Offset: 0x0034EA56
			public double Token2Score { readonly get; set; }
		}

		// Token: 0x02001CB3 RID: 7347
		public struct RoundingSpecScoreFeatures
		{
			// Token: 0x17002995 RID: 10645
			// (get) Token: 0x0600F8F0 RID: 63728 RVA: 0x0035085F File Offset: 0x0034EA5F
			// (set) Token: 0x0600F8F1 RID: 63729 RVA: 0x00350867 File Offset: 0x0034EA67
			public double Delta { readonly get; set; }

			// Token: 0x17002996 RID: 10646
			// (get) Token: 0x0600F8F2 RID: 63730 RVA: 0x00350870 File Offset: 0x0034EA70
			// (set) Token: 0x0600F8F3 RID: 63731 RVA: 0x00350878 File Offset: 0x0034EA78
			public double LogDelta { readonly get; set; }

			// Token: 0x17002997 RID: 10647
			// (get) Token: 0x0600F8F4 RID: 63732 RVA: 0x00350881 File Offset: 0x0034EA81
			// (set) Token: 0x0600F8F5 RID: 63733 RVA: 0x00350889 File Offset: 0x0034EA89
			public double DeltaIsPowerOf10 { readonly get; set; }

			// Token: 0x17002998 RID: 10648
			// (get) Token: 0x0600F8F6 RID: 63734 RVA: 0x00350892 File Offset: 0x0034EA92
			// (set) Token: 0x0600F8F7 RID: 63735 RVA: 0x0035089A File Offset: 0x0034EA9A
			public double Zero { readonly get; set; }

			// Token: 0x17002999 RID: 10649
			// (get) Token: 0x0600F8F8 RID: 63736 RVA: 0x003508A3 File Offset: 0x0034EAA3
			// (set) Token: 0x0600F8F9 RID: 63737 RVA: 0x003508AB File Offset: 0x0034EAAB
			public double ZeroIsZero { readonly get; set; }

			// Token: 0x1700299A RID: 10650
			// (get) Token: 0x0600F8FA RID: 63738 RVA: 0x003508B4 File Offset: 0x0034EAB4
			// (set) Token: 0x0600F8FB RID: 63739 RVA: 0x003508BC File Offset: 0x0034EABC
			public double RoundingMode { readonly get; set; }

			// Token: 0x1700299B RID: 10651
			// (get) Token: 0x0600F8FC RID: 63740 RVA: 0x003508C5 File Offset: 0x0034EAC5
			// (set) Token: 0x0600F8FD RID: 63741 RVA: 0x003508CD File Offset: 0x0034EACD
			public double RoundingModeIsNearest { readonly get; set; }

			// Token: 0x1700299C RID: 10652
			// (get) Token: 0x0600F8FE RID: 63742 RVA: 0x003508D6 File Offset: 0x0034EAD6
			// (set) Token: 0x0600F8FF RID: 63743 RVA: 0x003508DE File Offset: 0x0034EADE
			public double RoundingModeIsTowardZero { readonly get; set; }

			// Token: 0x1700299D RID: 10653
			// (get) Token: 0x0600F900 RID: 63744 RVA: 0x003508E7 File Offset: 0x0034EAE7
			// (set) Token: 0x0600F901 RID: 63745 RVA: 0x003508EF File Offset: 0x0034EAEF
			public double RoundingModeIsAwayFromZero { readonly get; set; }
		}

		// Token: 0x02001CB4 RID: 7348
		public struct DateTimeRoundingSpecScoreFeatures
		{
			// Token: 0x1700299E RID: 10654
			// (get) Token: 0x0600F902 RID: 63746 RVA: 0x003508F8 File Offset: 0x0034EAF8
			// (set) Token: 0x0600F903 RID: 63747 RVA: 0x00350900 File Offset: 0x0034EB00
			public double UnitScore { readonly get; set; }

			// Token: 0x1700299F RID: 10655
			// (get) Token: 0x0600F904 RID: 63748 RVA: 0x00350909 File Offset: 0x0034EB09
			// (set) Token: 0x0600F905 RID: 63749 RVA: 0x00350911 File Offset: 0x0034EB11
			public double RoundingSpecUnit { readonly get; set; }

			// Token: 0x170029A0 RID: 10656
			// (get) Token: 0x0600F906 RID: 63750 RVA: 0x0035091A File Offset: 0x0034EB1A
			// (set) Token: 0x0600F907 RID: 63751 RVA: 0x00350922 File Offset: 0x0034EB22
			public double IsCloseFactor { readonly get; set; }

			// Token: 0x170029A1 RID: 10657
			// (get) Token: 0x0600F908 RID: 63752 RVA: 0x0035092B File Offset: 0x0034EB2B
			// (set) Token: 0x0600F909 RID: 63753 RVA: 0x00350933 File Offset: 0x0034EB33
			public double UpperExcludePart { readonly get; set; }

			// Token: 0x170029A2 RID: 10658
			// (get) Token: 0x0600F90A RID: 63754 RVA: 0x0035093C File Offset: 0x0034EB3C
			// (set) Token: 0x0600F90B RID: 63755 RVA: 0x00350944 File Offset: 0x0034EB44
			public double DisplayDeltaRatio { readonly get; set; }

			// Token: 0x170029A3 RID: 10659
			// (get) Token: 0x0600F90C RID: 63756 RVA: 0x0035094D File Offset: 0x0034EB4D
			// (set) Token: 0x0600F90D RID: 63757 RVA: 0x00350955 File Offset: 0x0034EB55
			public double ReducedDenominatorInverse { readonly get; set; }

			// Token: 0x170029A4 RID: 10660
			// (get) Token: 0x0600F90E RID: 63758 RVA: 0x0035095E File Offset: 0x0034EB5E
			// (set) Token: 0x0600F90F RID: 63759 RVA: 0x00350966 File Offset: 0x0034EB66
			public double IsRoundingUp { readonly get; set; }
		}

		// Token: 0x02001CB5 RID: 7349
		public struct Score_BuildNumberFormatFeatures
		{
			// Token: 0x170029A5 RID: 10661
			// (get) Token: 0x0600F910 RID: 63760 RVA: 0x0035096F File Offset: 0x0034EB6F
			// (set) Token: 0x0600F911 RID: 63761 RVA: 0x00350977 File Offset: 0x0034EB77
			public double MinLeadingZerosHasValue { readonly get; set; }

			// Token: 0x170029A6 RID: 10662
			// (get) Token: 0x0600F912 RID: 63762 RVA: 0x00350980 File Offset: 0x0034EB80
			// (set) Token: 0x0600F913 RID: 63763 RVA: 0x00350988 File Offset: 0x0034EB88
			public double MinLeadingZeros { readonly get; set; }

			// Token: 0x170029A7 RID: 10663
			// (get) Token: 0x0600F914 RID: 63764 RVA: 0x00350991 File Offset: 0x0034EB91
			// (set) Token: 0x0600F915 RID: 63765 RVA: 0x00350999 File Offset: 0x0034EB99
			public double MinTrailingZerosHasValue { readonly get; set; }

			// Token: 0x170029A8 RID: 10664
			// (get) Token: 0x0600F916 RID: 63766 RVA: 0x003509A2 File Offset: 0x0034EBA2
			// (set) Token: 0x0600F917 RID: 63767 RVA: 0x003509AA File Offset: 0x0034EBAA
			public double MinTrailingZeros { readonly get; set; }

			// Token: 0x170029A9 RID: 10665
			// (get) Token: 0x0600F918 RID: 63768 RVA: 0x003509B3 File Offset: 0x0034EBB3
			// (set) Token: 0x0600F919 RID: 63769 RVA: 0x003509BB File Offset: 0x0034EBBB
			public double MaxTrailingZerosHasValue { readonly get; set; }

			// Token: 0x170029AA RID: 10666
			// (get) Token: 0x0600F91A RID: 63770 RVA: 0x003509C4 File Offset: 0x0034EBC4
			// (set) Token: 0x0600F91B RID: 63771 RVA: 0x003509CC File Offset: 0x0034EBCC
			public double MaxTrailingZeros { readonly get; set; }

			// Token: 0x170029AB RID: 10667
			// (get) Token: 0x0600F91C RID: 63772 RVA: 0x003509D5 File Offset: 0x0034EBD5
			// (set) Token: 0x0600F91D RID: 63773 RVA: 0x003509DD File Offset: 0x0034EBDD
			public double MinLeadingZerosAndWhiteSpaceHasValue { readonly get; set; }

			// Token: 0x170029AC RID: 10668
			// (get) Token: 0x0600F91E RID: 63774 RVA: 0x003509E6 File Offset: 0x0034EBE6
			// (set) Token: 0x0600F91F RID: 63775 RVA: 0x003509EE File Offset: 0x0034EBEE
			public double MinLeadingZerosAndWhitespace { readonly get; set; }

			// Token: 0x170029AD RID: 10669
			// (get) Token: 0x0600F920 RID: 63776 RVA: 0x003509F7 File Offset: 0x0034EBF7
			// (set) Token: 0x0600F921 RID: 63777 RVA: 0x003509FF File Offset: 0x0034EBFF
			public double MinTrailingZerosAndWhiteSpaceHasValue { readonly get; set; }

			// Token: 0x170029AE RID: 10670
			// (get) Token: 0x0600F922 RID: 63778 RVA: 0x00350A08 File Offset: 0x0034EC08
			// (set) Token: 0x0600F923 RID: 63779 RVA: 0x00350A10 File Offset: 0x0034EC10
			public double MinTrailingZerosAndWhitespace { readonly get; set; }

			// Token: 0x170029AF RID: 10671
			// (get) Token: 0x0600F924 RID: 63780 RVA: 0x00350A19 File Offset: 0x0034EC19
			// (set) Token: 0x0600F925 RID: 63781 RVA: 0x00350A21 File Offset: 0x0034EC21
			public double MinLeadingZeros_greaterThan_MinLeadingZerosAndWhitespace { readonly get; set; }

			// Token: 0x170029B0 RID: 10672
			// (get) Token: 0x0600F926 RID: 63782 RVA: 0x00350A2A File Offset: 0x0034EC2A
			// (set) Token: 0x0600F927 RID: 63783 RVA: 0x00350A32 File Offset: 0x0034EC32
			public double MinTrailingZeros_greaterThan_MinTrailingZerosAndWhitespace { readonly get; set; }

			// Token: 0x170029B1 RID: 10673
			// (get) Token: 0x0600F928 RID: 63784 RVA: 0x00350A3B File Offset: 0x0034EC3B
			// (set) Token: 0x0600F929 RID: 63785 RVA: 0x00350A43 File Offset: 0x0034EC43
			public double MaxTrailingZeros_lessThan_MinTrailingZerosAndWhitespace { readonly get; set; }

			// Token: 0x170029B2 RID: 10674
			// (get) Token: 0x0600F92A RID: 63786 RVA: 0x00350A4C File Offset: 0x0034EC4C
			// (set) Token: 0x0600F92B RID: 63787 RVA: 0x00350A54 File Offset: 0x0034EC54
			public double MinTrailingZeros_eq_MaxTrailingZeros { readonly get; set; }

			// Token: 0x170029B3 RID: 10675
			// (get) Token: 0x0600F92C RID: 63788 RVA: 0x00350A5D File Offset: 0x0034EC5D
			// (set) Token: 0x0600F92D RID: 63789 RVA: 0x00350A65 File Offset: 0x0034EC65
			public double MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosHasValue { readonly get; set; }

			// Token: 0x170029B4 RID: 10676
			// (get) Token: 0x0600F92E RID: 63790 RVA: 0x00350A6E File Offset: 0x0034EC6E
			// (set) Token: 0x0600F92F RID: 63791 RVA: 0x00350A76 File Offset: 0x0034EC76
			public double MinTrailingZeros_lessThan_minTrailingZerosAndWhitespace_and_maxTrailingZerosNoValue { readonly get; set; }

			// Token: 0x170029B5 RID: 10677
			// (get) Token: 0x0600F930 RID: 63792 RVA: 0x00350A7F File Offset: 0x0034EC7F
			// (set) Token: 0x0600F931 RID: 63793 RVA: 0x00350A87 File Offset: 0x0034EC87
			public double MinTrailingZeros_gte_minTrailingZerosAndWhitespace { readonly get; set; }

			// Token: 0x170029B6 RID: 10678
			// (get) Token: 0x0600F932 RID: 63794 RVA: 0x00350A90 File Offset: 0x0034EC90
			// (set) Token: 0x0600F933 RID: 63795 RVA: 0x00350A98 File Offset: 0x0034EC98
			public double ScaleHasValueAndMaxTrailingZerosIsZero { readonly get; set; }

			// Token: 0x170029B7 RID: 10679
			// (get) Token: 0x0600F934 RID: 63796 RVA: 0x00350AA1 File Offset: 0x0034ECA1
			// (set) Token: 0x0600F935 RID: 63797 RVA: 0x00350AA9 File Offset: 0x0034ECA9
			public double Scale { readonly get; set; }

			// Token: 0x170029B8 RID: 10680
			// (get) Token: 0x0600F936 RID: 63798 RVA: 0x00350AB2 File Offset: 0x0034ECB2
			// (set) Token: 0x0600F937 RID: 63799 RVA: 0x00350ABA File Offset: 0x0034ECBA
			public double HasScale { readonly get; set; }

			// Token: 0x170029B9 RID: 10681
			// (get) Token: 0x0600F938 RID: 63800 RVA: 0x00350AC3 File Offset: 0x0034ECC3
			// (set) Token: 0x0600F939 RID: 63801 RVA: 0x00350ACB File Offset: 0x0034ECCB
			public double HasSeparator { readonly get; set; }
		}

		// Token: 0x02001CB6 RID: 7350
		public struct OutputDtFormatScoreFeatures
		{
			// Token: 0x170029BA RID: 10682
			// (get) Token: 0x0600F93A RID: 63802 RVA: 0x00350AD4 File Offset: 0x0034ECD4
			// (set) Token: 0x0600F93B RID: 63803 RVA: 0x00350ADC File Offset: 0x0034ECDC
			public double TimeBeforeDate { readonly get; set; }

			// Token: 0x170029BB RID: 10683
			// (get) Token: 0x0600F93C RID: 63804 RVA: 0x00350AE5 File Offset: 0x0034ECE5
			// (set) Token: 0x0600F93D RID: 63805 RVA: 0x00350AED File Offset: 0x0034ECED
			public double PeriodWithFullHour { readonly get; set; }

			// Token: 0x170029BC RID: 10684
			// (get) Token: 0x0600F93E RID: 63806 RVA: 0x00350AF6 File Offset: 0x0034ECF6
			// (set) Token: 0x0600F93F RID: 63807 RVA: 0x00350AFE File Offset: 0x0034ECFE
			public double HasOneDecimalPoint { readonly get; set; }

			// Token: 0x170029BD RID: 10685
			// (get) Token: 0x0600F940 RID: 63808 RVA: 0x00350B07 File Offset: 0x0034ED07
			// (set) Token: 0x0600F941 RID: 63809 RVA: 0x00350B0F File Offset: 0x0034ED0F
			public double ConstantLength { readonly get; set; }

			// Token: 0x170029BE RID: 10686
			// (get) Token: 0x0600F942 RID: 63810 RVA: 0x00350B18 File Offset: 0x0034ED18
			// (set) Token: 0x0600F943 RID: 63811 RVA: 0x00350B20 File Offset: 0x0034ED20
			public double DigitConstantLength { readonly get; set; }

			// Token: 0x170029BF RID: 10687
			// (get) Token: 0x0600F944 RID: 63812 RVA: 0x00350B29 File Offset: 0x0034ED29
			// (set) Token: 0x0600F945 RID: 63813 RVA: 0x00350B31 File Offset: 0x0034ED31
			public double SeparatorKindMatches { readonly get; set; }

			// Token: 0x170029C0 RID: 10688
			// (get) Token: 0x0600F946 RID: 63814 RVA: 0x00350B3A File Offset: 0x0034ED3A
			// (set) Token: 0x0600F947 RID: 63815 RVA: 0x00350B42 File Offset: 0x0034ED42
			public double SeparatorKindMisMatches { readonly get; set; }

			// Token: 0x170029C1 RID: 10689
			// (get) Token: 0x0600F948 RID: 63816 RVA: 0x00350B4B File Offset: 0x0034ED4B
			// (set) Token: 0x0600F949 RID: 63817 RVA: 0x00350B53 File Offset: 0x0034ED53
			public double UnlikelySeparatorCount { readonly get; set; }

			// Token: 0x170029C2 RID: 10690
			// (get) Token: 0x0600F94A RID: 63818 RVA: 0x00350B5C File Offset: 0x0034ED5C
			// (set) Token: 0x0600F94B RID: 63819 RVA: 0x00350B64 File Offset: 0x0034ED64
			public double SeparatorCount { readonly get; set; }

			// Token: 0x170029C3 RID: 10691
			// (get) Token: 0x0600F94C RID: 63820 RVA: 0x00350B6D File Offset: 0x0034ED6D
			// (set) Token: 0x0600F94D RID: 63821 RVA: 0x00350B75 File Offset: 0x0034ED75
			public double HasNonDelimitedNumbers { readonly get; set; }

			// Token: 0x170029C4 RID: 10692
			// (get) Token: 0x0600F94E RID: 63822 RVA: 0x00350B7E File Offset: 0x0034ED7E
			// (set) Token: 0x0600F94F RID: 63823 RVA: 0x00350B86 File Offset: 0x0034ED86
			public double IsNumeric { readonly get; set; }

			// Token: 0x170029C5 RID: 10693
			// (get) Token: 0x0600F950 RID: 63824 RVA: 0x00350B8F File Offset: 0x0034ED8F
			// (set) Token: 0x0600F951 RID: 63825 RVA: 0x00350B97 File Offset: 0x0034ED97
			public double MinDateInversions { readonly get; set; }

			// Token: 0x170029C6 RID: 10694
			// (get) Token: 0x0600F952 RID: 63826 RVA: 0x00350BA0 File Offset: 0x0034EDA0
			// (set) Token: 0x0600F953 RID: 63827 RVA: 0x00350BA8 File Offset: 0x0034EDA8
			public double MinTimeInversions { readonly get; set; }

			// Token: 0x170029C7 RID: 10695
			// (get) Token: 0x0600F954 RID: 63828 RVA: 0x00350BB1 File Offset: 0x0034EDB1
			// (set) Token: 0x0600F955 RID: 63829 RVA: 0x00350BB9 File Offset: 0x0034EDB9
			public double IsMatchingCommonDatePartsOrders { readonly get; set; }

			// Token: 0x170029C8 RID: 10696
			// (get) Token: 0x0600F956 RID: 63830 RVA: 0x00350BC2 File Offset: 0x0034EDC2
			// (set) Token: 0x0600F957 RID: 63831 RVA: 0x00350BCA File Offset: 0x0034EDCA
			public double DatePartOrderCount { readonly get; set; }

			// Token: 0x170029C9 RID: 10697
			// (get) Token: 0x0600F958 RID: 63832 RVA: 0x00350BD3 File Offset: 0x0034EDD3
			// (set) Token: 0x0600F959 RID: 63833 RVA: 0x00350BDB File Offset: 0x0034EDDB
			public double IsMatchingCommonTimePartsOrders { readonly get; set; }

			// Token: 0x170029CA RID: 10698
			// (get) Token: 0x0600F95A RID: 63834 RVA: 0x00350BE4 File Offset: 0x0034EDE4
			// (set) Token: 0x0600F95B RID: 63835 RVA: 0x00350BEC File Offset: 0x0034EDEC
			public double TimePartOrderCount { readonly get; set; }

			// Token: 0x170029CB RID: 10699
			// (get) Token: 0x0600F95C RID: 63836 RVA: 0x00350BF5 File Offset: 0x0034EDF5
			// (set) Token: 0x0600F95D RID: 63837 RVA: 0x00350BFD File Offset: 0x0034EDFD
			public double VariableLengthCount { readonly get; set; }

			// Token: 0x170029CC RID: 10700
			// (get) Token: 0x0600F95E RID: 63838 RVA: 0x00350C06 File Offset: 0x0034EE06
			// (set) Token: 0x0600F95F RID: 63839 RVA: 0x00350C0E File Offset: 0x0034EE0E
			public double MatchedPartsCount { readonly get; set; }

			// Token: 0x170029CD RID: 10701
			// (get) Token: 0x0600F960 RID: 63840 RVA: 0x00350C17 File Offset: 0x0034EE17
			// (set) Token: 0x0600F961 RID: 63841 RVA: 0x00350C1F File Offset: 0x0034EE1F
			public double HasDayOfWeekInMonth { readonly get; set; }

			// Token: 0x170029CE RID: 10702
			// (get) Token: 0x0600F962 RID: 63842 RVA: 0x00350C28 File Offset: 0x0034EE28
			// (set) Token: 0x0600F963 RID: 63843 RVA: 0x00350C30 File Offset: 0x0034EE30
			public double TimeAndDateShareSeparator { readonly get; set; }

			// Token: 0x170029CF RID: 10703
			// (get) Token: 0x0600F964 RID: 63844 RVA: 0x00350C39 File Offset: 0x0034EE39
			// (set) Token: 0x0600F965 RID: 63845 RVA: 0x00350C41 File Offset: 0x0034EE41
			public double BetweenTimeDateSeparatorReused { readonly get; set; }
		}

		// Token: 0x02001CB7 RID: 7351
		private class InputDtFeatures : DtFeatures
		{
			// Token: 0x170029D0 RID: 10704
			// (get) Token: 0x0600F966 RID: 63846 RVA: 0x00350C4A File Offset: 0x0034EE4A
			public double NumAbbreviatedParts { get; }

			// Token: 0x0600F967 RID: 63847 RVA: 0x00350C52 File Offset: 0x0034EE52
			public InputDtFeatures(DateTimeFormat inputDtFormat, bool avoidImperialDateTimeFormat)
				: base(inputDtFormat, avoidImperialDateTimeFormat, false)
			{
				this.NumAbbreviatedParts = (double)inputDtFormat.FormatParts.Count(delegate(DateTimeFormatPart fp)
				{
					StringDateTimeFormatPart stringDateTimeFormatPart = fp as StringDateTimeFormatPart;
					return ((stringDateTimeFormatPart != null) ? stringDateTimeFormatPart.AbbreviationOf : null) != null || (fp.IsNumeric && fp.MinimumLength != fp.MaximumLength && !fp.AllowsLeadingZeros());
				});
			}

			// Token: 0x0600F968 RID: 63848 RVA: 0x00350C8E File Offset: 0x0034EE8E
			public override double GetScore()
			{
				return base.GetScore() - this.NumAbbreviatedParts;
			}
		}

		// Token: 0x02001CB9 RID: 7353
		public struct InputDtFormatScoreFeatures
		{
			// Token: 0x170029D1 RID: 10705
			// (get) Token: 0x0600F96C RID: 63852 RVA: 0x00350CE2 File Offset: 0x0034EEE2
			// (set) Token: 0x0600F96D RID: 63853 RVA: 0x00350CEA File Offset: 0x0034EEEA
			public double MaxScore { readonly get; set; }

			// Token: 0x170029D2 RID: 10706
			// (get) Token: 0x0600F96E RID: 63854 RVA: 0x00350CF3 File Offset: 0x0034EEF3
			// (set) Token: 0x0600F96F RID: 63855 RVA: 0x00350CFB File Offset: 0x0034EEFB
			public double MinScore { readonly get; set; }

			// Token: 0x170029D3 RID: 10707
			// (get) Token: 0x0600F970 RID: 63856 RVA: 0x00350D04 File Offset: 0x0034EF04
			// (set) Token: 0x0600F971 RID: 63857 RVA: 0x00350D0C File Offset: 0x0034EF0C
			public double AverageScore { readonly get; set; }

			// Token: 0x170029D4 RID: 10708
			// (get) Token: 0x0600F972 RID: 63858 RVA: 0x00350D15 File Offset: 0x0034EF15
			// (set) Token: 0x0600F973 RID: 63859 RVA: 0x00350D1D File Offset: 0x0034EF1D
			public double NumFormats { readonly get; set; }

			// Token: 0x170029D5 RID: 10709
			// (get) Token: 0x0600F974 RID: 63860 RVA: 0x00350D26 File Offset: 0x0034EF26
			// (set) Token: 0x0600F975 RID: 63861 RVA: 0x00350D2E File Offset: 0x0034EF2E
			public double AnyAmbiguous { readonly get; set; }

			// Token: 0x170029D6 RID: 10710
			// (get) Token: 0x0600F976 RID: 63862 RVA: 0x00350D37 File Offset: 0x0034EF37
			// (set) Token: 0x0600F977 RID: 63863 RVA: 0x00350D3F File Offset: 0x0034EF3F
			public double MatchedDifferentParts { readonly get; set; }
		}

		// Token: 0x02001CBA RID: 7354
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005C65 RID: 23653
			public static Func<char, bool> <0>__IsDigit;

			// Token: 0x04005C66 RID: 23654
			public static Func<char, bool> <1>__IsWhiteSpace;
		}
	}
}

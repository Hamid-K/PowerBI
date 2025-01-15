using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Learning;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Split.Text
{
	// Token: 0x020012FF RID: 4863
	public class SplitProgramLearner : ProgramLearner<SplitProgram, StringRegion, SplitCell[]>
	{
		// Token: 0x0600927F RID: 37503 RVA: 0x001ECDAC File Offset: 0x001EAFAC
		private SplitProgramLearner()
			: base(false, true)
		{
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x06009280 RID: 37504 RVA: 0x001ECDC6 File Offset: 0x001EAFC6
		public static SplitProgramLearner Instance { get; } = new SplitProgramLearner();

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x06009281 RID: 37505 RVA: 0x001ECDCD File Offset: 0x001EAFCD
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar);

		// Token: 0x06009282 RID: 37506 RVA: 0x001ECDD8 File Offset: 0x001EAFD8
		protected override ProgramCollection<SplitProgram, StringRegion, SplitCell[], TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<StringRegion, SplitCell[]>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<StringRegion> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			SplitProgramLearner.CheckUnsupportedConstraints(constraints);
			IEnumerable<StringRegion> inputsForLearning = SplitProgramLearner.GetInputsForLearning(additionalInputs, constraints);
			if (inputsForLearning == null)
			{
				return ProgramCollection<SplitProgram, StringRegion, SplitCell[], TFeatureValue>.Empty;
			}
			WithInputTopSpec withInputTopSpec = new WithInputTopSpec(inputsForLearning.Select((StringRegion e) => State.CreateForLearning(Language.Grammar.InputSymbol, e)));
			Witnesses.Options options;
			LogListener logListener;
			IEnumerable<regionSplit> enumerable = SplitProgramLearner.CreateSynthesisEngine(constraints, inputsForLearning, out options, out logListener).LearnSymbolTopK(Language.Build.Symbol.regionSplit, withInputTopSpec, feature, k, null, ProgramSamplingStrategy.UniformRandom, cancel, null).RealizedPrograms.Select(new Func<ProgramNode, regionSplit>(Language.Build.Node.Cast.regionSplit));
			options.SaveLogToXMLIfEnabled(logListener, null);
			return new ProgramCollection<SplitProgram, StringRegion, SplitCell[], TFeatureValue>(enumerable.Select((regionSplit p) => new SplitProgram(p)), null, null, null);
		}

		// Token: 0x06009283 RID: 37507 RVA: 0x001ECEB0 File Offset: 0x001EB0B0
		public override ProgramSet LearnAll(IEnumerable<Constraint<StringRegion, SplitCell[]>> constraints, IEnumerable<StringRegion> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			SplitProgramLearner.CheckUnsupportedConstraints(constraints);
			IEnumerable<StringRegion> inputsForLearning = SplitProgramLearner.GetInputsForLearning(additionalInputs, constraints);
			if (inputsForLearning == null)
			{
				return ProgramSet.Empty(Language.Build.Symbol.regionSplit);
			}
			WithInputTopSpec withInputTopSpec = new WithInputTopSpec(inputsForLearning.Select((StringRegion e) => State.CreateForLearning(Language.Grammar.InputSymbol, e)));
			Witnesses.Options options;
			LogListener logListener;
			ProgramSet programSet = SplitProgramLearner.CreateSynthesisEngine(constraints, inputsForLearning, out options, out logListener).LearnSymbol(Language.Build.Symbol.regionSplit, withInputTopSpec, cancel);
			options.SaveLogToXMLIfEnabled(logListener, null);
			return programSet;
		}

		// Token: 0x06009284 RID: 37508 RVA: 0x001ECF38 File Offset: 0x001EB138
		public static SplitProgram LearnProgram(IEnumerable<StringRegion> inputs, CancellationToken cancel = default(CancellationToken))
		{
			regionSplit? regionSplit = SplitProgramLearner.Instance.LearnAll(Enumerable.Empty<Constraint<StringRegion, SplitCell[]>>(), inputs, default(CancellationToken)).RealizedPrograms.Select(new Func<ProgramNode, regionSplit>(Language.Build.Node.Cast.regionSplit)).FirstOrNull<regionSplit>();
			if (regionSplit == null)
			{
				return null;
			}
			return new SplitProgram(regionSplit.Value);
		}

		// Token: 0x06009285 RID: 37509 RVA: 0x001ECFA0 File Offset: 0x001EB1A0
		private static IEnumerable<StringRegion> GetSampleInputs(IEnumerable<StringRegion> inputs, int prefixSize, int uniformSize)
		{
			int num = inputs.Count<StringRegion>();
			if (num - prefixSize > uniformSize)
			{
				int nStep = (num - prefixSize) / uniformSize;
				IEnumerable<StringRegion> enumerable = inputs.Take(prefixSize);
				StringRegion[] array = inputs.Skip(prefixSize).Where((StringRegion input, int i) => i % nStep == 0).ToArray<StringRegion>();
				return enumerable.Concat(array);
			}
			return inputs;
		}

		// Token: 0x06009286 RID: 37510 RVA: 0x001ECFF8 File Offset: 0x001EB1F8
		private static IEnumerable<StringRegion> GetInputsForLearning(IEnumerable<StringRegion> inputs, IEnumerable<Constraint<StringRegion, SplitCell[]>> constraints)
		{
			HashSet<string> hashSet = (from c in constraints.OfType<ConstraintOnInput<StringRegion, SplitCell[]>>()
				select c.Input.Value).ConvertToHashSet<string>();
			int num = (hashSet.Any<string>() ? 20 : 1000);
			SuggestionsMode suggestionsMode = constraints.OfType<SuggestionsMode>().FirstOrDefault<SuggestionsMode>();
			if (!(suggestionsMode != null) || !suggestionsMode.IsSuggestionsMode)
			{
				inputs = inputs.Where((StringRegion input) => input != null && !string.IsNullOrWhiteSpace(input.Value)).ToArray<StringRegion>();
				IEnumerable<StringRegion> enumerable = SplitProgramLearner.GetSampleInputs(inputs, 50, num).ToList<StringRegion>();
				hashSet.ExceptWith(enumerable.Select((StringRegion s) => s.Value));
				return hashSet.Select((string s) => new StringRegion(s, Semantics.Tokens)).Concat(enumerable);
			}
			if (!SplitProgramLearner.HandRaiseInputCheck(inputs))
			{
				return null;
			}
			return inputs;
		}

		// Token: 0x06009287 RID: 37511 RVA: 0x001ED104 File Offset: 0x001EB304
		private static bool HandRaiseInputCheck(IEnumerable<StringRegion> inputs)
		{
			IEnumerable<StringRegion> nonNullInputs = inputs.Where((StringRegion r) => r.Value != null);
			if (nonNullInputs.Any((StringRegion r) => r.Value.Length > 500))
			{
				return false;
			}
			return (from r in nonNullInputs
				select r.Value.Trim() into s
				where !string.IsNullOrEmpty(s)
				select s).Distinct<string>().Count<string>() >= 2 && !Semantics.GetDataTypeRegexes().Any((Regex regex) => nonNullInputs.Any((StringRegion r) => Semantics.IsFullMatch(regex, r.Value)));
		}

		// Token: 0x06009288 RID: 37512 RVA: 0x001ED1E8 File Offset: 0x001EB3E8
		private static void CheckUnsupportedConstraints(IEnumerable<Constraint<StringRegion, SplitCell[]>> constraints)
		{
			IEnumerable<Constraint<StringRegion, SplitCell[]>> enumerable = constraints.Where((Constraint<StringRegion, SplitCell[]> c) => !(c is IncludeDelimitersInOutput) && !(c is SimpleDelimiter) && !(c is DelimiterStringsConstraint) && !(c is FillStrategyConstraint) && !(c is NthExampleConstraint) && !(c is FixedWidthConstraint) && !(c is SimpleDelimitersOrFixedWidth) && !(c is SplitInputOutputExample) && !(c is QuotingConfigurationConstraint) && !(c is SuggestionsMode) && !(c is TranslationConstraint));
			if (enumerable.Any<Constraint<StringRegion, SplitCell[]>>())
			{
				throw new ArgumentException("Unsupported constraints:\n" + string.Join<Constraint<StringRegion, SplitCell[]>>("\n", enumerable), "constraints");
			}
		}

		// Token: 0x06009289 RID: 37513 RVA: 0x001ED244 File Offset: 0x001EB444
		private static SynthesisEngine CreateSynthesisEngine(IEnumerable<Constraint<StringRegion, SplitCell[]>> constraintsEnumerable, IEnumerable<StringRegion> inputs, out Witnesses.Options options, out LogListener logListener)
		{
			IReadOnlyCollection<Constraint<StringRegion, SplitCell[]>> readOnlyCollection = (constraintsEnumerable as IReadOnlyCollection<Constraint<StringRegion, SplitCell[]>>) ?? constraintsEnumerable.ToList<Constraint<StringRegion, SplitCell[]>>();
			DomainGuidedCBS.Config config = SplitProgramLearner.GetConfig(inputs, readOnlyCollection);
			options = new Witnesses.Options();
			foreach (IOptionConstraint<Witnesses.Options> optionConstraint in readOnlyCollection.OfType<IOptionConstraint<Witnesses.Options>>())
			{
				optionConstraint.SetOptions(options);
			}
			Witnesses witnesses = new Witnesses(Language.Grammar, options, config);
			foreach (NthExampleConstraint nthExampleConstraint in readOnlyCollection.OfType<NthExampleConstraint>())
			{
				witnesses.AddNthExampleConstraint(nthExampleConstraint.InputString, nthExampleConstraint.SplitIndex, nthExampleConstraint.ExampleValue);
			}
			foreach (SplitInputOutputExample splitInputOutputExample in readOnlyCollection.OfType<SplitInputOutputExample>())
			{
				if (splitInputOutputExample.Input.Value == null)
				{
					throw new Exception("Provided example input is null");
				}
				for (int i = 0; i < splitInputOutputExample.Output.Length; i++)
				{
					witnesses.AddNthExampleConstraint(splitInputOutputExample.Input.Value, i, splitInputOutputExample.Output[i].CellValue.Value);
				}
			}
			logListener = options.GetLogListenerIfEnabled(null);
			Grammar grammar = Language.Grammar;
			SynthesisEngine.Config config2 = new SynthesisEngine.Config();
			SynthesisEngine.Config config3 = config2;
			ISynthesisStrategy[] array = new ISynthesisStrategy[2];
			array[0] = new DomainGuidedCBS(witnesses, config);
			int num = 1;
			DomainLearningLogic domainLearningLogic = witnesses;
			DeductiveSynthesis.Config config4 = new DeductiveSynthesis.Config();
			config4.PrereqTopProgramsThreshold = (int k) => new int?(1);
			array[num] = new DeductiveSynthesis(domainLearningLogic, config4);
			config3.Strategies = array;
			config2.UseThreads = false;
			config2.LogListener = logListener;
			config2.UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck;
			return new SynthesisEngine(grammar, config2, null);
		}

		// Token: 0x0600928A RID: 37514 RVA: 0x001ED438 File Offset: 0x001EB638
		public List<List<string>> LearnTableExtraction(string[] rows, IEnumerable<Constraint<StringRegion, SplitCell[]>> constraints = null, bool includeDelimiters = true, bool learnSimpleDelimiters = false, bool learnFixedWidth = false, bool showSigInputs = false, int numLearningInputs = -1)
		{
			StringRegion[] array = rows.Select((string line) => new StringRegion(line, Semantics.Tokens)).ToArray<StringRegion>();
			SplitSession splitSession = new SplitSession(null, null, null);
			NotifyingCollection<StringRegion> inputs = splitSession.Inputs;
			IEnumerable<StringRegion> enumerable;
			if (numLearningInputs >= 0)
			{
				enumerable = array.Take(numLearningInputs);
			}
			else
			{
				IEnumerable<StringRegion> enumerable2 = array;
				enumerable = enumerable2;
			}
			inputs.Add(enumerable);
			splitSession.Constraints.Add(new IncludeDelimitersInOutput(includeDelimiters));
			if (learnSimpleDelimiters)
			{
				splitSession.Constraints.Add(new SimpleDelimiter());
			}
			if (learnFixedWidth)
			{
				splitSession.Constraints.Add(new FixedWidthConstraint());
			}
			if (constraints != null)
			{
				splitSession.Constraints.Add(constraints);
			}
			SplitProgram program = splitSession.Learn(null, default(CancellationToken), null);
			List<List<string>> list = new List<List<string>> { rows.ToList<string>() };
			if (program == null)
			{
				return list;
			}
			SplitCell[][] array2 = array.Select((StringRegion r) => program.Run(r)).ToArray<SplitCell[]>();
			if (!array2.Any<SplitCell[]>() || array2.First<SplitCell[]>().Length == 0)
			{
				return list;
			}
			int num = array2[0].Length + 1;
			List<List<string>> list2 = (from r in Enumerable.Range(0, num)
				select new List<string>()).ToList<List<string>>();
			if (showSigInputs && list2.Count > 0)
			{
				string[] array3 = splitSession.GetSignificantInputsAsync(null, default(CancellationToken), null).Result.Select((SignificantInput<StringRegion> t) => t.Input.Value).ToArray<string>();
				list2[0].Add("SIGNIFICANT INPUTS:");
				for (int m = 1; m < num; m++)
				{
					list2[m].Add(string.Empty);
				}
				for (int j = 0; j < array3.Length; j++)
				{
					string sigInput = array3[j];
					list2[0].Add(sigInput);
					int num2 = array.Select(delegate(StringRegion r, int ind)
					{
						if (!(r.Value == sigInput))
						{
							return -1;
						}
						return ind;
					}).First((int ind) => ind >= 0);
					for (int k = 1; k < num; k++)
					{
						SplitCell splitCell = array2[num2][k - 1];
						list2[k].Add((splitCell.CellValue == null) ? string.Empty : splitCell.CellValue.Value);
					}
				}
				list2[0].Add("RESULTS:");
				for (int l = 1; l < num; l++)
				{
					list2[l].Add(string.Empty);
				}
			}
			list2[0].AddRange(array.Select((StringRegion r) => r.Value).ToList<string>());
			int i;
			int i2;
			for (i = 1; i < num; i = i2 + 1)
			{
				IEnumerable<string> enumerable3 = array2.Select(delegate(SplitCell[] cells)
				{
					if (!(cells[i - 1].CellValue == null))
					{
						return cells[i - 1].CellValue.Value;
					}
					return string.Empty;
				});
				list2[i].AddRange(enumerable3);
				i2 = i;
			}
			return list2;
		}

		// Token: 0x0600928B RID: 37515 RVA: 0x001ED7C4 File Offset: 0x001EB9C4
		private static HashSet<RegularExpression> GetBasicTokens(IEnumerable<StringRegion> regions)
		{
			StringRegion[] array = regions.Where((StringRegion r) => !string.IsNullOrEmpty(r.Value)).ToArray<StringRegion>();
			int highFrequencyBound = (int)((double)array.Length * Semantics.HighFrequencyRatio);
			List<HashSet<Token>> tokenSets = new List<HashSet<Token>>();
			HashSet<Token> hashSet = new HashSet<Token>();
			foreach (StringRegion stringRegion in array)
			{
				stringRegion.Cache.InitializeStaticTokens(null);
				HashSet<Token> hashSet2 = new HashSet<Token>(stringRegion.Cache.GetAllTokensMatchPositions(stringRegion.Start, stringRegion.End).Keys);
				tokenSets.Add(hashSet2);
				hashSet.AddRange(hashSet2);
			}
			if (!tokenSets.Any<HashSet<Token>>())
			{
				return new HashSet<RegularExpression>();
			}
			return (from t in hashSet
				where tokenSets.Count((HashSet<Token> s) => s.Contains(t)) >= highFrequencyBound
				select RegularExpression.Create(new Token[] { t }, 0)).ConvertToHashSet<RegularExpression>();
		}

		// Token: 0x0600928C RID: 37516 RVA: 0x001ED8D4 File Offset: 0x001EBAD4
		private static HashSet<string> GetBasicStrings(string[] lines, HashSet<Regex> regexSet, bool inSuggestionsMode)
		{
			string[] nonEmptyLines = lines.Where((string l) => !string.IsNullOrEmpty(l)).ToArray<string>();
			int num = (int)((double)nonEmptyLines.Length * Semantics.HighFrequencyRatio);
			Func<string, HashSet<string>> specialCharStrings = (string line) => new HashSet<string>(regexSet.SelectMany((Regex r) => from m in r.NonCachingMatches(line)
				select m.Value));
			HashSet<string> commonMaximalStrings = SplitProgramLearner.GetCommonMaximalStrings((from x in nonEmptyLines.SelectMany((string x) => specialCharStrings(x)).Distinct<string>().ToArray<string>()
					.Distinct<string>()
				orderby x.Length
				select x).ToArray<string>(), nonEmptyLines, num, inSuggestionsMode);
			if (commonMaximalStrings.Any((string s) => nonEmptyLines.All((string line) => line.Trim() == s.Trim())))
			{
				commonMaximalStrings.AddRange(commonMaximalStrings.SelectMany((string s) => s.Select((char c) => c.ToString())).ToArray<string>());
			}
			commonMaximalStrings.AddRange((from s in commonMaximalStrings
				select s.Trim() into s
				where !string.IsNullOrEmpty(s)
				select s).ToArray<string>());
			return commonMaximalStrings;
		}

		// Token: 0x0600928D RID: 37517 RVA: 0x001EDA38 File Offset: 0x001EBC38
		private static HashSet<string> GetCommonMaximalStrings(IEnumerable<string> maximalStrings, string[] lines, int highFrequencyBound, bool inSuggestionsMode)
		{
			HashSet<string> hashSet = new HashSet<string>();
			using (IEnumerator<string> enumerator = maximalStrings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string s = enumerator.Current;
					if (lines.Count((string x) => x.Contains(s)) >= highFrequencyBound)
					{
						hashSet.Add(s);
					}
					if (inSuggestionsMode)
					{
						if (s.Any((char c) => !char.IsLetterOrDigit(c)))
						{
							List<int> list = (from k in Enumerable.Range(0, s.Length)
								where k == 0 || k == s.Length - 1 || !char.IsLetterOrDigit(s[k])
								select k).ToList<int>();
							foreach (int num in list)
							{
								foreach (int num2 in list)
								{
									if (num2 > num + 1)
									{
										string s1 = s.Substring(num, num2 - num + 1);
										if (lines.Count((string x) => x.Contains(s1)) < highFrequencyBound)
										{
											break;
										}
										hashSet.Add(s1);
									}
								}
							}
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x0600928E RID: 37518 RVA: 0x001EDBF8 File Offset: 0x001EBDF8
		private static DomainGuidedCBS.Config GetConfig(IEnumerable<StringRegion> inputs, IEnumerable<Constraint<StringRegion, SplitCell[]>> constraints)
		{
			string[] array = inputs.Select((StringRegion r) => r.Value).ToArray<string>();
			Dictionary<string, IEnumerable<object>> dictionary = new Dictionary<string, IEnumerable<object>>();
			DelimiterStringsConstraint delimiterStringsConstraint = constraints.OfType<DelimiterStringsConstraint>().FirstOrDefault<DelimiterStringsConstraint>();
			bool flag = constraints.OfType<SimpleDelimiter>().Any<SimpleDelimiter>() || constraints.OfType<SimpleDelimitersOrFixedWidth>().Any<SimpleDelimitersOrFixedWidth>();
			constraints.OfType<FixedWidthConstraint>().Any<FixedWidthConstraint>();
			HashSet<string> hashSet = new HashSet<string>();
			new HashSet<string>();
			List<RegularExpression> list = new List<RegularExpression>();
			List<RegularExpression> list2 = new List<RegularExpression>();
			string text = ",;\\t|";
			HashSet<Regex> hashSet2 = new HashSet<Regex>
			{
				new Regex(FormattableString.Invariant(FormattableStringFactory.Create("[^a-zA-Z0-9{0}]+", new object[] { text }))),
				new Regex(FormattableString.Invariant(FormattableStringFactory.Create("[^-+a-zA-Z0-9{0}]+", new object[] { text }))),
				new Regex(FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { text })))
			};
			SuggestionsMode suggestionsMode = constraints.OfType<SuggestionsMode>().FirstOrDefault<SuggestionsMode>();
			bool flag2 = suggestionsMode != null && suggestionsMode.IsSuggestionsMode;
			if (flag2)
			{
				hashSet2.AddRange(new Regex[]
				{
					new Regex(FormattableString.Invariant(FormattableStringFactory.Create("[a-zA-Z]+", Array.Empty<object>()))),
					new Regex(FormattableString.Invariant(FormattableStringFactory.Create("[a-zA-Z ]+", Array.Empty<object>()))),
					new Regex(FormattableString.Invariant(FormattableStringFactory.Create("[^0-9]+", Array.Empty<object>())))
				});
			}
			if (flag)
			{
				hashSet2.Add(new Regex(FormattableString.Invariant(FormattableStringFactory.Create("[^a-zA-Z0-9]", Array.Empty<object>()))));
			}
			hashSet = SplitProgramLearner.GetBasicStrings(array, hashSet2, flag2);
			if (delimiterStringsConstraint != null)
			{
				hashSet.AddRange(delimiterStringsConstraint.DelimiterStrings);
			}
			if (!flag)
			{
				list.AddRange(SplitProgramLearner.GetBasicTokens(inputs));
			}
			list2 = Semantics.SpecialRegexes.Where((KeyValuePair<Regex, Record<bool, Regex, string>> kvp) => kvp.Value.Item1).Select(delegate(KeyValuePair<Regex, Record<bool, Regex, string>> kvp)
			{
				RegexToken[] array2 = new RegexToken[1];
				array2[0] = new RegexToken(kvp.Key, kvp.Value.Item3, 1, -5.5, (string s) => -2.3, false, true, null);
				return RegularExpression.Create(array2, 0);
			}).ToList<RegularExpression>();
			dictionary["s"] = hashSet.Cast<object>().ToArray<object>();
			dictionary["a"] = new object[0];
			dictionary["regex"] = list.Cast<object>().ToArray<object>();
			dictionary["fregex"] = list2.Cast<object>().ToArray<object>();
			dictionary["quotingConf"] = (from c in constraints.OfType<QuotingConfigurationConstraint>()
				select c.Configuration).Cast<object>().ToArray<object>();
			int num = ((constraints.OfType<SimpleDelimiter>().Any<SimpleDelimiter>() || constraints.OfType<SimpleDelimitersOrFixedWidth>().Any<SimpleDelimitersOrFixedWidth>()) ? 1 : 4);
			return new DomainGuidedCBS.Config
			{
				TerminalGenerators = dictionary,
				ValidStartSymbols = new HashSet<Symbol>
				{
					Language.Build.Symbol.d,
					Language.Build.Symbol.constantDelimiterMatches
				},
				IgnoreRuleApplications = new Dictionary<string, HashSet<string>>(),
				NonRankingRules = new HashSet<string>(),
				MaxIterations = num
			};
		}

		// Token: 0x04003C11 RID: 15377
		private const int MaxInputs = 1000;

		// Token: 0x04003C12 RID: 15378
		private const int MaxInputsWithExamples = 20;

		// Token: 0x04003C13 RID: 15379
		private const int NumPrefixSampleInputs = 50;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Compound.Split.Constraints;
using Microsoft.ProgramSynthesis.Compound.Split.Learning;
using Microsoft.ProgramSynthesis.Compound.Split.Semantics;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Compound.Split
{
	// Token: 0x0200090C RID: 2316
	public class Learner : ProgramLearner<Program, StringRegion, ITable<StringRegion>>
	{
		// Token: 0x060031DE RID: 12766 RVA: 0x00092C7A File Offset: 0x00090E7A
		private Learner()
			: base(false, true)
		{
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x00092C94 File Offset: 0x00090E94
		public static IReadOnlyDictionary<string, Token> Tokens
		{
			get
			{
				return Token.NonDisjunctiveTokens;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x00092C9B File Offset: 0x00090E9B
		public static Learner Instance { get; } = new Learner();

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x00092CA2 File Offset: 0x00090EA2
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar);

		// Token: 0x060031E2 RID: 12770 RVA: 0x00092CAC File Offset: 0x00090EAC
		protected override ProgramCollection<Program, StringRegion, ITable<StringRegion>, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<StringRegion, ITable<StringRegion>>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<StringRegion> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			Learner.<>c__DisplayClass9_0<TFeatureValue> CS$<>8__locals1 = new Learner.<>c__DisplayClass9_0<TFeatureValue>();
			PrefixSpec prefixSpec;
			Learner.GetLearningOptionsAndSpecFromConstraints(constraints, out CS$<>8__locals1.options, out prefixSpec);
			bool flag = false;
			CS$<>8__locals1.linesInputs = ((additionalInputs == null) ? null : Semantics.SplitIntoLines(additionalInputs, CS$<>8__locals1.options.ReadInputLineCount, CS$<>8__locals1.options.LineLengthLimit, out flag));
			CS$<>8__locals1.options.LinesTrimmed = flag;
			Learner.<>c__DisplayClass9_0<TFeatureValue> CS$<>8__locals2 = CS$<>8__locals1;
			IReadOnlyList<InputLineTelemetry> readOnlyList;
			if (!CS$<>8__locals1.options.EnableTelemetry || CS$<>8__locals1.linesInputs == null)
			{
				readOnlyList = null;
			}
			else
			{
				readOnlyList = (from line in CS$<>8__locals1.linesInputs.SelectMany((IReadOnlyList<StringRegion> x) => x).Take(CS$<>8__locals1.options.TelemetryLineCount)
					select new InputLineTelemetry(line, CS$<>8__locals1.options.TelemetryTrackSymbols)).ToList<InputLineTelemetry>();
			}
			CS$<>8__locals2.telemetryLines = readOnlyList;
			CS$<>8__locals1.witnesses = new Witnesses(Language.Grammar, CS$<>8__locals1.options);
			IReadOnlyList<Program> readOnlyList2;
			if (prefixSpec == null)
			{
				readOnlyList2 = (from res in CS$<>8__locals1.witnesses.LearnAuto(CS$<>8__locals1.linesInputs, feature, k, cancel, guid)
					select new Program(res.Program, res.ColumnNames, res.NewLines, CS$<>8__locals1.telemetryLines, res.SkipEmptyAndCommentLinesCount, res.HasEmptyLines)).ToList<Program>();
			}
			else
			{
				IEnumerable<splitLines> enumerable = this.LearnSplitFileFromSpec(CS$<>8__locals1.options, prefixSpec, cancel).Set.TopK(feature, k, null, null).Select(new Func<ProgramNode, splitLines>(Language.Build.Node.Cast.splitLines));
				Func<splitLines, topSplit> func;
				if ((func = Learner.<>O.<0>__BuildTopSplit) == null)
				{
					func = (Learner.<>O.<0>__BuildTopSplit = new Func<splitLines, topSplit>(Learner.BuildTopSplit));
				}
				readOnlyList2 = (from p in enumerable.Select(func)
					select new Program(p, null, base.<LearnTopKUnchecked>g__GetNewLines|2(p), CS$<>8__locals1.telemetryLines, 0, false)).ToList<Program>();
			}
			return new ProgramCollection<Program, StringRegion, ITable<StringRegion>, TFeatureValue>(readOnlyList2, null, null, null);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x00092E40 File Offset: 0x00091040
		private static topSplit BuildTopSplit(splitLines splitFileProg)
		{
			GrammarBuilders build = Language.Build;
			return build.Node.Rule.LetFileRecordSplit(build.Node.Rule.LetSplitFile(build.Node.Rule.SplitFile(build.Node.Variable.file), build.Node.Rule.MergeRecordLines(splitFileProg)), build.Node.UnnamedConversion.splitRecordsSelect_splitRecords(build.Node.Rule.NoSplit(build.Node.Variable.records, build.Node.Rule.hasHeader(false))));
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x00092EE8 File Offset: 0x000910E8
		private static void GetLearningOptionsAndSpecFromConstraints(IEnumerable<Constraint<StringRegion, ITable<StringRegion>>> constraints, out Options options, out PrefixSpec examplesSpec)
		{
			options = new Options();
			List<SequenceExample> list = new List<SequenceExample>();
			AllowRowFiltering allowRowFiltering = null;
			foreach (Constraint<StringRegion, ITable<StringRegion>> constraint in constraints)
			{
				IOptionConstraint<Options> optionConstraint = constraint as IOptionConstraint<Options>;
				if (optionConstraint == null)
				{
					AllowRowFiltering allowRowFiltering2 = constraint as AllowRowFiltering;
					if (allowRowFiltering2 == null)
					{
						if (!(constraint is TestingConstraintOnInput))
						{
							throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Unsupported constraint {0}", new object[] { constraint.GetType() })), "constraint");
						}
					}
					else
					{
						allowRowFiltering = allowRowFiltering2;
					}
				}
				else
				{
					IgnoreSplitRecords ignoreSplitRecords = optionConstraint as IgnoreSplitRecords;
					if (ignoreSplitRecords != null)
					{
						list.AddRange(ignoreSplitRecords.Examples);
					}
					optionConstraint.SetOptions(options);
				}
			}
			if (allowRowFiltering != null)
			{
				options.IgnoreSelectData = false;
				options.IgnoreFilterHeader = false;
				options.IgnoreQuote = true;
				options.MaxRowPrefixRegexTokens = allowRowFiltering.MaxRowPrefixRegexTokens;
			}
			PrefixSpec prefixSpec;
			if (list.Count != 0)
			{
				prefixSpec = new PrefixSpec(list.ToDictionary((SequenceExample c) => State.CreateForLearning(Language.Build.Symbol.allLines, c.Input), (SequenceExample p) => p.Output));
			}
			else
			{
				prefixSpec = null;
			}
			examplesSpec = prefixSpec;
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x00093034 File Offset: 0x00091234
		public override ProgramSet LearnAll(IEnumerable<Constraint<StringRegion, ITable<StringRegion>>> constraints, IEnumerable<StringRegion> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			Options options;
			PrefixSpec prefixSpec;
			Learner.GetLearningOptionsAndSpecFromConstraints(constraints, out options, out prefixSpec);
			if (!options.IgnoreSplitRecords)
			{
				throw new NotSupportedException(FormattableString.Invariant(FormattableStringFactory.Create("LearnAll supported only with the {0} constraint.", new object[] { "IgnoreSplitRecords" })));
			}
			ProgramSetBuilder<splitLines> programSetBuilder = this.LearnSplitFileFromSpec(options, prefixSpec, cancel);
			if (ProgramSetBuilder.IsNullOrEmpty<splitLines>(programSetBuilder))
			{
				return ProgramSet.Empty(Language.Grammar.StartSymbol);
			}
			ProgramSetBuilder<_LetB0> programSetBuilder2 = ProgramSetBuilder.List<_LetB0>(new _LetB0[] { Language.Build.Node.Rule.SplitFile(Language.Build.Node.Variable.file) });
			ProgramSetBuilder<_LetB1> programSetBuilder3 = Language.Build.Set.Join.MergeRecordLines(programSetBuilder);
			ProgramSetBuilder<splitFile> programSetBuilder4 = Language.Build.Set.Join.LetSplitFile(programSetBuilder2, programSetBuilder3);
			ProgramSetBuilder<splitRecords> programSetBuilder5 = ProgramSetBuilder.List<splitRecords>(new splitRecords[] { Language.Build.Node.Rule.NoSplit(Language.Build.Node.Variable.records, Language.Build.Node.Rule.hasHeader(false)) });
			return Language.Build.Set.Join.LetFileRecordSplit(programSetBuilder4, Language.Build.Set.UnnamedConversion.splitRecordsSelect_splitRecords(programSetBuilder5)).Set;
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x00093188 File Offset: 0x00091388
		private ProgramSetBuilder<splitLines> LearnSplitFileFromSpec(Options options, PrefixSpec spec, CancellationToken cancel = default(CancellationToken))
		{
			Witnesses witnesses = new Witnesses(Language.Grammar, options);
			LogListener logListenerIfEnabled = options.GetLogListenerIfEnabled(this.ScoreFeature);
			SynthesisEngine synthesisEngine = new SynthesisEngine(Language.Grammar, new SynthesisEngine.Config
			{
				Strategies = new ISynthesisStrategy[]
				{
					new DeductiveSynthesis(witnesses, null)
				},
				UseThreads = false,
				LogListener = logListenerIfEnabled,
				UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck
			}, null);
			ProgramSetBuilder<splitLines> programSetBuilder = Language.Build.Set.Cast.splitLines(synthesisEngine.LearnSymbol(Language.Build.Symbol.splitLines, spec, cancel));
			options.SaveLogToXMLIfEnabled(logListenerIfEnabled, null);
			return programSetBuilder;
		}

		// Token: 0x0200090D RID: 2317
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040018DB RID: 6363
			public static Func<splitLines, topSplit> <0>__BuildTopSplit;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Learning;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Geometry;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet
{
	// Token: 0x02000DCF RID: 3535
	public class Learner : ProgramLearner<Program, ISpreadsheetPair, SpreadsheetArea>
	{
		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x060059AE RID: 22958 RVA: 0x0011CE2B File Offset: 0x0011B02B
		public static Learner Instance { get; } = new Learner();

		// Token: 0x060059AF RID: 22959 RVA: 0x0011CE32 File Offset: 0x0011B032
		private Learner()
			: base(true, true)
		{
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x0011CE50 File Offset: 0x0011B050
		private Witnesses.Options InterpretConstraints(IEnumerable<Constraint<ISpreadsheetPair, SpreadsheetArea>> constraints)
		{
			Witnesses.Options options = new Witnesses.Options();
			foreach (Constraint<ISpreadsheetPair, SpreadsheetArea> constraint in constraints)
			{
				IOptionConstraint<Witnesses.Options> optionConstraint = constraint as IOptionConstraint<Witnesses.Options>;
				if (optionConstraint != null)
				{
					optionConstraint.SetOptions(options);
				}
			}
			return options;
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x0011CEA8 File Offset: 0x0011B0A8
		internal static IEnumerable<StyleFilter> GenerateStyleFilters(ISpreadsheetPair spreadsheet)
		{
			IReadOnlyList<ICellStyleInfo> readOnlyList = spreadsheet.WithFormatting.EnumerateCells(Axis.Horizontal, true, false, true).Collect((ISpreadsheetCell cell) => cell.StyleInfo).Distinct(FontInfoEqualityComparer.Instance)
				.ToList<ICellStyleInfo>();
			return (from fontName in readOnlyList.Collect((ICellStyleInfo style) => style.FontName).Distinct<string>()
				select new StyleFilter(false, false, false, false, false, fontName, null, null)).Concat(from fontSize in readOnlyList.Collect((ICellStyleInfo style) => style.FontSize).Distinct<int>()
				select new StyleFilter(false, false, false, false, false, null, new int?(fontSize), null)).Concat(from obj in (from style in readOnlyList
					where style.FontName != null && style.FontSize != null
					select new
					{
						FontName = style.FontName,
						FontSize = style.FontSize.Value,
						Bold = style.Bold,
						Italic = style.Italic,
						Underline = style.Underline
					}).Distinct()
				select new StyleFilter(obj.Bold, obj.Italic, obj.Underline, false, false, obj.FontName, new int?(obj.FontSize), null));
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x0011D010 File Offset: 0x0011B210
		private Record<SynthesisEngine, Spec, IReadOnlyList<State>> BuildEngineAndSpec(IEnumerable<Constraint<ISpreadsheetPair, SpreadsheetArea>> constraints, IEnumerable<ISpreadsheetPair> additionalInputs)
		{
			IReadOnlyList<Constraint<ISpreadsheetPair, SpreadsheetArea>> readOnlyList = constraints.ToList<Constraint<ISpreadsheetPair, SpreadsheetArea>>();
			Witnesses.Options options = this.InterpretConstraints(readOnlyList);
			LogListener logListenerIfEnabled = options.GetLogListenerIfEnabled(null);
			Grammar grammar = Language.Grammar;
			SynthesisEngine.Config config = new SynthesisEngine.Config();
			SynthesisEngine.Config config2 = config;
			ISynthesisStrategy[] array = new ISynthesisStrategy[1];
			int num = 0;
			IEnumerable<Symbol> enumerable = new Symbol[]
			{
				Language.Build.Symbol.index,
				Language.Build.Symbol.withoutFormatting
			};
			Func<ComponentBasedSynthesis.LearnerState, Spec, bool> func = null;
			Dictionary<TerminalRule, Func<object, IEnumerable<object>>> dictionary = new Dictionary<TerminalRule, Func<object, IEnumerable<object>>>();
			TerminalRule terminalRule = Language.Build.Symbol.rangeName.TerminalRule;
			dictionary[terminalRule] = delegate(object obj)
			{
				IReadOnlyList<DefinedRange> definedRanges = ((ISpreadsheetPair)obj).WithFormatting.DefinedRanges;
				if (definedRanges == null)
				{
					return null;
				}
				return from dr in definedRanges
					where (!dr.Hidden && !dr.IsSpecialName) || dr.InternalName
					select dr.Name;
			};
			TerminalRule terminalRule2 = Language.Build.Symbol.styleFilter.TerminalRule;
			dictionary[terminalRule2] = (object obj) => Learner.GenerateStyleFilters((ISpreadsheetPair)obj);
			array[num] = new ComponentBasedSynthesis(enumerable, func, dictionary, null);
			config2.Strategies = array;
			config.LogListener = logListenerIfEnabled;
			config.UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck;
			SynthesisEngine synthesisEngine = new SynthesisEngine(grammar, config, null);
			bool flag = readOnlyList.OfType<Example<ISpreadsheetPair, SpreadsheetArea>>().Any<Example<ISpreadsheetPair, SpreadsheetArea>>();
			IReadOnlyList<State> readOnlyList2;
			if (additionalInputs == null)
			{
				readOnlyList2 = null;
			}
			else
			{
				readOnlyList2 = additionalInputs.Select((ISpreadsheetPair input) => State.CreateForLearning(Language.Grammar.InputSymbol, input)).ToList<State>();
			}
			IReadOnlyList<State> readOnlyList3 = readOnlyList2;
			Spec spec;
			if (flag)
			{
				spec = new ExampleSpec(readOnlyList.OfType<Example<ISpreadsheetPair, SpreadsheetArea>>().ToDictionary((Example<ISpreadsheetPair, SpreadsheetArea> c) => State.CreateForLearning(Language.Grammar.InputSymbol, c.Input), (Example<ISpreadsheetPair, SpreadsheetArea> c) => c.Output));
			}
			else
			{
				spec = new WithInputTopSpec(readOnlyList3);
				readOnlyList3 = null;
			}
			return Record.Create<SynthesisEngine, Spec, IReadOnlyList<State>>(synthesisEngine, spec, readOnlyList3);
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x0011D1CC File Offset: 0x0011B3CC
		protected override ProgramCollection<Program, ISpreadsheetPair, SpreadsheetArea, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<ISpreadsheetPair, SpreadsheetArea>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<ISpreadsheetPair> additionalInputs = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			SynthesisEngine synthesisEngine;
			Spec spec;
			IReadOnlyList<State> readOnlyList;
			this.BuildEngineAndSpec(constraints, additionalInputs).Deconstruct(out synthesisEngine, out spec, out readOnlyList);
			SynthesisEngine synthesisEngine2 = synthesisEngine;
			Spec spec2 = spec;
			IReadOnlyList<State> readOnlyList2 = readOnlyList;
			LearningTask learningTask = new LearningTask(Language.Grammar.StartSymbol, spec2, k, feature, null)
			{
				AdditionalInputs = readOnlyList2
			};
			ProgramSet programSet = synthesisEngine2.Learn(learningTask, cancel);
			PrunedProgramSet prunedProgramSet = programSet as PrunedProgramSet;
			return ProgramCollection<Program, ISpreadsheetPair, SpreadsheetArea, TFeatureValue>.From(prunedProgramSet, delegate(ProgramNode node)
			{
				Feature<TFeatureValue> feature2 = feature;
				PrunedProgramSet prunedProgramSet2 = prunedProgramSet;
				TFeatureValue featureValue = node.GetFeatureValue<TFeatureValue>(feature2, (prunedProgramSet2 != null) ? prunedProgramSet2.FeatureCalculationContext.WithProgramNode(node) : null);
				double num2;
				if (featureValue is double)
				{
					double num = featureValue as double;
					num2 = num;
				}
				else
				{
					num2 = 0.0;
				}
				return new Program(node, num2);
			}, feature);
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x0011D258 File Offset: 0x0011B458
		public override ProgramSet LearnAll(IEnumerable<Constraint<ISpreadsheetPair, SpreadsheetArea>> constraints, IEnumerable<ISpreadsheetPair> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			SynthesisEngine synthesisEngine;
			Spec spec;
			IReadOnlyList<State> readOnlyList;
			this.BuildEngineAndSpec(constraints, additionalInputs).Deconstruct(out synthesisEngine, out spec, out readOnlyList);
			SynthesisEngine synthesisEngine2 = synthesisEngine;
			Spec spec2 = spec;
			IReadOnlyList<State> readOnlyList2 = readOnlyList;
			LearningTask learningTask = new LearningTask(Language.Grammar.StartSymbol, spec2)
			{
				AdditionalInputs = readOnlyList2
			};
			return synthesisEngine2.Learn(learningTask, cancel);
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x0011D2A0 File Offset: 0x0011B4A0
		public IReadOnlyList<Program> LearnAllTables(ISpreadsheetPair spreadsheet, bool includeAll = false, bool sortByScore = false, CancellationToken cancel = default(CancellationToken))
		{
			List<Record<SpreadsheetArea, Program>> list = new List<Record<SpreadsheetArea, Program>>();
			List<SpreadsheetArea> list2 = new List<SpreadsheetArea>();
			foreach (DefinedRange definedRange in spreadsheet.WithFormatting.DefinedRanges)
			{
				if (!definedRange.Hidden && !definedRange.IsSpecialName)
				{
					SpreadsheetArea spreadsheetArea = Semantics.Trim(new SpreadsheetArea(spreadsheet.WithFormatting, definedRange.Span, null, null, null));
					list2.Add(spreadsheetArea);
				}
			}
			foreach (Program program2 in base.LearnTopK(new Constraint<ISpreadsheetPair, SpreadsheetArea>[0], 1000, new ISpreadsheetPair[] { spreadsheet }, cancel))
			{
				if (includeAll || program2.Score > 0.0)
				{
					SpreadsheetArea table = program2.Run(spreadsheet);
					if (!(table == null) && !list2.Any((SpreadsheetArea covered) => covered.Span.Equals(table.Span)) && (includeAll || !list2.Any((SpreadsheetArea covered) => covered.Span.Overlaps(table.Span))))
					{
						list.Add(Record.Create<SpreadsheetArea, Program>(table, program2));
						list2.Add(table);
					}
				}
			}
			IEnumerable<Record<SpreadsheetArea, Program>> enumerable = list;
			if (!sortByScore)
			{
				enumerable = from r in enumerable
					orderby r.Item1.Span.Top, r.Item1.Span.Left
					select r;
			}
			return enumerable.Select2((SpreadsheetArea _, Program program) => program).ToList<Program>();
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x060059B6 RID: 22966 RVA: 0x0011D488 File Offset: 0x0011B688
		public override Feature<double> ScoreFeature { get; } = new RankingScore(Language.Grammar, true);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build;
using Microsoft.ProgramSynthesis.Extraction.Web.Learning;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FD3 RID: 4051
	public class TextTableProgramLearner : ProgramLearner<TextTableProgram, WebRegion, IEnumerable<IEnumerable<string>>>
	{
		// Token: 0x06006FC0 RID: 28608 RVA: 0x0016CED8 File Offset: 0x0016B0D8
		private TextTableProgramLearner()
			: base(true, true)
		{
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06006FC1 RID: 28609 RVA: 0x0016CEE2 File Offset: 0x0016B0E2
		public static TextTableProgramLearner Instance { get; } = new TextTableProgramLearner();

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06006FC2 RID: 28610 RVA: 0x0016B102 File Offset: 0x00169302
		public override Feature<double> ScoreFeature
		{
			get
			{
				return ExtractionLearner.Instance.ScoreFeature;
			}
		}

		// Token: 0x06006FC3 RID: 28611 RVA: 0x0016CEEC File Offset: 0x0016B0EC
		private IEnumerable<WebRegion> GetInputs(IEnumerable<Constraint<WebRegion, IEnumerable<IEnumerable<string>>>> constraints)
		{
			TextTableConstraint textTableConstraint = constraints.OfType<TextTableConstraint>().FirstOrDefault<TextTableConstraint>();
			WebRegion webRegion = ((textTableConstraint != null) ? textTableConstraint.Input : null);
			return new WebRegion[] { webRegion };
		}

		// Token: 0x06006FC4 RID: 28612 RVA: 0x0016CF1C File Offset: 0x0016B11C
		private Spec GetSpecification(IEnumerable<Constraint<WebRegion, IEnumerable<IEnumerable<string>>>> constraints, IEnumerable<WebRegion> inputs)
		{
			TextTableConstraint[] array = constraints.OfType<TextTableConstraint>().ToArray<TextTableConstraint>();
			if (array.Length == 0)
			{
				return null;
			}
			if (array.Length == 1 && array[0].ColumnExamples == null)
			{
				IEnumerable<IDomNode> allChildrenAndSelf = array[0].Input.GetAllChildrenAndSelf();
				return new WithInputTopSpec(new State[] { State.CreateForLearning(Language.Grammar.InputSymbol, allChildrenAndSelf) });
			}
			List<Tuple<State, TextTableSpec>> list = new List<Tuple<State, TextTableSpec>>();
			IEnumerable<int> enumerable = (from c in array
				where ((c != null) ? c.ColumnExamples : null) != null
				select c.ColumnExamples.Count).Distinct<int>();
			if (enumerable.Skip(1).Any<int>() || !enumerable.Any<int>())
			{
				return null;
			}
			int num = enumerable.First<int>();
			foreach (TextTableConstraint textTableConstraint in array)
			{
				IEnumerable<IDomNode> allChildrenAndSelf2 = textTableConstraint.Input.GetAllChildrenAndSelf();
				State state = State.CreateForLearning(Language.Grammar.InputSymbol, allChildrenAndSelf2);
				TextTableSpec textTableSpec = null;
				if (textTableConstraint.ColumnExamples != null)
				{
					List<TextSubsequenceSpec> list2 = new List<TextSubsequenceSpec>();
					for (int j = 0; j < num; j++)
					{
						Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
						Dictionary<State, int[]> dictionary2 = new Dictionary<State, int[]>();
						dictionary2[state] = textTableConstraint.NodeIndexes[j].ToArray<int>();
						Dictionary<State, bool[]> dictionary3 = new Dictionary<State, bool[]> { 
						{
							state,
							textTableConstraint.SoftConstraints[j].ToArray<bool>()
						} };
						dictionary[state] = textTableConstraint.ColumnExamples[j].ToArray<string>();
						list2.Add(new TextSubsequenceSpec(dictionary, dictionary2, dictionary3));
					}
					textTableSpec = new TextTableSpec(list2);
				}
				list.Add(Tuple.Create<State, TextTableSpec>(state, textTableSpec));
			}
			return new MultiPageTextTableSpec(list);
		}

		// Token: 0x06006FC5 RID: 28613 RVA: 0x0016D0F8 File Offset: 0x0016B2F8
		protected override ProgramCollection<TextTableProgram, WebRegion, IEnumerable<IEnumerable<string>>, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<WebRegion, IEnumerable<IEnumerable<string>>>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<WebRegion> additionalReferences = null, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			IEnumerable<WebRegion> inputs = this.GetInputs(constraints);
			Spec specification = this.GetSpecification(constraints, inputs);
			ProgramCollection<TextTableProgram, WebRegion, IEnumerable<IEnumerable<string>>, TFeatureValue> empty = ProgramCollection<TextTableProgram, WebRegion, IEnumerable<IEnumerable<string>>, TFeatureValue>.Empty;
			if (specification == null)
			{
				return empty;
			}
			Witnesses.Options options;
			LogListener logListener;
			SynthesisEngine synthesisEngine = this.CreateSynthesisEngine(inputs, constraints, out options, out logListener);
			if (synthesisEngine == null)
			{
				return empty;
			}
			ProgramCollection<TextTableProgram, WebRegion, IEnumerable<IEnumerable<string>>, TFeatureValue> programCollection;
			using (new DumbDownTokenScoreForWebLearning())
			{
				try
				{
					ProgramSet programSet = synthesisEngine.LearnSymbolTopK(TextTableProgram.ProgramSymbol, specification, feature, k, numRandomProgramsToInclude, samplingStrategy, cancel, null);
					if (programSet == null || programSet.IsEmpty)
					{
						programCollection = ProgramCollection<TextTableProgram, WebRegion, IEnumerable<IEnumerable<string>>, TFeatureValue>.Empty;
					}
					else
					{
						PrunedProgramSet prunedProgramSet = programSet as PrunedProgramSet;
						options.SaveLogToXMLIfEnabled(logListener, null);
						programCollection = ProgramCollection<TextTableProgram, WebRegion, IEnumerable<IEnumerable<string>>, TFeatureValue>.From(prunedProgramSet, (ProgramNode x) => new TextTableProgram(Language.Build.Node.Cast.resultTable(x)), feature);
					}
				}
				finally
				{
					CachedObjectEquality<State>.Clear();
				}
			}
			return programCollection;
		}

		// Token: 0x06006FC6 RID: 28614 RVA: 0x0016D1D8 File Offset: 0x0016B3D8
		public override ProgramSet LearnAll(IEnumerable<Constraint<WebRegion, IEnumerable<IEnumerable<string>>>> constraints, IEnumerable<WebRegion> additionalReferences = null, CancellationToken cancel = default(CancellationToken))
		{
			IEnumerable<WebRegion> inputs = this.GetInputs(constraints);
			Spec specification = this.GetSpecification(constraints, inputs);
			if (specification == null)
			{
				return ProgramSet.Empty(TextTableProgram.ProgramSymbol);
			}
			Witnesses.Options options;
			LogListener logListener;
			SynthesisEngine synthesisEngine = this.CreateSynthesisEngine(inputs, constraints, out options, out logListener);
			if (synthesisEngine == null)
			{
				return ProgramSet.Empty(TextTableProgram.ProgramSymbol);
			}
			ProgramSet programSet2;
			using (new DumbDownTokenScoreForWebLearning())
			{
				try
				{
					ProgramSet programSet = synthesisEngine.LearnSymbol(TextTableProgram.ProgramSymbol, specification, cancel);
					options.SaveLogToXMLIfEnabled(logListener, null);
					programSet2 = programSet;
				}
				finally
				{
					CachedObjectEquality<State>.Clear();
				}
			}
			return programSet2;
		}

		// Token: 0x06006FC7 RID: 28615 RVA: 0x0016D278 File Offset: 0x0016B478
		internal SynthesisEngine CreateSynthesisEngine(IEnumerable<WebRegion> inputs, IEnumerable<Constraint<WebRegion, IEnumerable<IEnumerable<string>>>> constraints, out Witnesses.Options options, out LogListener logListener)
		{
			IEnumerable<IOptionConstraint<Witnesses.Options>> enumerable = constraints.OfType<IOptionConstraint<Witnesses.Options>>().ToList<IOptionConstraint<Witnesses.Options>>();
			options = new Witnesses.Options();
			Witnesses.Options options2 = options;
			TextTableConstraint textTableConstraint = constraints.OfType<TextTableConstraint>().FirstOrDefault<TextTableConstraint>();
			options2.LearnPredictive = ((textTableConstraint != null) ? textTableConstraint.ColumnExamples : null) == null;
			foreach (IOptionConstraint<Witnesses.Options> optionConstraint in enumerable)
			{
				optionConstraint.SetOptions(options);
			}
			logListener = options.GetLogListenerIfEnabled(null);
			DomainGuidedCBS.Config config = TextTableProgramLearner.GetConfig(inputs, constraints, options.LearnPredictive);
			Witnesses witnesses = new Witnesses(Language.Grammar, this.ScoreFeature, options, config);
			List<ISynthesisStrategy> list = new List<ISynthesisStrategy>();
			if (config == null)
			{
				return null;
			}
			list.Add(new DomainGuidedCBS(witnesses, config));
			list.Add(new DeductiveSynthesis(witnesses, new DeductiveSynthesis.Config()));
			return new SynthesisEngine(Language.Grammar, new SynthesisEngine.Config
			{
				Strategies = list.ToArray(),
				UseThreads = false,
				LogListener = logListener,
				UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck
			}, null);
		}

		// Token: 0x06006FC8 RID: 28616 RVA: 0x0016D38C File Offset: 0x0016B58C
		private static DomainGuidedCBS.Config GetConfig(IEnumerable<WebRegion> regions, IEnumerable<Constraint<WebRegion, IEnumerable<IEnumerable<string>>>> constraints, bool learnPredictive)
		{
			HashSet<State> hashSet = new HashSet<State>();
			HashSet<string> hashSet2 = null;
			HashSet<string> hashSet3 = null;
			HashSet<string> hashSet4 = null;
			HashSet<string> hashSet5 = null;
			int num = 2;
			foreach (WebRegion webRegion in regions)
			{
				IDomNode domNode = webRegion.GetAllChildrenAndSelf().FirstOrDefault((IDomNode n) => n.NodeName.ToLower() == "body");
				if (domNode == null)
				{
					return null;
				}
				IDomNode[] array = new WebRegion(domNode).GetAllChildrenAndSelf().ToArray<IDomNode>();
				IDomNode[] array2 = array.Where((IDomNode n) => n.ChildrenCount >= 10).ToArray<IDomNode>();
				if (hashSet2 == null)
				{
					hashSet2 = (from n in array
						select n.NodeName into s
						where !string.IsNullOrEmpty(s)
						select s).ConvertToHashSet<string>();
					hashSet3 = (from n in array2
						select n.Id into s
						where !string.IsNullOrEmpty(s)
						select s).ConvertToHashSet<string>();
					hashSet4 = (from s in array.SelectMany((IDomNode n) => n.Classes)
						where !string.IsNullOrEmpty(s)
						select s).ConvertToHashSet<string>();
					hashSet5 = (from n in array
						select n.GetAttribute("itemprop") into s
						where !string.IsNullOrEmpty(s)
						select s).ConvertToHashSet<string>();
				}
				else
				{
					hashSet2.IntersectWith(array.Select((IDomNode n) => n.NodeName));
					hashSet3.IntersectWith(array2.Select((IDomNode n) => n.Id));
					hashSet4.IntersectWith(array.SelectMany((IDomNode n) => n.Classes));
					hashSet5.IntersectWith(array.Select((IDomNode n) => n.GetAttribute("itemprop")));
				}
				num = Math.Max(num, (from n in array
					select n.ChildrenCount into k
					where k <= TextTableProgramLearner._nthChildIndexUpperBound
					select k).Max());
				State state = State.CreateForLearning(Language.Grammar.InputSymbol, array);
				hashSet.Add(state);
			}
			Dictionary<string, IEnumerable<object>> dictionary = new Dictionary<string, IEnumerable<object>>();
			GrammarBuilders.GrammarSymbols symbol = Language.Build.Symbol;
			dictionary[symbol.nodeName.Name] = ((hashSet2 != null) ? hashSet2.Cast<object>().ToArray<object>() : null);
			dictionary[symbol.idName.Name] = ((hashSet3 != null) ? hashSet3.Cast<object>().ToArray<object>() : null);
			dictionary[symbol.className.Name] = ((hashSet4 != null) ? hashSet4.Cast<object>().ToArray<object>() : null);
			dictionary[symbol.propName.Name] = ((hashSet5 != null) ? hashSet5.Cast<object>().ToArray<object>() : null);
			dictionary[symbol.idx1.Name] = Enumerable.Range(1, num).Cast<object>().ToList<object>();
			dictionary[symbol.idx2.Name] = new List<object> { 0 };
			return new DomainGuidedCBS.Config
			{
				TerminalGenerators = dictionary,
				ValidStartSymbols = new HashSet<Symbol> { Language.Build.Symbol.nodeCollection },
				IgnoreRuleApplications = TextTableProgramLearner.IgnoredRuleApplications(learnPredictive),
				NonRankingRules = TextTableProgramLearner.NonRankingRules(),
				MaxIterations = (learnPredictive ? 6 : 5)
			};
		}

		// Token: 0x06006FC9 RID: 28617 RVA: 0x0016D820 File Offset: 0x0016BA20
		private static HashSet<string> NonRankingRules()
		{
			return new HashSet<string> { "NthChildFilter", "NthLastChildFilter" };
		}

		// Token: 0x06006FCA RID: 28618 RVA: 0x0016D840 File Offset: 0x0016BA40
		private static Dictionary<string, HashSet<string>> IgnoredRuleApplications(bool learnPredictive)
		{
			HashSet<string> hashSet = new HashSet<string> { "NthChildFilter", "NthLastChildFilter", "ClassFilter", "IDFilter", "NodeNameFilter", "ItemPropFilter" };
			HashSet<string> hashSet2 = new HashSet<string> { "DescendantsOf", "RightSiblingOf", "NthLastChildFilter", "AsCollection", "ItemPropFilter" };
			Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>();
			dictionary["DescendantsOf"] = hashSet2;
			dictionary["RightSiblingOf"] = hashSet2;
			dictionary["NthChildFilter"] = (learnPredictive ? hashSet2.Concat(new string[] { "NthChildFilter", "NthLastChildFilter" }).ConvertToHashSet<string>() : hashSet.Concat(hashSet2).ConvertToHashSet<string>());
			dictionary["NthLastChildFilter"] = hashSet.Concat(hashSet2).ConvertToHashSet<string>();
			dictionary["ClassFilter"] = new HashSet<string>(hashSet.Except(new string[] { "ClassFilter" }));
			dictionary["IDFilter"] = hashSet;
			dictionary["NodeNameFilter"] = hashSet;
			dictionary["ItemPropFilter"] = hashSet.Concat(hashSet2).Except(new string[] { "AsCollection" }).ConvertToHashSet<string>();
			return dictionary;
		}

		// Token: 0x0400309F RID: 12447
		private static int _nthChildIndexUpperBound = 15;
	}
}

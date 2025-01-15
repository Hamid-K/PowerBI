using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;
using Microsoft.ProgramSynthesis.Extraction.Web.Build;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Transformation.Text;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Learning
{
	// Token: 0x020010DC RID: 4316
	public class Witnesses : DomainGuidedCBSLearningLogic
	{
		// Token: 0x06008191 RID: 33169 RVA: 0x001B1AFC File Offset: 0x001AFCFC
		private Witnesses(Grammar grammar, DomainGuidedCBS.Config config)
			: base(grammar, config)
		{
			this._options = new Witnesses.Options();
			this._build = GrammarBuilders.Instance(grammar);
		}

		// Token: 0x06008192 RID: 33170 RVA: 0x001B1B5C File Offset: 0x001AFD5C
		public Witnesses(Grammar grammar, Feature<double> score, Witnesses.Options options = null, DomainGuidedCBS.Config config = null)
			: base(grammar, config)
		{
			this._options = options ?? new Witnesses.Options();
			this._build = GrammarBuilders.Instance(grammar);
			this._rules = this._build.Node.Rule;
			this._selectionProgramResultsCache = new Dictionary<ProgramNode, Dictionary<State, IDomNode[]>>();
		}

		// Token: 0x06008193 RID: 33171 RVA: 0x001B1BE4 File Offset: 0x001AFDE4
		private BlackBoxRule GetRuleByName(string name)
		{
			return base.Grammar.Rules.OfType<BlackBoxRule>().Single((BlackBoxRule r) => r.Name == name);
		}

		// Token: 0x06008194 RID: 33172 RVA: 0x001B1C1F File Offset: 0x001AFE1F
		[WitnessFunction("LetRegion", 0)]
		internal ExampleSpec WitnessBeginNode(LetRule rule, ExampleSpec spec)
		{
			return this.WitnessBeginNodeForRegion(rule, spec);
		}

		// Token: 0x06008195 RID: 33173 RVA: 0x001B1C2C File Offset: 0x001AFE2C
		[WitnessFunction("LetContains", 1, DependsOnParameters = new int[] { 0 })]
		private static DisjunctiveExamplesSpec WitnessLetBody(LetRule rule, BooleanExampleSpec spec, ExampleSpec values)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in spec.ProvidedInputs)
			{
				State state2 = state.Bind(rule.Variable, values.Examples[state]);
				IEnumerable<object> enumerable;
				if (!dictionary.TryGetValue(state2, out enumerable))
				{
					dictionary[state2] = spec.DisjunctiveExamples[state];
				}
				else
				{
					List<object> list = enumerable.Intersect(spec.DisjunctiveExamples[state], ValueEquality.Comparer).ToList<object>();
					if (!list.Any<object>())
					{
						return null;
					}
					dictionary[state2] = list;
				}
			}
			return new BooleanSoftSpec(dictionary.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.First<object>()));
		}

		// Token: 0x06008196 RID: 33174 RVA: 0x001B1D34 File Offset: 0x001AFF34
		[WitnessFunction("MapToWebRegion", 1)]
		internal SubsequenceSpec WitnessSelectioninNodeSequenceToWebRegion(GrammarRule rule, SubsequenceSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in spec.ProvidedInputs)
			{
				List<WebRegion> list = spec.PositiveExamples[state].OfType<WebRegion>().ToList<WebRegion>();
				if (list.Any((WebRegion r) => r.IsPair))
				{
					return null;
				}
				dictionary[state] = list.Select((WebRegion example) => example.BeginNode).ToList<IDomNode>();
			}
			return new SubsequenceSpec(dictionary);
		}

		// Token: 0x06008197 RID: 33175 RVA: 0x001B1E00 File Offset: 0x001B0000
		[WitnessFunction("FindEndNode", 1)]
		internal SubsequenceSpec WitnessSelectioninRegionSequenceToWebRegion(GrammarRule rule, SubsequenceSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in spec.ProvidedInputs)
			{
				List<WebRegion> list = spec.PositiveExamples[state].OfType<WebRegion>().ToList<WebRegion>();
				dictionary[state] = list.Select((WebRegion example) => example.BeginNode).ToList<IDomNode>();
			}
			return new SubsequenceSpec(dictionary);
		}

		// Token: 0x06008198 RID: 33176 RVA: 0x001B1E9C File Offset: 0x001B009C
		[WitnessFunction("NodeRegionToWebRegion", 0)]
		[WitnessFunction("NodeRegionToWebRegionInSequence", 0)]
		internal ExampleSpec WitnessBeginNodeForRegion(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in spec.ProvidedInputs)
			{
				dictionary[state] = ((WebRegion)spec.Examples[state]).BeginNode;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x06008199 RID: 33177 RVA: 0x001B1F0C File Offset: 0x001B010C
		[WitnessFunction("NodeRegionToWebRegion", 1)]
		[WitnessFunction("NodeRegionToWebRegionInSequence", 1)]
		internal ExampleSpec WitnessEndNodeForRegion(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in spec.ProvidedInputs)
			{
				dictionary[state] = ((WebRegion)spec.Examples[state]).EndNode;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0600819A RID: 33178 RVA: 0x001B1F7C File Offset: 0x001B017C
		[WitnessFunction("Not", 0)]
		internal ExampleSpec WitnessNot(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			Dictionary<State, bool> dictionary = new Dictionary<State, bool>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				dictionary[state] = !(bool)outerSpec.Examples[state];
			}
			if (!dictionary.ContainsValue(true))
			{
				return null;
			}
			return new BooleanHardNegativeSpec(dictionary);
		}

		// Token: 0x0600819B RID: 33179 RVA: 0x001B1FF4 File Offset: 0x001B01F4
		[WitnessFunction("NodeNames", "names")]
		internal ExampleSpec WitnessNodeNames(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			Symbol symbol = rule.Body[1];
			if (outerSpec is BooleanHardNegativeSpec || this._options.LearnCrossTemplates)
			{
				return null;
			}
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			HashSet<string> hashSet = new HashSet<string>();
			HashSet<string> hashSet2 = new HashSet<string>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				IDomNode domNode = state[symbol] as IDomNode;
				if (domNode == null)
				{
					return null;
				}
				if (outerSpec.Selection[state])
				{
					hashSet.Add(domNode.NodeName);
				}
				else
				{
					hashSet2.Add(domNode.NodeName);
				}
			}
			if (hashSet.Count <= 1 || hashSet.Count > 3)
			{
				return null;
			}
			string[] array = hashSet.ToArray<string>();
			foreach (State state2 in outerSpec.ProvidedInputs)
			{
				dictionary[state2] = array;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0600819C RID: 33180 RVA: 0x001B2128 File Offset: 0x001B0328
		[WitnessFunction("ContainsLeafNodes", "names")]
		internal ExampleSpec WitnessContainLeafNodes(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			Symbol symbol = rule.Body[1];
			if (outerSpec is BooleanHardNegativeSpec || this._options.LearnCrossTemplates)
			{
				return null;
			}
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			List<string> list = null;
			foreach (State state in outerSpec.ProvidedInputs)
			{
				DomNode domNode = state[symbol] as DomNode;
				if (domNode == null)
				{
					return null;
				}
				if (outerSpec.Selection[state])
				{
					list = ((list != null) ? list.Intersect(domNode.LeafNodes).ToList<string>() : null) ?? new List<string>(domNode.LeafNodes);
				}
			}
			if (list == null || list.Count == 0)
			{
				return null;
			}
			foreach (State state2 in outerSpec.ProvidedInputs)
			{
				IDomNode domNode2 = state2[symbol] as IDomNode;
				if (domNode2 == null)
				{
					return null;
				}
				if (!outerSpec.Selection[state2] && list.Except(domNode2.LeafNodes).ToList<string>().Count == 0)
				{
					return null;
				}
			}
			string[] array = list.ToArray();
			foreach (State state3 in outerSpec.ProvidedInputs)
			{
				dictionary[state3] = array;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0600819D RID: 33181 RVA: 0x001B22D8 File Offset: 0x001B04D8
		[WitnessFunction("NodeName", "name")]
		internal DisjunctiveExamplesSpec WitnessNodeName(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			if (!this._options.LearnCrossTemplates)
			{
				return WitnessUtils.WitnessGenericProperty<IDomNode, string>(rule, 1, outerSpec, (IDomNode node) => node.NodeName);
			}
			return null;
		}

		// Token: 0x0600819E RID: 33182 RVA: 0x001B2310 File Offset: 0x001B0510
		[WitnessFunction("ChildrenCount", "count")]
		internal DisjunctiveExamplesSpec WitnessChildrenCount(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			return WitnessUtils.WitnessGenericProperty<IDomNode, int>(rule, 1, outerSpec, (IDomNode node) => node.ChildrenCount);
		}

		// Token: 0x0600819F RID: 33183 RVA: 0x001B233C File Offset: 0x001B053C
		[WitnessFunction("NthChild", "idx1")]
		[WitnessFunction("NthLastChild", "idx2")]
		internal DisjunctiveExamplesSpec WitnessNthChild(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			if (rule.Name == "NthChild")
			{
				return WitnessUtils.WitnessGenericProperty<IDomNode, int>(rule, 1, outerSpec, (IDomNode node) => node.Index);
			}
			return WitnessUtils.WitnessGenericProperty<IDomNode, int>(rule, 1, outerSpec, (IDomNode node) => node.IndexFromLast);
		}

		// Token: 0x060081A0 RID: 33184 RVA: 0x001B23AA File Offset: 0x001B05AA
		[WitnessFunction("Class", "name")]
		internal DisjunctiveExamplesSpec WitnessClassName(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			return WitnessUtils.WitnessGenericProperty<IDomNode, string>(rule, 1, outerSpec, (IDomNode node) => node.Classes);
		}

		// Token: 0x060081A1 RID: 33185 RVA: 0x001B23D3 File Offset: 0x001B05D3
		[WitnessFunction("TitleIs", 0)]
		internal DisjunctiveExamplesSpec WitnessTitle(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			return WitnessUtils.WitnessGenericProperty<IDomNode, string>(rule, 1, outerSpec, delegate(IDomNode node)
			{
				if (node != null && node.Title != null)
				{
					return new string[] { node.Title };
				}
				return new string[0];
			});
		}

		// Token: 0x060081A2 RID: 33186 RVA: 0x001B23FC File Offset: 0x001B05FC
		[WitnessFunction("HasAttribute", "name")]
		internal DisjunctiveExamplesSpec WitnessAttribute(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			return WitnessUtils.WitnessGenericProperty<IDomNode, string>(rule, 2, outerSpec, delegate(IDomNode node)
			{
				List<string> list = new List<string>();
				if (node == null)
				{
					return new string[0];
				}
				foreach (string text in node.GetAttributes())
				{
					if (!text.Equals("class", StringComparison.Ordinal) && !text.Equals("style", StringComparison.Ordinal) && !text.Equals("id", StringComparison.Ordinal) && !text.Equals("title", StringComparison.Ordinal) && !text.Equals("href", StringComparison.Ordinal))
					{
						list.Add(text);
					}
				}
				return list.ToArray();
			});
		}

		// Token: 0x060081A3 RID: 33187 RVA: 0x001B2425 File Offset: 0x001B0625
		[WitnessFunction("HasStyle", "name")]
		internal DisjunctiveExamplesSpec WitnessStyle(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			return WitnessUtils.WitnessGenericProperty<IDomNode, string>(rule, 2, outerSpec, delegate(IDomNode node)
			{
				if (node == null)
				{
					return new string[0];
				}
				return node.GetStyles().ToArray<string>();
			});
		}

		// Token: 0x060081A4 RID: 33188 RVA: 0x001B2450 File Offset: 0x001B0650
		[WitnessFunction("HasAttribute", "value", DependsOnSymbols = new string[] { "name" })]
		internal DisjunctiveExamplesSpec WitnessAttributeValue(BlackBoxRule rule, BooleanExampleSpec spec, ExampleSpec nameSpec)
		{
			string[] array = nameSpec.Examples.Values.Cast<string>().Distinct(StringComparer.OrdinalIgnoreCase).ToArray<string>();
			if (array.Length != 1)
			{
				return null;
			}
			string attribute = array[0];
			return WitnessUtils.WitnessGenericProperty<IDomNode, string>(rule, 2, spec, (IDomNode node) => node.GetAttribute(attribute));
		}

		// Token: 0x060081A5 RID: 33189 RVA: 0x001B24A8 File Offset: 0x001B06A8
		[WitnessFunction("HasStyle", "value", DependsOnSymbols = new string[] { "name" })]
		internal DisjunctiveExamplesSpec WitnessStyleValue(BlackBoxRule rule, BooleanExampleSpec spec, ExampleSpec nameSpec)
		{
			string[] array = nameSpec.Examples.Values.Cast<string>().Distinct(StringComparer.OrdinalIgnoreCase).ToArray<string>();
			if (array.Length != 1)
			{
				return null;
			}
			string style = array[0];
			return WitnessUtils.WitnessGenericProperty<IDomNode, string>(rule, 2, spec, (IDomNode node) => node.GetStyle(style));
		}

		// Token: 0x060081A6 RID: 33190 RVA: 0x001B2500 File Offset: 0x001B0700
		[WitnessFunction("ID_substring", "name")]
		internal DisjunctiveExamplesSpec WitnessIdName(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			Symbol symbol = rule.Body[1];
			if (outerSpec is BooleanHardNegativeSpec)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				IDomNode domNode = state[symbol] as IDomNode;
				if (domNode == null)
				{
					return null;
				}
				if (outerSpec.Selection[state])
				{
					list.Add(domNode.Id);
				}
			}
			if (!list.Any<string>())
			{
				return null;
			}
			List<string> list2 = StringUtils.LongestCommonSubstrings(list, 5, true, 45).ToList<string>();
			if (list2.Count == 0)
			{
				return null;
			}
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state2 in outerSpec.ProvidedInputs)
			{
				dictionary[state2] = list2;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060081A7 RID: 33191 RVA: 0x001B2618 File Offset: 0x001B0818
		[WitnessFunction("ChildrenOf", 0)]
		[WitnessFunction("LeafChildrenOf1", 0)]
		[WitnessFunction("LeafChildrenOf2", 0)]
		[WitnessFunction("LeafChildrenOf3", 0)]
		[WitnessFunction("LeafChildrenOf4", 0)]
		internal SubsequenceSpec WitnessChildrenOf(BlackBoxRule rule, SubsequenceSpec outerSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				List<IDomNode> list = outerSpec.PositiveExamples[state].Cast<IDomNode>().Distinct<IDomNode>().ToList<IDomNode>();
				if (list.Any((IDomNode x) => x.Parent == null))
				{
					return null;
				}
				if (list.Any((IDomNode x) => x.NodeName.Equals("BODY") || x.NodeName.Equals("HTML")))
				{
					return null;
				}
				List<IDomNode> list2 = list.Select((IDomNode t) => t.Parent).Distinct<IDomNode>().ToList<IDomNode>();
				dictionary[state] = list2;
			}
			return new SubsequenceSpec(dictionary);
		}

		// Token: 0x060081A8 RID: 33192 RVA: 0x001B2724 File Offset: 0x001B0924
		[WitnessFunction("YoungerSiblingsOf", 0)]
		internal DisjunctiveExamplesSpec WitnessYoungerSiblingsOf(BlackBoxRule rule, SubsequenceSpec outerSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				IEnumerable<IDomNode> enumerable = outerSpec.PositiveExamples[state].Cast<IDomNode>().Distinct<IDomNode>().ToList<IDomNode>();
				if (enumerable.Any((IDomNode x) => x.NodeName.Equals("BODY") || x.NodeName.Equals("HTML")))
				{
					return null;
				}
				if (enumerable.Select((IDomNode n) => n.Parent).Distinct<IDomNode>().Count<IDomNode>() > 1)
				{
					return null;
				}
				int num = enumerable.Min((IDomNode n) => n.Index);
				if (num == 1)
				{
					return null;
				}
				List<IDomNode> list = enumerable.First<IDomNode>().Parent.GetChildren().Take(num - 1).ToList<IDomNode>();
				dictionary[state] = list;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060081A9 RID: 33193 RVA: 0x001B285C File Offset: 0x001B0A5C
		[RuleLearner("KthNodeInSelection")]
		public Optional<ProgramSet> LearnKthNodeInSelection(SynthesisEngine engine, GrammarRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel)
		{
			ExampleSpec spec = task.Spec;
			SubsequenceSpec subsequenceSpec = new SubsequenceSpec(spec.Examples.ToDictionary((KeyValuePair<State, object> kvp) => kvp.Key, (KeyValuePair<State, object> kvp) => Seq.Of<object>(new object[] { kvp.Value })));
			IEnumerable<selection> enumerable = engine.LearnSymbol(this._build.Symbol.selection, subsequenceSpec, cancel).RealizedPrograms.Select(new Func<ProgramNode, selection>(this._build.Node.Cast.selection)).ToArray<selection>();
			IReadOnlyList<State> providedInputs = subsequenceSpec.ProvidedInputs;
			int maxIndex = 5;
			selection[] array = enumerable.OrderByDescending((selection p) => Utils.GetProgramSize<selection>(p)).SkipWhile((selection p) => Witnesses.GetIndexInSelection(p, spec, maxIndex) < 0).ToArray<selection>();
			if (array.Length == 0)
			{
				return Optional<ProgramSet>.Nothing;
			}
			int bestProgramSize = Utils.GetProgramSize<selection>(array[0]);
			beginNode[] array2 = (from program in array.TakeWhile((selection p) => Utils.GetProgramSize<selection>(p) == bestProgramSize)
				select new
				{
					index = Witnesses.GetIndexInSelection(program, spec, maxIndex),
					program = program
				} into x
				where x.index >= 0
				select this._rules.KthNodeInSelection(x.program, this._rules.k(x.index))).ToArray<beginNode>();
			return ProgramSetBuilder.List<beginNode>(this._build.Symbol.beginNode, array2).Set.Some<ProgramSet>();
		}

		// Token: 0x060081AA RID: 33194 RVA: 0x001B29FC File Offset: 0x001B0BFC
		private List<List<string>> GetTableTextExamples(State input, TextTableSpec spec)
		{
			return spec.ColumnSpecs.Select((TextSubsequenceSpec s) => s.PositiveExamples[input].Cast<string>().ToList<string>()).ToList<List<string>>();
		}

		// Token: 0x060081AB RID: 33195 RVA: 0x001B2A34 File Offset: 0x001B0C34
		private bool SatisfiesRow(IDomNode[] rowNodes, TableNodesMatch match)
		{
			return rowNodes != null && !(match == null) && NodeTextMatching.IsOrderedNonHierarchicalNodeSequence(rowNodes) && match.RowNodes.All((KeyValuePair<int, IDomNode> kvp) => kvp.Key < rowNodes.Length && rowNodes[kvp.Key] == kvp.Value);
		}

		// Token: 0x060081AC RID: 33196 RVA: 0x001B2A88 File Offset: 0x001B0C88
		private bool SatisfiesColumn(IDomNode[] colNodes, Dictionary<int, IDomNode> columnExamples, Dictionary<int, HashSet<IDomNode>> rowInputsMap = null, IDomNode[] rowNodes = null)
		{
			foreach (KeyValuePair<int, IDomNode> keyValuePair in columnExamples)
			{
				int key = keyValuePair.Key;
				IDomNode value = keyValuePair.Value;
				IDomNode domNode;
				if (rowInputsMap == null)
				{
					domNode = ((key < colNodes.Length) ? colNodes[key] : null);
				}
				else
				{
					HashSet<IDomNode> rowDescendantNodes;
					rowInputsMap.TryGetValue(key, out rowDescendantNodes);
					if (rowDescendantNodes == null && key < rowNodes.Length)
					{
						rowDescendantNodes = rowNodes[key].GetDescendants(true).ConvertToHashSet<IDomNode>();
						rowInputsMap[key] = rowDescendantNodes;
					}
					domNode = ((rowDescendantNodes == null) ? null : colNodes.FirstOrDefault((IDomNode n) => rowDescendantNodes.Contains(n)));
				}
				if (domNode != value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060081AD RID: 33197 RVA: 0x001B2B74 File Offset: 0x001B0D74
		private bool RowSelectorIsSuperfluous(IDomNode[] colNodes, Dictionary<int, IDomNode> columnExamples, Dictionary<int, HashSet<IDomNode>> rowInputsMap, IDomNode[] rowNodes)
		{
			if (columnExamples.Any((KeyValuePair<int, IDomNode> e) => e.Key >= colNodes.Length || colNodes[e.Key] != e.Value))
			{
				return false;
			}
			if (colNodes.Length <= rowNodes.Length)
			{
				return true;
			}
			int num = 0;
			for (int i = 0; i < rowNodes.Length; i++)
			{
				IDomNode domNode = rowNodes[i];
				HashSet<IDomNode> rowDescendantNodes;
				rowInputsMap.TryGetValue(num, out rowDescendantNodes);
				if (rowDescendantNodes == null)
				{
					rowDescendantNodes = domNode.GetDescendants(true).ConvertToHashSet<IDomNode>();
					rowInputsMap[num] = rowDescendantNodes;
				}
				if (((rowDescendantNodes == null) ? null : colNodes.FirstOrDefault((IDomNode n) => rowDescendantNodes.Contains(n))) == null)
				{
					return true;
				}
				num++;
			}
			return false;
		}

		// Token: 0x060081AE RID: 33198 RVA: 0x001B2C3C File Offset: 0x001B0E3C
		private bool[] GeneralizedColumns(State inpState, TextTableSpec spec, List<List<string>> textTableExamples)
		{
			bool[] array = textTableExamples.Select((List<string> x) => false).ToArray<bool>();
			if (this._options.PreviousTextTableExamples == null || this._options.PreviouslyLearntColumnSelectors == null || this._options.PreviousTextTableExamples.Count != this._options.PreviouslyLearntColumnSelectors.Length)
			{
				return array;
			}
			for (int i = 0; i < textTableExamples.Count; i++)
			{
				List<string> list = textTableExamples[i];
				if (i >= this._options.PreviousTextTableExamples.Count)
				{
					break;
				}
				IReadOnlyList<string> readOnlyList = this._options.PreviousTextTableExamples[i];
				if (((list != null) & (readOnlyList != null)) && list.Count >= readOnlyList.Count && list.Take(readOnlyList.Count).SequenceEqual(readOnlyList))
				{
					array[i] = true;
				}
			}
			return array;
		}

		// Token: 0x060081AF RID: 33199 RVA: 0x001B2D20 File Offset: 0x001B0F20
		private Witnesses.PreviousProgramsMatch GetPreviousProgramMatches(State inpState, TextTableSpec spec, List<List<string>> textTableExamples)
		{
			if (this._options.PreviousTextTableExamples == null || this._options.PreviouslyLearntColumnSelectors == null || this._options.PreviousTextTableExamples.Count != this._options.PreviouslyLearntColumnSelectors.Length)
			{
				return null;
			}
			Witnesses.PreviousProgramsMatch previousProgramsMatch = new Witnesses.PreviousProgramsMatch();
			previousProgramsMatch.ColumnProgramsNodes = spec.ColumnSpecs.Select((TextSubsequenceSpec x) => new Dictionary<resultSequence, IDomNode[]>()).ToArray<Dictionary<resultSequence, IDomNode[]>>();
			Dictionary<resultSequence, IDomNode[]> dictionary = new Dictionary<resultSequence, IDomNode[]>();
			for (int i = 0; i < this._options.PreviousTextTableExamples.Count; i++)
			{
				IReadOnlyList<string> readOnlyList = this._options.PreviousTextTableExamples[i];
				if (readOnlyList != null)
				{
					bool flag = false;
					for (int j = 0; j < textTableExamples.Count; j++)
					{
						List<string> list = textTableExamples[j];
						if (list != null && list.Take(readOnlyList.Count).SequenceEqual(readOnlyList))
						{
							flag = true;
							resultSequence resultSequence = this._options.PreviouslyLearntColumnSelectors[i];
							IDomNode[] array;
							dictionary.TryGetValue(resultSequence, out array);
							if (array == null)
							{
								array = this.ExecuteSequenceProgram(resultSequence, inpState);
								dictionary[resultSequence] = array;
							}
							previousProgramsMatch.ColumnProgramsNodes[j][resultSequence] = array;
						}
					}
					if (!flag)
					{
						return null;
					}
				}
			}
			previousProgramsMatch.RowProgram = this._options.PreviouslyLearntRowSelector;
			previousProgramsMatch.RowNodes = ((this._options.PreviouslyLearntRowSelector == null) ? null : this.ExecuteSequenceProgram(this._options.PreviouslyLearntRowSelector.Value, inpState));
			return previousProgramsMatch;
		}

		// Token: 0x060081B0 RID: 33200 RVA: 0x001B2EBB File Offset: 0x001B10BB
		public static bool HasNodeName(IDomNode n, string s)
		{
			return string.Equals(n.NodeName, s, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060081B1 RID: 33201 RVA: 0x001B2ECC File Offset: 0x001B10CC
		private resultSequence BuildCssSelectionProgram(string cssSelector)
		{
			if (cssSelector == string.Empty)
			{
				return this._rules.EmptySequence();
			}
			selection selection = this._rules.CSSSelection(this._rules.cssSelector(cssSelector), this._build.Node.Variable.allNodes);
			mapNodeInSequence mapNodeInSequence = this._rules.NodeToWebRegionInSequence(this._build.Node.Variable.node);
			ProgramNode programNode = base.Grammar.Rules.OfType<LambdaRule>().First((LambdaRule r) => r.Body[0].Name == "mapNodeInSequence").BuildASTNode(mapNodeInSequence.Node);
			GrammarRule grammarRule = base.Grammar.Rule("MapToWebRegion");
			subNodeSequence subNodeSequence = this._build.Node.Cast.subNodeSequence(grammarRule.BuildASTNode(programNode, selection.Node));
			return this._build.Node.UnnamedConversion.resultSequence_subNodeSequence(subNodeSequence);
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x001B2FD0 File Offset: 0x001B11D0
		private resultTable BuildTableProgram(string rowSelector, List<string> colSelectors, TableKind kind, string tableTitle, int numRows)
		{
			resultSequence resultSequence = this.BuildCssSelectionProgram(rowSelector);
			columnSelectors columnSelectors = colSelectors.Select(new Func<string, resultSequence>(this.BuildCssSelectionProgram)).AggregateSeedFunc(new Func<resultSequence, columnSelectors>(this._rules.SingleColumn), new Func<columnSelectors, resultSequence, columnSelectors>(this._rules.ColumnSequence));
			resultTable resultTable = this._rules.ExtractRowBasedTable(columnSelectors, resultSequence);
			resultTable.Node.Metadata = new ProgramMetadata(tableTitle, kind, numRows, null);
			return resultTable;
		}

		// Token: 0x060081B3 RID: 33203 RVA: 0x001B3048 File Offset: 0x001B1248
		private string InferSpecificSelector(IDomNode n, IEnumerable<IDomNode> nodeSet)
		{
			if (!string.IsNullOrEmpty(n.Id) && Witnesses.PermittedIdFormat.Match(n.Id).Success)
			{
				Match match = Witnesses.MinIdNumericSubstring.Match(n.Id);
				if (match.Success && match.Index > 0)
				{
					string nonNumericMatch = n.Id.Substring(0, match.Index);
					if (!nodeSet.Any((IDomNode x) => x.Id != null && x != n && x.Id.StartsWith(nonNumericMatch)))
					{
						return FormattableString.Invariant(FormattableStringFactory.Create("{0}[id^='{1}']", new object[]
						{
							n.NodeName,
							DomNode.EscapeSpecialCharactersCss(nonNumericMatch)
						}));
					}
				}
				if (!nodeSet.Any((IDomNode x) => x.Id == n.Id && x != n))
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("{0}[id='{1}']", new object[]
					{
						n.NodeName,
						DomNode.EscapeSpecialCharactersCss(n.Id)
					}));
				}
			}
			string text = n.NodeName;
			if (n.Classes.Any<string>())
			{
				string text2 = text;
				string text3 = ".";
				string text4 = ".";
				IEnumerable<string> classes = n.Classes;
				Func<string, string> func;
				if ((func = Witnesses.<>O.<0>__EscapeSpecialCharactersCss) == null)
				{
					func = (Witnesses.<>O.<0>__EscapeSpecialCharactersCss = new Func<string, string>(DomNode.EscapeSpecialCharactersCss));
				}
				text = text2 + text3 + string.Join(text4, classes.Select(func));
			}
			HashSet<string> nClasses = n.Classes.ConvertToHashSet<string>();
			List<IDomNode> list = nodeSet.Where((IDomNode x) => x.NodeName == n.NodeName && nClasses.Except(x.Classes).IsEmpty<string>()).ToList<IDomNode>();
			if (list.Count == 1)
			{
				return text;
			}
			if (list.Any((IDomNode x) => x.Index != n.Index))
			{
				text += string.Format(":nth-child({0})", n.Index);
				list = list.Where((IDomNode x) => x.Index == n.Index).ToList<IDomNode>();
			}
			if (list.Count == 1)
			{
				return text;
			}
			if (n.Parent == null)
			{
				return null;
			}
			List<IDomNode> list2 = (from x in list
				select x.Parent into x
				where x != null
				select x).ToList<IDomNode>();
			string text5 = this.InferSpecificSelector(n.Parent, list2);
			if (text5 == null)
			{
				return null;
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} > {1}", new object[] { text5, text }));
		}

		// Token: 0x060081B4 RID: 33204 RVA: 0x001B331C File Offset: 0x001B151C
		private ProgramSet LearnHtmlTablePrograms(State inpState, List<List<string>> textTableExamples, out List<IDomNode[][]> tableNodeMatches)
		{
			tableNodeMatches = new List<IDomNode[][]>();
			List<List<string>> list = null;
			string[] array = null;
			if (textTableExamples != null)
			{
				if (textTableExamples.All((List<string> c) => c.Count < 2))
				{
					return null;
				}
				list = textTableExamples.Select(delegate(List<string> c)
				{
					Func<string, string> func2;
					if ((func2 = Witnesses.<>O.<1>__NormalizeText) == null)
					{
						func2 = (Witnesses.<>O.<1>__NormalizeText = new Func<string, string>(HtmlDoc.NormalizeText));
					}
					return c.Select(func2).ToList<string>();
				}).ToList<List<string>>();
				array = (from v in list.SelectMany((List<string> c) => c)
					where !string.IsNullOrWhiteSpace(v)
					select v).ToArray<string>();
			}
			IEnumerable<IDomNode> enumerable = (IEnumerable<IDomNode>)inpState.Bindings.SingleOrDefault((KeyValuePair<Symbol, object> kvp) => kvp.Key == base.Grammar.InputSymbol).Value;
			List<Tuple<string, List<string>, string, int>> list2 = new List<Tuple<string, List<string>, string, int>>();
			foreach (IDomNode domNode in enumerable)
			{
				if (Witnesses.HasNodeName(domNode, "table"))
				{
					string[] array2 = (from n in domNode.GetDescendants(true)
						where Witnesses.HasNodeName(n, "TD") || Witnesses.HasNodeName(n, "TH")
						select n.NormalizedInnerText).ToArray<string>();
					HashSet<string> tableTextSet = array2.ConvertToHashSet(this._options.TextComparer);
					IEnumerable<string> tableTextSet2 = tableTextSet;
					Func<string, bool> func;
					if ((func = Witnesses.<>O.<2>__IsNullOrWhiteSpace) == null)
					{
						func = (Witnesses.<>O.<2>__IsNullOrWhiteSpace = new Func<string, bool>(string.IsNullOrWhiteSpace));
					}
					if (tableTextSet2.All(func) || (array != null && array.Any((string v) => !tableTextSet.Contains(v))))
					{
						continue;
					}
					string text = this.InferSpecificSelector(domNode, domNode.Document.AllNodes);
					if (text == null)
					{
						continue;
					}
					List<string> list3 = new List<string>();
					if (domNode.GetChildren().Any((IDomNode n) => n.NodeName == "TR"))
					{
						list3.Add(text + " > TR");
					}
					if (domNode.GetChildren().Any((IDomNode n) => n.GetChildren().Any((IDomNode c) => c.NodeName == "TR")))
					{
						list3.Add(text + " > * > TR");
					}
					if (!list3.Any<string>())
					{
						continue;
					}
					IDomNode[][] array3;
					List<string> list4 = HtmlTableInference.InferHtmlColumnSelectors(domNode, list3, list, this._options.TextComparer, out array3);
					if (list4 == null && textTableExamples != null)
					{
						List<string> list5;
						if (!domNode.GetChildren().Any((IDomNode n) => Witnesses.HasNodeName(n, "tbody")))
						{
							list5 = list3.Select((string s) => s + " + TR").ToList<string>();
						}
						else
						{
							(list5 = new List<string>()).Add(text + " > TBODY > TR");
						}
						list3 = list5;
						list4 = HtmlTableInference.InferHtmlColumnSelectors(domNode, list3, list, this._options.TextComparer, out array3);
					}
					if (list4 != null && array3 != null)
					{
						string text2 = string.Join(", ", list3);
						string text3 = Witnesses.InferHtmlTableTitle(domNode);
						int num = ((array3.Length != 0) ? array3[0].Length : 0);
						if (textTableExamples != null)
						{
							return ProgramSetBuilder.List<resultTable>(this._build.Symbol.resultTable, new resultTable[] { this.BuildTableProgram(text2, list4, TableKind.HtmlTable, text3, num) }).Set;
						}
						list2.Add(Tuple.Create<string, List<string>, string, int>(text2, list4, text3, num));
						tableNodeMatches.Add(array3);
					}
				}
				if (list2.Count > 10000)
				{
					break;
				}
			}
			resultTable[] array4 = list2.Select((Tuple<string, List<string>, string, int> t) => this.BuildTableProgram(t.Item1, t.Item2, TableKind.HtmlTable, t.Item3, t.Item4)).ToArray<resultTable>();
			return ProgramSetBuilder.List<resultTable>(this._build.Symbol.resultTable, array4).Set;
		}

		// Token: 0x060081B5 RID: 33205 RVA: 0x001B3758 File Offset: 0x001B1958
		private List<resultTable> GetPredictiveTableProgramsFromCache(out List<IDomNode[][]> tableNodes)
		{
			tableNodes = new List<IDomNode[][]>();
			List<resultTable> list = new List<resultTable>();
			if (this._predictiveLearningCache == null || base.LearnedPrograms == null || !this._predictiveLearningCache.Any<Witnesses.AlignmentGroup>())
			{
				return list;
			}
			foreach (Witnesses.AlignmentGroup alignmentGroup in this._predictiveLearningCache)
			{
				IDomNode[][] tNodes = (from s in alignmentGroup.FullColumnStates.Concat(alignmentGroup.SubColumnStates)
					select s.Result).ToArray<IDomNode[]>();
				if (!this.AllSameColumns(tNodes) && !this.AllSameRows(tNodes) && !tableNodes.Any((IDomNode[][] n) => this.IsSubTable(tNodes, n, 2)))
				{
					resultSequence program = alignmentGroup.RowState.Program;
					columnSelectors columnSelectors = (from s in alignmentGroup.FullColumnStates.Concat(alignmentGroup.SubColumnStates)
						select s.Program).AggregateSeedFunc(new Func<resultSequence, columnSelectors>(this._rules.SingleColumn), new Func<columnSelectors, resultSequence, columnSelectors>(this._rules.ColumnSequence));
					resultTable resultTable = this._rules.ExtractRowBasedTable(columnSelectors, program);
					int num = ((tNodes.Length != 0) ? tNodes[0].Length : 0);
					resultTable.Node.Metadata = new ProgramMetadata(null, TableKind.LogicalTable, num, null);
					int[] array = (from k in tableNodes.Select(delegate(IDomNode[][] n, int k)
						{
							if (!this.IsSubTable(n, tNodes, 2))
							{
								return -1;
							}
							return k;
						})
						where k != -1
						select k).ToArray<int>();
					if (array.Any<int>())
					{
						int num2 = array[0];
						list[num2] = resultTable;
						tableNodes[num2] = tNodes;
					}
					else
					{
						list.Add(resultTable);
						tableNodes.Add(tNodes);
					}
				}
			}
			return list;
		}

		// Token: 0x060081B6 RID: 33206 RVA: 0x001B3994 File Offset: 0x001B1B94
		private static string InferHtmlTableTitle(IDomNode tableNode)
		{
			string text;
			if (!Witnesses.TryInferTitleFromCaptionTag(tableNode, out text))
			{
				return Witnesses.InferTitleFromOuterTags(tableNode);
			}
			return text;
		}

		// Token: 0x060081B7 RID: 33207 RVA: 0x001B39B4 File Offset: 0x001B1BB4
		private static bool TryInferTitleFromCaptionTag(IDomNode tableNode, out string title)
		{
			title = null;
			IEnumerable<IDomNode> children = tableNode.GetChildren();
			IDomNode domNode = children.FirstOrDefault((IDomNode childNode) => Witnesses.HasNodeName(childNode, "caption"));
			if (domNode != null)
			{
				title = domNode.GetVisibleTextContent();
				return true;
			}
			using (IEnumerator<IDomNode> enumerator = children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (Witnesses.TryInferTitleFromCaptionTag(enumerator.Current, out title))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060081B8 RID: 33208 RVA: 0x001B3A40 File Offset: 0x001B1C40
		private static string InferTitleFromOuterTags(IDomNode tableNode)
		{
			foreach (IDomNode domNode in tableNode.GetOlderSiblings().Reverse<IDomNode>().Take(Witnesses.MaxSiblingsToSearchForTitle))
			{
				if (Witnesses.HeaderTagMatcher.Match(domNode.NodeName).Success)
				{
					return domNode.GetVisibleTextContent();
				}
			}
			return null;
		}

		// Token: 0x060081B9 RID: 33209 RVA: 0x001B3AB8 File Offset: 0x001B1CB8
		private List<IDomNode> GetFirstChildNodeSequence(List<IDomNode> nodes)
		{
			if (nodes.All((IDomNode n) => n.ChildrenCount > 0))
			{
				return nodes.Select((IDomNode n) => n.GetChildren().FirstOrDefault<IDomNode>()).ToList<IDomNode>();
			}
			return null;
		}

		// Token: 0x060081BA RID: 33210 RVA: 0x001B3B18 File Offset: 0x001B1D18
		private bool InferRowColumnAlignment(ref resultSequence? rowProgram, ref List<resultSequence> columnPrograms, ref int numTableRows, ref IDomNode[] rowResult, ref IReadOnlyList<IDomNode[]> colResults, List<List<string>> textTableExamples, string[] colAttributes, State inpState)
		{
			if (rowProgram != null)
			{
				colResults = Microsoft.ProgramSynthesis.Extraction.Web.Semantics.Semantics.GetBoundaryBasedRowAlignment(colResults, rowResult);
			}
			int i;
			int[] array = colResults.Select(delegate(IDomNode[] r, int i)
			{
				if (r != null)
				{
					return this.GetMaxExampleSatisfactionOffset(r, textTableExamples[i], colAttributes[i]);
				}
				return -1;
			}).ToArray<int>();
			if (colResults.Any<IDomNode[]>())
			{
				numTableRows = colResults[0].Length;
			}
			if (rowProgram == null && columnPrograms.Count > 1)
			{
				resultSequence? resultSequence = null;
				IDomNode[] array2 = null;
				int num = -1;
				for (i = 0; i < columnPrograms.Count; i++)
				{
					int num2 = array[i];
					if (num2 >= 0 && num2 <= this._options.MaxExampleSatisfactionOffset && colResults[i].Any<IDomNode>() && (num < 0 || colResults[i][0].Start < num))
					{
						resultSequence = new resultSequence?(columnPrograms[i]);
						array2 = colResults[i];
						num = array2[0].Start;
					}
				}
				if (array2 != null)
				{
					IDomNode[][] array3 = Microsoft.ProgramSynthesis.Extraction.Web.Semantics.Semantics.GetBoundaryBasedRowAlignment(colResults, array2);
					int[] array4 = array3.Select(delegate(IDomNode[] r, int i)
					{
						if (r != null)
						{
							return this.GetMaxExampleSatisfactionOffset(r, textTableExamples[i], colAttributes[i]);
						}
						return -1;
					}).ToArray<int>();
					if (array4.Any((int k) => k < 0))
					{
						IDomNode[][] array5;
						resultSequence? resultSequence2 = this.InferAncestorBasedRowSelector(columnPrograms, colResults, colAttributes, textTableExamples, inpState, out array5);
						if (resultSequence2 != null)
						{
							resultSequence = resultSequence2;
							array3 = array5;
							array4 = Enumerable.Repeat<int>(0, columnPrograms.Count).ToArray<int>();
						}
					}
					colResults = array3;
					array = array4;
					rowProgram = resultSequence;
					if (colResults.Any<IDomNode[]>())
					{
						numTableRows = colResults[0].Length;
					}
				}
			}
			if (array.All((int k) => k == 0))
			{
				return true;
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] < 0 || array[j] > this._options.MaxExampleSatisfactionOffset)
				{
					columnPrograms[j] = this._rules.EmptySequence();
				}
			}
			return false;
		}

		// Token: 0x060081BB RID: 33211 RVA: 0x001B3D50 File Offset: 0x001B1F50
		private resultSequence? InferAncestorBasedRowSelector(List<resultSequence> columnPrograms, IReadOnlyList<IDomNode[]> colResults, string[] colAttributes, List<List<string>> textTableExamples, State inpState, out IDomNode[][] bestAlignedNodes)
		{
			bestAlignedNodes = null;
			IEnumerable<IDomNode[]> enumerable = colResults.Where((IDomNode[] c) => c.Length != 0 && c[0] != null);
			if (enumerable.IsEmpty<IDomNode[]>())
			{
				return null;
			}
			int num = enumerable.Min((IDomNode[] c) => c[0].Start);
			List<List<IDomNode>> list = new List<List<IDomNode>>();
			Func<IDomNode[], int, int> <>9__4;
			for (int i = 0; i < columnPrograms.Count; i++)
			{
				if (colResults[i].Length > 1)
				{
					if (!colResults[i].Any((IDomNode n) => n == null))
					{
						List<IDomNode> candidateRowNodes = NodeSequence.GetMaxUncommonAncestorNodes(colResults[i]);
						int num2 = 0;
						int num3 = 4;
						while (candidateRowNodes != null && num2 < num3 && candidateRowNodes[0].Start <= num && list.All((List<IDomNode> x) => !x.SequenceEqual(candidateRowNodes)))
						{
							list.Add(candidateRowNodes);
							IDomNode[][] boundaryBasedRowAlignment = Microsoft.ProgramSynthesis.Extraction.Web.Semantics.Semantics.GetBoundaryBasedRowAlignment(colResults, candidateRowNodes);
							IEnumerable<IDomNode[]> enumerable2 = boundaryBasedRowAlignment;
							Func<IDomNode[], int, int> func;
							if ((func = <>9__4) == null)
							{
								func = (<>9__4 = delegate(IDomNode[] r, int k)
								{
									if (r != null)
									{
										return this.GetMaxExampleSatisfactionOffset(r, textTableExamples[k], colAttributes[k]);
									}
									return -1;
								});
							}
							if (enumerable2.Select(func).ToArray<int>().All((int k) => k == 0))
							{
								WebRegion[] array = candidateRowNodes.Select((IDomNode n) => new WebRegion(n)).Take(100).ToArray<WebRegion>();
								SubsequenceSpec subsequenceSpec = new SubsequenceSpec(new Dictionary<State, IEnumerable<object>> { { inpState, array } });
								LearningTask learningTask = this._learningTask.Clone(this._build.Symbol.resultSequence, subsequenceSpec);
								resultSequence? resultSequence = this._engine.Learn(learningTask, this._cancel).RealizedPrograms.Select(new Func<ProgramNode, resultSequence>(this._build.Node.Cast.resultSequence)).FirstOrNull<resultSequence>();
								IDomNode[] array2 = ((resultSequence == null) ? null : this.ExecuteSequenceProgram(resultSequence.Value, inpState));
								if (array2 != null && array2.SequenceEqual(candidateRowNodes))
								{
									bestAlignedNodes = boundaryBasedRowAlignment;
									return resultSequence;
								}
							}
							candidateRowNodes = this.GetFirstChildNodeSequence(candidateRowNodes);
							num2++;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060081BC RID: 33212 RVA: 0x001B401C File Offset: 0x001B221C
		private HashSet<int> GetPreviouslySatisfiedUnchangedColumns(List<List<string>> newExamples)
		{
			if (this._options.PreviousTextTableExamples == null || this._options.PreviouslyLearntColumnSelectors == null || this._options.PreviousTextTableExamples.Count != this._options.PreviouslyLearntColumnSelectors.Length)
			{
				return null;
			}
			HashSet<int> hashSet = new HashSet<int>();
			int num = 0;
			while (num < newExamples.Count && num < this._options.PreviousTextTableExamples.Count)
			{
				List<string> list = newExamples[num];
				IReadOnlyList<string> readOnlyList = this._options.PreviousTextTableExamples[num];
				if (((list != null) & (readOnlyList != null)) && list.SequenceEqual(readOnlyList))
				{
					resultSequence resultSequence = this._options.PreviouslyLearntColumnSelectors[num];
					if (!resultSequence.Is_EmptySequence(this._build))
					{
						hashSet.Add(num);
					}
				}
				num++;
			}
			return hashSet;
		}

		// Token: 0x060081BD RID: 33213 RVA: 0x001B40E8 File Offset: 0x001B22E8
		private static bool HasDegenerateTextValues(NodeCollection col)
		{
			Witnesses.<>c__DisplayClass84_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass84_0();
			Witnesses.<>c__DisplayClass84_0 CS$<>8__locals2 = CS$<>8__locals1;
			IDomNode[] sortedSet = col.SortedSet;
			string text;
			if (sortedSet == null)
			{
				text = null;
			}
			else
			{
				text = sortedSet.Select(delegate(IDomNode n)
				{
					if (n == null)
					{
						return null;
					}
					return n.TrimmedInnerText;
				}).FirstOrDefault((string s) => !string.IsNullOrWhiteSpace(s));
			}
			CS$<>8__locals2.firstVal = text;
			if (CS$<>8__locals1.firstVal != null)
			{
				return CS$<>8__locals1.firstVal.All((char c) => !char.IsLetterOrDigit(c)) && col.SortedSet.All((IDomNode n) => ((n != null) ? n.TrimmedInnerText : null) == CS$<>8__locals1.firstVal);
			}
			return true;
		}

		// Token: 0x060081BE RID: 33214 RVA: 0x001B41A8 File Offset: 0x001B23A8
		private Optional<ProgramSet> LearnExtractTablePredictive(SynthesisEngine engine, LearningTask<Spec> task, CancellationToken cancel)
		{
			WithInputTopSpec inputSpec = task.Spec as WithInputTopSpec;
			if (inputSpec == null)
			{
				return Optional<ProgramSet>.Nothing;
			}
			State inpState = inputSpec.ProvidedInputs.FirstOrDefault<State>();
			List<IDomNode[][]> list2;
			List<Record<resultTable, IDomNode[][]>> list = (from r in this.LearnHtmlTablePrograms(inpState, null, out list2).RealizedPrograms.Select(new Func<ProgramNode, resultTable>(this._build.Node.Cast.resultTable)).ToList<resultTable>().ZipWith(list2)
				orderby this.GetTableDocumentOrderIndex(r.Item2)
				select r).ToList<Record<resultTable, IDomNode[][]>>();
			if (list2.Count((IDomNode[][] t) => this.GetNumRows(t) > 5) < 15)
			{
				resultTable? resultTable = null;
				resultSequence[] array = new resultSequence[0];
				IDomNode[][] array2 = new IDomNode[0][];
				Task<resultSequence[]> task2 = Task.Run<resultSequence[]>(() => engine.LearnSymbol(this._build.Symbol.nodeCollection, inputSpec, cancel).RealizedPrograms.Select(new Func<ProgramNode, ProgramNode>(this._build.Rule.ConvertToWebRegions.BuildASTNode)).Select(new Func<ProgramNode, resultSequence>(this._build.Node.Cast.resultSequence)).ToArray<resultSequence>());
				if (task2.Wait(this._options.PredictiveLogicalTableInferenceTimeout))
				{
					array = task2.Result;
				}
				if (array.Length == 1)
				{
					resultTable = new resultTable?(this._rules.ExtractTable(array.AggregateSeedFunc(new Func<resultSequence, columnSelectors>(this._rules.SingleColumn), new Func<columnSelectors, resultSequence, columnSelectors>(this._rules.ColumnSequence))));
					array2 = array.Select(delegate(resultSequence p)
					{
						object obj = p.Node.Invoke(inpState);
						if (obj == null)
						{
							return null;
						}
						IEnumerable<object> enumerable = obj.ToEnumerable<object>();
						if (enumerable == null)
						{
							return null;
						}
						return (from WebRegion r in enumerable
							select r.BeginNode).ToArray<IDomNode>();
					}).ToArray<IDomNode[]>();
				}
				else if (this._predictiveTopAlignmentGroup != null)
				{
					resultSequence program = this._predictiveTopAlignmentGroup.RowState.Program;
					array2 = (from s in this._predictiveTopAlignmentGroup.FullColumnStates.Concat(this._predictiveTopAlignmentGroup.SubColumnStates)
						select s.Result).ToArray<IDomNode[]>();
					array = (from s in this._predictiveTopAlignmentGroup.FullColumnStates.Concat(this._predictiveTopAlignmentGroup.SubColumnStates)
						select s.Program).ToArray<resultSequence>();
					if (array.Length != 0)
					{
						columnSelectors columnSelectors = array.AggregateSeedFunc(new Func<resultSequence, columnSelectors>(this._rules.SingleColumn), new Func<columnSelectors, resultSequence, columnSelectors>(this._rules.ColumnSequence));
						resultTable = new resultTable?(this._rules.ExtractRowBasedTable(columnSelectors, program));
						int num = ((array2.Length != 0) ? array2[0].Length : 0);
						resultTable.Value.Node.Metadata = new ProgramMetadata(null, TableKind.LogicalTable, num, null);
					}
				}
				List<resultTable> list3 = new List<resultTable>();
				List<IDomNode[][]> list4 = new List<IDomNode[][]>();
				if (resultTable != null && !this.AllSameRows(array2) && !this.AllSameColumns(array2))
				{
					list3.Add(resultTable.Value);
					list4.Add(array2);
				}
				List<IDomNode[][]> list5;
				List<resultTable> predictiveTableProgramsFromCache = this.GetPredictiveTableProgramsFromCache(out list5);
				for (int i = 0; i < predictiveTableProgramsFromCache.Count; i++)
				{
					if (!this.IsSubTable(list5[i], array2, 2))
					{
						list3.Add(predictiveTableProgramsFromCache[i]);
						list4.Add(list5[i]);
					}
				}
				for (int j = 0; j < list3.Count; j++)
				{
					IDomNode[][] tableNodes = list4[j];
					if (!tableNodes.All((IDomNode[] c) => c.All((IDomNode n) => n == null || Witnesses.HasNodeName(n, "TD") || Witnesses.HasNodeName(n, "TH"))) || list2.All((IDomNode[][] t) => !this.IsSubTable(tableNodes, t, 2)))
					{
						list.Add(new Record<resultTable, IDomNode[][]>(list3[j], list4[j]));
					}
				}
			}
			resultTable[] array3 = list.Select((Record<resultTable, IDomNode[][]> r) => r.Item1).ToArray<resultTable>();
			return ProgramSetBuilder.List<resultTable>(this._build.Symbol.resultTable, array3).Set.Some<ProgramSet>();
		}

		// Token: 0x060081BF RID: 33215 RVA: 0x001B45BC File Offset: 0x001B27BC
		private int GetTableDocumentOrderIndex(IDomNode[][] t)
		{
			if (t == null || t.Length == 0)
			{
				return -1;
			}
			IDomNode domNode = t.Where((IDomNode[] c) => c != null).SelectMany((IDomNode[] c) => c).FirstOrDefault<IDomNode>();
			if (domNode == null)
			{
				return -1;
			}
			return domNode.Start;
		}

		// Token: 0x060081C0 RID: 33216 RVA: 0x001B462C File Offset: 0x001B282C
		private bool EqualTableText(IDomNode[][] t1, IDomNode[][] t2)
		{
			if (t1 == null || t2 == null)
			{
				return false;
			}
			if (!t1.Any((IDomNode[] c) => c == null))
			{
				if (!t2.Any((IDomNode[] c) => c == null))
				{
					return t1.Length == t2.Length && !t1.Any((IDomNode[] c, int i) => c.Length != t2[i].Length) && !t1.Any((IDomNode[] c, int i) => c.Any(delegate(IDomNode n, int j)
					{
						string text = ((n != null) ? n.TrimmedInnerText : null);
						IDomNode domNode = t2[i][j];
						return text != ((domNode != null) ? domNode.TrimmedInnerText : null);
					}));
				}
			}
			return false;
		}

		// Token: 0x060081C1 RID: 33217 RVA: 0x001B46E0 File Offset: 0x001B28E0
		private int GetNumRows(IDomNode[][] t)
		{
			if (t != null && t.Length != 0)
			{
				if (!t.Any((IDomNode[] c) => c == null))
				{
					return t[0].Length;
				}
			}
			return 0;
		}

		// Token: 0x060081C2 RID: 33218 RVA: 0x001B4718 File Offset: 0x001B2918
		private bool AllSameColumns(IDomNode[][] t)
		{
			if (t == null || t.Length == 0)
			{
				return true;
			}
			if (t.Any((IDomNode[] c) => c == null))
			{
				return t.All((IDomNode[] c) => c == null);
			}
			IEnumerable<string> firstColText = t[0].Select(delegate(IDomNode n)
			{
				if (n == null)
				{
					return null;
				}
				return n.TrimmedInnerText;
			});
			return t.All((IDomNode[] c) => c.Select(delegate(IDomNode n)
			{
				if (n == null)
				{
					return null;
				}
				return n.TrimmedInnerText;
			}).SequenceEqual(firstColText));
		}

		// Token: 0x060081C3 RID: 33219 RVA: 0x001B47C0 File Offset: 0x001B29C0
		private bool AllSameRows(IDomNode[][] t)
		{
			if (t != null && t.Length != 0)
			{
				if (!t.Any((IDomNode[] c) => c == null))
				{
					int numRows = t[0].Length;
					if (numRows == 0)
					{
						return true;
					}
					if (t.Any((IDomNode[] c) => c.Length != numRows))
					{
						return false;
					}
					return t.All((IDomNode[] c) => c.All(delegate(IDomNode n)
					{
						string text = ((n != null) ? n.TrimmedInnerText : null);
						IDomNode domNode = c[0];
						return text == ((domNode != null) ? domNode.TrimmedInnerText : null);
					}));
				}
			}
			return true;
		}

		// Token: 0x060081C4 RID: 33220 RVA: 0x001B4854 File Offset: 0x001B2A54
		private bool IsSubTable(IDomNode[][] t1, IDomNode[][] t2, int maxRowDiff)
		{
			if (t1 != null && t2 != null && t1.Length != 0 && t2.Length != 0)
			{
				if (!t1.Any((IDomNode[] c) => c == null))
				{
					if (!t2.Any((IDomNode[] c) => c == null) && t1.Length <= t2.Length)
					{
						int numRows1 = t1[0].Length;
						int num = t2[0].Length;
						return numRows1 <= num && Enumerable.Range(0, num - numRows1 + 1).Any((int i) => t1.All((IDomNode[] c1) => t2.Any((IDomNode[] c2) => c2.Skip(i).Take(numRows1).Select(delegate(IDomNode n)
						{
							if (n == null)
							{
								return null;
							}
							return n.TrimmedInnerText;
						})
							.SequenceEqual(c1.Select(delegate(IDomNode n)
							{
								if (n == null)
								{
									return null;
								}
								return n.TrimmedInnerText;
							})))));
					}
				}
			}
			return false;
		}

		// Token: 0x060081C5 RID: 33221 RVA: 0x001B4950 File Offset: 0x001B2B50
		private void InitializePredictiveCache(State inpState)
		{
			if (this._options.PredictiveRowColumnSelectors == null)
			{
				return;
			}
			this._predictiveLearningCache = new List<Witnesses.AlignmentGroup>();
			foreach (Tuple<resultSequence, resultSequence[]> tuple in this._options.PredictiveRowColumnSelectors)
			{
				resultSequence item = tuple.Item1;
				IDomNode[] array = this.ExecuteSequenceProgram(item, inpState);
				if (array != null && !array.IsEmpty<IDomNode>())
				{
					IDomNode domNode = DomNodeExt.LowestCommonAncestor(array);
					if (domNode != null)
					{
						IDomNode[][] array2 = new IDomNode[tuple.Item2.Length][];
						for (int i = 0; i < tuple.Item2.Length; i++)
						{
							array2[i] = this.ExecuteSequenceProgram(tuple.Item2[i], inpState);
						}
						if (!array2.Any((IDomNode[] c) => c == null || c.IsEmpty<IDomNode>()))
						{
							IDomNode[][] boundaryBasedRowAlignment = Microsoft.ProgramSynthesis.Extraction.Web.Semantics.Semantics.GetBoundaryBasedRowAlignment(array2, array);
							List<Witnesses.ProgramState> list = new List<Witnesses.ProgramState>();
							List<Witnesses.ProgramState> list2 = new List<Witnesses.ProgramState>();
							for (int j = 0; j < tuple.Item2.Length; j++)
							{
								if (boundaryBasedRowAlignment[j].All((IDomNode n) => n != null))
								{
									list.Add(new Witnesses.ProgramState(tuple.Item2[j], boundaryBasedRowAlignment[j]));
								}
								else
								{
									list2.Add(new Witnesses.ProgramState(tuple.Item2[j], boundaryBasedRowAlignment[j]));
								}
							}
							Witnesses.ProgramState programState = new Witnesses.ProgramState(item, array);
							this._predictiveLearningCache.Add(new Witnesses.AlignmentGroup(domNode, programState, list, list2));
						}
					}
				}
			}
		}

		// Token: 0x060081C6 RID: 33222 RVA: 0x001B4B20 File Offset: 0x001B2D20
		private Tuple<Witnesses.ProgramState, IReadOnlyList<Witnesses.ProgramState>> LearnPredictiveProgramFromCache(List<List<string>> textTableExamples)
		{
			if (this._predictiveLearningCache == null)
			{
				return null;
			}
			if (textTableExamples.Count != 0)
			{
				if (!textTableExamples.Any((List<string> c) => c.Count == 0))
				{
					List<List<string>> list = textTableExamples.Select(delegate(List<string> c)
					{
						Func<string, string> func3;
						if ((func3 = Witnesses.<>O.<1>__NormalizeText) == null)
						{
							func3 = (Witnesses.<>O.<1>__NormalizeText = new Func<string, string>(HtmlDoc.NormalizeText));
						}
						return c.Select(func3).ToList<string>();
					}).ToList<List<string>>();
					if (list.All(delegate(List<string> c)
					{
						Func<string, bool> func4;
						if ((func4 = Witnesses.<>O.<2>__IsNullOrWhiteSpace) == null)
						{
							func4 = (Witnesses.<>O.<2>__IsNullOrWhiteSpace = new Func<string, bool>(string.IsNullOrWhiteSpace));
						}
						return c.All(func4);
					}))
					{
						return null;
					}
					Tuple<Witnesses.ProgramState, IReadOnlyList<Witnesses.ProgramState>> tuple = null;
					foreach (Witnesses.AlignmentGroup alignmentGroup in this._predictiveLearningCache)
					{
						IDomNode[] result = alignmentGroup.RowState.Result;
						List<Witnesses.ProgramState> list2 = new List<Witnesses.ProgramState>();
						HashSet<Witnesses.ProgramState> usedColStates = new HashSet<Witnesses.ProgramState>();
						Func<Witnesses.ProgramState, int> <>9__5;
						foreach (List<string> list3 in list)
						{
							IEnumerable<Witnesses.ProgramState> enumerable = alignmentGroup.FullColumnStates.Concat(alignmentGroup.SubColumnStates);
							Func<Witnesses.ProgramState, int> func;
							if ((func = <>9__5) == null)
							{
								func = (<>9__5 = (Witnesses.ProgramState s) => (usedColStates.Contains(s) > false) ? 1 : 0);
							}
							IEnumerable<Witnesses.ProgramState> enumerable2 = enumerable.OrderBy(func);
							bool flag = false;
							foreach (Witnesses.ProgramState programState in enumerable2)
							{
								IDomNode[] tableColNodes = programState.Result;
								if (tableColNodes.Length >= list3.Count && tableColNodes.Length == result.Length && !list3.Any(delegate(string ex, int k)
								{
									StringComparer textComparer = this._options.TextComparer;
									IDomNode domNode = tableColNodes[k];
									return !textComparer.Equals(ex, ((domNode != null) ? domNode.NormalizedInnerText : null) ?? string.Empty);
								}))
								{
									list2.Add(programState);
									usedColStates.Add(programState);
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								list2.Add(null);
							}
						}
						int num = list2.Count((Witnesses.ProgramState c) => c != null);
						if (num != 0)
						{
							if (tuple != null)
							{
								if (num < tuple.Item2.Count((Witnesses.ProgramState c) => c != null))
								{
									continue;
								}
							}
							if (list.Count > 1)
							{
								goto IL_028F;
							}
							IEnumerable<string> enumerable3 = list[0];
							Func<string, bool> func2;
							if ((func2 = Witnesses.<>O.<3>__IsNullOrEmpty) == null)
							{
								func2 = (Witnesses.<>O.<3>__IsNullOrEmpty = new Func<string, bool>(string.IsNullOrEmpty));
							}
							if (enumerable3.Any(func2))
							{
								goto IL_028F;
							}
							Witnesses.ProgramState programState2 = null;
							IL_0295:
							tuple = new Tuple<Witnesses.ProgramState, IReadOnlyList<Witnesses.ProgramState>>(programState2, list2);
							if (num == textTableExamples.Count)
							{
								break;
							}
							continue;
							IL_028F:
							programState2 = alignmentGroup.RowState;
							goto IL_0295;
						}
					}
					return tuple;
				}
			}
			return null;
		}

		// Token: 0x060081C7 RID: 33223 RVA: 0x001B4E40 File Offset: 0x001B3040
		private ProgramSet BuildTableProgramSet(Witnesses.ProgramState rowState, IReadOnlyList<Witnesses.ProgramState> colStates)
		{
			ProgramSetBuilder<columnSelectors> programSetBuilder = ProgramSetBuilder.List<columnSelectors>(this._build.Symbol.columnSelectors, new columnSelectors[] { colStates.Select(delegate(Witnesses.ProgramState s)
			{
				if (s == null)
				{
					return this._rules.EmptySequence();
				}
				return s.Program;
			}).AggregateSeedFunc(new Func<resultSequence, columnSelectors>(this._rules.SingleColumn), new Func<columnSelectors, resultSequence, columnSelectors>(this._rules.ColumnSequence)) });
			if (rowState == null)
			{
				return this._build.Set.Join.ExtractTable(programSetBuilder).Set;
			}
			ProgramSetBuilder<resultSequence> programSetBuilder2 = ProgramSetBuilder.List<resultSequence>(this._build.Symbol.resultSequence, new resultSequence[] { rowState.Program });
			return this._build.Set.Join.ExtractRowBasedTable(programSetBuilder, programSetBuilder2).Set;
		}

		// Token: 0x060081C8 RID: 33224 RVA: 0x001B4F10 File Offset: 0x001B3110
		private bool InitializePredictiveTextExamples(List<List<string>> textTableExamples, out List<string> normalizedExamples, out string[] exampleTextSet)
		{
			normalizedExamples = null;
			exampleTextSet = null;
			if (textTableExamples.Count != 1 || textTableExamples[0].Count < 2)
			{
				return false;
			}
			IEnumerable<string> enumerable = textTableExamples[0];
			Func<string, string> func;
			if ((func = Witnesses.<>O.<1>__NormalizeText) == null)
			{
				func = (Witnesses.<>O.<1>__NormalizeText = new Func<string, string>(HtmlDoc.NormalizeText));
			}
			normalizedExamples = enumerable.Select(func).ToList<string>();
			exampleTextSet = normalizedExamples.Where((string v) => !string.IsNullOrWhiteSpace(v)).ToArray<string>();
			return !exampleTextSet.IsEmpty<string>();
		}

		// Token: 0x060081C9 RID: 33225 RVA: 0x001B4FA4 File Offset: 0x001B31A4
		private Tuple<resultSequence?, IDomNode[]> InferHigherRankedProgramFromPredictiveCache(List<List<string>> textTableExamples, IDomNode[] topDownResult, State inpState, SynthesisEngine engine, LearningTask<Spec> task, CancellationToken cancel)
		{
			Witnesses.<>c__DisplayClass96_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass96_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.topDownResult = topDownResult;
			if (CS$<>8__locals1.topDownResult != null)
			{
				if (!CS$<>8__locals1.topDownResult.Any((IDomNode n) => n == null))
				{
					List<string> list;
					string[] array;
					if (!this.InitializePredictiveTextExamples(textTableExamples, out list, out array))
					{
						return null;
					}
					IDomNode domNode = (CS$<>8__locals1.topDownResult.IsEmpty<IDomNode>() ? null : DomNodeExt.LowestCommonAncestor(CS$<>8__locals1.topDownResult));
					bool flag = CS$<>8__locals1.topDownResult.Length >= list.Count && !list.Any(delegate(string ex, int k)
					{
						StringComparer textComparer = CS$<>8__locals1.<>4__this._options.TextComparer;
						IDomNode domNode3 = CS$<>8__locals1.topDownResult[k];
						return !textComparer.Equals(ex, (domNode3 != null) ? domNode3.NormalizedInnerText : null);
					});
					if (this._predictiveLearningCache != null)
					{
						foreach (Witnesses.AlignmentGroup alignmentGroup in this._predictiveLearningCache)
						{
							HashSet<string> tableTextSet = alignmentGroup.FullColumnStates.SelectMany((Witnesses.ProgramState r) => r.Result.Select((IDomNode n) => n.NormalizedInnerText)).ConvertToHashSet(this._options.TextComparer);
							IEnumerable<string> tableTextSet2 = tableTextSet;
							Func<string, bool> func;
							if ((func = Witnesses.<>O.<2>__IsNullOrWhiteSpace) == null)
							{
								func = (Witnesses.<>O.<2>__IsNullOrWhiteSpace = new Func<string, bool>(string.IsNullOrWhiteSpace));
							}
							if (!tableTextSet2.All(func) && !array.Any((string v) => !tableTextSet.Contains(v)))
							{
								IDomNode[] result = alignmentGroup.RowState.Result;
								int num;
								if (this.ContainsChildSequence(result, CS$<>8__locals1.topDownResult, out num))
								{
									foreach (Witnesses.ProgramState programState in alignmentGroup.FullColumnStates)
									{
										IDomNode[] tableColNodes = programState.Result;
										if (tableColNodes.Length >= list.Count && tableColNodes.Length == result.Length && !list.Any(delegate(string ex, int k)
										{
											StringComparer textComparer2 = CS$<>8__locals1.<>4__this._options.TextComparer;
											IDomNode domNode4 = tableColNodes[k];
											return !textComparer2.Equals(ex, (domNode4 != null) ? domNode4.NormalizedInnerText : null);
										}) && (domNode == null || domNode == alignmentGroup.LcaNode))
										{
											return Tuple.Create<resultSequence?, IDomNode[]>(new resultSequence?(programState.Program), programState.Result);
										}
									}
								}
							}
						}
						if (flag)
						{
							foreach (Witnesses.AlignmentGroup alignmentGroup2 in this._predictiveLearningCache)
							{
								Witnesses.<>c__DisplayClass96_3 CS$<>8__locals4 = new Witnesses.<>c__DisplayClass96_3();
								CS$<>8__locals4.CS$<>8__locals3 = CS$<>8__locals1;
								if (domNode == alignmentGroup2.LcaNode)
								{
									IDomNode[] result2 = alignmentGroup2.RowState.Result;
									if (CS$<>8__locals4.CS$<>8__locals3.topDownResult.Length == result2.Length)
									{
										return null;
									}
									int num2;
									if (this.ContainsChildSequence(result2, CS$<>8__locals4.CS$<>8__locals3.topDownResult, out num2))
									{
										WebRegion[] array2 = (from n in CS$<>8__locals4.CS$<>8__locals3.topDownResult.Take(list.Count)
											select new WebRegion(n)).ToArray<WebRegion>();
										SubsequenceSpec subsequenceSpec = new SubsequenceSpec(new Dictionary<State, IEnumerable<object>> { { inpState, array2 } });
										LearningTask learningTask = task.Clone(this._build.Symbol.resultSequence, subsequenceSpec);
										this._rowNodesContrainingTopDownSynthesis = result2;
										IDomNode domNode2 = DomNodeExt.LastUnderLCA(result2);
										this._extractionBoundaryNodeIndex = ((domNode2 != null) ? domNode2.Start : (-1));
										engine.ClearLearningCache();
										resultSequence? resultSequence = engine.Learn(learningTask, cancel).RealizedPrograms.Select(new Func<ProgramNode, resultSequence>(this._build.Node.Cast.resultSequence)).FirstOrNull<resultSequence>();
										this._rowNodesContrainingTopDownSynthesis = null;
										this._extractionBoundaryNodeIndex = -1;
										Witnesses.<>c__DisplayClass96_3 CS$<>8__locals5 = CS$<>8__locals4;
										IDomNode[] array3;
										if (resultSequence == null)
										{
											array3 = null;
										}
										else
										{
											object obj = resultSequence.GetValueOrDefault().Node.Invoke(inpState);
											if (obj == null)
											{
												array3 = null;
											}
											else
											{
												IEnumerable<object> enumerable = obj.ToEnumerable<object>();
												if (enumerable == null)
												{
													array3 = null;
												}
												else
												{
													array3 = (from WebRegion r in enumerable
														select r.BeginNode).ToArray<IDomNode>();
												}
											}
										}
										CS$<>8__locals5.predResult = array3;
										if (CS$<>8__locals4.predResult != null && this.ContainsChildSequence(result2, CS$<>8__locals4.predResult, out num2) && !list.Any(delegate(string ex, int k)
										{
											StringComparer textComparer3 = CS$<>8__locals4.CS$<>8__locals3.<>4__this._options.TextComparer;
											IDomNode domNode5 = CS$<>8__locals4.predResult[k];
											return !textComparer3.Equals(ex, (domNode5 != null) ? domNode5.NormalizedInnerText : null);
										}))
										{
											return Tuple.Create<resultSequence?, IDomNode[]>(resultSequence, CS$<>8__locals4.predResult);
										}
									}
								}
							}
						}
					}
					if (!flag)
					{
						if (base.LearnedPrograms == null)
						{
							WithInputTopSpec withInputTopSpec = new WithInputTopSpec(new State[] { inpState });
							engine.LearnSymbol(this._build.Symbol.nodeCollection, withInputTopSpec, cancel).RealizedPrograms.FirstOrDefault<ProgramNode>();
						}
						Dictionary<object[], LearnerState> dictionary;
						if (base.LearnedPrograms != null && base.LearnedPrograms.TryGetValue(this._build.Symbol.nodeCollection, out dictionary))
						{
							foreach (KeyValuePair<object[], LearnerState> keyValuePair in dictionary)
							{
								NodeCollection c = keyValuePair.Key[0] as NodeCollection;
								if (c != null && list.Count <= c.SortedSet.Length && !list.Any(delegate(string ex, int k)
								{
									StringComparer textComparer4 = CS$<>8__locals1.<>4__this._options.TextComparer;
									IDomNode domNode6 = c.SortedSet[k];
									return !textComparer4.Equals(ex, (domNode6 != null) ? domNode6.NormalizedInnerText : null);
								}))
								{
									return Tuple.Create<resultSequence?, IDomNode[]>(new resultSequence?(this._build.Node.Cast.resultSequence(this._build.Rule.ConvertToWebRegions.BuildASTNode(keyValuePair.Value.Program))), c.SortedSet);
								}
							}
						}
					}
					return null;
				}
			}
			return null;
		}

		// Token: 0x060081CA RID: 33226 RVA: 0x001B55F4 File Offset: 0x001B37F4
		private bool ContainsChildSequence(IDomNode[] s1, IDomNode[] s2, out int rowIndex)
		{
			rowIndex = -1;
			if (s1.Length == 0 || s2.Length == 0)
			{
				return false;
			}
			foreach (IDomNode domNode in s2)
			{
				rowIndex++;
				while (rowIndex < s1.Length && !s1[rowIndex].Contains(domNode))
				{
					rowIndex++;
				}
				if (rowIndex == s1.Length)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060081CB RID: 33227 RVA: 0x001B5650 File Offset: 0x001B3850
		private bool IsSingleColumnCommonLca(Tuple<Witnesses.ProgramState, IReadOnlyList<Witnesses.ProgramState>> predictiveState, List<List<string>> textTableExamples)
		{
			if (((predictiveState != null) ? predictiveState.Item2 : null) == null || predictiveState.Item2.Count != 1 || textTableExamples.Count != 1 || textTableExamples[0].Count <= 1)
			{
				return false;
			}
			Witnesses.ProgramState programState = predictiveState.Item2[0];
			IDomNode[] array = ((programState != null) ? programState.Result : null);
			if (array != null && array.Length >= textTableExamples[0].Count)
			{
				if (!array.Any((IDomNode n) => n == null))
				{
					IDomNode[] array2 = array.Take(textTableExamples[0].Count).ToArray<IDomNode>();
					return !array2.IsEmpty<IDomNode>() && DomNodeExt.LowestCommonAncestor(array) == DomNodeExt.LowestCommonAncestor(array2);
				}
			}
			return false;
		}

		// Token: 0x060081CC RID: 33228 RVA: 0x001B571C File Offset: 0x001B391C
		private IDomNode[] ExecuteSequenceProgram(resultSequence prog, State input)
		{
			if (this._progResultCache == null)
			{
				this._progResultCache = new Dictionary<State, Dictionary<resultSequence, IDomNode[]>>();
			}
			if (this._progResultCache.ContainsKey(input) && this._progResultCache[input].ContainsKey(prog))
			{
				return this._progResultCache[input][prog];
			}
			object obj = prog.Node.Invoke(input);
			IDomNode[] array;
			if (obj == null)
			{
				array = null;
			}
			else
			{
				IEnumerable<object> enumerable = obj.ToEnumerable<object>();
				if (enumerable == null)
				{
					array = null;
				}
				else
				{
					array = (from WebRegion r in enumerable
						select r.BeginNode).ToArray<IDomNode>();
				}
			}
			IDomNode[] array2 = array;
			if (!this._progResultCache.ContainsKey(input))
			{
				this._progResultCache[input] = new Dictionary<resultSequence, IDomNode[]>();
			}
			this._progResultCache[input][prog] = array2;
			return array2;
		}

		// Token: 0x060081CD RID: 33229 RVA: 0x001B57F4 File Offset: 0x001B39F4
		private Dictionary<State, HashSet<int>> GetUnsatisfiedStates(resultTable tableProgram, List<Tuple<State, TextTableSpec>> stateTableSpecs, string[] columnAttributes)
		{
			resultSequence? rowSelectorNode = this.GetRowSelectorNode(tableProgram);
			resultSequence[] array = this.GetColumnSelectorNodes(tableProgram).ToArray<resultSequence>();
			Dictionary<State, HashSet<int>> dictionary = new Dictionary<State, HashSet<int>>();
			foreach (Tuple<State, TextTableSpec> tuple in stateTableSpecs)
			{
				State inputState = tuple.Item1;
				TextTableSpec item = tuple.Item2;
				List<List<string>> textTableExamples = this.GetTableTextExamples(inputState, item);
				IDomNode[] array2 = ((rowSelectorNode == null) ? null : this.ExecuteSequenceProgram(rowSelectorNode.Value, inputState));
				IDomNode[][] colResults = array.Select((resultSequence p) => this.ExecuteSequenceProgram(p, inputState)).ToArray<IDomNode[]>();
				if (rowSelectorNode != null)
				{
					colResults = Microsoft.ProgramSynthesis.Extraction.Web.Semantics.Semantics.GetBoundaryBasedRowAlignment(colResults, array2);
				}
				int[] maxExampleOffsets = colResults.Select(delegate(IDomNode[] r, int i)
				{
					if (r != null)
					{
						return this.GetMaxExampleSatisfactionOffset(r, textTableExamples[i], columnAttributes[i]);
					}
					return -1;
				}).ToArray<int>();
				if (maxExampleOffsets.Any((int x, int i) => x != 0 && colResults[i] != null && colResults[i].Any<IDomNode>()))
				{
					return null;
				}
				HashSet<int> hashSet = (from i in Enumerable.Range(0, maxExampleOffsets.Length)
					where maxExampleOffsets[i] != 0
					select i).ConvertToHashSet<int>();
				if (hashSet.Any<int>())
				{
					dictionary[inputState] = hashSet;
				}
			}
			return dictionary;
		}

		// Token: 0x060081CE RID: 33230 RVA: 0x001B59A4 File Offset: 0x001B3BA4
		[RuleLearner("ExtractTable")]
		public Optional<ProgramSet> LearnExtractTableRule(SynthesisEngine engine, GrammarRule rule, LearningTask<Spec> task, CancellationToken cancel)
		{
			if (task.Spec is WithInputTopSpec)
			{
				return this.LearnExtractTablePredictive(engine, task, cancel);
			}
			MultiPageTextTableSpec multiPageTextTableSpec = task.Spec as MultiPageTextTableSpec;
			if (multiPageTextTableSpec == null || multiPageTextTableSpec.TableSpecs.Count == 0)
			{
				return Optional<ProgramSet>.Nothing;
			}
			this._multiPageStateSpecs = multiPageTextTableSpec.TableSpecs;
			if (this._multiPageStateSpecs.Count == 1)
			{
				Tuple<State, TextTableSpec> tuple = this._multiPageStateSpecs.FirstOrDefault((Tuple<State, TextTableSpec> t) => t.Item2 != null);
				LearningTask<Spec> learningTask = task.Clone(rule.Head, tuple.Item2).Cast<Spec>();
				string[] array;
				return this.LearnSinglePageExtractTableRule(engine, rule, learningTask, cancel, out array);
			}
			List<Tuple<State, TextTableSpec>> list = this._multiPageStateSpecs.Where((Tuple<State, TextTableSpec> t) => t.Item2 != null).ToList<Tuple<State, TextTableSpec>>();
			resultTable? resultTable = null;
			Dictionary<State, HashSet<int>> dictionary = null;
			string[] array2 = null;
			for (int i = 0; i < 2; i++)
			{
				foreach (Tuple<State, TextTableSpec> tuple2 in list)
				{
					if (dictionary != null)
					{
						if (dictionary.IsEmpty<KeyValuePair<State, HashSet<int>>>())
						{
							break;
						}
						if (!dictionary.ContainsKey(tuple2.Item1))
						{
							continue;
						}
					}
					LearningTask<Spec> learningTask2 = task.Clone(rule.Head, tuple2.Item2).Cast<Spec>();
					Optional<ProgramSet> optional = this.LearnSinglePageExtractTableRule(engine, rule, learningTask2, cancel, out array2);
					ProgramNode programNode;
					if (optional.HasValue && !((programNode = optional.Value.RealizedPrograms.FirstOrDefault<ProgramNode>()) == null) && array2 != null)
					{
						resultTable resultTable2 = this._build.Node.Cast.resultTable(programNode);
						this.AugmentUnionProgram(ref resultTable, resultTable2, list, ref dictionary, array2, true);
					}
				}
				if (dictionary != null && dictionary.IsEmpty<KeyValuePair<State, HashSet<int>>>())
				{
					break;
				}
				engine.ClearLearningCache();
				this._learnMostSpecificConjunction = true;
			}
			if (resultTable == null)
			{
				return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
			}
			resultTable.Value.Node.Metadata = new ProgramMetadata(null, TableKind.ByExample, 0, array2);
			return ProgramSetBuilder.List<resultTable>(this._build.Symbol.resultTable, new resultTable[] { resultTable.Value }).Set.Some<ProgramSet>();
		}

		// Token: 0x060081CF RID: 33231 RVA: 0x001B5C1C File Offset: 0x001B3E1C
		private bool AugmentUnionProgram(ref resultTable? originalProg, resultTable newProg, List<Tuple<State, TextTableSpec>> ioSpecs, ref Dictionary<State, HashSet<int>> unsatStates, string[] colAttributes, bool includeRowSelector = false)
		{
			if (originalProg == null)
			{
				Dictionary<State, HashSet<int>> dictionary = this.GetUnsatisfiedStates(newProg, ioSpecs, colAttributes);
				if (!this.MoreSatisfied(unsatStates, dictionary))
				{
					return false;
				}
				originalProg = new resultTable?(newProg);
				unsatStates = dictionary;
				return true;
			}
			else
			{
				if (this.GetUnsatisfiedStates(newProg, ioSpecs, colAttributes) == null)
				{
					return false;
				}
				resultSequence? rowSelectorNode = this.GetRowSelectorNode(originalProg.Value);
				resultSequence[] array = this.GetColumnSelectorNodes(originalProg.Value).ToArray<resultSequence>();
				resultSequence? rowSelectorNode2 = this.GetRowSelectorNode(newProg);
				resultSequence[] newColProgs = this.GetColumnSelectorNodes(newProg).ToArray<resultSequence>();
				HashSet<int> unsatCols = unsatStates.SelectMany((KeyValuePair<State, HashSet<int>> kvp) => kvp.Value).ConvertToHashSet<int>();
				resultSequence[] updatedColProgs = array.Select(delegate(resultSequence p, int k)
				{
					if (!unsatCols.Contains(k))
					{
						return p;
					}
					return this._rules.Union(p, newColProgs[k]);
				}).ToArray<resultSequence>();
				resultSequence? resultSequence = ((includeRowSelector && rowSelectorNode != null && rowSelectorNode2 != null) ? new resultSequence?(this._rules.Union(rowSelectorNode.Value, rowSelectorNode2.Value)) : rowSelectorNode);
				resultTable resultTable = this.BuildTableProgram(resultSequence, updatedColProgs);
				Dictionary<State, HashSet<int>> dictionary = this.GetUnsatisfiedStates(resultTable, ioSpecs, colAttributes);
				if (!this.MoreSatisfied(unsatStates, dictionary))
				{
					return !includeRowSelector && this.AugmentUnionProgram(ref originalProg, newProg, ioSpecs, ref unsatStates, colAttributes, true);
				}
				HashSet<int> improvedCols = this.GetImprovedCols(unsatStates, dictionary);
				updatedColProgs = array.Select(delegate(resultSequence p, int k)
				{
					if (!improvedCols.Contains(k))
					{
						return p;
					}
					return updatedColProgs[k];
				}).ToArray<resultSequence>();
				originalProg = new resultTable?(this.BuildTableProgram(resultSequence, updatedColProgs));
				unsatStates = dictionary;
				return true;
			}
		}

		// Token: 0x060081D0 RID: 33232 RVA: 0x001B5DC4 File Offset: 0x001B3FC4
		private resultTable BuildTableProgram(resultSequence? rowProg, resultSequence[] colProgs)
		{
			columnSelectors columnSelectors = colProgs.AggregateSeedFunc(new Func<resultSequence, columnSelectors>(this._rules.SingleColumn), new Func<columnSelectors, resultSequence, columnSelectors>(this._rules.ColumnSequence));
			if (rowProg == null)
			{
				return this._rules.ExtractTable(columnSelectors);
			}
			return this._rules.ExtractRowBasedTable(columnSelectors, rowProg.Value);
		}

		// Token: 0x060081D1 RID: 33233 RVA: 0x001B5E24 File Offset: 0x001B4024
		private bool MoreSatisfied(Dictionary<State, HashSet<int>> oldStates, Dictionary<State, HashSet<int>> newStates)
		{
			if (newStates == null)
			{
				return false;
			}
			if (oldStates == null)
			{
				return true;
			}
			if (newStates.All((KeyValuePair<State, HashSet<int>> kvp) => oldStates.ContainsKey(kvp.Key) && oldStates[kvp.Key].IsSupersetOf(kvp.Value)))
			{
				return newStates.Sum((KeyValuePair<State, HashSet<int>> kvp) => kvp.Value.Count) < oldStates.Sum((KeyValuePair<State, HashSet<int>> kvp) => kvp.Value.Count);
			}
			return false;
		}

		// Token: 0x060081D2 RID: 33234 RVA: 0x001B5EB4 File Offset: 0x001B40B4
		private HashSet<int> GetImprovedCols(Dictionary<State, HashSet<int>> oldStates, Dictionary<State, HashSet<int>> newStates)
		{
			HashSet<int> hashSet = new HashSet<int>();
			foreach (KeyValuePair<State, HashSet<int>> keyValuePair in oldStates)
			{
				State state = keyValuePair.Key;
				HashSet<int> value = keyValuePair.Value;
				if (newStates.ContainsKey(state))
				{
					hashSet.AddRange(value.Where((int x) => !newStates[state].Contains(x)));
				}
				else
				{
					hashSet.AddRange(value);
				}
			}
			return hashSet;
		}

		// Token: 0x060081D3 RID: 33235 RVA: 0x001B5F74 File Offset: 0x001B4174
		private Optional<ProgramSet> LearnSinglePageExtractTableRule(SynthesisEngine engine, GrammarRule rule, LearningTask<Spec> task, CancellationToken cancel, out string[] columnAttributes)
		{
			TextTableSpec textTableSpec = task.Spec as TextTableSpec;
			columnAttributes = null;
			if (textTableSpec == null)
			{
				return Optional<ProgramSet>.Nothing;
			}
			columnAttributes = Enumerable.Repeat<string>(null, textTableSpec.ColumnSpecs.Length).ToArray<string>();
			TextSubsequenceSpec textSubsequenceSpec = textTableSpec.ColumnSpecs.FirstOrDefault<TextSubsequenceSpec>();
			State state = ((textSubsequenceSpec != null) ? textSubsequenceSpec.ProvidedInputs.FirstOrDefault<State>() : null);
			if (state == null)
			{
				throw new ArgumentException("No valid input state found in column specs");
			}
			List<List<string>> tableTextExamples = this.GetTableTextExamples(state, textTableSpec);
			this._engine = engine;
			this._learningTask = task;
			this._cancel = cancel;
			List<IDomNode[][]> list;
			ProgramSet programSet = this.LearnHtmlTablePrograms(state, tableTextExamples, out list);
			if (programSet != null && !programSet.IsEmpty)
			{
				return programSet.Some<ProgramSet>();
			}
			Tuple<Witnesses.ProgramState, IReadOnlyList<Witnesses.ProgramState>> tuple = null;
			if (this._options.PredictiveRowColumnSelectors != null)
			{
				this.InitializePredictiveCache(state);
				tuple = this.LearnPredictiveProgramFromCache(tableTextExamples);
				if (this.IsSingleColumnCommonLca(tuple, tableTextExamples))
				{
					return this.BuildTableProgramSet(tuple.Item1, tuple.Item2).Some<ProgramSet>();
				}
			}
			Dictionary<TableNodesMatch, Tuple<resultSequence?, List<resultSequence>, int>> dictionary = new Dictionary<TableNodesMatch, Tuple<resultSequence?, List<resultSequence>, int>>();
			Witnesses.PreviousProgramsMatch previousProgramMatches = this.GetPreviousProgramMatches(state, textTableSpec, tableTextExamples);
			if (previousProgramMatches == null && textTableSpec.IsSoft())
			{
				bool[] array = this.GeneralizedColumns(state, textTableSpec, tableTextExamples);
				TextTableSpec hardSpec = textTableSpec.GetHardSpec(array);
				if (hardSpec.ColumnSpecs.Count((TextSubsequenceSpec s) => s.IsSoft()) < textTableSpec.ColumnSpecs.Count((TextSubsequenceSpec s) => s.IsSoft()))
				{
					LearningTask<Spec> learningTask = task.Clone(rule.Head, hardSpec).Cast<Spec>();
					return this.LearnSinglePageExtractTableRule(engine, rule, learningTask, cancel, out columnAttributes);
				}
			}
			resultSequence? resultSequence = null;
			List<resultSequence> list2 = new List<resultSequence>();
			int num = -1;
			bool flag = true;
			bool flag2 = false;
			resultSequence? resultSequence2 = new resultSequence?(this._rules.EmptySequence());
			List<TableNodesMatch> list3 = null;
			list3 = TableNodeMatching.GetTableNodeMatches(base.Grammar, textTableSpec, this._options.TextComparer, out columnAttributes, this._options.PermittedNodeAttributes);
			foreach (TableNodesMatch tableNodesMatch in list3)
			{
				if (!tableNodesMatch.RowNodes.Any((KeyValuePair<int, IDomNode> kvp) => kvp.Value == null))
				{
					IDomNode[] array2 = null;
					if (previousProgramMatches != null && this.SatisfiesRow(previousProgramMatches.RowNodes, tableNodesMatch))
					{
						resultSequence = previousProgramMatches.RowProgram;
						array2 = previousProgramMatches.RowNodes;
					}
					else
					{
						WebRegion[] array3 = (from kvp in tableNodesMatch.RowNodes
							orderby kvp.Key
							select new WebRegion(kvp.Value)).ToArray<WebRegion>();
						SubsequenceSpec subsequenceSpec = new SubsequenceSpec(new Dictionary<State, IEnumerable<object>> { { state, array3 } });
						LearningTask learningTask2 = task.Clone(this._build.Symbol.resultSequence, subsequenceSpec);
						resultSequence = engine.Learn(learningTask2, cancel).RealizedPrograms.Select(new Func<ProgramNode, resultSequence>(this._build.Node.Cast.resultSequence)).FirstOrNull<resultSequence>();
						if (resultSequence == null)
						{
							continue;
						}
						array2 = this.ExecuteSequenceProgram(resultSequence.Value, state);
						if (!this.SatisfiesRow(array2, tableNodesMatch))
						{
							resultSequence = null;
							continue;
						}
					}
					num = array2.Length;
					this._extractionBoundaryNodeIndex = ((num > 1) ? DomNodeExt.LastUnderLCA(array2).Start : (-1));
					dictionary[tableNodesMatch] = Tuple.Create<resultSequence?, List<resultSequence>, int>(resultSequence, new List<resultSequence>(), this._extractionBoundaryNodeIndex);
					Dictionary<int, HashSet<IDomNode>> dictionary2 = tableNodesMatch.RowNodes.ToDictionary((KeyValuePair<int, IDomNode> kvp) => kvp.Key, (KeyValuePair<int, IDomNode> kvp) => kvp.Value.GetDescendants(true).ConvertToHashSet<IDomNode>());
					bool flag3 = true;
					foreach (KeyValuePair<int, Dictionary<int, IDomNode>> keyValuePair in tableNodesMatch.ColumnNodes.OrderBy((KeyValuePair<int, Dictionary<int, IDomNode>> kvp) => kvp.Key))
					{
						int key = keyValuePair.Key;
						Dictionary<int, IDomNode> value = keyValuePair.Value;
						WebRegion[] array4 = (from p in keyValuePair.Value
							orderby p.Key
							select new WebRegion(p.Value)).ToArray<WebRegion>();
						resultSequence? resultSequence3 = null;
						IDomNode[] array5 = null;
						if (array4.IsEmpty<WebRegion>())
						{
							resultSequence3 = resultSequence2;
						}
						else
						{
							if (previousProgramMatches != null && key < previousProgramMatches.ColumnProgramsNodes.Length)
							{
								foreach (KeyValuePair<resultSequence, IDomNode[]> keyValuePair2 in previousProgramMatches.ColumnProgramsNodes[key])
								{
									if (this.SatisfiesColumn(keyValuePair2.Value, value, dictionary2, array2))
									{
										resultSequence3 = new resultSequence?(keyValuePair2.Key);
										array5 = keyValuePair2.Value;
										break;
									}
								}
							}
							if (resultSequence3 == null)
							{
								SubsequenceSpec subsequenceSpec2 = new SubsequenceSpec(new Dictionary<State, IEnumerable<object>> { { state, array4 } });
								LearningTask learningTask3 = task.Clone(this._build.Symbol.resultSequence, subsequenceSpec2);
								resultSequence3 = engine.Learn(learningTask3, cancel).RealizedPrograms.Select(new Func<ProgramNode, resultSequence>(this._build.Node.Cast.resultSequence)).FirstOrNull<resultSequence>();
								if (resultSequence3 == null)
								{
									flag3 = false;
								}
								else
								{
									array5 = this.ExecuteSequenceProgram(resultSequence3.Value, state);
									if (!this.SatisfiesColumn(array5, value, dictionary2, array2))
									{
										flag3 = false;
									}
								}
							}
						}
						if (!flag3)
						{
							break;
						}
						if (tableNodesMatch.ColumnNodes.Count == 1 && array5 != null && this.RowSelectorIsSuperfluous(array5, value, dictionary2, array2))
						{
							resultSequence = null;
							dictionary[tableNodesMatch] = Tuple.Create<resultSequence?, List<resultSequence>, int>(null, new List<resultSequence>(), -1);
							Tuple<resultSequence?, IDomNode[]> tuple2 = this.InferHigherRankedProgramFromPredictiveCache(tableTextExamples, array5, state, engine, task, cancel);
							if (tuple2 != null)
							{
								resultSequence3 = tuple2.Item1;
								array5 = tuple2.Item2;
								num = array5.Length;
							}
						}
						list2.Add(resultSequence3.Value);
						dictionary[tableNodesMatch].Item2.Add(resultSequence3.Value);
					}
					if (flag3)
					{
						flag2 = true;
						break;
					}
					resultSequence = null;
					list2.Clear();
					num = -1;
				}
			}
			if (!flag2)
			{
				TableNodesMatch tableNodesMatch2 = (from kvp in dictionary
					orderby kvp.Value.Item2.Count descending
					select kvp.Key).FirstOrDefault<TableNodesMatch>() ?? ((list3 != null) ? list3.FirstOrDefault<TableNodesMatch>() : null);
				List<resultSequence> list4 = null;
				if (tableNodesMatch2 != null && dictionary.ContainsKey(tableNodesMatch2))
				{
					resultSequence = dictionary[tableNodesMatch2].Item1;
					list4 = dictionary[tableNodesMatch2].Item2;
					this._extractionBoundaryNodeIndex = dictionary[tableNodesMatch2].Item3;
				}
				IDomNode[] array6 = null;
				List<IDomNode[]> list5 = new List<IDomNode[]>();
				for (int i = 0; i < textTableSpec.ColumnSpecs.Length; i++)
				{
					string text = columnAttributes[i];
					resultSequence? resultSequence4 = null;
					IDomNode[] array7 = null;
					if (list4 != null && i < list4.Count)
					{
						resultSequence4 = new resultSequence?(list4[i]);
					}
					else
					{
						TextSubsequenceSpec textSubsequenceSpec2 = textTableSpec.ColumnSpecs[i];
						LearningTask<TextSubsequenceSpec> learningTask4 = task.Clone(this._build.Symbol.resultSequence, textSubsequenceSpec2).Cast<TextSubsequenceSpec>();
						if (tableNodesMatch2 != null)
						{
							Dictionary<int, IDomNode> colExamples = tableNodesMatch2.ColumnNodes[i];
							if (previousProgramMatches != null && i < previousProgramMatches.ColumnProgramsNodes.Length)
							{
								resultSequence4 = (from kvp1 in previousProgramMatches.ColumnProgramsNodes[i]
									where this.SatisfiesColumn(kvp1.Value, colExamples, null, null)
									select kvp1.Key).FirstOrNull<resultSequence>();
							}
							if (resultSequence4 == null)
							{
								WebRegion[] array8 = (from p in colExamples
									orderby p.Key
									select new WebRegion(p.Value)).ToArray<WebRegion>();
								SubsequenceSpec subsequenceSpec3 = new SubsequenceSpec(new Dictionary<State, IEnumerable<object>> { { state, array8 } });
								LearningTask learningTask5 = task.Clone(this._build.Symbol.resultSequence, subsequenceSpec3);
								resultSequence4 = engine.Learn(learningTask5, cancel).RealizedPrograms.Select(new Func<ProgramNode, resultSequence>(this._build.Node.Cast.resultSequence)).FirstOrNull<resultSequence>();
								if (!this.SatisfiesPrefixConstraint(resultSequence4, learningTask4, text))
								{
									resultSequence4 = null;
								}
							}
						}
						if (resultSequence4 == null)
						{
							ProgramSetBuilder<resultSequence> programSetBuilder = this.LearnTextBasedSequencePrograms(engine, rule, learningTask4, cancel, text, null);
							resultSequence4 = ((programSetBuilder != null) ? programSetBuilder.Set.RealizedPrograms.Select(new Func<ProgramNode, resultSequence>(this._build.Node.Cast.resultSequence)).FirstOrNull<resultSequence>() : null);
							array7 = ((resultSequence4 == null) ? null : this.ExecuteSequenceProgram(resultSequence4.Value, state));
							if (array7 != null && array7.Length > 1)
							{
								IDomNode domNode = DomNodeExt.LastUnderLCA(array7);
								int num2 = ((domNode != null) ? domNode.Start : (-1));
								if (num2 > this._extractionBoundaryNodeIndex)
								{
									this._extractionBoundaryNodeIndex = num2;
								}
							}
						}
					}
					if (resultSequence4 == null)
					{
						resultSequence4 = resultSequence2;
					}
					if (array7 == null)
					{
						array7 = ((resultSequence4 == null) ? null : this.ExecuteSequenceProgram(resultSequence4.Value, state));
					}
					Tuple<resultSequence?, IDomNode[]> tuple3 = this.InferHigherRankedProgramFromPredictiveCache(tableTextExamples, array7, state, engine, task, cancel);
					if (tuple3 != null)
					{
						resultSequence4 = tuple3.Item1;
						array7 = tuple3.Item2;
					}
					list2.Add(resultSequence4.Value);
					list5.Add(array7);
				}
				if (resultSequence != null)
				{
					array6 = this.ExecuteSequenceProgram(resultSequence.Value, state);
				}
				IReadOnlyList<IDomNode[]> readOnlyList = list5;
				flag = this.InferRowColumnAlignment(ref resultSequence, ref list2, ref num, ref array6, ref readOnlyList, tableTextExamples, columnAttributes, state);
			}
			HashSet<int> previouslySatisfiedUnchangedColumns = this.GetPreviouslySatisfiedUnchangedColumns(tableTextExamples);
			if (tuple != null)
			{
				Witnesses.ProgramState item = tuple.Item1;
				resultSequence? resultSequence5 = ((item != null) ? new resultSequence?(item.Program) : null);
				List<resultSequence> list6 = tuple.Item2.Select(delegate(Witnesses.ProgramState s)
				{
					if (s == null)
					{
						return this._rules.EmptySequence();
					}
					return s.Program;
				}).ToList<resultSequence>();
				Witnesses.ProgProperties progProperties = this.CheckPredFallback(resultSequence, list2, resultSequence5, list6, previouslySatisfiedUnchangedColumns, flag, tableTextExamples, state, columnAttributes);
				if (progProperties != null)
				{
					resultSequence = progProperties.RowProg;
					list2 = progProperties.ColProgs;
					num = progProperties.NumRows;
				}
			}
			if (previouslySatisfiedUnchangedColumns != null && list2.Count == tableTextExamples.Count)
			{
				this.CheckPrevProgramFallback(ref resultSequence, ref list2, previouslySatisfiedUnchangedColumns, previousProgramMatches, state, tableTextExamples, columnAttributes);
			}
			if (!list2.IsEmpty<resultSequence>() && !list2.All((resultSequence p) => p.Is_EmptySequence(this._build)))
			{
				columnSelectors columnSelectors = list2.AggregateSeedFunc(new Func<resultSequence, columnSelectors>(this._rules.SingleColumn), new Func<columnSelectors, resultSequence, columnSelectors>(this._rules.ColumnSequence));
				resultTable resultTable;
				if (resultSequence == null)
				{
					if (list2.Count > 1)
					{
						return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
					}
					resultTable = this._rules.ExtractTable(columnSelectors);
				}
				else
				{
					resultTable = this._rules.ExtractRowBasedTable(columnSelectors, resultSequence.Value);
				}
				resultTable.Node.Metadata = new ProgramMetadata(null, TableKind.ByExample, num, columnAttributes);
				return ProgramSetBuilder.List<resultTable>(this._build.Symbol.resultTable, new resultTable[] { resultTable }).Set.Some<ProgramSet>();
			}
			if (textTableSpec.IsSoft())
			{
				LearningTask<Spec> learningTask6 = task.Clone(rule.Head, textTableSpec.GetHardSpec(null)).Cast<Spec>();
				return this.LearnSinglePageExtractTableRule(engine, rule, learningTask6, cancel, out columnAttributes);
			}
			return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
		}

		// Token: 0x060081D4 RID: 33236 RVA: 0x001B6C38 File Offset: 0x001B4E38
		private void CheckPrevProgramFallback(ref resultSequence? rowProgram, ref List<resultSequence> columnPrograms, HashSet<int> prevSatCols, Witnesses.PreviousProgramsMatch prevMatch, State inpState, List<List<string>> textTableExamples, string[] columnAttributes)
		{
			if (this._options.PreviouslyLearntColumnSelectors == null)
			{
				return;
			}
			if (columnPrograms.Any((resultSequence p, int k) => p.Is_EmptySequence(this._build) && prevSatCols.Contains(k)) || columnPrograms.Where((resultSequence p, int k) => !prevSatCols.Contains(k)).All((resultSequence p) => p.Is_EmptySequence(this._build)))
			{
				rowProgram = this._options.PreviouslyLearntRowSelector;
				if (rowProgram == null && this._options.PreviouslyLearntColumnSelectors.Length == 1 && columnPrograms.Count > 1)
				{
					rowProgram = new resultSequence?(this._options.PreviouslyLearntColumnSelectors[0]);
				}
				for (int j = 0; j < columnPrograms.Count; j++)
				{
					columnPrograms[j] = (prevSatCols.Contains(j) ? this._options.PreviouslyLearntColumnSelectors[j] : this._rules.EmptySequence());
				}
				return;
			}
			if (prevMatch != null && columnPrograms.Count > 1)
			{
				IReadOnlyList<IDomNode[]> readOnlyList = columnPrograms.Select((resultSequence c) => this.ExecuteSequenceProgram(c, inpState)).ToArray<IDomNode[]>();
				Dictionary<int, resultSequence> prevFallBackPrograms = new Dictionary<int, resultSequence>();
				Dictionary<int, IDomNode[]> prevFallBackResults = new Dictionary<int, IDomNode[]>();
				int num = 0;
				while (num < columnPrograms.Count && num < this._options.PreviouslyLearntColumnSelectors.Length)
				{
					if (prevSatCols.Contains(num))
					{
						resultSequence resultSequence = this._options.PreviouslyLearntColumnSelectors[num];
						if (prevMatch.ColumnProgramsNodes.Length > num)
						{
							IDomNode[] array;
							prevMatch.ColumnProgramsNodes[num].TryGetValue(resultSequence, out array);
							if (array != null && array.Length != readOnlyList[num].Length)
							{
								prevFallBackPrograms[num] = resultSequence;
								prevFallBackResults[num] = array;
							}
						}
					}
					num++;
				}
				List<Tuple<resultSequence?, IDomNode[]>> list = new List<Tuple<resultSequence?, IDomNode[]>> { Tuple.Create<resultSequence?, IDomNode[]>(prevMatch.RowProgram, prevMatch.RowNodes) };
				IDomNode[] array2 = ((rowProgram == null) ? null : this.ExecuteSequenceProgram(rowProgram.Value, inpState));
				IDomNode[] rowNodes = prevMatch.RowNodes;
				int? num2 = ((rowNodes != null) ? new int?(rowNodes.Length) : null);
				int? num3 = ((array2 != null) ? new int?(array2.Length) : null);
				if (!((num2.GetValueOrDefault() == num3.GetValueOrDefault()) & (num2 != null == (num3 != null))))
				{
					list.Add(Tuple.Create<resultSequence?, IDomNode[]>(rowProgram, array2));
				}
				if (prevFallBackPrograms.Any<KeyValuePair<int, resultSequence>>() || list.Count > 1)
				{
					List<resultSequence> list2 = columnPrograms.Select(delegate(resultSequence c, int i)
					{
						if (!prevFallBackPrograms.ContainsKey(i))
						{
							return c;
						}
						return prevFallBackPrograms[i];
					}).ToList<resultSequence>();
					IReadOnlyList<IDomNode[]> readOnlyList2 = readOnlyList.Select(delegate(IDomNode[] n, int i)
					{
						if (!prevFallBackResults.ContainsKey(i))
						{
							return n;
						}
						return prevFallBackResults[i];
					}).ToList<IDomNode[]>();
					foreach (Tuple<resultSequence?, IDomNode[]> tuple in list)
					{
						resultSequence? item = tuple.Item1;
						IDomNode[] item2 = tuple.Item2;
						int num4 = ((item2 != null) ? item2.Length : readOnlyList.FirstOrDefault<IDomNode[]>().Length);
						if (this.InferRowColumnAlignment(ref item, ref list2, ref num4, ref item2, ref readOnlyList2, textTableExamples, columnAttributes, inpState))
						{
							rowProgram = item;
							columnPrograms = list2;
							break;
						}
					}
				}
			}
		}

		// Token: 0x060081D5 RID: 33237 RVA: 0x001B6FB8 File Offset: 0x001B51B8
		private IEnumerable<resultSequence> GetColumnSelectorNodes(resultTable tableProgramNode)
		{
			columnSelectors columnSelectors = tableProgramNode.Switch<columnSelectors>(this._build, (ExtractTable extractTable) => extractTable.columnSelectors, (ExtractRowBasedTable extractRowBasedTable) => extractRowBasedTable.columnSelectors);
			return this.GetColumnSelectorNodes(columnSelectors);
		}

		// Token: 0x060081D6 RID: 33238 RVA: 0x001B7018 File Offset: 0x001B5218
		private IEnumerable<resultSequence> GetColumnSelectorNodes(columnSelectors colSelectorsNode)
		{
			return colSelectorsNode.Switch<IEnumerable<resultSequence>>(this._build, (SingleColumn singleCol) => new resultSequence[] { singleCol.resultSequence }, (ColumnSequence colSeq) => this.GetColumnSelectorNodes(colSeq.columnSelectors).AppendItem(colSeq.resultSequence));
		}

		// Token: 0x060081D7 RID: 33239 RVA: 0x001B7054 File Offset: 0x001B5254
		private resultSequence? GetRowSelectorNode(resultTable tableProgramNode)
		{
			return tableProgramNode.Switch<resultSequence?>(this._build, (ExtractTable extractTable) => null, (ExtractRowBasedTable extractRowBasedTable) => new resultSequence?(extractRowBasedTable.resultSequence));
		}

		// Token: 0x060081D8 RID: 33240 RVA: 0x001B70AC File Offset: 0x001B52AC
		private Witnesses.ProgProperties GetColCompletion(resultSequence? rowProg, List<resultSequence> colProgs, bool allColsSatisfied, List<resultSequence> otherColProgs, List<List<string>> textTableExamples, State inpState, string[] columnAttributes)
		{
			resultSequence? resultSequence = rowProg;
			IDomNode[] array = ((resultSequence == null) ? null : this.ExecuteSequenceProgram(resultSequence.Value, inpState));
			List<resultSequence> list = ((otherColProgs.Count == colProgs.Count) ? colProgs.Select(delegate(resultSequence c, int i)
			{
				if (!c.Is_EmptySequence(this._build))
				{
					return c;
				}
				return otherColProgs[i];
			}).ToList<resultSequence>() : colProgs.ToList<resultSequence>());
			IReadOnlyList<IDomNode[]> readOnlyList = list.Select((resultSequence c) => this.ExecuteSequenceProgram(c, inpState)).ToArray<IDomNode[]>();
			int num = ((array != null) ? array.Length : readOnlyList.FirstOrDefault<IDomNode[]>().Length);
			bool flag;
			if (colProgs.All((resultSequence c) => !c.Is_EmptySequence(this._build)))
			{
				flag = allColsSatisfied;
				if (array != null)
				{
					readOnlyList = Microsoft.ProgramSynthesis.Extraction.Web.Semantics.Semantics.GetBoundaryBasedRowAlignment(readOnlyList, array);
				}
			}
			else
			{
				flag = this.InferRowColumnAlignment(ref resultSequence, ref list, ref num, ref array, ref readOnlyList, textTableExamples, columnAttributes, inpState);
			}
			return new Witnesses.ProgProperties(resultSequence, list, num, flag, readOnlyList);
		}

		// Token: 0x060081D9 RID: 33241 RVA: 0x001B71A8 File Offset: 0x001B53A8
		private Witnesses.ProgProperties CheckPredFallback(resultSequence? byExRowProg, List<resultSequence> byExColProgs, resultSequence? predRowProg, List<resultSequence> predColProgs, HashSet<int> prevSatCols, bool allByExColumnsSatisfiedPrecisely, List<List<string>> textTableExamples, State inpState, string[] columnAttributes)
		{
			int count = textTableExamples.Count;
			if (count == 0)
			{
				return null;
			}
			List<Witnesses.ProgProperties> list = new List<Witnesses.ProgProperties>();
			Witnesses.ProgProperties colCompletion = this.GetColCompletion(predRowProg, predColProgs, true, byExColProgs, textTableExamples, inpState, columnAttributes);
			list.Add(colCompletion);
			if (byExColProgs.Count == count)
			{
				Witnesses.ProgProperties colCompletion2 = this.GetColCompletion(byExRowProg, byExColProgs, allByExColumnsSatisfiedPrecisely, predColProgs, textTableExamples, inpState, columnAttributes);
				list.Add(colCompletion2);
			}
			Func<resultSequence, bool> <>9__6;
			return list.Where((Witnesses.ProgProperties p) => (p.RowProg != null || p.NumCols == 1) && (prevSatCols == null || prevSatCols.All((int k) => k < p.NumCols && !p.ColProgs[k].Is_EmptySequence(this._build)))).OrderBy(delegate(Witnesses.ProgProperties p)
			{
				IEnumerable<resultSequence> colProgs = p.ColProgs;
				Func<resultSequence, bool> func;
				if ((func = <>9__6) == null)
				{
					func = (<>9__6 = (resultSequence c) => c.Is_EmptySequence(this._build));
				}
				return colProgs.Count(func);
			}).ThenBy((Witnesses.ProgProperties p) => (!p.ColumnExPreciselySatisfied) ? 1 : 0)
				.ThenBy((Witnesses.ProgProperties p) => p.NumRows)
				.ThenBy((Witnesses.ProgProperties p) => p.NumNonEmptyNodes)
				.FirstOrDefault<Witnesses.ProgProperties>();
		}

		// Token: 0x060081DA RID: 33242 RVA: 0x001B72B0 File Offset: 0x001B54B0
		private ProgramSetBuilder<resultSequence> LearnTextBasedSequencePrograms(SynthesisEngine engine, GrammarRule rule, LearningTask<TextSubsequenceSpec> task, CancellationToken cancel, string attribute, int? maxExamples = null)
		{
			int i;
			List<Tuple<State, string, int>> list = (from e in task.Spec.PositiveExamples.SelectMany((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.Take(maxExamples ?? kvp.Value.Count<object>()).Select((object v, int i) => Tuple.Create<State, string, int>(kvp.Key, v as string, task.Spec.NodeIndexes[kvp.Key][i])))
				where e.Item3 != -1 || !string.IsNullOrWhiteSpace(e.Item2)
				select e).ToList<Tuple<State, string, int>>();
			List<HashSet<IDomNode>> matchingNodeSets = NodeTextMatching.GetMatchingNodeSets(base.Grammar, list, this._options.TextComparer, attribute, false);
			if (matchingNodeSets != null)
			{
				if (!matchingNodeSets.Any((HashSet<IDomNode> s) => s.IsEmpty<IDomNode>()))
				{
					int count = task.Spec.PositiveExamples.Count;
					int uniformAveragePerElement = NodeTextMatching.GetUniformAveragePerElement(10, count);
					IGrouping<State, Record<Tuple<State, string, int>, HashSet<IDomNode>>>[] array = (from z in list.ZipWith(matchingNodeSets)
						group z by z.Item1.Item1).ToArray<IGrouping<State, Record<Tuple<State, string, int>, HashSet<IDomNode>>>>();
					List<List<KeyValuePair<State, IEnumerable<object>>>> list2 = new List<List<KeyValuePair<State, IEnumerable<object>>>>();
					IGrouping<State, Record<Tuple<State, string, int>, HashSet<IDomNode>>>[] array2 = array;
					for (i = 0; i < array2.Length; i++)
					{
						IGrouping<State, Record<Tuple<State, string, int>, HashSet<IDomNode>>> grouping = array2[i];
						State state = grouping.Key;
						List<HashSet<IDomNode>> nodeSets = grouping.Select((Record<Tuple<State, string, int>, HashSet<IDomNode>> t) => t.Item2).ToList<HashSet<IDomNode>>();
						NodeSequence[] array3 = NodeTextMatching.GetMatchingNodeSequenceCombinations(nodeSets).Take(uniformAveragePerElement).ToArray<NodeSequence>();
						List<Witnesses.AlignmentGroup> predictiveLearningCache = this._predictiveLearningCache;
						List<HashSet<IDomNode>> list3;
						if (predictiveLearningCache == null)
						{
							list3 = null;
						}
						else
						{
							list3 = (from a in predictiveLearningCache
								select a.RowState.Result into r
								where r.Length >= nodeSets.Count
								select nodeSets.Select((HashSet<IDomNode> s, int i) => s.Where((IDomNode n) => r[i].Contains(n)).ConvertToHashSet<IDomNode>()).ToList<HashSet<IDomNode>>()).FirstOrDefault((List<HashSet<IDomNode>> ns) => ns.All((HashSet<IDomNode> s) => s.Any<IDomNode>()));
						}
						List<HashSet<IDomNode>> list4 = list3;
						if (list4 != null)
						{
							array3 = NodeTextMatching.GetMatchingNodeSequenceCombinations(list4).Concat(array3).Take(uniformAveragePerElement)
								.ToArray<NodeSequence>();
						}
						List<KeyValuePair<State, IEnumerable<object>>> list5 = array3.Select((NodeSequence c) => new KeyValuePair<State, IEnumerable<object>>(state, c.Nodes.Select((IDomNode n) => new WebRegion(n)))).ToList<KeyValuePair<State, IEnumerable<object>>>();
						if (list5.IsEmpty<KeyValuePair<State, IEnumerable<object>>>())
						{
							return null;
						}
						list2.Add(list5);
					}
					IEnumerable<SubsequenceSpec> enumerable = from c in list2.CartesianProduct<KeyValuePair<State, IEnumerable<object>>>().Take(10)
						select new SubsequenceSpec(c);
					bool flag;
					ProgramSetBuilder<resultSequence> programSetBuilder = this.LearnBestSpecSatisfyingProgramSet(enumerable, true, engine, task, cancel, out flag, attribute);
					if (!flag)
					{
						int num = task.Spec.PositiveExamples.Max((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.Count<object>());
						if (maxExamples == null && num > 5)
						{
							int num2 = Math.Min(10, num);
							for (int j = 3; j <= num2; j++)
							{
								ProgramSetBuilder<resultSequence> programSetBuilder2 = this.LearnTextBasedSequencePrograms(engine, rule, task, cancel, attribute, new int?(j));
								if (programSetBuilder2 != null && !programSetBuilder2.Set.IsEmpty && this.SatisfiesPrefixConstraint(programSetBuilder2, task, attribute))
								{
									return ProgramSetBuilder.List<resultSequence>(this._build.Symbol.resultSequence, new resultSequence[] { this._build.Node.Cast.resultSequence(programSetBuilder2.Set.RealizedPrograms.First<ProgramNode>()) });
								}
							}
						}
					}
					return programSetBuilder;
				}
			}
			return null;
		}

		// Token: 0x060081DB RID: 33243 RVA: 0x001B7658 File Offset: 0x001B5858
		private ProgramSetBuilder<resultSequence> LearnBestSpecSatisfyingProgramSet(IEnumerable<SubsequenceSpec> specs, bool preferPrefix, SynthesisEngine engine, LearningTask<TextSubsequenceSpec> task, CancellationToken cancel, out bool foundPrefix, string attribute)
		{
			foundPrefix = false;
			List<ProgramSetBuilder<resultSequence>> list = new List<ProgramSetBuilder<resultSequence>>();
			foreach (SubsequenceSpec subsequenceSpec in specs)
			{
				LearningTask learningTask = task.Clone(this._build.Symbol.resultSequence, subsequenceSpec);
				ProgramSetBuilder<resultSequence> programSetBuilder = this._build.Set.Cast.resultSequence(engine.Learn(learningTask, cancel));
				if (!ProgramSetBuilder.IsNullOrEmpty<resultSequence>(programSetBuilder))
				{
					if (!preferPrefix)
					{
						return programSetBuilder;
					}
					if (this.SatisfiesPrefixConstraint(programSetBuilder, task, attribute))
					{
						foundPrefix = true;
						return programSetBuilder;
					}
					list.Add(programSetBuilder);
					if (list.Count > 4)
					{
						return list[0];
					}
				}
			}
			return list.FirstOrDefault<ProgramSetBuilder<resultSequence>>();
		}

		// Token: 0x060081DC RID: 33244 RVA: 0x001B7734 File Offset: 0x001B5934
		private bool SatisfiesPrefixConstraint(ProgramSetBuilder<resultSequence> s, LearningTask<TextSubsequenceSpec> task, string attribute)
		{
			resultSequence? resultSequence = s.Set.RealizedPrograms.Select(new Func<ProgramNode, resultSequence>(this._build.Node.Cast.resultSequence)).FirstOrNull<resultSequence>();
			return this.SatisfiesPrefixConstraint(resultSequence, task, attribute);
		}

		// Token: 0x060081DD RID: 33245 RVA: 0x001B777C File Offset: 0x001B597C
		private bool SatisfiesPrefixConstraint(resultSequence? p, LearningTask<TextSubsequenceSpec> task, string attribute)
		{
			if (p == null)
			{
				return false;
			}
			ProgramNode node = p.Value.Node;
			Func<WebRegion, string> <>9__0;
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in task.Spec.PositiveExamples)
			{
				State key = keyValuePair.Key;
				string[] array = (keyValuePair.Value as string[]) ?? keyValuePair.Value.Cast<string>().ToArray<string>();
				object obj = node.Invoke(key);
				string[] array2;
				if (obj == null)
				{
					array2 = null;
				}
				else
				{
					IEnumerable<WebRegion> enumerable = obj.ToEnumerable<object>().Cast<WebRegion>();
					Func<WebRegion, string> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = delegate(WebRegion r)
						{
							if (attribute != null)
							{
								return HtmlDoc.NormalizeText(r.BeginNode.GetAttribute(attribute));
							}
							return r.NonWhitespaceText();
						});
					}
					array2 = enumerable.Select(func).ToArray<string>();
				}
				string[] array3 = array2;
				IEnumerable<string> enumerable2 = array3;
				Func<string, bool> func2;
				if ((func2 = Witnesses.<>O.<3>__IsNullOrEmpty) == null)
				{
					func2 = (Witnesses.<>O.<3>__IsNullOrEmpty = new Func<string, bool>(string.IsNullOrEmpty));
				}
				if (!enumerable2.Any(func2))
				{
					array = array.Where((string s) => !string.IsNullOrEmpty(s)).ToArray<string>();
				}
				if (array3 == null || array3.Length < array.Length)
				{
					return false;
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (!this._options.TextComparer.Equals(HtmlDoc.NormalizeText(array[i]), array3[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060081DE RID: 33246 RVA: 0x001B7918 File Offset: 0x001B5B18
		private int GetMaxExampleSatisfactionOffset(IDomNode[] resultNodes, List<string> examples, string attribute)
		{
			Witnesses.<>c__DisplayClass118_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass118_0();
			CS$<>8__locals1.attribute = attribute;
			CS$<>8__locals1.<>4__this = this;
			if (resultNodes == null)
			{
				return -1;
			}
			int num = -1;
			string[] array = resultNodes.Select(delegate(IDomNode n)
			{
				if (CS$<>8__locals1.attribute != null)
				{
					if (n == null)
					{
						return null;
					}
					return n.GetAttribute(CS$<>8__locals1.attribute);
				}
				else
				{
					if (n == null)
					{
						return null;
					}
					return n.NormalizedInnerText;
				}
			}).ToArray<string>();
			int i;
			int j;
			for (i = 0; i < examples.Count; i = j + 1)
			{
				string exampleString = HtmlDoc.NormalizeText(examples[i]);
				if (!string.IsNullOrEmpty(exampleString))
				{
					int[] array2 = (from k in array.Select(delegate(string s, int k)
						{
							if (!CS$<>8__locals1.<>4__this._options.TextComparer.Equals(exampleString, s))
							{
								return -1;
							}
							return Math.Abs(k - i);
						})
						where k >= 0
						select k).ToArray<int>();
					if (array2.IsEmpty<int>())
					{
						return -1;
					}
					int num2 = array2.Min();
					if (num2 > num)
					{
						num = num2;
					}
				}
				j = i;
			}
			return num;
		}

		// Token: 0x060081DF RID: 33247 RVA: 0x001B7A2C File Offset: 0x001B5C2C
		[RuleLearner("TrimmedTextField")]
		public Optional<ProgramSet> LearnTrimmedTextFieldRule(SynthesisEngine engine, GrammarRule rule, LearningTask<FieldExtractionSpec> task, CancellationToken cancel)
		{
			List<Tuple<State, string, int>> list = new List<Tuple<State, string, int>>();
			foreach (KeyValuePair<State, object> keyValuePair in task.Spec.Examples)
			{
				string[] array = keyValuePair.Value as string[];
				int[] array2 = task.Spec.NodeIndexes[keyValuePair.Key];
				if (array == null || array.Length != 1 || array2 == null || array2.Length != 1)
				{
					return OptionalUtils.Some((T)null);
				}
				list.Add(Tuple.Create<State, string, int>(keyValuePair.Key, array[0], array2[0]));
			}
			List<HashSet<IDomNode>> matchingNodeSets = NodeTextMatching.GetMatchingNodeSets(base.Grammar, list, this._options.TextComparer, null, false);
			if (matchingNodeSets != null)
			{
				if (!matchingNodeSets.Any((HashSet<IDomNode> s) => s.IsEmpty<IDomNode>()))
				{
					IEnumerable<IEnumerable<IDomNode>> matchingNodeCombinations = NodeTextMatching.GetMatchingNodeCombinations(matchingNodeSets);
					KeyValuePair<State, object>[] array3 = task.Spec.Examples.ToArray<KeyValuePair<State, object>>();
					foreach (IEnumerable<IDomNode> enumerable in matchingNodeCombinations)
					{
						ExampleWithNegativesSpec exampleWithNegativesSpec = new ExampleWithNegativesSpec(enumerable.ZipWith(array3).ToDictionary((Record<IDomNode, KeyValuePair<State, object>> t) => t.Item2.Key, (Record<IDomNode, KeyValuePair<State, object>> t) => new Record<object, IEnumerable<object>>(new WebRegion(t.Item1), new List<object>())));
						LearningTask learningTask = task.Clone(this._build.Symbol.resultRegion, exampleWithNegativesSpec);
						ProgramSetBuilder<resultRegion> programSetBuilder = this._build.Set.Cast.resultRegion(engine.Learn(learningTask, cancel));
						if (!ProgramSetBuilder.IsNullOrEmpty<resultRegion>(programSetBuilder))
						{
							return this._build.Set.Join.TrimmedTextField(programSetBuilder).Set.Some<ProgramSet>();
						}
					}
					return OptionalUtils.Some((T)null);
				}
			}
			return OptionalUtils.Some((T)null);
		}

		// Token: 0x060081E0 RID: 33248 RVA: 0x001B7C40 File Offset: 0x001B5E40
		private ProgramSetBuilder<substringDisj> BuildDisjSubstring(substring[] substringPrograms)
		{
			if (substringPrograms.Length == 0)
			{
				return ProgramSetBuilder.Empty<substringDisj>(this._build.Symbol.substringDisj);
			}
			substringDisj substringDisj = substringPrograms.AggregateSeedFunc(new Func<substring, substringDisj>(this._rules.SingleSubstring), new Func<substringDisj, substring, substringDisj>(this._rules.DisjSubstring));
			return ProgramSetBuilder.List<substringDisj>(new substringDisj[] { substringDisj });
		}

		// Token: 0x060081E1 RID: 33249 RVA: 0x001B7CA4 File Offset: 0x001B5EA4
		private Tuple<ProgramSetBuilder<singletonField>, IEnumerable<IDomNode>> LearnSubstringFieldPrograms(SynthesisEngine engine, LearningTask<FieldExtractionSpec> task, KeyValuePair<State, object>[] examples, List<HashSet<IDomNode>> matchingNodeSets, CancellationToken cancel)
		{
			foreach (IEnumerable<IDomNode> enumerable in NodeTextMatching.GetMatchingNodeCombinations(matchingNodeSets))
			{
				ExampleWithNegativesSpec exampleWithNegativesSpec = new ExampleWithNegativesSpec(enumerable.ZipWith(examples).ToDictionary((Record<IDomNode, KeyValuePair<State, object>> t) => t.Item2.Key, (Record<IDomNode, KeyValuePair<State, object>> t) => new Record<object, IEnumerable<object>>(new WebRegion(t.Item1), new List<object>())));
				LearningTask learningTask = task.Clone(this._build.Symbol.resultRegion, exampleWithNegativesSpec);
				ProgramSetBuilder<resultRegion> programSetBuilder = this._build.Set.Cast.resultRegion(engine.Learn(learningTask, cancel));
				if (!ProgramSetBuilder.IsNullOrEmpty<resultRegion>(programSetBuilder))
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					Dictionary<State, object> dictionary2 = new Dictionary<State, object>();
					foreach (Record<KeyValuePair<State, object>, IDomNode> record in examples.ZipWith(enumerable))
					{
						ValueSubstring valueSubstring = ValueSubstring.Create(record.Item2.InnerText, null, null, null, null);
						KeyValuePair<State, object> keyValuePair = record.Item1;
						State state = keyValuePair.Key.Bind(this._build.Symbol.cs, valueSubstring).Bind(Language.Grammar.InputSymbol, new InputRow(new object[] { valueSubstring.Value }));
						keyValuePair = record.Item1;
						ValueSubstring valueSubstring2 = ValueSubstring.Create(((string[])keyValuePair.Value)[0], null, null, null, null);
						dictionary2[state] = valueSubstring2;
						dictionary[valueSubstring.Value] = valueSubstring2.Value;
					}
					LearningTask learningTask2 = task.Clone(this._build.Symbol.substring, new ExampleSpec(dictionary2)).WithTopKPrograms(new int?(10));
					ProgramSet programSet = engine.Learn(learningTask2, cancel);
					if (!ProgramSet.IsNullOrEmpty(programSet))
					{
						substring[] array = programSet.RealizedPrograms.Take(10).Select(new Func<ProgramNode, substring>(this._build.Node.Cast.substring)).ToArray<substring>();
						ProgramSetBuilder<substringDisj> programSetBuilder2 = this.BuildDisjSubstring(array);
						ProgramSetBuilder<y> valueSubstring3 = this._build.Set.Join.GetValueSubstring(programSetBuilder);
						Tuple<string[], int[]> tuple = this.LearnFeatureVectors(dictionary);
						ProgramSetBuilder<substringFeatureNames> programSetBuilder3 = ProgramSetBuilder.List<substringFeatureNames>(new substringFeatureNames[] { this._rules.substringFeatureNames(tuple.Item1) });
						ProgramSetBuilder<substringFeatureValues> programSetBuilder4 = ProgramSetBuilder.List<substringFeatureValues>(new substringFeatureValues[] { this._rules.substringFeatureValues(tuple.Item2) });
						ProgramSetBuilder<selectSubstring> programSetBuilder5 = this._build.Set.Join.SelectSubstring(programSetBuilder2, programSetBuilder3, programSetBuilder4);
						ProgramSetBuilder<fieldSubstring> programSetBuilder6 = this._build.Set.Join.LetSubstring(valueSubstring3, programSetBuilder5);
						return Tuple.Create<ProgramSetBuilder<singletonField>, IEnumerable<IDomNode>>(this._build.Set.Join.SubstringField(programSetBuilder6), enumerable);
					}
				}
			}
			return null;
		}

		// Token: 0x060081E2 RID: 33250 RVA: 0x001B8008 File Offset: 0x001B6208
		private Tuple<string[], int[]> LearnFeatureVectors(Dictionary<string, string> ioExamples)
		{
			Witnesses.<>c__DisplayClass122_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass122_0();
			List<string> list = new List<string> { "IsNull" };
			List<int> list2 = new List<int>();
			list2.Add((ioExamples.All((KeyValuePair<string, string> kvp) => kvp.Value == null) > false) ? 1 : 0);
			Witnesses.<>c__DisplayClass122_0 CS$<>8__locals2 = CS$<>8__locals1;
			string value = ioExamples.First<KeyValuePair<string, string>>().Value;
			CS$<>8__locals2.numChars = ((value != null) ? value.Length : 0);
			if (ioExamples.All(delegate(KeyValuePair<string, string> kvp)
			{
				string value3 = kvp.Value;
				return value3 != null && value3.Length == CS$<>8__locals1.numChars;
			}))
			{
				list.Add("NumChars");
				list2.Add(CS$<>8__locals1.numChars);
			}
			Witnesses.<>c__DisplayClass122_0 CS$<>8__locals3 = CS$<>8__locals1;
			string value2 = ioExamples.First<KeyValuePair<string, string>>().Value;
			int num;
			if (value2 == null)
			{
				num = 0;
			}
			else
			{
				num = value2.Count((char c) => !char.IsWhiteSpace(c));
			}
			CS$<>8__locals3.numNonWSChars = num;
			if (ioExamples.All(delegate(KeyValuePair<string, string> kvp)
			{
				string value4 = kvp.Value;
				if (value4 == null)
				{
					return false;
				}
				return value4.Count((char c) => !char.IsWhiteSpace(c)) == CS$<>8__locals1.numNonWSChars;
			}))
			{
				list.Add("NumNonWSChars");
				list2.Add(CS$<>8__locals1.numNonWSChars);
			}
			return new Tuple<string[], int[]>(list.ToArray(), list2.ToArray());
		}

		// Token: 0x060081E3 RID: 33251 RVA: 0x001B8128 File Offset: 0x001B6328
		[RuleLearner("SubstringField")]
		internal Optional<ProgramSet> LearnSelectSubstringFieldRule(SynthesisEngine engine, GrammarRule rule, LearningTask<FieldExtractionSpec> task, CancellationToken cancel)
		{
			List<Tuple<State, string, int>> list = new List<Tuple<State, string, int>>();
			foreach (KeyValuePair<State, object> keyValuePair in task.Spec.Examples)
			{
				string[] array = keyValuePair.Value as string[];
				int[] array2 = task.Spec.NodeIndexes[keyValuePair.Key];
				if (array == null || array.Length != 1 || array2 == null || array2.Length != 1)
				{
					return OptionalUtils.Some((T)null);
				}
				list.Add(Tuple.Create<State, string, int>(keyValuePair.Key, array[0], array2[0]));
			}
			List<HashSet<IDomNode>> matchingNodeSets = NodeTextMatching.GetMatchingNodeSets(base.Grammar, list, this._options.TextComparer, null, true);
			if (matchingNodeSets != null)
			{
				if (!matchingNodeSets.Any((HashSet<IDomNode> s) => s.IsEmpty<IDomNode>()))
				{
					int num = matchingNodeSets.Aggregate(1, (int x, HashSet<IDomNode> y) => x * y.Count);
					KeyValuePair<State, object>[] array3 = task.Spec.Examples.ToArray<KeyValuePair<State, object>>();
					if (array3.Length <= 2 || num < 10)
					{
						Tuple<ProgramSetBuilder<singletonField>, IEnumerable<IDomNode>> tuple = this.LearnSubstringFieldPrograms(engine, task, array3, matchingNodeSets, cancel);
						return ((tuple != null) ? tuple.Item1.Set : null).Some<ProgramSet>();
					}
					List<HashSet<IDomNode>> list2 = matchingNodeSets.Take(2).ToList<HashSet<IDomNode>>();
					Tuple<ProgramSetBuilder<singletonField>, IEnumerable<IDomNode>> tuple2 = this.LearnSubstringFieldPrograms(engine, task, array3, list2, cancel);
					if (tuple2 == null)
					{
						return OptionalUtils.Some((T)null);
					}
					List<IDomNode> list3 = tuple2.Item2.ToList<IDomNode>();
					for (int i = 2; i < matchingNodeSets.Count; i++)
					{
						list2 = list3.Select((IDomNode n) => new HashSet<IDomNode> { n }).AppendItem(matchingNodeSets[i]).ToList<HashSet<IDomNode>>();
						tuple2 = this.LearnSubstringFieldPrograms(engine, task, array3.Take(i + 1).ToArray<KeyValuePair<State, object>>(), list2, cancel);
						if (tuple2 == null)
						{
							return OptionalUtils.Some((T)null);
						}
						list3 = tuple2.Item2.ToList<IDomNode>();
					}
					return tuple2.Item1.Set.Some<ProgramSet>();
				}
			}
			return OptionalUtils.Some((T)null);
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x060081E4 RID: 33252 RVA: 0x001B8364 File Offset: 0x001B6564
		[ExternLearningLogicMapping("Substring")]
		public DomainLearningLogic ExternWitnessFunction { get; } = new Witnesses(Language.Grammar, Learner.Instance.ScoreFeature, new string[] { "col1" }, new Witnesses.Options());

		// Token: 0x060081E5 RID: 33253 RVA: 0x001B836C File Offset: 0x001B656C
		private static int GetIndexInSelection(selection p, ExampleSpec spec, int maxIndex)
		{
			Dictionary<State, IEnumerator<IDomNode>> dictionary = spec.Examples.ToDictionary((KeyValuePair<State, object> kvp) => kvp.Key, (KeyValuePair<State, object> kvp) => p.Node.Invoke(kvp.Key).ToEnumerable<object>().Cast<IDomNode>()
				.GetEnumerator());
			int i = 0;
			while (i <= maxIndex)
			{
				bool flag = true;
				foreach (KeyValuePair<State, IEnumerator<IDomNode>> keyValuePair in dictionary)
				{
					if (!keyValuePair.Value.MoveNext())
					{
						return -1;
					}
					if (!spec.Examples[keyValuePair.Key].Equals(keyValuePair.Value.Current))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return i;
				}
				i++;
				continue;
			}
			return -1;
		}

		// Token: 0x060081E6 RID: 33254 RVA: 0x001B844C File Offset: 0x001B664C
		[RuleLearner("AppendField")]
		public Optional<ProgramSet> LearnAppendFieldRule(SynthesisEngine engine, GrammarRule rule, LearningTask<FieldExtractionSpec> task, CancellationToken cancel)
		{
			string[][] array = task.Spec.Examples.Values.Cast<string[]>().ToArray<string[]>();
			if (array.IsEmpty<string[]>())
			{
				return OptionalUtils.Some((T)null);
			}
			int numFields = array[0].Length;
			if (array.Any((string[] t) => t.Length != numFields))
			{
				return OptionalUtils.Some((T)null);
			}
			List<ProgramSetBuilder<singletonField>> list = new List<ProgramSetBuilder<singletonField>>();
			for (int i = 0; i < numFields; i++)
			{
				Dictionary<State, Record<object, IEnumerable<object>>> dictionary = new Dictionary<State, Record<object, IEnumerable<object>>>();
				Dictionary<State, int[]> dictionary2 = new Dictionary<State, int[]>();
				foreach (KeyValuePair<State, object> keyValuePair in task.Spec.Examples)
				{
					State key = keyValuePair.Key;
					string[] array2 = new string[] { ((string[])keyValuePair.Value)[i] };
					dictionary[key] = new Record<object, IEnumerable<object>>(array2, new List<object>());
					int[] array3 = task.Spec.NodeIndexes[key];
					dictionary2[key] = new int[] { array3[i] };
				}
				LearningTask learningTask = task.Clone(this._build.Symbol.singletonField, new FieldExtractionSpec(dictionary, dictionary2));
				ProgramSetBuilder<singletonField> programSetBuilder = this._build.Set.Cast.singletonField(engine.Learn(learningTask, cancel));
				if (ProgramSetBuilder.IsNullOrEmpty<singletonField>(programSetBuilder))
				{
					return OptionalUtils.Some((T)null);
				}
				list.Add(programSetBuilder);
			}
			return list.AggregateSeedFunc(new Func<ProgramSetBuilder<singletonField>, ProgramSetBuilder<resultFields>>(this._build.Set.UnnamedConversion.resultFields_singletonField), new Func<ProgramSetBuilder<resultFields>, ProgramSetBuilder<singletonField>, ProgramSetBuilder<resultFields>>(this._build.Set.Join.AppendField)).Set.Some<ProgramSet>();
		}

		// Token: 0x060081E7 RID: 33255 RVA: 0x001B8620 File Offset: 0x001B6820
		[RuleLearner("DisjSelection1")]
		public Optional<ProgramSet> LearnDisjSelection1(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return this.LearnDisjSelectionCore<selection, filterSelection>(engine, rule, task, cancel, new Func<ProgramNode, filterSelection>(this._build.Node.Cast.filterSelection), new Func<filterSelection, selection>(this._rules.SingleSelection1), new Func<selection, filterSelection, selection>(this._rules.DisjSelection1)).Set.Some<ProgramSet>();
		}

		// Token: 0x060081E8 RID: 33256 RVA: 0x001B8680 File Offset: 0x001B6880
		[RuleLearner("DisjSelection2")]
		public Optional<ProgramSet> LearnDisjSelection2(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return this.LearnDisjSelectionCore<selection3, filterSelection2>(engine, rule, task, cancel, new Func<ProgramNode, filterSelection2>(this._build.Node.Cast.filterSelection2), new Func<filterSelection2, selection3>(this._rules.SingleSelection2), new Func<selection3, filterSelection2, selection3>(this._rules.DisjSelection2)).Set.Some<ProgramSet>();
		}

		// Token: 0x060081E9 RID: 33257 RVA: 0x001B86E0 File Offset: 0x001B68E0
		[RuleLearner("DisjSelection3")]
		public Optional<ProgramSet> LearnDisjSelection3(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return this.LearnDisjSelectionCore<selection5, filterSelection3>(engine, rule, task, cancel, new Func<ProgramNode, filterSelection3>(this._build.Node.Cast.filterSelection3), new Func<filterSelection3, selection5>(this._rules.SingleSelection3), new Func<selection5, filterSelection3, selection5>(this._rules.DisjSelection3)).Set.Some<ProgramSet>();
		}

		// Token: 0x060081EA RID: 33258 RVA: 0x001B8740 File Offset: 0x001B6940
		[RuleLearner("DisjSelection4")]
		public Optional<ProgramSet> LearnDisjSelection4(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return this.LearnDisjSelectionCore<selection7, filterSelection4>(engine, rule, task, cancel, new Func<ProgramNode, filterSelection4>(this._build.Node.Cast.filterSelection4), new Func<filterSelection4, selection7>(this._rules.SingleSelection4), new Func<selection7, filterSelection4, selection7>(this._rules.DisjSelection4)).Set.Some<ProgramSet>();
		}

		// Token: 0x060081EB RID: 33259 RVA: 0x001B87A0 File Offset: 0x001B69A0
		[RuleLearner("DisjSelection5")]
		public Optional<ProgramSet> LearnDisjSelection5(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return this.LearnDisjSelectionCore<selection9, filterSelection5>(engine, rule, task, cancel, new Func<ProgramNode, filterSelection5>(this._build.Node.Cast.filterSelection5), new Func<filterSelection5, selection9>(this._rules.SingleSelection5), new Func<selection9, filterSelection5, selection9>(this._rules.DisjSelection5)).Set.Some<ProgramSet>();
		}

		// Token: 0x060081EC RID: 33260 RVA: 0x001B8800 File Offset: 0x001B6A00
		private ProgramSetBuilder<TSelection> LearnDisjSelectionCore<TSelection, TFilter>(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel, Func<ProgramNode, TFilter> castFilter, Func<TFilter, TSelection> singleSelection, Func<TSelection, TFilter, TSelection> disjSelection) where TSelection : IProgramNodeBuilder where TFilter : IProgramNodeBuilder
		{
			if (rule == this._build.Rule.DisjSelection1)
			{
				this._haveSingleExampleForSequenceExtractionTask = task.Spec.PositiveExamples.All((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.Count<object>() == 1);
			}
			LearningTask learningTask = task.MakeSubtask(rule, 1, task.Spec);
			IEnumerable<TFilter> enumerable = engine.Learn(learningTask, cancel).RealizedPrograms.Select(castFilter);
			if (enumerable.IsEmpty<TFilter>())
			{
				return ProgramSetBuilder.Empty<TSelection>(rule.Head);
			}
			IEnumerable<TFilter> enumerable2 = enumerable;
			Func<TFilter, int> func;
			if ((func = Witnesses.<LearnDisjSelectionCore>O__134_0<TSelection, TFilter>.<0>__GetProgramSize) == null)
			{
				func = (Witnesses.<LearnDisjSelectionCore>O__134_0<TSelection, TFilter>.<0>__GetProgramSize = new Func<TFilter, int>(Utils.GetProgramSize<TFilter>));
			}
			TFilter[] array = enumerable2.OrderByDescending(func).ToArray<TFilter>();
			if (array.Length == 1 || this._options.LearnSimplePrograms)
			{
				TFilter[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					TFilter p2 = array2[i];
					this._selectionProgramResultsCache[p2.Node] = task.Spec.ProvidedInputs.ToDictionary((State s) => s, delegate(State s)
					{
						IEnumerable<object> enumerable3 = p2.Node.Invoke(s) as IEnumerable<object>;
						if (enumerable3 == null)
						{
							return null;
						}
						return enumerable3.Cast<IDomNode>().ToArray<IDomNode>();
					});
				}
				Dictionary<TFilter, double> numNodesSelected = array.ToDictionary((TFilter p) => p, (TFilter p) => this.AverageNumNodesSelected(p.Node, this._selectionProgramResultsCache[p.Node].Values));
				Dictionary<TFilter, bool> satisfiesExtractionBoundary = array.ToDictionary((TFilter p) => p, (TFilter p) => !this._haveSingleExampleForSequenceExtractionTask || (numNodesSelected[p] > 1.0 && this._extractionBoundaryNodeIndex > 0 && this._selectionProgramResultsCache[p.Node].Values.All((IDomNode[] s) => s.All((IDomNode n) => n.Start <= this._extractionBoundaryNodeIndex))));
				Dictionary<TFilter, bool> satisfiesRowConstraints = array.ToDictionary((TFilter p) => p, (TFilter p) => this._rowNodesContrainingTopDownSynthesis == null || this._selectionProgramResultsCache[p.Node].Values.All((IDomNode[] s) => s.All((IDomNode n) => this._rowNodesContrainingTopDownSynthesis.Any((IDomNode r) => r.Contains(n))) || this._rowNodesContrainingTopDownSynthesis.All((IDomNode r) => s.Any((IDomNode n) => n.Contains(r)))));
				IOrderedEnumerable<TFilter> orderedEnumerable = from p in array
					where satisfiesRowConstraints[p]
					orderby (!satisfiesExtractionBoundary[p]) ? 1 : 0, numNodesSelected[p]
					select p;
				Func<TFilter, int> func2;
				if ((func2 = Witnesses.<LearnDisjSelectionCore>O__134_0<TSelection, TFilter>.<0>__GetProgramSize) == null)
				{
					func2 = (Witnesses.<LearnDisjSelectionCore>O__134_0<TSelection, TFilter>.<0>__GetProgramSize = new Func<TFilter, int>(Utils.GetProgramSize<TFilter>));
				}
				array = orderedEnumerable.ThenBy(func2).Take(1).ToArray<TFilter>();
				return ProgramSetBuilder.List<TSelection>(rule.Head, array.Select(singleSelection));
			}
			TSelection tselection = array.AggregateSeedFunc(singleSelection, disjSelection);
			return ProgramSetBuilder.List<TSelection>(new TSelection[] { tselection });
		}

		// Token: 0x060081ED RID: 33261 RVA: 0x001B8A9E File Offset: 0x001B6C9E
		private double AverageNumNodesSelected(ProgramNode p, IEnumerable<IEnumerable<object>> results)
		{
			return results.Average(delegate(IEnumerable<object> s)
			{
				if (s == null)
				{
					return 0;
				}
				return s.Count<object>();
			});
		}

		// Token: 0x060081EE RID: 33262 RVA: 0x001B8AC8 File Offset: 0x001B6CC8
		[RuleLearner("LeafFilter1")]
		public Optional<ProgramSet> LearnLeafFilter1(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return this.LearnLeafFilterCore<selection2, filterSelection>(engine, rule, task, cancel, new Func<ProgramNode, selection2>(this._build.Node.Cast.selection2), new Func<ProgramNode, filterSelection>(this._build.Node.Cast.filterSelection), "LeafFilter1").Set.Some<ProgramSet>();
		}

		// Token: 0x060081EF RID: 33263 RVA: 0x001B8B28 File Offset: 0x001B6D28
		[RuleLearner("LeafFilter2")]
		public Optional<ProgramSet> LearnLeafFilter2(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return this.LearnLeafFilterCore<selection4, filterSelection2>(engine, rule, task, cancel, new Func<ProgramNode, selection4>(this._build.Node.Cast.selection4), new Func<ProgramNode, filterSelection2>(this._build.Node.Cast.filterSelection2), "LeafFilter2").Set.Some<ProgramSet>();
		}

		// Token: 0x060081F0 RID: 33264 RVA: 0x001B8B88 File Offset: 0x001B6D88
		[RuleLearner("LeafFilter3")]
		public Optional<ProgramSet> LearnLeafFilter3(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return this.LearnLeafFilterCore<selection6, filterSelection3>(engine, rule, task, cancel, new Func<ProgramNode, selection6>(this._build.Node.Cast.selection6), new Func<ProgramNode, filterSelection3>(this._build.Node.Cast.filterSelection3), "LeafFilter3").Set.Some<ProgramSet>();
		}

		// Token: 0x060081F1 RID: 33265 RVA: 0x001B8BE8 File Offset: 0x001B6DE8
		[RuleLearner("LeafFilter4")]
		public Optional<ProgramSet> LearnLeafFilter4(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return this.LearnLeafFilterCore<selection8, filterSelection4>(engine, rule, task, cancel, new Func<ProgramNode, selection8>(this._build.Node.Cast.selection8), new Func<ProgramNode, filterSelection4>(this._build.Node.Cast.filterSelection4), "LeafFilter4").Set.Some<ProgramSet>();
		}

		// Token: 0x060081F2 RID: 33266 RVA: 0x001B8C48 File Offset: 0x001B6E48
		[RuleLearner("LeafFilter5")]
		public Optional<ProgramSet> LearnLeafFilter5(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return this.LearnLeafFilterCore<selection10, filterSelection5>(engine, rule, task, cancel, new Func<ProgramNode, selection10>(this._build.Node.Cast.selection10), new Func<ProgramNode, filterSelection5>(this._build.Node.Cast.filterSelection5), "LeafFilter5").Set.Some<ProgramSet>();
		}

		// Token: 0x060081F3 RID: 33267 RVA: 0x001B8CA8 File Offset: 0x001B6EA8
		private ProgramSetBuilder<TFilter> LearnLeafFilterCore<TSelection, TFilter>(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel, Func<ProgramNode, TSelection> castSelection, Func<ProgramNode, TFilter> castFilter, string leafFilterRuleName) where TSelection : IProgramNodeBuilder where TFilter : IProgramNodeBuilder
		{
			LearningTask learningTask = task.MakeSubtask(rule, 1, task.Spec);
			IEnumerable<TSelection> enumerable = engine.Learn(learningTask, cancel).RealizedPrograms.Select(castSelection);
			if (enumerable.IsEmpty<TSelection>())
			{
				return ProgramSetBuilder.Empty<TFilter>(rule.Head);
			}
			GrammarRule leafFilterRule = base.Grammar.Rule(leafFilterRuleName);
			List<Tuple<TSelection, ProgramNode>> list = new List<Tuple<TSelection, ProgramNode>>();
			using (IEnumerator<TSelection> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSelection selNode = enumerator.Current;
					Dictionary<State, IDomNode[]> dictionary = task.Spec.ProvidedInputs.ToDictionary((State s) => s, (State s) => this.GetFilterProgramResult(selNode.Node, s));
					List<Tuple<State, TextTableSpec>> multiPageStateSpecs = this._multiPageStateSpecs;
					IDomNode[][] array = ((multiPageStateSpecs != null) ? multiPageStateSpecs.Select((Tuple<State, TextTableSpec> t) => this.GetFilterProgramResult(selNode.Node, t.Item1)).ToArray<IDomNode[]>() : null);
					ExampleSpec leafExprSpec = Witnesses.GetLeafExprSpec(rule as Filter, task.Spec, dictionary, array);
					LearningTask learningTask2 = task.Clone(leafFilterRule.Body[0], leafExprSpec);
					foreach (ProgramNode programNode in engine.Learn(learningTask2, cancel).RealizedPrograms)
					{
						list.Add(Tuple.Create<TSelection, ProgramNode>(selNode, programNode));
					}
				}
			}
			return ProgramSetBuilder.List<TFilter>(rule.Head, list.Select(delegate(Tuple<TSelection, ProgramNode> t)
			{
				GrammarRule leafFilterRule2 = leafFilterRule;
				ProgramNode item = t.Item2;
				TSelection item2 = t.Item1;
				return leafFilterRule2.BuildASTNode(item, item2.Node);
			}).Select(castFilter));
		}

		// Token: 0x060081F4 RID: 33268 RVA: 0x001B8E98 File Offset: 0x001B7098
		private IDomNode[] GetFilterProgramResult(ProgramNode p, State s)
		{
			if (p.Children.Length == 1 && p.Children[0].Children.Length == 1)
			{
				ProgramNode programNode = p.Children[0].Children[0];
				Dictionary<State, IDomNode[]> dictionary;
				IDomNode[] array;
				if (this._selectionProgramResultsCache.TryGetValue(programNode, out dictionary) && dictionary.TryGetValue(s, out array))
				{
					return Microsoft.ProgramSynthesis.Extraction.Web.Semantics.Semantics.LeafChildrenOf(array).ToArray<IDomNode>();
				}
			}
			if (p.Children.Length == 1 && p.Children[0].Symbol == base.Grammar.InputSymbol)
			{
				ProgramNode programNode2 = p.Children[0];
				Dictionary<State, IDomNode[]> dictionary;
				IDomNode[] array;
				if (this._selectionProgramResultsCache.TryGetValue(programNode2, out dictionary) && dictionary.TryGetValue(s, out array))
				{
					return array;
				}
				IEnumerable<object> enumerable = programNode2.Invoke(s) as IEnumerable<object>;
				array = ((enumerable != null) ? enumerable.Cast<IDomNode>().ToArray<IDomNode>() : null);
				if (dictionary == null)
				{
					this._selectionProgramResultsCache[programNode2] = new Dictionary<State, IDomNode[]>();
				}
				this._selectionProgramResultsCache[programNode2][s] = array;
				return array;
			}
			else
			{
				IEnumerable<object> enumerable2 = p.Invoke(s) as IEnumerable<object>;
				if (enumerable2 == null)
				{
					return null;
				}
				return enumerable2.Cast<IDomNode>().ToArray<IDomNode>();
			}
		}

		// Token: 0x060081F5 RID: 33269 RVA: 0x001B8FB0 File Offset: 0x001B71B0
		private static ExampleSpec GetLeafExprSpec(Filter rule, SubsequenceSpec spec, Dictionary<State, IDomNode[]> orderedSupersets, IDomNode[][] additionalDocSupersets)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			Symbol symbol = rule.Grammar.Symbol("superset");
			if (symbol == null)
			{
				symbol = rule.Grammar.AddSymbol("superset", new ResolvedType(typeof(IEnumerable<object>)), false);
			}
			Symbol symbol2 = rule.Grammar.Symbol("additionalDocSupersets");
			if (symbol2 == null)
			{
				symbol2 = rule.Grammar.AddSymbol("additionalDocSupersets", new ResolvedType(typeof(IDomNode[][])), false);
			}
			foreach (State state in spec.ProvidedInputs)
			{
				foreach (object obj in spec.PositiveExamples[state])
				{
					if (obj != null)
					{
						State state2 = state.WithFunctionalInput(obj, false);
						object obj2;
						if (dictionary.TryGetValue(state2, out obj2) && !true.Equals(obj2))
						{
							return null;
						}
						state2 = state2.Bind(symbol, orderedSupersets[state]);
						state2 = state2.Bind(symbol2, additionalDocSupersets);
						dictionary[state2] = true;
					}
				}
				foreach (object obj3 in spec.NegativeExamples[state].Intersect(orderedSupersets[state]))
				{
					if (obj3 != null)
					{
						State state3 = state.WithFunctionalInput(obj3, false);
						object obj4;
						if (dictionary.TryGetValue(state3, out obj4) && !false.Equals(obj4))
						{
							return null;
						}
						state3 = state3.Bind(symbol, orderedSupersets[state]);
						state3 = state3.Bind(symbol2, additionalDocSupersets);
						dictionary[state3] = false;
					}
				}
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060081F6 RID: 33270 RVA: 0x001B91F0 File Offset: 0x001B73F0
		private Optional<ProgramSet> LearnExceptUnderSimpleProgramsConstraint(GrammarRule rule)
		{
			if (this._options.LearnSimplePrograms)
			{
				return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
			}
			return Optional<ProgramSet>.Nothing;
		}

		// Token: 0x060081F7 RID: 33271 RVA: 0x001B9215 File Offset: 0x001B7415
		[RuleLearner("CSSSelection")]
		public Optional<ProgramSet> LearnCSSSelectionRule(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
		}

		// Token: 0x060081F8 RID: 33272 RVA: 0x001B9227 File Offset: 0x001B7427
		[RuleLearner("ContainsDate")]
		public Optional<ProgramSet> LearnContainsDateRule(SynthesisEngine engine, GrammarRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel)
		{
			return this.LearnExceptUnderSimpleProgramsConstraint(rule);
		}

		// Token: 0x060081F9 RID: 33273 RVA: 0x001B9227 File Offset: 0x001B7427
		[RuleLearner("ContainsNum")]
		public Optional<ProgramSet> LearnContainsNumRule(SynthesisEngine engine, GrammarRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel)
		{
			return this.LearnExceptUnderSimpleProgramsConstraint(rule);
		}

		// Token: 0x060081FA RID: 33274 RVA: 0x001B9227 File Offset: 0x001B7427
		[RuleLearner("ContainsLeafNodes")]
		public Optional<ProgramSet> LearnContainsLeafNodes(SynthesisEngine engine, GrammarRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel)
		{
			return this.LearnExceptUnderSimpleProgramsConstraint(rule);
		}

		// Token: 0x060081FB RID: 33275 RVA: 0x001B9227 File Offset: 0x001B7427
		[RuleLearner("ChildrenCount")]
		public Optional<ProgramSet> LearnChildrenCount(SynthesisEngine engine, GrammarRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel)
		{
			return this.LearnExceptUnderSimpleProgramsConstraint(rule);
		}

		// Token: 0x060081FC RID: 33276 RVA: 0x001B9227 File Offset: 0x001B7427
		[RuleLearner("HasEntityAnchor")]
		public Optional<ProgramSet> LearnHasAllEntities(SynthesisEngine engine, GrammarRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel)
		{
			return this.LearnExceptUnderSimpleProgramsConstraint(rule);
		}

		// Token: 0x060081FD RID: 33277 RVA: 0x001B9230 File Offset: 0x001B7430
		[RuleLearner("And")]
		public Optional<ProgramSet> LearnAndRule(SynthesisEngine engine, GrammarRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel)
		{
			return this.LearnAndCore<fexpr, literalExpr>(engine, rule, task, cancel, new Func<ProgramNode, literalExpr>(this._build.Node.Cast.literalExpr), new Func<literalExpr, fexpr>(this._build.Node.UnnamedConversion.fexpr_literalExpr), new Func<fexpr, literalExpr, fexpr>(this._rules.And)).Set.Some<ProgramSet>();
		}

		// Token: 0x060081FE RID: 33278 RVA: 0x001B929C File Offset: 0x001B749C
		[RuleLearner("LeafAnd")]
		public Optional<ProgramSet> LearnLeafAndRule(SynthesisEngine engine, GrammarRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel)
		{
			return this.LearnAndCore<leafFExpr, leafAtom>(engine, rule, task, cancel, new Func<ProgramNode, leafAtom>(this._build.Node.Cast.leafAtom), new Func<leafAtom, leafFExpr>(this._build.Node.UnnamedConversion.leafFExpr_leafAtom), new Func<leafFExpr, leafAtom, leafFExpr>(this._rules.LeafAnd)).Set.Some<ProgramSet>();
		}

		// Token: 0x060081FF RID: 33279 RVA: 0x001B9308 File Offset: 0x001B7508
		private ProgramSetBuilder<TExpr> LearnAndCore<TExpr, TLeaf>(SynthesisEngine engine, GrammarRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel, Func<ProgramNode, TLeaf> castLeaf, Func<TLeaf, TExpr> wrapAtom, Func<TExpr, TLeaf, TExpr> buildAnd) where TExpr : IProgramNodeBuilder where TLeaf : struct, IProgramNodeBuilder
		{
			Witnesses.<>c__DisplayClass153_0<TExpr, TLeaf> CS$<>8__locals1 = new Witnesses.<>c__DisplayClass153_0<TExpr, TLeaf>();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.wrapAtom = wrapAtom;
			CS$<>8__locals1.buildAnd = buildAnd;
			ExampleSpec spec = task.Spec;
			Conjunct conjunct = rule as Conjunct;
			if (conjunct == null)
			{
				throw new Exception();
			}
			if (spec is BooleanSoftSpec)
			{
				return ProgramSetBuilder.Empty<TExpr>(rule.Head);
			}
			CS$<>8__locals1.nodeSymbol = this._build.Symbol.node;
			CS$<>8__locals1.specNodes = spec.DisjunctiveExamples.Keys.Select((State s) => s[CS$<>8__locals1.nodeSymbol] as IDomNode).ConvertToHashSet<IDomNode>();
			BooleanSoftSpec booleanSoftSpec = new BooleanSoftSpec(spec.Examples);
			TLeaf[] array = engine.LearnSymbol(conjunct.Predicate2, booleanSoftSpec, cancel).RealizedPrograms.Select(castLeaf).ToArray<TLeaf>();
			CS$<>8__locals1.predicateAddDocsMap = null;
			CS$<>8__locals1.additionalDocSupersets = (IDomNode[][])spec.DisjunctiveExamples.Keys.First<State>().Bindings.SingleOrDefault((KeyValuePair<Symbol, object> kvp) => kvp.Key == CS$<>8__locals1.<>4__this.Grammar.Symbol("additionalDocSupersets")).Value;
			IDomNode[][] additionalDocSupersets = CS$<>8__locals1.additionalDocSupersets;
			if (additionalDocSupersets != null && additionalDocSupersets.Length > 1)
			{
				CS$<>8__locals1.predicateAddDocsMap = array.ToDictionary((TLeaf p) => p, (TLeaf p) => CS$<>8__locals1.<>4__this.NumDocsSatisfied<TLeaf>(p, CS$<>8__locals1.additionalDocSupersets));
				array = array.OrderByDescending((TLeaf p) => CS$<>8__locals1.predicateAddDocsMap[p]).ToArray<TLeaf>();
			}
			Dictionary<TLeaf, int[][]> dictionary = null;
			if (this._rowNodesContrainingTopDownSynthesis != null && CS$<>8__locals1.specNodes.All((IDomNode n) => CS$<>8__locals1.<>4__this._rowNodesContrainingTopDownSynthesis.Any((IDomNode r) => r.Contains(n))))
			{
				State[][] rowStates = this._rowNodesContrainingTopDownSynthesis.Select(delegate(IDomNode n)
				{
					IEnumerable<IDomNode> descendants = n.GetDescendants(true);
					Func<IDomNode, State> func3;
					if ((func3 = CS$<>8__locals1.<>9__13) == null)
					{
						func3 = (CS$<>8__locals1.<>9__13 = (IDomNode n1) => State.CreateForExecution(CS$<>8__locals1.nodeSymbol, n1));
					}
					return descendants.Select(func3).ToArray<State>();
				}).ToArray<State[]>();
				dictionary = array.ToDictionary((TLeaf p) => p, delegate(TLeaf p)
				{
					Func<State, int> <>9__15;
					return rowStates.Select(delegate(State[] r)
					{
						Func<State, int> func4;
						if ((func4 = <>9__15) == null)
						{
							func4 = (<>9__15 = delegate(State s)
							{
								bool? flag = p.Node.Invoke(s) as bool?;
								bool flag2 = true;
								return (((flag.GetValueOrDefault() == flag2) & (flag != null)) > false) ? 1 : 0;
							});
						}
						return r.Select(func4).ToArray<int>();
					}).ToArray<int[]>();
				});
			}
			if (array.Length == 0)
			{
				return ProgramSetBuilder.Empty<TExpr>(rule.Head);
			}
			if (array.Length == 1)
			{
				return ProgramSetBuilder.List<TExpr>(array.Select(CS$<>8__locals1.wrapAtom).ToArray<TExpr>());
			}
			List<TExpr> list = new List<TExpr>();
			TExpr texpr = array.AggregateSeedFunc(CS$<>8__locals1.wrapAtom, CS$<>8__locals1.buildAnd);
			if (this._learnMostSpecificConjunction)
			{
				return ProgramSetBuilder.List<TExpr>(rule.Head, new TExpr[] { texpr });
			}
			if (CS$<>8__locals1.predicateAddDocsMap != null)
			{
				if (CS$<>8__locals1.predicateAddDocsMap.Values.Any((int x) => x > 1))
				{
					int maxAddDocsCovered = (from v in CS$<>8__locals1.predicateAddDocsMap.Values
						group v by v into g
						where g.Key > 1
						select g).ArgMaxMultiple((IGrouping<int, int> g) => g.Count<int>()).Max((IGrouping<int, int> g) => g.Key);
					TExpr texpr2 = (from kvp in CS$<>8__locals1.predicateAddDocsMap
						where kvp.Value == maxAddDocsCovered
						select kvp.Key).ToArray<TLeaf>().AggregateSeedFunc(CS$<>8__locals1.wrapAtom, CS$<>8__locals1.buildAnd);
					int[] array2 = (from k in this.NumNodesExtracted<TExpr>(texpr, CS$<>8__locals1.additionalDocSupersets)
						where k > 0
						select k).ToArray<int>();
					int[] array3 = (from k in this.NumNodesExtracted<TExpr>(texpr2, CS$<>8__locals1.additionalDocSupersets)
						where k > 0
						select k).ToArray<int>();
					if (array2.Distinct<int>().Count<int>() == 1 && array3.Distinct<int>().Count<int>() == 1 && array2[0] == array3[0])
					{
						texpr = texpr2;
					}
				}
			}
			list.Add(texpr);
			CS$<>8__locals1.supersetSymbol = base.Grammar.Symbol("superset");
			if (spec.DisjunctiveExamples.Keys.All((State s) => base.<LearnAndCore>g__SupersetNodes|7(s) != null))
			{
				Witnesses.<>c__DisplayClass153_5<TExpr, TLeaf> CS$<>8__locals4 = new Witnesses.<>c__DisplayClass153_5<TExpr, TLeaf>();
				Dictionary<IDomNode, IEnumerable<IDomNode>> dictionary2 = spec.DisjunctiveExamples.Keys.GroupBy(new Func<State, object>(CS$<>8__locals1.<LearnAndCore>g__SupersetNodes|7)).ToDictionary(delegate(IGrouping<object, State> g)
				{
					Func<State, IDomNode> func5;
					if ((func5 = CS$<>8__locals1.<>9__34) == null)
					{
						func5 = (CS$<>8__locals1.<>9__34 = (State s) => s[CS$<>8__locals1.nodeSymbol] as IDomNode);
					}
					return g.Select(func5).ArgMax((IDomNode n) => n.Start);
				}, (IGrouping<object, State> g) => (g.Key as IEnumerable<object>).Cast<IDomNode>());
				List<IDomNode> list2 = (from n in dictionary2.SelectMany((KeyValuePair<IDomNode, IEnumerable<IDomNode>> kvp) => kvp.Value.TakeWhile((IDomNode n) => n.Start < kvp.Key.Start))
					where !CS$<>8__locals1.specNodes.Contains(n)
					select n).Distinct<IDomNode>().ToList<IDomNode>();
				Witnesses.<>c__DisplayClass153_5<TExpr, TLeaf> CS$<>8__locals5 = CS$<>8__locals4;
				IDomNode domNode = DomNodeExt.LastUnderLCA(CS$<>8__locals1.specNodes.ToArray<IDomNode>());
				CS$<>8__locals5.lastUnderLCA = ((domNode != null) ? new int?(domNode.Start) : null);
				if (CS$<>8__locals4.lastUnderLCA != null)
				{
					list2.AddRange((from n in dictionary2.SelectMany(delegate(KeyValuePair<IDomNode, IEnumerable<IDomNode>> kvp)
						{
							IEnumerable<IDomNode> value = kvp.Value;
							Func<IDomNode, bool> func6;
							if ((func6 = CS$<>8__locals4.<>9__37) == null)
							{
								func6 = (CS$<>8__locals4.<>9__37 = delegate(IDomNode n)
								{
									int start = n.Start;
									int? lastUnderLCA = CS$<>8__locals4.lastUnderLCA;
									return (start <= lastUnderLCA.GetValueOrDefault()) & (lastUnderLCA != null);
								});
							}
							return value.SkipWhile(func6);
						})
						where !CS$<>8__locals1.specNodes.Contains(n)
						select n).Distinct<IDomNode>().ToArray<IDomNode>());
				}
				List<IDomNode> list3 = new List<IDomNode>();
				Dictionary<TLeaf, List<int>> dictionary3 = array.ToDictionary((TLeaf p) => p, (TLeaf p) => new List<int>());
				foreach (IDomNode domNode2 in list2)
				{
					State nState = State.CreateForExecution(CS$<>8__locals1.nodeSymbol, domNode2);
					int[] array4 = array.Select(delegate(TLeaf p)
					{
						bool? flag3 = p.Node.Invoke(nState) as bool?;
						bool flag4 = true;
						return (((flag3.GetValueOrDefault() == flag4) & (flag3 != null)) > false) ? 1 : 0;
					}).ToArray<int>();
					if (array4.Any((int v) => v == 0))
					{
						list3.Add(domNode2);
						for (int i = 0; i < array.Length; i++)
						{
							dictionary3[array[i]].Add(array4[i]);
						}
					}
				}
				TExpr[] array5 = (from preds in this.GetMinimalCoveringConjunctionPredicates<TLeaf>(dictionary3, list3, dictionary)
					select preds.AggregateSeedFunc(CS$<>8__locals1.wrapAtom, CS$<>8__locals1.buildAnd)).ToArray<TExpr>();
				list.AddRange(array5);
				if (this._options.LearnSimplePrograms && list.Count > 1)
				{
					TExpr texpr3;
					if (CS$<>8__locals1.additionalDocSupersets == null || CS$<>8__locals1.additionalDocSupersets.Length == 1)
					{
						IEnumerable<TExpr> enumerable = list;
						Func<TExpr, int> func;
						if ((func = Witnesses.<LearnAndCore>O__153_0<TExpr, TLeaf>.<0>__GetProgramSize) == null)
						{
							func = (Witnesses.<LearnAndCore>O__153_0<TExpr, TLeaf>.<0>__GetProgramSize = new Func<TExpr, int>(Utils.GetProgramSize<TExpr>));
						}
						texpr3 = enumerable.ArgMin(func);
					}
					else
					{
						Dictionary<TExpr, int[]> numNodesExtracted = list.Distinct<TExpr>().ToDictionary((TExpr p) => p, (TExpr p) => (from k in CS$<>8__locals1.<>4__this.NumNodesExtracted<TExpr>(p, CS$<>8__locals1.additionalDocSupersets)
							where k > 0
							select k).ToArray<int>());
						IEnumerable<TExpr> enumerable2 = list.ArgMaxMultiple((TExpr p) => (numNodesExtracted[p].Distinct<int>().Count<int>() == 1) ? 1 : 0).ArgMaxMultiple((TExpr p) => numNodesExtracted[p].Count<int>());
						Func<TExpr, int> func2;
						if ((func2 = Witnesses.<LearnAndCore>O__153_0<TExpr, TLeaf>.<0>__GetProgramSize) == null)
						{
							func2 = (Witnesses.<LearnAndCore>O__153_0<TExpr, TLeaf>.<0>__GetProgramSize = new Func<TExpr, int>(Utils.GetProgramSize<TExpr>));
						}
						texpr3 = enumerable2.ArgMin(func2);
					}
					list = new List<TExpr> { texpr3 };
				}
			}
			return ProgramSetBuilder.List<TExpr>(rule.Head, list.Distinct<TExpr>().ToArray<TExpr>());
		}

		// Token: 0x06008200 RID: 33280 RVA: 0x001B9AE8 File Offset: 0x001B7CE8
		private int NumDocsSatisfied<T>(T p, IDomNode[][] additionalDocSupersets) where T : IProgramNodeBuilder
		{
			if (additionalDocSupersets == null || additionalDocSupersets.Length == 1)
			{
				return 1;
			}
			Symbol nodeSymbol = this._build.Symbol.node;
			Func<IDomNode, bool> <>9__1;
			return additionalDocSupersets.Count(delegate(IDomNode[] s)
			{
				Func<IDomNode, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(IDomNode n)
					{
						bool? flag = p.Node.Invoke(State.CreateForExecution(nodeSymbol, n)) as bool?;
						bool flag2 = true;
						return (flag.GetValueOrDefault() == flag2) & (flag != null);
					});
				}
				return s.Any(func);
			});
		}

		// Token: 0x06008201 RID: 33281 RVA: 0x001B9B38 File Offset: 0x001B7D38
		private int[] NumNodesExtracted<T>(T p, IDomNode[][] additionalDocSupersets) where T : IProgramNodeBuilder
		{
			Func<IDomNode, bool> <>9__1;
			return additionalDocSupersets.Select(delegate(IDomNode[] s)
			{
				Func<IDomNode, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(IDomNode n)
					{
						bool? flag = p.Node.Invoke(State.CreateForExecution(this._build.Symbol.node, n)) as bool?;
						bool flag2 = true;
						return (flag.GetValueOrDefault() == flag2) & (flag != null);
					});
				}
				return s.Count(func);
			}).ToArray<int>();
		}

		// Token: 0x06008202 RID: 33282 RVA: 0x001B9B70 File Offset: 0x001B7D70
		private List<List<TLeaf>> GetMinimalCoveringConjunctionPredicates<TLeaf>(Dictionary<TLeaf, List<int>> predicateValueMap, List<IDomNode> allPriorNonSatisfyingNodes, Dictionary<TLeaf, int[][]> rowStatesMap) where TLeaf : struct, IProgramNodeBuilder
		{
			List<List<TLeaf>> list = new List<List<TLeaf>>();
			List<List<TLeaf>> list2 = new List<List<TLeaf>>();
			int[] array = Enumerable.Range(0, allPriorNonSatisfyingNodes.Count).ToArray<int>();
			HashSet<TLeaf> coveredPredicates = new HashSet<TLeaf>();
			int count = predicateValueMap.Keys.Count;
			Func<KeyValuePair<TLeaf, List<int>>, bool> <>9__0;
			while (coveredPredicates.Count < count)
			{
				Func<KeyValuePair<TLeaf, List<int>>, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (KeyValuePair<TLeaf, List<int>> kvp) => !coveredPredicates.Contains(kvp.Key));
				}
				Dictionary<TLeaf, List<int>> dictionary = predicateValueMap.Where(func).ToDictionary<TLeaf, List<int>>();
				List<List<TLeaf>> list3;
				List<List<TLeaf>> minimalConjunctionPredicates = this.GetMinimalConjunctionPredicates<TLeaf>(dictionary, array, rowStatesMap, out list3);
				list.AddRange(minimalConjunctionPredicates);
				list2.AddRange(list3);
				if (list.Count > 8)
				{
					return list.Take(8).ToList<List<TLeaf>>();
				}
				int count2 = coveredPredicates.Count;
				coveredPredicates.UnionWith(minimalConjunctionPredicates.SelectMany((List<TLeaf> s) => s));
				if (coveredPredicates.Count == count2)
				{
					break;
				}
			}
			if (!list.Any<List<TLeaf>>())
			{
				return list2;
			}
			return list;
		}

		// Token: 0x06008203 RID: 33283 RVA: 0x001B9C8C File Offset: 0x001B7E8C
		private List<List<TLeaf>> GetMinimalConjunctionPredicates<TLeaf>(Dictionary<TLeaf, List<int>> predicateValueMap, int[] relevantPositions, Dictionary<TLeaf, int[][]> rowStatesMap, out List<List<TLeaf>> partialSatResult) where TLeaf : struct, IProgramNodeBuilder
		{
			Witnesses.<>c__DisplayClass157_0<TLeaf> CS$<>8__locals1 = new Witnesses.<>c__DisplayClass157_0<TLeaf>();
			CS$<>8__locals1.predicateValueMap = predicateValueMap;
			CS$<>8__locals1.rowStatesMap = rowStatesMap;
			List<List<TLeaf>> list = new List<List<TLeaf>>();
			partialSatResult = new List<List<TLeaf>>();
			if (CS$<>8__locals1.predicateValueMap.IsEmpty<KeyValuePair<TLeaf, List<int>>>() || relevantPositions.IsEmpty<int>())
			{
				return list;
			}
			CS$<>8__locals1.predicateCoverages = this.GetRankedPredicateCoverage<TLeaf>(CS$<>8__locals1.predicateValueMap, relevantPositions);
			TLeaf[] array = (from t in CS$<>8__locals1.predicateCoverages.TakeWhile((Tuple<TLeaf, int> t, int k) => k == 0 || CS$<>8__locals1.predicateCoverages[k - 1].Item2 == t.Item2)
				select t.Item1).ToArray<TLeaf>();
			if (array.IsEmpty<TLeaf>())
			{
				return list;
			}
			TLeaf[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Witnesses.<>c__DisplayClass157_1<TLeaf> CS$<>8__locals2 = new Witnesses.<>c__DisplayClass157_1<TLeaf>();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.firstPred = array2[i];
				List<TLeaf> resultList = new List<TLeaf> { CS$<>8__locals2.firstPred };
				int[] array3 = relevantPositions.Where((int p) => CS$<>8__locals2.CS$<>8__locals1.predicateValueMap[CS$<>8__locals2.firstPred][p] == 1).ToArray<int>();
				Dictionary<TLeaf, int[][]> rowStatesMap2 = CS$<>8__locals2.CS$<>8__locals1.rowStatesMap;
				List<int[]> list2;
				if (rowStatesMap2 == null)
				{
					list2 = null;
				}
				else
				{
					list2 = rowStatesMap2[CS$<>8__locals2.firstPred].Select((int[] a) => (from k in Enumerable.Range(0, a.Length)
						where a[k] == 1
						select k).ToArray<int>()).ToList<int[]>();
				}
				List<int[]> list3 = list2;
				Func<int, bool> <>9__5;
				while (array3.Any<int>())
				{
					Tuple<TLeaf, int>[] rankedPredicateCoverage = this.GetRankedPredicateCoverage<TLeaf>(CS$<>8__locals2.CS$<>8__locals1.predicateValueMap, array3);
					TLeaf? tleaf = null;
					if (CS$<>8__locals2.CS$<>8__locals1.rowStatesMap == null)
					{
						Tuple<TLeaf, int> tuple = rankedPredicateCoverage.FirstOrDefault<Tuple<TLeaf, int>>();
						tleaf = ((tuple != null) ? new TLeaf?(tuple.Item1) : null);
					}
					else
					{
						Tuple<TLeaf, int>[] array4 = rankedPredicateCoverage;
						for (int j = 0; j < array4.Length; j++)
						{
							Tuple<TLeaf, int> tuple2 = array4[j];
							TLeaf pred = tuple2.Item1;
							List<int[]> list4 = new List<int[]>();
							int l;
							int k;
							Func<int, bool> <>9__6;
							for (k = 0; k < list3.Count; k = l + 1)
							{
								IEnumerable<int> enumerable = list3[k];
								Func<int, bool> func;
								if ((func = <>9__6) == null)
								{
									func = (<>9__6 = (int p) => CS$<>8__locals2.CS$<>8__locals1.rowStatesMap[pred][k][p] == 1);
								}
								int[] array5 = enumerable.Where(func).ToArray<int>();
								if (array5.IsEmpty<int>())
								{
									break;
								}
								list4.Add(array5);
								l = k;
							}
							if (list4.Count == list3.Count)
							{
								tleaf = new TLeaf?(pred);
								list3 = list4;
								break;
							}
						}
					}
					if (tleaf == null)
					{
						break;
					}
					resultList.Add(tleaf.Value);
					IEnumerable<int> enumerable2 = array3;
					Func<int, bool> func2;
					if ((func2 = <>9__5) == null)
					{
						func2 = (<>9__5 = (int p) => resultList.All((TLeaf pred) => CS$<>8__locals2.CS$<>8__locals1.predicateValueMap[pred][p] == 1));
					}
					array3 = enumerable2.Where(func2).ToArray<int>();
				}
				if (array3.IsEmpty<int>())
				{
					list.Add(resultList);
				}
				else
				{
					partialSatResult.Add(resultList);
				}
			}
			return list;
		}

		// Token: 0x06008204 RID: 33284 RVA: 0x001B9FDC File Offset: 0x001B81DC
		private Tuple<TLeaf, int>[] GetRankedPredicateCoverage<TLeaf>(Dictionary<TLeaf, List<int>> predicateValueMap, int[] relevantPositions)
		{
			return (from kvp in predicateValueMap
				select Tuple.Create<TLeaf, int>(kvp.Key, this.GetSatisfactionScore(kvp.Value, relevantPositions)) into t
				orderby t.Item2 descending
				select t).TakeWhile((Tuple<TLeaf, int> t) => t.Item2 > 0).ToArray<Tuple<TLeaf, int>>();
		}

		// Token: 0x06008205 RID: 33285 RVA: 0x001BA05C File Offset: 0x001B825C
		private int GetSatisfactionScore(List<int> values, int[] relevantPositions)
		{
			return relevantPositions.Count((int p) => values[p] == 0);
		}

		// Token: 0x06008206 RID: 33286 RVA: 0x001BA088 File Offset: 0x001B8288
		[WitnessFunction("NodeToWebRegion", 0)]
		[WitnessFunction("NodeToWebRegionInSequence", 0)]
		internal DisjunctiveExamplesSpec WitnessNodeToWebRegion(GrammarRule rule, ExampleWithNegativesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in spec.ProvidedInputs)
			{
				List<WebRegion> list = spec.DisjunctiveExamples[state].Cast<WebRegion>().ToList<WebRegion>();
				if (list.Any((WebRegion r) => r.IsPair))
				{
					return null;
				}
				List<object> list2 = list.Select((WebRegion example) => example.BeginNode).Cast<object>().ToList<object>();
				dictionary[state] = list2;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x06008207 RID: 33287 RVA: 0x001BA160 File Offset: 0x001B8360
		[WitnessFunction("HasEntityAnchor", "direction")]
		internal DisjunctiveExamplesSpec WitnessDirection(BlackBoxRule rule, BooleanExampleSpec outerSpec)
		{
			if (outerSpec is BooleanHardNegativeSpec)
			{
				return null;
			}
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			dictionary[outerSpec.ProvidedInputs.First<State>()] = Enum.GetValues(typeof(KeyDirections)).ToEnumerable<object>();
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x06008208 RID: 33288 RVA: 0x001BA19C File Offset: 0x001B839C
		[WitnessFunction("HasEntityAnchor", "entityObjs", DependsOnParameters = new int[] { 1 })]
		internal ExampleSpec WitnessHasEntityAnchor(BlackBoxRule rule, BooleanExampleSpec outerSpec, ExampleSpec dirSpec)
		{
			if (outerSpec is BooleanHardNegativeSpec)
			{
				return null;
			}
			Symbol symbol = rule.Body[2];
			EntityDetectorsMap entityDetectorsMap = this._options.EntityDetectorsMap;
			IReadOnlyDictionary<string, EntityDetector> readOnlyDictionary = ((entityDetectorsMap != null) ? entityDetectorsMap.EmployedEntityDetectors : null);
			if (readOnlyDictionary == null || readOnlyDictionary.IsEmpty<KeyValuePair<string, EntityDetector>>())
			{
				return null;
			}
			object value = dirSpec.Examples.First<KeyValuePair<State, object>>().Value;
			if (!(value is KeyDirections))
			{
				throw new Exception("WitnessHasEntityAnchor did not get a valid direction value.");
			}
			IEnumerable<EntityDetector> enumerable = this.IntersectionOfEntityDetectors(symbol, outerSpec, (KeyDirections)value);
			if (enumerable == null || enumerable.IsEmpty<EntityDetector>())
			{
				return null;
			}
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			dictionary[outerSpec.ProvidedInputs.First<State>()] = enumerable.ToArray<EntityDetector>();
			return new ExampleSpec(dictionary);
		}

		// Token: 0x06008209 RID: 33289 RVA: 0x001BA24C File Offset: 0x001B844C
		private IEnumerable<EntityDetector> IntersectionOfEntityDetectors(Symbol s1, BooleanExampleSpec outerSpec, KeyDirections direction)
		{
			EntityDetectorsMap entityDetectorsMap = this._options.EntityDetectorsMap;
			IReadOnlyDictionary<string, EntityDetector> readOnlyDictionary = ((entityDetectorsMap != null) ? entityDetectorsMap.EmployedEntityDetectors : null);
			return outerSpec.ProvidedInputs.Collect(delegate(State state)
			{
				object obj = state[s1];
				if (!(obj is IDomNode))
				{
					throw new Exception("WitnessHasEntityAnchor did not get a valid value binding for the node variable.");
				}
				if (outerSpec.Selection[state])
				{
					if ((obj as IDomNode).Index < 2)
					{
					}
					IEnumerable<IDomNode> keys = DomNodeKVPUtils.GetKeys(obj as IDomNode, direction, 3);
					List<string> inputs = (from prevNode in keys
						select prevNode.TrimmedInnerText into input
						where !string.IsNullOrEmpty(input)
						select input).ToList<string>();
					return (EntityDetector instance) => inputs.Any((string input) => instance.HasEntity(input));
				}
				return null;
			}).IterativeFilterOf(readOnlyDictionary.Values);
		}

		// Token: 0x0600820A RID: 33290 RVA: 0x001BA2B4 File Offset: 0x001B84B4
		public override object[][][] Ranker(object[][] states, object[][] nonRankingStates, object[][] inputs)
		{
			if (!this._options.LearnPredictive)
			{
				return new NodeCollection[0][][];
			}
			NodeCollection[] array = states.Select((object[] s) => (NodeCollection)s[0]).ToArray<NodeCollection>();
			NodeCollection[] array2;
			if (!this._options.LearnPredictive)
			{
				array2 = new NodeCollection[0];
			}
			else
			{
				array2 = nonRankingStates.Select((object[] s) => (NodeCollection)s[0]).ToArray<NodeCollection>();
			}
			NodeCollection[] array3 = array2;
			HashSet<NodeCollection> auxRankingStates = new HashSet<NodeCollection>();
			IReadOnlyList<NodeCollection> readOnlyList = this.GetWellFormedNodeSets(array, 5000, auxRankingStates);
			IReadOnlyList<NodeCollection> readOnlyList2 = this.GetWellFormedNodeSets(array3, 5000, null);
			HashSet<NodeCollection> htmlTableColumns = readOnlyList2.Where(new Func<NodeCollection, bool>(this.IsValidHtmlTableColumn)).ConvertToHashSet<NodeCollection>();
			readOnlyList = readOnlyList.Union(htmlTableColumns).ToArray<NodeCollection>();
			readOnlyList2 = readOnlyList2.Where((NodeCollection n) => !htmlTableColumns.Contains(n)).ToArray<NodeCollection>();
			HashSet<IDomNode> hashSet;
			HashSet<NodeCollection>[] array4 = this.InferBestCliques(readOnlyList, auxRankingStates, out hashSet);
			HashSet<NodeCollection> hashSet2 = array4.FirstOrDefault<HashSet<NodeCollection>>();
			NodeCollection nodeCollection = ((hashSet2 != null) ? hashSet2.FirstOrDefault<NodeCollection>() : null);
			int? num;
			if (nodeCollection == null)
			{
				num = null;
			}
			else
			{
				num = new int?(nodeCollection.MaxRowAncestors.SortedSet.Select((IDomNode n) => n.Parent).Distinct<IDomNode>().Count<IDomNode>());
			}
			int? num2 = num;
			if (!(nodeCollection == null) && nodeCollection.Set.Count >= 5)
			{
				int? num3 = num2;
				int num4 = 5;
				if (!((num3.GetValueOrDefault() > num4) & (num3 != null)))
				{
					goto IL_0233;
				}
			}
			HashSet<NodeCollection> adjacentRowAncStates = readOnlyList2.Where(new Func<NodeCollection, bool>(this.HasAdjacentRowAncestors)).ConvertToHashSet<NodeCollection>();
			if (adjacentRowAncStates.Any<NodeCollection>())
			{
				readOnlyList = readOnlyList.Union(adjacentRowAncStates).ToArray<NodeCollection>();
				readOnlyList2 = readOnlyList2.Where((NodeCollection n) => !adjacentRowAncStates.Contains(n)).ToArray<NodeCollection>();
				array4 = this.InferBestCliques(readOnlyList, auxRankingStates, out hashSet);
				hashSet2 = array4.FirstOrDefault<HashSet<NodeCollection>>();
			}
			IL_0233:
			if (hashSet2 == null || hashSet2.IsEmpty<NodeCollection>())
			{
				return new NodeCollection[0][][];
			}
			List<NodeCollection[]> list = new List<NodeCollection[]>();
			readOnlyList = readOnlyList.Where((NodeCollection s) => !auxRankingStates.Contains(s)).ToArray<NodeCollection>();
			Predicate<NodeCollection> <>9__7;
			for (int i = 0; i < array4.Length; i++)
			{
				HashSet<NodeCollection> hashSet3 = array4[i];
				HashSet<NodeCollection> hashSet4 = hashSet3;
				Predicate<NodeCollection> predicate;
				if ((predicate = <>9__7) == null)
				{
					predicate = (<>9__7 = (NodeCollection s) => auxRankingStates.Contains(s));
				}
				hashSet4.RemoveWhere(predicate);
				int num5 = ((i == 0) ? Witnesses.MinAuxStateSize : Math.Max(hashSet3.First<NodeCollection>().Set.Count - Witnesses.AuxStateSizeVariation, Witnesses.AuxStateSizeVariation));
				NodeCollection[] allStates = Witnesses.GetAllAuxilliaryStates(hashSet3, array, readOnlyList, readOnlyList2, num5);
				allStates = allStates.Where((NodeCollection c1, int i1) => !Witnesses.HasDegenerateTextValues(c1) && !allStates.Any((NodeCollection c2, int i2) => (i2 < i1 && c2.SortedSet.SequenceEqual(c1.SortedSet)) || this.IsTrivialChildNodeSequence(c1, c2))).ToArray<NodeCollection>();
				if (allStates.Any<NodeCollection>())
				{
					list.Add(allStates);
				}
			}
			if (list.IsEmpty<NodeCollection[]>())
			{
				return new NodeCollection[0][][];
			}
			this.InitializePredictiveLearningCache(list.Skip(1));
			NodeCollection[] array5 = list[0];
			this._predictiveTopAlignmentGroup = this.InferAlignmentGroup(array5);
			if (this._predictiveTopAlignmentGroup == null || this._predictiveTopAlignmentGroup.NumColumns != array5.Length)
			{
				return new NodeCollection[0][][];
			}
			NodeCollection[][][] array6 = new NodeCollection[1][][];
			array6[0] = array5.Select((NodeCollection s) => new NodeCollection[] { s }).ToArray<NodeCollection[]>();
			return array6;
		}

		// Token: 0x0600820B RID: 33291 RVA: 0x001BA6B0 File Offset: 0x001B88B0
		private void InitializePredictiveLearningCache(IEnumerable<NodeCollection[]> bestCliques)
		{
			this._predictiveLearningCache = new List<Witnesses.AlignmentGroup>();
			foreach (NodeCollection[] array in from c in bestCliques
				orderby c.Select((NodeCollection s) => s.SortedSet.First<IDomNode>().Start).Min(), c[0].Set.Count
				select c)
			{
				int num = array.Select((NodeCollection c) => c.Set.Count).Max();
				Witnesses.AlignmentGroup alignmentGroup = this.InferAlignmentGroup(array);
				if (alignmentGroup != null && alignmentGroup.NumColumns >= 2 && alignmentGroup.NumColumns * num >= 10)
				{
					this._predictiveLearningCache.Add(alignmentGroup);
				}
			}
		}

		// Token: 0x0600820C RID: 33292 RVA: 0x001BA79C File Offset: 0x001B899C
		private Witnesses.AlignmentGroup InferAlignmentGroup(Witnesses.ProgramState rowState, NodeCollection[] columns, IDomNode[] maxRowAncs = null)
		{
			Dictionary<object[], LearnerState> dictionary = base.LearnedPrograms[this._build.Symbol.nodeCollection];
			if (rowState == null || rowState.Result.Length == 0)
			{
				return null;
			}
			IDomNode domNode = DomNodeExt.LowestCommonAncestor(rowState.Result);
			if (domNode == null)
			{
				return null;
			}
			List<Witnesses.ProgramState> list = new List<Witnesses.ProgramState>();
			List<Witnesses.ProgramState> list2 = new List<Witnesses.ProgramState>();
			IDomNode[][] boundaryBasedRowAlignment = Microsoft.ProgramSynthesis.Extraction.Web.Semantics.Semantics.GetBoundaryBasedRowAlignment(columns.Select((NodeCollection s) => s.SortedSet).ToArray<IDomNode[]>(), rowState.Result);
			int i = 0;
			Func<IDomNode, int, bool> <>9__1;
			Func<IDomNode, int, bool> <>9__2;
			while (i < columns.Length)
			{
				NodeCollection nodeCollection = columns[i];
				IDomNode[] array = boundaryBasedRowAlignment[i];
				if (maxRowAncs == null || maxRowAncs.Length != array.Length)
				{
					goto IL_0120;
				}
				IEnumerable<IDomNode> enumerable = array;
				Func<IDomNode, int, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(IDomNode n, int k)
					{
						if (n != null && k < maxRowAncs.Length - 1)
						{
							IDomNode domNode2 = maxRowAncs[k + 1];
							return domNode2 != null && domNode2.Contains(n);
						}
						return false;
					});
				}
				if (!enumerable.Any(func))
				{
					if (nodeCollection.Set.Count >= 4)
					{
						goto IL_0120;
					}
					IEnumerable<IDomNode> enumerable2 = array;
					Func<IDomNode, int, bool> func2;
					if ((func2 = <>9__2) == null)
					{
						func2 = (<>9__2 = delegate(IDomNode n, int k)
						{
							if (n != null)
							{
								IDomNode domNode3 = maxRowAncs[k];
								return domNode3 == null || !domNode3.Contains(n);
							}
							return false;
						});
					}
					if (!enumerable2.Any(func2))
					{
						goto IL_0120;
					}
				}
				IL_01B4:
				i++;
				continue;
				IL_0120:
				LearnerState learnerState;
				if (!dictionary.TryGetValue(new object[] { nodeCollection }, out learnerState))
				{
					goto IL_01B4;
				}
				resultSequence resultSequence = this._build.Node.Cast.resultSequence(this._build.Rule.ConvertToWebRegions.BuildASTNode(learnerState.Program));
				if (boundaryBasedRowAlignment[i].All((IDomNode n) => n != null))
				{
					list.Add(new Witnesses.ProgramState(resultSequence, array));
					goto IL_01B4;
				}
				list2.Add(new Witnesses.ProgramState(resultSequence, array));
				goto IL_01B4;
			}
			if (list.Count + list2.Count == 0)
			{
				return null;
			}
			return new Witnesses.AlignmentGroup(domNode, rowState, list, list2);
		}

		// Token: 0x0600820D RID: 33293 RVA: 0x001BA98C File Offset: 0x001B8B8C
		private Witnesses.AlignmentGroup InferAlignmentGroup(NodeCollection[] columnSelections)
		{
			if (!columnSelections.Any<NodeCollection>())
			{
				return null;
			}
			int numRows = columnSelections.Max((NodeCollection c) => c.Set.Count);
			if (numRows < 2)
			{
				return null;
			}
			Dictionary<object[], LearnerState> dictionary = base.LearnedPrograms[this._build.Symbol.nodeCollection];
			NodeCollection[] fullColumns = columnSelections.Where((NodeCollection c) => c != null && c.Set.Count == numRows).ToArray<NodeCollection>();
			NodeCollection[] array = columnSelections.Where((NodeCollection c) => c != null && c.Set.Count < numRows).ToArray<NodeCollection>();
			NodeCollection[] array2 = fullColumns.Select((NodeCollection c) => c.MaxRowAncestors).Where(delegate(NodeCollection c)
			{
				Func<IDomNode, bool> <>9__6;
				return columnSelections.All(delegate(NodeCollection c1)
				{
					IEnumerable<IDomNode> set = c1.Set;
					Func<IDomNode, bool> func;
					if ((func = <>9__6) == null)
					{
						func = (<>9__6 = (IDomNode n) => c.MaxRowAncestors.Set.Any((IDomNode n1) => n1.Contains(n)));
					}
					return set.All(func);
				});
			}).Distinct<NodeCollection>()
				.ToArray<NodeCollection>();
			for (int j = 0; j < array2.Length; j++)
			{
				NodeCollection maxRowAncestors = array2[j].MaxRowAncestors;
				LearnerState learnerState;
				if (maxRowAncestors != null && dictionary.TryGetValue(new object[] { maxRowAncestors }, out learnerState))
				{
					Witnesses.ProgramState programState = new Witnesses.ProgramState(this._build.Node.Cast.resultSequence(this._build.Rule.ConvertToWebRegions.BuildASTNode(learnerState.Program)), maxRowAncestors.SortedSet);
					return this.InferAlignmentGroup(programState, columnSelections, null);
				}
			}
			NodeCollection nodeCollection = null;
			if (fullColumns.Length == 1)
			{
				nodeCollection = fullColumns[0];
			}
			else
			{
				IDomNode[] array3 = (from i in Enumerable.Range(0, numRows)
					select DomNodeExt.LowestCommonAncestor(fullColumns.Select((NodeCollection c) => c.SortedSet[i]).ToArray<IDomNode>())).ToArray<IDomNode>();
				if (array3.Length == numRows)
				{
					if (array3.All((IDomNode n) => n != null))
					{
						nodeCollection = new NodeCollection(array3);
					}
				}
			}
			if (nodeCollection != null)
			{
				foreach (KeyValuePair<object[], LearnerState> keyValuePair in dictionary)
				{
					NodeCollection c = keyValuePair.Key[0] as NodeCollection;
					Func<IDomNode, bool> <>9__12;
					if (c != null && c.Set.Count == numRows && NodeCollection.IsChildSequence(c, nodeCollection) && array.All(delegate(NodeCollection c1)
					{
						IEnumerable<IDomNode> set2 = c1.Set;
						Func<IDomNode, bool> func2;
						if ((func2 = <>9__12) == null)
						{
							func2 = (<>9__12 = (IDomNode n) => c.Set.Any((IDomNode n1) => n1.Contains(n)));
						}
						return set2.All(func2);
					}))
					{
						Witnesses.ProgramState programState2 = new Witnesses.ProgramState(this._build.Node.Cast.resultSequence(this._build.Rule.ConvertToWebRegions.BuildASTNode(keyValuePair.Value.Program)), c.SortedSet);
						return this.InferAlignmentGroup(programState2, columnSelections, null);
					}
				}
			}
			int num = -1;
			NodeCollection nodeCollection2 = null;
			foreach (NodeCollection nodeCollection3 in fullColumns)
			{
				if (num < 0 || nodeCollection3.SortedSet[0].Start < num)
				{
					nodeCollection2 = nodeCollection3;
					num = nodeCollection2.SortedSet[0].Start;
				}
			}
			LearnerState learnerState2;
			if (nodeCollection2 != null && dictionary.TryGetValue(new object[] { nodeCollection2 }, out learnerState2))
			{
				Witnesses.ProgramState programState3 = new Witnesses.ProgramState(this._build.Node.Cast.resultSequence(this._build.Rule.ConvertToWebRegions.BuildASTNode(learnerState2.Program)), nodeCollection2.SortedSet);
				return this.InferAlignmentGroup(programState3, columnSelections, nodeCollection2.MaxRowAncestors.SortedSet);
			}
			return null;
		}

		// Token: 0x0600820E RID: 33294 RVA: 0x001BAD88 File Offset: 0x001B8F88
		private bool IsTrivialChildNodeSequence(NodeCollection c1, NodeCollection c2)
		{
			return c1.SortedSet.Select(delegate(IDomNode n)
			{
				if (n == null)
				{
					return null;
				}
				return n.TrimmedInnerText;
			}).SequenceEqual(c2.SortedSet.Select(delegate(IDomNode n)
			{
				if (n == null)
				{
					return null;
				}
				return n.TrimmedInnerText;
			})) && NodeCollection.IsChildSequence(c2, c1);
		}

		// Token: 0x0600820F RID: 33295 RVA: 0x001BADFC File Offset: 0x001B8FFC
		private HashSet<NodeCollection>[] InferBestCliques(IReadOnlyList<NodeCollection> rankingStates, HashSet<NodeCollection> auxRankingStates, out HashSet<IDomNode> bestCliqueNodes)
		{
			Dictionary<int, HashSet<NodeCollection>> dictionary = new Dictionary<int, HashSet<NodeCollection>>();
			foreach (NodeCollection nodeCollection in rankingStates)
			{
				int count = nodeCollection.Set.Count;
				if (count >= 4)
				{
					dictionary.AddOrInsert(count, nodeCollection);
				}
			}
			KeyValuePair<int, HashSet<NodeCollection>>[] array = dictionary.OrderByDescending((KeyValuePair<int, HashSet<NodeCollection>> kvp) => kvp.Value.Count).ToArray<KeyValuePair<int, HashSet<NodeCollection>>>();
			Dictionary<NodeCollection, HashSet<NodeCollection>> interleavingRelationships = new Dictionary<NodeCollection, HashSet<NodeCollection>>();
			foreach (KeyValuePair<int, HashSet<NodeCollection>> keyValuePair in array)
			{
				foreach (NodeCollection nodeCollection2 in keyValuePair.Value)
				{
					HashSet<NodeCollection> hashSet = new HashSet<NodeCollection>();
					foreach (NodeCollection nodeCollection3 in keyValuePair.Value)
					{
						if (hashSet.Count > 50)
						{
							break;
						}
						HashSet<NodeCollection> hashSet2;
						if (interleavingRelationships.TryGetValue(nodeCollection3, out hashSet2) && hashSet2.Contains(nodeCollection2))
						{
							hashSet.Add(nodeCollection3);
						}
						else if (NodeCollection.IsInterleaving(nodeCollection2, nodeCollection3))
						{
							hashSet.Add(nodeCollection3);
						}
					}
					interleavingRelationships[nodeCollection2] = hashSet;
				}
			}
			Dictionary<NodeCollection, HashSet<NodeCollection>> minimalInterleavingRelationships = new Dictionary<NodeCollection, HashSet<NodeCollection>>();
			Dictionary<NodeCollection, HashSet<NodeCollection>> strongInterleavingRelationships = new Dictionary<NodeCollection, HashSet<NodeCollection>>();
			foreach (KeyValuePair<int, HashSet<NodeCollection>> keyValuePair2 in array)
			{
				using (HashSet<NodeCollection>.Enumerator enumerator2 = keyValuePair2.Value.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						NodeCollection s1 = enumerator2.Current;
						HashSet<NodeCollection> s1Interleavings = interleavingRelationships[s1];
						HashSet<NodeCollection> hashSet3 = s1Interleavings.Where((NodeCollection s) => s1Interleavings.All((NodeCollection s2) => s2 == s || !NodeCollection.IsChildSequence(s, s2))).ConvertToHashSet<NodeCollection>();
						minimalInterleavingRelationships[s1] = hashSet3;
						if (s1.MaxRowAncestors == null)
						{
							strongInterleavingRelationships[s1] = new HashSet<NodeCollection>();
						}
						else
						{
							strongInterleavingRelationships[s1] = new HashSet<NodeCollection>(hashSet3.Where((NodeCollection s) => s.SatisfiesRowAncestorContainment(s1)));
						}
					}
				}
			}
			KeyValuePair<NodeCollection, HashSet<NodeCollection>>[] array3 = (from kvp in interleavingRelationships.Concat(strongInterleavingRelationships).ToArray<KeyValuePair<NodeCollection, HashSet<NodeCollection>>>()
				orderby (kvp.Value.Count > 1) ? 1 : 0 descending, (strongInterleavingRelationships[kvp.Key].Count > 1) ? 1 : 0 descending, (kvp.Key.Set.Count >= 10) ? 1 : 0 descending, Math.Min(10, strongInterleavingRelationships[kvp.Key].Count) descending, Math.Min(10, minimalInterleavingRelationships[kvp.Key].Count) descending, kvp.Key.Set.Count descending
				select kvp).ToArray<KeyValuePair<NodeCollection, HashSet<NodeCollection>>>();
			List<HashSet<NodeCollection>> list = new List<HashSet<NodeCollection>>();
			HashSet<NodeCollection> hashSet4 = new HashSet<NodeCollection>();
			KeyValuePair<NodeCollection, HashSet<NodeCollection>>[] array4 = array3;
			Func<NodeCollection, bool> <>9__11;
			for (int i = 0; i < array4.Length; i++)
			{
				KeyValuePair<NodeCollection, HashSet<NodeCollection>> kvp = array4[i];
				if (!hashSet4.Overlaps(kvp.Value))
				{
					IEnumerable<NodeCollection> value = kvp.Value;
					Func<NodeCollection, bool> func;
					if ((func = <>9__11) == null)
					{
						func = (<>9__11 = (NodeCollection s) => !auxRankingStates.Contains(s));
					}
					if (value.Any(func) && kvp.Value.All((NodeCollection s) => kvp.Value.All((NodeCollection s1) => interleavingRelationships[s].Contains(s1) || interleavingRelationships[s1].Contains(s))))
					{
						list.Add(kvp.Value);
						hashSet4.AddRange(kvp.Value);
						if (list.Count == 10)
						{
							break;
						}
					}
				}
			}
			bestCliqueNodes = null;
			LinkedList<HashSet<NodeCollection>> linkedList = new LinkedList<HashSet<NodeCollection>>();
			if (list.Any<HashSet<NodeCollection>>())
			{
				HashSet<NodeCollection> hashSet5 = list.First<HashSet<NodeCollection>>();
				bestCliqueNodes = new HashSet<IDomNode>(hashSet5.SelectMany((NodeCollection c) => c.Set));
				int num = strongInterleavingRelationships[hashSet5.First<NodeCollection>()].Count;
				foreach (HashSet<NodeCollection> hashSet6 in list.Where((HashSet<NodeCollection> c) => c.Count > 1))
				{
					HashSet<IDomNode> hashSet7 = new HashSet<IDomNode>(hashSet6.SelectMany((NodeCollection c) => c.Set));
					int count2 = strongInterleavingRelationships[hashSet6.First<NodeCollection>()].Count;
					if (hashSet6.First<NodeCollection>().Set.Count > hashSet5.First<NodeCollection>().Set.Count && Witnesses.Contains(hashSet7, bestCliqueNodes) && num <= count2)
					{
						linkedList.AddFirst(hashSet6);
						hashSet5 = hashSet6;
						bestCliqueNodes = hashSet7;
						num = count2;
					}
					else
					{
						linkedList.AddLast(hashSet6);
					}
				}
			}
			return linkedList.Where(delegate(HashSet<NodeCollection> c)
			{
				if (c.Count >= 2)
				{
					return c.All((NodeCollection s) => s.Set.Count > 2);
				}
				return false;
			}).ToArray<HashSet<NodeCollection>>();
		}

		// Token: 0x06008210 RID: 33296 RVA: 0x001BB428 File Offset: 0x001B9628
		private bool IsValidHtmlTableColumn(NodeCollection s)
		{
			IDomNode first = s.Set.First<IDomNode>();
			IDomNode firstAnc = s.MaxRowAncestors.Set.First<IDomNode>();
			if (s.Set.Count > 2)
			{
				if (s.Set.All((IDomNode n) => n.NodeName == "TD") && s.Set.All((IDomNode n) => n.Index == first.Index && n.IndexFromLast == first.IndexFromLast) && s.MaxRowAncestors.Set.All((IDomNode n) => n.Parent == firstAnc.Parent))
				{
					return s.MaxRowAncestors.Set.Count == firstAnc.Parent.GetChildren().Count((IDomNode n) => n.GetChildren().Any((IDomNode c) => c.NodeName == "TD"));
				}
			}
			return false;
		}

		// Token: 0x06008211 RID: 33297 RVA: 0x001BB520 File Offset: 0x001B9720
		private bool IsKvpTextSelection(NodeCollection s)
		{
			IDomNode domNode = s.Set.FirstOrDefault<IDomNode>();
			if (domNode == null)
			{
				return false;
			}
			int num = domNode.TrimmedInnerText.IndexOf(':');
			if (num < 0)
			{
				return false;
			}
			string keyString = domNode.TrimmedInnerText.Substring(num + 1);
			return s.Set.All((IDomNode n) => n != null && n.TrimmedInnerText.StartsWith(keyString));
		}

		// Token: 0x06008212 RID: 33298 RVA: 0x001BB584 File Offset: 0x001B9784
		private bool HasAdjacentRowAncestors(NodeCollection s)
		{
			if (s.Set.Count > 3 && s.MaxRowAncestors.Set.Count == s.Set.Count && s.MaxRowAncestors.Set.First<IDomNode>().Parent != null)
			{
				return s.MaxRowAncestors.SortedSet.Select((IDomNode n, int i) => i == 0 || n.Equals(s.MaxRowAncestors.SortedSet[i - 1].NextSibling)).All((bool b) => b);
			}
			return false;
		}

		// Token: 0x06008213 RID: 33299 RVA: 0x001BB640 File Offset: 0x001B9840
		private static NodeCollection[] GetAllAuxilliaryStates(HashSet<NodeCollection> clique, IReadOnlyList<NodeCollection> rankingNodeSets, IReadOnlyList<NodeCollection> filteredRankingNodeSets, IReadOnlyList<NodeCollection> nonRankingNodeSets, int minSize)
		{
			if (clique.First<NodeCollection>().Set.Count > Witnesses.AuxStateSizeBound)
			{
				return clique.ToArray<NodeCollection>();
			}
			HashSet<IDomNode> hashSet = clique.SelectMany((NodeCollection c) => c.Set).ConvertToHashSet<IDomNode>();
			int count = clique.First<NodeCollection>().Set.Count;
			IEnumerable<NodeCollection> enumerable = Witnesses.LearnAuxilliaryStates(Witnesses.GetNonMinimalNodeSets(rankingNodeSets, count, count), clique, count, count, hashSet, null);
			List<NodeCollection> list = Witnesses.LearnAuxilliaryStates(filteredRankingNodeSets, clique, minSize, count - 1, hashSet, null);
			List<NodeCollection> list2 = Witnesses.LearnAuxilliaryStates(nonRankingNodeSets, clique, minSize, count, hashSet, list);
			IEnumerable<NodeCollection> enumerable2 = enumerable.Concat(list).Concat(list2);
			return (from s in clique.Concat(enumerable2)
				orderby s.Set.Count descending, (Witnesses.IsUniqueValueCollection(s) > false) ? 1 : 0, (!clique.Contains(s)) ? 1 : 0, s.Set.First<IDomNode>().Start
				select s).ToArray<NodeCollection>();
		}

		// Token: 0x06008214 RID: 33300 RVA: 0x001BB7A4 File Offset: 0x001B99A4
		private static bool IsGeneralizingColumn(NodeCollection c, IEnumerable<NodeCollection> generalizableColumns)
		{
			IEnumerable<NodeCollection> enumerable = generalizableColumns.Where((NodeCollection c1) => c.Set.IsProperSupersetOf(c1.Set));
			if (enumerable.HasAtLeast(2))
			{
				return c.Set.SetEquals(enumerable.SelectMany((NodeCollection s) => s.Set));
			}
			return false;
		}

		// Token: 0x06008215 RID: 33301 RVA: 0x001BB814 File Offset: 0x001B9A14
		private IReadOnlyList<NodeCollection> GetWellFormedNodeSets(IEnumerable<NodeCollection> nodeSets, int maxSize, HashSet<NodeCollection> auxRankingNodeSets = null)
		{
			List<NodeCollection> list = new List<NodeCollection>();
			using (IEnumerator<NodeCollection> enumerator = nodeSets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NodeCollection s = enumerator.Current;
					if (s.Set.Count > 1 && s.Set.Count <= maxSize && !Witnesses.ContainsExcludedNodes(s) && s.SortedSet.Where((IDomNode n, int i) => i > 0 && n.Start <= s.SortedSet[i - 1].End).IsEmpty<IDomNode>())
					{
						if (Witnesses.IsMinimalTextNodeCollection(s))
						{
							list.Add(s);
						}
						else if (auxRankingNodeSets != null && Witnesses.IsImageNodeCollection(s))
						{
							list.Add(s);
							auxRankingNodeSets.Add(s);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06008216 RID: 33302 RVA: 0x001BB90C File Offset: 0x001B9B0C
		private static bool IsImageNodeCollection(NodeCollection s)
		{
			IEnumerable<IDomNode> set = s.Set;
			Func<IDomNode, bool> func;
			if ((func = Witnesses.<>O.<4>__IsImageNode) == null)
			{
				func = (Witnesses.<>O.<4>__IsImageNode = new Func<IDomNode, bool>(Witnesses.IsImageNode));
			}
			return set.All(func);
		}

		// Token: 0x06008217 RID: 33303 RVA: 0x001BB934 File Offset: 0x001B9B34
		private static bool IsImageNode(IDomNode n)
		{
			return Witnesses.HasNodeName(n, "IMG");
		}

		// Token: 0x06008218 RID: 33304 RVA: 0x001BB944 File Offset: 0x001B9B44
		private static bool IsUniqueValueCollection(NodeCollection s)
		{
			Witnesses.<>c__DisplayClass178_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass178_0();
			Witnesses.<>c__DisplayClass178_0 CS$<>8__locals2 = CS$<>8__locals1;
			IDomNode domNode = s.Set.FirstOrDefault<IDomNode>();
			CS$<>8__locals2.firstVal = ((domNode != null) ? domNode.TrimmedInnerText : null);
			return CS$<>8__locals1.firstVal == null || s.Set.All((IDomNode n) => n.TrimmedInnerText == CS$<>8__locals1.firstVal);
		}

		// Token: 0x06008219 RID: 33305 RVA: 0x001BB995 File Offset: 0x001B9B95
		private static bool ContainsExcludedNodes(NodeCollection s)
		{
			return s.Set.Any((IDomNode n) => Witnesses.ExcludedNodeNames.Contains(n.NodeName));
		}

		// Token: 0x0600821A RID: 33306 RVA: 0x001BB9C4 File Offset: 0x001B9BC4
		private static bool IsMinimalTextNodeCollection(NodeCollection s)
		{
			HashSet<IDomNode> set = s.Set;
			if (!set.All((IDomNode n) => n.NodeName == "TD" || n.NodeName == "TH"))
			{
				if (!set.All((IDomNode n) => n.HasMinimalText()))
				{
					return false;
				}
			}
			return set.Any((IDomNode n) => !string.IsNullOrWhiteSpace(n.TrimmedInnerText));
		}

		// Token: 0x0600821B RID: 33307 RVA: 0x001BBA50 File Offset: 0x001B9C50
		private static List<NodeCollection> GetNonMinimalNodeSets(IEnumerable<NodeCollection> nodeSets, int maxSize, int minSize)
		{
			return nodeSets.Where(delegate(NodeCollection s)
			{
				if (s.Set.Count > 1 && s.Set.Count <= maxSize && s.Set.Count >= minSize)
				{
					if (s.Set.Any((IDomNode n) => !string.IsNullOrWhiteSpace(n.TrimmedInnerText)))
					{
						if (s.Set.All((IDomNode n) => n.HasMinimalText() || n.GetChildren().Any((IDomNode c) => c.NodeName == "WBR" || (c.HasMinimalText() && c.TrimmedInnerText == n.TrimmedInnerText)) || string.IsNullOrWhiteSpace(n.TrimmedInnerText)) && !Witnesses.ContainsExcludedNodes(s))
						{
							return s.SortedSet.Where((IDomNode n, int i) => i > 0 && n.Start <= s.SortedSet[i - 1].End).IsEmpty<IDomNode>();
						}
					}
				}
				return false;
			}).ToList<NodeCollection>();
		}

		// Token: 0x0600821C RID: 33308 RVA: 0x001BBA88 File Offset: 0x001B9C88
		private static List<NodeCollection> LearnAuxilliaryStates(IEnumerable<NodeCollection> candidateStates, HashSet<NodeCollection> clique, int minSize, int maxSize, HashSet<IDomNode> allPrevNodesSet, List<NodeCollection> generalizableStates = null)
		{
			Dictionary<NodeCollection, int> strongInterleavingScore = new Dictionary<NodeCollection, int>();
			HashSet<NodeCollection> hashSet = new HashSet<NodeCollection>();
			using (IEnumerator<NodeCollection> enumerator = candidateStates.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NodeCollection state = enumerator.Current;
					if (state.Set.Count >= minSize && state.Set.Count <= maxSize && clique.All((NodeCollection c) => NodeCollection.IsInterleaving(state, c)))
					{
						strongInterleavingScore[state] = clique.Count((NodeCollection c) => state.SatisfiesRowAncestorContainment(c));
						if (strongInterleavingScore[state] > 0 || state.Set.Count > 2)
						{
							hashSet.Add(state);
						}
					}
				}
			}
			NodeCollection[] array = (from s in hashSet
				orderby strongInterleavingScore[s] descending, s.Set.Count descending
				select s).ToArray<NodeCollection>();
			List<NodeCollection> list = new List<NodeCollection>();
			List<NodeCollection> list2 = new List<NodeCollection>();
			foreach (NodeCollection nodeCollection in array)
			{
				if (!Witnesses.DomNodesOverlap(nodeCollection.Set, allPrevNodesSet) || Witnesses.ContainsDistinctNodesWithNonTrivialTextSubstrings(nodeCollection.Set, allPrevNodesSet))
				{
					list.Add(nodeCollection);
					if (list.Count >= 20)
					{
						break;
					}
					allPrevNodesSet.UnionWith(nodeCollection.Set);
				}
				else
				{
					list2.Add(nodeCollection);
				}
			}
			if (generalizableStates != null)
			{
				foreach (NodeCollection nodeCollection2 in list2)
				{
					if (Witnesses.IsGeneralizingColumn(nodeCollection2, generalizableStates))
					{
						list.Add(nodeCollection2);
					}
				}
			}
			return list;
		}

		// Token: 0x0600821D RID: 33309 RVA: 0x001BBC94 File Offset: 0x001B9E94
		private static bool ContainsDistinctNodesWithNonTrivialTextSubstrings(HashSet<IDomNode> set1, HashSet<IDomNode> set2)
		{
			return set1.All((IDomNode e1) => e1.TrimmedInnerText.Length > 0 && !set2.Contains(e1) && set2.Any((IDomNode e2) => e2.Contains(e1) && e2.TrimmedInnerText.Length > e1.TrimmedInnerText.Length));
		}

		// Token: 0x0600821E RID: 33310 RVA: 0x001BBCC0 File Offset: 0x001B9EC0
		private static bool DomNodesOverlap(HashSet<IDomNode> set1, HashSet<IDomNode> set2)
		{
			return set1.Any((IDomNode e1) => set2.Any((IDomNode e2) => e1.Equals(e2) || e1.Contains(e2) || e2.Contains(e1)));
		}

		// Token: 0x0600821F RID: 33311 RVA: 0x001BBCEC File Offset: 0x001B9EEC
		private static bool Contains(HashSet<IDomNode> set1, HashSet<IDomNode> set2)
		{
			return !(from n in set2.Where((IDomNode n) => !set1.Contains(n)).ToArray<IDomNode>()
				where !set1.Any((IDomNode n2) => n2.Contains(n) || n.Contains(n2))
				select n).Any<IDomNode>();
		}

		// Token: 0x0400348D RID: 13453
		private GrammarBuilders.Nodes.NodeRules _rules;

		// Token: 0x0400348E RID: 13454
		private readonly Witnesses.Options _options;

		// Token: 0x0400348F RID: 13455
		private const int MinBoundaryBasedColSize = 4;

		// Token: 0x04003490 RID: 13456
		private const int ManyExamplesThreshold = 5;

		// Token: 0x04003491 RID: 13457
		private const int MaxTruncatedExamples = 10;

		// Token: 0x04003492 RID: 13458
		private const int MinTruncatedExamples = 3;

		// Token: 0x04003493 RID: 13459
		private const int MaxPrefixLearningAttempts = 4;

		// Token: 0x04003494 RID: 13460
		private const int MaxCombinationsAcrossInputStates = 10;

		// Token: 0x04003495 RID: 13461
		private const int MaxNumHtmlTables = 10000;

		// Token: 0x04003496 RID: 13462
		private const int HtmlTableSizeThreshold = 5;

		// Token: 0x04003497 RID: 13463
		private const int HtmlTableCountThreshold = 15;

		// Token: 0x04003498 RID: 13464
		private const int MinLogicalTableNumRows = 4;

		// Token: 0x04003499 RID: 13465
		private const int MaxNumInterleavingChecks = 50;

		// Token: 0x0400349A RID: 13466
		private const int MaxNumConjunctions = 8;

		// Token: 0x0400349B RID: 13467
		private static readonly int AuxStateSizeBound = 500;

		// Token: 0x0400349C RID: 13468
		private static readonly int MinAuxStateSize = 2;

		// Token: 0x0400349D RID: 13469
		private static readonly int AuxStateSizeVariation = 5;

		// Token: 0x0400349E RID: 13470
		private static readonly int MaxSiblingsToSearchForTitle = 5;

		// Token: 0x0400349F RID: 13471
		private const string AdditionalDocsGrammarSymbolName = "additionalDocSupersets";

		// Token: 0x040034A0 RID: 13472
		private static readonly Regex HeaderTagMatcher = new Regex("^h[1-6]", RegexOptions.IgnoreCase);

		// Token: 0x040034A1 RID: 13473
		private static readonly HashSet<string> ExcludedNodeNames = new HashSet<string> { "SCRIPT", "NOSCRIPT", "STYLE" };

		// Token: 0x040034A2 RID: 13474
		private int _extractionBoundaryNodeIndex = -1;

		// Token: 0x040034A3 RID: 13475
		private IDomNode[] _rowNodesContrainingTopDownSynthesis;

		// Token: 0x040034A4 RID: 13476
		private bool _haveSingleExampleForSequenceExtractionTask;

		// Token: 0x040034A5 RID: 13477
		private static readonly Regex MinIdNumericSubstring = new Regex("[0-9]{3}", RegexOptions.Compiled);

		// Token: 0x040034A6 RID: 13478
		private static readonly Regex PermittedIdFormat = new Regex("^[^0-9]+[0-9]{0,3}$", RegexOptions.Compiled);

		// Token: 0x040034A7 RID: 13479
		private readonly GrammarBuilders _build;

		// Token: 0x040034A8 RID: 13480
		private List<Tuple<State, TextTableSpec>> _multiPageStateSpecs;

		// Token: 0x040034A9 RID: 13481
		private Dictionary<ProgramNode, Dictionary<State, IDomNode[]>> _selectionProgramResultsCache;

		// Token: 0x040034AA RID: 13482
		private Witnesses.AlignmentGroup _predictiveTopAlignmentGroup;

		// Token: 0x040034AB RID: 13483
		private List<Witnesses.AlignmentGroup> _predictiveLearningCache;

		// Token: 0x040034AC RID: 13484
		private SynthesisEngine _engine;

		// Token: 0x040034AD RID: 13485
		private LearningTask<Spec> _learningTask;

		// Token: 0x040034AE RID: 13486
		private CancellationToken _cancel;

		// Token: 0x040034AF RID: 13487
		private Dictionary<State, Dictionary<resultSequence, IDomNode[]>> _progResultCache;

		// Token: 0x040034B0 RID: 13488
		private bool _learnMostSpecificConjunction;

		// Token: 0x020010DD RID: 4317
		public class Options : DSLOptions
		{
			// Token: 0x170016AF RID: 5807
			// (get) Token: 0x0600822D RID: 33325 RVA: 0x001BBF4D File Offset: 0x001BA14D
			// (set) Token: 0x0600822E RID: 33326 RVA: 0x001BBF55 File Offset: 0x001BA155
			public bool LearnSimplePrograms { get; set; }

			// Token: 0x170016B0 RID: 5808
			// (get) Token: 0x0600822F RID: 33327 RVA: 0x001BBF5E File Offset: 0x001BA15E
			// (set) Token: 0x06008230 RID: 33328 RVA: 0x001BBF66 File Offset: 0x001BA166
			public bool LearnCrossTemplates { get; set; }

			// Token: 0x170016B1 RID: 5809
			// (get) Token: 0x06008231 RID: 33329 RVA: 0x001BBF6F File Offset: 0x001BA16F
			// (set) Token: 0x06008232 RID: 33330 RVA: 0x001BBF77 File Offset: 0x001BA177
			public resultSequence? PreviouslyLearntRowSelector { get; set; }

			// Token: 0x170016B2 RID: 5810
			// (get) Token: 0x06008233 RID: 33331 RVA: 0x001BBF80 File Offset: 0x001BA180
			// (set) Token: 0x06008234 RID: 33332 RVA: 0x001BBF88 File Offset: 0x001BA188
			public resultSequence[] PreviouslyLearntColumnSelectors { get; set; }

			// Token: 0x170016B3 RID: 5811
			// (get) Token: 0x06008235 RID: 33333 RVA: 0x001BBF91 File Offset: 0x001BA191
			// (set) Token: 0x06008236 RID: 33334 RVA: 0x001BBF99 File Offset: 0x001BA199
			public IReadOnlyList<IReadOnlyList<string>> PreviousTextTableExamples { get; set; }

			// Token: 0x170016B4 RID: 5812
			// (get) Token: 0x06008237 RID: 33335 RVA: 0x001BBFA2 File Offset: 0x001BA1A2
			// (set) Token: 0x06008238 RID: 33336 RVA: 0x001BBFAA File Offset: 0x001BA1AA
			public List<Tuple<resultSequence, resultSequence[]>> PredictiveRowColumnSelectors { get; set; }

			// Token: 0x170016B5 RID: 5813
			// (get) Token: 0x06008239 RID: 33337 RVA: 0x001BBFB3 File Offset: 0x001BA1B3
			// (set) Token: 0x0600823A RID: 33338 RVA: 0x001BBFBB File Offset: 0x001BA1BB
			public int MaxExampleSatisfactionOffset { get; set; } = 5;

			// Token: 0x170016B6 RID: 5814
			// (get) Token: 0x0600823B RID: 33339 RVA: 0x001BBFC4 File Offset: 0x001BA1C4
			// (set) Token: 0x0600823C RID: 33340 RVA: 0x001BBFCC File Offset: 0x001BA1CC
			public StringComparer TextComparer { get; set; } = StringComparer.CurrentCultureIgnoreCase;

			// Token: 0x170016B7 RID: 5815
			// (get) Token: 0x0600823D RID: 33341 RVA: 0x001BBFD5 File Offset: 0x001BA1D5
			// (set) Token: 0x0600823E RID: 33342 RVA: 0x001BBFDD File Offset: 0x001BA1DD
			public bool LearnPredictive { get; set; }

			// Token: 0x170016B8 RID: 5816
			// (get) Token: 0x0600823F RID: 33343 RVA: 0x001BBFE6 File Offset: 0x001BA1E6
			// (set) Token: 0x06008240 RID: 33344 RVA: 0x001BBFEE File Offset: 0x001BA1EE
			public int PredictiveLogicalTableInferenceTimeout { get; set; } = 30000;

			// Token: 0x170016B9 RID: 5817
			// (get) Token: 0x06008241 RID: 33345 RVA: 0x001BBFF7 File Offset: 0x001BA1F7
			// (set) Token: 0x06008242 RID: 33346 RVA: 0x001BBFFF File Offset: 0x001BA1FF
			public string[] PermittedNodeAttributes { get; set; } = new string[0];
		}

		// Token: 0x020010DE RID: 4318
		private class PreviousProgramsMatch
		{
			// Token: 0x040034BD RID: 13501
			public resultSequence? RowProgram;

			// Token: 0x040034BE RID: 13502
			public IDomNode[] RowNodes;

			// Token: 0x040034BF RID: 13503
			public Dictionary<resultSequence, IDomNode[]>[] ColumnProgramsNodes;
		}

		// Token: 0x020010DF RID: 4319
		private class ProgramState
		{
			// Token: 0x06008245 RID: 33349 RVA: 0x001BC039 File Offset: 0x001BA239
			internal ProgramState(resultSequence program, IDomNode[] result)
			{
				this.Program = program;
				this.Result = result;
			}

			// Token: 0x040034C0 RID: 13504
			public resultSequence Program;

			// Token: 0x040034C1 RID: 13505
			public IDomNode[] Result;
		}

		// Token: 0x020010E0 RID: 4320
		private class AlignmentGroup
		{
			// Token: 0x06008246 RID: 33350 RVA: 0x001BC04F File Offset: 0x001BA24F
			internal AlignmentGroup(IDomNode lca, Witnesses.ProgramState rowState, List<Witnesses.ProgramState> fullColumns, List<Witnesses.ProgramState> subColumns)
			{
				this.LcaNode = lca;
				this.RowState = rowState;
				this.FullColumnStates = fullColumns;
				this.SubColumnStates = subColumns;
				this.NumColumns = fullColumns.Count + subColumns.Count;
			}

			// Token: 0x040034C2 RID: 13506
			public IDomNode LcaNode;

			// Token: 0x040034C3 RID: 13507
			public Witnesses.ProgramState RowState;

			// Token: 0x040034C4 RID: 13508
			public List<Witnesses.ProgramState> FullColumnStates;

			// Token: 0x040034C5 RID: 13509
			public List<Witnesses.ProgramState> SubColumnStates;

			// Token: 0x040034C6 RID: 13510
			public int NumColumns;
		}

		// Token: 0x020010E1 RID: 4321
		private class ProgProperties
		{
			// Token: 0x06008247 RID: 33351 RVA: 0x001BC088 File Offset: 0x001BA288
			public ProgProperties(resultSequence? rowProg, List<resultSequence> colProgs, int numRows, bool columnExPreciselySatisfied, IReadOnlyList<IDomNode[]> resultTableNodes)
			{
				this.RowProg = rowProg;
				this.ColProgs = colProgs;
				this.NumRows = numRows;
				this.NumCols = colProgs.Count;
				this.NumNonEmptyNodes = resultTableNodes.Sum((IDomNode[] c) => c.Count((IDomNode n) => !string.IsNullOrWhiteSpace((n != null) ? n.InnerText : null)));
				this.ColumnExPreciselySatisfied = columnExPreciselySatisfied;
				this.ResultTableNodes = resultTableNodes;
			}

			// Token: 0x040034C7 RID: 13511
			public resultSequence? RowProg;

			// Token: 0x040034C8 RID: 13512
			public List<resultSequence> ColProgs;

			// Token: 0x040034C9 RID: 13513
			public int NumRows;

			// Token: 0x040034CA RID: 13514
			public int NumCols;

			// Token: 0x040034CB RID: 13515
			public int NumNonEmptyNodes;

			// Token: 0x040034CC RID: 13516
			public bool ColumnExPreciselySatisfied;

			// Token: 0x040034CD RID: 13517
			public IReadOnlyList<IDomNode[]> ResultTableNodes;
		}

		// Token: 0x020010E3 RID: 4323
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040034D1 RID: 13521
			public static Func<string, string> <0>__EscapeSpecialCharactersCss;

			// Token: 0x040034D2 RID: 13522
			public static Func<string, string> <1>__NormalizeText;

			// Token: 0x040034D3 RID: 13523
			public static Func<string, bool> <2>__IsNullOrWhiteSpace;

			// Token: 0x040034D4 RID: 13524
			public static Func<string, bool> <3>__IsNullOrEmpty;

			// Token: 0x040034D5 RID: 13525
			public static Func<IDomNode, bool> <4>__IsImageNode;
		}

		// Token: 0x02001166 RID: 4454
		[CompilerGenerated]
		private static class <LearnAndCore>O__153_0<TExpr, TLeaf> where TExpr : IProgramNodeBuilder where TLeaf : struct, IProgramNodeBuilder
		{
			// Token: 0x0400369F RID: 13983
			public static Func<TExpr, int> <0>__GetProgramSize;
		}

		// Token: 0x02001167 RID: 4455
		[CompilerGenerated]
		private static class <LearnDisjSelectionCore>O__134_0<TSelection, TFilter> where TSelection : IProgramNodeBuilder where TFilter : IProgramNodeBuilder
		{
			// Token: 0x040036A0 RID: 13984
			public static Func<TFilter, int> <0>__GetProgramSize;
		}
	}
}

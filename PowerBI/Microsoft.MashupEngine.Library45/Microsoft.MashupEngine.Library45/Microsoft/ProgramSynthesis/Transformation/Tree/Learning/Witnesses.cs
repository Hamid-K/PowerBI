using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Tree.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Tree.Utils;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Tree;
using Microsoft.ProgramSynthesis.Wrangling.Tree.TreePathStep;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Learning
{
	// Token: 0x02001EB6 RID: 7862
	public class Witnesses : DomainLearningLogic
	{
		// Token: 0x06010978 RID: 67960 RVA: 0x0039094A File Offset: 0x0038EB4A
		public Witnesses(Grammar grammar, Witnesses.Options options, IFeature ranking)
			: base(grammar)
		{
			this._build = GrammarBuilders.Instance(grammar);
			this._options = options;
			this._ranking = ranking;
		}

		// Token: 0x06010979 RID: 67961 RVA: 0x0039096D File Offset: 0x0038EB6D
		[Tactic("newDsl")]
		internal Optional<ProgramSet> TacticsForNewDsl(IAlternatingLanguage language, Func<ILanguage, ProgramSet> learner)
		{
			return ShortCircuitTactic.Instance.LearnAlternative(language, learner);
		}

		// Token: 0x0601097A RID: 67962 RVA: 0x0039097C File Offset: 0x0038EB7C
		[RuleLearner("TmpFilter")]
		public Optional<ProgramSet> TmpFilterLearner(SynthesisEngine engine, GrammarRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			ProgramSetBuilder<match> programSetBuilder = this._build.Set.Cast.match(engine.Learn(task.Clone(this._build.Symbol.match, task.Spec), cancel));
			inorderAllNodes inorderAllNodes = this._build.Node.Rule.InOrderAllNodes(this._build.Node.Variable.selectedNode);
			ProgramSetBuilder<inorderAllNodes> programSetBuilder2 = ProgramSetBuilder.List<inorderAllNodes>(new inorderAllNodes[] { inorderAllNodes });
			ProgramSet programSet = ProgramSet.Join(this._build.Rule.TmpFilter.Body[0].LambdaRule, new ProgramSet[] { programSetBuilder.Set });
			return ProgramSet.Join(this._build.Rule.TmpFilter, new ProgramSet[] { programSet, programSetBuilder2.Set }).Some<ProgramSet>();
		}

		// Token: 0x0601097B RID: 67963 RVA: 0x00390A64 File Offset: 0x0038EC64
		[WitnessFunction("IsKind", 1)]
		public ExampleSpec WitnessEmptyStepInKind(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in spec.ProvidedInputs)
			{
				if ((state[this._build.Symbol.x] as Node).IsIrrelevantNode())
				{
					return null;
				}
				dictionary[state] = new TreePath(new TreePathStep[] { CurrentStep.Instance });
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0601097C RID: 67964 RVA: 0x00390AF8 File Offset: 0x0038ECF8
		[WitnessFunction("IsKind", 2)]
		public ExampleSpec WitnessKindInKind(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in spec.ProvidedInputs)
			{
				Node node = state[this._build.Symbol.x] as Node;
				if (node.IsIrrelevantNode())
				{
					return null;
				}
				dictionary[state] = node.Label;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0601097D RID: 67965 RVA: 0x00390B84 File Offset: 0x0038ED84
		[WitnessFunction("IsNthChild", 1)]
		public ExampleSpec WitnessKInNthKind(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in spec.ProvidedInputs)
			{
				Node node = state[this._build.Symbol.x] as Node;
				if (node.IsIrrelevantNode())
				{
					return null;
				}
				if (((node != null) ? node.Parent : null) == null)
				{
					dictionary[state] = 0;
				}
				else
				{
					dictionary[state] = node.Parent.Children.IndexOf(node);
				}
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0601097E RID: 67966 RVA: 0x00390C3C File Offset: 0x0038EE3C
		[WitnessFunction("IsAttributePresent", 1)]
		public ExampleSpec WitnessEmptyPathInAttribute(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in spec.ProvidedInputs)
			{
				if ((state[this._build.Symbol.x] as Node).IsIrrelevantNode())
				{
					return null;
				}
				dictionary[state] = new TreePath(new TreePathStep[]
				{
					new KthStep(0)
				});
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0601097F RID: 67967 RVA: 0x00390CD4 File Offset: 0x0038EED4
		[WitnessFunction("IsAttributePresent", 2)]
		public DisjunctiveExamplesSpec WitnessKeyInAttribute(GrammarRule rule, ExampleSpec spec)
		{
			return DisjunctiveExamplesSpec.From(spec.ProvidedInputs.ToDictionary((State s) => s, delegate(State s)
			{
				Node node = (Node)s[this._build.Symbol.x];
				return (from a in node.GetRelevantAttributes()
					where this.IsAttributeSupportedByNode(node, a.Name)
					select a into e
					select e.Name).Cast<object>();
			}));
		}

		// Token: 0x06010980 RID: 67968 RVA: 0x00390D14 File Offset: 0x0038EF14
		[WitnessFunction("IsAttributePresent", 3, DependsOnParameters = new int[] { 2 })]
		public ExampleSpec WitnessValueInAttribute(GrammarRule rule, ExampleSpec spec, ExampleSpec keySpec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				State key = keyValuePair.Key;
				Node node = keyValuePair.Value as Node;
				string text = keySpec.Examples[key] as string;
				string text2;
				if (text == null || node == null || !node.Attributes.TryGetValue(text, out text2))
				{
					return null;
				}
				dictionary[keyValuePair.Key] = text2;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x06010981 RID: 67969 RVA: 0x00390DC0 File Offset: 0x0038EFC0
		[RuleLearner("Conj")]
		public Optional<ProgramSet> LearnConj(SynthesisEngine engine, NonterminalRule rule, LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			if (!this.CanLearnPredicate(task))
			{
				return OptionalUtils.Some((T)null);
			}
			Record<IEnumerable<pred>, IEnumerable<pred>>? record = this.GeneratePredicates(task, cancel);
			if (record == null)
			{
				return OptionalUtils.Some((T)null);
			}
			IEnumerable<pred> enumerable;
			IEnumerable<pred> enumerable2;
			record.GetValueOrDefault().Deconstruct(out enumerable, out enumerable2);
			if (enumerable.Count<pred>() + enumerable2.Count<pred>() < 2)
			{
				return OptionalUtils.Some((T)null);
			}
			match match = enumerable.Take(500).AggregateSeedFunc(new Func<pred, match>(this._build.Node.UnnamedConversion.match_pred), (match a, pred b) => this._build.Node.Rule.Conj(b, a));
			match = (from a in enumerable2.Take(500)
				select this._build.Node.Rule.Not(a)).Aggregate(match, (match a, pred b) => this._build.Node.Rule.Conj(b, a));
			ProgramSet set = ProgramSetBuilder.List<match>(new match[] { match }).Set;
			if (!ProgramSet.IsNullOrEmpty(set))
			{
				return set.Some<ProgramSet>();
			}
			return OptionalUtils.Some((T)null);
		}

		// Token: 0x06010982 RID: 67970 RVA: 0x00390EB8 File Offset: 0x0038F0B8
		[WitnessFunction("GuardedRule", 0)]
		public SubsequenceSpec WitnessGuardInGuardedRule(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				dictionary[keyValuePair.Key] = (keyValuePair.Key[this._build.Symbol.selectedNode] as Node).Yield<Node>();
			}
			return new SubsequenceSpec(dictionary);
		}

		// Token: 0x06010983 RID: 67971 RVA: 0x0003B61D File Offset: 0x0003981D
		[WitnessFunction("GuardedRule", 1)]
		public ExampleSpec WitnessTransformationInGuardedRule(GrammarRule rule, ExampleSpec spec)
		{
			return spec;
		}

		// Token: 0x06010984 RID: 67972 RVA: 0x00390F40 File Offset: 0x0038F140
		private bool CanLearnPredicate(LearningTask<SubsequenceSpec> conjTask)
		{
			if (this._options.TreeSizeThreshold == null)
			{
				return true;
			}
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in conjTask.Spec.PositiveExamples)
			{
				foreach (Node node in keyValuePair.Value.Cast<Node>())
				{
					int count = node.Count;
					int? treeSizeThreshold = this._options.TreeSizeThreshold;
					if (((count > treeSizeThreshold.GetValueOrDefault()) & (treeSizeThreshold != null)) && !(node is SequenceNode))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06010985 RID: 67973 RVA: 0x00391018 File Offset: 0x0038F218
		private Record<IEnumerable<pred>, IEnumerable<pred>>? GeneratePredicates(LearningTask<SubsequenceSpec> task, CancellationToken cancel)
		{
			HashSet<pred> hashSet = null;
			HashSet<pred> hashSet2 = null;
			HashSet<pred> hashSet3 = new HashSet<pred>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in task.Spec.PositiveExamples)
			{
				IEnumerable<Node> enumerable = keyValuePair.Value.Cast<Node>();
				IDictionary<State, HashSet<object>> negativeExamples = task.Spec.NegativeExamples;
				KeyValuePair<State, HashSet<object>>? keyValuePair2;
				bool valueOrDefault = ((negativeExamples != null) ? ((negativeExamples.FirstOrNull<KeyValuePair<State, HashSet<object>>>() != null) ? new bool?(keyValuePair2.GetValueOrDefault().Value.Any<object>()) : null) : null).GetValueOrDefault();
				foreach (Node node in enumerable)
				{
					cancel.ThrowIfCancellationRequested();
					HashSet<pred> hashSet4 = this.ExtractPreds(node);
					if (hashSet4 != null)
					{
						if (valueOrDefault)
						{
							HashSet<pred> hashSet5 = this.ExtractAdditionalPreds(node);
							if (hashSet5 != null)
							{
								hashSet2 = ((hashSet2 == null) ? hashSet5 : hashSet2.Intersect(hashSet5).ConvertToHashSet<pred>());
							}
						}
						this.AddDescendentPreds(node, hashSet4, new TreePath(new List<TreePathStep>()), cancel);
						hashSet3.AddRange(hashSet4);
						hashSet = ((hashSet == null) ? hashSet4 : hashSet.Intersect(hashSet4).ConvertToHashSet<pred>());
					}
				}
			}
			pred? pred = hashSet.FirstOrNull((pred p) => p.Is_IsNthChild(this._build));
			pred? pred2 = hashSet.FirstOrNull((pred p) => p.Is_IsKind(this._build) && p.As_IsKind(this._build).Value.path.Value.Steps.OnlyOrDefault<TreePathStep>() == ParentStep.Instance);
			if (pred != null && pred2 == null)
			{
				hashSet.Remove(pred.Value);
			}
			HashSet<pred> hashSet6 = new HashSet<pred>();
			bool flag = false;
			foreach (KeyValuePair<State, HashSet<object>> keyValuePair3 in task.Spec.NegativeExamples)
			{
				foreach (object obj in keyValuePair3.Value)
				{
					Node node2 = (Node)obj;
					HashSet<pred> hashSet7 = this.ExtractPreds(node2);
					this.AddDescendentPreds(node2, hashSet7, new TreePath(new List<TreePathStep>()), cancel);
					IEnumerable<pred> enumerable2 = Witnesses.GetDifferentPredicates(hashSet7, hashSet3);
					if (enumerable2.IsEmpty<pred>())
					{
						HashSet<pred> differentPredicates = Witnesses.GetDifferentPredicates(this.ExtractAdditionalPreds(node2), hashSet2);
						if (differentPredicates == null || differentPredicates.IsEmpty<pred>())
						{
							return null;
						}
						if (!flag)
						{
							hashSet.AddRange(hashSet2);
							flag = true;
						}
						else
						{
							enumerable2 = differentPredicates;
						}
					}
					if (!hashSet6.Overlaps(enumerable2))
					{
						if (hashSet6.IsEmpty<pred>())
						{
							hashSet6 = enumerable2.ConvertToHashSet<pred>();
						}
						else
						{
							hashSet6.AddRange(Witnesses.GetDifferentPredicates(enumerable2, hashSet6));
						}
					}
				}
			}
			return new Record<IEnumerable<pred>, IEnumerable<pred>>?(Record.Create<IEnumerable<pred>, IEnumerable<pred>>(hashSet, hashSet6));
		}

		// Token: 0x06010986 RID: 67974 RVA: 0x00391348 File Offset: 0x0038F548
		private HashSet<pred> ExtractSiblingPreds(Node node)
		{
			if (node.Parent == null)
			{
				return null;
			}
			HashSet<pred> hashSet = new HashSet<pred>();
			Node node2 = RightSiblingTokenStep.Instance.Find(node);
			if (node2 != null)
			{
				hashSet.AddRange(this.CreateLabelAndAttributePredicates(new TreePath(new TreePathStep[] { RightSiblingTokenStep.Instance }), node2));
			}
			return hashSet;
		}

		// Token: 0x06010987 RID: 67975 RVA: 0x00391398 File Offset: 0x0038F598
		private HashSet<pred> CreateLabelAndAttributePredicates(TreePath path, Node node)
		{
			pred pred = this.CreateIsKind(node.Label, path);
			HashSet<pred> hashSet = new HashSet<pred>();
			hashSet.Add(pred);
			IEnumerable<Attributes.Attribute> enumerable = from a in node.GetRelevantAttributes()
				where this.IsAttributeSupportedByNode(node, a.Name)
				select a;
			hashSet.AddRange(enumerable.Select((Attributes.Attribute kvp) => this.CreateIsAttributePresent(kvp, path)));
			return hashSet;
		}

		// Token: 0x06010988 RID: 67976 RVA: 0x0039141A File Offset: 0x0038F61A
		private static HashSet<pred> GetDifferentPredicates(IEnumerable<pred> preds, IEnumerable<pred> comparedPreds)
		{
			if (preds != null && comparedPreds != null)
			{
				return preds.Except(comparedPreds).ConvertToHashSet<pred>();
			}
			return null;
		}

		// Token: 0x06010989 RID: 67977 RVA: 0x00391430 File Offset: 0x0038F630
		private void AddDescendentPreds(Node current, HashSet<pred> preds, TreePath path, CancellationToken cancel)
		{
			cancel.ThrowIfCancellationRequested();
			int num = 1;
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Node[] children = current.Children;
			for (int i = 0; i < children.Length; i++)
			{
				Witnesses.<>c__DisplayClass23_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass23_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.child = children[i];
				if (!CS$<>8__locals1.child.IsIrrelevantNode())
				{
					if (dictionary.ContainsKey(CS$<>8__locals1.child.Label))
					{
						Dictionary<string, int> dictionary2 = dictionary;
						string label = CS$<>8__locals1.child.Label;
						dictionary2[label]++;
					}
					else
					{
						dictionary.Add(CS$<>8__locals1.child.Label, 1);
					}
					List<TreePathStep> list = new List<TreePathStep>(path.Steps)
					{
						new KthLabelStep(CS$<>8__locals1.child.Label, dictionary[CS$<>8__locals1.child.Label])
					};
					TreePath newPath = new TreePath(list);
					pred pred = this.CreateIsKind(CS$<>8__locals1.child.Label, newPath);
					preds.Add(pred);
					if (CS$<>8__locals1.child is SequenceNode && this._options.HasNChildrenSequenceNodes.Contains(CS$<>8__locals1.child.Label) && CS$<>8__locals1.child.Children.Length == 1)
					{
						pred pred2 = this.CreateHasNChildren(CS$<>8__locals1.child.Children.Length, newPath);
						preds.Add(pred2);
					}
					IEnumerable<Attributes.Attribute> enumerable = from a in CS$<>8__locals1.child.GetRelevantAttributes()
						where CS$<>8__locals1.<>4__this.IsAttributeSupportedByNode(CS$<>8__locals1.child, a.Name)
						select a;
					preds.AddRange(enumerable.Select((Attributes.Attribute kvp) => CS$<>8__locals1.<>4__this.CreateIsAttributePresent(kvp, newPath)));
					this.AddDescendentPreds(CS$<>8__locals1.child, preds, newPath, cancel);
					num++;
				}
			}
		}

		// Token: 0x0601098A RID: 67978 RVA: 0x00391658 File Offset: 0x0038F858
		[RuleLearner("Select")]
		public Optional<ProgramSet> LearnKthNodeInSelection(SynthesisEngine engine, GrammarRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel)
		{
			ExampleSpec spec = task.Spec;
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				Node node = keyValuePair.Value as Node;
				if (node == null)
				{
					return OptionalUtils.Some((T)null);
				}
				if (node.Attributes.AllAttributes.Select((Attributes.Attribute e) => e.Value).Any((string v) => Witnesses.Keywords.Contains(v)) || node.IsIrrelevantNode())
				{
					return OptionalUtils.Some((T)null);
				}
				Node equivalentNodeInTheInputTree = this.GetEquivalentNodeInTheInputTree(node, keyValuePair);
				if (equivalentNodeInTheInputTree == null)
				{
					return OptionalUtils.Some((T)null);
				}
				dictionary[keyValuePair.Key] = Seq.Of<Node>(new Node[] { equivalentNodeInTheInputTree });
			}
			SubsequenceSpec subsequenceSpec = new SubsequenceSpec(dictionary);
			ProgramNode[] array = engine.Learn(task.Clone(this._build.Symbol.tmpFilter, subsequenceSpec), cancel).RealizedPrograms.ToArray<ProgramNode>();
			if (array.Length == 0)
			{
				return OptionalUtils.Some((T)null);
			}
			Symbol symbol = this._build.Symbol.k;
			ProgramNode[] array2 = (from program in array
				select new
				{
					index = Witnesses.GetIndexInSelection(program, subsequenceSpec, 5),
					program = program
				} into x
				where x.index >= 0
				select rule.BuildASTNode(x.program, new LiteralNode(symbol, x.index))).ToArray<ProgramNode>();
			if (!array2.IsEmpty<ProgramNode>())
			{
				return ProgramSet.List(rule.Head, array2).Some<ProgramSet>();
			}
			return OptionalUtils.Some((T)null);
		}

		// Token: 0x0601098B RID: 67979 RVA: 0x00391858 File Offset: 0x0038FA58
		private Node GetEquivalentNodeInTheInputTree(Node node, KeyValuePair<State, object> example)
		{
			Node node2 = example.Key[this._build.Symbol.selectedNode] as Node;
			if (node2 == null)
			{
				return null;
			}
			Node[] array = Semantics.InOrderAllNodes(node2);
			if (array.FirstOrDefault((Node n) => n == node) != null)
			{
				return node;
			}
			int? num = array.IndexOf(node);
			Node node3 = null;
			if (num != null)
			{
				node3 = array[num.Value];
			}
			return node3;
		}

		// Token: 0x0601098C RID: 67980 RVA: 0x003918E4 File Offset: 0x0038FAE4
		private static int GetIndexInSelection(ProgramNode p, SubsequenceSpec spec, int maxIndex)
		{
			Dictionary<State, IEnumerator<Node>> dictionary = spec.PositiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => p.Invoke(kvp.Key).ToEnumerable<object>().Cast<Node>()
				.GetEnumerator());
			int i = 0;
			while (i <= maxIndex)
			{
				bool flag = true;
				foreach (KeyValuePair<State, IEnumerator<Node>> keyValuePair in dictionary)
				{
					if (!keyValuePair.Value.MoveNext())
					{
						return -1;
					}
					Node node = spec.PositiveExamples[keyValuePair.Key].First<object>() as Node;
					if (node == null)
					{
						return -1;
					}
					if (!node.Equals(keyValuePair.Value.Current))
					{
						flag = false;
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

		// Token: 0x0601098D RID: 67981 RVA: 0x003919E0 File Offset: 0x0038FBE0
		private HashSet<pred> ExtractPreds(Node selectedNode)
		{
			if (selectedNode.IsIrrelevantNode())
			{
				return null;
			}
			TreePath treePath = new TreePath(new TreePathStep[] { CurrentStep.Instance });
			this.CreateIsKind(selectedNode.Label, treePath);
			HashSet<pred> hashSet = this.CreateLabelAndAttributePredicates(treePath, selectedNode);
			if (selectedNode.Parent != null)
			{
				hashSet.Add(this.CreateIsNthChild(selectedNode.Parent.Children.IndexOf(selectedNode).Value));
				hashSet.Add(this.CreateIsKind(selectedNode.Parent.Label, new TreePath(new TreePathStep[] { ParentStep.Instance })));
				IEnumerable<Attributes.Attribute> enumerable = from a in selectedNode.Parent.GetRelevantAttributes()
					where this.IsAttributeSupportedByNode(selectedNode.Parent, a.Name)
					select a;
				hashSet.AddRange(enumerable.Select((Attributes.Attribute kvp) => this.CreateIsAttributePresent(kvp, new TreePath(new TreePathStep[] { ParentStep.Instance }))));
			}
			if (selectedNode is SequenceNode && this._options.HasNChildrenSequenceNodes.Contains(selectedNode.Label) && selectedNode.Children.Length == 1)
			{
				hashSet.Add(this.CreateHasNChildren(selectedNode.Children.Length, new TreePath(new TreePathStep[] { CurrentStep.Instance })));
			}
			return hashSet;
		}

		// Token: 0x0601098E RID: 67982 RVA: 0x00391B58 File Offset: 0x0038FD58
		private HashSet<pred> ExtractAdditionalPreds(Node selectedNode)
		{
			return this.ExtractSiblingPreds(selectedNode);
		}

		// Token: 0x0601098F RID: 67983 RVA: 0x00391B64 File Offset: 0x0038FD64
		private bool IsAttributeSupportedByNode(Node selectedNode, string name)
		{
			Witnesses.Options options = this._options;
			HashSet<string> hashSet;
			return ((options != null) ? options.ForbiddenAttributesByLabel : null) == null || !this._options.ForbiddenAttributesByLabel.TryGetValue(selectedNode.Label, out hashSet) || !hashSet.Contains(name);
		}

		// Token: 0x06010990 RID: 67984 RVA: 0x00391BB0 File Offset: 0x0038FDB0
		private pred CreateIsAttributePresent(Attributes.Attribute attribute, TreePath path)
		{
			return this._build.Node.Rule.IsAttributePresent(this._build.Node.Variable.x, this._build.Node.Rule.path(path), this._build.Node.Rule.name(attribute.Name), this._build.Node.Rule.value(attribute.Value));
		}

		// Token: 0x06010991 RID: 67985 RVA: 0x00391C34 File Offset: 0x0038FE34
		private pred CreateIsKind(string kind, TreePath path)
		{
			return this._build.Node.Rule.IsKind(this._build.Node.Variable.x, this._build.Node.Rule.path(path), this._build.Node.Rule.kind(kind));
		}

		// Token: 0x06010992 RID: 67986 RVA: 0x00391C98 File Offset: 0x0038FE98
		private pred CreateHasNChildren(int k, TreePath path)
		{
			return this._build.Node.Rule.HasNChildren(this._build.Node.Variable.x, this._build.Node.Rule.path(path), this._build.Node.Rule.k(k));
		}

		// Token: 0x06010993 RID: 67987 RVA: 0x00391CFC File Offset: 0x0038FEFC
		private pred CreateIsNthChild(int nth)
		{
			return this._build.Node.Rule.IsNthChild(this._build.Node.Variable.x, this._build.Node.Rule.k(nth));
		}

		// Token: 0x06010994 RID: 67988 RVA: 0x00391D4C File Offset: 0x0038FF4C
		private match GenerateMatch(List<pred> preds, CancellationToken cancel)
		{
			cancel.ThrowIfCancellationRequested();
			if (preds.Count == 1)
			{
				return this._build.Node.UnnamedConversion.match_pred(preds.First<pred>());
			}
			return this._build.Node.Rule.Conj(preds.First<pred>(), this.GenerateMatch(preds.GetRange(1, preds.Count - 1), cancel));
		}

		// Token: 0x06010995 RID: 67989 RVA: 0x00391DB8 File Offset: 0x0038FFB8
		private bool CanLearnChildrenList(Node node)
		{
			if (this._options.TreeSizeThreshold != null)
			{
				int count = node.Count;
				int? treeSizeThreshold = this._options.TreeSizeThreshold;
				return (count < treeSizeThreshold.GetValueOrDefault()) & (treeSizeThreshold != null);
			}
			return true;
		}

		// Token: 0x06010996 RID: 67990 RVA: 0x00391E00 File Offset: 0x00390000
		[WitnessFunction("LeafConstLabelNode", 0)]
		internal ExampleSpec WitnessLabelInLeafConstLabelNode(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				StructNode structNode = keyValuePair.Value as StructNode;
				if (structNode == null)
				{
					return null;
				}
				if (structNode.Children.Length != 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = structNode.Label;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x06010997 RID: 67991 RVA: 0x00391E8C File Offset: 0x0039008C
		[WitnessFunction("LeafConstLabelNode", 1)]
		internal ExampleSpec WitnessValueInLeafConstLabelNode(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				StructNode structNode = keyValuePair.Value as StructNode;
				if (structNode == null)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = this.ExtractAttributes(structNode);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x06010998 RID: 67992 RVA: 0x00391F0C File Offset: 0x0039010C
		private Dictionary<string, string> ExtractAttributes(Node node)
		{
			return node.Attributes.StrongAttributes.ToDictionary((Attributes.Attribute e) => e.Name, (Attributes.Attribute e) => e.Value);
		}

		// Token: 0x06010999 RID: 67993 RVA: 0x00391F68 File Offset: 0x00390168
		[WitnessFunction("ConstLabelNode", 0)]
		internal ExampleSpec WitnessLabelInConstLabelNode(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				StructNode structNode = keyValuePair.Value as StructNode;
				if (structNode == null)
				{
					return null;
				}
				if (structNode.Children.IsEmpty<Node>())
				{
					return null;
				}
				dictionary[keyValuePair.Key] = structNode.Label;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0601099A RID: 67994 RVA: 0x00391FF8 File Offset: 0x003901F8
		[WitnessFunction("ConstLabelNode", 1)]
		internal ExampleSpec WitnessValueInConstLabelNode(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				StructNode structNode = keyValuePair.Value as StructNode;
				if (structNode == null)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = this.ExtractAttributes(structNode);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0601099B RID: 67995 RVA: 0x00392078 File Offset: 0x00390278
		[WitnessFunction("ConstLabelNode", 2)]
		internal ExampleSpec WitnessNodeInAddChildren(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				StructNode structNode = keyValuePair.Value as StructNode;
				if (structNode == null)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = Tuple.Create<Node, Node[]>(structNode, structNode.Children);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0601099C RID: 67996 RVA: 0x003920FC File Offset: 0x003902FC
		[WitnessFunction("LeafConstSequenceLabelNode", 0)]
		internal ExampleSpec WitnessLabelInLeafConstLabelNodeSequence(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				SequenceNode sequenceNode = keyValuePair.Value as SequenceNode;
				if (sequenceNode == null || sequenceNode.Children.Any<Node>())
				{
					return null;
				}
				dictionary[keyValuePair.Key] = sequenceNode.Label;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0601099D RID: 67997 RVA: 0x00392188 File Offset: 0x00390388
		[WitnessFunction("LeafConstSequenceLabelNode", 1)]
		internal ExampleSpec WitnessValueInLeafConstLabelNodeSequence(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				SequenceNode sequenceNode = keyValuePair.Value as SequenceNode;
				if (sequenceNode == null || sequenceNode.Children.Any<Node>())
				{
					return null;
				}
				dictionary[keyValuePair.Key] = this.ExtractAttributes(sequenceNode);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0601099E RID: 67998 RVA: 0x00392214 File Offset: 0x00390414
		[WitnessFunction("LeafConstSequenceLabelNode", 2)]
		internal ExampleSpec WitnessSeparatorInLeafSequence(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				SequenceNode sequenceNode = keyValuePair.Value as SequenceNode;
				if (sequenceNode == null || sequenceNode.Children.Any<Node>())
				{
					return null;
				}
				dictionary[keyValuePair.Key] = sequenceNode.Separator;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0601099F RID: 67999 RVA: 0x003922A0 File Offset: 0x003904A0
		[WitnessFunction("ConstSequenceLabelNode", 0)]
		internal ExampleSpec WitnessLabelInConstLabelNodeSequence(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				SequenceNode sequenceNode = keyValuePair.Value as SequenceNode;
				if (sequenceNode == null)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = sequenceNode.Label;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060109A0 RID: 68000 RVA: 0x00392320 File Offset: 0x00390520
		[WitnessFunction("ConstSequenceLabelNode", 1)]
		internal ExampleSpec WitnessValueInConstLabelNodeSequence(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				SequenceNode sequenceNode = keyValuePair.Value as SequenceNode;
				if (sequenceNode == null)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = this.ExtractAttributes(sequenceNode);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060109A1 RID: 68001 RVA: 0x003923A0 File Offset: 0x003905A0
		[WitnessFunction("ConstSequenceLabelNode", 2)]
		internal ExampleSpec WitnessSeparatorInSequence(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				SequenceNode sequenceNode = keyValuePair.Value as SequenceNode;
				if (sequenceNode == null)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = sequenceNode.Separator;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060109A2 RID: 68002 RVA: 0x00392420 File Offset: 0x00390620
		[WitnessFunction("ConstSequenceLabelNode", 3)]
		internal ExampleSpec WitnessNodeInAddChildrenSequence(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				SequenceNode sequenceNode = keyValuePair.Value as SequenceNode;
				if (sequenceNode == null)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = Tuple.Create<Node, Node[]>(sequenceNode, sequenceNode.Children);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060109A3 RID: 68003 RVA: 0x003924A4 File Offset: 0x003906A4
		[WitnessFunction("InsertAtAbs", 0)]
		[WitnessFunction("InsertAtRel", 0)]
		internal DisjunctiveExamplesSpec WitnessParentInInsertAt(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				Witnesses.ChildrenSpec childrenSpec = this.GetNodeAndNodeSeq(keyValuePair.Value);
				SequenceNode parent = childrenSpec.Parent as SequenceNode;
				if (parent == null || childrenSpec.Children == null)
				{
					return null;
				}
				Node[] array = Semantics.AllNodes(keyValuePair.Key[this._build.Symbol.selectedNode] as Node, (Node n) => n.Label == parent.Label && Attributes.StrongComparer.Default.Equals(n.Attributes, parent.Attributes) && childrenSpec.Children.Count == n.Children.Count<Node>() + 1 && n.Children.IsSubsequenceOf(childrenSpec.Children));
				if (array.IsEmpty<Node>())
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109A4 RID: 68004 RVA: 0x0039259C File Offset: 0x0039079C
		[WitnessFunction("InsertAtAbs", 1, DependsOnParameters = new int[] { 0 })]
		[WitnessFunction("InsertAtRel", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessPositionInInsertAt(GrammarRule rule, ExampleSpec spec, ExampleSpec prefixSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				State key = keyValuePair.Key;
				Witnesses.ChildrenSpec nodeAndNodeSeq = this.GetNodeAndNodeSeq(keyValuePair.Value);
				SequenceNode sequenceNode = prefixSpec.Examples[key] as SequenceNode;
				if (sequenceNode == null || nodeAndNodeSeq.Children == null)
				{
					return null;
				}
				int num = sequenceNode.Children.ZipWith(nodeAndNodeSeq.Children).TakeWhile((Record<Node, Node> n) => n.Item1.Equals(n.Item2)).Count<Record<Node, Node>>();
				if (num == sequenceNode.Children.Length)
				{
					dictionary[keyValuePair.Key] = Seq.Of<Node>(new Node[] { nodeAndNodeSeq.Children[num] });
				}
				else
				{
					dictionary[keyValuePair.Key] = Seq.Of<Node>(new Node[] { sequenceNode.Children[num] });
				}
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109A5 RID: 68005 RVA: 0x003926D4 File Offset: 0x003908D4
		[WitnessFunction("InsertAtAbs", 2, DependsOnParameters = new int[] { 0 })]
		[WitnessFunction("InsertAtRel", 2, DependsOnParameters = new int[] { 0 })]
		internal ExampleSpec WitnessNewNodeInInsertAt(GrammarRule rule, ExampleSpec spec, ExampleSpec prefixSpec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				State key = keyValuePair.Key;
				Witnesses.ChildrenSpec nodeAndNodeSeq = this.GetNodeAndNodeSeq(keyValuePair.Value);
				SequenceNode sequenceNode = prefixSpec.Examples[key] as SequenceNode;
				if (sequenceNode == null || nodeAndNodeSeq.Children == null)
				{
					return null;
				}
				int num = sequenceNode.Children.ZipWith(nodeAndNodeSeq.Children).TakeWhile((Record<Node, Node> n) => n.Item1.Equals(n.Item2)).Count<Record<Node, Node>>();
				dictionary[keyValuePair.Key] = nodeAndNodeSeq.Children[num];
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060109A6 RID: 68006 RVA: 0x003927C4 File Offset: 0x003909C4
		[WitnessFunction("DeleteChild", 0)]
		internal DisjunctiveExamplesSpec WitnessParentInSubSequence(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				Witnesses.ChildrenSpec childrenSpec = this.GetNodeAndNodeSeq(keyValuePair.Value);
				SequenceNode parent = childrenSpec.Parent as SequenceNode;
				if (parent == null || childrenSpec.Children == null)
				{
					return null;
				}
				Node[] array = Semantics.AllNodes(keyValuePair.Key[this._build.Symbol.selectedNode] as Node, (Node n) => n.Label == parent.Label && Attributes.StrongComparer.Default.Equals(n.Attributes, parent.Attributes) && childrenSpec.Children.Count == n.Children.Count<Node>() - 1 && childrenSpec.Children.IsSubsequenceOf(n.Children));
				if (array.IsEmpty<Node>())
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109A7 RID: 68007 RVA: 0x003928BC File Offset: 0x00390ABC
		[WitnessFunction("DeleteChild", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessChildInDeleteChild(GrammarRule rule, ExampleSpec spec, ExampleSpec prefixSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				State key = keyValuePair.Key;
				Witnesses.ChildrenSpec nodeAndNodeSeq = this.GetNodeAndNodeSeq(keyValuePair.Value);
				SequenceNode sequenceNode = prefixSpec.Examples[key] as SequenceNode;
				if (sequenceNode == null || nodeAndNodeSeq.Children == null)
				{
					return null;
				}
				int num = sequenceNode.Children.ZipWith(nodeAndNodeSeq.Children).TakeWhile((Record<Node, Node> n) => n.Item1.Equals(n.Item2)).Count<Record<Node, Node>>();
				dictionary[keyValuePair.Key] = Seq.Of<Node>(new Node[] { sequenceNode.Children[num] });
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109A8 RID: 68008 RVA: 0x003929BC File Offset: 0x00390BBC
		[WitnessFunction("ReplaceChildren", 0)]
		internal DisjunctiveExamplesSpec WitnessParentInReplaceChildren(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				Witnesses.ChildrenSpec nodeAndNodeSeq = this.GetNodeAndNodeSeq(keyValuePair.Value);
				SequenceNode parent = nodeAndNodeSeq.Parent as SequenceNode;
				if (parent == null || nodeAndNodeSeq.Children == null)
				{
					return null;
				}
				Node[] array = Semantics.AllNodes(keyValuePair.Key[this._build.Symbol.selectedNode] as Node, (Node node) => node.Label == parent.Label);
				List<Node> list = new List<Node>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Equals(parent))
					{
						return null;
					}
					if (Attributes.StrongComparer.Default.Equals(array[i].Attributes, parent.Attributes) && !nodeAndNodeSeq.Children.IsSubsequenceOf(array[i].Children))
					{
						Tuple<Node, List<int>, List<Node>> tuple = Edit.ComputeReplacedNodes(array[i], parent);
						if (tuple != null && tuple.Item2.Count != array[i].Children.Length)
						{
							list.Add(tuple.Item1);
						}
					}
				}
				if (list.IsEmpty<Node>())
				{
					return null;
				}
				dictionary[keyValuePair.Key] = list;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109A9 RID: 68009 RVA: 0x00392B68 File Offset: 0x00390D68
		[WitnessFunction("ReplaceChildren", 1, DependsOnParameters = new int[] { 0 })]
		internal ExampleSpec WitnessStartInReplaceChildren(GrammarRule rule, ExampleSpec spec, ExampleSpec prefixSpec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				State key = keyValuePair.Key;
				Witnesses.ChildrenSpec nodeAndNodeSeq = this.GetNodeAndNodeSeq(keyValuePair.Value);
				SequenceNode sequenceNode = prefixSpec.Examples[key] as SequenceNode;
				if (sequenceNode == null || nodeAndNodeSeq.Children == null)
				{
					return null;
				}
				SequenceNode sequenceNode2 = nodeAndNodeSeq.Parent as SequenceNode;
				Tuple<Node, List<int>, List<Node>> range = Edit.ComputeReplacedNodes(sequenceNode, sequenceNode2);
				if (range == null)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = range.Item2.Select((int i) => range.Item1.Children[i]).ToList<Node>();
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060109AA RID: 68010 RVA: 0x00392C60 File Offset: 0x00390E60
		[WitnessFunction("ReplaceChildren", 2, DependsOnParameters = new int[] { 0 })]
		internal ExampleSpec WitnessChildrenInReplaceChildren(GrammarRule rule, ExampleSpec spec, ExampleSpec prefixSpec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				State key = keyValuePair.Key;
				Witnesses.ChildrenSpec nodeAndNodeSeq = this.GetNodeAndNodeSeq(keyValuePair.Value);
				SequenceNode sequenceNode = prefixSpec.Examples[key] as SequenceNode;
				if (sequenceNode == null || nodeAndNodeSeq.Children == null)
				{
					return null;
				}
				SequenceNode sequenceNode2 = nodeAndNodeSeq.Parent as SequenceNode;
				Tuple<Node, List<int>, List<Node>> tuple = Edit.ComputeReplacedNodes(sequenceNode, sequenceNode2);
				if (tuple == null)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = new Witnesses.ChildrenSpec(tuple.Item3.First<Node>().Parent, tuple.Item3.ToList<Node>());
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060109AB RID: 68011 RVA: 0x00392D50 File Offset: 0x00390F50
		[WitnessFunction("ConcatChild", 0)]
		internal ExampleSpec WitnessPosInConcatChild(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				IEnumerable<Node> enumerable = keyValuePair.Value as IEnumerable<Node>;
				if (enumerable == null || enumerable.Count<Node>() < 2)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = enumerable.Take(1);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060109AC RID: 68012 RVA: 0x00392DD8 File Offset: 0x00390FD8
		[WitnessFunction("ConcatChild", 1)]
		internal ExampleSpec WitnessTailInConcatChild(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				IEnumerable<Node> enumerable = keyValuePair.Value as IEnumerable<Node>;
				if (enumerable == null || enumerable.Count<Node>() < 2)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = enumerable.Skip(1);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060109AD RID: 68013 RVA: 0x00392E60 File Offset: 0x00391060
		[WitnessFunction("SinglePosList", 0)]
		internal DisjunctiveExamplesSpec WitnessPosInSinglePosList(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				IEnumerable<Node> enumerable = keyValuePair.Value as IEnumerable<Node>;
				if (enumerable == null || enumerable.Count<Node>() != 1)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = enumerable;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109AE RID: 68014 RVA: 0x00392EE4 File Offset: 0x003910E4
		[WitnessFunction("AbsPos", 0)]
		internal DisjunctiveExamplesSpec WitnessKinAbsPos(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				State key = keyValuePair.Key;
				if (keyValuePair.Value.Cast<Node>().ToList<Node>().Any((Node n) => n.Parent == null))
				{
					return null;
				}
				List<int> list = new List<int>();
				foreach (object obj in keyValuePair.Value)
				{
					Node node = (Node)obj;
					int? num = node.Parent.Children.IndexOfByReference(node);
					list.Add(num.Value + 1);
					list.Add(num.Value - node.Parent.Children.Count<Node>());
				}
				if (list.Count == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = list.Cast<object>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109AF RID: 68015 RVA: 0x00393030 File Offset: 0x00391230
		[WitnessFunction("RelChild", 0)]
		internal DisjunctiveExamplesSpec WitnessNodeinRelChild(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				State key = keyValuePair.Key;
				IReadOnlyList<Node> readOnlyList = keyValuePair.Value.Cast<Node>().ToList<Node>();
				if (readOnlyList.Any((Node n) => n.Parent == null))
				{
					return null;
				}
				dictionary[keyValuePair.Key] = readOnlyList;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109B0 RID: 68016 RVA: 0x003930DC File Offset: 0x003912DC
		[WitnessFunction("Prepend", 0)]
		[WitnessFunction("PrependReplacement", 0)]
		internal DisjunctiveExamplesSpec WitnessNodeInPrepend(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				Witnesses.ChildrenSpec nodeAndNodeSeq = this.GetNodeAndNodeSeq(keyValuePair.Value);
				if (nodeAndNodeSeq.Children == null || nodeAndNodeSeq.Children.Count < 2)
				{
					return null;
				}
				if (!this.CanLearnChildrenList(nodeAndNodeSeq.Parent))
				{
					return null;
				}
				List<Node> list = nodeAndNodeSeq.Children.ToList<Node>();
				List<Tuple<Node, IReadOnlyList<Node>>> list2 = new List<Tuple<Node, IReadOnlyList<Node>>>();
				for (int i = 0; i < nodeAndNodeSeq.Children.Count; i++)
				{
					IReadOnlyList<Node> range = list.GetRange(0, i + 1);
					list2.Add(Tuple.Create<Node, IReadOnlyList<Node>>(nodeAndNodeSeq.Parent, range));
				}
				if (list2.IsEmpty<Tuple<Node, IReadOnlyList<Node>>>())
				{
					return null;
				}
				dictionary[keyValuePair.Key] = list2;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109B1 RID: 68017 RVA: 0x003931E8 File Offset: 0x003913E8
		[WitnessFunction("Prepend", 1, DependsOnParameters = new int[] { 0 })]
		[WitnessFunction("PrependReplacement", 1, DependsOnParameters = new int[] { 0 })]
		internal ExampleSpec WitnessElementsInPrepend(GrammarRule rule, ExampleSpec spec, ExampleSpec prefixSpec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				State key = keyValuePair.Key;
				Witnesses.ChildrenSpec nodeAndNodeSeq = this.GetNodeAndNodeSeq(keyValuePair.Value);
				Node[] array = prefixSpec.Examples[key] as Node[];
				if (array == null || nodeAndNodeSeq.Children == null)
				{
					return null;
				}
				if (nodeAndNodeSeq.Children.Count < array.Length)
				{
					return null;
				}
				if (!nodeAndNodeSeq.Children.Take(array.Length).SequenceEqual(array))
				{
					return null;
				}
				IReadOnlyList<Node> readOnlyList = nodeAndNodeSeq.Children.Skip(array.Length).ToList<Node>();
				dictionary[keyValuePair.Key] = Tuple.Create<Node, IReadOnlyList<Node>>(nodeAndNodeSeq.Parent, readOnlyList);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060109B2 RID: 68018 RVA: 0x003932EC File Offset: 0x003914EC
		[WitnessFunction("SingleList", 0)]
		internal DisjunctiveExamplesSpec WitnessFirstNodeInSingleList(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				List<Node> list = new List<Node>();
				foreach (object obj in keyValuePair.Value)
				{
					Witnesses.ChildrenSpec nodeAndNodeSeq = this.GetNodeAndNodeSeq(obj);
					if (nodeAndNodeSeq.Children != null && nodeAndNodeSeq.Children.Count == 1)
					{
						if (!this.CanLearnChildrenList(nodeAndNodeSeq.Parent))
						{
							return null;
						}
						list.Add(nodeAndNodeSeq.Children.Single<Node>());
					}
				}
				if (list.Count == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = list;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109B3 RID: 68019 RVA: 0x003933EC File Offset: 0x003915EC
		[WitnessFunction("ConvertSequence", 0)]
		internal DisjunctiveExamplesSpec WitnessParentInConvertSequence(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				Witnesses.ChildrenSpec childrenSpec = this.GetNodeAndNodeSeq(keyValuePair.Value);
				if (!(childrenSpec.Parent is SequenceNode) || childrenSpec.Children == null)
				{
					return null;
				}
				Node[] array = (from p in Semantics.InOrderAllNodes(keyValuePair.Key[this._build.Symbol.selectedNode] as Node)
					where Witnesses.<WitnessParentInConvertSequence>g__IsPotentialParent|65_0(p, childrenSpec)
					select p).ToArray<Node>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109B4 RID: 68020 RVA: 0x003934DC File Offset: 0x003916DC
		[WitnessFunction("ConvertSequence", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessSequenceMapInConvertSequence(GrammarRule rule, ExampleSpec outerSpec, ExampleSpec values)
		{
			ExampleSpec exampleSpec = new ExampleSpec(outerSpec.Examples.ToDictionary((KeyValuePair<State, object> kvp) => kvp.Key, (KeyValuePair<State, object> kvp) => this.GetNodeAndNodeSeq(kvp.Value).Children));
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in exampleSpec.ProvidedInputs)
			{
				State state2 = state.Bind(this._build.Symbol.parent, values.Examples[state]);
				IEnumerable<object> enumerable;
				if (!dictionary.TryGetValue(state2, out enumerable))
				{
					dictionary[state2] = exampleSpec.DisjunctiveExamples[state];
				}
				else
				{
					IReadOnlyList<object> readOnlyList = enumerable.Intersect(exampleSpec.DisjunctiveExamples[state], ValueEquality.Comparer).ToList<object>();
					if (readOnlyList.IsEmpty<object>())
					{
						return null;
					}
					dictionary[state2] = readOnlyList;
				}
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060109B5 RID: 68021 RVA: 0x003935F0 File Offset: 0x003917F0
		private Witnesses.ChildrenSpec GetNodeAndNodeSeq(object value)
		{
			Tuple<Node, Node[]> tuple = value as Tuple<Node, Node[]>;
			if (tuple != null)
			{
				return new Witnesses.ChildrenSpec(tuple.Item1, tuple.Item2);
			}
			Tuple<Node, IReadOnlyList<Node>> tuple2 = value as Tuple<Node, IReadOnlyList<Node>>;
			if (tuple2 != null)
			{
				return new Witnesses.ChildrenSpec(tuple2.Item1, tuple2.Item2);
			}
			if (value is Witnesses.ChildrenSpec)
			{
				return (Witnesses.ChildrenSpec)value;
			}
			return new Witnesses.ChildrenSpec(null, null);
		}

		// Token: 0x060109BD RID: 68029 RVA: 0x0039378C File Offset: 0x0039198C
		[CompilerGenerated]
		internal static bool <WitnessParentInConvertSequence>g__IsPotentialParent|65_0(Node parent, Witnesses.ChildrenSpec childrenSpec)
		{
			IReadOnlyList<Node> children = childrenSpec.Children;
			if (!(parent is SequenceNode) || parent.Children.Length != children.Count || parent.Equals(childrenSpec.Parent))
			{
				return false;
			}
			foreach (Record<Node, Node> record in parent.Children.ZipWith(children))
			{
				Node node;
				Node node2;
				record.Deconstruct(out node, out node2);
				Node node3 = node;
				Node node4 = node2;
				IEnumerable<Node> enumerable = Semantics.AllNodes(node3).ConvertToHashSet<Node>();
				HashSet<Node> hashSet = Semantics.AllNodes(node4).ConvertToHashSet<Node>();
				if ((from n in enumerable.Intersect(hashSet, EqualityComparer<Node>.Default)
					where n.HasPosition
					select n).IsEmpty<Node>())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04006327 RID: 25383
		private const int PredicateUpperBound = 500;

		// Token: 0x04006328 RID: 25384
		public static readonly string[] Keywords = new string[] { "(", ")", ",", ";" };

		// Token: 0x04006329 RID: 25385
		private readonly GrammarBuilders _build;

		// Token: 0x0400632A RID: 25386
		private readonly Witnesses.Options _options;

		// Token: 0x0400632B RID: 25387
		private readonly IFeature _ranking;

		// Token: 0x02001EB7 RID: 7863
		private struct ChildrenSpec
		{
			// Token: 0x060109BF RID: 68031 RVA: 0x00393880 File Offset: 0x00391A80
			public ChildrenSpec(Node parent, IReadOnlyList<Node> children)
			{
				this.Parent = parent;
				this.Children = children;
			}

			// Token: 0x0400632C RID: 25388
			public readonly Node Parent;

			// Token: 0x0400632D RID: 25389
			public readonly IReadOnlyList<Node> Children;
		}

		// Token: 0x02001EB8 RID: 7864
		public class Options : DSLOptions
		{
			// Token: 0x17002C02 RID: 11266
			// (get) Token: 0x060109C0 RID: 68032 RVA: 0x00393890 File Offset: 0x00391A90
			// (set) Token: 0x060109C1 RID: 68033 RVA: 0x00393898 File Offset: 0x00391A98
			public Dictionary<string, HashSet<string>> ForbiddenAttributesByLabel { get; set; }

			// Token: 0x17002C03 RID: 11267
			// (get) Token: 0x060109C2 RID: 68034 RVA: 0x003938A1 File Offset: 0x00391AA1
			// (set) Token: 0x060109C3 RID: 68035 RVA: 0x003938A9 File Offset: 0x00391AA9
			public int? TreeSizeThreshold { get; set; }

			// Token: 0x17002C04 RID: 11268
			// (get) Token: 0x060109C4 RID: 68036 RVA: 0x003938B2 File Offset: 0x00391AB2
			// (set) Token: 0x060109C5 RID: 68037 RVA: 0x003938BA File Offset: 0x00391ABA
			public Dictionary<string, double> AttributeNameScore { get; set; }

			// Token: 0x17002C05 RID: 11269
			// (get) Token: 0x060109C6 RID: 68038 RVA: 0x003938C3 File Offset: 0x00391AC3
			// (set) Token: 0x060109C7 RID: 68039 RVA: 0x003938CB File Offset: 0x00391ACB
			public HashSet<string> HasNChildrenSequenceNodes { get; set; } = new HashSet<string>();
		}
	}
}

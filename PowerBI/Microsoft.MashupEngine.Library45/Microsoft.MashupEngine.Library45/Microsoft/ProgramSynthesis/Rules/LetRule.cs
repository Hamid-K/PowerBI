using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Features.InputTransformation;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x02000390 RID: 912
	[DataContract]
	public class LetRule : NonterminalRule
	{
		// Token: 0x06001487 RID: 5255 RVA: 0x0003BE98 File Offset: 0x0003A098
		private LetRule(Symbol head, Symbol variable, Symbol value, Symbol letBody)
			: base(head, new Symbol[] { value, letBody })
		{
			this.Variable = variable;
			this.Value = value;
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0003BEBE File Offset: 0x0003A0BE
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x0003BEC6 File Offset: 0x0003A0C6
		[DataMember]
		public Symbol Variable { get; private set; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0003BECF File Offset: 0x0003A0CF
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x0003BED7 File Offset: 0x0003A0D7
		[DataMember]
		public Symbol Value { get; private set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0003BEE0 File Offset: 0x0003A0E0
		public Symbol LetBody
		{
			get
			{
				return base.Body[base.Body.Count - 1];
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0003BEFA File Offset: 0x0003A0FA
		private static string InternalLetVariable
		{
			get
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}", new object[]
				{
					"_LetB",
					LetRule._letCount++
				}));
			}
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0003BF2E File Offset: 0x0003A12E
		internal static bool LikelyIsInternalLetName(string name)
		{
			return name.StartsWith("_LetB", StringComparison.Ordinal);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0003BF3C File Offset: 0x0003A13C
		public static LetRule Create(Symbol head, Symbol variable, GrammarRule valueRule, GrammarRule bodyRule)
		{
			Symbol symbol = LetRule.AddWithReplacedHead(head.Grammar, valueRule, variable.GrammarType);
			Symbol symbol2 = LetRule.AddWithReplacedHead(head.Grammar, bodyRule, head.GrammarType);
			return new LetRule(head, variable, symbol, symbol2);
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0003BF78 File Offset: 0x0003A178
		private static Symbol AddWithReplacedHead(Grammar grammar, GrammarRule rule, GrammarType actualType)
		{
			ConversionRule conversionRule = rule as ConversionRule;
			if (conversionRule != null && conversionRule.IsTrivial)
			{
				return conversionRule.Body[0];
			}
			Symbol symbol = grammar.AddSymbol(LetRule.InternalLetVariable, actualType, false);
			symbol.OriginLocation = rule.OriginLocation;
			rule.Head = symbol;
			grammar.AddRule(rule);
			return symbol;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0003BFD0 File Offset: 0x0003A1D0
		internal override State ValidStateFromArgumentInvocations(params Record<State, object>[] argumentInvocations)
		{
			State state2;
			State state = (state2 = argumentInvocations[0].Item1);
			if (!state.Equals(state))
			{
				return null;
			}
			object item = argumentInvocations[0].Item2;
			state2 = state2.Bind(this.Variable, item);
			State item2 = argumentInvocations[1].Item1;
			if (item2.Equals(state2))
			{
				return item2;
			}
			return null;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0003C029 File Offset: 0x0003A229
		private IEnumerable<KeyValuePair<object, ProgramSet>> CollectLetClusters(JoinProgramSet space, State inputState)
		{
			foreach (KeyValuePair<object, ProgramSet> valueCluster in space.ParameterSpaces[0].ClusterOnInput(inputState))
			{
				State state = inputState.Bind(this.Variable, valueCluster.Key);
				foreach (KeyValuePair<object, ProgramSet> keyValuePair in space.ParameterSpaces[1].ClusterOnInput(state))
				{
					yield return new KeyValuePair<object, ProgramSet>(keyValuePair.Key, new JoinProgramSet(this, new ProgramSet[] { valueCluster.Value, keyValuePair.Value }));
				}
				Dictionary<object, ProgramSet>.Enumerator enumerator2 = default(Dictionary<object, ProgramSet>.Enumerator);
				valueCluster = default(KeyValuePair<object, ProgramSet>);
			}
			Dictionary<object, ProgramSet>.Enumerator enumerator = default(Dictionary<object, ProgramSet>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0003C048 File Offset: 0x0003A248
		internal override Dictionary<object, ProgramSet> Cluster(JoinProgramSet space, State inputState)
		{
			return this.CollectLetClusters(space, inputState).GroupBy((KeyValuePair<object, ProgramSet> kvp) => kvp.Key, ValueEquality.Comparer).ToDictionary((IGrouping<object, KeyValuePair<object, ProgramSet>> g) => g.Key, (IGrouping<object, KeyValuePair<object, ProgramSet>> g) => g.Select((KeyValuePair<object, ProgramSet> kvp) => kvp.Value).ToList<ProgramSet>().NormalizedUnion(), ValueEquality.Comparer);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0003C0D0 File Offset: 0x0003A2D0
		protected override void InitializeStandardWitnessFunctions(int parameter)
		{
			base.InitializeStandardWitnessFunctions(parameter);
			if (parameter != 1)
			{
				return;
			}
			base.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(LetRule.WitnessLetBody_DisjunctiveExamplesSpec(LetRule, DisjunctiveExamplesSpec, ExampleSpec)), new Expression[]
			{
				Expression.Constant(this, typeof(LetRule)),
				Expression.Constant(null, typeof(DisjunctiveExamplesSpec)),
				Expression.Constant(null, typeof(ExampleSpec))
			}), Array.Empty<ParameterExpression>()), parameter, null);
			base.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(LetRule.WitnessLetBody_GroupedExamplesSpec(LetRule, GroupedExamplesSpec, ExampleSpec)), new Expression[]
			{
				Expression.Constant(this, typeof(LetRule)),
				Expression.Constant(null, typeof(GroupedExamplesSpec)),
				Expression.Constant(null, typeof(ExampleSpec))
			}), Array.Empty<ParameterExpression>()), parameter, null);
			base.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(LetRule.WitnessLetBody_OutputNotNullSpec(LetRule, OutputNotNullSpec, ExampleSpec)), new Expression[]
			{
				Expression.Constant(this, typeof(LetRule)),
				Expression.Constant(null, typeof(OutputNotNullSpec)),
				Expression.Constant(null, typeof(ExampleSpec))
			}), Array.Empty<ParameterExpression>()), parameter, null);
			base.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(LetRule.WitnessLetBody_PrefixSpec(LetRule, PrefixSpec, ExampleSpec)), new Expression[]
			{
				Expression.Constant(this, typeof(LetRule)),
				Expression.Constant(null, typeof(PrefixSpec)),
				Expression.Constant(null, typeof(ExampleSpec))
			}), Array.Empty<ParameterExpression>()), parameter, null);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0003C281 File Offset: 0x0003A481
		public override TResult Accept<TResult, TArgs>(GrammarRuleVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitLetRule(this, args);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0003C28B File Offset: 0x0003A48B
		public override ProgramNode BuildASTNode(object data, params ProgramNode[] children)
		{
			return new LetNode(this, children[0], children[1]);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0003C299 File Offset: 0x0003A499
		internal override object Evaluate(object[] args)
		{
			return args[args.Length - 1];
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0003C2A4 File Offset: 0x0003A4A4
		internal override CodeBuilder FormatAST(IEnumerable<CodeBuilder> parameters, ASTSerializationSettings settings)
		{
			IList<CodeBuilder> list = (parameters as IList<CodeBuilder>) ?? parameters.ToList<CodeBuilder>();
			CodeBuilder codeBuilder = CodeBuilder.Create("let ");
			using (codeBuilder.NewScope(null, settings.IndentIncrement))
			{
				codeBuilder.Append(this.Variable.Name);
				codeBuilder.Append(" = ");
				codeBuilder.Append(list[0]);
			}
			codeBuilder.Append(" in ");
			if (settings.HasIndent)
			{
				codeBuilder.AppendLine();
			}
			codeBuilder.Append(list[1]);
			return codeBuilder;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0003C34C File Offset: 0x0003A54C
		[WitnessFunction(1, DependsOnParameters = new int[] { 0 })]
		private static DisjunctiveExamplesSpec WitnessLetBody_DisjunctiveExamplesSpec(LetRule rule, DisjunctiveExamplesSpec spec, ExampleSpec values)
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
					IReadOnlyList<object> readOnlyList = enumerable.Intersect(spec.DisjunctiveExamples[state], ValueEquality.Comparer).ToList<object>();
					if (readOnlyList.IsEmpty<object>())
					{
						return null;
					}
					dictionary[state2] = readOnlyList;
				}
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0003C410 File Offset: 0x0003A610
		[WitnessFunction(1, DependsOnParameters = new int[] { 0 })]
		private static GroupedExamplesSpec WitnessLetBody_GroupedExamplesSpec(LetRule rule, GroupedExamplesSpec spec, ExampleSpec values)
		{
			return new GroupedExamplesSpec(spec.ExamplesWithCounts.ToDictionary((KeyValuePair<State, KeyValuePair<object, uint>> pair) => pair.Key.Bind(rule.Variable, values.Examples[pair.Key]), (KeyValuePair<State, KeyValuePair<object, uint>> pair) => pair.Value));
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0003C46C File Offset: 0x0003A66C
		[WitnessFunction(1, DependsOnParameters = new int[] { 0 })]
		private static OutputNotNullSpec WitnessLetBody_OutputNotNullSpec(LetRule rule, OutputNotNullSpec spec, ExampleSpec values)
		{
			return new OutputNotNullSpec(spec.ProvidedInputs.Select((State input) => input.Bind(rule.Variable, values.Examples[input])).ToList<State>());
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0003C4B0 File Offset: 0x0003A6B0
		[WitnessFunction(1, DependsOnParameters = new int[] { 0 })]
		private static PrefixSpec WitnessLetBody_PrefixSpec(LetRule rule, PrefixSpec spec, ExampleSpec values)
		{
			Dictionary<State, Record<IEnumerable<object>, IEnumerable<object>>> dictionary = new Dictionary<State, Record<IEnumerable<object>, IEnumerable<object>>>();
			foreach (State state in spec.ProvidedInputs)
			{
				State state2 = state.Bind(rule.Variable, values.Examples[state]);
				Record<IEnumerable<object>, IEnumerable<object>> record;
				if (!dictionary.TryGetValue(state2, out record))
				{
					dictionary[state2] = Record.Create<IEnumerable<object>, IEnumerable<object>>(spec.PositiveExamples[state], spec.NegativeExamples[state].ToEnumerable<HashSet<object>>());
				}
				else
				{
					bool flag = false;
					bool flag2 = false;
					using (IEnumerator<object> enumerator2 = record.Item1.GetEnumerator())
					{
						using (IEnumerator<object> enumerator3 = spec.PositiveExamples[state].GetEnumerator())
						{
							bool flag3 = true;
							while (flag3 && (flag2 = enumerator3.MoveNext()) && (flag = enumerator2.MoveNext()))
							{
								flag3 &= ValueEquality.Comparer.Equals(enumerator2.Current, enumerator3.Current);
							}
						}
					}
					if (!flag2)
					{
						dictionary[state2] = Record.Create<IEnumerable<object>, IEnumerable<object>>(record.Item1, record.Item2.Union(spec.NegativeExamples[state]));
					}
					else
					{
						if (flag)
						{
							return null;
						}
						dictionary[state2] = Record.Create<IEnumerable<object>, IEnumerable<object>>(spec.PositiveExamples[state], record.Item2.Union(spec.NegativeExamples[state]));
					}
				}
			}
			return new PrefixSpec(dictionary);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0003C684 File Offset: 0x0003A884
		internal override IEnumerable<Symbol> RequiredChildrenForFeatureCalculator(FeatureCalculator fc)
		{
			if (fc is LetRule.DefaultFeatureCalculator)
			{
				return new Symbol[] { this.LetBody };
			}
			return base.RequiredChildrenForFeatureCalculator(fc);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0003C6A5 File Offset: 0x0003A8A5
		internal override FeatureCalculator BuildDefaultFeatureCalculator(FeatureInfo feature)
		{
			return new LetRule.DefaultFeatureCalculator(feature);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0003C6B0 File Offset: 0x0003A8B0
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			if (fcc == null || fcc.ComputeSpecInputs().Count == 0 || programSet.Rule != this)
			{
				return NonterminalRule.GenericTopKStream(programSet, feature, k, null, null);
			}
			int num;
			int num2;
			NonterminalRule.ComputeParamK(k, programSet.ParameterSpaces[0].Size, programSet.ParameterSpaces[1].Size).Deconstruct(out num, out num2);
			int num3 = num;
			int bodyParamK = num2;
			IEnumerable<ProgramNode> enumerable = programSet.ParameterSpaces[0].TopK(feature.GetExternFeature(this, 0), num3, fcc, logListener);
			ProgramSet bodySpace = programSet.ParameterSpaces[1];
			return enumerable.SelectMany(delegate(ProgramNode bindingProgram)
			{
				FeatureCalculationContext featureCalculationContext = fcc.WithAdditionalTransform(new LetBodyInputTransformer(this, bindingProgram));
				return from p in bodySpace.TopK(feature.GetExternFeature(this, 1), bodyParamK, featureCalculationContext, logListener)
					select this.BuildASTNode(bindingProgram, p);
			}).Distinct<ProgramNode>();
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0003C791 File Offset: 0x0003A991
		public override IInputTransformer GetInputTransformer(ProgramNode p, int childIndex)
		{
			if (childIndex != 1)
			{
				return null;
			}
			return new LetBodyInputTransformer(this, p.Children[0]);
		}

		// Token: 0x04000A1C RID: 2588
		private static int _letCount;

		// Token: 0x04000A1D RID: 2589
		internal const int LetBindingIndex = 0;

		// Token: 0x04000A1E RID: 2590
		internal const int LetBodyIndex = 1;

		// Token: 0x04000A1F RID: 2591
		internal const int NumBindings = 1;

		// Token: 0x04000A22 RID: 2594
		private const string InternalLetVariablePrefix = "_LetB";

		// Token: 0x02000391 RID: 913
		[DataContract]
		internal class DefaultFeatureCalculator : FeatureCalculator
		{
			// Token: 0x060014A1 RID: 5281 RVA: 0x0003BE71 File Offset: 0x0003A071
			public DefaultFeatureCalculator(FeatureInfo feature)
				: base(feature, false)
			{
			}

			// Token: 0x060014A2 RID: 5282 RVA: 0x0003C7A8 File Offset: 0x0003A9A8
			public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
			{
				ProgramNode bodyNode = ((LetNode)program).BodyNode;
				LearningInfo learningInfo2 = ((learningInfo != null) ? learningInfo.ForChild(1) : null);
				return bodyNode.GetFeatureValue(instance, learningInfo2);
			}
		}
	}
}

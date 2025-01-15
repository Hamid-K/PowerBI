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
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x0200038E RID: 910
	[DataContract]
	public class LambdaRule : NonterminalRule
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x0003BB5B File Offset: 0x00039D5B
		private LambdaRule(Symbol head, Symbol variable, Symbol body)
			: base(head, new Symbol[] { body })
		{
			this.Variable = variable;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0003BB75 File Offset: 0x00039D75
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x0003BB7D File Offset: 0x00039D7D
		[DataMember]
		public Symbol Variable { get; private set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol LambdaBody
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0003BB94 File Offset: 0x00039D94
		private static string LambdaHeadSymbolName
		{
			get
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("_LFun{0}", new object[] { LambdaRule._lambdaCount++ }));
			}
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0003BBC0 File Offset: 0x00039DC0
		public static LambdaRule Create(Symbol variable, Symbol body)
		{
			GrammarType grammarType = GrammarType.MakeFunctionType(new GrammarType[] { variable.GrammarType, body.GrammarType });
			return new LambdaRule(body.Grammar.AddSymbol(LambdaRule.LambdaHeadSymbolName, grammarType, false), variable, body);
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0003BC04 File Offset: 0x00039E04
		internal override Dictionary<object, ProgramSet> Cluster(JoinProgramSet space, State inputState)
		{
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Trying to cluster a LambdaRule VSA on a state {0}. Should be unreachable", new object[] { inputState })));
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0003BC24 File Offset: 0x00039E24
		public override TResult Accept<TResult, TArgs>(GrammarRuleVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitLambdaRule(this, args);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0003AC32 File Offset: 0x00038E32
		public override ProgramNode BuildASTNode(object data, params ProgramNode[] children)
		{
			return this.BuildASTNode(children[0]);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0003BC30 File Offset: 0x00039E30
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			if (fcc == null || fcc.ComputeSpecInputs().Count == 0 || programSet.Rule != this)
			{
				return NonterminalRule.GenericTopKStream(programSet, feature, k, null, logListener);
			}
			FeatureCalculationContext featureCalculationContext = fcc.WithAdditionalTransform(new LambdaBodyInputTransformer(this.Variable));
			return NonterminalRule.GenericTopKStream(programSet, feature, k, featureCalculationContext, logListener);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0003BC82 File Offset: 0x00039E82
		public override ProgramNode BuildASTNode(ProgramNode child)
		{
			return new LambdaNode(this, child);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0003BC8C File Offset: 0x00039E8C
		protected override void InitializeStandardWitnessFunctions(int parameter)
		{
			base.InitializeStandardWitnessFunctions(parameter);
			if (parameter != 0)
			{
				return;
			}
			base.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(LambdaRule.WitnessLambdaBody_ExampleSpec(LambdaRule, ExampleSpec)), new Expression[]
			{
				Expression.Constant(this, typeof(LambdaRule)),
				Expression.Constant(null, typeof(ExampleSpec))
			}), Array.Empty<ParameterExpression>()), parameter, null);
			base.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(LambdaRule.WitnessLambdaBody_OutputNonEmptySpec(LambdaRule, OutputNonEmptySpec)), new Expression[]
			{
				Expression.Constant(this, typeof(LambdaRule)),
				Expression.Constant(null, typeof(OutputNonEmptySpec))
			}), Array.Empty<ParameterExpression>()), parameter, null);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x000170F6 File Offset: 0x000152F6
		internal override object Evaluate(object[] args)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000170F6 File Offset: 0x000152F6
		internal override State ValidStateFromArgumentInvocations(params Record<State, object>[] argumentInvocations)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0003BD4A File Offset: 0x00039F4A
		internal override CodeBuilder FormatAST(IEnumerable<CodeBuilder> parameters, ASTSerializationSettings settings)
		{
			CodeBuilder codeBuilder = CodeBuilder.Create("\\");
			codeBuilder.Append(this.Variable.Name);
			codeBuilder.Append(" => ");
			codeBuilder.Append(parameters.First<CodeBuilder>());
			return codeBuilder;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0003BD80 File Offset: 0x00039F80
		[WitnessFunction(0)]
		private static ExampleSpec WitnessLambdaBody_ExampleSpec(LambdaRule rule, ExampleSpec spec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				State key = keyValuePair.Key;
				object value = keyValuePair.Value;
				dictionary.Add(key.BindFunctionalInput(rule.Variable), value);
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0003BDF8 File Offset: 0x00039FF8
		[WitnessFunction(0)]
		private static OutputNonEmptySpec WitnessLambdaBody_OutputNonEmptySpec(LambdaRule rule, OutputNonEmptySpec spec)
		{
			List<State> list = new List<State>();
			foreach (State state in spec.ProvidedInputs)
			{
				list.Add(state.BindFunctionalInput(rule.Variable));
			}
			return new OutputNonEmptySpec(list);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0003BE5C File Offset: 0x0003A05C
		internal override FeatureCalculator BuildDefaultFeatureCalculator(FeatureInfo feature)
		{
			return new LambdaRule.DefaultFeatureCalculator(feature);
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0003BE64 File Offset: 0x0003A064
		public override IInputTransformer GetInputTransformer(ProgramNode p, int childIndex)
		{
			return new LambdaBodyInputTransformer(this.Variable);
		}

		// Token: 0x04000A1A RID: 2586
		private static int _lambdaCount;

		// Token: 0x0200038F RID: 911
		[DataContract]
		internal class DefaultFeatureCalculator : FeatureCalculator
		{
			// Token: 0x06001485 RID: 5253 RVA: 0x0003BE71 File Offset: 0x0003A071
			public DefaultFeatureCalculator(FeatureInfo feature)
				: base(feature, false)
			{
			}

			// Token: 0x06001486 RID: 5254 RVA: 0x0003BE7B File Offset: 0x0003A07B
			public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
			{
				return program.Children[0].GetFeatureValue(instance, (learningInfo != null) ? learningInfo.ForChild(0) : null);
			}
		}
	}
}

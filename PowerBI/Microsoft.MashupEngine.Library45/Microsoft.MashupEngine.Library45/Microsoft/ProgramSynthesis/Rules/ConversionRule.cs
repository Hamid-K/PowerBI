using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x02000382 RID: 898
	[DataContract]
	public class ConversionRule : OperatorRule
	{
		// Token: 0x06001406 RID: 5126 RVA: 0x0003AAB8 File Offset: 0x00038CB8
		public ConversionRule(Symbol head, Symbol body, ExternalSymbolUsage externalSymbolUsage = null)
			: base(FormattableString.Invariant(FormattableStringFactory.Create("~convert_{0}_{1}", new object[] { head, body })), head, new Symbol[] { body })
		{
			this.ExternSymbolUsage = externalSymbolUsage ?? new ExternalSymbolUsage(null);
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0003AB04 File Offset: 0x00038D04
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x0003AB0C File Offset: 0x00038D0C
		[DataMember]
		public ExternalSymbolUsage ExternSymbolUsage { get; private set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0003AB15 File Offset: 0x00038D15
		public Dictionary<Symbol, Symbol> Substitutions
		{
			get
			{
				return this.ExternSymbolUsage.Substitutions;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x0003AB22 File Offset: 0x00038D22
		public bool IsTrivial
		{
			get
			{
				return this.Substitutions.Count == 0;
			}
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0003AB32 File Offset: 0x00038D32
		protected override MethodReference<OperatorRule.OperatorSemantics> InitializeSemantics()
		{
			return MethodReference.WithoutReference<OperatorRule.OperatorSemantics>((object[] args) => args[0]);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0000CC37 File Offset: 0x0000AE37
		internal override void ValidateSemantics(DiagnosticsContext diagnosticsContext)
		{
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0003AB58 File Offset: 0x00038D58
		internal State ApplySubstitutions(State state, bool inverse = false)
		{
			return this.Substitutions.Aggregate(state, delegate(State acc, KeyValuePair<Symbol, Symbol> sub)
			{
				if (!inverse)
				{
					return acc.Substitute(sub.Value, sub.Key);
				}
				return acc.Substitute(sub.Key, sub.Value);
			});
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0003AB8A File Offset: 0x00038D8A
		internal override Dictionary<object, ProgramSet> Cluster(JoinProgramSet space, State inputState)
		{
			return base.Cluster(space, this.ApplySubstitutions(inputState, false));
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0003AB9B File Offset: 0x00038D9B
		public override TResult Accept<TResult, TArgs>(GrammarRuleVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitConversionRule(this, args);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0003ABA8 File Offset: 0x00038DA8
		internal override CodeBuilder FormatAST(IEnumerable<CodeBuilder> parameters, ASTSerializationSettings settings)
		{
			if (this.IsTrivial)
			{
				return parameters.First<CodeBuilder>();
			}
			CodeBuilder codeBuilder = CodeBuilder.Create(base.Id);
			codeBuilder.Append("(");
			using (codeBuilder.NewScope(null, settings.IndentIncrement))
			{
				codeBuilder.Append(parameters.First<CodeBuilder>());
			}
			codeBuilder.Append(")");
			return codeBuilder;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0003AC20 File Offset: 0x00038E20
		public override ProgramNode BuildASTNode(ProgramNode child)
		{
			return new NonterminalNode(this, new ProgramNode[] { child });
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0003AC32 File Offset: 0x00038E32
		public override ProgramNode BuildASTNode(object data, params ProgramNode[] children)
		{
			return this.BuildASTNode(children[0]);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0003AC40 File Offset: 0x00038E40
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			FeatureCalculationContext featureCalculationContext = ((fcc != null) ? fcc.TransformForConversionRule(this) : null);
			return NonterminalRule.GenericTopKStream(programSet, feature, k, featureCalculationContext, logListener);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0003AC68 File Offset: 0x00038E68
		internal override FeatureCalculator BuildDefaultFeatureCalculator(FeatureInfo feature)
		{
			return new ConversionRule.DefaultFeatureCalculator(this, feature);
		}

		// Token: 0x02000383 RID: 899
		[DataContract]
		public class DefaultFeatureCalculator : FeatureCalculator
		{
			// Token: 0x06001415 RID: 5141 RVA: 0x0003AC71 File Offset: 0x00038E71
			public DefaultFeatureCalculator(ConversionRule rule, FeatureInfo feature)
				: base(feature, false)
			{
				this._rule = rule;
			}

			// Token: 0x06001416 RID: 5142 RVA: 0x0003AC84 File Offset: 0x00038E84
			public override object Calculate(ProgramNode program, LearningInfo learningInfo = null, IFeature instance = null)
			{
				IFeature feature = ((instance != null) ? instance.GetExternFeature(this._rule, 0) : null);
				ProgramNode programNode = program.Children[0];
				LearningInfo learningInfo2 = ((learningInfo != null) ? learningInfo.ForChild(0) : null);
				object featureValue = programNode.GetFeatureValue(feature, learningInfo2);
				if (feature == null || feature.Equals(instance))
				{
					return featureValue;
				}
				object obj;
				try
				{
					obj = this._rule.ExternSymbolUsage.ConvertFromExternFeatureValue(base.Feature, feature.Info, featureValue);
				}
				catch (InvalidCastException)
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot convert the result of feature {0} of type {1} to type {2} of the feature {3}", new object[]
					{
						feature,
						feature.Info.PropertyType,
						base.Feature.PropertyType,
						base.Feature
					})));
				}
				return obj;
			}

			// Token: 0x040009F2 RID: 2546
			[DataMember]
			private readonly ConversionRule _rule;
		}
	}
}

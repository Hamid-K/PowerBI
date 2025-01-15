using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Features.InputTransformation;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003D4 RID: 980
	[Concept("TakeWhile")]
	[DataContract]
	public sealed class TakeWhile : ConceptRule
	{
		// Token: 0x060015DB RID: 5595 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private TakeWhile(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Predicate
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Set
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0003FD34 File Offset: 0x0003DF34
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Constructor(typeof(Func<, >), new ConceptRule.TypeParam[]
					{
						ConceptRule.TP.Generic("A"),
						ConceptRule.TP.Primitive(typeof(bool))
					}),
					ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") })
				}, ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") }));
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x0003FDCC File Offset: 0x0003DFCC
		protected override object Evaluate(object[] args)
		{
			IFunctionalSymbol1 predicate = args[0] as IFunctionalSymbol1;
			return args[1].ToEnumerable<object>().TakeWhile(delegate(object e)
			{
				object obj = predicate.Evaluate(e);
				return obj != null && (bool)obj;
			});
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0003FE08 File Offset: 0x0003E008
		public override IInputTransformer GetInputTransformer(ProgramNode p, int childIndex)
		{
			if (base.Body[this.DslBodyMapping[childIndex].ConceptIndex].LambdaRule == null)
			{
				return null;
			}
			return new SequenceTransformConceptLambdaInputTransformer(p.Children[1], this.Predicate.LambdaRule.Variable);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0003FE5A File Offset: 0x0003E05A
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			return ConceptTopKStreamHelper.SequenceTransformerTopKStream(programSet, feature, 1, 0, (ProgramNode generator, ProgramNode transformer) => this.BuildASTNode(transformer, generator), k, fcc, logListener);
		}
	}
}

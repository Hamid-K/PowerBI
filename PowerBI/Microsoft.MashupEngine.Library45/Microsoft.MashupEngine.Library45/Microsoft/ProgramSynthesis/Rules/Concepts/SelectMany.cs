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
	// Token: 0x020003CE RID: 974
	[Concept("SelectMany")]
	[DataContract]
	public sealed class SelectMany : ConceptRule
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private SelectMany(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Function
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol List
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x0003F8A4 File Offset: 0x0003DAA4
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Constructor(typeof(Func<, >), new ConceptRule.TypeParam[]
					{
						ConceptRule.TP.Generic("A"),
						ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("B") })
					}),
					ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") })
				}, ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("B") }));
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0003F950 File Offset: 0x0003DB50
		protected override object Evaluate(object[] args)
		{
			IFunctionalSymbol1 function = args[0] as IFunctionalSymbol1;
			return args[1].ToEnumerable<object>().SelectMany(delegate(object e)
			{
				object obj = function.Evaluate(e);
				if (obj != null)
				{
					return obj.ToEnumerable<object>();
				}
				return Enumerable.Empty<object>();
			});
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0003F98C File Offset: 0x0003DB8C
		public override IInputTransformer GetInputTransformer(ProgramNode p, int childIndex)
		{
			if (base.Body[this.DslBodyMapping[childIndex].ConceptIndex].LambdaRule == null)
			{
				return null;
			}
			return new SequenceTransformConceptLambdaInputTransformer(p.Children[1], this.Function.LambdaRule.Variable);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0003F9DE File Offset: 0x0003DBDE
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			return ConceptTopKStreamHelper.SequenceTransformerTopKStream(programSet, feature, 1, 0, (ProgramNode generator, ProgramNode transformer) => this.BuildASTNode(transformer, generator), k, fcc, logListener);
		}
	}
}

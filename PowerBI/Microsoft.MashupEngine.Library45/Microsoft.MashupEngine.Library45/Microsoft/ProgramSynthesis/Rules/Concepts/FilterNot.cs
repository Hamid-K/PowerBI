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
	// Token: 0x020003C1 RID: 961
	[Concept("FilterNot")]
	[DataContract]
	public sealed class FilterNot : ConceptRule
	{
		// Token: 0x0600157E RID: 5502 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private FilterNot(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Predicate
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Set
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x0003EC80 File Offset: 0x0003CE80
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

		// Token: 0x06001582 RID: 5506 RVA: 0x0003ED18 File Offset: 0x0003CF18
		protected override object Evaluate(object[] args)
		{
			IFunctionalSymbol1 predicate = args[0] as IFunctionalSymbol1;
			return args[1].ToEnumerable<object>().Where(delegate(object e)
			{
				object obj = predicate.Evaluate(e);
				return obj != null && !(bool)obj;
			});
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0003ED54 File Offset: 0x0003CF54
		public override IInputTransformer GetInputTransformer(ProgramNode p, int childIndex)
		{
			if (base.Body[this.DslBodyMapping[childIndex].ConceptIndex].LambdaRule == null)
			{
				return null;
			}
			return new SequenceTransformConceptLambdaInputTransformer(p.Children[1], this.Predicate.LambdaRule.Variable);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0003EDA6 File Offset: 0x0003CFA6
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			return ConceptTopKStreamHelper.SequenceTransformerTopKStream(programSet, feature, 1, 0, (ProgramNode generator, ProgramNode transformer) => this.BuildASTNode(transformer, generator), k, fcc, logListener);
		}
	}
}

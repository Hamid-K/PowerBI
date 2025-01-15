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
	// Token: 0x020003BE RID: 958
	[Concept("Filter")]
	[DataContract]
	public sealed class Filter : ConceptRule
	{
		// Token: 0x0600156F RID: 5487 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private Filter(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Predicate
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Set
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0003EA3C File Offset: 0x0003CC3C
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

		// Token: 0x06001573 RID: 5491 RVA: 0x0003EAD4 File Offset: 0x0003CCD4
		protected override object Evaluate(object[] args)
		{
			IFunctionalSymbol1 predicate = args[0] as IFunctionalSymbol1;
			return args[1].ToEnumerable<object>().Where(delegate(object e)
			{
				object obj = predicate.Evaluate(e);
				return obj != null && (bool)obj;
			});
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0003EB10 File Offset: 0x0003CD10
		public override IInputTransformer GetInputTransformer(ProgramNode p, int childIndex)
		{
			if (base.Body[this.DslBodyMapping[childIndex].ConceptIndex].LambdaRule == null)
			{
				return null;
			}
			return new SequenceTransformConceptLambdaInputTransformer(p.Children[1], this.Predicate.LambdaRule.Variable);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0003EB62 File Offset: 0x0003CD62
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			return ConceptTopKStreamHelper.SequenceTransformerTopKStream(programSet, feature, 1, 0, (ProgramNode generator, ProgramNode transformer) => this.BuildASTNode(transformer, generator), k, fcc, logListener);
		}
	}
}

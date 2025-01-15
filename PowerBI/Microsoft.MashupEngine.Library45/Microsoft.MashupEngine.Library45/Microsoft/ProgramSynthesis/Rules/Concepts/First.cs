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
	// Token: 0x020003C3 RID: 963
	[Concept("First")]
	[DataContract]
	public sealed class First : ConceptRule
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Predicate
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Set
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private First(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x0003EDEC File Offset: 0x0003CFEC
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
				}, ConceptRule.TP.Generic("A"));
			}
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0003EE6C File Offset: 0x0003D06C
		protected override object Evaluate(object[] args)
		{
			IFunctionalSymbol1 predicate = args[0] as IFunctionalSymbol1;
			return args[1].ToEnumerable<object>().FirstOrDefault(delegate(object e)
			{
				object obj = predicate.Evaluate(e);
				return obj != null && (bool)obj;
			});
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0003EEA8 File Offset: 0x0003D0A8
		public override IInputTransformer GetInputTransformer(ProgramNode p, int childIndex)
		{
			if (base.Body[this.DslBodyMapping[childIndex].ConceptIndex].LambdaRule == null)
			{
				return null;
			}
			return new SequenceTransformConceptLambdaInputTransformer(p.Children[1], this.Predicate.LambdaRule.Variable);
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x0003EEFA File Offset: 0x0003D0FA
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			return ConceptTopKStreamHelper.SequenceTransformerTopKStream(programSet, feature, 1, 0, (ProgramNode generator, ProgramNode transformer) => this.BuildASTNode(transformer, generator), k, fcc, logListener);
		}
	}
}

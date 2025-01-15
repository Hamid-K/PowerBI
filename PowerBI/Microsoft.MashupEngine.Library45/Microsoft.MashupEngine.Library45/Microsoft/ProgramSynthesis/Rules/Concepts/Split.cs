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
	// Token: 0x020003D0 RID: 976
	[Concept("Split")]
	[DataContract]
	internal sealed class Split : ConceptRule
	{
		// Token: 0x060015C7 RID: 5575 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private Split(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol StrSymbol
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Predicate
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0003FA28 File Offset: 0x0003DC28
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") }),
					ConceptRule.TP.Constructor(typeof(Func<, >), new ConceptRule.TypeParam[]
					{
						ConceptRule.TP.Constructor(typeof(Record<, >), new ConceptRule.TypeParam[]
						{
							ConceptRule.TP.Generic("A"),
							ConceptRule.TP.Generic("A")
						}),
						ConceptRule.TP.Primitive(typeof(bool))
					})
				}, ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") }) }));
			}
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0003FAFC File Offset: 0x0003DCFC
		protected override object Evaluate(object[] args)
		{
			List<object> list = ((args[0] is string) ? args[0].ToEnumerable<object>() : (args[0] as IEnumerable<object>)).ToList<object>();
			IFunctionalSymbol1 functionalSymbol = args[1] as IFunctionalSymbol1;
			List<object[]> list2 = new List<object[]>();
			int num = 0;
			int i;
			for (i = 1; i < list.Count; i++)
			{
				if (!(bool)functionalSymbol.Evaluate(Record.Create<object, object>(list[i - 1], list[i])))
				{
					list2.Add(list.GetRange(num, i - num).ToArray());
					num = i;
				}
			}
			list2.Add(list.GetRange(num, i - num).ToArray());
			if (!(args[0] is string))
			{
				return list2.ToArray();
			}
			return list2.Select((object[] arr) => new string(arr.Cast<char>().ToArray<char>())).ToArray<object>();
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0003FBE8 File Offset: 0x0003DDE8
		public override IInputTransformer GetInputTransformer(ProgramNode p, int childIndex)
		{
			if (base.Body[this.DslBodyMapping[childIndex].ConceptIndex].LambdaRule == null)
			{
				return null;
			}
			return new SequenceTransformConceptLambdaInputTransformer(p.Children[0], this.Predicate.LambdaRule.Variable);
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0003FC3A File Offset: 0x0003DE3A
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			return ConceptTopKStreamHelper.SequenceTransformerTopKStream(programSet, feature, 0, 1, new Func<ProgramNode, ProgramNode, ProgramNode>(this.BuildASTNode), k, fcc, logListener);
		}
	}
}

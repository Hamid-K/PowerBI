using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Features.InputTransformation;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003C8 RID: 968
	[Concept("Map")]
	[DataContract]
	public sealed class Map : ConceptRule
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private Map(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Function
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Set
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0003F050 File Offset: 0x0003D250
		protected override object Evaluate(object[] args)
		{
			IFunctionalSymbol1 functionalSymbol = args[0] as IFunctionalSymbol1;
			return args[1].ToEnumerable<object>().Select(new Func<object, object>(functionalSymbol.Evaluate)).ToEnumerable<IEnumerable<object>>();
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0003F088 File Offset: 0x0003D288
		public override IInputTransformer GetInputTransformer(ProgramNode p, int childIndex)
		{
			if (base.Body[this.DslBodyMapping[childIndex].ConceptIndex].LambdaRule == null)
			{
				return null;
			}
			return new SequenceTransformConceptLambdaInputTransformer(p.Children[1], this.Function.LambdaRule.Variable);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0003F0DA File Offset: 0x0003D2DA
		internal override IEnumerable<ProgramNode> GetTopKStream(JoinProgramSet programSet, IFeature feature, int k = 1, FeatureCalculationContext fcc = null, LogListener logListener = null)
		{
			return ConceptTopKStreamHelper.SequenceTransformerTopKStream(programSet, feature, 1, 0, (ProgramNode generator, ProgramNode transformer) => this.BuildASTNode(transformer, generator), k, fcc, logListener);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0003F0F8 File Offset: 0x0003D2F8
		protected override void InitializeStandardWitnessFunctions(int parameter)
		{
			base.InitializeStandardWitnessFunctions(parameter);
			if (parameter == 0)
			{
				base.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(Map.WitnessTransformerPrefix(Map, SubsequenceSpec, SubsequenceSpec)), new Expression[]
				{
					Expression.Constant(this, typeof(Map)),
					Expression.Constant(null, typeof(SubsequenceSpec)),
					Expression.Constant(null, typeof(SubsequenceSpec))
				}), Array.Empty<ParameterExpression>()), parameter, null);
				base.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(Map.WitnessTransformerExamples(Map, ExampleSpec, ExampleSpec)), new Expression[]
				{
					Expression.Constant(this, typeof(Map)),
					Expression.Constant(null, typeof(ExampleSpec)),
					Expression.Constant(null, typeof(ExampleSpec))
				}), Array.Empty<ParameterExpression>()), parameter, null);
				base.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(Map.WitnessTransformerNonEmpty(Map, OutputNonEmptySpec, ExampleSpec)), new Expression[]
				{
					Expression.Constant(this, typeof(Map)),
					Expression.Constant(null, typeof(OutputNonEmptySpec)),
					Expression.Constant(null, typeof(ExampleSpec))
				}), Array.Empty<ParameterExpression>()), parameter, null);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0003F244 File Offset: 0x0003D444
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Constructor(typeof(Func<, >), new ConceptRule.TypeParam[]
					{
						ConceptRule.TP.Generic("A"),
						ConceptRule.TP.Generic("B")
					}),
					ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") })
				}, ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("B") }));
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0003F2D8 File Offset: 0x0003D4D8
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		private static ExampleSpec WitnessTransformerPrefix(Map rule, SubsequenceSpec outerSpec, SubsequenceSpec setValues)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in setValues.PositiveExamples)
			{
				State key = keyValuePair.Key;
				foreach (Record<object, object> record in keyValuePair.Value.ZipWith(outerSpec.PositiveExamples[key]))
				{
					State state = key.WithFunctionalInput(record.Item1, false);
					object item = record.Item2;
					object obj;
					if (dictionary.TryGetValue(state, out obj) && !ValueEquality.Comparer.Equals(obj, item))
					{
						return null;
					}
					dictionary[state] = item;
				}
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0003F3CC File Offset: 0x0003D5CC
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		private static ExampleSpec WitnessTransformerExamples(Map rule, ExampleSpec outerSpec, ExampleSpec setValues)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in setValues.Examples)
			{
				State key = keyValuePair.Key;
				foreach (Record<object, object> record in keyValuePair.Value.ToEnumerable<object>().ZipWith(outerSpec.Examples[key].ToEnumerable<object>()))
				{
					object item = record.Item1;
					State state = key.WithFunctionalInput(item, false);
					object item2 = record.Item2;
					object obj;
					if (dictionary.TryGetValue(key, out obj) && !ValueEquality.Comparer.Equals(obj, item2))
					{
						return null;
					}
					dictionary[state] = item2;
				}
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0003F4C8 File Offset: 0x0003D6C8
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		private static OutputNonEmptySpec WitnessTransformerNonEmpty(Map rule, OutputNonEmptySpec spec, ExampleSpec setValues)
		{
			List<State> list = new List<State>();
			foreach (KeyValuePair<State, object> keyValuePair in setValues.Examples)
			{
				State key = keyValuePair.Key;
				foreach (object obj in keyValuePair.Value.ToEnumerable<object>())
				{
					list.Add(key.WithFunctionalInput(obj, false));
				}
			}
			return new OutputNonEmptySpec(list);
		}
	}
}

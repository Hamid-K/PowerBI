using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003D6 RID: 982
	[Concept("Windowed")]
	[DataContract]
	internal sealed class Windowed : ConceptRule
	{
		// Token: 0x060015E5 RID: 5605 RVA: 0x0003EA31 File Offset: 0x0003CC31
		public Windowed(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0003FEA0 File Offset: 0x0003E0A0
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[] { ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") }) }, ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Constructor(typeof(Record<, >), new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Generic("A"),
					ConceptRule.TP.Generic("A")
				}) }));
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x0003FF24 File Offset: 0x0003E124
		private Type ElementType
		{
			get
			{
				Type type;
				if ((type = this._elementType) == null)
				{
					type = (this._elementType = base.Body[0].ResolvedType.GetGenericArguments()[0]);
				}
				return type;
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0003FF5C File Offset: 0x0003E15C
		protected override object Evaluate(object[] args)
		{
			return this.Evaluate(args[0].ToEnumerable<object>());
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0003FF6C File Offset: 0x0003E16C
		protected override void InitializeStandardWitnessFunctions(int parameter)
		{
			base.InitializeStandardWitnessFunctions(parameter);
			if (parameter == 0)
			{
				base.AddStandardWitnessFunction(Expression.Lambda<Action>(Expression.Call(null, methodof(Windowed.WitnessList(Windowed, PrefixSpec)), new Expression[]
				{
					Expression.Constant(this, typeof(Windowed)),
					Expression.Constant(null, typeof(PrefixSpec))
				}), Array.Empty<ParameterExpression>()), parameter, null);
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0003FFD6 File Offset: 0x0003E1D6
		private IEnumerable<object> Evaluate(IEnumerable<object> seq)
		{
			if (seq == null)
			{
				yield break;
			}
			Optional<object> optional = seq.MaybeFirst<object>();
			if (!optional.HasValue)
			{
				yield break;
			}
			object obj = optional.Value;
			foreach (object elem in seq.Skip(1))
			{
				yield return RecordUtils.GetRecordCreator(new Type[] { this.ElementType, this.ElementType }).Invoke(null, new object[] { obj, elem });
				obj = elem;
				elem = null;
			}
			IEnumerator<object> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0003FFF0 File Offset: 0x0003E1F0
		[WitnessFunction(0)]
		private static PrefixSpec WitnessList(Windowed rule, PrefixSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.PositiveExamples)
			{
				State key = keyValuePair.Key;
				Record<object, object>[] array = keyValuePair.Value.ToEnumerable<IEnumerable<object>>().Cast<Record<object, object>>().ToArray<Record<object, object>>();
				object[] array2 = new object[array.Length + 1];
				array2[0] = array[0].Item1;
				for (int i = 1; i < array.Length; i++)
				{
					if (!ValueEquality.Comparer.Equals(array[i - 1].Item2, array[i].Item1))
					{
						return null;
					}
					array2[i] = array[i].Item1;
				}
				array2[array.Length] = array[array.Length - 1].Item2;
				dictionary[key] = array2;
			}
			return new PrefixSpec(dictionary);
		}

		// Token: 0x04000A9C RID: 2716
		private Type _elementType;
	}
}

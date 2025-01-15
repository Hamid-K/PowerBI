using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003CA RID: 970
	[Concept("Pair")]
	[DataContract]
	public class Pair : ConceptRule
	{
		// Token: 0x060015AB RID: 5547 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private Pair(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Left
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Right
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0003F574 File Offset: 0x0003D774
		private Pair.RecordCreatorDelegate RecordCreator
		{
			get
			{
				if (this._recordCreator != null)
				{
					return this._recordCreator;
				}
				return this._recordCreator = RecordUtils.GetRecordCreator(new Type[]
				{
					this.Left.ResolvedType,
					this.Right.ResolvedType
				}).ToDelegateWithParams(false);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x0003F5C8 File Offset: 0x0003D7C8
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Generic("A"),
					ConceptRule.TP.Generic("B")
				}, ConceptRule.TP.Constructor(typeof(Nullable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Constructor(typeof(Record<, >), new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Generic("A"),
					ConceptRule.TP.Generic("B")
				}) }));
			}
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0003F641 File Offset: 0x0003D841
		protected override object Evaluate(object[] args)
		{
			return this.RecordCreator(new object[]
			{
				args[0],
				args[1]
			});
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0003F660 File Offset: 0x0003D860
		internal override Dictionary<object, ProgramSet> Cluster(JoinProgramSet space, State inputState)
		{
			Dictionary<object, ProgramSet> dictionary = space.ParameterSpaces[0].ClusterOnInput(inputState);
			Dictionary<object, ProgramSet> dictionary2 = space.ParameterSpaces[1].ClusterOnInput(inputState);
			int num = dictionary.Count * dictionary2.Count;
			Dictionary<object, ProgramSet> dictionary3 = new Dictionary<object, ProgramSet>(num, ValueEquality.Comparer);
			List<ProgramSet> list = new List<ProgramSet>(num);
			foreach (KeyValuePair<object, ProgramSet> keyValuePair in dictionary)
			{
				foreach (KeyValuePair<object, ProgramSet> keyValuePair2 in dictionary2)
				{
					if (keyValuePair.Key == null || keyValuePair2.Key == null || keyValuePair.Key is Bottom || keyValuePair2.Key is Bottom)
					{
						list.Add(ProgramSet.Join(this, new ProgramSet[] { keyValuePair.Value, keyValuePair2.Value }));
					}
					else
					{
						object obj = this.RecordCreator(new object[] { keyValuePair.Key, keyValuePair2.Key });
						dictionary3[obj] = ProgramSet.Join(this, new ProgramSet[] { keyValuePair.Value, keyValuePair2.Value });
					}
				}
			}
			if (list.Count > 0)
			{
				dictionary3[Bottom.Value] = list.NormalizedUnion();
			}
			return dictionary3;
		}

		// Token: 0x04000A95 RID: 2709
		private Pair.RecordCreatorDelegate _recordCreator;

		// Token: 0x020003CB RID: 971
		// (Invoke) Token: 0x060015B3 RID: 5555
		private delegate object RecordCreatorDelegate(params object[] elements);
	}
}

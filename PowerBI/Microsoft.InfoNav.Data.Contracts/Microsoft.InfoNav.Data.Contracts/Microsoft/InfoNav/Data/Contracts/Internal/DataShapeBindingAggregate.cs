using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000195 RID: 405
	[DataContract(Name = "Aggregates", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingAggregate : IEquatable<DataShapeBindingAggregate>
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00015ACE File Offset: 0x00013CCE
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x00015AD6 File Offset: 0x00013CD6
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public int Select { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00015ADF File Offset: 0x00013CDF
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x00015AE7 File Offset: 0x00013CE7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataShapeBindingAggregateKind Kind { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00015AF0 File Offset: 0x00013CF0
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x00015AF8 File Offset: 0x00013CF8
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<DataShapeBindingAggregateContainer> Aggregations { get; set; }

		// Token: 0x06000AFD RID: 2813 RVA: 0x00015B04 File Offset: 0x00013D04
		public bool Equals(DataShapeBindingAggregate other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAggregate>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Kind == other.Kind && this.Select == other.Select && this.Aggregations.SequenceEqual(other.Aggregations);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00015B5C File Offset: 0x00013D5C
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<DataShapeBindingAggregateContainer>(this.Aggregations, null), this.Kind.GetHashCode(), this.Select.GetHashCode());
		}
	}
}

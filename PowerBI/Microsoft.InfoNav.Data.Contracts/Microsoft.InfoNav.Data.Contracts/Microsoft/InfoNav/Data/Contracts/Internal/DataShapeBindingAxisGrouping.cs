using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200019A RID: 410
	[DataContract(Name = "Grouping", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingAxisGrouping : IEquatable<DataShapeBindingAxisGrouping>
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00015E01 File Offset: 0x00014001
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x00015E09 File Offset: 0x00014009
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public IList<int> Projections { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00015E12 File Offset: 0x00014012
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x00015E1A File Offset: 0x0001401A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 15)]
		public IList<int> GroupBy { get; set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00015E23 File Offset: 0x00014023
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x00015E2B File Offset: 0x0001402B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public List<int> SuppressedProjections { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x00015E34 File Offset: 0x00014034
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x00015E3C File Offset: 0x0001403C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<int> ShowItemsWithNoData { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00015E45 File Offset: 0x00014045
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x00015E4D File Offset: 0x0001404D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public SubtotalType? Subtotal { get; set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00015E56 File Offset: 0x00014056
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x00015E5E File Offset: 0x0001405E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public IList<FilterDefinition> InstanceFilters { get; set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00015E67 File Offset: 0x00014067
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x00015E6F File Offset: 0x0001406F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public IList<DataShapeBindingAggregate> Aggregates { get; set; }

		// Token: 0x06000B24 RID: 2852 RVA: 0x00015E78 File Offset: 0x00014078
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<int>(this.Projections, null), Hashing.CombineHash<int>(this.GroupBy, null), Hashing.CombineHash<int>(this.SuppressedProjections, null), Hashing.CombineHash<int>(this.ShowItemsWithNoData, null), this.Subtotal.GetHashCode(), Hashing.CombineHashUnordered<FilterDefinition>(this.InstanceFilters, null), Hashing.CombineHash<DataShapeBindingAggregate>(this.Aggregates, null));
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00015EE6 File Offset: 0x000140E6
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingAxisGrouping);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00015EF4 File Offset: 0x000140F4
		public bool Equals(DataShapeBindingAxisGrouping other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxisGrouping>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Subtotal.Equals(other.Subtotal) && this.Projections.BagEquals(other.Projections, EqualityComparer<int>.Default) && this.SuppressedProjections.BagEquals(other.SuppressedProjections, EqualityComparer<int>.Default) && this.GroupBy.BagEquals(other.GroupBy, EqualityComparer<int>.Default) && this.ShowItemsWithNoData.BagEquals(other.ShowItemsWithNoData, EqualityComparer<int>.Default) && this.InstanceFilters.BagEquals(other.InstanceFilters, EqualityComparer<FilterDefinition>.Default) && this.Aggregates.BagEquals(other.Aggregates, EqualityComparer<DataShapeBindingAggregate>.Default);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00015FDC File Offset: 0x000141DC
		public static bool operator ==(DataShapeBindingAxisGrouping left, DataShapeBindingAxisGrouping right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAxisGrouping>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00016009 File Offset: 0x00014209
		public static bool operator !=(DataShapeBindingAxisGrouping left, DataShapeBindingAxisGrouping right)
		{
			return !(left == right);
		}
	}
}

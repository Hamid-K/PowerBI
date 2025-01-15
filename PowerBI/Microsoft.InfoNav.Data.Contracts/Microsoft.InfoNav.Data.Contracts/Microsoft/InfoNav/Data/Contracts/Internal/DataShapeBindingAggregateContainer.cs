using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000178 RID: 376
	[DataContract(Name = "SelectAggregate", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBindingAggregateContainer : IEquatable<DataShapeBindingAggregateContainer>
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00013DF1 File Offset: 0x00011FF1
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x00013DF9 File Offset: 0x00011FF9
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public DataShapeBindingPercentileAggregate Percentile { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00013E02 File Offset: 0x00012002
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00013E0A File Offset: 0x0001200A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataShapeBindingMedianAggregate Median { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00013E13 File Offset: 0x00012013
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x00013E1B File Offset: 0x0001201B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public DataShapeBindingMinAggregate Min { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00013E24 File Offset: 0x00012024
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x00013E2C File Offset: 0x0001202C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public DataShapeBindingMaxAggregate Max { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00013E35 File Offset: 0x00012035
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00013E3D File Offset: 0x0001203D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public DataShapeBindingAverageAggregate Average { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00013E46 File Offset: 0x00012046
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00013E4E File Offset: 0x0001204E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public AggregateScope Scope { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00013E57 File Offset: 0x00012057
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00013E5F File Offset: 0x0001205F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public bool RespectInstanceFilters { get; set; }

		// Token: 0x060009E2 RID: 2530 RVA: 0x00013E68 File Offset: 0x00012068
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBindingAggregateContainer);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00013E78 File Offset: 0x00012078
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<DataShapeBindingPercentileAggregate>(this.Percentile, null), Hashing.GetHashCode<DataShapeBindingMedianAggregate>(this.Median, null), Hashing.GetHashCode<DataShapeBindingMinAggregate>(this.Min, null), Hashing.GetHashCode<DataShapeBindingMaxAggregate>(this.Max, null), Hashing.GetHashCode<DataShapeBindingAverageAggregate>(this.Average, null), Hashing.GetHashCode<AggregateScope>(this.Scope, null), Hashing.GetHashCode<bool>(this.RespectInstanceFilters, null));
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00013EE0 File Offset: 0x000120E0
		public bool Equals(DataShapeBindingAggregateContainer other)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAggregateContainer>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return (!(this.Percentile != null) || this.Percentile.Equals(other.Percentile)) && (!(this.Median != null) || this.Median.Equals(other.Median)) && (!(this.Min != null) || this.Min.Equals(other.Min)) && (!(this.Max != null) || this.Max.Equals(other.Max)) && (!(this.Average != null) || this.Average.Equals(other.Average)) && !(this.Scope != other.Scope) && this.RespectInstanceFilters == other.RespectInstanceFilters;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00013FDC File Offset: 0x000121DC
		public static bool operator ==(DataShapeBindingAggregateContainer left, DataShapeBindingAggregateContainer right)
		{
			bool? flag = Util.AreEqual<DataShapeBindingAggregateContainer>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00014009 File Offset: 0x00012209
		public static bool operator !=(DataShapeBindingAggregateContainer left, DataShapeBindingAggregateContainer right)
		{
			return !(left == right);
		}
	}
}

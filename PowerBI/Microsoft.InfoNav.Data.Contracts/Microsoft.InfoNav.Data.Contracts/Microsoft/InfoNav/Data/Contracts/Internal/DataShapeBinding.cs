using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000194 RID: 404
	[DataContract(Name = "Binding", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataShapeBinding : IEquatable<DataShapeBinding>
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x000157A1 File Offset: 0x000139A1
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x000157A9 File Offset: 0x000139A9
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public int? Version { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x000157B2 File Offset: 0x000139B2
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x000157BA File Offset: 0x000139BA
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public DataShapeBindingAxis Primary { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x000157C3 File Offset: 0x000139C3
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x000157CB File Offset: 0x000139CB
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataShapeBindingAxis Secondary { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000157D4 File Offset: 0x000139D4
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x000157DC File Offset: 0x000139DC
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 25)]
		public IList<DataShapeBindingAggregate> Aggregates { get; set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x000157E5 File Offset: 0x000139E5
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x000157ED File Offset: 0x000139ED
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 27)]
		public IList<int> Projections { get; set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x000157F6 File Offset: 0x000139F6
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x000157FE File Offset: 0x000139FE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public DataShapeBindingOrderBy OrderBy { get; set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00015807 File Offset: 0x00013A07
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0001580F File Offset: 0x00013A0F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public IList<DataShapeBindingLimit> Limits { get; set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00015818 File Offset: 0x00013A18
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00015820 File Offset: 0x00013A20
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public IList<FilterDefinition> Highlights { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00015829 File Offset: 0x00013A29
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x00015831 File Offset: 0x00013A31
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public DataReduction DataReduction { get; set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0001583A File Offset: 0x00013A3A
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00015842 File Offset: 0x00013A42
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public bool IncludeEmptyGroups { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0001584B File Offset: 0x00013A4B
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00015853 File Offset: 0x00013A53
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public IList<int> SuppressedJoinPredicates { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0001585C File Offset: 0x00013A5C
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x00015864 File Offset: 0x00013A64
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 100)]
		public IList<DataShapeBindingSuppressedJoinPredicate> SuppressedJoinPredicatesByName { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0001586D File Offset: 0x00013A6D
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x00015875 File Offset: 0x00013A75
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 110)]
		public IList<DataShapeBindingHiddenProjections> HiddenProjections { get; set; }

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001587E File Offset: 0x00013A7E
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataShapeBinding);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001588C File Offset: 0x00013A8C
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<DataShapeBindingAxis>(this.Primary, null), Hashing.GetHashCode<DataShapeBindingAxis>(this.Secondary, null), Hashing.CombineHash<DataShapeBindingAggregate>(this.Aggregates, null), Hashing.CombineHash<int>(this.Projections, null), Hashing.GetHashCode<DataShapeBindingOrderBy>(this.OrderBy, null), Hashing.CombineHash<DataShapeBindingLimit>(this.Limits, null), Hashing.CombineHash<FilterDefinition>(this.Highlights, null), Hashing.GetHashCode<DataReduction>(this.DataReduction, null), Hashing.CombineHash<int>(this.SuppressedJoinPredicates, null), Hashing.CombineHash<DataShapeBindingSuppressedJoinPredicate>(this.SuppressedJoinPredicatesByName, null), Hashing.CombineHash<DataShapeBindingHiddenProjections>(this.HiddenProjections, null));
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00015924 File Offset: 0x00013B24
		public bool Equals(DataShapeBinding other)
		{
			bool? flag = Util.AreEqual<DataShapeBinding>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return !(this.Primary != other.Primary) && !(this.Secondary != other.Secondary) && !(this.OrderBy != other.OrderBy) && this.AreSequenceEqual<int>(this.Projections, other.Projections) && this.AreSequenceEqual<DataShapeBindingAggregate>(this.Aggregates, other.Aggregates) && this.AreSequenceEqual<DataShapeBindingLimit>(this.Limits, other.Limits) && this.AreSequenceEqual<FilterDefinition>(this.Highlights, other.Highlights) && !(this.DataReduction != other.DataReduction) && this.IncludeEmptyGroups == other.IncludeEmptyGroups && this.AreSequenceEqual<int>(this.SuppressedJoinPredicates, other.SuppressedJoinPredicates) && this.AreSequenceEqual<DataShapeBindingSuppressedJoinPredicate>(this.SuppressedJoinPredicatesByName, other.SuppressedJoinPredicatesByName) && this.AreSequenceEqual<DataShapeBindingHiddenProjections>(this.HiddenProjections, other.HiddenProjections);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00015A4C File Offset: 0x00013C4C
		public static bool operator ==(DataShapeBinding left, DataShapeBinding right)
		{
			bool? flag = Util.AreEqual<DataShapeBinding>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00015A79 File Offset: 0x00013C79
		public static bool operator !=(DataShapeBinding left, DataShapeBinding right)
		{
			return !(left == right);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00015A88 File Offset: 0x00013C88
		private bool AreSequenceEqual<T>(IList<T> left, IList<T> right)
		{
			bool? flag = Util.AreEqual<IList<T>>(left, right);
			return (flag == null || flag.Value) && (flag != null || left.SequenceEqual(right));
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000188 RID: 392
	[DataContract(Name = "BinnedLineSample", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionBinnedLineSampleLimit : IEquatable<DataReductionBinnedLineSampleLimit>
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00014D59 File Offset: 0x00012F59
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x00014D61 File Offset: 0x00012F61
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public int? Count { get; set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00014D6A File Offset: 0x00012F6A
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x00014D72 File Offset: 0x00012F72
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public int? MinPointsPerSeries { get; set; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00014D7B File Offset: 0x00012F7B
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x00014D83 File Offset: 0x00012F83
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public int? MaxDynamicSeriesCount { get; set; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00014D8C File Offset: 0x00012F8C
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00014D94 File Offset: 0x00012F94
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public int? PrimaryScalarKey { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00014D9D File Offset: 0x00012F9D
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x00014DA5 File Offset: 0x00012FA5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public int? WarningCount { get; set; }

		// Token: 0x06000A80 RID: 2688 RVA: 0x00014DAE File Offset: 0x00012FAE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionBinnedLineSampleLimit);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00014DBC File Offset: 0x00012FBC
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<int?>(this.Count, null), Hashing.GetHashCode<int?>(this.MinPointsPerSeries, null), Hashing.GetHashCode<int?>(this.MaxDynamicSeriesCount, null), Hashing.GetHashCode<int?>(this.PrimaryScalarKey, null), Hashing.GetHashCode<int?>(this.WarningCount, null));
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00014E0C File Offset: 0x0001300C
		public bool Equals(DataReductionBinnedLineSampleLimit other)
		{
			bool? flag = Util.AreEqual<DataReductionBinnedLineSampleLimit>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			int? num = this.Count;
			int? num2 = other.Count;
			if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
			{
				num2 = this.MinPointsPerSeries;
				num = other.MinPointsPerSeries;
				if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
				{
					num = this.MaxDynamicSeriesCount;
					num2 = other.MaxDynamicSeriesCount;
					if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
					{
						num2 = this.PrimaryScalarKey;
						num = other.PrimaryScalarKey;
						if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
						{
							num = this.WarningCount;
							num2 = other.WarningCount;
							return (num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null));
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00014F30 File Offset: 0x00013130
		public static bool operator ==(DataReductionBinnedLineSampleLimit left, DataReductionBinnedLineSampleLimit right)
		{
			bool? flag = Util.AreEqual<DataReductionBinnedLineSampleLimit>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00014F5D File Offset: 0x0001315D
		public static bool operator !=(DataReductionBinnedLineSampleLimit left, DataReductionBinnedLineSampleLimit right)
		{
			return !(left == right);
		}
	}
}

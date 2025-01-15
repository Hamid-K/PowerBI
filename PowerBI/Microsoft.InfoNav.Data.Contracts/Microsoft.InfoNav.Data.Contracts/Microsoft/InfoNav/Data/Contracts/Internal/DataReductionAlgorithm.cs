using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000187 RID: 391
	[DataContract(Name = "DataReductionAlgorithm", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionAlgorithm : IEquatable<DataReductionAlgorithm>
	{
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00014B7D File Offset: 0x00012D7D
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00014B85 File Offset: 0x00012D85
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public DataReductionTopLimit Top { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00014B8E File Offset: 0x00012D8E
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x00014B96 File Offset: 0x00012D96
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataReductionSampleLimit Sample { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00014B9F File Offset: 0x00012D9F
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x00014BA7 File Offset: 0x00012DA7
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public DataReductionBottomLimit Bottom { get; set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00014BB0 File Offset: 0x00012DB0
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x00014BB8 File Offset: 0x00012DB8
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public DataReductionDataWindow Window { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00014BC1 File Offset: 0x00012DC1
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00014BC9 File Offset: 0x00012DC9
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public DataReductionBinnedLineSampleLimit BinnedLineSample { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00014BD2 File Offset: 0x00012DD2
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00014BDA File Offset: 0x00012DDA
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public DataReductionOverlappingPointsSampleLimit OverlappingPointsSample { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00014BE3 File Offset: 0x00012DE3
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00014BEB File Offset: 0x00012DEB
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public DataReductionTopNPerLevelSampleLimit TopNPerLevel { get; set; }

		// Token: 0x06000A70 RID: 2672 RVA: 0x00014BF4 File Offset: 0x00012DF4
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionAlgorithm);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00014C04 File Offset: 0x00012E04
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<DataReductionTopLimit>(this.Top, null), Hashing.GetHashCode<DataReductionSampleLimit>(this.Sample, null), Hashing.GetHashCode<DataReductionBottomLimit>(this.Bottom, null), Hashing.GetHashCode<DataReductionDataWindow>(this.Window, null), Hashing.GetHashCode<DataReductionBinnedLineSampleLimit>(this.BinnedLineSample, null), Hashing.GetHashCode<DataReductionOverlappingPointsSampleLimit>(this.OverlappingPointsSample, null), Hashing.GetHashCode<DataReductionTopNPerLevelSampleLimit>(this.TopNPerLevel, null));
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00014C6C File Offset: 0x00012E6C
		public bool Equals(DataReductionAlgorithm other)
		{
			bool? flag = Util.AreEqual<DataReductionAlgorithm>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Top == other.Top && this.Sample == other.Sample && this.Bottom == other.Bottom && this.Window == other.Window && this.BinnedLineSample == other.BinnedLineSample && this.OverlappingPointsSample == other.OverlappingPointsSample && this.TopNPerLevel == other.TopNPerLevel;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00014D18 File Offset: 0x00012F18
		public static bool operator ==(DataReductionAlgorithm left, DataReductionAlgorithm right)
		{
			bool? flag = Util.AreEqual<DataReductionAlgorithm>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00014D45 File Offset: 0x00012F45
		public static bool operator !=(DataReductionAlgorithm left, DataReductionAlgorithm right)
		{
			return !(left == right);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000186 RID: 390
	[DataContract(Name = "DataReduction", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReduction : IEquatable<DataReduction>
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x000149E3 File Offset: 0x00012BE3
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x000149EB File Offset: 0x00012BEB
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public DataReductionAlgorithm Primary { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000149F4 File Offset: 0x00012BF4
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x000149FC File Offset: 0x00012BFC
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataReductionAlgorithm Secondary { get; set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00014A05 File Offset: 0x00012C05
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00014A0D File Offset: 0x00012C0D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 25)]
		public DataReductionAlgorithm Intersection { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00014A16 File Offset: 0x00012C16
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x00014A1E File Offset: 0x00012C1E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public int? DataVolume { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00014A27 File Offset: 0x00012C27
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x00014A2F File Offset: 0x00012C2F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public IList<ScopedDataReduction> Scoped { get; set; }

		// Token: 0x06000A5C RID: 2652 RVA: 0x00014A38 File Offset: 0x00012C38
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReduction);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00014A48 File Offset: 0x00012C48
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<DataReductionAlgorithm>(this.Primary, null), Hashing.GetHashCode<DataReductionAlgorithm>(this.Secondary, null), Hashing.GetHashCode<DataReductionAlgorithm>(this.Intersection, null), Hashing.GetHashCode<int?>(this.DataVolume, null), Hashing.CombineHash<ScopedDataReduction>(this.Scoped, null));
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00014A98 File Offset: 0x00012C98
		public bool Equals(DataReduction other)
		{
			bool? flag = Util.AreEqual<DataReduction>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			if (this.Primary == other.Primary && this.Secondary == other.Secondary && this.Intersection == other.Intersection)
			{
				int? dataVolume = this.DataVolume;
				int? dataVolume2 = other.DataVolume;
				if ((dataVolume.GetValueOrDefault() == dataVolume2.GetValueOrDefault()) & (dataVolume != null == (dataVolume2 != null)))
				{
					return this.Scoped.SequenceEqual(other.Scoped);
				}
			}
			return false;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00014B3C File Offset: 0x00012D3C
		public static bool operator ==(DataReduction left, DataReduction right)
		{
			bool? flag = Util.AreEqual<DataReduction>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00014B69 File Offset: 0x00012D69
		public static bool operator !=(DataReduction left, DataReduction right)
		{
			return !(left == right);
		}
	}
}

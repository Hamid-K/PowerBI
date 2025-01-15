using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000193 RID: 403
	[DataContract(Name = "PerLevelSampleLimit", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionTopNPerLevelSampleLimit : IEquatable<DataReductionTopNPerLevelSampleLimit>
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000156A5 File Offset: 0x000138A5
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x000156AD File Offset: 0x000138AD
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public int? Count { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x000156B6 File Offset: 0x000138B6
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x000156BE File Offset: 0x000138BE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataReductionWindowExpansionState WindowExpansion { get; set; }

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000156C7 File Offset: 0x000138C7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionTopNPerLevelSampleLimit);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000156D5 File Offset: 0x000138D5
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<int?>(this.Count, null), Hashing.GetHashCode<DataReductionWindowExpansionState>(this.WindowExpansion, null));
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000156F4 File Offset: 0x000138F4
		public bool Equals(DataReductionTopNPerLevelSampleLimit other)
		{
			bool? flag = Util.AreEqual<DataReductionTopNPerLevelSampleLimit>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			int? count = this.Count;
			int? count2 = other.Count;
			return ((count.GetValueOrDefault() == count2.GetValueOrDefault()) & (count != null == (count2 != null))) && this.WindowExpansion == other.WindowExpansion;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00015760 File Offset: 0x00013960
		public static bool operator ==(DataReductionTopNPerLevelSampleLimit left, DataReductionTopNPerLevelSampleLimit right)
		{
			bool? flag = Util.AreEqual<DataReductionTopNPerLevelSampleLimit>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001578D File Offset: 0x0001398D
		public static bool operator !=(DataReductionTopNPerLevelSampleLimit left, DataReductionTopNPerLevelSampleLimit right)
		{
			return !(left == right);
		}
	}
}

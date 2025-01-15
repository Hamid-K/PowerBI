using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000192 RID: 402
	[DataContract(Name = "TopLimit", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionTopLimit : IEquatable<DataReductionTopLimit>
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x000155B9 File Offset: 0x000137B9
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x000155C1 File Offset: 0x000137C1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public int? Count { get; set; }

		// Token: 0x06000AC6 RID: 2758 RVA: 0x000155CA File Offset: 0x000137CA
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionTopLimit);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000155D8 File Offset: 0x000137D8
		public override int GetHashCode()
		{
			if (this.Count == null)
			{
				return 0;
			}
			return this.Count.GetHashCode();
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001560C File Offset: 0x0001380C
		public bool Equals(DataReductionTopLimit other)
		{
			bool? flag = Util.AreEqual<DataReductionTopLimit>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			int? count = this.Count;
			int? count2 = other.Count;
			return (count.GetValueOrDefault() == count2.GetValueOrDefault()) & (count != null == (count2 != null));
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00015664 File Offset: 0x00013864
		public static bool operator ==(DataReductionTopLimit left, DataReductionTopLimit right)
		{
			bool? flag = Util.AreEqual<DataReductionTopLimit>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00015691 File Offset: 0x00013891
		public static bool operator !=(DataReductionTopLimit left, DataReductionTopLimit right)
		{
			return !(left == right);
		}
	}
}

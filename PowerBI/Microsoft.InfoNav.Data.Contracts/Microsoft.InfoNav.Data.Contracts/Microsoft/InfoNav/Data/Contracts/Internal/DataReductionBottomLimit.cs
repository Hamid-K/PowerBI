using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000189 RID: 393
	[DataContract(Name = "BottomLimit", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionBottomLimit : IEquatable<DataReductionBottomLimit>
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00014F71 File Offset: 0x00013171
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00014F79 File Offset: 0x00013179
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public int? Count { get; set; }

		// Token: 0x06000A88 RID: 2696 RVA: 0x00014F82 File Offset: 0x00013182
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionBottomLimit);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00014F90 File Offset: 0x00013190
		public override int GetHashCode()
		{
			if (this.Count == null)
			{
				return 0;
			}
			return this.Count.GetHashCode();
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00014FC4 File Offset: 0x000131C4
		public bool Equals(DataReductionBottomLimit other)
		{
			bool? flag = Util.AreEqual<DataReductionBottomLimit>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			int? count = this.Count;
			int? count2 = other.Count;
			return (count.GetValueOrDefault() == count2.GetValueOrDefault()) & (count != null == (count2 != null));
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001501C File Offset: 0x0001321C
		public static bool operator ==(DataReductionBottomLimit left, DataReductionBottomLimit right)
		{
			bool? flag = Util.AreEqual<DataReductionBottomLimit>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00015049 File Offset: 0x00013249
		public static bool operator !=(DataReductionBottomLimit left, DataReductionBottomLimit right)
		{
			return !(left == right);
		}
	}
}

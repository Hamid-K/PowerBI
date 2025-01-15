using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000190 RID: 400
	[DataContract(Name = "SampleLimit", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionSampleLimit : IEquatable<DataReductionSampleLimit>
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x000153F1 File Offset: 0x000135F1
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x000153F9 File Offset: 0x000135F9
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public int? Count { get; set; }

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00015402 File Offset: 0x00013602
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionSampleLimit);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00015410 File Offset: 0x00013610
		public override int GetHashCode()
		{
			if (this.Count == null)
			{
				return 0;
			}
			return this.Count.GetHashCode();
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00015444 File Offset: 0x00013644
		public bool Equals(DataReductionSampleLimit other)
		{
			bool? flag = Util.AreEqual<DataReductionSampleLimit>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			int? count = this.Count;
			int? count2 = other.Count;
			return (count.GetValueOrDefault() == count2.GetValueOrDefault()) & (count != null == (count2 != null));
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001549C File Offset: 0x0001369C
		public static bool operator ==(DataReductionSampleLimit left, DataReductionSampleLimit right)
		{
			bool? flag = Util.AreEqual<DataReductionSampleLimit>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000154C9 File Offset: 0x000136C9
		public static bool operator !=(DataReductionSampleLimit left, DataReductionSampleLimit right)
		{
			return !(left == right);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000191 RID: 401
	[DataContract(Name = "DataReductionScope", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataReductionScope : IEquatable<DataReductionScope>
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x000154DD File Offset: 0x000136DD
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x000154E5 File Offset: 0x000136E5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<int> Primary { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x000154EE File Offset: 0x000136EE
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x000154F6 File Offset: 0x000136F6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public IList<int> Secondary { get; set; }

		// Token: 0x06000ABE RID: 2750 RVA: 0x000154FF File Offset: 0x000136FF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DataReductionScope);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001550D File Offset: 0x0001370D
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.CombineHash<int>(this.Primary, null), Hashing.CombineHash<int>(this.Secondary, null));
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001552C File Offset: 0x0001372C
		public bool Equals(DataReductionScope other)
		{
			bool? flag = Util.AreEqual<DataReductionScope>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Primary.SequenceEqual(other.Primary) && this.Secondary.SequenceEqual(other.Secondary);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00015578 File Offset: 0x00013778
		public static bool operator ==(DataReductionScope left, DataReductionScope right)
		{
			bool? flag = Util.AreEqual<DataReductionScope>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000155A5 File Offset: 0x000137A5
		public static bool operator !=(DataReductionScope left, DataReductionScope right)
		{
			return !(left == right);
		}
	}
}

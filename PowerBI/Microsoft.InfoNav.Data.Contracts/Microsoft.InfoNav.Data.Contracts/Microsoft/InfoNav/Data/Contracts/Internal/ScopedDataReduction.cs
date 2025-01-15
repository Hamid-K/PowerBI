using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001D2 RID: 466
	[DataContract(Name = "ScopedDataReduction", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ScopedDataReduction : IEquatable<ScopedDataReduction>
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00018395 File Offset: 0x00016595
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x0001839D File Offset: 0x0001659D
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public DataReductionScope Scope { get; set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x000183A6 File Offset: 0x000165A6
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x000183AE File Offset: 0x000165AE
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 20)]
		public DataReductionAlgorithm Algorithm { get; set; }

		// Token: 0x06000C5A RID: 3162 RVA: 0x000183B7 File Offset: 0x000165B7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ScopedDataReduction);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000183C5 File Offset: 0x000165C5
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<DataReductionScope>(this.Scope, null), Hashing.GetHashCode<DataReductionAlgorithm>(this.Algorithm, null));
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000183E4 File Offset: 0x000165E4
		public bool Equals(ScopedDataReduction other)
		{
			bool? flag = Util.AreEqual<ScopedDataReduction>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Scope == other.Scope && this.Algorithm == other.Algorithm;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00018430 File Offset: 0x00016630
		public static bool operator ==(ScopedDataReduction left, ScopedDataReduction right)
		{
			bool? flag = Util.AreEqual<ScopedDataReduction>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0001845D File Offset: 0x0001665D
		public static bool operator !=(ScopedDataReduction left, ScopedDataReduction right)
		{
			return !(left == right);
		}
	}
}

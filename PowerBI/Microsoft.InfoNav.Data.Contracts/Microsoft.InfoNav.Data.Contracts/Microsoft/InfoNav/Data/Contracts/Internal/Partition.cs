using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001E2 RID: 482
	[DataContract(Name = "Partition", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class Partition : IDataContractValidatable, IEquatable<Partition>
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00019B85 File Offset: 0x00017D85
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00019B8D File Offset: 0x00017D8D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string DisplayName { get; set; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00019B96 File Offset: 0x00017D96
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00019B9E File Offset: 0x00017D9E
		[DataMember(IsRequired = true, Order = 20)]
		public List<QueryExpressionContainer> PartitionIds { get; set; }

		// Token: 0x06000D13 RID: 3347 RVA: 0x00019BA7 File Offset: 0x00017DA7
		public bool IsValid()
		{
			return !this.PartitionIds.IsNullOrEmpty<QueryExpressionContainer>() && QueryExpressionValidator.AreValidStandalone(this.PartitionIds);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00019BC3 File Offset: 0x00017DC3
		public bool Equals(Partition other)
		{
			return other != null && (this == other || (this.DisplayName == other.DisplayName && this.PartitionIds.SequenceEqual(other.PartitionIds)));
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00019BF6 File Offset: 0x00017DF6
		public override bool Equals(object other)
		{
			return this.Equals(other as Partition);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00019C04 File Offset: 0x00017E04
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.DisplayName, null), (this.PartitionIds != null) ? Hashing.CombineHash<QueryExpressionContainer>(this.PartitionIds, null) : 0);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00019C2E File Offset: 0x00017E2E
		public static bool operator ==(Partition left, Partition right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00019C44 File Offset: 0x00017E44
		public static bool operator !=(Partition left, Partition right)
		{
			return !(left == right);
		}
	}
}

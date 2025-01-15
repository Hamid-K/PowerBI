using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001DE RID: 478
	[DataContract(Name = "BinItem", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class BinItem : IDataContractValidatable, IEquatable<BinItem>
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x000192EB File Offset: 0x000174EB
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x000192F3 File Offset: 0x000174F3
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 1)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x06000CDD RID: 3293 RVA: 0x000192FC File Offset: 0x000174FC
		public bool IsValid()
		{
			return QueryExpressionValidator.IsValid(this.Expression);
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x00019309 File Offset: 0x00017509
		public bool Equals(BinItem other)
		{
			return other != null && (this == other || this.Expression.Equals(other.Expression));
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00019327 File Offset: 0x00017527
		public override bool Equals(object other)
		{
			return this.Equals(other as BinItem);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00019335 File Offset: 0x00017535
		public override int GetHashCode()
		{
			return this.Expression.GetHashCode();
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00019342 File Offset: 0x00017542
		public static bool operator ==(BinItem left, BinItem right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00019358 File Offset: 0x00017558
		public static bool operator !=(BinItem left, BinItem right)
		{
			return !(left == right);
		}
	}
}

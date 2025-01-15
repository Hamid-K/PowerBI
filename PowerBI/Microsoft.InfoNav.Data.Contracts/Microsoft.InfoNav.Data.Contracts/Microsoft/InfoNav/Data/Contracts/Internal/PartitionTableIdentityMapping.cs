using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001E5 RID: 485
	[DataContract(Name = "PartitionTableIdentityMapping", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class PartitionTableIdentityMapping : IDataContractValidatable, IEquatable<PartitionTableIdentityMapping>
	{
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00019F1E File Offset: 0x0001811E
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00019F26 File Offset: 0x00018126
		[DataMember(IsRequired = true, Order = 10)]
		public QueryExpressionContainer PartitionTableColumn { get; set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00019F2F File Offset: 0x0001812F
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00019F37 File Offset: 0x00018137
		[DataMember(IsRequired = true, Order = 20)]
		public QueryExpressionContainer SourceTableColumn { get; set; }

		// Token: 0x06000D3A RID: 3386 RVA: 0x00019F40 File Offset: 0x00018140
		public bool Equals(PartitionTableIdentityMapping other)
		{
			bool? flag = Util.AreEqual<PartitionTableIdentityMapping>(this, other);
			if (flag == null)
			{
				return object.Equals(this.PartitionTableColumn, other.PartitionTableColumn) && object.Equals(this.SourceTableColumn, other.SourceTableColumn);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00019F8C File Offset: 0x0001818C
		public override bool Equals(object other)
		{
			return this.Equals(other as PartitionTableIdentityMapping);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00019F9A File Offset: 0x0001819A
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<QueryExpressionContainer>(this.PartitionTableColumn, null), Hashing.GetHashCode<QueryExpressionContainer>(this.SourceTableColumn, null));
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00019FBC File Offset: 0x000181BC
		public static bool operator ==(PartitionTableIdentityMapping left, PartitionTableIdentityMapping right)
		{
			bool? flag = Util.AreEqual<PartitionTableIdentityMapping>(left, right);
			if (flag == null)
			{
				return left.Equals(right);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00019FE9 File Offset: 0x000181E9
		public static bool operator !=(PartitionTableIdentityMapping left, PartitionTableIdentityMapping right)
		{
			return !(left == right);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00019FF5 File Offset: 0x000181F5
		public bool IsValid()
		{
			return this.PartitionTableColumn != null && QueryExpressionValidator.IsValidStandalone(this.PartitionTableColumn) && this.SourceTableColumn != null && QueryExpressionValidator.IsValidStandalone(this.SourceTableColumn);
		}
	}
}

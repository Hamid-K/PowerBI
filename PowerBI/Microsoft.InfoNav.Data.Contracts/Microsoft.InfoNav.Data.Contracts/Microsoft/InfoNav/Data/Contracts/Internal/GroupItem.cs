using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Common.Internal;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001E1 RID: 481
	[DataContract(Name = "GroupItem", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class GroupItem : IDataContractValidatable, IEquatable<GroupItem>
	{
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00019A60 File Offset: 0x00017C60
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x00019A68 File Offset: 0x00017C68
		[DataMember(IsRequired = true, Order = 1)]
		public string DisplayName { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00019A71 File Offset: 0x00017C71
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x00019A79 File Offset: 0x00017C79
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public QueryExpressionContainer Expression { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00019A82 File Offset: 0x00017C82
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x00019A8A File Offset: 0x00017C8A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public bool BlankDefaultPlaceholder { get; set; }

		// Token: 0x06000D08 RID: 3336 RVA: 0x00019A93 File Offset: 0x00017C93
		public bool IsValid()
		{
			return this.DisplayName != null && (this.Expression == null || (QueryExpressionValidator.IsValid(this.Expression) && !this.BlankDefaultPlaceholder));
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00019AC8 File Offset: 0x00017CC8
		public bool Equals(GroupItem other)
		{
			return other != null && (this == other || (StringUtil.EqualsOrdinal(this.DisplayName, other.DisplayName) && this.Expression == other.Expression && this.BlankDefaultPlaceholder == other.BlankDefaultPlaceholder));
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00019B16 File Offset: 0x00017D16
		public override bool Equals(object other)
		{
			return this.Equals(other as GroupItem);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00019B24 File Offset: 0x00017D24
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.DisplayName.GetHashCode(), Hashing.GetHashCode<QueryExpressionContainer>(this.Expression, null), this.BlankDefaultPlaceholder.GetHashCode());
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00019B5B File Offset: 0x00017D5B
		public static bool operator ==(GroupItem left, GroupItem right)
		{
			return (left == null && right == null) || (left != null && left.Equals(right));
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00019B71 File Offset: 0x00017D71
		public static bool operator !=(GroupItem left, GroupItem right)
		{
			return !(left == right);
		}
	}
}

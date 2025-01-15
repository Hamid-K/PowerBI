using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200045A RID: 1114
	internal sealed class SpanIndex
	{
		// Token: 0x060036B8 RID: 14008 RVA: 0x000B0AE0 File Offset: 0x000AECE0
		internal void AddSpannedRowType(RowType spannedRowType, TypeUsage originalRowType)
		{
			if (this._rowMap == null)
			{
				this._rowMap = new Dictionary<RowType, TypeUsage>(SpanIndex.RowTypeEqualityComparer.Instance);
			}
			this._rowMap[spannedRowType] = originalRowType;
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x000B0B08 File Offset: 0x000AED08
		internal TypeUsage GetSpannedRowType(RowType spannedRowType)
		{
			TypeUsage typeUsage;
			if (this._rowMap != null && this._rowMap.TryGetValue(spannedRowType, out typeUsage))
			{
				return typeUsage;
			}
			return null;
		}

		// Token: 0x060036BA RID: 14010 RVA: 0x000B0B30 File Offset: 0x000AED30
		internal bool HasSpanMap(RowType spanRowType)
		{
			return this._spanMap != null && this._spanMap.ContainsKey(spanRowType);
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x000B0B48 File Offset: 0x000AED48
		internal void AddSpanMap(RowType rowType, Dictionary<int, AssociationEndMember> columnMap)
		{
			if (this._spanMap == null)
			{
				this._spanMap = new Dictionary<RowType, Dictionary<int, AssociationEndMember>>(SpanIndex.RowTypeEqualityComparer.Instance);
			}
			this._spanMap[rowType] = columnMap;
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000B0B70 File Offset: 0x000AED70
		internal Dictionary<int, AssociationEndMember> GetSpanMap(RowType rowType)
		{
			Dictionary<int, AssociationEndMember> dictionary = null;
			if (this._spanMap != null && this._spanMap.TryGetValue(rowType, out dictionary))
			{
				return dictionary;
			}
			return null;
		}

		// Token: 0x040011C0 RID: 4544
		private Dictionary<RowType, Dictionary<int, AssociationEndMember>> _spanMap;

		// Token: 0x040011C1 RID: 4545
		private Dictionary<RowType, TypeUsage> _rowMap;

		// Token: 0x02000A6C RID: 2668
		private sealed class RowTypeEqualityComparer : IEqualityComparer<RowType>
		{
			// Token: 0x060061CA RID: 25034 RVA: 0x0015353A File Offset: 0x0015173A
			private RowTypeEqualityComparer()
			{
			}

			// Token: 0x060061CB RID: 25035 RVA: 0x00153542 File Offset: 0x00151742
			public bool Equals(RowType x, RowType y)
			{
				return x != null && y != null && x.EdmEquals(y);
			}

			// Token: 0x060061CC RID: 25036 RVA: 0x00153553 File Offset: 0x00151753
			public int GetHashCode(RowType obj)
			{
				return obj.Identity.GetHashCode();
			}

			// Token: 0x04002B13 RID: 11027
			internal static readonly SpanIndex.RowTypeEqualityComparer Instance = new SpanIndex.RowTypeEqualityComparer();
		}
	}
}

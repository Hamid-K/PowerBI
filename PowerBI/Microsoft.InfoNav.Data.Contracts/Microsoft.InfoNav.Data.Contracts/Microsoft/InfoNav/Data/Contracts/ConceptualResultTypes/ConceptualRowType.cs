using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes
{
	// Token: 0x02000145 RID: 325
	public sealed class ConceptualRowType : ConceptualResultType
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x000114BC File Offset: 0x0000F6BC
		private ConceptualRowType()
		{
			this.Columns = new List<ConceptualTypeColumn>();
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000114CF File Offset: 0x0000F6CF
		public ConceptualRowType(IReadOnlyList<ConceptualTypeColumn> columns)
		{
			this.Columns = columns;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x000114DE File Offset: 0x0000F6DE
		public override ConceptualResultTypeKind Kind
		{
			get
			{
				return ConceptualResultTypeKind.Row;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x000114E1 File Offset: 0x0000F6E1
		public IReadOnlyList<ConceptualTypeColumn> Columns { get; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x000114E9 File Offset: 0x0000F6E9
		private HashSet<ConceptualTypeColumn> ColumnsHashSet
		{
			get
			{
				if (this._columnsHashset == null)
				{
					this._columnsHashset = new HashSet<ConceptualTypeColumn>(this.Columns);
				}
				return this._columnsHashset;
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001150C File Offset: 0x0000F70C
		public ConceptualTypeColumn GetColumn(string name)
		{
			foreach (ConceptualTypeColumn conceptualTypeColumn in this.Columns)
			{
				if (ConceptualNameComparer.Instance.Equals(conceptualTypeColumn.Name, name))
				{
					return conceptualTypeColumn;
				}
			}
			return null;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001156C File Offset: 0x0000F76C
		public ConceptualTypeColumn GetColumnByEdmName(string edmName)
		{
			foreach (ConceptualTypeColumn conceptualTypeColumn in this.Columns)
			{
				if (ConceptualNameComparer.Instance.Equals(conceptualTypeColumn.EdmName, edmName))
				{
					return conceptualTypeColumn;
				}
			}
			return null;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000115CC File Offset: 0x0000F7CC
		public bool IsSupersetOf(ConceptualRowType other)
		{
			return this.ColumnsHashSet.IsSupersetOf(other.Columns);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000115E0 File Offset: 0x0000F7E0
		public override bool Equals(ConceptualResultType other)
		{
			ConceptualRowType conceptualRowType = other as ConceptualRowType;
			return conceptualRowType != null && this.Columns.SequenceEqualReadOnly(conceptualRowType.Columns);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001160C File Offset: 0x0000F80C
		public bool EqualsOrderIndependent(ConceptualResultType other)
		{
			ConceptualRowType conceptualRowType = other as ConceptualRowType;
			return conceptualRowType != null && this.ColumnsHashSet.SetEquals(conceptualRowType.ColumnsHashSet);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00011636 File Offset: 0x0000F836
		public override int GetHashCode()
		{
			return Hashing.CombineHashReadOnly<ConceptualTypeColumn>(this.Columns, null);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00011644 File Offset: 0x0000F844
		public override string ToString()
		{
			return "Row[" + string.Join<ConceptualTypeColumn>(",", this.Columns) + "]";
		}

		// Token: 0x040003E2 RID: 994
		public static readonly ConceptualRowType Empty = new ConceptualRowType();

		// Token: 0x040003E3 RID: 995
		private HashSet<ConceptualTypeColumn> _columnsHashset;
	}
}

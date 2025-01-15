using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes
{
	// Token: 0x02000146 RID: 326
	public sealed class ConceptualTableType : ConceptualResultType
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x00011671 File Offset: 0x0000F871
		internal ConceptualTableType(ConceptualRowType row)
		{
			this.RowType = row;
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00011680 File Offset: 0x0000F880
		public override ConceptualResultTypeKind Kind
		{
			get
			{
				return ConceptualResultTypeKind.Table;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00011683 File Offset: 0x0000F883
		public ConceptualRowType RowType { get; }

		// Token: 0x0600085A RID: 2138 RVA: 0x0001168C File Offset: 0x0000F88C
		public override bool Equals(ConceptualResultType other)
		{
			ConceptualTableType conceptualTableType = other as ConceptualTableType;
			return conceptualTableType != null && this.RowType.Equals(conceptualTableType.RowType);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000116B8 File Offset: 0x0000F8B8
		public bool EqualsOrderIndependent(ConceptualResultType other)
		{
			ConceptualTableType conceptualTableType = other as ConceptualTableType;
			return conceptualTableType != null && this.RowType.EqualsOrderIndependent(conceptualTableType.RowType);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000116E2 File Offset: 0x0000F8E2
		public override int GetHashCode()
		{
			return this.RowType.GetHashCode();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000116EF File Offset: 0x0000F8EF
		public override string ToString()
		{
			return "Table[" + this.RowType.ToString() + "]";
		}

		// Token: 0x040003E5 RID: 997
		public static readonly ConceptualTableType Empty = new ConceptualTableType(ConceptualRowType.Empty);
	}
}

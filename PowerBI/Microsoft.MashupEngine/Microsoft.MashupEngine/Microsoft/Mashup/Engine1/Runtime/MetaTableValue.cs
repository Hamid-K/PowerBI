using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200164B RID: 5707
	internal class MetaTableValue : DelegatingTableValue
	{
		// Token: 0x06008FF6 RID: 36854 RVA: 0x001DE691 File Offset: 0x001DC891
		public MetaTableValue(TableValue table, RecordValue meta)
			: base(table)
		{
			this.meta = meta;
		}

		// Token: 0x06008FF7 RID: 36855 RVA: 0x001DE6A1 File Offset: 0x001DC8A1
		private TableValue New(TableValue table)
		{
			return TableValue.New(table, this.meta);
		}

		// Token: 0x170025B0 RID: 9648
		// (get) Token: 0x06008FF8 RID: 36856 RVA: 0x001DE6AF File Offset: 0x001DC8AF
		public override RecordValue MetaValue
		{
			get
			{
				return this.meta;
			}
		}

		// Token: 0x06008FF9 RID: 36857 RVA: 0x001DE6B7 File Offset: 0x001DC8B7
		public override Value NewMeta(RecordValue metaValue)
		{
			return TableValue.New(base.Table, metaValue);
		}

		// Token: 0x06008FFA RID: 36858 RVA: 0x001DE6C5 File Offset: 0x001DC8C5
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables)
		{
			return this.New(base.Table.ReplaceRelatedTables(relatedTables));
		}

		// Token: 0x06008FFB RID: 36859 RVA: 0x001DE6D9 File Offset: 0x001DC8D9
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables, ColumnIdentity[] columnIdentities, IList<Relationship> relationships)
		{
			return this.New(base.Table.ReplaceRelatedTables(relatedTables, columnIdentities, relationships));
		}

		// Token: 0x06008FFC RID: 36860 RVA: 0x001DE6EF File Offset: 0x001DC8EF
		public override TableValue ReplaceRelationshipIdentity(string identity)
		{
			return this.New(base.Table.ReplaceRelationshipIdentity(identity));
		}

		// Token: 0x06008FFD RID: 36861 RVA: 0x001DE703 File Offset: 0x001DC903
		public override TableValue ReplaceColumnIdentities(ColumnIdentity[] columnIdentities)
		{
			return this.New(base.Table.ReplaceColumnIdentities(columnIdentities));
		}

		// Token: 0x06008FFE RID: 36862 RVA: 0x001DE717 File Offset: 0x001DC917
		public override TableValue ReplaceRelationships(IList<Relationship> relationships)
		{
			return this.New(base.Table.ReplaceRelationships(relationships));
		}

		// Token: 0x04004D9B RID: 19867
		private readonly RecordValue meta;
	}
}

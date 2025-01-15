using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001DF RID: 479
	[Serializable]
	internal class NamedTableReference : TableReferenceWithAlias
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x001603CB File Offset: 0x0015E5CB
		// (set) Token: 0x0600232E RID: 9006 RVA: 0x001603D3 File Offset: 0x0015E5D3
		public SchemaObjectName SchemaObject
		{
			get
			{
				return this._schemaObject;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._schemaObject = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x001603E3 File Offset: 0x0015E5E3
		public IList<TableHint> TableHints
		{
			get
			{
				return this._tableHints;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x001603EB File Offset: 0x0015E5EB
		// (set) Token: 0x06002331 RID: 9009 RVA: 0x001603F3 File Offset: 0x0015E5F3
		public TableSampleClause TableSampleClause
		{
			get
			{
				return this._tableSampleClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._tableSampleClause = value;
			}
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00160403 File Offset: 0x0015E603
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00160410 File Offset: 0x0015E610
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SchemaObject != null)
			{
				this.SchemaObject.Accept(visitor);
			}
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.TableHints.Count;
			while (i < count)
			{
				this.TableHints[i].Accept(visitor);
				i++;
			}
			if (this.TableSampleClause != null)
			{
				this.TableSampleClause.Accept(visitor);
			}
		}

		// Token: 0x04001A5B RID: 6747
		private SchemaObjectName _schemaObject;

		// Token: 0x04001A5C RID: 6748
		private List<TableHint> _tableHints = new List<TableHint>();

		// Token: 0x04001A5D RID: 6749
		private TableSampleClause _tableSampleClause;
	}
}

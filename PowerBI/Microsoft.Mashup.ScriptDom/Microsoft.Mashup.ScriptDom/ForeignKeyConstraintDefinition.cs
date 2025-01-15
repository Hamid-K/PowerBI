using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000347 RID: 839
	[Serializable]
	internal class ForeignKeyConstraintDefinition : ConstraintDefinition
	{
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x0016952E File Offset: 0x0016772E
		public IList<Identifier> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06002BBE RID: 11198 RVA: 0x00169536 File Offset: 0x00167736
		// (set) Token: 0x06002BBF RID: 11199 RVA: 0x0016953E File Offset: 0x0016773E
		public SchemaObjectName ReferenceTableName
		{
			get
			{
				return this._referenceTableName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._referenceTableName = value;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x0016954E File Offset: 0x0016774E
		public IList<Identifier> ReferencedTableColumns
		{
			get
			{
				return this._referencedTableColumns;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x00169556 File Offset: 0x00167756
		// (set) Token: 0x06002BC2 RID: 11202 RVA: 0x0016955E File Offset: 0x0016775E
		public DeleteUpdateAction DeleteAction
		{
			get
			{
				return this._deleteAction;
			}
			set
			{
				this._deleteAction = value;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x00169567 File Offset: 0x00167767
		// (set) Token: 0x06002BC4 RID: 11204 RVA: 0x0016956F File Offset: 0x0016776F
		public DeleteUpdateAction UpdateAction
		{
			get
			{
				return this._updateAction;
			}
			set
			{
				this._updateAction = value;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x00169578 File Offset: 0x00167778
		// (set) Token: 0x06002BC6 RID: 11206 RVA: 0x00169580 File Offset: 0x00167780
		public bool NotForReplication
		{
			get
			{
				return this._notForReplication;
			}
			set
			{
				this._notForReplication = value;
			}
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x00169589 File Offset: 0x00167789
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x00169598 File Offset: 0x00167798
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			if (this.ReferenceTableName != null)
			{
				this.ReferenceTableName.Accept(visitor);
			}
			int j = 0;
			int count2 = this.ReferencedTableColumns.Count;
			while (j < count2)
			{
				this.ReferencedTableColumns[j].Accept(visitor);
				j++;
			}
		}

		// Token: 0x04001CCA RID: 7370
		private List<Identifier> _columns = new List<Identifier>();

		// Token: 0x04001CCB RID: 7371
		private SchemaObjectName _referenceTableName;

		// Token: 0x04001CCC RID: 7372
		private List<Identifier> _referencedTableColumns = new List<Identifier>();

		// Token: 0x04001CCD RID: 7373
		private DeleteUpdateAction _deleteAction;

		// Token: 0x04001CCE RID: 7374
		private DeleteUpdateAction _updateAction;

		// Token: 0x04001CCF RID: 7375
		private bool _notForReplication;
	}
}

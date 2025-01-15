using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003CE RID: 974
	[Serializable]
	internal class ChangeTableVersionTableReference : TableReferenceWithAliasAndColumns
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06002F24 RID: 12068 RVA: 0x0016D130 File Offset: 0x0016B330
		// (set) Token: 0x06002F25 RID: 12069 RVA: 0x0016D138 File Offset: 0x0016B338
		public SchemaObjectName Target
		{
			get
			{
				return this._target;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._target = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x0016D148 File Offset: 0x0016B348
		public IList<Identifier> PrimaryKeyColumns
		{
			get
			{
				return this._primaryKeyColumns;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x0016D150 File Offset: 0x0016B350
		public IList<ScalarExpression> PrimaryKeyValues
		{
			get
			{
				return this._primaryKeyValues;
			}
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x0016D158 File Offset: 0x0016B358
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x0016D164 File Offset: 0x0016B364
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Target != null)
			{
				this.Target.Accept(visitor);
			}
			int i = 0;
			int count = this.PrimaryKeyColumns.Count;
			while (i < count)
			{
				this.PrimaryKeyColumns[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.PrimaryKeyValues.Count;
			while (j < count2)
			{
				this.PrimaryKeyValues[j].Accept(visitor);
				j++;
			}
		}

		// Token: 0x04001DD5 RID: 7637
		private SchemaObjectName _target;

		// Token: 0x04001DD6 RID: 7638
		private List<Identifier> _primaryKeyColumns = new List<Identifier>();

		// Token: 0x04001DD7 RID: 7639
		private List<ScalarExpression> _primaryKeyValues = new List<ScalarExpression>();
	}
}

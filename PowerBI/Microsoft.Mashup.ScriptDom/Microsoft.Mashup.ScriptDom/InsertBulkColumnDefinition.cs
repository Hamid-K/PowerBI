using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200035D RID: 861
	[Serializable]
	internal class InsertBulkColumnDefinition : TSqlFragment
	{
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06002C4B RID: 11339 RVA: 0x0016A015 File Offset: 0x00168215
		// (set) Token: 0x06002C4C RID: 11340 RVA: 0x0016A01D File Offset: 0x0016821D
		public ColumnDefinitionBase Column
		{
			get
			{
				return this._column;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._column = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06002C4D RID: 11341 RVA: 0x0016A02D File Offset: 0x0016822D
		// (set) Token: 0x06002C4E RID: 11342 RVA: 0x0016A035 File Offset: 0x00168235
		public NullNotNull NullNotNull
		{
			get
			{
				return this._nullNotNull;
			}
			set
			{
				this._nullNotNull = value;
			}
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x0016A03E File Offset: 0x0016823E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x0016A04A File Offset: 0x0016824A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Column != null)
			{
				this.Column.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CF9 RID: 7417
		private ColumnDefinitionBase _column;

		// Token: 0x04001CFA RID: 7418
		private NullNotNull _nullNotNull;
	}
}

using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200023F RID: 575
	[Serializable]
	internal class CreateSchemaStatement : TSqlStatement, IAuthorization
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x00162FB9 File Offset: 0x001611B9
		// (set) Token: 0x06002586 RID: 9606 RVA: 0x00162FC1 File Offset: 0x001611C1
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x00162FD1 File Offset: 0x001611D1
		// (set) Token: 0x06002588 RID: 9608 RVA: 0x00162FD9 File Offset: 0x001611D9
		public StatementList StatementList
		{
			get
			{
				return this._statementList;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._statementList = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x00162FE9 File Offset: 0x001611E9
		// (set) Token: 0x0600258A RID: 9610 RVA: 0x00162FF1 File Offset: 0x001611F1
		public Identifier Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._owner = value;
			}
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00163001 File Offset: 0x00161201
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00163010 File Offset: 0x00161210
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.StatementList != null)
			{
				this.StatementList.Accept(visitor);
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B0C RID: 6924
		private Identifier _name;

		// Token: 0x04001B0D RID: 6925
		private StatementList _statementList;

		// Token: 0x04001B0E RID: 6926
		private Identifier _owner;
	}
}

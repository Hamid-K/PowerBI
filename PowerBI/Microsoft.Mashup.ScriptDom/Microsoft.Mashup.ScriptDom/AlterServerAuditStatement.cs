using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000447 RID: 1095
	[Serializable]
	internal class AlterServerAuditStatement : ServerAuditStatement
	{
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060031C9 RID: 12745 RVA: 0x0016F982 File Offset: 0x0016DB82
		// (set) Token: 0x060031CA RID: 12746 RVA: 0x0016F98A File Offset: 0x0016DB8A
		public Identifier NewName
		{
			get
			{
				return this._newName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._newName = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x0016F99A File Offset: 0x0016DB9A
		// (set) Token: 0x060031CC RID: 12748 RVA: 0x0016F9A2 File Offset: 0x0016DBA2
		public bool RemoveWhere
		{
			get
			{
				return this._removeWhere;
			}
			set
			{
				this._removeWhere = value;
			}
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x0016F9AB File Offset: 0x0016DBAB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x0016F9B7 File Offset: 0x0016DBB7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.NewName != null)
			{
				this.NewName.Accept(visitor);
			}
		}

		// Token: 0x04001E83 RID: 7811
		private Identifier _newName;

		// Token: 0x04001E84 RID: 7812
		private bool _removeWhere;
	}
}

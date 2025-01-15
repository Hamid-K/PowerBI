using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002C4 RID: 708
	[Serializable]
	internal class AlterRoleStatement : RoleStatement
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x001665DD File Offset: 0x001647DD
		// (set) Token: 0x060028AB RID: 10411 RVA: 0x001665E5 File Offset: 0x001647E5
		public AlterRoleAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._action = value;
			}
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x001665F5 File Offset: 0x001647F5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x00166601 File Offset: 0x00164801
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Action != null)
			{
				this.Action.Accept(visitor);
			}
		}

		// Token: 0x04001BF3 RID: 7155
		private AlterRoleAction _action;
	}
}

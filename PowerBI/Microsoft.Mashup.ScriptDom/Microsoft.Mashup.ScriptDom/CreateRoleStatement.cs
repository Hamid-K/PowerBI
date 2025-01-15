using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002C3 RID: 707
	[Serializable]
	internal class CreateRoleStatement : RoleStatement, IAuthorization
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x00166594 File Offset: 0x00164794
		// (set) Token: 0x060028A6 RID: 10406 RVA: 0x0016659C File Offset: 0x0016479C
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

		// Token: 0x060028A7 RID: 10407 RVA: 0x001665AC File Offset: 0x001647AC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x001665B8 File Offset: 0x001647B8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
		}

		// Token: 0x04001BF2 RID: 7154
		private Identifier _owner;
	}
}

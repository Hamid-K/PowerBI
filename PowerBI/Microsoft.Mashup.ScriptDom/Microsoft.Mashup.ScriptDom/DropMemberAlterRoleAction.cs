using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002C8 RID: 712
	[Serializable]
	internal class DropMemberAlterRoleAction : AlterRoleAction
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x001666C9 File Offset: 0x001648C9
		// (set) Token: 0x060028BC RID: 10428 RVA: 0x001666D1 File Offset: 0x001648D1
		public Identifier Member
		{
			get
			{
				return this._member;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._member = value;
			}
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x001666E1 File Offset: 0x001648E1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x001666ED File Offset: 0x001648ED
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Member != null)
			{
				this.Member.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BF6 RID: 7158
		private Identifier _member;
	}
}

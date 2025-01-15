using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002C7 RID: 711
	[Serializable]
	internal class AddMemberAlterRoleAction : AlterRoleAction
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x00166680 File Offset: 0x00164880
		// (set) Token: 0x060028B7 RID: 10423 RVA: 0x00166688 File Offset: 0x00164888
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

		// Token: 0x060028B8 RID: 10424 RVA: 0x00166698 File Offset: 0x00164898
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x001666A4 File Offset: 0x001648A4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Member != null)
			{
				this.Member.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BF5 RID: 7157
		private Identifier _member;
	}
}

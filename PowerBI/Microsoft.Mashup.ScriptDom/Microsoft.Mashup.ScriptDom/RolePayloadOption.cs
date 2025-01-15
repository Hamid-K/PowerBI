using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000390 RID: 912
	[Serializable]
	internal class RolePayloadOption : PayloadOption
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x0016B665 File Offset: 0x00169865
		// (set) Token: 0x06002DA5 RID: 11685 RVA: 0x0016B66D File Offset: 0x0016986D
		public DatabaseMirroringEndpointRole Role
		{
			get
			{
				return this._role;
			}
			set
			{
				this._role = value;
			}
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x0016B676 File Offset: 0x00169876
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x0016B682 File Offset: 0x00169882
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D63 RID: 7523
		private DatabaseMirroringEndpointRole _role;
	}
}

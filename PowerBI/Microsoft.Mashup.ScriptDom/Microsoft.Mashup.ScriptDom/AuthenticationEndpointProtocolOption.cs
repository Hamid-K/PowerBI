using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000382 RID: 898
	[Serializable]
	internal class AuthenticationEndpointProtocolOption : EndpointProtocolOption
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x0016B18B File Offset: 0x0016938B
		// (set) Token: 0x06002D46 RID: 11590 RVA: 0x0016B193 File Offset: 0x00169393
		public AuthenticationTypes AuthenticationTypes
		{
			get
			{
				return this._authenticationTypes;
			}
			set
			{
				this._authenticationTypes = value;
			}
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x0016B19C File Offset: 0x0016939C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x0016B1A8 File Offset: 0x001693A8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D48 RID: 7496
		private AuthenticationTypes _authenticationTypes;
	}
}

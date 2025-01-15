using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000368 RID: 872
	[Serializable]
	internal class UserRemoteServiceBindingOption : RemoteServiceBindingOption
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x0016A5B5 File Offset: 0x001687B5
		// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x0016A5BD File Offset: 0x001687BD
		public Identifier User
		{
			get
			{
				return this._user;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._user = value;
			}
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x0016A5CD File Offset: 0x001687CD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x0016A5D9 File Offset: 0x001687D9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.User != null)
			{
				this.User.Accept(visitor);
			}
		}

		// Token: 0x04001D17 RID: 7447
		private Identifier _user;
	}
}

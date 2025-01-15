using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002CD RID: 717
	[Serializable]
	internal class UserLoginOption : TSqlFragment
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x001667A6 File Offset: 0x001649A6
		// (set) Token: 0x060028CE RID: 10446 RVA: 0x001667AE File Offset: 0x001649AE
		public UserLoginOptionType UserLoginOptionType
		{
			get
			{
				return this._userLoginOptionType;
			}
			set
			{
				this._userLoginOptionType = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x001667B7 File Offset: 0x001649B7
		// (set) Token: 0x060028D0 RID: 10448 RVA: 0x001667BF File Offset: 0x001649BF
		public Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identifier = value;
			}
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x001667CF File Offset: 0x001649CF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x001667DB File Offset: 0x001649DB
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Identifier != null)
			{
				this.Identifier.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BF8 RID: 7160
		private UserLoginOptionType _userLoginOptionType;

		// Token: 0x04001BF9 RID: 7161
		private Identifier _identifier;
	}
}

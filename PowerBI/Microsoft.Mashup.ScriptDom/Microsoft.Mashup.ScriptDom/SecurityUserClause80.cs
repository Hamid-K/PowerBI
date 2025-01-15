using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000257 RID: 599
	[Serializable]
	internal class SecurityUserClause80 : TSqlFragment
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x00163CB1 File Offset: 0x00161EB1
		public IList<Identifier> Users
		{
			get
			{
				return this._users;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x00163CB9 File Offset: 0x00161EB9
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x00163CC1 File Offset: 0x00161EC1
		public UserType80 UserType80
		{
			get
			{
				return this._userType80;
			}
			set
			{
				this._userType80 = value;
			}
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00163CCA File Offset: 0x00161ECA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00163CD8 File Offset: 0x00161ED8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Users.Count;
			while (i < count)
			{
				this.Users[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B45 RID: 6981
		private List<Identifier> _users = new List<Identifier>();

		// Token: 0x04001B46 RID: 6982
		private UserType80 _userType80;
	}
}

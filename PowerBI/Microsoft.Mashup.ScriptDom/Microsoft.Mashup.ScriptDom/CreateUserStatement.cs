using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002CF RID: 719
	[Serializable]
	internal class CreateUserStatement : UserStatement
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x00166885 File Offset: 0x00164A85
		// (set) Token: 0x060028DA RID: 10458 RVA: 0x0016688D File Offset: 0x00164A8D
		public UserLoginOption UserLoginOption
		{
			get
			{
				return this._userLoginOption;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._userLoginOption = value;
			}
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0016689D File Offset: 0x00164A9D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x001668AC File Offset: 0x00164AAC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (this.UserLoginOption != null)
			{
				this.UserLoginOption.Accept(visitor);
			}
			int i = 0;
			int count = base.UserOptions.Count;
			while (i < count)
			{
				base.UserOptions[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001BFC RID: 7164
		private UserLoginOption _userLoginOption;
	}
}

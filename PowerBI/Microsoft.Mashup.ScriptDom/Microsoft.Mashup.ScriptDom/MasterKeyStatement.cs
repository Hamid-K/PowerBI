using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002BB RID: 699
	[Serializable]
	internal abstract class MasterKeyStatement : TSqlStatement
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x001663B3 File Offset: 0x001645B3
		// (set) Token: 0x06002884 RID: 10372 RVA: 0x001663BB File Offset: 0x001645BB
		public Literal Password
		{
			get
			{
				return this._password;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._password = value;
			}
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x001663CB File Offset: 0x001645CB
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Password != null)
			{
				this.Password.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BEB RID: 7147
		private Literal _password;
	}
}

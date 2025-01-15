using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002E1 RID: 737
	[Serializable]
	internal class OpenMasterKeyStatement : TSqlStatement
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x0016708B File Offset: 0x0016528B
		// (set) Token: 0x0600294F RID: 10575 RVA: 0x00167093 File Offset: 0x00165293
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

		// Token: 0x06002950 RID: 10576 RVA: 0x001670A3 File Offset: 0x001652A3
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x001670AF File Offset: 0x001652AF
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Password != null)
			{
				this.Password.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C1F RID: 7199
		private Literal _password;
	}
}

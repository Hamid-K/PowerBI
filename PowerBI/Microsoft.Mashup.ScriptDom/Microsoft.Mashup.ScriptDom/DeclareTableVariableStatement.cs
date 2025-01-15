using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001DC RID: 476
	[Serializable]
	internal class DeclareTableVariableStatement : TSqlStatement
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x00160334 File Offset: 0x0015E534
		// (set) Token: 0x06002323 RID: 8995 RVA: 0x0016033C File Offset: 0x0015E53C
		public DeclareTableVariableBody Body
		{
			get
			{
				return this._body;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._body = value;
			}
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0016034C File Offset: 0x0015E54C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00160358 File Offset: 0x0015E558
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Body != null)
			{
				this.Body.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A59 RID: 6745
		private DeclareTableVariableBody _body;
	}
}

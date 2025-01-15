using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000405 RID: 1029
	[Serializable]
	internal class AlterLoginAddDropCredentialStatement : AlterLoginStatement
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x0016E31F File Offset: 0x0016C51F
		// (set) Token: 0x0600305A RID: 12378 RVA: 0x0016E327 File Offset: 0x0016C527
		public bool IsAdd
		{
			get
			{
				return this._isAdd;
			}
			set
			{
				this._isAdd = value;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x0016E330 File Offset: 0x0016C530
		// (set) Token: 0x0600305C RID: 12380 RVA: 0x0016E338 File Offset: 0x0016C538
		public Identifier CredentialName
		{
			get
			{
				return this._credentialName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._credentialName = value;
			}
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x0016E348 File Offset: 0x0016C548
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x0016E354 File Offset: 0x0016C554
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.CredentialName != null)
			{
				this.CredentialName.Accept(visitor);
			}
		}

		// Token: 0x04001E23 RID: 7715
		private bool _isAdd;

		// Token: 0x04001E24 RID: 7716
		private Identifier _credentialName;
	}
}

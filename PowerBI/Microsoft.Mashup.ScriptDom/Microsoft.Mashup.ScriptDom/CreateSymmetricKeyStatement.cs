using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000394 RID: 916
	[Serializable]
	internal class CreateSymmetricKeyStatement : SymmetricKeyStatement, IAuthorization
	{
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x0016B7D5 File Offset: 0x001699D5
		public IList<KeyOption> KeyOptions
		{
			get
			{
				return this._keyOptions;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x0016B7DD File Offset: 0x001699DD
		// (set) Token: 0x06002DC2 RID: 11714 RVA: 0x0016B7E5 File Offset: 0x001699E5
		public Identifier Provider
		{
			get
			{
				return this._provider;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._provider = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x0016B7F5 File Offset: 0x001699F5
		// (set) Token: 0x06002DC4 RID: 11716 RVA: 0x0016B7FD File Offset: 0x001699FD
		public Identifier Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._owner = value;
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x0016B80D File Offset: 0x00169A0D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x0016B81C File Offset: 0x00169A1C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.KeyOptions.Count;
			while (i < count)
			{
				this.KeyOptions[i].Accept(visitor);
				i++;
			}
			if (this.Provider != null)
			{
				this.Provider.Accept(visitor);
			}
			int j = 0;
			int count2 = base.EncryptingMechanisms.Count;
			while (j < count2)
			{
				base.EncryptingMechanisms[j].Accept(visitor);
				j++;
			}
			if (this.Owner != null)
			{
				this.Owner.Accept(visitor);
			}
		}

		// Token: 0x04001D6C RID: 7532
		private List<KeyOption> _keyOptions = new List<KeyOption>();

		// Token: 0x04001D6D RID: 7533
		private Identifier _provider;

		// Token: 0x04001D6E RID: 7534
		private Identifier _owner;
	}
}

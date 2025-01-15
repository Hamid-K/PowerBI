using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200039B RID: 923
	[Serializable]
	internal class AlterSymmetricKeyStatement : SymmetricKeyStatement
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06002DE5 RID: 11749 RVA: 0x0016BA25 File Offset: 0x00169C25
		// (set) Token: 0x06002DE6 RID: 11750 RVA: 0x0016BA2D File Offset: 0x00169C2D
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

		// Token: 0x06002DE7 RID: 11751 RVA: 0x0016BA36 File Offset: 0x00169C36
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x0016BA44 File Offset: 0x00169C44
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			int i = 0;
			int count = base.EncryptingMechanisms.Count;
			while (i < count)
			{
				base.EncryptingMechanisms[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001D75 RID: 7541
		private bool _isAdd;
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000393 RID: 915
	[Serializable]
	internal abstract class SymmetricKeyStatement : TSqlStatement
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x0016B74E File Offset: 0x0016994E
		// (set) Token: 0x06002DBC RID: 11708 RVA: 0x0016B756 File Offset: 0x00169956
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x0016B766 File Offset: 0x00169966
		public IList<CryptoMechanism> EncryptingMechanisms
		{
			get
			{
				return this._encryptingMechanisms;
			}
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x0016B770 File Offset: 0x00169970
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.EncryptingMechanisms.Count;
			while (i < count)
			{
				this.EncryptingMechanisms[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D6A RID: 7530
		private Identifier _name;

		// Token: 0x04001D6B RID: 7531
		private List<CryptoMechanism> _encryptingMechanisms = new List<CryptoMechanism>();
	}
}

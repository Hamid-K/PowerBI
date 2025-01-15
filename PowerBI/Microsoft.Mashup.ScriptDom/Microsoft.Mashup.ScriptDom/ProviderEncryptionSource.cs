using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200036E RID: 878
	[Serializable]
	internal class ProviderEncryptionSource : EncryptionSource
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x0016A788 File Offset: 0x00168988
		// (set) Token: 0x06002CBF RID: 11455 RVA: 0x0016A790 File Offset: 0x00168990
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

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x0016A7A0 File Offset: 0x001689A0
		public IList<KeyOption> KeyOptions
		{
			get
			{
				return this._keyOptions;
			}
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x0016A7A8 File Offset: 0x001689A8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x0016A7B4 File Offset: 0x001689B4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.KeyOptions.Count;
			while (i < count)
			{
				this.KeyOptions[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D1D RID: 7453
		private Identifier _name;

		// Token: 0x04001D1E RID: 7454
		private List<KeyOption> _keyOptions = new List<KeyOption>();
	}
}

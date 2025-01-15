using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002DF RID: 735
	[Serializable]
	internal class OpenSymmetricKeyStatement : TSqlStatement
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x00166FBC File Offset: 0x001651BC
		// (set) Token: 0x06002941 RID: 10561 RVA: 0x00166FC4 File Offset: 0x001651C4
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

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x00166FD4 File Offset: 0x001651D4
		// (set) Token: 0x06002943 RID: 10563 RVA: 0x00166FDC File Offset: 0x001651DC
		public CryptoMechanism DecryptionMechanism
		{
			get
			{
				return this._decryptionMechanism;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._decryptionMechanism = value;
			}
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x00166FEC File Offset: 0x001651EC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x00166FF8 File Offset: 0x001651F8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.DecryptionMechanism != null)
			{
				this.DecryptionMechanism.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C1B RID: 7195
		private Identifier _name;

		// Token: 0x04001C1C RID: 7196
		private CryptoMechanism _decryptionMechanism;
	}
}

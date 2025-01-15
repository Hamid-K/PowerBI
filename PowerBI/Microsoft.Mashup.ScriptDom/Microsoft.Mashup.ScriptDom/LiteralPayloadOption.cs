using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200038C RID: 908
	[Serializable]
	internal class LiteralPayloadOption : PayloadOption
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x0016B566 File Offset: 0x00169766
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x0016B56E File Offset: 0x0016976E
		public Literal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x0016B57E File Offset: 0x0016977E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x0016B58A File Offset: 0x0016978A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001D5E RID: 7518
		private Literal _value;
	}
}

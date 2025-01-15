using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200038A RID: 906
	[Serializable]
	internal class WsdlPayloadOption : PayloadOption
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x0016B4DE File Offset: 0x001696DE
		// (set) Token: 0x06002D83 RID: 11651 RVA: 0x0016B4E6 File Offset: 0x001696E6
		public bool IsNone
		{
			get
			{
				return this._isNone;
			}
			set
			{
				this._isNone = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0016B4EF File Offset: 0x001696EF
		// (set) Token: 0x06002D85 RID: 11653 RVA: 0x0016B4F7 File Offset: 0x001696F7
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

		// Token: 0x06002D86 RID: 11654 RVA: 0x0016B507 File Offset: 0x00169707
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x0016B513 File Offset: 0x00169713
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001D5B RID: 7515
		private bool _isNone;

		// Token: 0x04001D5C RID: 7516
		private Literal _value;
	}
}

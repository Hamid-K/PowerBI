using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000451 RID: 1105
	[Serializable]
	internal class MaxRolloverFilesAuditTargetOption : AuditTargetOption
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x0016FC0E File Offset: 0x0016DE0E
		// (set) Token: 0x060031FF RID: 12799 RVA: 0x0016FC16 File Offset: 0x0016DE16
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

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x0016FC26 File Offset: 0x0016DE26
		// (set) Token: 0x06003201 RID: 12801 RVA: 0x0016FC2E File Offset: 0x0016DE2E
		public bool IsUnlimited
		{
			get
			{
				return this._isUnlimited;
			}
			set
			{
				this._isUnlimited = value;
			}
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x0016FC37 File Offset: 0x0016DE37
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x0016FC43 File Offset: 0x0016DE43
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001E90 RID: 7824
		private Literal _value;

		// Token: 0x04001E91 RID: 7825
		private bool _isUnlimited;
	}
}

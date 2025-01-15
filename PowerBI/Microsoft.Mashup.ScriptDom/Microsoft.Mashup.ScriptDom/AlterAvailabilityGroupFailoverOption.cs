using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200049A RID: 1178
	[Serializable]
	internal class AlterAvailabilityGroupFailoverOption : TSqlFragment
	{
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x001714AD File Offset: 0x0016F6AD
		// (set) Token: 0x06003392 RID: 13202 RVA: 0x001714B5 File Offset: 0x0016F6B5
		public FailoverActionOptionKind OptionKind
		{
			get
			{
				return this._optionKind;
			}
			set
			{
				this._optionKind = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x001714BE File Offset: 0x0016F6BE
		// (set) Token: 0x06003394 RID: 13204 RVA: 0x001714C6 File Offset: 0x0016F6C6
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

		// Token: 0x06003395 RID: 13205 RVA: 0x001714D6 File Offset: 0x0016F6D6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x001714E2 File Offset: 0x0016F6E2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EFB RID: 7931
		private FailoverActionOptionKind _optionKind;

		// Token: 0x04001EFC RID: 7932
		private Literal _value;
	}
}

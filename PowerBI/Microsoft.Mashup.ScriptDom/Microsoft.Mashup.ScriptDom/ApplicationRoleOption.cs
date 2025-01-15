using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002BE RID: 702
	[Serializable]
	internal class ApplicationRoleOption : TSqlFragment
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x0016643B File Offset: 0x0016463B
		// (set) Token: 0x06002890 RID: 10384 RVA: 0x00166443 File Offset: 0x00164643
		public ApplicationRoleOptionKind OptionKind
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

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x0016644C File Offset: 0x0016464C
		// (set) Token: 0x06002892 RID: 10386 RVA: 0x00166454 File Offset: 0x00164654
		public IdentifierOrValueExpression Value
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

		// Token: 0x06002893 RID: 10387 RVA: 0x00166464 File Offset: 0x00164664
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x00166470 File Offset: 0x00164670
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BED RID: 7149
		private ApplicationRoleOptionKind _optionKind;

		// Token: 0x04001BEE RID: 7150
		private IdentifierOrValueExpression _value;
	}
}

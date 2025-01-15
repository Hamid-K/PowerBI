using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200037F RID: 895
	[Serializable]
	internal class EndpointAffinity : TSqlFragment
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x0016B0C6 File Offset: 0x001692C6
		// (set) Token: 0x06002D36 RID: 11574 RVA: 0x0016B0CE File Offset: 0x001692CE
		public AffinityKind Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				this._kind = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x0016B0D7 File Offset: 0x001692D7
		// (set) Token: 0x06002D38 RID: 11576 RVA: 0x0016B0DF File Offset: 0x001692DF
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

		// Token: 0x06002D39 RID: 11577 RVA: 0x0016B0EF File Offset: 0x001692EF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x0016B0FB File Offset: 0x001692FB
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D44 RID: 7492
		private AffinityKind _kind;

		// Token: 0x04001D45 RID: 7493
		private Literal _value;
	}
}

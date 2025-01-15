using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003FC RID: 1020
	[Serializable]
	internal class LiteralPrincipalOption : PrincipalOption
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x0016DFC5 File Offset: 0x0016C1C5
		// (set) Token: 0x06003024 RID: 12324 RVA: 0x0016DFCD File Offset: 0x0016C1CD
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

		// Token: 0x06003025 RID: 12325 RVA: 0x0016DFDD File Offset: 0x0016C1DD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x0016DFE9 File Offset: 0x0016C1E9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001E14 RID: 7700
		private Literal _value;
	}
}

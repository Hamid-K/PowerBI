using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003FA RID: 1018
	[Serializable]
	internal class PrincipalOption : TSqlFragment
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06003019 RID: 12313 RVA: 0x0016DF69 File Offset: 0x0016C169
		// (set) Token: 0x0600301A RID: 12314 RVA: 0x0016DF71 File Offset: 0x0016C171
		public PrincipalOptionKind OptionKind
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

		// Token: 0x0600301B RID: 12315 RVA: 0x0016DF7A File Offset: 0x0016C17A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x0016DF86 File Offset: 0x0016C186
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E12 RID: 7698
		private PrincipalOptionKind _optionKind;
	}
}

using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001B5 RID: 437
	[Serializable]
	internal class ViewOption : TSqlFragment
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x0015F376 File Offset: 0x0015D576
		// (set) Token: 0x06002248 RID: 8776 RVA: 0x0015F37E File Offset: 0x0015D57E
		public ViewOptionKind OptionKind
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

		// Token: 0x06002249 RID: 8777 RVA: 0x0015F387 File Offset: 0x0015D587
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x0015F393 File Offset: 0x0015D593
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A1B RID: 6683
		private ViewOptionKind _optionKind;
	}
}

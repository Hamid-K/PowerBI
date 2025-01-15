using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002EB RID: 747
	[Serializable]
	internal class BackwardsCompatibleDropIndexClause : DropIndexClauseBase
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x001673AA File Offset: 0x001655AA
		// (set) Token: 0x0600297B RID: 10619 RVA: 0x001673B2 File Offset: 0x001655B2
		public ChildObjectName Index
		{
			get
			{
				return this._index;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._index = value;
			}
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x001673C2 File Offset: 0x001655C2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x001673CE File Offset: 0x001655CE
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Index != null)
			{
				this.Index.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C29 RID: 7209
		private ChildObjectName _index;
	}
}

using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003E3 RID: 995
	[Serializable]
	internal class DropSymmetricKeyStatement : DropUnownedObjectStatement
	{
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x0016D777 File Offset: 0x0016B977
		// (set) Token: 0x06002F93 RID: 12179 RVA: 0x0016D77F File Offset: 0x0016B97F
		public bool RemoveProviderKey
		{
			get
			{
				return this._removeProviderKey;
			}
			set
			{
				this._removeProviderKey = value;
			}
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x0016D788 File Offset: 0x0016B988
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x0016D794 File Offset: 0x0016B994
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DEE RID: 7662
		private bool _removeProviderKey;
	}
}

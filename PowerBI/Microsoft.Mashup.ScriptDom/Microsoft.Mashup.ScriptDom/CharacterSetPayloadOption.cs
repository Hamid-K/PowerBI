using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200038F RID: 911
	[Serializable]
	internal class CharacterSetPayloadOption : PayloadOption
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x0016B637 File Offset: 0x00169837
		// (set) Token: 0x06002DA0 RID: 11680 RVA: 0x0016B63F File Offset: 0x0016983F
		public bool IsSql
		{
			get
			{
				return this._isSql;
			}
			set
			{
				this._isSql = value;
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x0016B648 File Offset: 0x00169848
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x0016B654 File Offset: 0x00169854
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D62 RID: 7522
		private bool _isSql;
	}
}

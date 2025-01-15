using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003E4 RID: 996
	[Serializable]
	internal class DropAsymmetricKeyStatement : DropUnownedObjectStatement
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06002F97 RID: 12183 RVA: 0x0016D7A5 File Offset: 0x0016B9A5
		// (set) Token: 0x06002F98 RID: 12184 RVA: 0x0016D7AD File Offset: 0x0016B9AD
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

		// Token: 0x06002F99 RID: 12185 RVA: 0x0016D7B6 File Offset: 0x0016B9B6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x0016D7C2 File Offset: 0x0016B9C2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DEF RID: 7663
		private bool _removeProviderKey;
	}
}

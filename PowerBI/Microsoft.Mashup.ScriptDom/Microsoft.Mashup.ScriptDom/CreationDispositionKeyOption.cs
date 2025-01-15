using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200039A RID: 922
	[Serializable]
	internal class CreationDispositionKeyOption : KeyOption
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x0016B9F7 File Offset: 0x00169BF7
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x0016B9FF File Offset: 0x00169BFF
		public bool IsCreateNew
		{
			get
			{
				return this._isCreateNew;
			}
			set
			{
				this._isCreateNew = value;
			}
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x0016BA08 File Offset: 0x00169C08
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x0016BA14 File Offset: 0x00169C14
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D74 RID: 7540
		private bool _isCreateNew;
	}
}

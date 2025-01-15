using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003EB RID: 1003
	[Serializable]
	internal class SimpleAlterFullTextIndexAction : AlterFullTextIndexAction
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x0016D98E File Offset: 0x0016BB8E
		// (set) Token: 0x06002FBC RID: 12220 RVA: 0x0016D996 File Offset: 0x0016BB96
		public SimpleAlterFullTextIndexActionKind ActionKind
		{
			get
			{
				return this._actionKind;
			}
			set
			{
				this._actionKind = value;
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x0016D99F File Offset: 0x0016BB9F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x0016D9AB File Offset: 0x0016BBAB
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DF7 RID: 7671
		private SimpleAlterFullTextIndexActionKind _actionKind;
	}
}

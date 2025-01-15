using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200046B RID: 1131
	[Serializable]
	internal class AlterFullTextStopListStatement : TSqlStatement
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06003284 RID: 12932 RVA: 0x00170390 File Offset: 0x0016E590
		// (set) Token: 0x06003285 RID: 12933 RVA: 0x00170398 File Offset: 0x0016E598
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06003286 RID: 12934 RVA: 0x001703A8 File Offset: 0x0016E5A8
		// (set) Token: 0x06003287 RID: 12935 RVA: 0x001703B0 File Offset: 0x0016E5B0
		public FullTextStopListAction Action
		{
			get
			{
				return this._action;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._action = value;
			}
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x001703C0 File Offset: 0x0016E5C0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x001703CC File Offset: 0x0016E5CC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.Action != null)
			{
				this.Action.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EB0 RID: 7856
		private Identifier _name;

		// Token: 0x04001EB1 RID: 7857
		private FullTextStopListAction _action;
	}
}

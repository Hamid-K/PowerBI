using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200030D RID: 781
	[Serializable]
	internal class SetTransactionIsolationLevelStatement : TSqlStatement
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x00167D35 File Offset: 0x00165F35
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x00167D3D File Offset: 0x00165F3D
		public IsolationLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x00167D46 File Offset: 0x00165F46
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x00167D52 File Offset: 0x00165F52
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C51 RID: 7249
		private IsolationLevel _level;
	}
}

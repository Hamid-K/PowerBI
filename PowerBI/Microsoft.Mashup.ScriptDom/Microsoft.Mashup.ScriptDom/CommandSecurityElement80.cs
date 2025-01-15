using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000254 RID: 596
	[Serializable]
	internal class CommandSecurityElement80 : SecurityElement80
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x00163B2D File Offset: 0x00161D2D
		// (set) Token: 0x06002620 RID: 9760 RVA: 0x00163B35 File Offset: 0x00161D35
		public bool All
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x00163B3E File Offset: 0x00161D3E
		// (set) Token: 0x06002622 RID: 9762 RVA: 0x00163B46 File Offset: 0x00161D46
		public CommandOptions CommandOptions
		{
			get
			{
				return this._commandOptions;
			}
			set
			{
				this._commandOptions = value;
			}
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00163B4F File Offset: 0x00161D4F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00163B5B File Offset: 0x00161D5B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B3E RID: 6974
		private bool _all;

		// Token: 0x04001B3F RID: 6975
		private CommandOptions _commandOptions;
	}
}

using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200042D RID: 1069
	[Serializable]
	internal class TSqlStatementSnippet : TSqlStatement
	{
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x0016F12E File Offset: 0x0016D32E
		// (set) Token: 0x0600314B RID: 12619 RVA: 0x0016F136 File Offset: 0x0016D336
		public string Script
		{
			get
			{
				return this._script;
			}
			set
			{
				this._script = value;
			}
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x0016F13F File Offset: 0x0016D33F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x0016F14B File Offset: 0x0016D34B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E64 RID: 7780
		private string _script;
	}
}

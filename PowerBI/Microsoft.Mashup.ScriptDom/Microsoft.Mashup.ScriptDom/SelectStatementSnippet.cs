using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200042A RID: 1066
	[Serializable]
	internal class SelectStatementSnippet : SelectStatement
	{
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x0016F0A4 File Offset: 0x0016D2A4
		// (set) Token: 0x0600313C RID: 12604 RVA: 0x0016F0AC File Offset: 0x0016D2AC
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

		// Token: 0x0600313D RID: 12605 RVA: 0x0016F0B5 File Offset: 0x0016D2B5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x0016F0C1 File Offset: 0x0016D2C1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E61 RID: 7777
		private string _script;
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	internal class StatementList : TSqlFragment
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x0015EC9E File Offset: 0x0015CE9E
		public IList<TSqlStatement> Statements
		{
			get
			{
				return this._statements;
			}
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x0015ECA6 File Offset: 0x0015CEA6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0015ECB4 File Offset: 0x0015CEB4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Statements.Count;
			while (i < count)
			{
				this.Statements[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x040019FF RID: 6655
		private List<TSqlStatement> _statements = new List<TSqlStatement>();
	}
}

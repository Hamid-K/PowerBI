using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000430 RID: 1072
	[Serializable]
	internal class TSqlBatch : TSqlFragment
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x0016F1F1 File Offset: 0x0016D3F1
		public IList<TSqlStatement> Statements
		{
			get
			{
				return this._statements;
			}
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x0016F1F9 File Offset: 0x0016D3F9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x0016F208 File Offset: 0x0016D408
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

		// Token: 0x04001E67 RID: 7783
		private List<TSqlStatement> _statements = new List<TSqlStatement>();
	}
}

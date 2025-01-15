using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200042F RID: 1071
	[Serializable]
	internal class TSqlScript : TSqlFragment
	{
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06003154 RID: 12628 RVA: 0x0016F18A File Offset: 0x0016D38A
		public IList<TSqlBatch> Batches
		{
			get
			{
				return this._batches;
			}
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x0016F192 File Offset: 0x0016D392
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x0016F1A0 File Offset: 0x0016D3A0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Batches.Count;
			while (i < count)
			{
				this.Batches[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E66 RID: 7782
		private List<TSqlBatch> _batches = new List<TSqlBatch>();
	}
}

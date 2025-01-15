using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003A7 RID: 935
	[Serializable]
	internal class ComputeClause : TSqlFragment
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x0016BFB5 File Offset: 0x0016A1B5
		public IList<ComputeFunction> ComputeFunctions
		{
			get
			{
				return this._computeFunctions;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x0016BFBD File Offset: 0x0016A1BD
		public IList<ScalarExpression> ByExpressions
		{
			get
			{
				return this._byExpressions;
			}
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x0016BFC5 File Offset: 0x0016A1C5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x0016BFD4 File Offset: 0x0016A1D4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.ComputeFunctions.Count;
			while (i < count)
			{
				this.ComputeFunctions[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.ByExpressions.Count;
			while (j < count2)
			{
				this.ByExpressions[j].Accept(visitor);
				j++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D8A RID: 7562
		private List<ComputeFunction> _computeFunctions = new List<ComputeFunction>();

		// Token: 0x04001D8B RID: 7563
		private List<ScalarExpression> _byExpressions = new List<ScalarExpression>();
	}
}

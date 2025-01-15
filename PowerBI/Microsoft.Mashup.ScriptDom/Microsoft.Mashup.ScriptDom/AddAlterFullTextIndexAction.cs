using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003EF RID: 1007
	[Serializable]
	internal class AddAlterFullTextIndexAction : AlterFullTextIndexAction
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x0016DAE9 File Offset: 0x0016BCE9
		public IList<FullTextIndexColumn> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x0016DAF1 File Offset: 0x0016BCF1
		// (set) Token: 0x06002FD6 RID: 12246 RVA: 0x0016DAF9 File Offset: 0x0016BCF9
		public bool WithNoPopulation
		{
			get
			{
				return this._withNoPopulation;
			}
			set
			{
				this._withNoPopulation = value;
			}
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x0016DB02 File Offset: 0x0016BD02
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x0016DB10 File Offset: 0x0016BD10
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DFE RID: 7678
		private List<FullTextIndexColumn> _columns = new List<FullTextIndexColumn>();

		// Token: 0x04001DFF RID: 7679
		private bool _withNoPopulation;
	}
}

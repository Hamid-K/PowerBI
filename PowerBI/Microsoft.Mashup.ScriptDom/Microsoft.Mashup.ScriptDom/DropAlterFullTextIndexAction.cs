using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003EE RID: 1006
	[Serializable]
	internal class DropAlterFullTextIndexAction : AlterFullTextIndexAction
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06002FCE RID: 12238 RVA: 0x0016DA70 File Offset: 0x0016BC70
		public IList<Identifier> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x0016DA78 File Offset: 0x0016BC78
		// (set) Token: 0x06002FD0 RID: 12240 RVA: 0x0016DA80 File Offset: 0x0016BC80
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

		// Token: 0x06002FD1 RID: 12241 RVA: 0x0016DA89 File Offset: 0x0016BC89
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x0016DA98 File Offset: 0x0016BC98
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

		// Token: 0x04001DFC RID: 7676
		private List<Identifier> _columns = new List<Identifier>();

		// Token: 0x04001DFD RID: 7677
		private bool _withNoPopulation;
	}
}

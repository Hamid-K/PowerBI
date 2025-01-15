using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001E0 RID: 480
	[Serializable]
	internal abstract class TableReferenceWithAliasAndColumns : TableReferenceWithAlias
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x00160489 File Offset: 0x0015E689
		public IList<Identifier> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00160494 File Offset: 0x0015E694
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A5E RID: 6750
		private List<Identifier> _columns = new List<Identifier>();
	}
}

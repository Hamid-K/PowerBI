using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000359 RID: 857
	[Serializable]
	internal class InsertBulkStatement : BulkInsertBase
	{
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06002C37 RID: 11319 RVA: 0x00169EBF File Offset: 0x001680BF
		public IList<InsertBulkColumnDefinition> ColumnDefinitions
		{
			get
			{
				return this._columnDefinitions;
			}
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x00169EC7 File Offset: 0x001680C7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x00169ED4 File Offset: 0x001680D4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.ColumnDefinitions.Count;
			while (i < count)
			{
				this.ColumnDefinitions[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001CF4 RID: 7412
		private List<InsertBulkColumnDefinition> _columnDefinitions = new List<InsertBulkColumnDefinition>();
	}
}

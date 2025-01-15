using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003C7 RID: 967
	[Serializable]
	internal class FromClause : TSqlFragment
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x0016CEB5 File Offset: 0x0016B0B5
		public IList<TableReference> TableReferences
		{
			get
			{
				return this._tableReferences;
			}
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0016CEBD File Offset: 0x0016B0BD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x0016CECC File Offset: 0x0016B0CC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.TableReferences.Count;
			while (i < count)
			{
				this.TableReferences[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DCB RID: 7627
		private List<TableReference> _tableReferences = new List<TableReference>();
	}
}

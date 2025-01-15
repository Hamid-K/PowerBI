using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001DA RID: 474
	[Serializable]
	internal class TableDefinition : TSqlFragment
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x0016020C File Offset: 0x0015E40C
		public IList<ColumnDefinition> ColumnDefinitions
		{
			get
			{
				return this._columnDefinitions;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x00160214 File Offset: 0x0015E414
		public IList<ConstraintDefinition> TableConstraints
		{
			get
			{
				return this._tableConstraints;
			}
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x0016021C File Offset: 0x0015E41C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00160228 File Offset: 0x0015E428
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.ColumnDefinitions.Count;
			while (i < count)
			{
				this.ColumnDefinitions[i].Accept(visitor);
				i++;
			}
			int j = 0;
			int count2 = this.TableConstraints.Count;
			while (j < count2)
			{
				this.TableConstraints[j].Accept(visitor);
				j++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A54 RID: 6740
		private List<ColumnDefinition> _columnDefinitions = new List<ColumnDefinition>();

		// Token: 0x04001A55 RID: 6741
		private List<ConstraintDefinition> _tableConstraints = new List<ConstraintDefinition>();
	}
}

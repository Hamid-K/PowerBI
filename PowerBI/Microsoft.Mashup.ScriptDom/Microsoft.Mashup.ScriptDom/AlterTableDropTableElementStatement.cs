using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000291 RID: 657
	[Serializable]
	internal class AlterTableDropTableElementStatement : AlterTableStatement
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x001650B9 File Offset: 0x001632B9
		public IList<AlterTableDropTableElement> AlterTableDropTableElements
		{
			get
			{
				return this._alterTableDropTableElements;
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x001650C1 File Offset: 0x001632C1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x001650D0 File Offset: 0x001632D0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.SchemaObjectName != null)
			{
				base.SchemaObjectName.Accept(visitor);
			}
			int i = 0;
			int count = this.AlterTableDropTableElements.Count;
			while (i < count)
			{
				this.AlterTableDropTableElements[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001B97 RID: 7063
		private List<AlterTableDropTableElement> _alterTableDropTableElements = new List<AlterTableDropTableElement>();
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000282 RID: 642
	[Serializable]
	internal class AlterTableSetStatement : AlterTableStatement
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x00164B85 File Offset: 0x00162D85
		public IList<TableOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x00164B8D File Offset: 0x00162D8D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x00164B9C File Offset: 0x00162D9C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.SchemaObjectName != null)
			{
				base.SchemaObjectName.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001B80 RID: 7040
		private List<TableOption> _options = new List<TableOption>();
	}
}

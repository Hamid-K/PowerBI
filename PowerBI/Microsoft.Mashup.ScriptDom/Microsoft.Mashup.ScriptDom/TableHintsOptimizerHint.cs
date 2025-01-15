using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001F8 RID: 504
	[Serializable]
	internal class TableHintsOptimizerHint : OptimizerHint
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x00160E34 File Offset: 0x0015F034
		// (set) Token: 0x060023C0 RID: 9152 RVA: 0x00160E3C File Offset: 0x0015F03C
		public SchemaObjectName ObjectName
		{
			get
			{
				return this._objectName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._objectName = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x00160E4C File Offset: 0x0015F04C
		public IList<TableHint> TableHints
		{
			get
			{
				return this._tableHints;
			}
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x00160E54 File Offset: 0x0015F054
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x00160E60 File Offset: 0x0015F060
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.ObjectName != null)
			{
				this.ObjectName.Accept(visitor);
			}
			int i = 0;
			int count = this.TableHints.Count;
			while (i < count)
			{
				this.TableHints[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A86 RID: 6790
		private SchemaObjectName _objectName;

		// Token: 0x04001A87 RID: 6791
		private List<TableHint> _tableHints = new List<TableHint>();
	}
}

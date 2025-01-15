using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020004A0 RID: 1184
	[Serializable]
	internal class DiskStatement : TSqlStatement
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x0017176C File Offset: 0x0016F96C
		// (set) Token: 0x060033BE RID: 13246 RVA: 0x00171774 File Offset: 0x0016F974
		public DiskStatementType DiskStatementType
		{
			get
			{
				return this._diskStatementType;
			}
			set
			{
				this._diskStatementType = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x0017177D File Offset: 0x0016F97D
		public IList<DiskStatementOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x00171785 File Offset: 0x0016F985
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x00171794 File Offset: 0x0016F994
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001F08 RID: 7944
		private DiskStatementType _diskStatementType;

		// Token: 0x04001F09 RID: 7945
		private List<DiskStatementOption> _options = new List<DiskStatementOption>();
	}
}

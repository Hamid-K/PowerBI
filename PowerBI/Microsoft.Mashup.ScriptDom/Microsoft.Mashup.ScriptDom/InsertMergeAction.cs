using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000437 RID: 1079
	[Serializable]
	internal class InsertMergeAction : MergeAction
	{
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x0016F4A6 File Offset: 0x0016D6A6
		public IList<ColumnReferenceExpression> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x0016F4AE File Offset: 0x0016D6AE
		// (set) Token: 0x0600317F RID: 12671 RVA: 0x0016F4B6 File Offset: 0x0016D6B6
		public ValuesInsertSource Source
		{
			get
			{
				return this._source;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._source = value;
			}
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x0016F4C6 File Offset: 0x0016D6C6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x0016F4D4 File Offset: 0x0016D6D4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			if (this.Source != null)
			{
				this.Source.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E71 RID: 7793
		private List<ColumnReferenceExpression> _columns = new List<ColumnReferenceExpression>();

		// Token: 0x04001E72 RID: 7794
		private ValuesInsertSource _source;
	}
}

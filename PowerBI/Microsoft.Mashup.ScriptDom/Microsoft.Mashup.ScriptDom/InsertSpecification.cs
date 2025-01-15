using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200023C RID: 572
	[Serializable]
	internal class InsertSpecification : DataModificationSpecification
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x00162E67 File Offset: 0x00161067
		// (set) Token: 0x06002575 RID: 9589 RVA: 0x00162E6F File Offset: 0x0016106F
		public InsertOption InsertOption
		{
			get
			{
				return this._insertOption;
			}
			set
			{
				this._insertOption = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x00162E78 File Offset: 0x00161078
		// (set) Token: 0x06002577 RID: 9591 RVA: 0x00162E80 File Offset: 0x00161080
		public InsertSource InsertSource
		{
			get
			{
				return this._insertSource;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._insertSource = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x00162E90 File Offset: 0x00161090
		public IList<ColumnReferenceExpression> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x00162E98 File Offset: 0x00161098
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x00162EA4 File Offset: 0x001610A4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.InsertSource != null)
			{
				this.InsertSource.Accept(visitor);
			}
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001B07 RID: 6919
		private InsertOption _insertOption;

		// Token: 0x04001B08 RID: 6920
		private InsertSource _insertSource;

		// Token: 0x04001B09 RID: 6921
		private List<ColumnReferenceExpression> _columns = new List<ColumnReferenceExpression>();
	}
}

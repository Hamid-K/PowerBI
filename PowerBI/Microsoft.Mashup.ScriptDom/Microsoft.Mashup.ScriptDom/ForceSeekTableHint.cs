using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001F9 RID: 505
	[Serializable]
	internal class ForceSeekTableHint : TableHint
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x00160EC5 File Offset: 0x0015F0C5
		// (set) Token: 0x060023C6 RID: 9158 RVA: 0x00160ECD File Offset: 0x0015F0CD
		public IdentifierOrValueExpression IndexValue
		{
			get
			{
				return this._indexValue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._indexValue = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x00160EDD File Offset: 0x0015F0DD
		public IList<ColumnReferenceExpression> ColumnValues
		{
			get
			{
				return this._columnValues;
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x00160EE5 File Offset: 0x0015F0E5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x00160EF4 File Offset: 0x0015F0F4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.IndexValue != null)
			{
				this.IndexValue.Accept(visitor);
			}
			int i = 0;
			int count = this.ColumnValues.Count;
			while (i < count)
			{
				this.ColumnValues[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A88 RID: 6792
		private IdentifierOrValueExpression _indexValue;

		// Token: 0x04001A89 RID: 6793
		private List<ColumnReferenceExpression> _columnValues = new List<ColumnReferenceExpression>();
	}
}

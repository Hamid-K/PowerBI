using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002E6 RID: 742
	[Serializable]
	internal class WhereClause : TSqlFragment
	{
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x001671F9 File Offset: 0x001653F9
		// (set) Token: 0x06002967 RID: 10599 RVA: 0x00167201 File Offset: 0x00165401
		public BooleanExpression SearchCondition
		{
			get
			{
				return this._searchCondition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._searchCondition = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x00167211 File Offset: 0x00165411
		// (set) Token: 0x06002969 RID: 10601 RVA: 0x00167219 File Offset: 0x00165419
		public CursorId Cursor
		{
			get
			{
				return this._cursor;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._cursor = value;
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x00167229 File Offset: 0x00165429
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x00167235 File Offset: 0x00165435
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SearchCondition != null)
			{
				this.SearchCondition.Accept(visitor);
			}
			if (this.Cursor != null)
			{
				this.Cursor.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C24 RID: 7204
		private BooleanExpression _searchCondition;

		// Token: 0x04001C25 RID: 7205
		private CursorId _cursor;
	}
}

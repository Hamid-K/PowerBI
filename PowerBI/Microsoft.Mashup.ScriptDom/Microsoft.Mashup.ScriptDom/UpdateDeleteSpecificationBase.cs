using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000239 RID: 569
	[Serializable]
	internal abstract class UpdateDeleteSpecificationBase : DataModificationSpecification
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x00162D98 File Offset: 0x00160F98
		// (set) Token: 0x06002567 RID: 9575 RVA: 0x00162DA0 File Offset: 0x00160FA0
		public FromClause FromClause
		{
			get
			{
				return this._fromClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fromClause = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x00162DB0 File Offset: 0x00160FB0
		// (set) Token: 0x06002569 RID: 9577 RVA: 0x00162DB8 File Offset: 0x00160FB8
		public WhereClause WhereClause
		{
			get
			{
				return this._whereClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._whereClause = value;
			}
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x00162DC8 File Offset: 0x00160FC8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FromClause != null)
			{
				this.FromClause.Accept(visitor);
			}
			if (this.WhereClause != null)
			{
				this.WhereClause.Accept(visitor);
			}
		}

		// Token: 0x04001B04 RID: 6916
		private FromClause _fromClause;

		// Token: 0x04001B05 RID: 6917
		private WhereClause _whereClause;
	}
}

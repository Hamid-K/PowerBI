using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000695 RID: 1685
	internal sealed class QueryExpr : Node
	{
		// Token: 0x06004F73 RID: 20339 RVA: 0x001207B1 File Offset: 0x0011E9B1
		internal QueryExpr(SelectClause selectClause, FromClause fromClause, Node whereClause, GroupByClause groupByClause, HavingClause havingClause, OrderByClause orderByClause)
		{
			this._selectClause = selectClause;
			this._fromClause = fromClause;
			this._whereClause = whereClause;
			this._groupByClause = groupByClause;
			this._havingClause = havingClause;
			this._orderByClause = orderByClause;
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06004F74 RID: 20340 RVA: 0x001207E6 File Offset: 0x0011E9E6
		internal SelectClause SelectClause
		{
			get
			{
				return this._selectClause;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06004F75 RID: 20341 RVA: 0x001207EE File Offset: 0x0011E9EE
		internal FromClause FromClause
		{
			get
			{
				return this._fromClause;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06004F76 RID: 20342 RVA: 0x001207F6 File Offset: 0x0011E9F6
		internal Node WhereClause
		{
			get
			{
				return this._whereClause;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06004F77 RID: 20343 RVA: 0x001207FE File Offset: 0x0011E9FE
		internal GroupByClause GroupByClause
		{
			get
			{
				return this._groupByClause;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x00120806 File Offset: 0x0011EA06
		internal HavingClause HavingClause
		{
			get
			{
				return this._havingClause;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06004F79 RID: 20345 RVA: 0x0012080E File Offset: 0x0011EA0E
		internal OrderByClause OrderByClause
		{
			get
			{
				return this._orderByClause;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06004F7A RID: 20346 RVA: 0x00120816 File Offset: 0x0011EA16
		internal bool HasMethodCall
		{
			get
			{
				return this._selectClause.HasMethodCall || (this._havingClause != null && this._havingClause.HasMethodCall) || (this._orderByClause != null && this._orderByClause.HasMethodCall);
			}
		}

		// Token: 0x04001D19 RID: 7449
		private readonly SelectClause _selectClause;

		// Token: 0x04001D1A RID: 7450
		private readonly FromClause _fromClause;

		// Token: 0x04001D1B RID: 7451
		private readonly Node _whereClause;

		// Token: 0x04001D1C RID: 7452
		private readonly GroupByClause _groupByClause;

		// Token: 0x04001D1D RID: 7453
		private readonly HavingClause _havingClause;

		// Token: 0x04001D1E RID: 7454
		private readonly OrderByClause _orderByClause;
	}
}

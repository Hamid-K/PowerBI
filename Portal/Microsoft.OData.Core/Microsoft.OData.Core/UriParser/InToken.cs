using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000114 RID: 276
	public sealed class InToken : QueryToken
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x000265C8 File Offset: 0x000247C8
		public InToken(QueryToken left, QueryToken right)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(left, "left");
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(right, "right");
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x000265F6 File Offset: 0x000247F6
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.In;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x000265FA File Offset: 0x000247FA
		public QueryToken Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00026602 File Offset: 0x00024802
		public QueryToken Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0002660A File Offset: 0x0002480A
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400078C RID: 1932
		private readonly QueryToken left;

		// Token: 0x0400078D RID: 1933
		private readonly QueryToken right;
	}
}

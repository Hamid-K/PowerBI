using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000125 RID: 293
	public sealed class InToken : QueryToken
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x0002CE7F File Offset: 0x0002B07F
		public InToken(QueryToken left, QueryToken right)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(left, "left");
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(right, "right");
			this.left = left;
			this.right = right;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0002CEAD File Offset: 0x0002B0AD
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.In;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0002CEB1 File Offset: 0x0002B0B1
		public QueryToken Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0002CEB9 File Offset: 0x0002B0B9
		public QueryToken Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002CEC1 File Offset: 0x0002B0C1
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000670 RID: 1648
		private readonly QueryToken left;

		// Token: 0x04000671 RID: 1649
		private readonly QueryToken right;
	}
}

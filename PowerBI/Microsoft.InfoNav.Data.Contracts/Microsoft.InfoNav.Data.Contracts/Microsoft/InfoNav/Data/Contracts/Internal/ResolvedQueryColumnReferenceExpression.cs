using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000216 RID: 534
	[ImmutableObject(true)]
	public sealed class ResolvedQueryColumnReferenceExpression : ResolvedQueryExpression
	{
		// Token: 0x06000F74 RID: 3956 RVA: 0x0001D95F File Offset: 0x0001BB5F
		internal ResolvedQueryColumnReferenceExpression(ResolvedQueryExpression source, string selectName)
		{
			this._source = source;
			this._selectName = selectName;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0001D975 File Offset: 0x0001BB75
		public ResolvedQueryExpression Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0001D97D File Offset: 0x0001BB7D
		public string SelectName
		{
			get
			{
				return this._selectName;
			}
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0001D985 File Offset: 0x0001BB85
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0001D98E File Offset: 0x0001BB8E
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0001D997 File Offset: 0x0001BB97
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryColumnReferenceExpression);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0001D9A6 File Offset: 0x0001BBA6
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x0400072B RID: 1835
		private readonly ResolvedQueryExpression _source;

		// Token: 0x0400072C RID: 1836
		private readonly string _selectName;
	}
}

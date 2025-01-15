using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000254 RID: 596
	[ImmutableObject(true)]
	public sealed class ResolvedQuerySourceRefExpression : ResolvedQueryExpression
	{
		// Token: 0x060011F2 RID: 4594 RVA: 0x0001FDAD File Offset: 0x0001DFAD
		internal ResolvedQuerySourceRefExpression(string sourceName, IConceptualEntity entity)
		{
			this._sourceName = sourceName;
			this._entity = entity;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x0001FDC3 File Offset: 0x0001DFC3
		public ResolvedQuerySourceRefExpression(IConceptualEntity entity)
			: this(null, entity)
		{
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0001FDCD File Offset: 0x0001DFCD
		public string SourceName
		{
			get
			{
				return this._sourceName;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0001FDD5 File Offset: 0x0001DFD5
		public IConceptualEntity SourceEntity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0001FDDD File Offset: 0x0001DFDD
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0001FDE6 File Offset: 0x0001DFE6
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0001FDEF File Offset: 0x0001DFEF
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQuerySourceRefExpression);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0001FDFE File Offset: 0x0001DFFE
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x040007A5 RID: 1957
		private readonly IConceptualEntity _entity;

		// Token: 0x040007A6 RID: 1958
		private readonly string _sourceName;
	}
}

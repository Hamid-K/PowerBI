using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001B3 RID: 435
	internal sealed class QuerySampleCartesianPointsByCoverExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x0003D134 File Offset: 0x0003B334
		internal QuerySampleCartesianPointsByCoverExpression(ConceptualResultType conceptualResultType, QueryExpression maxTargetPointCount, QueryExpressionBinding input, QueryExpression x, QueryExpression y, QueryExpression radius, QueryExpression maxMinRatio, QueryExpression maxBlankRatio)
			: base(conceptualResultType)
		{
			this._maxTargetPointCount = maxTargetPointCount;
			this._input = input;
			this._x = x ?? QuerySampleCartesianPointsByCoverExpression.DefaultAxisExpression;
			this._y = y ?? QuerySampleCartesianPointsByCoverExpression.DefaultAxisExpression;
			this._radius = radius;
			this._maxMinRatio = maxMinRatio;
			this._maxBlankRatio = maxBlankRatio;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0003D190 File Offset: 0x0003B390
		public QueryExpression MaxTargetPointCount
		{
			get
			{
				return this._maxTargetPointCount;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x0003D198 File Offset: 0x0003B398
		public QueryExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0003D1A0 File Offset: 0x0003B3A0
		public QueryExpression X
		{
			get
			{
				return this._x;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x0003D1A8 File Offset: 0x0003B3A8
		public QueryExpression Y
		{
			get
			{
				return this._y;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0003D1B0 File Offset: 0x0003B3B0
		public QueryExpression Radius
		{
			get
			{
				return this._radius;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x0003D1B8 File Offset: 0x0003B3B8
		public QueryExpression MaxMinRatio
		{
			get
			{
				return this._maxMinRatio;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0003D1C0 File Offset: 0x0003B3C0
		public QueryExpression MaxBlankRatio
		{
			get
			{
				return this._maxBlankRatio;
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0003D1C8 File Offset: 0x0003B3C8
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0003D1D4 File Offset: 0x0003B3D4
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QuerySampleCartesianPointsByCoverExpression querySampleCartesianPointsByCoverExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QuerySampleCartesianPointsByCoverExpression>(this, other, out flag, out querySampleCartesianPointsByCoverExpression))
			{
				return flag;
			}
			return this.MaxTargetPointCount.Equals(querySampleCartesianPointsByCoverExpression.MaxTargetPointCount) && this.Input.Equals(querySampleCartesianPointsByCoverExpression.Input) && this.X.Equals(querySampleCartesianPointsByCoverExpression.X) && this.Y.Equals(querySampleCartesianPointsByCoverExpression.Y) && this.Radius.Equals(querySampleCartesianPointsByCoverExpression.Radius) && this.MaxMinRatio.Equals(querySampleCartesianPointsByCoverExpression.MaxMinRatio) && this.MaxBlankRatio.Equals(querySampleCartesianPointsByCoverExpression.MaxBlankRatio);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0003D278 File Offset: 0x0003B478
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<QueryExpression>(this.MaxTargetPointCount, null), Hashing.GetHashCode<QueryExpressionBinding>(this.Input, null), Hashing.GetHashCode<QueryExpression>(this.X, null), Hashing.GetHashCode<QueryExpression>(this.Y, null), Hashing.GetHashCode<QueryExpression>(this.Radius, null), Hashing.GetHashCode<QueryExpression>(this.MaxMinRatio, null), Hashing.GetHashCode<QueryExpression>(this.MaxBlankRatio, null));
		}

		// Token: 0x04000BBF RID: 3007
		private static readonly QueryExpression DefaultAxisExpression = Literals.ZeroInt64;

		// Token: 0x04000BC0 RID: 3008
		private readonly QueryExpression _maxTargetPointCount;

		// Token: 0x04000BC1 RID: 3009
		private readonly QueryExpressionBinding _input;

		// Token: 0x04000BC2 RID: 3010
		private readonly QueryExpression _x;

		// Token: 0x04000BC3 RID: 3011
		private readonly QueryExpression _y;

		// Token: 0x04000BC4 RID: 3012
		private readonly QueryExpression _radius;

		// Token: 0x04000BC5 RID: 3013
		private readonly QueryExpression _maxMinRatio;

		// Token: 0x04000BC6 RID: 3014
		private readonly QueryExpression _maxBlankRatio;
	}
}

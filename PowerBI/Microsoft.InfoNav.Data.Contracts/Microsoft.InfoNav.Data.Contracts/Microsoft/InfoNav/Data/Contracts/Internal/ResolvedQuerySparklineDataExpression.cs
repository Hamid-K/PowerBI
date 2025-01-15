using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000255 RID: 597
	[ImmutableObject(true)]
	public sealed class ResolvedQuerySparklineDataExpression : ResolvedQueryExpression
	{
		// Token: 0x060011FA RID: 4602 RVA: 0x0001FE07 File Offset: 0x0001E007
		internal ResolvedQuerySparklineDataExpression(ResolvedQueryExpression measure, IReadOnlyList<ResolvedQueryExpression> groupings, int pointsPerSparkline, ResolvedQueryExpression scalarKey, bool includeMinGroupingInterval)
		{
			this._measure = measure;
			this._groupings = groupings;
			this._pointsPerSparkline = pointsPerSparkline;
			this._scalarKey = scalarKey;
			this._includeMinGroupingInterval = includeMinGroupingInterval;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0001FE34 File Offset: 0x0001E034
		public ResolvedQueryExpression Measure
		{
			get
			{
				return this._measure;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0001FE3C File Offset: 0x0001E03C
		public IReadOnlyList<ResolvedQueryExpression> Groupings
		{
			get
			{
				return this._groupings;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0001FE44 File Offset: 0x0001E044
		public int PointsPerSparkline
		{
			get
			{
				return this._pointsPerSparkline;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0001FE4C File Offset: 0x0001E04C
		public ResolvedQueryExpression ScalarKey
		{
			get
			{
				return this._scalarKey;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0001FE54 File Offset: 0x0001E054
		public bool IncludeMinGroupingInterval
		{
			get
			{
				return this._includeMinGroupingInterval;
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0001FE5C File Offset: 0x0001E05C
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0001FE65 File Offset: 0x0001E065
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0001FE6E File Offset: 0x0001E06E
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQuerySparklineDataExpression);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0001FE7D File Offset: 0x0001E07D
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x040007A7 RID: 1959
		private readonly ResolvedQueryExpression _measure;

		// Token: 0x040007A8 RID: 1960
		private readonly IReadOnlyList<ResolvedQueryExpression> _groupings;

		// Token: 0x040007A9 RID: 1961
		private readonly int _pointsPerSparkline;

		// Token: 0x040007AA RID: 1962
		private readonly ResolvedQueryExpression _scalarKey;

		// Token: 0x040007AB RID: 1963
		private readonly bool _includeMinGroupingInterval;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001B1 RID: 433
	internal sealed class QuerySampleAxisWithLocalMinMaxExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060015EC RID: 5612 RVA: 0x0003CF14 File Offset: 0x0003B114
		internal QuerySampleAxisWithLocalMinMaxExpression(ConceptualResultType conceptualResultType, QueryExpression maxTargetPointCount, QueryExpressionBinding input, QueryExpression axis, IReadOnlyList<QueryExpression> measures, QueryExpression minPointsResolution, IReadOnlyList<QueryExpression> series, DynamicSeriesSelectionCriteria dynamicSeriesSelectionCriteria, SortDirection dynamicSeriesSelectionCriteriaOrder, QueryExpression maxPointsResolution, QueryExpression maxDynamicSeriesCount)
			: base(conceptualResultType)
		{
			this._maxTargetPointCount = maxTargetPointCount;
			this._input = input;
			this._axis = axis;
			this._measures = measures;
			this._minPointsResolution = minPointsResolution;
			this._series = series;
			this._dynamicSeriesSelectionCriteria = dynamicSeriesSelectionCriteria;
			this._dynamicSeriesSelectionCriteriaOrder = dynamicSeriesSelectionCriteriaOrder;
			this._maxPointsResolution = maxPointsResolution;
			this._maxDynamicSeriesCount = maxDynamicSeriesCount;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0003CF76 File Offset: 0x0003B176
		public QueryExpression MaxTargetPointCount
		{
			get
			{
				return this._maxTargetPointCount;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0003CF7E File Offset: 0x0003B17E
		public QueryExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x0003CF86 File Offset: 0x0003B186
		public QueryExpression Axis
		{
			get
			{
				return this._axis;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0003CF8E File Offset: 0x0003B18E
		public IReadOnlyList<QueryExpression> Measures
		{
			get
			{
				return this._measures;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0003CF96 File Offset: 0x0003B196
		public QueryExpression MinPointsResolution
		{
			get
			{
				return this._minPointsResolution;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0003CF9E File Offset: 0x0003B19E
		public IReadOnlyList<QueryExpression> Series
		{
			get
			{
				return this._series;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0003CFA6 File Offset: 0x0003B1A6
		public DynamicSeriesSelectionCriteria DynamicSeriesSelectionCriteria
		{
			get
			{
				return this._dynamicSeriesSelectionCriteria;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0003CFAE File Offset: 0x0003B1AE
		public SortDirection DynamicSeriesSelectionCriteriaOrder
		{
			get
			{
				return this._dynamicSeriesSelectionCriteriaOrder;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0003CFB6 File Offset: 0x0003B1B6
		public QueryExpression MaxPointsResolution
		{
			get
			{
				return this._maxPointsResolution;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0003CFBE File Offset: 0x0003B1BE
		public QueryExpression MaxDynamicSeriesCount
		{
			get
			{
				return this._maxDynamicSeriesCount;
			}
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x0003CFC6 File Offset: 0x0003B1C6
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0003CFD0 File Offset: 0x0003B1D0
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QuerySampleAxisWithLocalMinMaxExpression querySampleAxisWithLocalMinMaxExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QuerySampleAxisWithLocalMinMaxExpression>(this, other, out flag, out querySampleAxisWithLocalMinMaxExpression))
			{
				return flag;
			}
			return this.MaxTargetPointCount.Equals(querySampleAxisWithLocalMinMaxExpression.MaxTargetPointCount) && this.Input.Equals(querySampleAxisWithLocalMinMaxExpression.Input) && this.Axis.Equals(querySampleAxisWithLocalMinMaxExpression.Axis) && this.Measures.SequenceEqualReadOnly(querySampleAxisWithLocalMinMaxExpression.Measures) && this.MinPointsResolution.Equals(querySampleAxisWithLocalMinMaxExpression.MinPointsResolution) && this.Series.SequenceEqualReadOnly(querySampleAxisWithLocalMinMaxExpression.Series) && this.DynamicSeriesSelectionCriteria == querySampleAxisWithLocalMinMaxExpression.DynamicSeriesSelectionCriteria && this.DynamicSeriesSelectionCriteriaOrder == querySampleAxisWithLocalMinMaxExpression.DynamicSeriesSelectionCriteriaOrder && this.MaxPointsResolution.Equals(querySampleAxisWithLocalMinMaxExpression.MaxPointsResolution) && this.MaxDynamicSeriesCount.Equals(querySampleAxisWithLocalMinMaxExpression.MaxDynamicSeriesCount);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x0003D0A8 File Offset: 0x0003B2A8
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<QueryExpression>(this.MaxTargetPointCount, null), Hashing.GetHashCode<QueryExpressionBinding>(this.Input, null), Hashing.GetHashCode<QueryExpression>(this.Axis, null), Hashing.GetHashCode<IReadOnlyList<QueryExpression>>(this.Measures, null), Hashing.GetHashCode<QueryExpression>(this.MinPointsResolution, null), Hashing.GetHashCode<IReadOnlyList<QueryExpression>>(this.Series, null), Hashing.GetHashCode<DynamicSeriesSelectionCriteria>(this.DynamicSeriesSelectionCriteria, null), Hashing.GetHashCode<SortDirection>(this.DynamicSeriesSelectionCriteriaOrder, null), Hashing.GetHashCode<QueryExpression>(this.MaxPointsResolution, null), Hashing.GetHashCode<QueryExpression>(this.MaxDynamicSeriesCount, null));
		}

		// Token: 0x04000BB2 RID: 2994
		private readonly QueryExpression _maxTargetPointCount;

		// Token: 0x04000BB3 RID: 2995
		private readonly QueryExpressionBinding _input;

		// Token: 0x04000BB4 RID: 2996
		private readonly QueryExpression _axis;

		// Token: 0x04000BB5 RID: 2997
		private readonly IReadOnlyList<QueryExpression> _measures;

		// Token: 0x04000BB6 RID: 2998
		private readonly QueryExpression _minPointsResolution;

		// Token: 0x04000BB7 RID: 2999
		private readonly IReadOnlyList<QueryExpression> _series;

		// Token: 0x04000BB8 RID: 3000
		private readonly DynamicSeriesSelectionCriteria _dynamicSeriesSelectionCriteria;

		// Token: 0x04000BB9 RID: 3001
		private readonly SortDirection _dynamicSeriesSelectionCriteriaOrder;

		// Token: 0x04000BBA RID: 3002
		private readonly QueryExpression _maxPointsResolution;

		// Token: 0x04000BBB RID: 3003
		private readonly QueryExpression _maxDynamicSeriesCount;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200006F RID: 111
	[ImmutableObject(false)]
	internal sealed class ProjectedDsqExpression
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x000120C0 File Offset: 0x000102C0
		internal ProjectedDsqExpression(int? selectIndex, IProjectedDsqExpressionValue value, bool suppressJoinPredicate, bool? isScalar, string nativeReferenceName = null, bool isContextOnly = false)
		{
			this._semanticQuerySelectIndex = selectIndex;
			this._value = value;
			this._suppressJoinPredicate = suppressJoinPredicate;
			this._isScalar = isScalar;
			this.NativeReferenceName = nativeReferenceName;
			this.IsContextOnly = isContextOnly;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x000120F5 File Offset: 0x000102F5
		internal int? SemanticQuerySelectIndex
		{
			get
			{
				return this._semanticQuerySelectIndex;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000120FD File Offset: 0x000102FD
		internal IProjectedDsqExpressionValue Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00012105 File Offset: 0x00010305
		internal bool SuppressJoinPredicate
		{
			get
			{
				return this._suppressJoinPredicate;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001210D File Offset: 0x0001030D
		internal DsqExpressionAggregates Aggregates
		{
			get
			{
				if (this._aggregates == null)
				{
					this._aggregates = new DsqExpressionAggregates();
				}
				return this._aggregates;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00012128 File Offset: 0x00010328
		internal List<int> AdditionalSemanticQuerySelectIndices
		{
			get
			{
				return this._additionalSemanticQuerySelectIndices;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00012130 File Offset: 0x00010330
		internal bool? IsScalar
		{
			get
			{
				return this._isScalar;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00012138 File Offset: 0x00010338
		public string NativeReferenceName { get; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00012140 File Offset: 0x00010340
		public bool IsContextOnly { get; }

		// Token: 0x060004C7 RID: 1223 RVA: 0x00012148 File Offset: 0x00010348
		internal void AddAdditionalSemanticQuerySelectIndex(int index)
		{
			if (this._additionalSemanticQuerySelectIndices == null)
			{
				this._additionalSemanticQuerySelectIndices = new List<int>(1);
			}
			this._additionalSemanticQuerySelectIndices.Add(index);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001216C File Offset: 0x0001036C
		internal ProjectedDsqExpression CloneWithOverrides(IProjectedDsqExpressionValue value = null, bool? isScalar = null)
		{
			int? semanticQuerySelectIndex = this._semanticQuerySelectIndex;
			IProjectedDsqExpressionValue projectedDsqExpressionValue = value ?? this._value;
			bool suppressJoinPredicate = this._suppressJoinPredicate;
			bool? flag = isScalar;
			return new ProjectedDsqExpression(semanticQuerySelectIndex, projectedDsqExpressionValue, suppressJoinPredicate, (flag != null) ? flag : this._isScalar, this.NativeReferenceName, this.IsContextOnly);
		}

		// Token: 0x0400029E RID: 670
		private readonly int? _semanticQuerySelectIndex;

		// Token: 0x0400029F RID: 671
		private readonly IProjectedDsqExpressionValue _value;

		// Token: 0x040002A0 RID: 672
		private readonly bool _suppressJoinPredicate;

		// Token: 0x040002A1 RID: 673
		private readonly bool? _isScalar;

		// Token: 0x040002A2 RID: 674
		private DsqExpressionAggregates _aggregates;

		// Token: 0x040002A3 RID: 675
		private List<int> _additionalSemanticQuerySelectIndices;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Processing.DataShapeResultGeneration;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.Utils;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000AD RID: 173
	internal sealed class ValueBasedCorrelationGovernor : CorrelationGovernor
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000DC46 File Offset: 0x0000BE46
		internal ValueBasedCorrelationGovernor(CellScopeToIntersectionRangeMapping cellScopeRangeMapping, IDataComparer comparer, IKeyGenerator keyGenerator)
			: base(cellScopeRangeMapping)
		{
			this._joinCorrelator = new JoinCorrelator(comparer, keyGenerator);
			this._comparer = comparer;
			this._rowIndices = new Stack<long>();
			this._evaluator = new ExpressionEvaluatorSingleRow(comparer, keyGenerator);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000DC7B File Offset: 0x0000BE7B
		internal override void SetCorrelationInfo(int correlationIndex, IReadOnlyRowCache rowCache)
		{
			this._rowCache = rowCache;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000DC84 File Offset: 0x0000BE84
		protected override void EnterMemberInstanceImpl(DataMember member, long rowIndex)
		{
			if (base.IsInSecondaryHierarchy)
			{
				if (rowIndex == -1L && this._rowIndices.Count > 0)
				{
					rowIndex = this._rowIndices.Peek();
				}
				this._rowIndices.Push(rowIndex);
			}
			base.EnterMemberInstanceImpl(member, rowIndex);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000DCC2 File Offset: 0x0000BEC2
		internal override void ExitMemberInstance(DataMember member)
		{
			if (base.IsInSecondaryHierarchy)
			{
				this._rowIndices.Pop();
			}
			base.ExitMemberInstance(member);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000DCE0 File Offset: 0x0000BEE0
		internal override CorrelationStatus Correlate(IDataRow row, DataBinding dataBinding, int currentColumnIndex)
		{
			if (dataBinding == null || dataBinding.Relationships == null)
			{
				return CorrelationStatus.Unknown;
			}
			IDataRow cachedRow = this.GetCachedRow(currentColumnIndex);
			return this._joinCorrelator.Correlate(cachedRow, row, dataBinding);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000DD10 File Offset: 0x0000BF10
		private IDataRow GetCachedRow(int currentColumnIndex)
		{
			IDataRow dataRow = null;
			long rowIndex = base.IntersectionRangeGovernor.GetRowIndex(currentColumnIndex);
			if (rowIndex != -1L)
			{
				dataRow = this._rowCache[Convert.ToInt32(rowIndex)];
			}
			return dataRow;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000DD44 File Offset: 0x0000BF44
		internal override ColumnMatchCondition CreateCorrelationConditionFromRow(IDataRow row)
		{
			return null;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000DD47 File Offset: 0x0000BF47
		internal override int PushCorrelationMatchConditions(MatchConditionGovernor matchConditions, DataBinding dataBinding, int currentColumnIndex)
		{
			this._evaluator.SetActiveRow(this.GetCachedRow(currentColumnIndex));
			return DataPipelineUtils.PushMatchConditionsForRelationships(dataBinding, this._evaluator, matchConditions);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000DD68 File Offset: 0x0000BF68
		internal override bool IsCorrelationEnabled
		{
			get
			{
				return !base.IsInSecondaryHierarchy;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000DD73 File Offset: 0x0000BF73
		internal override CorrelationGovernor ToReadOnly()
		{
			return this;
		}

		// Token: 0x04000249 RID: 585
		private readonly JoinCorrelator _joinCorrelator;

		// Token: 0x0400024A RID: 586
		private readonly IDataComparer _comparer;

		// Token: 0x0400024B RID: 587
		private readonly Stack<long> _rowIndices;

		// Token: 0x0400024C RID: 588
		private readonly ExpressionEvaluatorSingleRow _evaluator;

		// Token: 0x0400024D RID: 589
		private IReadOnlyRowCache _rowCache;
	}
}

using System;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000A9 RID: 169
	internal abstract class IndexBasedCorrelationGovernor : CorrelationGovernor
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x0000D945 File Offset: 0x0000BB45
		protected IndexBasedCorrelationGovernor(CellScopeToIntersectionRangeMapping cellScopeToIntersectionRangeMapping)
			: base(cellScopeToIntersectionRangeMapping)
		{
			this._correlationFieldIndex = -1;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000D955 File Offset: 0x0000BB55
		protected IndexBasedCorrelationGovernor(IndexBasedCorrelationGovernor existingGovernor)
			: base(existingGovernor)
		{
			this._correlationFieldIndex = existingGovernor._correlationFieldIndex;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000D96C File Offset: 0x0000BB6C
		internal override CorrelationStatus Correlate(IDataRow row, DataBinding dataBinding, int currentColumnIndex)
		{
			if (!this.IsCorrelationEnabled)
			{
				return CorrelationStatus.Match;
			}
			long rowIndex = base.IntersectionRangeGovernor.GetRowIndex(currentColumnIndex);
			if (rowIndex == -1L)
			{
				return CorrelationStatus.Unknown;
			}
			int num = -1;
			if (!this.TryGetCorrelationValue(row, out num))
			{
				return CorrelationStatus.Invalid;
			}
			if (!base.IntersectionRangeGovernor.IsValidColumnIndex(num))
			{
				return CorrelationStatus.Invalid;
			}
			Contract.RetailAssert(rowIndex <= (long)num, "Correlation cell index less than current instance.");
			if (rowIndex == (long)num)
			{
				return CorrelationStatus.Match;
			}
			return CorrelationStatus.ValidNoMatch;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000D9D0 File Offset: 0x0000BBD0
		internal bool TryGetCorrelationValue(IDataRow row, out int result)
		{
			result = -1;
			if (row == null)
			{
				return false;
			}
			object @object = row.GetObject(this._correlationFieldIndex);
			if (@object == null)
			{
				return false;
			}
			long num = (long)@object;
			result = (int)num;
			Contract.RetailAssert(result >= 0, "Correlation index must be >= 0");
			return true;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000DA18 File Offset: 0x0000BC18
		internal override int PushCorrelationMatchConditions(MatchConditionGovernor matchConditions, DataBinding dataBinding, int currentColumnIndex)
		{
			long num = (long)currentColumnIndex;
			matchConditions.PushCondition(new ColumnMatchCondition(this._correlationFieldIndex, num, ColumnMatchOperator.Equal));
			return 1;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000DA44 File Offset: 0x0000BC44
		internal override ColumnMatchCondition CreateCorrelationConditionFromRow(IDataRow row)
		{
			int num;
			if (this.TryGetCorrelationValue(row, out num))
			{
				return new ColumnMatchCondition(this._correlationFieldIndex, (long)num, ColumnMatchOperator.Greater);
			}
			return null;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000DA71 File Offset: 0x0000BC71
		internal override bool IsCorrelationEnabled
		{
			get
			{
				return this._correlationFieldIndex != -1 && !base.IsInSecondaryHierarchy;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000DA87 File Offset: 0x0000BC87
		internal int CorrelationFieldIndex
		{
			get
			{
				return this._correlationFieldIndex;
			}
		}

		// Token: 0x04000241 RID: 577
		protected const int DisabledCorrelation = -1;

		// Token: 0x04000242 RID: 578
		protected int _correlationFieldIndex;
	}
}

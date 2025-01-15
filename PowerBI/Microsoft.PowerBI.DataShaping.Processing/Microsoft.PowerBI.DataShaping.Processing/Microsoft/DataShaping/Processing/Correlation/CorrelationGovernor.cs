using System;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000A4 RID: 164
	internal abstract class CorrelationGovernor
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x0000D607 File Offset: 0x0000B807
		protected CorrelationGovernor(CellScopeToIntersectionRangeMapping cellScopeToIntersectionRangeMapping)
		{
			this._isInSecondaryHierarchy = false;
			this._intersectionRangeGovernor = new DataIntersectionRangeGovernor(cellScopeToIntersectionRangeMapping);
			this._telemetry = new ProcessingDataShapeTelemetry();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000D62D File Offset: 0x0000B82D
		protected CorrelationGovernor(CorrelationGovernor existingGovernor)
		{
			this._isInSecondaryHierarchy = false;
			this._intersectionRangeGovernor = existingGovernor._intersectionRangeGovernor;
			this._telemetry = existingGovernor._telemetry;
		}

		// Token: 0x0600043E RID: 1086
		internal abstract void SetCorrelationInfo(int correlationIndex, IReadOnlyRowCache rowCache);

		// Token: 0x0600043F RID: 1087
		internal abstract CorrelationStatus Correlate(IDataRow row, DataBinding dataBinding, int currentColumnIndex);

		// Token: 0x06000440 RID: 1088
		internal abstract ColumnMatchCondition CreateCorrelationConditionFromRow(IDataRow row);

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000441 RID: 1089
		internal abstract bool IsCorrelationEnabled { get; }

		// Token: 0x06000442 RID: 1090
		internal abstract CorrelationGovernor ToReadOnly();

		// Token: 0x06000443 RID: 1091
		internal abstract int PushCorrelationMatchConditions(MatchConditionGovernor matchConditions, DataBinding dataBinding, int currentColumnIndex);

		// Token: 0x06000444 RID: 1092 RVA: 0x0000D654 File Offset: 0x0000B854
		internal void EnterMemberInstance(DataMember member, long rowIndex)
		{
			this.EnterMemberInstanceImpl(member, rowIndex);
			this.IncrementMemberInstanceCount(member);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000D668 File Offset: 0x0000B868
		protected virtual void IncrementMemberInstanceCount(DataMember member)
		{
			if (!member.IsLeaf)
			{
				return;
			}
			long num;
			if (this.IsInSecondaryHierarchy)
			{
				ProcessingDataShapeTelemetry telemetry = this._telemetry;
				num = telemetry.SecondaryCount;
				telemetry.SecondaryCount = num + 1L;
				return;
			}
			ProcessingDataShapeTelemetry telemetry2 = this._telemetry;
			num = telemetry2.PrimaryCount;
			telemetry2.PrimaryCount = num + 1L;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		protected virtual void EnterMemberInstanceImpl(DataMember member, long rowIndex)
		{
			if (this.IsInSecondaryHierarchy)
			{
				if (member.IsDynamic)
				{
					this.IntersectionRangeGovernor.EnterCellScope();
				}
				if (member.IsLeaf && !this.IntersectionRangeGovernor.IsCellScopeHandled())
				{
					int cellScopeIndex = member.CellScopeIndex;
					this.IntersectionRangeGovernor.AddColumnIndexMapping(cellScopeIndex, rowIndex);
				}
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000D705 File Offset: 0x0000B905
		internal virtual void ExitMemberInstance(DataMember member)
		{
			if (this.IsInSecondaryHierarchy && member.IsDynamic)
			{
				this.IntersectionRangeGovernor.ExitCellScope();
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000D722 File Offset: 0x0000B922
		internal virtual void SkipInstance()
		{
			if (this.IsInSecondaryHierarchy)
			{
				this.IntersectionRangeGovernor.SkipColumnIndex();
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000D737 File Offset: 0x0000B937
		internal void StartInSecondaryHierarchy()
		{
			Contract.RetailAssert(!this._isInSecondaryHierarchy, "Correlation tracking should have been stopped.");
			this._isInSecondaryHierarchy = true;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000D753 File Offset: 0x0000B953
		internal void StopInSecondaryHierarchy()
		{
			Contract.RetailAssert(this._isInSecondaryHierarchy, "Correlation tracking should have been started.");
			this._isInSecondaryHierarchy = false;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000D76C File Offset: 0x0000B96C
		internal ProcessingDataShapeTelemetry DataShapeTelemetry
		{
			get
			{
				return this._telemetry;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000D774 File Offset: 0x0000B974
		internal bool IsInSecondaryHierarchy
		{
			get
			{
				return this._isInSecondaryHierarchy;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000D77C File Offset: 0x0000B97C
		internal DataIntersectionRangeGovernor IntersectionRangeGovernor
		{
			get
			{
				return this._intersectionRangeGovernor;
			}
		}

		// Token: 0x04000233 RID: 563
		private readonly DataIntersectionRangeGovernor _intersectionRangeGovernor;

		// Token: 0x04000234 RID: 564
		private readonly ProcessingDataShapeTelemetry _telemetry;

		// Token: 0x04000235 RID: 565
		private bool _isInSecondaryHierarchy;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Processing.Correlation;
using Microsoft.DataShaping.Processing.DataShapeResultGeneration;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.Utils;
using Microsoft.PowerBI.Analytics.Contracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x0200008F RID: 143
	internal sealed class DataPipeline
	{
		// Token: 0x060003AD RID: 941 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		internal DataPipeline(IRowSourceManager rowSourceManager, IDataComparer dataComparer, IKeyGenerator keyGenerator, int resultSetCount, IDataPipelineRowMetadata rowMetadata)
		{
			this._nestingManager = new NestingManager(new CorrelationGovernorFactory(dataComparer, keyGenerator));
			this._rowSourceManager = rowSourceManager;
			this._dataContexts = new List<DataContext>(resultSetCount);
			for (int i = 0; i < resultSetCount; i++)
			{
				this._dataContexts.Add(new DataContext(dataComparer));
			}
			this._joinCorrelator = new JoinCorrelator(dataComparer, keyGenerator);
			this._rowMetadata = rowMetadata;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000C44C File Offset: 0x0000A64C
		internal MatchConditionGovernor MatchConditions
		{
			get
			{
				return this.ActiveContext.MatchConditions;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000C459 File Offset: 0x0000A659
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000C466 File Offset: 0x0000A666
		internal DataWindowGovernor WindowGovernor
		{
			get
			{
				return this._nestingManager.DataWindowGovernor;
			}
			set
			{
				this._nestingManager.DataWindowGovernor = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000C474 File Offset: 0x0000A674
		internal DataLimitGovernor LimitGovernor
		{
			get
			{
				return this._nestingManager.DataLimitGovernor;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000C481 File Offset: 0x0000A681
		internal CorrelationGovernor CorrelationGovernor
		{
			get
			{
				return this._nestingManager.CorrelationGovernor;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000C48E File Offset: 0x0000A68E
		internal ProcessingDataShapeTelemetry DataShapeTelemetry
		{
			get
			{
				return this._nestingManager.DataShapeTelemetry;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000C49B File Offset: 0x0000A69B
		internal IRowSourceManager RowSourceManager
		{
			get
			{
				return this._rowSourceManager;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000C4A3 File Offset: 0x0000A6A3
		internal bool HasMoreRowsInCurrentContext
		{
			get
			{
				return this.HasMoreRows && (this.HasPendingRow && this.MatchConditions.Evaluate(this.PendingRow) == MatchEvaluationResult.Pass);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000C4CD File Offset: 0x0000A6CD
		internal int ActiveContextIndex
		{
			get
			{
				return this._rowSourceManager.ActiveResultSetIndex;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000C4DA File Offset: 0x0000A6DA
		internal void EnterDataShape(DataShape dataShape)
		{
			this._nestingManager.EnterDataShape(dataShape);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		internal void ExitDataShape()
		{
			this._nestingManager.ExitDataShape();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		internal bool SetupNextRow(ExpressionEvaluator evaluator)
		{
			if (this.ActiveRow != null)
			{
				MatchEvaluationResult matchEvaluationResult = this.MatchConditions.Evaluate(this.ActiveRow);
				switch (matchEvaluationResult)
				{
				case MatchEvaluationResult.Pass:
					evaluator.SetActiveRow(this.ActiveRow, this._rowSourceManager.ActiveResultSetIndex);
					return true;
				case MatchEvaluationResult.NotMatchPreserve:
					return false;
				case MatchEvaluationResult.Discard:
					this.ClearActiveRow();
					break;
				default:
					Contract.RetailFail("Unrecognized MatchEvaluationResult {0}", matchEvaluationResult);
					break;
				}
			}
			IDataRow dataRow = this.ReadAndBufferRow();
			if (dataRow != null)
			{
				evaluator.SetActiveRow(dataRow, this._rowSourceManager.ActiveResultSetIndex);
				return true;
			}
			return false;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000C588 File Offset: 0x0000A788
		internal bool RestoreDataContext(DataContextToken token, ExpressionEvaluator evaluator)
		{
			for (int i = 0; i < token.MatchConditionCount; i++)
			{
				this.MatchConditions.PopCondition();
			}
			if (this.IsValidContextIndex(token.RestorationContextIndex))
			{
				this.ClearActiveRow();
				return this.RestoreContext(token.RestorationContextIndex, evaluator);
			}
			return true;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		internal bool SetupDataContext(DataBinding dataBinding, ExpressionEvaluator evaluator, ExpressionTypeEvaluator typeEvaluator, out DataContextToken token)
		{
			if (dataBinding == null)
			{
				token = new DataContextToken(0, -1);
				return true;
			}
			int activeContextIndex = this.ActiveContextIndex;
			bool flag = this.SetupDataContext(typeEvaluator, dataBinding.TableIndex);
			if (flag)
			{
				this.SkipInvalidRows(dataBinding, evaluator, activeContextIndex);
			}
			int num = DataPipelineUtils.PushMatchConditionsForRelationships(dataBinding, evaluator, this.MatchConditions);
			token = new DataContextToken(num, dataBinding.ShouldRestoreContext ? activeContextIndex : (-1));
			return flag;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000C640 File Offset: 0x0000A840
		private void SkipInvalidRows(DataBinding dataBinding, ExpressionEvaluator evaluator, int previousContextIndex)
		{
			if (dataBinding == null || dataBinding.Relationships == null)
			{
				return;
			}
			IDataRow activeRow = evaluator.ActiveRow;
			IDataRow dataRow = this.ActiveRow;
			if (dataRow == null)
			{
				dataRow = this.ReadAndBufferRow();
			}
			CorrelationStatus correlationStatus = CorrelationStatus.Invalid;
			while (dataRow != null)
			{
				correlationStatus = this._joinCorrelator.Correlate(activeRow, dataRow, dataBinding);
				if (correlationStatus != CorrelationStatus.Invalid)
				{
					break;
				}
				dataRow = this.ReadAndBufferRow();
			}
			if (dataRow == null)
			{
				return;
			}
			if (correlationStatus == CorrelationStatus.ValidNoMatch)
			{
				this.ClearActiveRow();
				this.BufferRow(dataRow);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		internal bool SetupDataContext(ExpressionTypeEvaluator typeEvaluator, int neededTableIndex)
		{
			if (!this._rowSourceManager.TrySetupResultSet(neededTableIndex))
			{
				throw new ProcessingException("DataExtensionMissingResultSet", ProcessingErrorMessages.DataExtensionMissingResultSet(neededTableIndex), null, Microsoft.PowerBI.Query.Contracts.ErrorSource.PowerBI);
			}
			IReadOnlyList<Type> activeResultSetColumnTypes = this._rowSourceManager.GetActiveResultSetColumnTypes();
			typeEvaluator.SetColumnTypes(activeResultSetColumnTypes, this._rowSourceManager.ActiveResultSetIndex);
			return true;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000C6F5 File Offset: 0x0000A8F5
		internal bool IsValidContextIndex(int contextIndex)
		{
			return contextIndex > -1;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000C6FB File Offset: 0x0000A8FB
		internal bool RestoreContext(int tableIndex, ExpressionEvaluator evaluator)
		{
			if (!this._rowSourceManager.TryRestoreResultSet(tableIndex))
			{
				return false;
			}
			if (this.ActiveRow != null)
			{
				evaluator.SetActiveRow(this.ActiveRow, tableIndex);
			}
			else
			{
				evaluator.SetActiveTableIndex(tableIndex);
			}
			return true;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000C72C File Offset: 0x0000A92C
		internal bool SetupContextAndNextRow(ExpressionEvaluator evaluator, ExpressionTypeEvaluator typeEvaluator, int neededTableIndex)
		{
			return this.SetupDataContext(typeEvaluator, neededTableIndex) && this.SetupNextRow(evaluator);
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000C741 File Offset: 0x0000A941
		internal bool HasMoreRows
		{
			get
			{
				if (this.HasPendingRow)
				{
					return true;
				}
				this.PendingRow = this._rowSourceManager.ReadRow();
				return this.HasPendingRow;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000C769 File Offset: 0x0000A969
		internal void ClearActiveRow()
		{
			this.ActiveContext.ClearActiveRow();
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000C776 File Offset: 0x0000A976
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000C783 File Offset: 0x0000A983
		internal IDataRow ActiveRow
		{
			get
			{
				return this.ActiveContext.ActiveRow;
			}
			private set
			{
				this.ActiveContext.ActiveRow = value;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000C794 File Offset: 0x0000A994
		private IDataRow ReadAndBufferRow()
		{
			IDataRow dataRow;
			MatchEvaluationResult matchEvaluationResult;
			for (;;)
			{
				dataRow = this.PendingRow;
				if (dataRow != null)
				{
					this.ClearPendingRow();
				}
				else
				{
					dataRow = this._rowSourceManager.ReadRow();
				}
				if (dataRow == null)
				{
					break;
				}
				if (!this.WindowGovernor.SatisfiesWindowConstraints())
				{
					goto Block_2;
				}
				matchEvaluationResult = this.MatchConditions.Evaluate(dataRow);
				switch (matchEvaluationResult)
				{
				case MatchEvaluationResult.Pass:
					goto IL_008B;
				case MatchEvaluationResult.NotMatchPreserve:
					goto IL_0113;
				case MatchEvaluationResult.Discard:
					if (!true)
					{
						goto Block_10;
					}
					continue;
				}
				goto Block_4;
			}
			this.ClearActiveRow();
			this.BufferRow(dataRow);
			return null;
			Block_2:
			if (this._rowMetadata.CountsForLimiting(dataRow, this.ActiveContextIndex))
			{
				this.WindowGovernor.SetHasExceededCapacity();
			}
			this.BufferRow(dataRow);
			return null;
			Block_4:
			Contract.RetailFail("Unrecognized MatchEvaluationResult {0}", matchEvaluationResult);
			return null;
			IL_008B:
			if (this.LimitGovernor.HasCapacity || !this._rowMetadata.CountsForLimiting(dataRow, this.ActiveContextIndex))
			{
				this.ActiveRow = dataRow;
				return dataRow;
			}
			this.LimitGovernor.SetLimitsExceeded();
			if (this.LimitGovernor.SkipInstancesWhenExceeded)
			{
				CorrelationGovernor correlationGovernor = this.CorrelationGovernor;
				while (dataRow != null)
				{
					if ((matchEvaluationResult = this.MatchConditions.Evaluate(dataRow)) == MatchEvaluationResult.NotMatchPreserve)
					{
						break;
					}
					if (matchEvaluationResult == MatchEvaluationResult.Pass)
					{
						correlationGovernor.SkipInstance();
					}
					dataRow = this._rowSourceManager.ReadRow();
				}
				this.BufferRow(dataRow);
				return null;
			}
			this.ActiveRow = dataRow;
			return dataRow;
			IL_0113:
			this.BufferRow(dataRow);
			return null;
			Block_10:
			Contract.RetailFail("ReadAndBufferRow should not hit this point");
			return null;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		private void BufferRow(IDataRow row)
		{
			this.PendingRow = row;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000C8ED File Offset: 0x0000AAED
		private void ClearPendingRow()
		{
			this.ActiveContext.ClearPendingRow();
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000C8FC File Offset: 0x0000AAFC
		private DataContext ActiveContext
		{
			get
			{
				int activeResultSetIndex = this._rowSourceManager.ActiveResultSetIndex;
				return this._dataContexts[activeResultSetIndex];
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000C921 File Offset: 0x0000AB21
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000C92E File Offset: 0x0000AB2E
		private IDataRow PendingRow
		{
			get
			{
				return this.ActiveContext.PendingRow;
			}
			set
			{
				this.ActiveContext.PendingRow = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000C93C File Offset: 0x0000AB3C
		private bool HasPendingRow
		{
			get
			{
				return this.PendingRow != null;
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000C948 File Offset: 0x0000AB48
		internal bool AreResultSetsComplete(IList<int> tableIndices)
		{
			if (tableIndices != null)
			{
				bool flag = false;
				foreach (int num in tableIndices)
				{
					if (this._rowSourceManager.WasSetup(num))
					{
						if (this._rowSourceManager.TryRestoreResultSet(num) && this.HasMoreRowsInCurrentContext)
						{
							return false;
						}
						flag |= !this._rowSourceManager.HasRows(num);
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		internal void SetCorrelationInfo(int correlationIndex)
		{
			this.CorrelationGovernor.SetCorrelationInfo(correlationIndex, this._rowSourceManager.ActiveRowCache);
		}

		// Token: 0x04000207 RID: 519
		private const int InvalidContextIndex = -1;

		// Token: 0x04000208 RID: 520
		private readonly List<DataContext> _dataContexts;

		// Token: 0x04000209 RID: 521
		private readonly NestingManager _nestingManager;

		// Token: 0x0400020A RID: 522
		private readonly IRowSourceManager _rowSourceManager;

		// Token: 0x0400020B RID: 523
		private readonly JoinCorrelator _joinCorrelator;

		// Token: 0x0400020C RID: 524
		private readonly IDataPipelineRowMetadata _rowMetadata;
	}
}

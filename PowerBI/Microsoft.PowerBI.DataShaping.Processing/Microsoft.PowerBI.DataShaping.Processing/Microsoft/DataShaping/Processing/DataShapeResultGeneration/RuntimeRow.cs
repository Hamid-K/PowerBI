using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.Correlation;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000080 RID: 128
	internal sealed class RuntimeRow
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000A7D4 File Offset: 0x000089D4
		internal static void Process(DataPipeline data, ExpressionEvaluator evaluator, Action<IndexRange, int, bool> processIntersectionRange, IList<DataIntersection> dataIntersections, bool omitEmptyIntersections, ISet<string> visitedIntersectionIds, bool isNextRowReady)
		{
			CorrelationGovernor correlationGovernor = data.CorrelationGovernor;
			DataIntersectionRangeGovernor intersectionRangeGovernor = correlationGovernor.IntersectionRangeGovernor;
			IDataRow dataRow = null;
			int columnCount = intersectionRangeGovernor.ColumnCount;
			bool flag = false;
			for (int i = 0; i < columnCount; i++)
			{
				if (intersectionRangeGovernor.IsValidColumnIndex(i))
				{
					IndexRange intersectionRange = intersectionRangeGovernor.GetIntersectionRange(i);
					DataIntersection dataIntersection = dataIntersections[intersectionRange.Start];
					DataBinding dataBinding = dataIntersection.DataBinding;
					bool flag2 = visitedIntersectionIds.Add(dataIntersection.Id) && !dataIntersection.IsEmpty;
					CorrelationStatus correlationStatus = (isNextRowReady ? correlationGovernor.Correlate(evaluator.ActiveRow, dataBinding, i) : CorrelationStatus.Invalid);
					while (isNextRowReady && correlationStatus == CorrelationStatus.Invalid)
					{
						isNextRowReady = RuntimeRow.SetupNextRow(data, evaluator, dataRow);
						correlationStatus = correlationGovernor.Correlate(evaluator.ActiveRow, dataBinding, i);
					}
					if (!isNextRowReady)
					{
						if (!omitEmptyIntersections || flag2)
						{
							RuntimeRow.WriteEmptyIntersection(evaluator, intersectionRange, processIntersectionRange, i, flag);
						}
						flag = true;
					}
					else if (correlationStatus == CorrelationStatus.ValidNoMatch || correlationStatus == CorrelationStatus.Unknown)
					{
						if (!omitEmptyIntersections || flag2)
						{
							RuntimeRow.WriteEmptyIntersection(evaluator, intersectionRange, processIntersectionRange, i, flag);
						}
						flag = true;
					}
					else if (correlationStatus == CorrelationStatus.Match)
					{
						dataRow = evaluator.ActiveRow;
						RuntimeRow.WriteIntersectionInstanceRange(intersectionRange, processIntersectionRange, i, flag);
						isNextRowReady = RuntimeRow.SetupNextRow(data, evaluator, dataRow);
						flag = false;
					}
				}
			}
			while (isNextRowReady)
			{
				isNextRowReady = RuntimeRow.SetupNextRow(data, evaluator, dataRow);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000A91C File Offset: 0x00008B1C
		private static bool SetupNextRow(DataPipeline data, ExpressionEvaluator evaluator, IDataRow lastProcessedRow)
		{
			data.ClearActiveRow();
			ColumnMatchCondition columnMatchCondition = null;
			if (lastProcessedRow != null)
			{
				columnMatchCondition = data.CorrelationGovernor.CreateCorrelationConditionFromRow(lastProcessedRow);
			}
			if (columnMatchCondition != null)
			{
				data.MatchConditions.PushCondition(columnMatchCondition);
			}
			bool flag = data.SetupNextRow(evaluator);
			if (columnMatchCondition != null)
			{
				data.MatchConditions.PopCondition();
			}
			return flag;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000A968 File Offset: 0x00008B68
		internal static void ProcessEmptyRow(DataPipeline data, ExpressionEvaluator evaluator, Action<IndexRange, int, bool> processIntersectionRange)
		{
			DataIntersectionRangeGovernor intersectionRangeGovernor = data.CorrelationGovernor.IntersectionRangeGovernor;
			for (int i = 0; i < intersectionRangeGovernor.ColumnCount; i++)
			{
				RuntimeRow.WriteEmptyIntersection(evaluator, intersectionRangeGovernor.GetIntersectionRange(i), processIntersectionRange, i, false);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000A9A2 File Offset: 0x00008BA2
		private static void WriteIntersectionInstanceRange(IndexRange intersectionRange, Action<IndexRange, int, bool> processIntersectionRange, int currentColumnIndex, bool hadIntersectionGap)
		{
			if (intersectionRange != null)
			{
				processIntersectionRange(intersectionRange, currentColumnIndex, hadIntersectionGap);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000A9B0 File Offset: 0x00008BB0
		private static void WriteEmptyIntersection(ExpressionEvaluator evaluator, IndexRange intersectionRange, Action<IndexRange, int, bool> processIntersectionRange, int currentColumnIndex, bool hadIntersectionGap)
		{
			IDataRow activeRow = evaluator.ActiveRow;
			evaluator.SetActiveRow(null, evaluator.ActiveTableIndex);
			RuntimeRow.WriteIntersectionInstanceRange(intersectionRange, processIntersectionRange, currentColumnIndex, hadIntersectionGap);
			evaluator.SetActiveRow(activeRow, evaluator.ActiveTableIndex);
		}
	}
}

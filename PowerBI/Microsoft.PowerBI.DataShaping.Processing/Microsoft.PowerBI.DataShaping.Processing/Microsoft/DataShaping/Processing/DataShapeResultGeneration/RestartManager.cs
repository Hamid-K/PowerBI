using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;
using Microsoft.DataShaping.Processing.Utils;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x0200007D RID: 125
	internal sealed class RestartManager
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000A584 File Offset: 0x00008784
		internal RestartManager(IDataComparer comparer, IKeyGenerator keyGenerator, int resultTablesCount)
		{
			this._evaluator = new ExpressionEvaluator(comparer, keyGenerator, resultTablesCount);
			this._lastRowsRead = new IDataRow[resultTablesCount];
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000A5A8 File Offset: 0x000087A8
		internal void WriteRestartTokens(IList<IList<ExpressionNode>> restartDefinitions, RestartTokenCollectionWriter writer)
		{
			for (int i = 0; i < this._lastRowsRead.Length; i++)
			{
				this._evaluator.SetActiveRow(this._lastRowsRead[i], i);
			}
			foreach (IEnumerable<ExpressionNode> enumerable in restartDefinitions)
			{
				writer.BeginCollection();
				foreach (ExpressionNode expressionNode in enumerable)
				{
					object obj = this._evaluator.Evaluate(expressionNode);
					writer.WriteValue(obj);
				}
				writer.EndCollection();
			}
			writer.End();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000A668 File Offset: 0x00008868
		internal void SetLastRowRead(IDataRow lastRowRead, int lastResultSetIndexRead, bool shouldAcceptEmptyRow)
		{
			if (shouldAcceptEmptyRow && lastRowRead == null)
			{
				return;
			}
			this._lastRowsRead[lastResultSetIndexRead] = lastRowRead;
		}

		// Token: 0x040001D4 RID: 468
		private readonly ExpressionEvaluator _evaluator;

		// Token: 0x040001D5 RID: 469
		private readonly IDataRow[] _lastRowsRead;
	}
}

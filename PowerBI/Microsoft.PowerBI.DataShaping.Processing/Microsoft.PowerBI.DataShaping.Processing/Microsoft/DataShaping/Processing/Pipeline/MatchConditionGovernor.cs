using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x0200009E RID: 158
	internal sealed class MatchConditionGovernor
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0000D27A File Offset: 0x0000B47A
		internal MatchConditionGovernor(IDataComparer comparer)
		{
			this._comparer = comparer;
			this._conditions = new Stack<ColumnMatchCondition>();
			this._discardConditions = new Stack<ColumnMatchCondition>();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000D29F File Offset: 0x0000B49F
		internal void PushCondition(int fieldIndex, object value)
		{
			this._conditions.Push(new ColumnMatchCondition(fieldIndex, value, ColumnMatchOperator.Equal));
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		internal void PushDiscardCondition(int fieldIndex, object value, DiscardConditionComparisonOperator op)
		{
			if (op == DiscardConditionComparisonOperator.NotEqual)
			{
				this._discardConditions.Push(new ColumnMatchCondition(fieldIndex, value, ColumnMatchOperator.NotEqual));
				return;
			}
			Contract.RetailFail("Unrecognized DiscardConditionComparisonOperator {0}", op);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000D2DD File Offset: 0x0000B4DD
		internal void PushCondition(ColumnMatchCondition condition)
		{
			this._conditions.Push(condition);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000D2EB File Offset: 0x0000B4EB
		internal void PopCondition()
		{
			this._conditions.Pop();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000D2F9 File Offset: 0x0000B4F9
		internal void PopDiscardCondition()
		{
			this._discardConditions.Pop();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000D308 File Offset: 0x0000B508
		internal MatchEvaluationResult Evaluate(IDataRow row)
		{
			if (this._conditions.IsNullOrEmpty<ColumnMatchCondition>() && this._discardConditions.IsNullOrEmpty<ColumnMatchCondition>())
			{
				return MatchEvaluationResult.Pass;
			}
			using (Stack<ColumnMatchCondition>.Enumerator enumerator = this._conditions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Matches(row, this._comparer))
					{
						return MatchEvaluationResult.NotMatchPreserve;
					}
				}
			}
			using (Stack<ColumnMatchCondition>.Enumerator enumerator = this._discardConditions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Matches(row, this._comparer))
					{
						return MatchEvaluationResult.Discard;
					}
				}
			}
			return MatchEvaluationResult.Pass;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
		internal int ConditionCount
		{
			get
			{
				return this._conditions.Count;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000D3DD File Offset: 0x0000B5DD
		internal int DiscardConditionCount
		{
			get
			{
				return this._discardConditions.Count;
			}
		}

		// Token: 0x04000225 RID: 549
		private readonly IDataComparer _comparer;

		// Token: 0x04000226 RID: 550
		private readonly Stack<ColumnMatchCondition> _conditions;

		// Token: 0x04000227 RID: 551
		private readonly Stack<ColumnMatchCondition> _discardConditions;
	}
}

using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Processing.DataShapeResultGeneration;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.Utils;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000AB RID: 171
	internal sealed class JoinCorrelator
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x0000DB24 File Offset: 0x0000BD24
		internal JoinCorrelator(IDataComparer comparer, IKeyGenerator keyGenerator)
		{
			this._comparer = comparer;
			this._primaryEvaluator = new ExpressionEvaluatorSingleRow(comparer, keyGenerator);
			this._secondaryEvaluator = new ExpressionEvaluatorSingleRow(comparer, keyGenerator);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000DB50 File Offset: 0x0000BD50
		internal CorrelationStatus Correlate(IDataRow primaryRow, IDataRow secondaryRow, DataBinding dataBinding)
		{
			Relationship relationship = dataBinding.Relationships[0];
			if (relationship.JoinConditions != null)
			{
				this._primaryEvaluator.SetActiveRow(primaryRow);
				this._secondaryEvaluator.SetActiveRow(secondaryRow);
				for (int i = 0; i < relationship.JoinConditions.Count; i++)
				{
					CorrelationStatus correlationStatus = this.Compare(relationship.JoinConditions[i]);
					if (correlationStatus != CorrelationStatus.Match)
					{
						return correlationStatus;
					}
				}
			}
			return CorrelationStatus.Match;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000DBBC File Offset: 0x0000BDBC
		private CorrelationStatus Compare(JoinCondition joinCondition)
		{
			object obj = this._primaryEvaluator.Evaluate(joinCondition.PrimaryKey);
			object obj2 = this._secondaryEvaluator.Evaluate(joinCondition.SecondaryKey);
			int num = this._comparer.Compare(obj2, obj);
			return this.DetermineSortDirectionCorrelation(num, joinCondition.SortDirection);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000DC08 File Offset: 0x0000BE08
		private CorrelationStatus DetermineSortDirectionCorrelation(int comparisonResult, Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection sortDirection)
		{
			if (comparisonResult == 0)
			{
				return CorrelationStatus.Match;
			}
			if ((sortDirection == Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection.Ascending && comparisonResult > 0) || (sortDirection == Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection.Descending && comparisonResult < 0))
			{
				return CorrelationStatus.ValidNoMatch;
			}
			return CorrelationStatus.Invalid;
		}

		// Token: 0x04000246 RID: 582
		private readonly ExpressionEvaluatorSingleRow _primaryEvaluator;

		// Token: 0x04000247 RID: 583
		private readonly ExpressionEvaluatorSingleRow _secondaryEvaluator;

		// Token: 0x04000248 RID: 584
		private readonly IDataComparer _comparer;
	}
}

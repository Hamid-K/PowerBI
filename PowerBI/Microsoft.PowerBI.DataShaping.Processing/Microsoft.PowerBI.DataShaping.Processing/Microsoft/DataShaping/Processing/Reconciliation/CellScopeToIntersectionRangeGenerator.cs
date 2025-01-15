using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.Correlation;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Reconciliation
{
	// Token: 0x0200001B RID: 27
	internal sealed class CellScopeToIntersectionRangeGenerator : DataShapeDefinitionVisitor
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003676 File Offset: 0x00001876
		private CellScopeToIntersectionRangeGenerator()
		{
			this._currentLeafIndex = 0;
			this._mapping = new CellScopeToIntersectionRangeMapping();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003690 File Offset: 0x00001890
		internal static void Generate(DataShape dataShape)
		{
			CellScopeToIntersectionRangeGenerator cellScopeToIntersectionRangeGenerator = new CellScopeToIntersectionRangeGenerator();
			cellScopeToIntersectionRangeGenerator.Visit(dataShape);
			dataShape.CellScopeToIntersectionRangeMapping = cellScopeToIntersectionRangeGenerator._mapping;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000036B6 File Offset: 0x000018B6
		internal override void Visit(DataShape dataShape)
		{
			Util.PushToLazyStack<IndexRange>(ref this._cellScopeStack, null);
			DataShapeDefinitionVisitor.Visit<DataMember>(dataShape.SecondaryHierarchy, new Action<DataMember>(this.Visit));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000036DC File Offset: 0x000018DC
		internal override void Visit(DataMember dataMember)
		{
			if (dataMember.IsDynamic)
			{
				Util.PushToLazyStack<IndexRange>(ref this._cellScopeStack, null);
			}
			if (dataMember.IsLeaf && !this._cellScopeStack.IsNullOrEmpty<IndexRange>())
			{
				IndexRange indexRange = this._cellScopeStack.Peek();
				if (indexRange == null)
				{
					indexRange = this._mapping.AddCellScope();
					this._cellScopeStack.Pop();
					this._cellScopeStack.Push(indexRange);
				}
				dataMember.CellScopeIndex = this._mapping.GetActiveCellScopeIndex();
				indexRange.ExtendRange(this._currentLeafIndex);
				this._currentLeafIndex++;
			}
			base.Visit(dataMember);
			if (dataMember.IsDynamic)
			{
				this._cellScopeStack.Pop();
			}
		}

		// Token: 0x0400008A RID: 138
		private readonly CellScopeToIntersectionRangeMapping _mapping;

		// Token: 0x0400008B RID: 139
		private Stack<IndexRange> _cellScopeStack;

		// Token: 0x0400008C RID: 140
		private int _currentLeafIndex;
	}
}

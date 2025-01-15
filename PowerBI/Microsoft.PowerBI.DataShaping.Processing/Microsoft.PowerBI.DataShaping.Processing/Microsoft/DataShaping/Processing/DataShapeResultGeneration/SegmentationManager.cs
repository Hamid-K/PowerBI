using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000081 RID: 129
	internal sealed class SegmentationManager
	{
		// Token: 0x06000341 RID: 833 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		internal SegmentationManager(IDataComparer comparer, IList<int> restartIndicesWithStartPositions)
		{
			this._comparer = comparer;
			this._appendFlagsWritten = new List<bool>();
			this._parentRestartIndexes = new List<int>();
			this._restartIndicesWithStartPositions = restartIndicesWithStartPositions;
			this._hasStartPositions = !restartIndicesWithStartPositions.IsNullOrEmpty<int>();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000AA2C File Offset: 0x00008C2C
		internal bool ShouldOutput(DataMember dataMember, bool isNextRowReady)
		{
			if (!dataMember.HasRestartIndex || !this._hasStartPositions)
			{
				return true;
			}
			int restartIndex = dataMember.RestartIndex;
			if (this.HasAppendFlag(restartIndex))
			{
				this._parentRestartIndexes.Add(restartIndex);
				return true;
			}
			bool flag = true;
			bool flag2 = false;
			if (!dataMember.IsDynamic && this.AreAllParentsMerge())
			{
				if (!isNextRowReady)
				{
					flag2 = true;
					flag = false;
				}
				else if (this._restartIndicesWithStartPositions != null)
				{
					for (int i = 0; i < this._restartIndicesWithStartPositions.Count; i++)
					{
						if (this._restartIndicesWithStartPositions[i] == restartIndex)
						{
							flag2 = false;
							flag = true;
							break;
						}
						if (this._restartIndicesWithStartPositions[i] > restartIndex)
						{
							flag2 = true;
							flag = false;
							break;
						}
					}
				}
			}
			this._parentRestartIndexes.Add(restartIndex);
			if (this._appendFlagsWritten.Count <= restartIndex)
			{
				for (int j = restartIndex - this._appendFlagsWritten.Count; j > 0; j--)
				{
					this._appendFlagsWritten.Add(false);
				}
				this._appendFlagsWritten.Add(flag2);
			}
			return flag;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000AB20 File Offset: 0x00008D20
		internal RestartFlag GetRestartFlag(DataMember dataMember, ExpressionEvaluator evaluator)
		{
			int restartIndex = dataMember.RestartIndex;
			if (!dataMember.HasStartPosition || this.HasAppendFlag(restartIndex) || this._parentRestartIndexes.Count == 0)
			{
				return RestartFlag.Append;
			}
			if (this._parentRestartIndexes.Count > 1)
			{
				int num = this._parentRestartIndexes[this._parentRestartIndexes.Count - 2];
				if (this.HasAppendFlag(num))
				{
					this._appendFlagsWritten[restartIndex] = true;
					return RestartFlag.Append;
				}
			}
			RestartFlag restartFlag = this.ComputeRestartFlag(dataMember, evaluator);
			if (restartFlag == RestartFlag.Append)
			{
				this._appendFlagsWritten[restartIndex] = true;
			}
			return restartFlag;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000ABAB File Offset: 0x00008DAB
		internal void ExitMember(DataMember dataMember)
		{
			if (dataMember.HasRestartIndex && this._parentRestartIndexes.Count > 0)
			{
				this._parentRestartIndexes.RemoveAt(this._parentRestartIndexes.Count - 1);
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000ABDC File Offset: 0x00008DDC
		private RestartFlag ComputeRestartFlag(DataMember dataMember, ExpressionEvaluator evaluator)
		{
			RestartFlag restartFlag = RestartFlag.Merge;
			StartPosition startPosition = dataMember.StartPosition;
			IList<ExpressionNode> expressions = startPosition.Expressions;
			IList<object> values = startPosition.Values;
			for (int i = 0; i < expressions.Count; i++)
			{
				object obj = evaluator.Evaluate(expressions[i]);
				if (!this._comparer.Equals(obj, values[i]))
				{
					return RestartFlag.Append;
				}
			}
			return restartFlag;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000AC38 File Offset: 0x00008E38
		private bool AreAllParentsMerge()
		{
			for (int i = 0; i < this._parentRestartIndexes.Count; i++)
			{
				if (this._appendFlagsWritten[this._parentRestartIndexes[i]])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000AC77 File Offset: 0x00008E77
		private bool HasAppendFlag(int restartIndex)
		{
			return this._appendFlagsWritten.Count > restartIndex && this._appendFlagsWritten[restartIndex];
		}

		// Token: 0x040001D8 RID: 472
		private readonly IDataComparer _comparer;

		// Token: 0x040001D9 RID: 473
		private readonly List<bool> _appendFlagsWritten;

		// Token: 0x040001DA RID: 474
		private readonly List<int> _parentRestartIndexes;

		// Token: 0x040001DB RID: 475
		private bool _hasStartPositions;

		// Token: 0x040001DC RID: 476
		private IList<int> _restartIndicesWithStartPositions;
	}
}

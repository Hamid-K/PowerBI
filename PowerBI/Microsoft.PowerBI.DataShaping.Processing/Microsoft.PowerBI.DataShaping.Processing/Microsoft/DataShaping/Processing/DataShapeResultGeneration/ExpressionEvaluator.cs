using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;
using Microsoft.DataShaping.Processing.Utils;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000076 RID: 118
	internal sealed class ExpressionEvaluator : ExpressionEvaluatorBase
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00009F33 File Offset: 0x00008133
		internal ExpressionEvaluator(IDataComparer comparer, IKeyGenerator keyGenerator, int resultTablesCount)
			: base(comparer, keyGenerator)
		{
			this._activeRows = new IDataRow[resultTablesCount];
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00009F49 File Offset: 0x00008149
		internal void SetActiveTableIndex(int tableIndex)
		{
			this._activeTableIndex = tableIndex;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00009F52 File Offset: 0x00008152
		internal void SetActiveRow(IDataRow row, int tableIndex)
		{
			this._activeTableIndex = tableIndex;
			IDataRow dataRow = this._activeRows[this._activeTableIndex];
			this._activeRows[this._activeTableIndex] = row;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00009F77 File Offset: 0x00008177
		internal bool HasActiveRow
		{
			get
			{
				return this.ActiveRow != null;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00009F82 File Offset: 0x00008182
		internal IDataRow ActiveRow
		{
			get
			{
				if (this._activeRows.Length <= this._activeTableIndex)
				{
					return null;
				}
				return this._activeRows[this._activeTableIndex];
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00009FA4 File Offset: 0x000081A4
		public override object Accept(FieldValueExpressionNode node)
		{
			if (this._activeRows.Length > node.TableIndex)
			{
				IDataRow dataRow = this._activeRows[node.TableIndex];
				if (dataRow != null)
				{
					return dataRow.GetObject(node.FieldIndex);
				}
			}
			return null;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00009FE0 File Offset: 0x000081E0
		internal int ActiveTableIndex
		{
			get
			{
				return this._activeTableIndex;
			}
		}

		// Token: 0x040001C4 RID: 452
		private readonly IDataRow[] _activeRows;

		// Token: 0x040001C5 RID: 453
		private int _activeTableIndex;
	}
}

using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions
{
	// Token: 0x02000056 RID: 86
	internal sealed class FieldValueExpressionNode : ExpressionNode
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00006468 File Offset: 0x00004668
		internal FieldValueExpressionNode(string fieldId, int fieldIndex, string tableId, int tableIndex)
		{
			this._fieldId = fieldId;
			this._fieldIndex = fieldIndex;
			this._tableId = tableId;
			this._tableIndex = tableIndex;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000648D File Offset: 0x0000468D
		internal string FieldId
		{
			get
			{
				return this._fieldId;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006495 File Offset: 0x00004695
		internal int FieldIndex
		{
			get
			{
				return this._fieldIndex;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000649D File Offset: 0x0000469D
		internal string TableId
		{
			get
			{
				return this._tableId;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000064A5 File Offset: 0x000046A5
		internal int TableIndex
		{
			get
			{
				return this._tableIndex;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000064AD File Offset: 0x000046AD
		internal override TResultType Accept<TResultType>(IExpressionNodeVisitor<TResultType> visitor)
		{
			return visitor.Accept(this);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000064B6 File Offset: 0x000046B6
		public override string ToString()
		{
			return StringUtil.FormatInvariant("FieldValue[{0}({1}), {2}({3})]", new object[] { this._tableId, this._tableIndex, this._fieldId, this._fieldIndex });
		}

		// Token: 0x0400014D RID: 333
		private readonly string _fieldId;

		// Token: 0x0400014E RID: 334
		private readonly int _fieldIndex;

		// Token: 0x0400014F RID: 335
		private readonly string _tableId;

		// Token: 0x04000150 RID: 336
		private readonly int _tableIndex;
	}
}

using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000C5 RID: 197
	internal sealed class DataTransformTableColumnReferenceExpressionNode : ExpressionNode
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		internal DataTransformTableColumnReferenceExpressionNode(StructureReferenceExpressionNode table, StructureReferenceExpressionNode column)
		{
			this.Table = table;
			this.Column = column;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0000AADA File Offset: 0x00008CDA
		public StructureReferenceExpressionNode Table { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000AAE2 File Offset: 0x00008CE2
		public StructureReferenceExpressionNode Column { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000AAEA File Offset: 0x00008CEA
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.DataTransformTableColumnReference;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			DataTransformTableColumnReferenceExpressionNode dataTransformTableColumnReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<DataTransformTableColumnReferenceExpressionNode>(this, other, out flag, out dataTransformTableColumnReferenceExpressionNode))
			{
				return flag;
			}
			return this.Table.Equals(dataTransformTableColumnReferenceExpressionNode.Table) && this.Column.Equals(dataTransformTableColumnReferenceExpressionNode.Column);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000AB32 File Offset: 0x00008D32
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.Table.GetHashCode(), this.Column.GetHashCode());
		}
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000CC RID: 204
	internal abstract class ExpressionNode : IEquatable<ExpressionNode>, IExpressionNode
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600053E RID: 1342
		public abstract ExpressionNodeKind Kind { get; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000AF3E File Offset: 0x0000913E
		public static IEqualityComparer<ExpressionNode> Comparer
		{
			get
			{
				return EqualityComparer<ExpressionNode>.Default;
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000AF45 File Offset: 0x00009145
		public sealed override bool Equals(object obj)
		{
			return this.Equals(obj as ExpressionNode);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000AF53 File Offset: 0x00009153
		public override int GetHashCode()
		{
			if (this.m_hashCode == null)
			{
				this.m_hashCode = new int?(this.GetHashCodeImpl());
			}
			return this.m_hashCode.Value;
		}

		// Token: 0x06000542 RID: 1346
		protected abstract int GetHashCodeImpl();

		// Token: 0x06000543 RID: 1347
		public abstract bool Equals(ExpressionNode other);

		// Token: 0x06000544 RID: 1348 RVA: 0x0000AF80 File Offset: 0x00009180
		protected static bool CheckReferenceAndTypeEquality<TExpression>(TExpression @this, ExpressionNode other, out bool areEqual, out TExpression otherTyped) where TExpression : ExpressionNode
		{
			if (@this == other)
			{
				areEqual = true;
				otherTyped = default(TExpression);
				return true;
			}
			if (@this == null || other == null || @this.Kind != other.Kind)
			{
				areEqual = false;
				otherTyped = default(TExpression);
				return true;
			}
			otherTyped = other as TExpression;
			if (otherTyped == null)
			{
				areEqual = false;
				return true;
			}
			areEqual = false;
			return false;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000AFF3 File Offset: 0x000091F3
		public override string ToString()
		{
			return new ExpressionStringBuilder().Write(this);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000B000 File Offset: 0x00009200
		public string ToString(IExpressionStringBuilder exprWriter)
		{
			return exprWriter.Write(this);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000B009 File Offset: 0x00009209
		public static implicit operator Expression(ExpressionNode node)
		{
			return new Expression(node);
		}

		// Token: 0x0400023B RID: 571
		private int? m_hashCode;
	}
}

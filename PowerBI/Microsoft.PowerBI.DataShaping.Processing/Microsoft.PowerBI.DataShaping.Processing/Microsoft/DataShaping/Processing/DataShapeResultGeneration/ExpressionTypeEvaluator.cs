using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000079 RID: 121
	internal sealed class ExpressionTypeEvaluator : IExpressionNodeVisitor<Type>
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000A285 File Offset: 0x00008485
		internal ExpressionTypeEvaluator(int resultTablesCount)
		{
			this._activeTypes = new IReadOnlyList<Type>[resultTablesCount];
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A299 File Offset: 0x00008499
		public Type Accept(FieldValueExpressionNode fieldValue)
		{
			return this._activeTypes[fieldValue.TableIndex][fieldValue.FieldIndex];
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public Type Accept(FunctionCallExpressionNode functionCall)
		{
			FunctionKind kind = functionCall.Kind;
			if (kind <= FunctionKind.MaxValue)
			{
				return typeof(object);
			}
			throw new NotImplementedException("FunctionKind not implemented");
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A2E4 File Offset: 0x000084E4
		public Type Accept(LiteralExpressionNode literal)
		{
			if (literal.Value == ScalarValue.Null)
			{
				return typeof(object);
			}
			return literal.Value.Value.GetType();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000A321 File Offset: 0x00008521
		internal void SetColumnTypes(IReadOnlyList<Type> newTypes, int tableIndex)
		{
			if (this._activeTypes[tableIndex] == null)
			{
				this._activeTypes[tableIndex] = newTypes;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000A336 File Offset: 0x00008536
		internal Type GetExpressionType(ExpressionNode expressionNode)
		{
			return expressionNode.Accept<Type>(this);
		}

		// Token: 0x040001CA RID: 458
		private readonly IReadOnlyList<Type>[] _activeTypes;
	}
}

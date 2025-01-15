using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;
using Microsoft.DataShaping.Processing.Utils;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000077 RID: 119
	internal abstract class ExpressionEvaluatorBase : IExpressionNodeVisitor<object>, IExpressionEvaluator<object>
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00009FE8 File Offset: 0x000081E8
		protected ExpressionEvaluatorBase(IDataComparer comparer, IKeyGenerator keyGenerator)
		{
			this._comparer = comparer;
			this._keyGenerator = keyGenerator;
		}

		// Token: 0x0600030D RID: 781
		public abstract object Accept(FieldValueExpressionNode node);

		// Token: 0x0600030E RID: 782 RVA: 0x00009FFE File Offset: 0x000081FE
		public object Evaluate(ExpressionNode expr)
		{
			return expr.Accept<object>(this);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A008 File Offset: 0x00008208
		private List<object> EvaluateFunctionArguments(IList<ExpressionNode> expressions)
		{
			List<object> list = new List<object>(expressions.Count);
			for (int i = 0; i < expressions.Count; i++)
			{
				list.Add(this.Evaluate(expressions[i]));
			}
			return list;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A048 File Offset: 0x00008248
		public object Accept(FunctionCallExpressionNode node)
		{
			switch (node.Kind)
			{
			case FunctionKind.Array:
			{
				List<object> list = new List<object>(node.Arguments.Count);
				foreach (ExpressionNode expressionNode in node.Arguments)
				{
					list.Add(this.Evaluate(expressionNode));
				}
				this._pendingValue = list;
				break;
			}
			case FunctionKind.Comparable:
			{
				List<object> list2 = new List<object>(node.Arguments.Count);
				foreach (ExpressionNode expressionNode2 in node.Arguments)
				{
					object obj = this.Evaluate(expressionNode2);
					list2.Add(this._keyGenerator.GetKey(obj));
				}
				this._pendingValue = list2;
				break;
			}
			case FunctionKind.MinValue:
			{
				List<object> list3 = this.EvaluateFunctionArguments(node.Arguments);
				Func<object, object, object> func = delegate(object min, object nextValue)
				{
					if (this._comparer.Compare(min, nextValue) > 0)
					{
						return nextValue;
					}
					return min;
				};
				this._pendingValue = ExpressionEvaluatorBase.FindExtreme<object>(list3, func);
				break;
			}
			case FunctionKind.MaxValue:
			{
				List<object> list3 = this.EvaluateFunctionArguments(node.Arguments);
				Func<object, object, object> func2 = delegate(object min, object nextValue)
				{
					if (this._comparer.Compare(min, nextValue) < 0)
					{
						return nextValue;
					}
					return min;
				};
				this._pendingValue = ExpressionEvaluatorBase.FindExtreme<object>(list3, func2);
				break;
			}
			default:
				throw new NotImplementedException("FunctionKind not implemented");
			}
			return this._pendingValue;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A1BC File Offset: 0x000083BC
		internal static T FindExtreme<T>(IList<T> source, Func<T, T, T> getExtreme)
		{
			T t = source[0];
			foreach (T t2 in source)
			{
				t = getExtreme(t, t2);
			}
			return t;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A210 File Offset: 0x00008410
		public object Accept(LiteralExpressionNode node)
		{
			return node.Value.Value;
		}

		// Token: 0x040001C6 RID: 454
		private readonly IDataComparer _comparer;

		// Token: 0x040001C7 RID: 455
		private readonly IKeyGenerator _keyGenerator;

		// Token: 0x040001C8 RID: 456
		private object _pendingValue;
	}
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000639 RID: 1593
	internal class CoordinatorScratchpad
	{
		// Token: 0x06004C98 RID: 19608 RVA: 0x0010EABE File Offset: 0x0010CCBE
		internal CoordinatorScratchpad(Type elementType)
		{
			this._elementType = elementType;
			this._nestedCoordinatorScratchpads = new List<CoordinatorScratchpad>();
			this._expressionWithErrorHandlingMap = new Dictionary<Expression, Expression>();
			this._inlineDelegates = new HashSet<LambdaExpression>();
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06004C99 RID: 19609 RVA: 0x0010EAEE File Offset: 0x0010CCEE
		internal CoordinatorScratchpad Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06004C9A RID: 19610 RVA: 0x0010EAF6 File Offset: 0x0010CCF6
		// (set) Token: 0x06004C9B RID: 19611 RVA: 0x0010EAFE File Offset: 0x0010CCFE
		internal Expression SetKeys { get; set; }

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06004C9C RID: 19612 RVA: 0x0010EB07 File Offset: 0x0010CD07
		// (set) Token: 0x06004C9D RID: 19613 RVA: 0x0010EB0F File Offset: 0x0010CD0F
		internal Expression CheckKeys { get; set; }

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06004C9E RID: 19614 RVA: 0x0010EB18 File Offset: 0x0010CD18
		// (set) Token: 0x06004C9F RID: 19615 RVA: 0x0010EB20 File Offset: 0x0010CD20
		internal Expression HasData { get; set; }

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x0010EB29 File Offset: 0x0010CD29
		// (set) Token: 0x06004CA1 RID: 19617 RVA: 0x0010EB31 File Offset: 0x0010CD31
		internal Expression Element { get; set; }

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06004CA2 RID: 19618 RVA: 0x0010EB3A File Offset: 0x0010CD3A
		// (set) Token: 0x06004CA3 RID: 19619 RVA: 0x0010EB42 File Offset: 0x0010CD42
		internal Expression InitializeCollection { get; set; }

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06004CA4 RID: 19620 RVA: 0x0010EB4B File Offset: 0x0010CD4B
		// (set) Token: 0x06004CA5 RID: 19621 RVA: 0x0010EB53 File Offset: 0x0010CD53
		internal int StateSlotNumber { get; set; }

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06004CA6 RID: 19622 RVA: 0x0010EB5C File Offset: 0x0010CD5C
		// (set) Token: 0x06004CA7 RID: 19623 RVA: 0x0010EB64 File Offset: 0x0010CD64
		internal int Depth { get; set; }

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0010EB6D File Offset: 0x0010CD6D
		internal void AddExpressionWithErrorHandling(Expression expression, Expression expressionWithErrorHandling)
		{
			this._expressionWithErrorHandlingMap[expression] = expressionWithErrorHandling;
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x0010EB7C File Offset: 0x0010CD7C
		internal void AddInlineDelegate(LambdaExpression expression)
		{
			this._inlineDelegates.Add(expression);
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x0010EB8B File Offset: 0x0010CD8B
		internal void AddNestedCoordinator(CoordinatorScratchpad nested)
		{
			nested._parent = this;
			this._nestedCoordinatorScratchpads.Add(nested);
		}

		// Token: 0x06004CAB RID: 19627 RVA: 0x0010EBA0 File Offset: 0x0010CDA0
		internal CoordinatorFactory Compile()
		{
			RecordStateFactory[] array;
			if (this._recordStateScratchpads != null)
			{
				array = new RecordStateFactory[this._recordStateScratchpads.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._recordStateScratchpads[i].Compile();
				}
			}
			else
			{
				array = new RecordStateFactory[0];
			}
			CoordinatorFactory[] array2 = new CoordinatorFactory[this._nestedCoordinatorScratchpads.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = this._nestedCoordinatorScratchpads[j].Compile();
			}
			Expression expression = new CoordinatorScratchpad.ReplacementExpressionVisitor(null, this._inlineDelegates).Visit(this.Element);
			Expression expression2 = new CoordinatorScratchpad.ReplacementExpressionVisitor(this._expressionWithErrorHandlingMap, this._inlineDelegates).Visit(this.Element);
			return (CoordinatorFactory)Activator.CreateInstance(typeof(CoordinatorFactory<>).MakeGenericType(new Type[] { this._elementType }), new object[] { this.Depth, this.StateSlotNumber, this.HasData, this.SetKeys, this.CheckKeys, array2, expression, expression2, this.InitializeCollection, array });
		}

		// Token: 0x06004CAC RID: 19628 RVA: 0x0010ECE8 File Offset: 0x0010CEE8
		internal RecordStateScratchpad CreateRecordStateScratchpad()
		{
			RecordStateScratchpad recordStateScratchpad = new RecordStateScratchpad();
			if (this._recordStateScratchpads == null)
			{
				this._recordStateScratchpads = new List<RecordStateScratchpad>();
			}
			this._recordStateScratchpads.Add(recordStateScratchpad);
			return recordStateScratchpad;
		}

		// Token: 0x04001B24 RID: 6948
		private readonly Type _elementType;

		// Token: 0x04001B25 RID: 6949
		private CoordinatorScratchpad _parent;

		// Token: 0x04001B26 RID: 6950
		private readonly List<CoordinatorScratchpad> _nestedCoordinatorScratchpads;

		// Token: 0x04001B27 RID: 6951
		private readonly Dictionary<Expression, Expression> _expressionWithErrorHandlingMap;

		// Token: 0x04001B28 RID: 6952
		private readonly HashSet<LambdaExpression> _inlineDelegates;

		// Token: 0x04001B30 RID: 6960
		private List<RecordStateScratchpad> _recordStateScratchpads;

		// Token: 0x02000C5D RID: 3165
		private class ReplacementExpressionVisitor : EntityExpressionVisitor
		{
			// Token: 0x06006AAD RID: 27309 RVA: 0x0016C4D1 File Offset: 0x0016A6D1
			internal ReplacementExpressionVisitor(Dictionary<Expression, Expression> replacementDictionary, HashSet<LambdaExpression> inlineDelegates)
			{
				this._replacementDictionary = replacementDictionary;
				this._inlineDelegates = inlineDelegates;
			}

			// Token: 0x06006AAE RID: 27310 RVA: 0x0016C4E8 File Offset: 0x0016A6E8
			internal override Expression Visit(Expression expression)
			{
				if (expression == null)
				{
					return expression;
				}
				Expression expression2;
				Expression expression3;
				if (this._replacementDictionary != null && this._replacementDictionary.TryGetValue(expression, out expression2))
				{
					expression3 = expression2;
				}
				else
				{
					bool flag = false;
					LambdaExpression lambdaExpression = null;
					if (expression.NodeType == ExpressionType.Lambda && this._inlineDelegates != null)
					{
						lambdaExpression = (LambdaExpression)expression;
						flag = this._inlineDelegates.Contains(lambdaExpression);
					}
					if (flag)
					{
						Expression expression4 = this.Visit(lambdaExpression.Body);
						expression3 = Expression.Constant(CodeGenEmitter.Compile(expression4.Type, expression4));
					}
					else
					{
						expression3 = base.Visit(expression);
					}
				}
				return expression3;
			}

			// Token: 0x040030E7 RID: 12519
			private readonly Dictionary<Expression, Expression> _replacementDictionary;

			// Token: 0x040030E8 RID: 12520
			private readonly HashSet<LambdaExpression> _inlineDelegates;
		}
	}
}

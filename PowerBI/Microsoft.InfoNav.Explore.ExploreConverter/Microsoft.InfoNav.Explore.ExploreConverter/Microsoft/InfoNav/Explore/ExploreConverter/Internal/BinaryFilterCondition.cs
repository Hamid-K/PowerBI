using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x0200005F RID: 95
	internal sealed class BinaryFilterCondition<T> : IFilterCondition<T>
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x0000B6AD File Offset: 0x000098AD
		internal BinaryFilterCondition(T leftExpression, bool notValue, FilterOperator filterOperator, PrimitiveValue rightExpression)
		{
			this._leftExpression = leftExpression;
			this._not = notValue;
			this._filterOperator = filterOperator;
			this._rightExpression = rightExpression;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000B6D2 File Offset: 0x000098D2
		public T LeftExpression
		{
			get
			{
				return this._leftExpression;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000B6DA File Offset: 0x000098DA
		public FilterOperator Operator
		{
			get
			{
				return this._filterOperator;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000B6E2 File Offset: 0x000098E2
		public PrimitiveValue RightExpression
		{
			get
			{
				return this._rightExpression;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000B6EA File Offset: 0x000098EA
		public bool Not
		{
			get
			{
				return this._not;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000B6F2 File Offset: 0x000098F2
		public void Accept(IFilterConditionVisitor<T> visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000160 RID: 352
		private readonly T _leftExpression;

		// Token: 0x04000161 RID: 353
		private readonly bool _not;

		// Token: 0x04000162 RID: 354
		private readonly FilterOperator _filterOperator;

		// Token: 0x04000163 RID: 355
		private readonly PrimitiveValue _rightExpression;
	}
}

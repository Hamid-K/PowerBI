using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000EF RID: 239
	internal abstract class FilterBaseBuilder<T, TType, TParent> : BuilderBase<TType, TParent>
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x0000E54F File Offset: 0x0000C74F
		protected FilterBaseBuilder(TParent parent, TType activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000E559 File Offset: 0x0000C759
		public T WithFilterCondition(Expression leftExpression, BinaryFilterOperator @operator, Expression rightExpression, bool? not = false)
		{
			return this.WithFilterCondition(null, leftExpression, @operator, rightExpression, not);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000E568 File Offset: 0x0000C768
		public T WithFilterCondition(Identifier id, Expression leftExpression, BinaryFilterOperator @operator, Expression rightExpression, bool? not = false)
		{
			return this.WithFilterCondition(new BinaryFilterCondition
			{
				Id = id,
				LeftExpression = leftExpression,
				Operator = @operator,
				RightExpression = rightExpression,
				Not = ((not != null) ? Candidate<bool>.Valid(not.Value) : null)
			});
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
		public T WithFilterCondition(Expression expression, bool? not = null)
		{
			return this.WithFilterCondition(null, expression, not);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0000E5CB File Offset: 0x0000C7CB
		public T WithFilterCondition(Identifier id, Expression expression, bool? not = false)
		{
			return this.WithFilterCondition(new UnaryFilterCondition
			{
				Id = id,
				Expression = expression,
				Not = ((not != null) ? Candidate<bool>.Valid(not.Value) : null)
			});
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000E604 File Offset: 0x0000C804
		public CompoundFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithCompoundFilterCondition(CompoundFilterOperator @operator)
		{
			return this.WithCompoundFilterCondition(null, @operator);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000E610 File Offset: 0x0000C810
		public CompoundFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithCompoundFilterCondition(Identifier id, CompoundFilterOperator @operator)
		{
			CompoundFilterCondition compoundFilterCondition = new CompoundFilterCondition
			{
				Id = id,
				Operator = @operator
			};
			this.WithFilterCondition(compoundFilterCondition);
			return new CompoundFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>>(this, compoundFilterCondition);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000E645 File Offset: 0x0000C845
		public T WithContextFilterCondition(DataShape filterContextDataShape)
		{
			return this.WithFilterCondition(new ContextFilterCondition
			{
				DataShape = filterContextDataShape
			});
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000E659 File Offset: 0x0000C859
		public T WithApplyFilterCondition(Expression dataShapeReference, Identifier id = null)
		{
			return this.WithFilterCondition(new ApplyFilterCondition
			{
				Id = id,
				DataShapeReference = dataShapeReference
			});
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000E674 File Offset: 0x0000C874
		public ContextFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithContextFilterCondition()
		{
			ContextFilterCondition contextFilterCondition = new ContextFilterCondition();
			this.WithFilterCondition(contextFilterCondition);
			return new ContextFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>>(this, contextFilterCondition);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000E696 File Offset: 0x0000C896
		public ExistsFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithExistsFilterCondition()
		{
			return this.WithExistsFilterCondition(null);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		public ExistsFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithExistsFilterCondition(Identifier id)
		{
			ExistsFilterCondition existsFilterCondition = new ExistsFilterCondition
			{
				Id = id
			};
			this.WithFilterCondition(existsFilterCondition);
			return new ExistsFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>>(this, existsFilterCondition);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000E6C9 File Offset: 0x0000C8C9
		public AnyValueFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithAnyValueFilterCondition(bool defaultValueOverridesAncestors = false)
		{
			return this.WithAnyValueFilterCondition(null, defaultValueOverridesAncestors);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		public AnyValueFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithAnyValueFilterCondition(Identifier id, bool defaultValueOverridesAncestors = false)
		{
			AnyValueFilterCondition anyValueFilterCondition = new AnyValueFilterCondition
			{
				Id = id,
				DefaultValueOverridesAncestors = defaultValueOverridesAncestors
			};
			this.WithFilterCondition(anyValueFilterCondition);
			return new AnyValueFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>>(this, anyValueFilterCondition);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000E704 File Offset: 0x0000C904
		public DefaultValueFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithDefaultValueFilterCondition()
		{
			return this.WithDefaultValueFilterCondition(null);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000E710 File Offset: 0x0000C910
		public DefaultValueFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithDefaultValueFilterCondition(Identifier id)
		{
			DefaultValueFilterCondition defaultValueFilterCondition = new DefaultValueFilterCondition
			{
				Id = id
			};
			this.WithFilterCondition(defaultValueFilterCondition);
			return new DefaultValueFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>>(this, defaultValueFilterCondition);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000E739 File Offset: 0x0000C939
		public InFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithInFilterCondition(bool identityComparison = false)
		{
			return this.WithInFilterCondition(null, identityComparison);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000E744 File Offset: 0x0000C944
		public InFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>> WithInFilterCondition(Identifier id, bool identityComparison = false)
		{
			InFilterCondition inFilterCondition = new InFilterCondition
			{
				Id = id,
				IdentityComparison = identityComparison
			};
			this.WithFilterCondition(inFilterCondition);
			return new InFilterConditionBuilder<FilterBaseBuilder<T, TType, TParent>>(this, inFilterCondition);
		}

		// Token: 0x060006B0 RID: 1712
		public abstract T WithFilterCondition(FilterCondition condition);
	}
}

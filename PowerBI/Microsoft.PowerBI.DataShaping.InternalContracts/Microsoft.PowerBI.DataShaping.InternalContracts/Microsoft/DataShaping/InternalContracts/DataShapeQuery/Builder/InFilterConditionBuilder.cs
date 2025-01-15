using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000F6 RID: 246
	internal sealed class InFilterConditionBuilder<TParent> : BuilderBase<InFilterCondition, TParent>
	{
		// Token: 0x060006C0 RID: 1728 RVA: 0x0000E918 File Offset: 0x0000CB18
		internal InFilterConditionBuilder(TParent parent, InFilterCondition activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000E924 File Offset: 0x0000CB24
		public InFilterConditionBuilder<TParent> WithExpression(Expression expression)
		{
			InFilterCondition activeObject = base.ActiveObject;
			if (activeObject.Expressions == null)
			{
				activeObject.Expressions = new List<Expression>();
			}
			activeObject.Expressions.Add(expression);
			return this;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000E958 File Offset: 0x0000CB58
		public InFilterConditionValuesBuilder<InFilterConditionBuilder<TParent>> WithValues()
		{
			List<Expression> list = new List<Expression>();
			this.WithValues(list);
			return new InFilterConditionValuesBuilder<InFilterConditionBuilder<TParent>>(this, list);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000E97A File Offset: 0x0000CB7A
		public InFilterConditionBuilder<TParent> WithValues(params Expression[] value)
		{
			return this.WithValues(value.ToList<Expression>());
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000E988 File Offset: 0x0000CB88
		public InFilterConditionBuilder<TParent> WithValues(List<Expression> value)
		{
			InFilterCondition activeObject = base.ActiveObject;
			if (activeObject.Values == null)
			{
				activeObject.Values = new List<List<Expression>>();
			}
			activeObject.Values.Add(value);
			return this;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		public InFilterConditionBuilder<TParent> WithValues(List<List<Expression>> values)
		{
			base.ActiveObject.Values = values;
			return this;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000E9CB File Offset: 0x0000CBCB
		public InFilterConditionBuilder<TParent> WithTable(Expression table)
		{
			base.ActiveObject.Table = table;
			return this;
		}
	}
}

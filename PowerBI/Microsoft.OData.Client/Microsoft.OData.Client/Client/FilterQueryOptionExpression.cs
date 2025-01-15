using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x0200009F RID: 159
	internal class FilterQueryOptionExpression : QueryOptionExpression
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x000132A2 File Offset: 0x000114A2
		internal FilterQueryOptionExpression(Type type)
			: base(type)
		{
			this.individualExpressions = new List<Expression>();
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000132B6 File Offset: 0x000114B6
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)10007;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x000132BD File Offset: 0x000114BD
		internal ReadOnlyCollection<Expression> PredicateConjuncts
		{
			get
			{
				return new ReadOnlyCollection<Expression>(this.individualExpressions);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000132CA File Offset: 0x000114CA
		public void AddPredicateConjuncts(IEnumerable<Expression> predicates)
		{
			this.individualExpressions.AddRange(predicates);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000132D8 File Offset: 0x000114D8
		public Expression GetPredicate()
		{
			Expression expression = null;
			bool flag = true;
			foreach (Expression expression2 in this.individualExpressions)
			{
				if (flag)
				{
					expression = expression2;
					flag = false;
				}
				else
				{
					expression = Expression.And(expression, expression2);
				}
			}
			return expression;
		}

		// Token: 0x04000220 RID: 544
		private readonly List<Expression> individualExpressions;
	}
}

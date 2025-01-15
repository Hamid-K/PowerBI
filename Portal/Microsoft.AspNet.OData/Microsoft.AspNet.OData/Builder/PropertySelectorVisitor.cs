using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000146 RID: 326
	internal class PropertySelectorVisitor : ExpressionVisitor
	{
		// Token: 0x06000C2E RID: 3118 RVA: 0x0002F6FA File Offset: 0x0002D8FA
		internal PropertySelectorVisitor(Expression exp)
		{
			this.Visit(exp);
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0002F715 File Offset: 0x0002D915
		public PropertyInfo Property
		{
			get
			{
				return this._properties.SingleOrDefault<PropertyInfo>();
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0002F722 File Offset: 0x0002D922
		public ICollection<PropertyInfo> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002F72C File Offset: 0x0002D92C
		protected override Expression VisitMember(MemberExpression node)
		{
			if (node == null)
			{
				throw Error.ArgumentNull("node");
			}
			PropertyInfo propertyInfo = node.Member as PropertyInfo;
			if (propertyInfo == null)
			{
				throw Error.InvalidOperation(SRResources.MemberExpressionsMustBeProperties, new object[]
				{
					TypeHelper.GetReflectedType(node.Member).FullName,
					node.Member.Name
				});
			}
			if (node.Expression.NodeType != ExpressionType.Parameter)
			{
				throw Error.InvalidOperation(SRResources.MemberExpressionsMustBeBoundToLambdaParameter, new object[0]);
			}
			this._properties.Add(propertyInfo);
			return node;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002F7BB File Offset: 0x0002D9BB
		public static PropertyInfo GetSelectedProperty(Expression exp)
		{
			return new PropertySelectorVisitor(exp).Property;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002F7C8 File Offset: 0x0002D9C8
		public static ICollection<PropertyInfo> GetSelectedProperties(Expression exp)
		{
			return new PropertySelectorVisitor(exp).Properties;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002F7D8 File Offset: 0x0002D9D8
		public override Expression Visit(Expression exp)
		{
			if (exp == null)
			{
				return exp;
			}
			ExpressionType nodeType = exp.NodeType;
			if (nodeType == ExpressionType.Lambda || nodeType == ExpressionType.MemberAccess || nodeType == ExpressionType.New)
			{
				return base.Visit(exp);
			}
			throw Error.NotSupported(SRResources.UnsupportedExpressionNodeType, new object[0]);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002F818 File Offset: 0x0002DA18
		protected override Expression VisitLambda<T>(Expression<T> lambda)
		{
			if (lambda == null)
			{
				throw Error.ArgumentNull("lambda");
			}
			if (lambda.Parameters.Count != 1)
			{
				throw Error.InvalidOperation(SRResources.LambdaExpressionMustHaveExactlyOneParameter, new object[0]);
			}
			Expression expression = this.Visit(lambda.Body);
			if (expression != lambda.Body)
			{
				return Expression.Lambda(lambda.Type, expression, lambda.Parameters);
			}
			return lambda;
		}

		// Token: 0x040003A5 RID: 933
		private List<PropertyInfo> _properties = new List<PropertyInfo>();
	}
}

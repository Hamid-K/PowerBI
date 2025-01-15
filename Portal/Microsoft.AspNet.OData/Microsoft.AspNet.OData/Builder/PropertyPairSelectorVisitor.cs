using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000111 RID: 273
	internal class PropertyPairSelectorVisitor : ExpressionVisitor
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00027708 File Offset: 0x00025908
		public IDictionary<PropertyInfo, PropertyInfo> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00027710 File Offset: 0x00025910
		public static IDictionary<PropertyInfo, PropertyInfo> GetSelectedProperty(Expression exp)
		{
			PropertyPairSelectorVisitor propertyPairSelectorVisitor = new PropertyPairSelectorVisitor();
			propertyPairSelectorVisitor.Visit(exp);
			return propertyPairSelectorVisitor.Properties;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00027724 File Offset: 0x00025924
		public override Expression Visit(Expression exp)
		{
			if (exp == null)
			{
				return null;
			}
			ExpressionType nodeType = exp.NodeType;
			if (nodeType - ExpressionType.And <= 1)
			{
				BinaryExpression binaryExpression = (BinaryExpression)exp;
				this.Visit(binaryExpression.Left);
				return this.Visit(binaryExpression.Right);
			}
			if (nodeType == ExpressionType.Equal)
			{
				return this.VisitEqual(exp);
			}
			if (nodeType == ExpressionType.Lambda)
			{
				return base.Visit(exp);
			}
			throw Error.NotSupported(SRResources.UnsupportedExpressionNodeTypeWithName, new object[] { exp.NodeType });
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002779C File Offset: 0x0002599C
		protected override Expression VisitLambda<T>(Expression<T> lambda)
		{
			if (lambda == null)
			{
				throw Error.ArgumentNull("lambda");
			}
			if (lambda.Parameters.Count != 2)
			{
				throw Error.InvalidOperation(SRResources.LambdaExpressionMustHaveExactlyTwoParameters, new object[0]);
			}
			Expression expression = this.Visit(lambda.Body);
			if (expression != lambda.Body)
			{
				return Expression.Lambda(lambda.Type, expression, lambda.Parameters);
			}
			return lambda;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00027800 File Offset: 0x00025A00
		private Expression VisitEqual(Expression exp)
		{
			BinaryExpression binaryExpression = (BinaryExpression)exp;
			PropertyInfo propertyInfo = this.VisitMemberProperty(binaryExpression.Left);
			PropertyInfo propertyInfo2 = this.VisitMemberProperty(binaryExpression.Right);
			if (propertyInfo != null && propertyInfo2 != null)
			{
				Type type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
				Type type2 = Nullable.GetUnderlyingType(propertyInfo2.PropertyType) ?? propertyInfo2.PropertyType;
				if (type != type2)
				{
					throw Error.InvalidOperation(SRResources.EqualExpressionsMustHaveSameTypes, new object[]
					{
						TypeHelper.GetReflectedType(propertyInfo).FullName,
						propertyInfo.Name,
						propertyInfo.PropertyType.FullName,
						TypeHelper.GetReflectedType(propertyInfo2).FullName,
						propertyInfo2.Name,
						propertyInfo2.PropertyType.FullName
					});
				}
				this._properties.Add(propertyInfo, propertyInfo2);
			}
			return exp;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000278E4 File Offset: 0x00025AE4
		private PropertyInfo VisitMemberProperty(Expression node)
		{
			ExpressionType nodeType = node.NodeType;
			if (nodeType == ExpressionType.Convert)
			{
				return this.VisitMemberProperty(((UnaryExpression)node).Operand);
			}
			if (nodeType == ExpressionType.MemberAccess)
			{
				return PropertyPairSelectorVisitor.GetPropertyInfo((MemberExpression)node);
			}
			return null;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00027924 File Offset: 0x00025B24
		private static PropertyInfo GetPropertyInfo(MemberExpression memberNode)
		{
			PropertyInfo propertyInfo = memberNode.Member as PropertyInfo;
			if (propertyInfo == null)
			{
				throw Error.InvalidOperation(SRResources.MemberExpressionsMustBeProperties, new object[]
				{
					TypeHelper.GetReflectedType(memberNode.Member).FullName,
					memberNode.Member.Name
				});
			}
			if (memberNode.Expression.NodeType != ExpressionType.Parameter)
			{
				throw Error.InvalidOperation(SRResources.MemberExpressionsMustBeBoundToLambdaParameter, new object[0]);
			}
			return propertyInfo;
		}

		// Token: 0x04000300 RID: 768
		private readonly IDictionary<PropertyInfo, PropertyInfo> _properties = new Dictionary<PropertyInfo, PropertyInfo>();
	}
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.OData.Client
{
	// Token: 0x020000A0 RID: 160
	internal sealed class InputBinder : DataServiceALinqExpressionVisitor
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x0001333C File Offset: 0x0001153C
		private InputBinder(ResourceExpression resource, ParameterExpression setReferenceParam)
		{
			this.input = resource;
			this.inputResource = resource as QueryableResourceExpression;
			this.inputParameter = setReferenceParam;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00013370 File Offset: 0x00011570
		internal static Expression Bind(Expression e, ResourceExpression currentInput, ParameterExpression inputParameter, List<ResourceExpression> referencedInputs)
		{
			InputBinder inputBinder = new InputBinder(currentInput, inputParameter);
			Expression expression = inputBinder.Visit(e);
			referencedInputs.AddRange(inputBinder.referencedInputs);
			return expression;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001339C File Offset: 0x0001159C
		internal override Expression VisitMemberAccess(MemberExpression m)
		{
			if (this.inputResource == null || !this.inputResource.HasTransparentScope)
			{
				return base.VisitMemberAccess(m);
			}
			ParameterExpression parameterExpression = null;
			Stack<PropertyInfo> stack = new Stack<PropertyInfo>();
			MemberExpression memberExpression = m;
			while (memberExpression != null && PlatformHelper.IsProperty(memberExpression.Member) && memberExpression.Expression != null)
			{
				stack.Push((PropertyInfo)memberExpression.Member);
				if (memberExpression.Expression.NodeType == ExpressionType.Parameter)
				{
					parameterExpression = (ParameterExpression)memberExpression.Expression;
				}
				memberExpression = memberExpression.Expression as MemberExpression;
			}
			if (parameterExpression != this.inputParameter || stack.Count == 0)
			{
				return m;
			}
			ResourceExpression resourceExpression = this.input;
			QueryableResourceExpression queryableResourceExpression = this.inputResource;
			bool flag = false;
			while (stack.Count > 0 && queryableResourceExpression != null && queryableResourceExpression.HasTransparentScope)
			{
				PropertyInfo propertyInfo = stack.Peek();
				if (propertyInfo.Name.Equals(queryableResourceExpression.TransparentScope.Accessor, StringComparison.Ordinal))
				{
					resourceExpression = queryableResourceExpression;
					stack.Pop();
					flag = true;
				}
				else
				{
					Expression expression;
					if (!queryableResourceExpression.TransparentScope.SourceAccessors.TryGetValue(propertyInfo.Name, out expression))
					{
						break;
					}
					flag = true;
					stack.Pop();
					InputReferenceExpression inputReferenceExpression = expression as InputReferenceExpression;
					if (inputReferenceExpression == null)
					{
						queryableResourceExpression = expression as QueryableResourceExpression;
						if (queryableResourceExpression == null || !queryableResourceExpression.HasTransparentScope)
						{
							resourceExpression = (ResourceExpression)expression;
						}
					}
					else
					{
						queryableResourceExpression = inputReferenceExpression.Target as QueryableResourceExpression;
						resourceExpression = queryableResourceExpression;
					}
				}
			}
			if (!flag)
			{
				return m;
			}
			Expression expression2 = this.CreateReference(resourceExpression);
			while (stack.Count > 0)
			{
				expression2 = Expression.Property(expression2, stack.Pop());
			}
			return expression2;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001352C File Offset: 0x0001172C
		internal override Expression VisitParameter(ParameterExpression p)
		{
			if ((this.inputResource == null || !this.inputResource.HasTransparentScope) && p == this.inputParameter)
			{
				return this.CreateReference(this.input);
			}
			return base.VisitParameter(p);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00013560 File Offset: 0x00011760
		private Expression CreateReference(ResourceExpression resource)
		{
			this.referencedInputs.Add(resource);
			return resource.CreateReference();
		}

		// Token: 0x04000221 RID: 545
		private readonly HashSet<ResourceExpression> referencedInputs = new HashSet<ResourceExpression>(EqualityComparer<ResourceExpression>.Default);

		// Token: 0x04000222 RID: 546
		private readonly ResourceExpression input;

		// Token: 0x04000223 RID: 547
		private readonly QueryableResourceExpression inputResource;

		// Token: 0x04000224 RID: 548
		private readonly ParameterExpression inputParameter;
	}
}

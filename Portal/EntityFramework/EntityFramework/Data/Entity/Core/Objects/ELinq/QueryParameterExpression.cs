using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000465 RID: 1125
	internal sealed class QueryParameterExpression : Expression
	{
		// Token: 0x0600375C RID: 14172 RVA: 0x000B424E File Offset: 0x000B244E
		internal QueryParameterExpression(DbParameterReferenceExpression parameterReference, Expression funcletizedExpression, IEnumerable<ParameterExpression> compiledQueryParameters)
		{
			this._compiledQueryParameters = compiledQueryParameters ?? Enumerable.Empty<ParameterExpression>();
			this._parameterReference = parameterReference;
			this._type = funcletizedExpression.Type;
			this._funcletizedExpression = funcletizedExpression;
			this._cachedDelegate = null;
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000B4288 File Offset: 0x000B2488
		internal object EvaluateParameter(object[] arguments)
		{
			if (this._cachedDelegate == null)
			{
				if (this._funcletizedExpression.NodeType == ExpressionType.Constant)
				{
					return ((ConstantExpression)this._funcletizedExpression).Value;
				}
				ConstantExpression constantExpression;
				if (QueryParameterExpression.TryEvaluatePath(this._funcletizedExpression, out constantExpression))
				{
					return constantExpression.Value;
				}
			}
			object obj;
			try
			{
				if (this._cachedDelegate == null)
				{
					Type delegateType = TypeSystem.GetDelegateType(this._compiledQueryParameters.Select((ParameterExpression p) => p.Type), this._type);
					this._cachedDelegate = Expression.Lambda(delegateType, this._funcletizedExpression, this._compiledQueryParameters).Compile();
				}
				obj = this._cachedDelegate.DynamicInvoke(arguments);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return obj;
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000B4358 File Offset: 0x000B2558
		internal QueryParameterExpression EscapeParameterForLike(Expression<Func<string, Tuple<string, bool>>> method)
		{
			Expression expression = Expression.Property(Expression.Invoke(method, new Expression[] { this._funcletizedExpression }), "Item1");
			return new QueryParameterExpression(this._parameterReference, expression, this._compiledQueryParameters);
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x000B4397 File Offset: 0x000B2597
		internal DbParameterReferenceExpression ParameterReference
		{
			get
			{
				return this._parameterReference;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000B439F File Offset: 0x000B259F
		public override Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000B43A7 File Offset: 0x000B25A7
		public override ExpressionType NodeType
		{
			get
			{
				return (ExpressionType)(-1);
			}
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000B43AC File Offset: 0x000B25AC
		private static bool TryEvaluatePath(Expression expression, out ConstantExpression constantExpression)
		{
			MemberExpression memberExpression = expression as MemberExpression;
			constantExpression = null;
			if (memberExpression != null)
			{
				Stack<MemberExpression> stack = new Stack<MemberExpression>();
				stack.Push(memberExpression);
				while ((memberExpression = memberExpression.Expression as MemberExpression) != null)
				{
					stack.Push(memberExpression);
				}
				memberExpression = stack.Pop();
				if (memberExpression.Expression is ConstantExpression)
				{
					object obj;
					if (!QueryParameterExpression.TryGetFieldOrPropertyValue(memberExpression, ((ConstantExpression)memberExpression.Expression).Value, out obj))
					{
						return false;
					}
					if (stack.Count > 0)
					{
						using (Stack<MemberExpression>.Enumerator enumerator = stack.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (!QueryParameterExpression.TryGetFieldOrPropertyValue(enumerator.Current, obj, out obj))
								{
									return false;
								}
							}
						}
					}
					constantExpression = Expression.Constant(obj, expression.Type);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000B4484 File Offset: 0x000B2684
		private static bool TryGetFieldOrPropertyValue(MemberExpression me, object instance, out object memberValue)
		{
			bool flag = false;
			memberValue = null;
			bool flag2;
			try
			{
				if (me.Member.MemberType == MemberTypes.Field)
				{
					memberValue = ((FieldInfo)me.Member).GetValue(instance);
					flag = true;
				}
				else if (me.Member.MemberType == MemberTypes.Property)
				{
					memberValue = ((PropertyInfo)me.Member).GetValue(instance, null);
					flag = true;
				}
				flag2 = flag;
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return flag2;
		}

		// Token: 0x04001216 RID: 4630
		private readonly DbParameterReferenceExpression _parameterReference;

		// Token: 0x04001217 RID: 4631
		private readonly Type _type;

		// Token: 0x04001218 RID: 4632
		private readonly Expression _funcletizedExpression;

		// Token: 0x04001219 RID: 4633
		private readonly IEnumerable<ParameterExpression> _compiledQueryParameters;

		// Token: 0x0400121A RID: 4634
		private Delegate _cachedDelegate;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004BA RID: 1210
	public static class MethodUtils
	{
		// Token: 0x06001B1B RID: 6939 RVA: 0x00051885 File Offset: 0x0004FA85
		public static MethodInfo GetInfo(Expression<Action> expression)
		{
			return MethodUtils.GetInfo(expression);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0005188D File Offset: 0x0004FA8D
		public static MethodInfo GetInfo(LambdaExpression expression)
		{
			MethodCallExpression methodCallExpression = expression.Body as MethodCallExpression;
			if (methodCallExpression == null)
			{
				throw new ArgumentException("Invalid Expression. Expression should consist of a method call only.");
			}
			return methodCallExpression.Method;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000518B0 File Offset: 0x0004FAB0
		public static TDelegate ToDelegateWithParams<TDelegate>(this MethodInfo method, bool instance = false)
		{
			IEnumerable<Type> enumerable = (from p in method.GetParameters()
				select p.ParameterType).ToArray<Type>();
			MethodInfo method2 = typeof(TDelegate).GetMethod("Invoke");
			ParameterExpression[] delegateParams = method2.GetParameters().Select((ParameterInfo p, int i) => Expression.Parameter(p.ParameterType, "arg" + i.ToString())).ToArray<ParameterExpression>();
			ParameterExpression delegateLastParamArray = delegateParams.Last<ParameterExpression>();
			IEnumerable<Expression> enumerable2 = enumerable.Select(delegate(Type upt, int i)
			{
				if (instance)
				{
					i++;
				}
				Expression expression = ((i < delegateParams.Length - 1) ? delegateParams[i] : Expression.ArrayIndex(delegateLastParamArray, Expression.Constant(i - delegateParams.Length + 1)));
				if (i == delegateParams.Length - 1 && upt.IsArray)
				{
					Type elementType = upt.GetElementType();
					MethodInfo methodInfo = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new Type[] { elementType });
					MethodInfo methodInfo2 = typeof(Enumerable).GetMethod("ToArray").MakeGenericMethod(new Type[] { elementType });
					ParameterExpression parameterExpression = Expression.Variable(typeof(IEnumerable), "temp" + i.ToString());
					return Expression.Coalesce(Expression.TypeAs(expression, upt), Expression.Block(new ParameterExpression[] { parameterExpression }, new Expression[]
					{
						Expression.Assign(parameterExpression, Expression.TypeAs(expression, typeof(IEnumerable))),
						Expression.Condition(Expression.Equal(parameterExpression, Expression.Constant(null)), Expression.Constant(null, methodInfo2.ReturnType), Expression.Call(methodInfo2, Expression.Call(methodInfo, parameterExpression)))
					}));
				}
				return MethodUtils.ConvertType(expression, upt);
			});
			return Expression.Lambda<TDelegate>(MethodUtils.ConvertType(instance ? Expression.Call(MethodUtils.ConvertType(delegateParams[0], method.DeclaringType), method, enumerable2) : Expression.Call(method, enumerable2), method2.ReturnType), delegateParams).Compile();
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000519AC File Offset: 0x0004FBAC
		public static TDelegate ToDelegate<TDelegate>(this MethodInfo method, bool instance = false)
		{
			Type[] array = (from p in method.GetParameters()
				select p.ParameterType).ToArray<Type>();
			MethodInfo method2 = typeof(TDelegate).GetMethod("Invoke");
			ParameterExpression[] array2 = method2.GetParameters().Select((ParameterInfo p, int i) => Expression.Parameter(p.ParameterType, "arg" + i.ToString())).ToArray<ParameterExpression>();
			Expression expression;
			if (!instance)
			{
				IEnumerable<ParameterExpression> enumerable = array2;
				IEnumerable<Type> enumerable2 = array;
				Func<ParameterExpression, Type, Expression> func;
				if ((func = MethodUtils.<>O.<0>__ConvertType) == null)
				{
					func = (MethodUtils.<>O.<0>__ConvertType = new Func<ParameterExpression, Type, Expression>(MethodUtils.ConvertType));
				}
				expression = Expression.Call(method, enumerable.Zip(enumerable2, func));
			}
			else
			{
				Expression expression2 = MethodUtils.ConvertType(array2.First<ParameterExpression>(), method.DeclaringType);
				IEnumerable<ParameterExpression> enumerable3 = array2.Skip(1);
				IEnumerable<Type> enumerable4 = array;
				Func<ParameterExpression, Type, Expression> func2;
				if ((func2 = MethodUtils.<>O.<0>__ConvertType) == null)
				{
					func2 = (MethodUtils.<>O.<0>__ConvertType = new Func<ParameterExpression, Type, Expression>(MethodUtils.ConvertType));
				}
				expression = Expression.Call(expression2, method, enumerable3.Zip(enumerable4, func2));
			}
			return Expression.Lambda<TDelegate>(MethodUtils.ConvertType(expression, method2.ReturnType), array2).Compile();
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00051AB0 File Offset: 0x0004FCB0
		public static Func<object, object> ToDelegateFieldLoad(this FieldInfo field)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "arg0");
			ParameterExpression[] array = new ParameterExpression[] { parameterExpression };
			return Expression.Lambda<Func<object, object>>(MethodUtils.ConvertType(Expression.Field(MethodUtils.ConvertType(parameterExpression, field.DeclaringType), field), typeof(object)), array).Compile();
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00051B0C File Offset: 0x0004FD0C
		private static Expression ConvertType(Expression expression, Type targetType)
		{
			if (targetType.GetTypeInfo().IsGenericType && targetType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
			{
				Type type = targetType.GetGenericArguments()[0];
				MethodInfo methodInfo = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new Type[] { type });
				return Expression.Condition(Expression.Equal(expression, Expression.Constant(null)), Expression.Constant(null, methodInfo.ReturnType), Expression.Coalesce(Expression.TypeAs(expression, targetType), Expression.Call(methodInfo, Expression.TypeAs(expression, typeof(IEnumerable)))));
			}
			return Expression.Convert(expression, targetType);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00051BB4 File Offset: 0x0004FDB4
		public static string FullName(this MemberInfo member)
		{
			return member.DeclaringType.FullName + "." + member.Name;
		}

		// Token: 0x020004BB RID: 1211
		public static class Resolution
		{
			// Token: 0x06001B22 RID: 6946 RVA: 0x00051BD4 File Offset: 0x0004FDD4
			public static MethodInfo ResolveMethodForType(Type type, string methodName, Type[] parameterTypes, Type returnType)
			{
				Func<ParameterInfo, int, bool> <>9__1;
				List<MethodInfo> list = type.GetTypeInfo().DeclaredMethods.Where(delegate(MethodInfo m)
				{
					if (!m.IsStatic)
					{
						return false;
					}
					if (m.Name != methodName)
					{
						return false;
					}
					ParameterInfo[] parameters = m.GetParameters();
					if (parameters.Length != parameterTypes.Length)
					{
						return false;
					}
					IEnumerable<ParameterInfo> enumerable = parameters;
					Func<ParameterInfo, int, bool> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (ParameterInfo pi, int idx) => pi.ParameterType != parameterTypes[idx]);
					}
					return !enumerable.Where(func).Any<ParameterInfo>() && returnType.GetTypeInfo().IsAssignableFrom(m.ReturnType);
				}).ToList<MethodInfo>();
				if (list.Count <= 0)
				{
					return null;
				}
				return list[0];
			}
		}

		// Token: 0x020004BD RID: 1213
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000D58 RID: 3416
			public static Func<ParameterExpression, Type, Expression> <0>__ConvertType;
		}
	}
}

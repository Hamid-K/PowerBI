using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x02000137 RID: 311
	internal static class ReflectionHelpers
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x00027678 File Offset: 0x00025878
		public static Type[] SafeGetTypes(this Assembly assembly)
		{
			Type[] array;
			try
			{
				array = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				Exception[] loaderExceptions = ex.LoaderExceptions;
				for (int i = 0; i < loaderExceptions.Length; i++)
				{
					InternalLogger.Warn(loaderExceptions[i], "Type load exception.");
				}
				List<Type> list = new List<Type>();
				foreach (Type type in ex.Types)
				{
					if (type != null)
					{
						list.Add(type);
					}
				}
				array = list.ToArray();
			}
			catch (Exception ex2)
			{
				InternalLogger.Warn(ex2, "Type load exception.");
				array = ArrayHelper.Empty<Type>();
			}
			return array;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00027728 File Offset: 0x00025928
		public static bool IsStaticClass(this Type type)
		{
			return type.IsClass() && type.IsAbstract() && type.IsSealed();
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00027744 File Offset: 0x00025944
		public static ReflectionHelpers.LateBoundMethod CreateLateBoundMethod(MethodInfo methodInfo)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), "instance");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), "parameters");
			List<Expression> list = new List<Expression>();
			ParameterInfo[] parameters2 = methodInfo.GetParameters();
			for (int i = 0; i < parameters2.Length; i++)
			{
				Expression expression = Expression.ArrayIndex(parameterExpression2, Expression.Constant(i));
				Type type = parameters2[i].ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				UnaryExpression unaryExpression = Expression.Convert(expression, type);
				list.Add(unaryExpression);
			}
			MethodCallExpression methodCallExpression = Expression.Call(methodInfo.IsStatic ? null : Expression.Convert(parameterExpression, methodInfo.DeclaringType), methodInfo, list);
			if (methodCallExpression.Type == typeof(void))
			{
				Expression<Action<object, object[]>> expression2 = Expression.Lambda<Action<object, object[]>>(methodCallExpression, new ParameterExpression[] { parameterExpression, parameterExpression2 });
				Action<object, object[]> execute = expression2.Compile();
				return delegate(object instance, object[] parameters)
				{
					execute(instance, parameters);
					return null;
				};
			}
			return Expression.Lambda<ReflectionHelpers.LateBoundMethod>(Expression.Convert(methodCallExpression, typeof(object)), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile();
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00027875 File Offset: 0x00025A75
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0002787D File Offset: 0x00025A7D
		public static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00027885 File Offset: 0x00025A85
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0002788D File Offset: 0x00025A8D
		public static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00027895 File Offset: 0x00025A95
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0002789D File Offset: 0x00025A9D
		public static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x000278A5 File Offset: 0x00025AA5
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x000278AD File Offset: 0x00025AAD
		public static TAttr GetCustomAttribute<TAttr>(this Type type) where TAttr : Attribute
		{
			return (TAttr)((object)Attribute.GetCustomAttribute(type, typeof(TAttr)));
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000278C4 File Offset: 0x00025AC4
		public static TAttr GetCustomAttribute<TAttr>(this PropertyInfo info) where TAttr : Attribute
		{
			return (TAttr)((object)Attribute.GetCustomAttribute(info, typeof(TAttr)));
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000278DB File Offset: 0x00025ADB
		public static TAttr GetCustomAttribute<TAttr>(this Assembly assembly) where TAttr : Attribute
		{
			return (TAttr)((object)Attribute.GetCustomAttribute(assembly, typeof(TAttr)));
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x000278F2 File Offset: 0x00025AF2
		public static IEnumerable<TAttr> GetCustomAttributes<TAttr>(this Type type, bool inherit) where TAttr : Attribute
		{
			return (TAttr[])type.GetCustomAttributes(typeof(TAttr), inherit);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0002790A File Offset: 0x00025B0A
		public static Assembly GetAssembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x02000276 RID: 630
		// (Invoke) Token: 0x06001650 RID: 5712
		public delegate object LateBoundMethod(object target, object[] arguments);
	}
}

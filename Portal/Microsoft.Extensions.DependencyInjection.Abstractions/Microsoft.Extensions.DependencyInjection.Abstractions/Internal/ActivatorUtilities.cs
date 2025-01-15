using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Microsoft.Extensions.Internal
{
	// Token: 0x02000010 RID: 16
	internal static class ActivatorUtilities
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000031A4 File Offset: 0x000013A4
		internal static object CreateInstance(IServiceProvider provider, Type instanceType, params object[] parameters)
		{
			int num = -1;
			ActivatorUtilities.ConstructorMatcher constructorMatcher = null;
			foreach (ActivatorUtilities.ConstructorMatcher constructorMatcher2 in from c in instanceType.GetTypeInfo().DeclaredConstructors
				where !c.IsStatic && c.IsPublic
				select c into constructor
				select new ActivatorUtilities.ConstructorMatcher(constructor))
			{
				int num2 = constructorMatcher2.Match(parameters);
				if (num2 != -1 && num < num2)
				{
					num = num2;
					constructorMatcher = constructorMatcher2;
				}
			}
			if (constructorMatcher == null)
			{
				throw new InvalidOperationException(string.Format("A suitable constructor for type '{0}' could not be located. Ensure the type is concrete and services are registered for all parameters of a public constructor.", new object[] { instanceType }));
			}
			return constructorMatcher.CreateInstance(provider);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003278 File Offset: 0x00001478
		internal static ObjectFactory CreateFactory(Type instanceType, Type[] argumentTypes)
		{
			ConstructorInfo constructorInfo;
			int?[] array;
			ActivatorUtilities.FindApplicableConstructor(instanceType, argumentTypes, out constructorInfo, out array);
			ParameterExpression parameterExpression;
			ParameterExpression parameterExpression2;
			return new ObjectFactory(Expression.Lambda<Func<IServiceProvider, object[], object>>(ActivatorUtilities.BuildFactoryExpression(constructorInfo, array, parameterExpression, parameterExpression2), new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile().Invoke);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000032E6 File Offset: 0x000014E6
		public static T CreateInstance<T>(IServiceProvider provider, params object[] parameters)
		{
			return (T)((object)ActivatorUtilities.CreateInstance(provider, typeof(T), parameters));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000032FE File Offset: 0x000014FE
		public static T GetServiceOrCreateInstance<T>(IServiceProvider provider)
		{
			return (T)((object)ActivatorUtilities.GetServiceOrCreateInstance(provider, typeof(T)));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003315 File Offset: 0x00001515
		public static object GetServiceOrCreateInstance(IServiceProvider provider, Type type)
		{
			return provider.GetService(type) ?? ActivatorUtilities.CreateInstance(provider, type, new object[0]);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000332F File Offset: 0x0000152F
		private static MethodInfo GetMethodInfo<T>(Expression<T> expr)
		{
			return ((MethodCallExpression)expr.Body).Method;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003344 File Offset: 0x00001544
		private static object GetService(IServiceProvider sp, Type type, Type requiredBy, bool isDefaultParameterRequired)
		{
			object service = sp.GetService(type);
			if (service == null && !isDefaultParameterRequired)
			{
				throw new InvalidOperationException(string.Format("Unable to resolve service for type '{0}' while attempting to activate '{1}'.", new object[] { type, requiredBy }));
			}
			return service;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003380 File Offset: 0x00001580
		private static Expression BuildFactoryExpression(ConstructorInfo constructor, int?[] parameterMap, Expression serviceProvider, Expression factoryArgumentArray)
		{
			ParameterInfo[] parameters = constructor.GetParameters();
			Expression[] array = new Expression[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				Type parameterType = parameters[i].ParameterType;
				if (parameterMap[i] != null)
				{
					array[i] = Expression.ArrayAccess(factoryArgumentArray, new Expression[] { Expression.Constant(parameterMap[i]) });
				}
				else
				{
					bool hasDefaultValue = parameters[i].HasDefaultValue;
					Expression[] array2 = new Expression[]
					{
						serviceProvider,
						Expression.Constant(parameterType, typeof(Type)),
						Expression.Constant(constructor.DeclaringType, typeof(Type)),
						Expression.Constant(hasDefaultValue)
					};
					array[i] = Expression.Call(ActivatorUtilities.GetServiceInfo, array2);
				}
				if (parameters[i].HasDefaultValue)
				{
					ConstantExpression constantExpression = Expression.Constant(parameters[i].DefaultValue);
					array[i] = Expression.Coalesce(array[i], constantExpression);
				}
				array[i] = Expression.Convert(array[i], parameterType);
			}
			return Expression.New(constructor, array);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003488 File Offset: 0x00001688
		private static void FindApplicableConstructor(Type instanceType, Type[] argumentTypes, out ConstructorInfo matchingConstructor, out int?[] parameterMap)
		{
			matchingConstructor = null;
			parameterMap = null;
			foreach (ConstructorInfo constructorInfo in instanceType.GetTypeInfo().DeclaredConstructors)
			{
				int?[] array;
				if (!constructorInfo.IsStatic && constructorInfo.IsPublic && ActivatorUtilities.TryCreateParameterMap(constructorInfo.GetParameters(), argumentTypes, out array))
				{
					if (matchingConstructor != null)
					{
						throw new InvalidOperationException(string.Format("Multiple constructors accepting all given argument types have been found in type '{0}'. There should only be one applicable constructor.", new object[] { instanceType }));
					}
					matchingConstructor = constructorInfo;
					parameterMap = array;
				}
			}
			if (matchingConstructor == null)
			{
				throw new InvalidOperationException(string.Format("A suitable constructor for type '{0}' could not be located. Ensure the type is concrete and services are registered for all parameters of a public constructor.", new object[] { instanceType }));
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000353C File Offset: 0x0000173C
		private static bool TryCreateParameterMap(ParameterInfo[] constructorParameters, Type[] argumentTypes, out int?[] parameterMap)
		{
			parameterMap = new int?[constructorParameters.Length];
			for (int i = 0; i < argumentTypes.Length; i++)
			{
				bool flag = false;
				TypeInfo typeInfo = argumentTypes[i].GetTypeInfo();
				for (int j = 0; j < constructorParameters.Length; j++)
				{
					if (parameterMap[j] == null && constructorParameters[j].ParameterType.GetTypeInfo().IsAssignableFrom(typeInfo))
					{
						flag = true;
						parameterMap[j] = new int?(i);
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400000B RID: 11
		private static readonly MethodInfo GetServiceInfo = ActivatorUtilities.GetMethodInfo<Func<IServiceProvider, Type, Type, bool, object>>((IServiceProvider sp, Type t, Type r, bool c) => ActivatorUtilities.GetService(sp, t, r, c));

		// Token: 0x02000014 RID: 20
		private class ConstructorMatcher
		{
			// Token: 0x06000089 RID: 137 RVA: 0x000036C0 File Offset: 0x000018C0
			public ConstructorMatcher(ConstructorInfo constructor)
			{
				this._constructor = constructor;
				this._parameters = this._constructor.GetParameters();
				this._parameterValuesSet = new bool[this._parameters.Length];
				this._parameterValues = new object[this._parameters.Length];
			}

			// Token: 0x0600008A RID: 138 RVA: 0x00003714 File Offset: 0x00001914
			public int Match(object[] givenParameters)
			{
				int num = 0;
				int num2 = 0;
				for (int num3 = 0; num3 != givenParameters.Length; num3++)
				{
					TypeInfo typeInfo = ((givenParameters[num3] == null) ? null : givenParameters[num3].GetType().GetTypeInfo());
					bool flag = false;
					int num4 = num;
					while (!flag && num4 != this._parameters.Length)
					{
						if (!this._parameterValuesSet[num4] && this._parameters[num4].ParameterType.GetTypeInfo().IsAssignableFrom(typeInfo))
						{
							flag = true;
							this._parameterValuesSet[num4] = true;
							this._parameterValues[num4] = givenParameters[num3];
							if (num == num4)
							{
								num++;
								if (num4 == num3)
								{
									num2 = num4;
								}
							}
						}
						num4++;
					}
					if (!flag)
					{
						return -1;
					}
				}
				return num2;
			}

			// Token: 0x0600008B RID: 139 RVA: 0x000037C4 File Offset: 0x000019C4
			public object CreateInstance(IServiceProvider provider)
			{
				for (int num = 0; num != this._parameters.Length; num++)
				{
					if (!this._parameterValuesSet[num])
					{
						object service = provider.GetService(this._parameters[num].ParameterType);
						if (service == null)
						{
							if (!this._parameters[num].HasDefaultValue)
							{
								throw new InvalidOperationException(string.Format("Unable to resolve service for type '{0}' while attempting to activate '{1}'.", new object[]
								{
									this._parameters[num].ParameterType,
									this._constructor.DeclaringType
								}));
							}
							this._parameterValues[num] = this._parameters[num].DefaultValue;
						}
						else
						{
							this._parameterValues[num] = service;
						}
					}
				}
				object obj;
				try
				{
					obj = this._constructor.Invoke(this._parameterValues);
				}
				catch (Exception ex)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
					throw;
				}
				return obj;
			}

			// Token: 0x04000010 RID: 16
			private readonly ConstructorInfo _constructor;

			// Token: 0x04000011 RID: 17
			private readonly ParameterInfo[] _parameters;

			// Token: 0x04000012 RID: 18
			private readonly object[] _parameterValues;

			// Token: 0x04000013 RID: 19
			private readonly bool[] _parameterValuesSet;
		}
	}
}

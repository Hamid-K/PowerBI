using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.OData.Client
{
	// Token: 0x0200004F RID: 79
	internal class DynamicProxyMethodGenerator
	{
		// Token: 0x06000262 RID: 610 RVA: 0x000096E6 File Offset: 0x000078E6
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "'this' parameter is required when compiling for the desktop.")]
		internal Expression GetCallWrapper(MethodBase method, params Expression[] arguments)
		{
			if (!this.ThisAssemblyCanCreateHostedDynamicMethodsWithSkipVisibility())
			{
				return DynamicProxyMethodGenerator.WrapOriginalMethodWithExpression(method, arguments);
			}
			return DynamicProxyMethodGenerator.GetDynamicMethodCallWrapper(method, arguments);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000096FF File Offset: 0x000078FF
		protected virtual bool ThisAssemblyCanCreateHostedDynamicMethodsWithSkipVisibility()
		{
			return typeof(DynamicProxyMethodGenerator).Assembly.IsFullyTrusted;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009718 File Offset: 0x00007918
		[SecuritySafeCritical]
		private static Expression GetDynamicMethodCallWrapper(MethodBase method, params Expression[] arguments)
		{
			if (method.DeclaringType == null || method.DeclaringType.Assembly != typeof(DynamicProxyMethodGenerator).Assembly)
			{
				return DynamicProxyMethodGenerator.WrapOriginalMethodWithExpression(method, arguments);
			}
			string text = "_dynamic_" + method.ReflectedType.Name + "_" + method.Name;
			MethodInfo methodInfo = null;
			Dictionary<MethodBase, MethodInfo> dictionary = DynamicProxyMethodGenerator.dynamicProxyMethods;
			lock (dictionary)
			{
				DynamicProxyMethodGenerator.dynamicProxyMethods.TryGetValue(method, out methodInfo);
			}
			if (methodInfo != null)
			{
				return Expression.Call(methodInfo, arguments);
			}
			Type[] array = (from p in method.GetParameters()
				select p.ParameterType).ToArray<Type>();
			MethodInfo methodInfo2 = method as MethodInfo;
			DynamicMethod dynamicMethod = DynamicProxyMethodGenerator.CreateDynamicMethod(text, (methodInfo2 == null) ? method.ReflectedType : methodInfo2.ReturnType, array);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			for (int i = 0; i < array.Length; i++)
			{
				switch (i)
				{
				case 0:
					ilgenerator.Emit(OpCodes.Ldarg_0);
					break;
				case 1:
					ilgenerator.Emit(OpCodes.Ldarg_1);
					break;
				case 2:
					ilgenerator.Emit(OpCodes.Ldarg_2);
					break;
				case 3:
					ilgenerator.Emit(OpCodes.Ldarg_3);
					break;
				default:
					ilgenerator.Emit(OpCodes.Ldarg, i);
					break;
				}
			}
			if (methodInfo2 == null)
			{
				ilgenerator.Emit(OpCodes.Newobj, (ConstructorInfo)method);
			}
			else
			{
				ilgenerator.EmitCall(OpCodes.Call, methodInfo2, null);
			}
			ilgenerator.Emit(OpCodes.Ret);
			Dictionary<MethodBase, MethodInfo> dictionary2 = DynamicProxyMethodGenerator.dynamicProxyMethods;
			lock (dictionary2)
			{
				if (!DynamicProxyMethodGenerator.dynamicProxyMethods.ContainsKey(method))
				{
					DynamicProxyMethodGenerator.dynamicProxyMethods.Add(method, dynamicMethod);
				}
			}
			return Expression.Call(dynamicMethod, arguments);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000992C File Offset: 0x00007B2C
		[SecurityCritical]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private static DynamicMethod CreateDynamicMethod(string name, Type returnType, Type[] parameterTypes)
		{
			return new DynamicMethod(name, returnType, parameterTypes, typeof(DynamicProxyMethodGenerator).Module, true);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009948 File Offset: 0x00007B48
		private static Expression WrapOriginalMethodWithExpression(MethodBase method, Expression[] arguments)
		{
			MethodInfo methodInfo = method as MethodInfo;
			if (methodInfo != null)
			{
				return Expression.Call(methodInfo, arguments);
			}
			return Expression.New((ConstructorInfo)method, arguments);
		}

		// Token: 0x040000D6 RID: 214
		private static Dictionary<MethodBase, MethodInfo> dynamicProxyMethods = new Dictionary<MethodBase, MethodInfo>(EqualityComparer<MethodBase>.Default);
	}
}

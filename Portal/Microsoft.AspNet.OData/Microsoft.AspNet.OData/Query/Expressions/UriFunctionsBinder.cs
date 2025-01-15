using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000ED RID: 237
	internal static class UriFunctionsBinder
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x0001E88C File Offset: 0x0001CA8C
		public static void BindUriFunctionName(string functionName, MethodInfo methodInfo)
		{
			if (string.IsNullOrEmpty(functionName))
			{
				throw Error.ArgumentNull("functionName");
			}
			if (methodInfo == null)
			{
				throw Error.ArgumentNull("methodInfo");
			}
			string methodLiteralSignature = UriFunctionsBinder.GetMethodLiteralSignature(functionName, methodInfo);
			object obj = UriFunctionsBinder.locker;
			lock (obj)
			{
				if (UriFunctionsBinder.methodLiteralSignaturesToMethodInfo.ContainsKey(methodLiteralSignature))
				{
					throw new ODataException(string.Format(CultureInfo.InvariantCulture, SRResources.UriFunctionClrBinderAlreadyBound, new object[] { methodLiteralSignature }));
				}
				UriFunctionsBinder.methodLiteralSignaturesToMethodInfo.Add(methodLiteralSignature, methodInfo);
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001E92C File Offset: 0x0001CB2C
		public static bool UnbindUriFunctionName(string functionName, MethodInfo methodInfo)
		{
			if (string.IsNullOrEmpty(functionName))
			{
				throw Error.ArgumentNull("functionName");
			}
			if (methodInfo == null)
			{
				throw Error.ArgumentNull("methodInfo");
			}
			string methodLiteralSignature = UriFunctionsBinder.GetMethodLiteralSignature(functionName, methodInfo);
			object obj = UriFunctionsBinder.locker;
			bool flag2;
			lock (obj)
			{
				flag2 = UriFunctionsBinder.methodLiteralSignaturesToMethodInfo.Remove(methodLiteralSignature);
			}
			return flag2;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
		public static bool TryGetMethodInfo(string functionName, IEnumerable<Type> methodArgumentsType, out MethodInfo methodInfo)
		{
			if (string.IsNullOrEmpty(functionName))
			{
				throw Error.ArgumentNull("functionName");
			}
			if (methodArgumentsType == null)
			{
				throw Error.ArgumentNull("methodArgumentsType");
			}
			string methodLiteralSignature = UriFunctionsBinder.GetMethodLiteralSignature(functionName, methodArgumentsType);
			object obj = UriFunctionsBinder.locker;
			bool flag2;
			lock (obj)
			{
				flag2 = UriFunctionsBinder.methodLiteralSignaturesToMethodInfo.TryGetValue(methodLiteralSignature, out methodInfo);
			}
			return flag2;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001EA14 File Offset: 0x0001CC14
		private static string GetMethodLiteralSignature(string methodName, MethodInfo methodInfo)
		{
			IEnumerable<Type> enumerable = from parameter in methodInfo.GetParameters()
				select parameter.ParameterType;
			if (!methodInfo.IsStatic)
			{
				enumerable = new Type[] { methodInfo.DeclaringType }.Concat(enumerable);
			}
			return UriFunctionsBinder.GetMethodLiteralSignature(methodName, enumerable);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001EA74 File Offset: 0x0001CC74
		private static string GetMethodLiteralSignature(string methodName, IEnumerable<Type> methodArgumentsType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			stringBuilder.Append(methodName);
			stringBuilder.Append('(');
			foreach (Type type in methodArgumentsType)
			{
				stringBuilder.Append(text);
				text = ", ";
				stringBuilder.Append(type.FullName);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x0400025E RID: 606
		private static Dictionary<string, MethodInfo> methodLiteralSignaturesToMethodInfo = new Dictionary<string, MethodInfo>();

		// Token: 0x0400025F RID: 607
		private static object locker = new object();
	}
}

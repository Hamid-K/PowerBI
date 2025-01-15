using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019D RID: 413
	public static class CustomUriFunctions
	{
		// Token: 0x060010CF RID: 4303 RVA: 0x0002EA20 File Offset: 0x0002CC20
		public static void AddCustomUriFunction(string functionName, FunctionSignatureWithReturnType functionSignature)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(functionName, "customFunctionName");
			ExceptionUtils.CheckArgumentNotNull<FunctionSignatureWithReturnType>(functionSignature, "newCustomFunctionSignature");
			CustomUriFunctions.ValidateFunctionWithReturnType(functionSignature);
			object locker = CustomUriFunctions.Locker;
			lock (locker)
			{
				FunctionSignatureWithReturnType[] array;
				if (BuiltInUriFunctions.TryGetBuiltInFunction(functionName, out array) && Enumerable.Any<FunctionSignatureWithReturnType>(array, (FunctionSignatureWithReturnType builtInFunction) => CustomUriFunctions.AreFunctionsSignatureEqual(functionSignature, builtInFunction)))
				{
					throw new ODataException(Strings.CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature(functionName));
				}
				CustomUriFunctions.AddCustomFunction(functionName, functionSignature);
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0002EABC File Offset: 0x0002CCBC
		public static bool RemoveCustomUriFunction(string functionName, FunctionSignatureWithReturnType functionSignature)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(functionName, "customFunctionName");
			ExceptionUtils.CheckArgumentNotNull<FunctionSignatureWithReturnType>(functionSignature, "customFunctionSignature");
			CustomUriFunctions.ValidateFunctionWithReturnType(functionSignature);
			object locker = CustomUriFunctions.Locker;
			bool flag;
			lock (locker)
			{
				FunctionSignatureWithReturnType[] array;
				if (!CustomUriFunctions.CustomFunctions.TryGetValue(functionName, ref array))
				{
					flag = false;
				}
				else
				{
					FunctionSignatureWithReturnType[] array2 = Enumerable.ToArray<FunctionSignatureWithReturnType>(Enumerable.SkipWhile<FunctionSignatureWithReturnType>(array, (FunctionSignatureWithReturnType funcOverload) => CustomUriFunctions.AreFunctionsSignatureEqual(funcOverload, functionSignature)));
					if (array2.Length == array.Length)
					{
						flag = false;
					}
					else if (array2.Length == 0)
					{
						flag = CustomUriFunctions.CustomFunctions.Remove(functionName);
					}
					else
					{
						CustomUriFunctions.CustomFunctions[functionName] = array2;
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0002EB80 File Offset: 0x0002CD80
		public static bool RemoveCustomUriFunction(string functionName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(functionName, "customFunctionName");
			object locker = CustomUriFunctions.Locker;
			bool flag;
			lock (locker)
			{
				flag = CustomUriFunctions.CustomFunctions.Remove(functionName);
			}
			return flag;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0002EBCC File Offset: 0x0002CDCC
		internal static bool TryGetCustomFunction(string name, out FunctionSignatureWithReturnType[] signatures)
		{
			object locker = CustomUriFunctions.Locker;
			bool flag;
			lock (locker)
			{
				flag = CustomUriFunctions.CustomFunctions.TryGetValue(name, ref signatures);
			}
			return flag;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0002EC0C File Offset: 0x0002CE0C
		private static void AddCustomFunction(string customFunctionName, FunctionSignatureWithReturnType newCustomFunctionSignature)
		{
			FunctionSignatureWithReturnType[] array;
			if (!CustomUriFunctions.CustomFunctions.TryGetValue(customFunctionName, ref array))
			{
				CustomUriFunctions.CustomFunctions.Add(customFunctionName, new FunctionSignatureWithReturnType[] { newCustomFunctionSignature });
				return;
			}
			bool flag = Enumerable.Any<FunctionSignatureWithReturnType>(array, (FunctionSignatureWithReturnType existingFunction) => CustomUriFunctions.AreFunctionsSignatureEqual(existingFunction, newCustomFunctionSignature));
			if (flag)
			{
				throw new ODataException(Strings.CustomUriFunctions_AddCustomUriFunction_CustomFunctionOverloadExists(customFunctionName));
			}
			CustomUriFunctions.CustomFunctions[customFunctionName] = Enumerable.ToArray<FunctionSignatureWithReturnType>(Enumerable.Concat<FunctionSignatureWithReturnType>(array, new FunctionSignatureWithReturnType[] { newCustomFunctionSignature }));
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0002EC98 File Offset: 0x0002CE98
		private static bool AreFunctionsSignatureEqual(FunctionSignatureWithReturnType functionOne, FunctionSignatureWithReturnType functionTwo)
		{
			if (!functionOne.ReturnType.IsEquivalentTo(functionTwo.ReturnType))
			{
				return false;
			}
			if (functionOne.ArgumentTypes.Length != functionTwo.ArgumentTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < functionOne.ArgumentTypes.Length; i++)
			{
				if (!functionOne.ArgumentTypes[i].IsEquivalentTo(functionTwo.ArgumentTypes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0002ECFB File Offset: 0x0002CEFB
		private static void ValidateFunctionWithReturnType(FunctionSignatureWithReturnType functionSignature)
		{
			if (functionSignature == null)
			{
				return;
			}
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(functionSignature.ReturnType, "functionSignatureWithReturnType must contain a return type");
		}

		// Token: 0x040008B0 RID: 2224
		private static readonly Dictionary<string, FunctionSignatureWithReturnType[]> CustomFunctions = new Dictionary<string, FunctionSignatureWithReturnType[]>(StringComparer.Ordinal);

		// Token: 0x040008B1 RID: 2225
		private static readonly object Locker = new object();
	}
}

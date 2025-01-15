using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020002B5 RID: 693
	public static class CustomUriFunctions
	{
		// Token: 0x060017E4 RID: 6116 RVA: 0x00051CF8 File Offset: 0x0004FEF8
		public static void AddCustomUriFunction(string functionName, FunctionSignatureWithReturnType functionSignature)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(functionName, "customFunctionName");
			ExceptionUtils.CheckArgumentNotNull<FunctionSignatureWithReturnType>(functionSignature, "newCustomFunctionSignature");
			CustomUriFunctions.ValidateFunctionWithReturnType(functionSignature);
			lock (CustomUriFunctions.Locker)
			{
				FunctionSignatureWithReturnType[] array;
				if (BuiltInUriFunctions.TryGetBuiltInFunction(functionName, out array))
				{
					if (Enumerable.Any<FunctionSignatureWithReturnType>(array, (FunctionSignatureWithReturnType builtInFunction) => CustomUriFunctions.AreFunctionsSignatureEqual(functionSignature, builtInFunction)))
					{
						throw new ODataException(Strings.CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature(functionName));
					}
				}
				CustomUriFunctions.AddCustomFunction(functionName, functionSignature);
			}
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00051DB4 File Offset: 0x0004FFB4
		public static bool RemoveCustomUriFunction(string functionName, FunctionSignatureWithReturnType functionSignature)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(functionName, "customFunctionName");
			ExceptionUtils.CheckArgumentNotNull<FunctionSignatureWithReturnType>(functionSignature, "customFunctionSignature");
			CustomUriFunctions.ValidateFunctionWithReturnType(functionSignature);
			bool flag;
			lock (CustomUriFunctions.Locker)
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

		// Token: 0x060017E6 RID: 6118 RVA: 0x00051E80 File Offset: 0x00050080
		public static bool RemoveCustomUriFunction(string functionName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(functionName, "customFunctionName");
			bool flag;
			lock (CustomUriFunctions.Locker)
			{
				flag = CustomUriFunctions.CustomFunctions.Remove(functionName);
			}
			return flag;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00051ECC File Offset: 0x000500CC
		internal static bool TryGetCustomFunction(string name, out FunctionSignatureWithReturnType[] signatures)
		{
			bool flag;
			lock (CustomUriFunctions.Locker)
			{
				flag = CustomUriFunctions.CustomFunctions.TryGetValue(name, ref signatures);
			}
			return flag;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00051F24 File Offset: 0x00050124
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

		// Token: 0x060017E9 RID: 6121 RVA: 0x00051FC0 File Offset: 0x000501C0
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

		// Token: 0x060017EA RID: 6122 RVA: 0x00052023 File Offset: 0x00050223
		private static void ValidateFunctionWithReturnType(FunctionSignatureWithReturnType functionSignature)
		{
			if (functionSignature == null)
			{
				return;
			}
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(functionSignature.ReturnType, "functionSignatureWithReturnType must contain a return type");
		}

		// Token: 0x04000A3C RID: 2620
		private static readonly Dictionary<string, FunctionSignatureWithReturnType[]> CustomFunctions = new Dictionary<string, FunctionSignatureWithReturnType[]>(StringComparer.Ordinal);

		// Token: 0x04000A3D RID: 2621
		private static readonly object Locker = new object();
	}
}

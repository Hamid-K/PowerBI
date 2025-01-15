using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000139 RID: 313
	public static class CustomUriFunctions
	{
		// Token: 0x0600106B RID: 4203 RVA: 0x0002C6F0 File Offset: 0x0002A8F0
		public static void AddCustomUriFunction(string functionName, FunctionSignatureWithReturnType functionSignature)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(functionName, "functionName");
			ExceptionUtils.CheckArgumentNotNull<FunctionSignatureWithReturnType>(functionSignature, "functionSignature");
			CustomUriFunctions.ValidateFunctionWithReturnType(functionSignature);
			object locker = CustomUriFunctions.Locker;
			lock (locker)
			{
				FunctionSignatureWithReturnType[] array;
				if (BuiltInUriFunctions.TryGetBuiltInFunction(functionName, out array) && array.Any((FunctionSignatureWithReturnType builtInFunction) => CustomUriFunctions.AreFunctionsSignatureEqual(functionSignature, builtInFunction)))
				{
					throw new ODataException(Strings.CustomUriFunctions_AddCustomUriFunction_BuiltInExistsFullSignature(functionName));
				}
				CustomUriFunctions.AddCustomFunction(functionName, functionSignature);
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0002C794 File Offset: 0x0002A994
		public static bool RemoveCustomUriFunction(string functionName, FunctionSignatureWithReturnType functionSignature)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(functionName, "functionName");
			ExceptionUtils.CheckArgumentNotNull<FunctionSignatureWithReturnType>(functionSignature, "functionSignature");
			CustomUriFunctions.ValidateFunctionWithReturnType(functionSignature);
			object locker = CustomUriFunctions.Locker;
			bool flag2;
			lock (locker)
			{
				FunctionSignatureWithReturnType[] array;
				if (!CustomUriFunctions.CustomFunctions.TryGetValue(functionName, out array))
				{
					flag2 = false;
				}
				else
				{
					FunctionSignatureWithReturnType[] array2 = array.SkipWhile((FunctionSignatureWithReturnType funcOverload) => CustomUriFunctions.AreFunctionsSignatureEqual(funcOverload, functionSignature)).ToArray<FunctionSignatureWithReturnType>();
					if (array2.Length == array.Length)
					{
						flag2 = false;
					}
					else if (array2.Length == 0)
					{
						flag2 = CustomUriFunctions.CustomFunctions.Remove(functionName);
					}
					else
					{
						CustomUriFunctions.CustomFunctions[functionName] = array2;
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0002C864 File Offset: 0x0002AA64
		public static bool RemoveCustomUriFunction(string functionName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(functionName, "functionName");
			object locker = CustomUriFunctions.Locker;
			bool flag2;
			lock (locker)
			{
				flag2 = CustomUriFunctions.CustomFunctions.Remove(functionName);
			}
			return flag2;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0002C8B8 File Offset: 0x0002AAB8
		internal static bool TryGetCustomFunction(string functionCallToken, out IList<KeyValuePair<string, FunctionSignatureWithReturnType>> nameSignatures, bool enableCaseInsensitive = false)
		{
			object locker = CustomUriFunctions.Locker;
			bool flag2;
			lock (locker)
			{
				IList<KeyValuePair<string, FunctionSignatureWithReturnType>> list = new List<KeyValuePair<string, FunctionSignatureWithReturnType>>();
				foreach (KeyValuePair<string, FunctionSignatureWithReturnType[]> keyValuePair in CustomUriFunctions.CustomFunctions)
				{
					if (keyValuePair.Key.Equals(functionCallToken, enableCaseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
					{
						foreach (FunctionSignatureWithReturnType functionSignatureWithReturnType in keyValuePair.Value)
						{
							list.Add(new KeyValuePair<string, FunctionSignatureWithReturnType>(keyValuePair.Key, functionSignatureWithReturnType));
						}
					}
				}
				nameSignatures = ((list.Count != 0) ? list : null);
				flag2 = nameSignatures != null;
			}
			return flag2;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0002C998 File Offset: 0x0002AB98
		private static void AddCustomFunction(string customFunctionName, FunctionSignatureWithReturnType newCustomFunctionSignature)
		{
			FunctionSignatureWithReturnType[] array;
			if (!CustomUriFunctions.CustomFunctions.TryGetValue(customFunctionName, out array))
			{
				CustomUriFunctions.CustomFunctions.Add(customFunctionName, new FunctionSignatureWithReturnType[] { newCustomFunctionSignature });
				return;
			}
			bool flag = array.Any((FunctionSignatureWithReturnType existingFunction) => CustomUriFunctions.AreFunctionsSignatureEqual(existingFunction, newCustomFunctionSignature));
			if (flag)
			{
				throw new ODataException(Strings.CustomUriFunctions_AddCustomUriFunction_CustomFunctionOverloadExists(customFunctionName));
			}
			CustomUriFunctions.CustomFunctions[customFunctionName] = array.Concat(new FunctionSignatureWithReturnType[] { newCustomFunctionSignature }).ToArray<FunctionSignatureWithReturnType>();
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0002CA24 File Offset: 0x0002AC24
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

		// Token: 0x06001071 RID: 4209 RVA: 0x0002CA87 File Offset: 0x0002AC87
		private static void ValidateFunctionWithReturnType(FunctionSignatureWithReturnType functionSignature)
		{
			if (functionSignature == null)
			{
				return;
			}
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(functionSignature.ReturnType, "functionSignatureWithReturnType must contain a return type");
		}

		// Token: 0x040007B3 RID: 1971
		private static readonly Dictionary<string, FunctionSignatureWithReturnType[]> CustomFunctions = new Dictionary<string, FunctionSignatureWithReturnType[]>(StringComparer.Ordinal);

		// Token: 0x040007B4 RID: 1972
		private static readonly object Locker = new object();
	}
}

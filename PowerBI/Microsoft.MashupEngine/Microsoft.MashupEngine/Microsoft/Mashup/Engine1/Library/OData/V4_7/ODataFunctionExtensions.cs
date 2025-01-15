using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7
{
	// Token: 0x0200076E RID: 1902
	internal static class ODataFunctionExtensions
	{
		// Token: 0x06003805 RID: 14341 RVA: 0x000B3874 File Offset: 0x000B1A74
		public static void ValidateArgs<T>(Value[] args)
		{
			bool flag = false;
			for (int i = 0; i < args.Length; i++)
			{
				if (flag && !args[i].IsNull)
				{
					throw ODataFunctionExtensions.GetError<T>();
				}
				if (!flag && args[i].IsNull)
				{
					flag = true;
				}
			}
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000B38B4 File Offset: 0x000B1AB4
		public static Microsoft.OData.Edm.IEdmFunction FindMatchOverload(this IEnumerable<KeyValuePair<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>> overloads, Value[] noneNullArgs, string entityBindingType)
		{
			IEnumerable<KeyValuePair<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>> enumerable = overloads.Where((KeyValuePair<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue> e) => e.Value.ParameterCount == noneNullArgs.Length);
			if (enumerable.Count<KeyValuePair<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>>() == 1)
			{
				return enumerable.Single<KeyValuePair<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue>>().Key;
			}
			foreach (KeyValuePair<Microsoft.OData.Edm.IEdmFunction, FunctionTypeValue> keyValuePair in enumerable)
			{
				string text;
				if (ODataFunctionExtensions.IsSameParameterTypes(noneNullArgs, keyValuePair.Value) && ODataFunctionExtensions.TryGetBindingParameterType(keyValuePair.Key, out text) && entityBindingType == text)
				{
					return keyValuePair.Key;
				}
			}
			throw ODataFunctionExtensions.GetError<Microsoft.OData.Edm.IEdmFunction>();
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000B3970 File Offset: 0x000B1B70
		private static bool TryGetBindingParameterType(Microsoft.OData.Edm.IEdmFunction function, out string bindingType)
		{
			Microsoft.OData.Edm.IEdmOperationParameter edmOperationParameter = function.Parameters.FirstOrDefault<Microsoft.OData.Edm.IEdmOperationParameter>();
			if (edmOperationParameter != null)
			{
				bindingType = edmOperationParameter.Type.FullName();
				return true;
			}
			bindingType = null;
			return false;
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000B39A0 File Offset: 0x000B1BA0
		private static bool IsSameParameterTypes(Value[] noneNullArgs, FunctionTypeValue type)
		{
			for (int i = 0; i < noneNullArgs.Length; i++)
			{
				if (!noneNullArgs[i].Type.TypeKind.Equals(type.ParameterType(i).TypeKind))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000B39EC File Offset: 0x000B1BEC
		private static ValueException GetError<T>()
		{
			return ValueException.NewExpressionError(ODataCommonErrors.CreateDataSourceExceptionMessage((typeof(T) == typeof(Microsoft.OData.Edm.IEdmFunction)) ? Strings.BoundFunctionNotFound : Strings.UnboundFunctionNotFound), null, null);
		}
	}
}

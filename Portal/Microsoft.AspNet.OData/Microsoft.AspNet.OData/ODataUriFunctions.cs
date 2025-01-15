using System;
using System.Reflection;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000033 RID: 51
	public static class ODataUriFunctions
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00006298 File Offset: 0x00004498
		public static void AddCustomUriFunction(string functionName, FunctionSignatureWithReturnType functionSignature, MethodInfo methodInfo)
		{
			try
			{
				CustomUriFunctions.AddCustomUriFunction(functionName, functionSignature);
				UriFunctionsBinder.BindUriFunctionName(functionName, methodInfo);
			}
			catch
			{
				ODataUriFunctions.RemoveCustomUriFunction(functionName, functionSignature, methodInfo);
				throw;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000062D4 File Offset: 0x000044D4
		public static bool RemoveCustomUriFunction(string functionName, FunctionSignatureWithReturnType functionSignature, MethodInfo methodInfo)
		{
			return CustomUriFunctions.RemoveCustomUriFunction(functionName, functionSignature) && UriFunctionsBinder.UnbindUriFunctionName(functionName, methodInfo);
		}
	}
}

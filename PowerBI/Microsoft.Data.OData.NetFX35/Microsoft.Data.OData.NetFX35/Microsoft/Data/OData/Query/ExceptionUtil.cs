using System;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x0200000F RID: 15
	internal static class ExceptionUtil
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002EDF File Offset: 0x000010DF
		internal static ODataException CreateResourceNotFound(string identifier)
		{
			return ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_ResourceNotFound(identifier));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EEC File Offset: 0x000010EC
		internal static ODataException ResourceNotFoundError(string errorMessage)
		{
			return new ODataUnrecognizedPathException(errorMessage);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EF4 File Offset: 0x000010F4
		internal static ODataException CreateSyntaxError()
		{
			return ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SyntaxError);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F00 File Offset: 0x00001100
		internal static ODataException CreateBadRequestError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F08 File Offset: 0x00001108
		internal static void ThrowSyntaxErrorIfNotValid(bool valid)
		{
			if (!valid)
			{
				throw ExceptionUtil.CreateSyntaxError();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F13 File Offset: 0x00001113
		internal static void ThrowIfResourceDoesNotExist(bool resourceExists, string identifier)
		{
			if (!resourceExists)
			{
				throw ExceptionUtil.CreateResourceNotFound(identifier);
			}
		}
	}
}

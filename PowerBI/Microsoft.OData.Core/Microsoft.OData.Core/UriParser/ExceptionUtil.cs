using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013B RID: 315
	internal static class ExceptionUtil
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x0002CBFB File Offset: 0x0002ADFB
		internal static ODataException CreateResourceNotFoundError(string identifier)
		{
			return ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_ResourceNotFound(identifier));
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0002CC08 File Offset: 0x0002AE08
		internal static ODataException ResourceNotFoundError(string errorMessage)
		{
			return new ODataUnrecognizedPathException(errorMessage);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0002CC10 File Offset: 0x0002AE10
		internal static ODataException CreateSyntaxError()
		{
			return ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SyntaxError);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0002CC1C File Offset: 0x0002AE1C
		internal static ODataException CreateBadRequestError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0002CC24 File Offset: 0x0002AE24
		internal static void ThrowIfTypesUnrelated(IEdmType type, IEdmType secondType, string segmentName)
		{
			if (!UriEdmHelpers.IsRelatedTo(type.AsElementType(), secondType.AsElementType()))
			{
				throw new ODataException(Strings.PathParser_TypeMustBeRelatedToSet(type, secondType, segmentName));
			}
		}
	}
}

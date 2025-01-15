using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000FC RID: 252
	internal static class ExceptionUtil
	{
		// Token: 0x06000BEF RID: 3055 RVA: 0x0001F713 File Offset: 0x0001D913
		internal static ODataException CreateResourceNotFoundError(string identifier)
		{
			return ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_ResourceNotFound(identifier));
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0001F720 File Offset: 0x0001D920
		internal static ODataException ResourceNotFoundError(string errorMessage)
		{
			return new ODataUnrecognizedPathException(errorMessage);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001F728 File Offset: 0x0001D928
		internal static ODataException CreateSyntaxError()
		{
			return ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SyntaxError);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001F734 File Offset: 0x0001D934
		internal static ODataException CreateBadRequestError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0001F73C File Offset: 0x0001D93C
		internal static void ThrowIfTypesUnrelated(IEdmType type, IEdmType secondType, string segmentName)
		{
			if (!UriEdmHelpers.IsRelatedTo(type.AsElementType(), secondType.AsElementType()))
			{
				throw new ODataException(Strings.PathParser_TypeMustBeRelatedToSet(type, secondType, segmentName));
			}
		}
	}
}

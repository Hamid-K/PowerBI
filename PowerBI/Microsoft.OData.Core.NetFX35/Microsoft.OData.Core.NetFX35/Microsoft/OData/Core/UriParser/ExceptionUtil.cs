using System;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Parsers;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001DF RID: 479
	internal static class ExceptionUtil
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x0003E52F File Offset: 0x0003C72F
		internal static ODataException CreateResourceNotFoundError(string identifier)
		{
			return ExceptionUtil.ResourceNotFoundError(Strings.RequestUriProcessor_ResourceNotFound(identifier));
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0003E53C File Offset: 0x0003C73C
		internal static ODataException ResourceNotFoundError(string errorMessage)
		{
			return new ODataUnrecognizedPathException(errorMessage);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0003E544 File Offset: 0x0003C744
		internal static ODataException CreateSyntaxError()
		{
			return ExceptionUtil.CreateBadRequestError(Strings.RequestUriProcessor_SyntaxError);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0003E550 File Offset: 0x0003C750
		internal static ODataException CreateBadRequestError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0003E558 File Offset: 0x0003C758
		internal static void ThrowIfTypesUnrelated(IEdmType type, IEdmType secondType, string segmentName)
		{
			if (!UriEdmHelpers.IsRelatedTo(type.AsElementType(), secondType))
			{
				throw new ODataException(Strings.PathParser_TypeMustBeRelatedToSet(type, secondType, segmentName));
			}
		}
	}
}

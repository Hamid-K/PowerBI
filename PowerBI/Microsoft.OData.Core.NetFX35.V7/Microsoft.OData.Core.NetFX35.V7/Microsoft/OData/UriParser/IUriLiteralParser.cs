using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A3 RID: 419
	public interface IUriLiteralParser
	{
		// Token: 0x060010F1 RID: 4337
		object ParseUriStringToType(string text, IEdmTypeReference targetType, out UriLiteralParsingException parsingException);
	}
}

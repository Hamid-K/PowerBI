using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000119 RID: 281
	public interface IUriLiteralParser
	{
		// Token: 0x06000F90 RID: 3984
		object ParseUriStringToType(string text, IEdmTypeReference targetType, out UriLiteralParsingException parsingException);
	}
}

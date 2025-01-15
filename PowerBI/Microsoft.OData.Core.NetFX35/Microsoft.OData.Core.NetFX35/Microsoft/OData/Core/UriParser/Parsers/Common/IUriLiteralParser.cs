using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers.Common
{
	// Token: 0x020002BB RID: 699
	public interface IUriLiteralParser
	{
		// Token: 0x06001828 RID: 6184
		object ParseUriStringToType(string text, IEdmTypeReference targetType, out UriLiteralParsingException parsingException);
	}
}

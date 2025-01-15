using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000159 RID: 345
	internal static class ODataPathFactory
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x000324A4 File Offset: 0x000306A4
		internal static ODataPath BindPath(ICollection<string> segments, ODataUriParserConfiguration configuration)
		{
			ODataPathParser odataPathParser = new ODataPathParser(configuration);
			IList<ODataPathSegment> list = odataPathParser.ParsePath(segments);
			return new ODataPath(list);
		}
	}
}

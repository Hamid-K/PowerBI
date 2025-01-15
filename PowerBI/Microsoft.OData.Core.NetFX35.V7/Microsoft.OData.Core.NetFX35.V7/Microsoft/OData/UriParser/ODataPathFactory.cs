using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000116 RID: 278
	internal static class ODataPathFactory
	{
		// Token: 0x06000CEA RID: 3306 RVA: 0x0002496C File Offset: 0x00022B6C
		internal static ODataPath BindPath(ICollection<string> segments, ODataUriParserConfiguration configuration)
		{
			ODataPathParser odataPathParser = new ODataPathParser(configuration);
			IList<ODataPathSegment> list = odataPathParser.ParsePath(segments);
			return new ODataPath(list);
		}
	}
}

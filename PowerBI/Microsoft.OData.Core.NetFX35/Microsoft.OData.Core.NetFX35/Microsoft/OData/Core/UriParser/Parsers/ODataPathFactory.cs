using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x02000208 RID: 520
	internal static class ODataPathFactory
	{
		// Token: 0x060012C0 RID: 4800 RVA: 0x0004433C File Offset: 0x0004253C
		internal static ODataPath BindPath(ICollection<string> segments, ODataUriParserConfiguration configuration)
		{
			ODataPathParser odataPathParser = new ODataPathParser(configuration);
			IList<ODataPathSegment> list = odataPathParser.ParsePath(segments);
			return new ODataPath(list);
		}
	}
}

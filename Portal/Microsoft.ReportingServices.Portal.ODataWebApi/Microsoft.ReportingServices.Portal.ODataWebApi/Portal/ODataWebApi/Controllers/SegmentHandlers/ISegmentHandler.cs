using System;
using Microsoft.OData.UriParser;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.SegmentHandlers
{
	// Token: 0x0200003E RID: 62
	internal interface ISegmentHandler
	{
		// Token: 0x060002F7 RID: 759
		object Handle(object source, ODataPathSegment segment);
	}
}

using System;
using Microsoft.OData.UriParser;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.SegmentHandlers
{
	// Token: 0x0200003F RID: 63
	internal class NoOpSegmentHandler : ISegmentHandler
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000C825 File Offset: 0x0000AA25
		public object Handle(object source, ODataPathSegment segment)
		{
			return source;
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x02000080 RID: 128
	public class NavigationPropertyLinkSegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
		public NavigationPropertyLinkSegmentTemplate(NavigationPropertyLinkSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.Segment = segment;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000F801 File Offset: 0x0000DA01
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000F809 File Offset: 0x0000DA09
		public NavigationPropertyLinkSegment Segment { get; private set; }

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000F814 File Offset: 0x0000DA14
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			NavigationPropertyLinkSegment navigationPropertyLinkSegment = pathSegment as NavigationPropertyLinkSegment;
			return navigationPropertyLinkSegment != null && navigationPropertyLinkSegment.NavigationProperty == this.Segment.NavigationProperty;
		}
	}
}

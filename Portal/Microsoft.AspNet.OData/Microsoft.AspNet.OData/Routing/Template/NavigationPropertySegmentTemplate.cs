using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x0200008B RID: 139
	public class NavigationPropertySegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x000101DB File Offset: 0x0000E3DB
		public NavigationPropertySegmentTemplate(NavigationPropertySegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.Segment = segment;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000101F8 File Offset: 0x0000E3F8
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00010200 File Offset: 0x0000E400
		public NavigationPropertySegment Segment { get; private set; }

		// Token: 0x06000501 RID: 1281 RVA: 0x0001020C File Offset: 0x0000E40C
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			NavigationPropertySegment navigationPropertySegment = pathSegment as NavigationPropertySegment;
			return navigationPropertySegment != null && navigationPropertySegment.NavigationProperty == this.Segment.NavigationProperty;
		}
	}
}

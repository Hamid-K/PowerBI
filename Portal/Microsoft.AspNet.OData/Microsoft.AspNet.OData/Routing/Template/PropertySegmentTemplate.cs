using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x02000085 RID: 133
	public class PropertySegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x0000FE93 File Offset: 0x0000E093
		public PropertySegmentTemplate(PropertySegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.Segment = segment;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		public PropertySegment Segment { get; private set; }

		// Token: 0x060004DB RID: 1243 RVA: 0x0000FEC4 File Offset: 0x0000E0C4
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			PropertySegment propertySegment = pathSegment as PropertySegment;
			return propertySegment != null && propertySegment.Property == this.Segment.Property;
		}
	}
}

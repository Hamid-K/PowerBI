using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x0200008C RID: 140
	public class TypeSegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00010238 File Offset: 0x0000E438
		public TypeSegmentTemplate(TypeSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.Segment = segment;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00010255 File Offset: 0x0000E455
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0001025D File Offset: 0x0000E45D
		public TypeSegment Segment { get; private set; }

		// Token: 0x06000505 RID: 1285 RVA: 0x00010268 File Offset: 0x0000E468
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			TypeSegment typeSegment = pathSegment as TypeSegment;
			return typeSegment != null && typeSegment.EdmType.FullTypeName() == this.Segment.EdmType.FullTypeName();
		}
	}
}

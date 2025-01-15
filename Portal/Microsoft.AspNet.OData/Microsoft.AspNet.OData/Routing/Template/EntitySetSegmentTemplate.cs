using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x02000087 RID: 135
	public class EntitySetSegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x0000FF4C File Offset: 0x0000E14C
		public EntitySetSegmentTemplate(EntitySetSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.Segment = segment;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000FF69 File Offset: 0x0000E169
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000FF71 File Offset: 0x0000E171
		public EntitySetSegment Segment { get; private set; }

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000FF7C File Offset: 0x0000E17C
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			EntitySetSegment entitySetSegment = pathSegment as EntitySetSegment;
			return entitySetSegment != null && entitySetSegment.EntitySet == this.Segment.EntitySet;
		}
	}
}

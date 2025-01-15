using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x02000086 RID: 134
	public class SingletonSegmentTemplate : ODataPathSegmentTemplate
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		public SingletonSegmentTemplate(SingletonSegment segment)
		{
			if (segment == null)
			{
				throw Error.ArgumentNull("segment");
			}
			this.Segment = segment;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000FF0D File Offset: 0x0000E10D
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0000FF15 File Offset: 0x0000E115
		public SingletonSegment Segment { get; private set; }

		// Token: 0x060004DF RID: 1247 RVA: 0x0000FF20 File Offset: 0x0000E120
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			SingletonSegment singletonSegment = pathSegment as SingletonSegment;
			return singletonSegment != null && singletonSegment.Singleton == this.Segment.Singleton;
		}
	}
}

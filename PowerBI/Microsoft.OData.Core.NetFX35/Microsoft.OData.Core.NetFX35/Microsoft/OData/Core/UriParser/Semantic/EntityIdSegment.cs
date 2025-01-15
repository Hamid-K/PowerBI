using System;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000235 RID: 565
	public sealed class EntityIdSegment
	{
		// Token: 0x06001457 RID: 5207 RVA: 0x00049678 File Offset: 0x00047878
		internal EntityIdSegment(Uri id)
		{
			this.Id = id;
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00049687 File Offset: 0x00047887
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x0004968F File Offset: 0x0004788F
		public Uri Id { get; private set; }
	}
}

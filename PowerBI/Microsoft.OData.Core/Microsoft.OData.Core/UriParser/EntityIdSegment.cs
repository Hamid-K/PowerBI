using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017D RID: 381
	public sealed class EntityIdSegment
	{
		// Token: 0x060012E6 RID: 4838 RVA: 0x00038D2B File Offset: 0x00036F2B
		internal EntityIdSegment(Uri id)
		{
			this.Id = id;
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00038D3A File Offset: 0x00036F3A
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x00038D42 File Offset: 0x00036F42
		public Uri Id { get; private set; }
	}
}

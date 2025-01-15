using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000135 RID: 309
	public sealed class EntityIdSegment
	{
		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002921C File Offset: 0x0002741C
		internal EntityIdSegment(Uri id)
		{
			this.Id = id;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0002922B File Offset: 0x0002742B
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x00029233 File Offset: 0x00027433
		public Uri Id { get; private set; }
	}
}

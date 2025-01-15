using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Infrastructure.MappingViews
{
	// Token: 0x0200026F RID: 623
	public abstract class DbMappingViewCache
	{
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001F7D RID: 8061
		public abstract string MappingHashValue { get; }

		// Token: 0x06001F7E RID: 8062
		public abstract DbMappingView GetView(EntitySetBase extent);
	}
}

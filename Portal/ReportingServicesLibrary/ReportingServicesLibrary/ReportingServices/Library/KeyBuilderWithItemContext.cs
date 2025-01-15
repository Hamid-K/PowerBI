using System;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200026F RID: 623
	internal abstract class KeyBuilderWithItemContext : ICacheKeyBuilder
	{
		// Token: 0x0600165F RID: 5727 RVA: 0x0005918C File Offset: 0x0005738C
		protected KeyBuilderWithItemContext(CatalogItemContext context)
		{
			RSTrace.CacheTracer.Assert(context != null, "context != null");
			this.m_itemContext = context;
		}

		// Token: 0x06001660 RID: 5728
		public abstract bool AppendKeyInformation(StringBuilder key);

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x000591AE File Offset: 0x000573AE
		protected CatalogItemContext ItemContext
		{
			get
			{
				return this.m_itemContext;
			}
		}

		// Token: 0x0400082D RID: 2093
		private readonly CatalogItemContext m_itemContext;
	}
}

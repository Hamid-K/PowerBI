using System;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000272 RID: 626
	internal sealed class ReportParameterCacheBuilder : BaseKeyBuilder
	{
		// Token: 0x06001667 RID: 5735 RVA: 0x000592D9 File Offset: 0x000574D9
		internal ReportParameterCacheBuilder(CatalogItemContext context)
			: base(context)
		{
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x000592E4 File Offset: 0x000574E4
		public override bool AppendKeyInformation(StringBuilder key)
		{
			RSTrace.CacheTracer.Assert(key != null, "key != null");
			RSTrace.CacheTracer.Assert(base.ItemContext != null, "ItemContext != null");
			key.Append("&");
			key.Append(CacheKeyBuilderUtilities.CollectionToKey(base.ItemContext.RSRequestParameters.ReportParameters));
			return true;
		}
	}
}

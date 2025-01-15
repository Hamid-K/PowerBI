using System;
using System.Collections.Specialized;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000274 RID: 628
	internal class PrimaryRenderedStreamKeyOnlyCacheBuilder : KeyBuilderWithItemContext
	{
		// Token: 0x0600166D RID: 5741 RVA: 0x000591B6 File Offset: 0x000573B6
		internal PrimaryRenderedStreamKeyOnlyCacheBuilder(CatalogItemContext context)
			: base(context)
		{
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00059494 File Offset: 0x00057694
		public override bool AppendKeyInformation(StringBuilder key)
		{
			RSTrace.CacheTracer.Assert(key != null);
			if (CacheKeyBuilderUtilities.IsInHtmlRenderer(base.ItemContext))
			{
				return false;
			}
			NameValueCollection nameValueCollection = new NameValueCollection(base.ItemContext.RSRequestParameters.RenderingParameters);
			CacheKeyBuilderUtilities.RemoveViewerParameters(nameValueCollection);
			CacheKeyBuilderUtilities.AddPaginationMode(base.ItemContext, nameValueCollection);
			string text = CacheKeyBuilderUtilities.CollectionToKey(nameValueCollection);
			key.Append("&");
			key.Append(text + ":");
			key.Append("__PrimaryStream__");
			return true;
		}
	}
}

using System;
using System.Collections.Specialized;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000276 RID: 630
	internal abstract class SecondaryRenderedStreamCacheBuilderBase : KeyBuilderWithItemContext
	{
		// Token: 0x06001672 RID: 5746 RVA: 0x000591B6 File Offset: 0x000573B6
		protected SecondaryRenderedStreamCacheBuilderBase(CatalogItemContext context)
			: base(context)
		{
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00059664 File Offset: 0x00057864
		protected bool BuildKey(StringBuilder key, string streamName)
		{
			RSTrace.CacheTracer.Assert(key != null);
			if (this.DetectHtmlRenderer && CacheKeyBuilderUtilities.IsInHtmlRenderer(base.ItemContext))
			{
				return false;
			}
			NameValueCollection nameValueCollection = new NameValueCollection();
			foreach (object obj in base.ItemContext.RSRequestParameters.RenderingParameters.Keys)
			{
				string text = (string)obj;
				if (text == RSRequestParameters.CacheDeviceInfoTags.StreamRoot.ToString())
				{
					nameValueCollection.Add(text, base.ItemContext.RSRequestParameters.RenderingParameters[text]);
				}
			}
			string text2 = CacheKeyBuilderUtilities.CollectionToKey(nameValueCollection);
			key.Append("&");
			key.Append(text2);
			key.Append(":");
			key.Append(streamName);
			return true;
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001674 RID: 5748
		protected abstract bool DetectHtmlRenderer { get; }
	}
}

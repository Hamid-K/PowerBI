using System;
using System.Collections.Specialized;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000273 RID: 627
	internal sealed class CatalogParametersCacheBuilder : BaseKeyBuilder
	{
		// Token: 0x06001669 RID: 5737 RVA: 0x000592D9 File Offset: 0x000574D9
		internal CatalogParametersCacheBuilder(CatalogItemContext context)
			: base(context)
		{
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00059348 File Offset: 0x00057548
		public override bool AppendKeyInformation(StringBuilder key)
		{
			RSTrace.CacheTracer.Assert(key != null, "key != null");
			if (this.IsNotCacheable)
			{
				return false;
			}
			NameValueCollection nameValueCollection = new NameValueCollection(base.ItemContext.RSRequestParameters.CatalogParameters);
			for (int i = 0; i < CatalogParametersCacheBuilder.m_filteredParameters.Length; i++)
			{
				nameValueCollection.Remove(CatalogParametersCacheBuilder.m_filteredParameters[i]);
			}
			string text = CacheKeyBuilderUtilities.CollectionToKey(nameValueCollection);
			key.Append("&");
			key.Append(text);
			return true;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x000593C4 File Offset: 0x000575C4
		private bool IsNotCacheable
		{
			get
			{
				NameValueCollection catalogParameters = base.ItemContext.RSRequestParameters.CatalogParameters;
				return CacheKeyBuilderUtilities.HasNonCacheableParameter(catalogParameters, base.ItemContext, "ShowHideToggle") || CacheKeyBuilderUtilities.HasNonCacheableParameter(catalogParameters, base.ItemContext, "Snapshot") || CacheKeyBuilderUtilities.HasNonCacheableParameter(catalogParameters, base.ItemContext, "PersistStreams") || CacheKeyBuilderUtilities.HasNonCacheableParameter(catalogParameters, base.ItemContext, "GetNextStream");
			}
		}

		// Token: 0x0400082F RID: 2095
		private static readonly string[] m_filteredParameters = new string[] { "SessionID", "ImageID", "Command", "ClearSession", "ErrorResponseAsXml", "AllowNewSessions", "PageCountMode", "ShowHideToggle", "Snapshot" };
	}
}

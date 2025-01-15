using System;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000278 RID: 632
	internal sealed class SecondaryRenderedStreamCacheBuilder : SecondaryRenderedStreamCacheBuilderBase, ICachedItemBuilder
	{
		// Token: 0x06001678 RID: 5752 RVA: 0x00059785 File Offset: 0x00057985
		internal SecondaryRenderedStreamCacheBuilder(CatalogItemContext context, RSStream streamToCache, ReportRenderingResult procResult, bool detectHtmlRenderer)
			: base(context)
		{
			RSTrace.CacheTracer.Assert(streamToCache != null);
			this.m_streamToCache = streamToCache;
			this.m_procResult = procResult;
			this.m_detectHtmlRenderer = detectHtmlRenderer;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x000597B2 File Offset: 0x000579B2
		public override bool AppendKeyInformation(StringBuilder key)
		{
			RSTrace.CacheTracer.Assert(key != null);
			RSTrace.CacheTracer.Assert(this.m_streamToCache != null);
			return base.BuildKey(key, this.m_streamToCache.Name);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000597E8 File Offset: 0x000579E8
		public IHierarchicalCachedItem CreateCachedItem(string itemKey, DateTime expirationDate, IHierarchicalCachedItem parent)
		{
			RSTrace.CacheTracer.Assert(!string.IsNullOrEmpty(itemKey));
			RSTrace.CacheTracer.Assert(parent != null);
			RSTrace.CacheTracer.Assert(this.m_streamToCache != null);
			CachedRenderingResult cachedRenderingResult = new CachedRenderingResult(this.m_procResult, this.m_streamToCache);
			((ICachedItem)cachedRenderingResult).Key = itemKey;
			((IHierarchicalCachedItem)cachedRenderingResult).MakeDependentOn(parent);
			return cachedRenderingResult;
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00059848 File Offset: 0x00057A48
		protected override bool DetectHtmlRenderer
		{
			get
			{
				return this.m_detectHtmlRenderer;
			}
		}

		// Token: 0x04000833 RID: 2099
		private readonly RSStream m_streamToCache;

		// Token: 0x04000834 RID: 2100
		private readonly ReportRenderingResult m_procResult;

		// Token: 0x04000835 RID: 2101
		private readonly bool m_detectHtmlRenderer;
	}
}

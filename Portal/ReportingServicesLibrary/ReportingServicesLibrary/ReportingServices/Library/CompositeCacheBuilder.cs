using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000279 RID: 633
	internal sealed class CompositeCacheBuilder
	{
		// Token: 0x0600167C RID: 5756 RVA: 0x00059850 File Offset: 0x00057A50
		internal static string BuildReportRootKey(CatalogItemContext context)
		{
			BaseKeyBuilder baseKeyBuilder = new BaseKeyBuilder(context);
			return CompositeCacheBuilder.CreateCacheKey(new ICacheKeyBuilder[] { baseKeyBuilder });
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00059873 File Offset: 0x00057A73
		internal static string BuildMainStreamKey(CatalogItemContext context, ReportSnapshot reportSnapshot)
		{
			return CompositeCacheBuilder.CreateCacheKey(new ICacheKeyBuilder[]
			{
				new BaseKeyBuilder(context),
				new ReportSnapshotCacheBuilder(context, reportSnapshot),
				new ReportParameterCacheBuilder(context),
				new CatalogParametersCacheBuilder(context),
				new PrimaryRenderedStreamKeyOnlyCacheBuilder(context)
			});
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000598AE File Offset: 0x00057AAE
		internal static string BuildSecondaryStreamKey(CatalogItemContext context, string streamName, ReportSnapshot reportSnapshot)
		{
			return CompositeCacheBuilder.CreateCacheKey(new ICacheKeyBuilder[]
			{
				new BaseKeyBuilder(context),
				new ReportSnapshotCacheBuilder(context, reportSnapshot),
				new ReportParameterCacheBuilder(context),
				new CatalogParametersCacheBuilder(context),
				new NameOnlySecondaryRenderedStreamCacheBuilder(context, streamName)
			});
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000598EC File Offset: 0x00057AEC
		internal static SortedList BuildMainStream(CatalogItemContext context, ReportRenderingResult renderResult, StreamManager streamManager, DateTime expirationDate, ReportSnapshot reportSnapshot)
		{
			ICacheKeyBuilder[] array = new ICacheKeyBuilder[]
			{
				new BaseKeyBuilder(context),
				new ReportSnapshotCacheBuilder(context, reportSnapshot),
				new ReportParameterCacheBuilder(context),
				new CatalogParametersCacheBuilder(context),
				new PrimaryRenderedStreamCacheBuilder(context, streamManager, renderResult)
			};
			return CompositeCacheBuilder.CreateCacheItemChain(expirationDate, array);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00059938 File Offset: 0x00057B38
		internal static SortedList BuildSecondaryStream(CatalogItemContext context, ReportRenderingResult renderResult, RSStream stream, DateTime expirationDate, ReportSnapshot reportSnapshot)
		{
			return CompositeCacheBuilder.BuildSecondaryStream(context, renderResult, stream, expirationDate, reportSnapshot, true);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00059948 File Offset: 0x00057B48
		internal static SortedList BuildSecondaryStream(CatalogItemContext context, ReportRenderingResult renderResult, RSStream stream, DateTime expirationDate, ReportSnapshot reportSnapshot, bool detectHtmlRenderer)
		{
			ICacheKeyBuilder[] array = new ICacheKeyBuilder[]
			{
				new BaseKeyBuilder(context),
				new ReportSnapshotCacheBuilder(context, reportSnapshot),
				new ReportParameterCacheBuilder(context),
				new CatalogParametersCacheBuilder(context),
				new SecondaryRenderedStreamCacheBuilder(context, stream, renderResult, detectHtmlRenderer)
			};
			return CompositeCacheBuilder.CreateCacheItemChain(expirationDate, array);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00059998 File Offset: 0x00057B98
		private static string CreateCacheKey(IEnumerable<ICacheKeyBuilder> keyBuilders)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (IEnumerator<ICacheKeyBuilder> enumerator = keyBuilders.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.AppendKeyInformation(stringBuilder))
					{
						return null;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x000599F4 File Offset: 0x00057BF4
		private static SortedList CreateCacheItemChain(DateTime expirationDate, IEnumerable<ICacheKeyBuilder> keyBuilders)
		{
			StringBuilder stringBuilder = new StringBuilder();
			SortedList sortedList = new SortedList();
			IHierarchicalCachedItem hierarchicalCachedItem = null;
			foreach (ICacheKeyBuilder cacheKeyBuilder in keyBuilders)
			{
				if (!cacheKeyBuilder.AppendKeyInformation(stringBuilder))
				{
					return null;
				}
				ICachedItemBuilder cachedItemBuilder = cacheKeyBuilder as ICachedItemBuilder;
				if (cachedItemBuilder != null)
				{
					string text = stringBuilder.ToString();
					IHierarchicalCachedItem hierarchicalCachedItem2 = cachedItemBuilder.CreateCachedItem(text, expirationDate, hierarchicalCachedItem);
					sortedList.Add(text, hierarchicalCachedItem2);
					hierarchicalCachedItem = hierarchicalCachedItem2;
				}
			}
			return sortedList;
		}
	}
}

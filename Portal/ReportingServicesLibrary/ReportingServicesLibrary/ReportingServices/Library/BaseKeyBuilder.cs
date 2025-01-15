using System;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000270 RID: 624
	internal class BaseKeyBuilder : KeyBuilderWithItemContext, ICachedItemBuilder
	{
		// Token: 0x06001662 RID: 5730 RVA: 0x000591B6 File Offset: 0x000573B6
		internal BaseKeyBuilder(CatalogItemContext context)
			: base(context)
		{
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000591C0 File Offset: 0x000573C0
		public override bool AppendKeyInformation(StringBuilder key)
		{
			RSTrace.CacheTracer.Assert(key != null);
			RSTrace.CacheTracer.Assert(base.ItemContext != null);
			key.Append("__RenderedSnapshotResult__");
			key.Append(":");
			key.Append(base.ItemContext.ItemPath.Value);
			return true;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00059220 File Offset: 0x00057420
		public virtual IHierarchicalCachedItem CreateCachedItem(string itemKey, DateTime expirationDate, IHierarchicalCachedItem parent)
		{
			RSTrace.CacheTracer.Assert(!string.IsNullOrEmpty(itemKey));
			IHierarchicalCachedItem hierarchicalCachedItem = new HierarchicalCachedItem(itemKey, expirationDate);
			if (parent != null)
			{
				hierarchicalCachedItem.MakeDependentOn(parent);
			}
			return hierarchicalCachedItem;
		}
	}
}

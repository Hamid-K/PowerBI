using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Caching;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000269 RID: 617
	internal class HierarchicalCachedItem : CachedItemBase, IHierarchicalCachedItem, ICachedItem
	{
		// Token: 0x0600163C RID: 5692 RVA: 0x00058809 File Offset: 0x00056A09
		internal HierarchicalCachedItem()
			: base(null, Cache.NoAbsoluteExpiration)
		{
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00058822 File Offset: 0x00056A22
		internal HierarchicalCachedItem(string key, DateTime expirationDate)
			: base(key, expirationDate)
		{
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x00058837 File Offset: 0x00056A37
		public string ParentKey
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_parentKey;
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00058840 File Offset: 0x00056A40
		public void MakeDependentOn(IHierarchicalCachedItem parent)
		{
			if (this.m_parentKey != null && this.m_parentKey != parent.Key)
			{
				throw new InternalCatalogException("Cannot change dependency");
			}
			this.m_parentKey = parent.Key;
			parent.AddDependentKey(base.Key);
			base.ExpirationDate = parent.ExpirationDate;
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x00058897 File Offset: 0x00056A97
		public List<string> ChildKeys
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_childKeys;
			}
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000588A0 File Offset: 0x00056AA0
		public void AddDependentKey(string parentKey)
		{
			List<string> childKeys = this.m_childKeys;
			lock (childKeys)
			{
				if (!this.m_childKeys.Contains(parentKey))
				{
					this.m_childKeys.Add(parentKey);
				}
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x000588F4 File Offset: 0x00056AF4
		public override long SizeEstimateKb
		{
			[DebuggerStepThrough]
			get
			{
				return 0L;
			}
		}

		// Token: 0x04000816 RID: 2070
		protected List<string> m_childKeys = new List<string>();

		// Token: 0x04000817 RID: 2071
		protected string m_parentKey;
	}
}

using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001E8 RID: 488
	public abstract class SelectItemTranslator<T>
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(WildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(PathSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(NamespaceQualifiedWildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(ExpandedNavigationSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Translate(ExpandedReferenceSelectItem item)
		{
			throw new NotImplementedException();
		}
	}
}

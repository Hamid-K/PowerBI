using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001E7 RID: 487
	public abstract class SelectItemHandler
	{
		// Token: 0x06001600 RID: 5632 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(WildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(PathSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(NamespaceQualifiedWildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(ExpandedNavigationSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Handle(ExpandedReferenceSelectItem item)
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x02000298 RID: 664
	public abstract class SelectItemHandler
	{
		// Token: 0x060016C6 RID: 5830 RVA: 0x0004E866 File Offset: 0x0004CA66
		public virtual void Handle(WildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x0004E86D File Offset: 0x0004CA6D
		public virtual void Handle(PathSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0004E874 File Offset: 0x0004CA74
		public virtual void Handle(NamespaceQualifiedWildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0004E87B File Offset: 0x0004CA7B
		public virtual void Handle(ExpandedNavigationSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0004E882 File Offset: 0x0004CA82
		public virtual void Handle(ExpandedReferenceSelectItem item)
		{
			throw new NotImplementedException();
		}
	}
}

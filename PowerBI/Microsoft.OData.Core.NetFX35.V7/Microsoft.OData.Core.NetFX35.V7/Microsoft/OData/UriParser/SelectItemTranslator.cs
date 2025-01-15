using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000198 RID: 408
	public abstract class SelectItemTranslator<T>
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(WildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(PathSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(NamespaceQualifiedWildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(ExpandedNavigationSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Translate(ExpandedReferenceSelectItem item)
		{
			throw new NotImplementedException();
		}
	}
}

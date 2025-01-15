using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000197 RID: 407
	public abstract class SelectItemHandler
	{
		// Token: 0x06001081 RID: 4225 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(WildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(PathSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(NamespaceQualifiedWildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(ExpandedNavigationSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Handle(ExpandedReferenceSelectItem item)
		{
			throw new NotImplementedException();
		}
	}
}

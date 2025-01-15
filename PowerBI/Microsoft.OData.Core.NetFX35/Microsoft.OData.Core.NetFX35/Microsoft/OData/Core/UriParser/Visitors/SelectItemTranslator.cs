using System;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x020001C0 RID: 448
	public abstract class SelectItemTranslator<T>
	{
		// Token: 0x060010CF RID: 4303 RVA: 0x0003A751 File Offset: 0x00038951
		public virtual T Translate(WildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003A758 File Offset: 0x00038958
		public virtual T Translate(PathSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003A75F File Offset: 0x0003895F
		public virtual T Translate(NamespaceQualifiedWildcardSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003A766 File Offset: 0x00038966
		public virtual T Translate(ExpandedNavigationSelectItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003A76D File Offset: 0x0003896D
		public virtual T Translate(ExpandedReferenceSelectItem item)
		{
			throw new NotImplementedException();
		}
	}
}

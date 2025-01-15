using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018A RID: 394
	public sealed class NamespaceQualifiedWildcardSelectItem : SelectItem
	{
		// Token: 0x0600134E RID: 4942 RVA: 0x000394FE File Offset: 0x000376FE
		public NamespaceQualifiedWildcardSelectItem(string namespaceName)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(namespaceName, "namespaceName");
			this.Namespace = namespaceName;
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00039519 File Offset: 0x00037719
		// (set) Token: 0x06001350 RID: 4944 RVA: 0x00039521 File Offset: 0x00037721
		public string Namespace { get; private set; }

		// Token: 0x06001351 RID: 4945 RVA: 0x0003952A File Offset: 0x0003772A
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00039533 File Offset: 0x00037733
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}

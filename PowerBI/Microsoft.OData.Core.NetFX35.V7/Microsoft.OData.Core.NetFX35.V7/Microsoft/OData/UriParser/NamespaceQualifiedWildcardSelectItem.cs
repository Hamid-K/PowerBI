using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000140 RID: 320
	public sealed class NamespaceQualifiedWildcardSelectItem : SelectItem
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x00029836 File Offset: 0x00027A36
		public NamespaceQualifiedWildcardSelectItem(string namespaceName)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(namespaceName, "namespaceName");
			this.Namespace = namespaceName;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00029851 File Offset: 0x00027A51
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x00029859 File Offset: 0x00027A59
		public string Namespace { get; private set; }

		// Token: 0x06000E4B RID: 3659 RVA: 0x00029862 File Offset: 0x00027A62
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0002986B File Offset: 0x00027A6B
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}

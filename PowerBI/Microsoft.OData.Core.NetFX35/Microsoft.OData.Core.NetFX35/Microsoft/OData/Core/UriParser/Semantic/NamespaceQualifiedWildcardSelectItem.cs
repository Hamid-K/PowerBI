using System;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000246 RID: 582
	public sealed class NamespaceQualifiedWildcardSelectItem : SelectItem
	{
		// Token: 0x060014BE RID: 5310 RVA: 0x00049D9A File Offset: 0x00047F9A
		public NamespaceQualifiedWildcardSelectItem(string namespaceName)
		{
			ExceptionUtils.CheckArgumentNotNull<string>(namespaceName, "namespaceName");
			this.Namespace = namespaceName;
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00049DB4 File Offset: 0x00047FB4
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x00049DBC File Offset: 0x00047FBC
		public string Namespace { get; private set; }

		// Token: 0x060014C1 RID: 5313 RVA: 0x00049DC5 File Offset: 0x00047FC5
		public override T TranslateWith<T>(SelectItemTranslator<T> translator)
		{
			return translator.Translate(this);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00049DCE File Offset: 0x00047FCE
		public override void HandleWith(SelectItemHandler handler)
		{
			handler.Handle(this);
		}
	}
}

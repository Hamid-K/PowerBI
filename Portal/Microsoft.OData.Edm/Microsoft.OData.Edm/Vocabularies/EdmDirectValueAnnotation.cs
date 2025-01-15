using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000D8 RID: 216
	public class EdmDirectValueAnnotation : EdmNamedElement, IEdmDirectValueAnnotation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x060006A0 RID: 1696 RVA: 0x0000F859 File Offset: 0x0000DA59
		public EdmDirectValueAnnotation(string namespaceUri, string name, object value)
			: this(namespaceUri, name)
		{
			EdmUtil.CheckArgumentNull<object>(value, "value");
			this.value = value;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0000F876 File Offset: 0x0000DA76
		internal EdmDirectValueAnnotation(string namespaceUri, string name)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceUri, "namespaceUri");
			this.namespaceUri = namespaceUri;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0000F892 File Offset: 0x0000DA92
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0000F89A File Offset: 0x0000DA9A
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040002DB RID: 731
		private readonly object value;

		// Token: 0x040002DC RID: 732
		private readonly string namespaceUri;
	}
}

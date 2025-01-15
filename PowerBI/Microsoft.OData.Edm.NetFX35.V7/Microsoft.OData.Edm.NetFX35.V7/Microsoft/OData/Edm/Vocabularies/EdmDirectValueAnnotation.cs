using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000DE RID: 222
	public class EdmDirectValueAnnotation : EdmNamedElement, IEdmDirectValueAnnotation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x000115ED File Offset: 0x0000F7ED
		public EdmDirectValueAnnotation(string namespaceUri, string name, object value)
			: this(namespaceUri, name)
		{
			EdmUtil.CheckArgumentNull<object>(value, "value");
			this.value = value;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001160A File Offset: 0x0000F80A
		internal EdmDirectValueAnnotation(string namespaceUri, string name)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceUri, "namespaceUri");
			this.namespaceUri = namespaceUri;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00011626 File Offset: 0x0000F826
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001162E File Offset: 0x0000F82E
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040003E7 RID: 999
		private readonly object value;

		// Token: 0x040003E8 RID: 1000
		private readonly string namespaceUri;
	}
}

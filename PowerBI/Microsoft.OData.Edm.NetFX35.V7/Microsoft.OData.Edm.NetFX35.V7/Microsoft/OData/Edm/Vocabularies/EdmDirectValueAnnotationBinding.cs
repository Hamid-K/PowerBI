using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000DF RID: 223
	public class EdmDirectValueAnnotationBinding : IEdmDirectValueAnnotationBinding
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x00011638 File Offset: 0x0000F838
		public EdmDirectValueAnnotationBinding(IEdmElement element, string namespaceUri, string name, object value)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			EdmUtil.CheckArgumentNull<string>(namespaceUri, "namespaceUri");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.element = element;
			this.namespaceUri = namespaceUri;
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001168C File Offset: 0x0000F88C
		public EdmDirectValueAnnotationBinding(IEdmElement element, string namespaceUri, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			EdmUtil.CheckArgumentNull<string>(namespaceUri, "namespaceUri");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.element = element;
			this.namespaceUri = namespaceUri;
			this.name = name;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x000116D8 File Offset: 0x0000F8D8
		public IEdmElement Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x000116E0 File Offset: 0x0000F8E0
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x000116E8 File Offset: 0x0000F8E8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x000116F0 File Offset: 0x0000F8F0
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040003E9 RID: 1001
		private readonly IEdmElement element;

		// Token: 0x040003EA RID: 1002
		private readonly string namespaceUri;

		// Token: 0x040003EB RID: 1003
		private readonly string name;

		// Token: 0x040003EC RID: 1004
		private readonly object value;
	}
}

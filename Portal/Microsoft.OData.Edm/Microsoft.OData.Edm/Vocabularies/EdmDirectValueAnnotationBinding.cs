using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000D9 RID: 217
	public class EdmDirectValueAnnotationBinding : IEdmDirectValueAnnotationBinding
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
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

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000F8F8 File Offset: 0x0000DAF8
		public EdmDirectValueAnnotationBinding(IEdmElement element, string namespaceUri, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			EdmUtil.CheckArgumentNull<string>(namespaceUri, "namespaceUri");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.element = element;
			this.namespaceUri = namespaceUri;
			this.name = name;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0000F944 File Offset: 0x0000DB44
		public IEdmElement Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0000F94C File Offset: 0x0000DB4C
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0000F954 File Offset: 0x0000DB54
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0000F95C File Offset: 0x0000DB5C
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040002DD RID: 733
		private readonly IEdmElement element;

		// Token: 0x040002DE RID: 734
		private readonly string namespaceUri;

		// Token: 0x040002DF RID: 735
		private readonly string name;

		// Token: 0x040002E0 RID: 736
		private readonly object value;
	}
}

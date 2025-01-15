using System;
using Microsoft.OData.Edm.Annotations;

namespace Microsoft.OData.Edm.Library.Annotations
{
	// Token: 0x02000168 RID: 360
	public class EdmDirectValueAnnotationBinding : IEdmDirectValueAnnotationBinding
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x00010050 File Offset: 0x0000E250
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

		// Token: 0x060006CB RID: 1739 RVA: 0x000100A4 File Offset: 0x0000E2A4
		public EdmDirectValueAnnotationBinding(IEdmElement element, string namespaceUri, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			EdmUtil.CheckArgumentNull<string>(namespaceUri, "namespaceUri");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.element = element;
			this.namespaceUri = namespaceUri;
			this.name = name;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public IEdmElement Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x000100F8 File Offset: 0x0000E2F8
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00010100 File Offset: 0x0000E300
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00010108 File Offset: 0x0000E308
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040002BB RID: 699
		private readonly IEdmElement element;

		// Token: 0x040002BC RID: 700
		private readonly string namespaceUri;

		// Token: 0x040002BD RID: 701
		private readonly string name;

		// Token: 0x040002BE RID: 702
		private readonly object value;
	}
}

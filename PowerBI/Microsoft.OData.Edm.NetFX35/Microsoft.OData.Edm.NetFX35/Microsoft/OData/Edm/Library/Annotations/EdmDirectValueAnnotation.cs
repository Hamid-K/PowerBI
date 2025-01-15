using System;
using Microsoft.OData.Edm.Annotations;

namespace Microsoft.OData.Edm.Library.Annotations
{
	// Token: 0x02000209 RID: 521
	public class EdmDirectValueAnnotation : EdmNamedElement, IEdmDirectValueAnnotation, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000C48 RID: 3144 RVA: 0x00022A68 File Offset: 0x00020C68
		public EdmDirectValueAnnotation(string namespaceUri, string name, object value)
			: this(namespaceUri, name)
		{
			EdmUtil.CheckArgumentNull<object>(value, "value");
			this.value = value;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00022A85 File Offset: 0x00020C85
		internal EdmDirectValueAnnotation(string namespaceUri, string name)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceUri, "namespaceUri");
			this.namespaceUri = namespaceUri;
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00022AA1 File Offset: 0x00020CA1
		public string NamespaceUri
		{
			get
			{
				return this.namespaceUri;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00022AA9 File Offset: 0x00020CA9
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000598 RID: 1432
		private readonly object value;

		// Token: 0x04000599 RID: 1433
		private readonly string namespaceUri;
	}
}

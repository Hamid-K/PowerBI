using System;
using Microsoft.OData.Edm.Annotations;

namespace Microsoft.OData.Edm.Library.Annotations
{
	// Token: 0x02000169 RID: 361
	public class EdmTypedDirectValueAnnotationBinding<T> : EdmNamedElement, IEdmDirectValueAnnotationBinding
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x00010110 File Offset: 0x0000E310
		public EdmTypedDirectValueAnnotationBinding(IEdmElement element, T value)
			: base(ExtensionMethods.TypeName<T>.LocalName)
		{
			this.element = element;
			this.value = value;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001012B File Offset: 0x0000E32B
		public IEdmElement Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00010133 File Offset: 0x0000E333
		public string NamespaceUri
		{
			get
			{
				return "http://schemas.microsoft.com/ado/2011/04/edm/internal";
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001013A File Offset: 0x0000E33A
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040002BF RID: 703
		private readonly IEdmElement element;

		// Token: 0x040002C0 RID: 704
		private readonly T value;
	}
}

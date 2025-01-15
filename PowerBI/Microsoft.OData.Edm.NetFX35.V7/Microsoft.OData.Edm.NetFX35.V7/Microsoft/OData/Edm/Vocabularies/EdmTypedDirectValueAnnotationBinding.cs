using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E3 RID: 227
	public class EdmTypedDirectValueAnnotationBinding<T> : EdmNamedElement, IEdmDirectValueAnnotationBinding
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x00011E1F File Offset: 0x0001001F
		public EdmTypedDirectValueAnnotationBinding(IEdmElement element, T value)
			: base(ExtensionMethods.TypeName<T>.LocalName)
		{
			this.element = element;
			this.value = value;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00011E3A File Offset: 0x0001003A
		public IEdmElement Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00011E42 File Offset: 0x00010042
		public string NamespaceUri
		{
			get
			{
				return "http://schemas.microsoft.com/ado/2011/04/edm/internal";
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00011E49 File Offset: 0x00010049
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040003F7 RID: 1015
		private readonly IEdmElement element;

		// Token: 0x040003F8 RID: 1016
		private readonly T value;
	}
}

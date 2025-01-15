using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000DC RID: 220
	public class EdmTypedDirectValueAnnotationBinding<T> : EdmNamedElement, IEdmDirectValueAnnotationBinding
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x0000FFD2 File Offset: 0x0000E1D2
		public EdmTypedDirectValueAnnotationBinding(IEdmElement element, T value)
			: base(ExtensionMethods.TypeName<T>.LocalName)
		{
			this.element = element;
			this.value = value;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0000FFED File Offset: 0x0000E1ED
		public IEdmElement Element
		{
			get
			{
				return this.element;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0000FFF5 File Offset: 0x0000E1F5
		public string NamespaceUri
		{
			get
			{
				return "http://schemas.microsoft.com/ado/2011/04/edm/internal";
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040002E7 RID: 743
		private readonly IEdmElement element;

		// Token: 0x040002E8 RID: 744
		private readonly T value;
	}
}

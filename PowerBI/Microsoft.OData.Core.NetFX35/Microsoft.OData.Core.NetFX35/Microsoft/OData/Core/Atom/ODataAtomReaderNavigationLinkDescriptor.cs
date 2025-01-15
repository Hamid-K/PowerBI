using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200005A RID: 90
	internal sealed class ODataAtomReaderNavigationLinkDescriptor
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x0000DAE3 File Offset: 0x0000BCE3
		internal ODataAtomReaderNavigationLinkDescriptor(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			this.navigationLink = navigationLink;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000DAF9 File Offset: 0x0000BCF9
		internal ODataNavigationLink NavigationLink
		{
			get
			{
				return this.navigationLink;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000DB01 File Offset: 0x0000BD01
		internal IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x040001C0 RID: 448
		private ODataNavigationLink navigationLink;

		// Token: 0x040001C1 RID: 449
		private IEdmNavigationProperty navigationProperty;
	}
}

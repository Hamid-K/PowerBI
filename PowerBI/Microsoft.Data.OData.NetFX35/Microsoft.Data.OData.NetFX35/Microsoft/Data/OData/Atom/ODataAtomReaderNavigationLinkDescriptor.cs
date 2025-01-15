using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000FC RID: 252
	internal sealed class ODataAtomReaderNavigationLinkDescriptor
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x000177AB File Offset: 0x000159AB
		internal ODataAtomReaderNavigationLinkDescriptor(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			this.navigationLink = navigationLink;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x000177C1 File Offset: 0x000159C1
		internal ODataNavigationLink NavigationLink
		{
			get
			{
				return this.navigationLink;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x000177C9 File Offset: 0x000159C9
		internal IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x04000297 RID: 663
		private ODataNavigationLink navigationLink;

		// Token: 0x04000298 RID: 664
		private IEdmNavigationProperty navigationProperty;
	}
}

using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000113 RID: 275
	public class EdmNavigationPropertyBinding : IEdmNavigationPropertyBinding
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public EdmNavigationPropertyBinding(IEdmNavigationProperty navigationProperty, IEdmNavigationSource target)
		{
			this.navigationProperty = navigationProperty;
			this.target = target;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000DC6A File Offset: 0x0000BE6A
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000DC72 File Offset: 0x0000BE72
		public IEdmNavigationSource Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x04000216 RID: 534
		private IEdmNavigationProperty navigationProperty;

		// Token: 0x04000217 RID: 535
		private IEdmNavigationSource target;
	}
}

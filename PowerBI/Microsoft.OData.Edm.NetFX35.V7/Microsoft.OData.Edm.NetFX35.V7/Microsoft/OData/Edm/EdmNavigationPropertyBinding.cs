using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000067 RID: 103
	public class EdmNavigationPropertyBinding : IEdmNavigationPropertyBinding
	{
		// Token: 0x060003AE RID: 942 RVA: 0x0000B7B1 File Offset: 0x000099B1
		public EdmNavigationPropertyBinding(IEdmNavigationProperty navigationProperty, IEdmNavigationSource target)
		{
			this.navigationProperty = navigationProperty;
			this.target = target;
			this.path = new EdmPathExpression((navigationProperty == null) ? string.Empty : navigationProperty.Name);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000B7E2 File Offset: 0x000099E2
		public EdmNavigationPropertyBinding(IEdmNavigationProperty navigationProperty, IEdmNavigationSource target, IEdmPathExpression bindingPath)
		{
			this.navigationProperty = navigationProperty;
			this.target = target;
			this.path = bindingPath;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000B7FF File Offset: 0x000099FF
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000B807 File Offset: 0x00009A07
		public IEdmNavigationSource Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000B80F File Offset: 0x00009A0F
		public IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x040000DB RID: 219
		private IEdmNavigationProperty navigationProperty;

		// Token: 0x040000DC RID: 220
		private IEdmNavigationSource target;

		// Token: 0x040000DD RID: 221
		private IEdmPathExpression path;
	}
}

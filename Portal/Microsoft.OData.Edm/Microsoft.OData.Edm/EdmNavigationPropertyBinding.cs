using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000047 RID: 71
	public class EdmNavigationPropertyBinding : IEdmNavigationPropertyBinding
	{
		// Token: 0x06000171 RID: 369 RVA: 0x00004916 File Offset: 0x00002B16
		public EdmNavigationPropertyBinding(IEdmNavigationProperty navigationProperty, IEdmNavigationSource target)
		{
			this.navigationProperty = navigationProperty;
			this.target = target;
			this.path = new EdmPathExpression((navigationProperty == null) ? string.Empty : navigationProperty.Name);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004947 File Offset: 0x00002B47
		public EdmNavigationPropertyBinding(IEdmNavigationProperty navigationProperty, IEdmNavigationSource target, IEdmPathExpression bindingPath)
		{
			this.navigationProperty = navigationProperty;
			this.target = target;
			this.path = bindingPath;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00004964 File Offset: 0x00002B64
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000496C File Offset: 0x00002B6C
		public IEdmNavigationSource Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00004974 File Offset: 0x00002B74
		public IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x04000089 RID: 137
		private IEdmNavigationProperty navigationProperty;

		// Token: 0x0400008A RID: 138
		private IEdmNavigationSource target;

		// Token: 0x0400008B RID: 139
		private IEdmPathExpression path;
	}
}

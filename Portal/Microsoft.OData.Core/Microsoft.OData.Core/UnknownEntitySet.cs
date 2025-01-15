using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x02000014 RID: 20
	internal class UnknownEntitySet : IEdmUnknownEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x000035EA File Offset: 0x000017EA
		public UnknownEntitySet(IEdmNavigationSource parentNavigationSource, IEdmNavigationProperty navigationProperty)
		{
			this.parentNavigationSource = parentNavigationSource;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003600 File Offset: 0x00001800
		public string Name
		{
			get
			{
				return this.navigationProperty.Name;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000360D File Offset: 0x0000180D
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003610 File Offset: 0x00001810
		public IEdmPathExpression Path
		{
			get
			{
				IEdmPathExpression edmPathExpression;
				if ((edmPathExpression = this.path) == null)
				{
					edmPathExpression = (this.path = this.ComputePath());
				}
				return edmPathExpression;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003636 File Offset: 0x00001836
		public IEdmType Type
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000360D File Offset: 0x0000180D
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty)
		{
			return null;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000360D File Offset: 0x0000180D
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			return null;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000360D File Offset: 0x0000180D
		public IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			return null;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003648 File Offset: 0x00001848
		private IEdmPathExpression ComputePath()
		{
			return new EdmPathExpression(new List<string>(this.parentNavigationSource.Path.PathSegments) { this.navigationProperty.Name }.ToArray());
		}

		// Token: 0x0400003A RID: 58
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x0400003B RID: 59
		private readonly IEdmNavigationSource parentNavigationSource;

		// Token: 0x0400003C RID: 60
		private IEdmPathExpression path;
	}
}

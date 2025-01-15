using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003E RID: 62
	internal class EdmUnknownEntitySet : EdmEntitySetBase, IEdmUnknownEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00004488 File Offset: 0x00002688
		public EdmUnknownEntitySet(IEdmNavigationSource parentNavigationSource, IEdmNavigationProperty navigationProperty)
			: base(navigationProperty.Name, navigationProperty.ToEntityType())
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(parentNavigationSource, "parentNavigationSource");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			this.parentNavigationSource = parentNavigationSource;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000044C4 File Offset: 0x000026C4
		public override IEdmPathExpression Path
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000044EA File Offset: 0x000026EA
		public override IEdmType Type
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000026B0 File Offset: 0x000008B0
		public override IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000044FC File Offset: 0x000026FC
		private IEdmPathExpression ComputePath()
		{
			return new EdmPathExpression(new List<string>(this.parentNavigationSource.Path.PathSegments) { this.navigationProperty.Name }.ToArray());
		}

		// Token: 0x04000073 RID: 115
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x04000074 RID: 116
		private readonly IEdmNavigationSource parentNavigationSource;

		// Token: 0x04000075 RID: 117
		private IEdmPathExpression path;
	}
}

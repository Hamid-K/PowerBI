using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007D RID: 125
	internal class EdmUnknownEntitySet : EdmEntitySetBase, IEdmUnknownEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0000C770 File Offset: 0x0000A970
		public EdmUnknownEntitySet(IEdmNavigationSource parentNavigationSource, IEdmNavigationProperty navigationProperty)
			: base(navigationProperty.Name, navigationProperty.ToEntityType())
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(parentNavigationSource, "parentNavigationSource");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			this.parentNavigationSource = parentNavigationSource;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000C7AC File Offset: 0x0000A9AC
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

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
		public override IEdmType Type
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00008D69 File Offset: 0x00006F69
		public override IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		private IEdmPathExpression ComputePath()
		{
			List<string> list = new List<string>(this.parentNavigationSource.Path.PathSegments);
			list.Add(this.navigationProperty.Name);
			return new EdmPathExpression(list.ToArray());
		}

		// Token: 0x04000117 RID: 279
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x04000118 RID: 280
		private readonly IEdmNavigationSource parentNavigationSource;

		// Token: 0x04000119 RID: 281
		private IEdmPathExpression path;
	}
}

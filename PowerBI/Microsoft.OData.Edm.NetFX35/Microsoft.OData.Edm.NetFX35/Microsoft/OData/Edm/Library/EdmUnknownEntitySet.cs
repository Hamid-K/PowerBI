using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000108 RID: 264
	internal class EdmUnknownEntitySet : EdmEntitySetBase, IEdmUnknownEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x0000D65D File Offset: 0x0000B85D
		public EdmUnknownEntitySet(IEdmNavigationSource parentNavigationSource, IEdmNavigationProperty navigationProperty)
			: base(navigationProperty.Name, navigationProperty.ToEntityType())
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(parentNavigationSource, "parentNavigationSource");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			this.parentNavigationSource = parentNavigationSource;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000D698 File Offset: 0x0000B898
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

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0000D6BE File Offset: 0x0000B8BE
		public override IEdmType Type
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
		public override IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		private IEdmPathExpression ComputePath()
		{
			List<string> list = new List<string>(this.parentNavigationSource.Path.Path);
			list.Add(this.navigationProperty.Name);
			return new EdmPathExpression(list.ToArray());
		}

		// Token: 0x040001F9 RID: 505
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x040001FA RID: 506
		private readonly IEdmNavigationSource parentNavigationSource;

		// Token: 0x040001FB RID: 507
		private IEdmPathExpression path;
	}
}

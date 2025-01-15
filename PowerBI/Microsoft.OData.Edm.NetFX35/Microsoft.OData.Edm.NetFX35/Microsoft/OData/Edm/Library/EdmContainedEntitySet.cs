using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000103 RID: 259
	internal class EdmContainedEntitySet : EdmEntitySetBase, IEdmContainedEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x0000D473 File Offset: 0x0000B673
		public EdmContainedEntitySet(IEdmNavigationSource parentNavigationSource, IEdmNavigationProperty navigationProperty)
			: base(navigationProperty.Name, navigationProperty.ToEntityType())
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(parentNavigationSource, "parentNavigationSource");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			this.parentNavigationSource = parentNavigationSource;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
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

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000D4D6 File Offset: 0x0000B6D6
		public IEdmNavigationSource ParentNavigationSource
		{
			get
			{
				return this.parentNavigationSource;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0000D4DE File Offset: 0x0000B6DE
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		public override IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			IEdmNavigationSource edmNavigationSource = base.FindNavigationTarget(property);
			if (edmNavigationSource is IEdmUnknownEntitySet)
			{
				return this.parentNavigationSource.FindNavigationTarget(property);
			}
			return edmNavigationSource;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000D514 File Offset: 0x0000B714
		private IEdmPathExpression ComputePath()
		{
			List<string> list = new List<string>(this.parentNavigationSource.Path.Path);
			list.Add(this.navigationProperty.Name);
			return new EdmPathExpression(list.ToArray());
		}

		// Token: 0x040001EB RID: 491
		private readonly IEdmNavigationSource parentNavigationSource;

		// Token: 0x040001EC RID: 492
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x040001ED RID: 493
		private IEdmPathExpression path;
	}
}

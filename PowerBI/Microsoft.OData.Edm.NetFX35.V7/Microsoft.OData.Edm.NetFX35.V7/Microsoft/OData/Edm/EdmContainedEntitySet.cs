using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004E RID: 78
	internal class EdmContainedEntitySet : EdmEntitySetBase, IEdmContainedEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x060002EB RID: 747 RVA: 0x00009D50 File Offset: 0x00007F50
		public EdmContainedEntitySet(IEdmNavigationSource parentNavigationSource, IEdmNavigationProperty navigationProperty)
			: base(navigationProperty.Name, navigationProperty.ToEntityType())
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(parentNavigationSource, "parentNavigationSource");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			this.parentNavigationSource = parentNavigationSource;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00009D8C File Offset: 0x00007F8C
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009DB2 File Offset: 0x00007FB2
		public IEdmNavigationSource ParentNavigationSource
		{
			get
			{
				return this.parentNavigationSource;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00009DBA File Offset: 0x00007FBA
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00009DC2 File Offset: 0x00007FC2
		public override IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			return this.parentNavigationSource.FindNavigationPropertyBindings(navigationProperty);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00009DD0 File Offset: 0x00007FD0
		public override IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty)
		{
			IEdmPathExpression edmPathExpression = ((!this.Type.AsElementType().IsOrInheritsFrom(navigationProperty.DeclaringType)) ? new EdmPathExpression(new string[]
			{
				base.Name,
				navigationProperty.DeclaringType.FullTypeName(),
				navigationProperty.Name
			}) : new EdmPathExpression(new string[] { base.Name, navigationProperty.Name }));
			return this.FindNavigationTarget(navigationProperty, edmPathExpression);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009E50 File Offset: 0x00008050
		public override IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property, IEdmPathExpression bindingPath)
		{
			IEdmNavigationSource edmNavigationSource = base.FindNavigationTarget(property, bindingPath);
			if (edmNavigationSource is IEdmUnknownEntitySet)
			{
				return this.parentNavigationSource.FindNavigationTarget(property, bindingPath);
			}
			return edmNavigationSource;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00009E80 File Offset: 0x00008080
		private IEdmPathExpression ComputePath()
		{
			List<string> list = new List<string>(this.parentNavigationSource.Path.PathSegments);
			list.Add(this.navigationProperty.Name);
			return new EdmPathExpression(list.ToArray());
		}

		// Token: 0x0400009B RID: 155
		private readonly IEdmNavigationSource parentNavigationSource;

		// Token: 0x0400009C RID: 156
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x0400009D RID: 157
		private IEdmPathExpression path;
	}
}

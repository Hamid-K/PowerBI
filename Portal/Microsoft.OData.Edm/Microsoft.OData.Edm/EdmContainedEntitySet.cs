using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000037 RID: 55
	internal class EdmContainedEntitySet : EdmEntitySetBase, IEdmContainedEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00003CE2 File Offset: 0x00001EE2
		public EdmContainedEntitySet(IEdmNavigationSource parentNavigationSource, IEdmNavigationProperty navigationProperty)
			: this(parentNavigationSource, navigationProperty, new EdmPathExpression(navigationProperty.Name))
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public EdmContainedEntitySet(IEdmNavigationSource parentNavigationSource, IEdmNavigationProperty navigationProperty, IEdmPathExpression navigationPath)
			: base(navigationProperty.Name, navigationProperty.ToEntityType())
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(parentNavigationSource, "parentNavigationSource");
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			this.parentNavigationSource = parentNavigationSource;
			this.navigationProperty = navigationProperty;
			this.navigationPath = navigationPath;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003D44 File Offset: 0x00001F44
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

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00003D6A File Offset: 0x00001F6A
		public IEdmNavigationSource ParentNavigationSource
		{
			get
			{
				return this.parentNavigationSource;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003D72 File Offset: 0x00001F72
		public IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00003D7A File Offset: 0x00001F7A
		internal IEdmPathExpression NavigationPath
		{
			get
			{
				return this.navigationPath;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003D84 File Offset: 0x00001F84
		private string FullNavigationPath
		{
			get
			{
				if (this.fullPath == null)
				{
					List<string> list = new List<string>();
					for (EdmContainedEntitySet edmContainedEntitySet = this; edmContainedEntitySet != null; edmContainedEntitySet = edmContainedEntitySet.ParentNavigationSource as EdmContainedEntitySet)
					{
						list.AddRange(edmContainedEntitySet.NavigationPath.PathSegments);
					}
					list.Reverse();
					this.fullPath = new EdmPathExpression(list).Path;
				}
				return this.fullPath;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public override IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmNavigationPropertyBinding> enumerable = base.FindNavigationPropertyBindings(navigationProperty);
			IEdmNavigationSource edmNavigationSource;
			for (IEdmContainedEntitySet edmContainedEntitySet = this; edmContainedEntitySet != null; edmContainedEntitySet = edmNavigationSource as IEdmContainedEntitySet)
			{
				edmNavigationSource = edmContainedEntitySet.ParentNavigationSource;
				IEnumerable<IEdmNavigationPropertyBinding> enumerable2 = edmNavigationSource.FindNavigationPropertyBindings(navigationProperty);
				if (enumerable2 != null)
				{
					enumerable = ((enumerable == null) ? enumerable2 : enumerable.Concat(enumerable2));
				}
			}
			return enumerable;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003E24 File Offset: 0x00002024
		public override IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty)
		{
			return this.FindNavigationTarget(navigationProperty, new EdmPathExpression(navigationProperty.Name));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003E38 File Offset: 0x00002038
		public override IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			if (bindingPath != null && bindingPath.Path.Length > this.FullNavigationPath.Length && bindingPath.Path.StartsWith(this.FullNavigationPath, StringComparison.Ordinal))
			{
				bindingPath = new EdmPathExpression(bindingPath.Path.Substring(this.FullNavigationPath.Length + 1));
			}
			IEdmNavigationSource edmNavigationSource = base.FindNavigationTarget(navigationProperty, bindingPath);
			if (edmNavigationSource is IEdmUnknownEntitySet)
			{
				IEnumerable<string> enumerable;
				if (bindingPath != null && !string.IsNullOrEmpty(bindingPath.Path))
				{
					enumerable = bindingPath.PathSegments;
				}
				else
				{
					IEnumerable<string> enumerable2 = new string[] { navigationProperty.Name };
					enumerable = enumerable2;
				}
				IEnumerable<string> enumerable3 = enumerable;
				bindingPath = new EdmPathExpression(this.NavigationPath.PathSegments.Concat(enumerable3));
				return this.parentNavigationSource.FindNavigationTarget(navigationProperty, bindingPath);
			}
			return edmNavigationSource;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003EF8 File Offset: 0x000020F8
		private IEdmPathExpression ComputePath()
		{
			IEdmType edmType = this.navigationProperty.DeclaringType.AsElementType();
			List<string> list = new List<string>(this.parentNavigationSource.Path.PathSegments);
			if (!(edmType is IEdmComplexType) && !this.parentNavigationSource.Type.AsElementType().IsOrInheritsFrom(edmType))
			{
				list.Add(edmType.FullTypeName());
			}
			list.AddRange(this.NavigationPath.PathSegments);
			return new EdmPathExpression(list.ToArray());
		}

		// Token: 0x0400005E RID: 94
		private readonly IEdmPathExpression navigationPath;

		// Token: 0x0400005F RID: 95
		private readonly IEdmNavigationSource parentNavigationSource;

		// Token: 0x04000060 RID: 96
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x04000061 RID: 97
		private IEdmPathExpression path;

		// Token: 0x04000062 RID: 98
		private string fullPath;
	}
}

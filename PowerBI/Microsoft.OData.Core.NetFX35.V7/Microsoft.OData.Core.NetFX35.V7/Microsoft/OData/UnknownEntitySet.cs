using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000CF RID: 207
	internal class UnknownEntitySet : IEdmUnknownEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x060007DB RID: 2011 RVA: 0x00015D9C File Offset: 0x00013F9C
		public UnknownEntitySet(IEdmNavigationSource parentNavigationSource, IEdmNavigationProperty navigationProperty)
		{
			this.parentNavigationSource = parentNavigationSource;
			this.navigationProperty = navigationProperty;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00015DB2 File Offset: 0x00013FB2
		public string Name
		{
			get
			{
				return this.navigationProperty.Name;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0000B41B File Offset: 0x0000961B
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00015DC0 File Offset: 0x00013FC0
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

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00015DE6 File Offset: 0x00013FE6
		public IEdmType Type
		{
			get
			{
				return this.navigationProperty.Type.Definition;
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0000B41B File Offset: 0x0000961B
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty)
		{
			return null;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0000B41B File Offset: 0x0000961B
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			return null;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0000B41B File Offset: 0x0000961B
		public IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			return null;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00015DF8 File Offset: 0x00013FF8
		private IEdmPathExpression ComputePath()
		{
			List<string> list = new List<string>(this.parentNavigationSource.Path.PathSegments);
			list.Add(this.navigationProperty.Name);
			return new EdmPathExpression(list.ToArray());
		}

		// Token: 0x04000330 RID: 816
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x04000331 RID: 817
		private readonly IEdmNavigationSource parentNavigationSource;

		// Token: 0x04000332 RID: 818
		private IEdmPathExpression path;
	}
}

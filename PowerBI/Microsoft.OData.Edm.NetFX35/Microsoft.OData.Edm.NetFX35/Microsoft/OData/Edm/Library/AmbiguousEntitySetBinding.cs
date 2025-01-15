using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200011A RID: 282
	internal class AmbiguousEntitySetBinding : AmbiguousBinding<IEdmEntitySet>, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000593 RID: 1427 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		public AmbiguousEntitySetBinding(IEdmEntitySet first, IEdmEntitySet second)
			: base(first, second)
		{
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000DDD6 File Offset: 0x0000BFD6
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public IEdmEntityContainer Container
		{
			get
			{
				IEdmEntitySet edmEntitySet = Enumerable.FirstOrDefault<IEdmEntitySet>(base.Bindings);
				if (edmEntitySet == null)
				{
					return null;
				}
				return edmEntitySet.Container;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000DE00 File Offset: 0x0000C000
		public IEdmPathExpression Path
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000DE03 File Offset: 0x0000C003
		public IEdmType Type
		{
			get
			{
				return new EdmCollectionType(new EdmEntityTypeReference(new BadEntityType(string.Empty, base.Errors), false));
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000DE20 File Offset: 0x0000C020
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return Enumerable.Empty<IEdmNavigationPropertyBinding>();
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000DE27 File Offset: 0x0000C027
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}
	}
}

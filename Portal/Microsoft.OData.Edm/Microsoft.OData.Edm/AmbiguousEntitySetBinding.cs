using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004B RID: 75
	internal class AmbiguousEntitySetBinding : AmbiguousBinding<IEdmEntitySet>, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement, IEdmEntityContainerElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00004B15 File Offset: 0x00002D15
		public AmbiguousEntitySetBinding(IEdmEntitySet first, IEdmEntitySet second)
			: base(first, second)
		{
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00004B20 File Offset: 0x00002D20
		public IEdmEntityContainer Container
		{
			get
			{
				IEdmEntitySet edmEntitySet = base.Bindings.FirstOrDefault<IEdmEntitySet>();
				if (edmEntitySet == null)
				{
					return null;
				}
				return edmEntitySet.Container;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmPathExpression Path
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00004B44 File Offset: 0x00002D44
		public IEdmType Type
		{
			get
			{
				return new EdmCollectionType(new EdmEntityTypeReference(new BadEntityType(string.Empty, base.Errors), false));
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000268E File Offset: 0x0000088E
		public bool IncludeInServiceDocument
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00003A24 File Offset: 0x00001C24
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return Enumerable.Empty<IEdmNavigationPropertyBinding>();
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			return null;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			return null;
		}
	}
}

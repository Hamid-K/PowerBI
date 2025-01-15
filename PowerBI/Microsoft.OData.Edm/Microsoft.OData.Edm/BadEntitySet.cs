using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005A RID: 90
	internal class BadEntitySet : BadElement, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement, IEdmEntityContainerElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00004FCE File Offset: 0x000031CE
		public BadEntitySet(string name, IEdmEntityContainer container, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.container = container;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004FEE File Offset: 0x000031EE
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00004FF6 File Offset: 0x000031F6
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00003A24 File Offset: 0x00001C24
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return Enumerable.Empty<IEdmNavigationPropertyBinding>();
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmPathExpression Path
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00004B44 File Offset: 0x00002D44
		public IEdmType Type
		{
			get
			{
				return new EdmCollectionType(new EdmEntityTypeReference(new BadEntityType(string.Empty, base.Errors), false));
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000268E File Offset: 0x0000088E
		public bool IncludeInServiceDocument
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			return null;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			return null;
		}

		// Token: 0x040000A9 RID: 169
		private readonly string name;

		// Token: 0x040000AA RID: 170
		private readonly IEdmEntityContainer container;
	}
}

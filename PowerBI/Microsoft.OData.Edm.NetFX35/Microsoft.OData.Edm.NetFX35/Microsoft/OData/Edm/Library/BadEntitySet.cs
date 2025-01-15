using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000135 RID: 309
	internal class BadEntitySet : BadElement, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x0000E3AB File Offset: 0x0000C5AB
		public BadEntitySet(string name, IEdmEntityContainer container, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.container = container;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x0000E3CB File Offset: 0x0000C5CB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000E3D3 File Offset: 0x0000C5D3
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0000E3D6 File Offset: 0x0000C5D6
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0000E3DE File Offset: 0x0000C5DE
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return Enumerable.Empty<IEdmNavigationPropertyBinding>();
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0000E3E5 File Offset: 0x0000C5E5
		public IEdmPathExpression Path
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		public IEdmType Type
		{
			get
			{
				return new EdmCollectionType(new EdmEntityTypeReference(new BadEntityType(string.Empty, base.Errors), false));
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000E405 File Offset: 0x0000C605
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x04000239 RID: 569
		private readonly string name;

		// Token: 0x0400023A RID: 570
		private readonly IEdmEntityContainer container;
	}
}

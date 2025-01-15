using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000031 RID: 49
	internal class BadEntitySet : BadElement, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement, IEdmEntityContainerElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00009220 File Offset: 0x00007420
		public BadEntitySet(string name, IEdmEntityContainer container, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.name = name ?? string.Empty;
			this.container = container;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00009240 File Offset: 0x00007440
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00009248 File Offset: 0x00007448
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00008DBD File Offset: 0x00006FBD
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return Enumerable.Empty<IEdmNavigationPropertyBinding>();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmPathExpression Path
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00008DA0 File Offset: 0x00006FA0
		public IEdmType Type
		{
			get
			{
				return new EdmCollectionType(new EdmEntityTypeReference(new BadEntityType(string.Empty, base.Errors), false));
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00008D76 File Offset: 0x00006F76
		public bool IncludeInServiceDocument
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			return null;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			return null;
		}

		// Token: 0x0400004E RID: 78
		private readonly string name;

		// Token: 0x0400004F RID: 79
		private readonly IEdmEntityContainer container;
	}
}

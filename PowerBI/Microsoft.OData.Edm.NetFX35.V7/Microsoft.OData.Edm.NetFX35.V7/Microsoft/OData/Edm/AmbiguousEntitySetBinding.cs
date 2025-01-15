using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000020 RID: 32
	internal class AmbiguousEntitySetBinding : AmbiguousBinding<IEdmEntitySet>, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement, IEdmEntityContainerElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00008D6C File Offset: 0x00006F6C
		public AmbiguousEntitySetBinding(IEdmEntitySet first, IEdmEntitySet second)
			: base(first, second)
		{
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00008D7C File Offset: 0x00006F7C
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmPathExpression Path
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00008DA0 File Offset: 0x00006FA0
		public IEdmType Type
		{
			get
			{
				return new EdmCollectionType(new EdmEntityTypeReference(new BadEntityType(string.Empty, base.Errors), false));
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008D76 File Offset: 0x00006F76
		public bool IncludeInServiceDocument
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00008DBD File Offset: 0x00006FBD
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return Enumerable.Empty<IEdmNavigationPropertyBinding>();
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			return null;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			return null;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			return null;
		}
	}
}

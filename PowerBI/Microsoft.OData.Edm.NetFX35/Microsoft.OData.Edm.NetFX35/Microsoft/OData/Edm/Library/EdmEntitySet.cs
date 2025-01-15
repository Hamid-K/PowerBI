using System;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001FE RID: 510
	public class EdmEntitySet : EdmEntitySetBase, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000BF6 RID: 3062 RVA: 0x00021E3C File Offset: 0x0002003C
		public EdmEntitySet(IEdmEntityContainer container, string name, IEdmEntityType elementType)
			: base(name, elementType)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			this.container = container;
			this.type = new EdmCollectionType(new EdmEntityTypeReference(elementType, false));
			this.path = new EdmPathExpression(this.container.FullName() + "." + base.Name);
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00021E9C File Offset: 0x0002009C
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00021E9F File Offset: 0x0002009F
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00021EA7 File Offset: 0x000200A7
		public override IEdmType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00021EAF File Offset: 0x000200AF
		public override IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x0400057A RID: 1402
		private readonly IEdmEntityContainer container;

		// Token: 0x0400057B RID: 1403
		private readonly IEdmCollectionType type;

		// Token: 0x0400057C RID: 1404
		private IEdmPathExpression path;
	}
}

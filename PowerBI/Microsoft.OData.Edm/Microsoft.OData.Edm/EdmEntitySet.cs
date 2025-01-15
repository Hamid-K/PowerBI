using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B5 RID: 181
	public class EdmEntitySet : EdmEntitySetBase, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement, IEdmEntityContainerElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x0000AF0C File Offset: 0x0000910C
		public EdmEntitySet(IEdmEntityContainer container, string name, IEdmEntityType elementType)
			: this(container, name, elementType, true)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			this.container = container;
			this.type = new EdmCollectionType(new EdmEntityTypeReference(elementType, false));
			this.path = new EdmPathExpression(this.container.FullName() + "." + base.Name);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000AF6E File Offset: 0x0000916E
		public EdmEntitySet(IEdmEntityContainer container, string name, IEdmEntityType elementType, bool includeInServiceDocument)
			: base(name, elementType)
		{
			this.includeInServiceDocument = includeInServiceDocument;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000AF80 File Offset: 0x00009180
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000AF88 File Offset: 0x00009188
		public override IEdmType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000AF90 File Offset: 0x00009190
		public override IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000AF98 File Offset: 0x00009198
		public bool IncludeInServiceDocument
		{
			get
			{
				return this.includeInServiceDocument;
			}
		}

		// Token: 0x04000149 RID: 329
		private readonly IEdmEntityContainer container;

		// Token: 0x0400014A RID: 330
		private readonly IEdmCollectionType type;

		// Token: 0x0400014B RID: 331
		private IEdmPathExpression path;

		// Token: 0x0400014C RID: 332
		private bool includeInServiceDocument;
	}
}

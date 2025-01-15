using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000057 RID: 87
	public class EdmEntitySet : EdmEntitySetBase, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement, IEdmEntityContainerElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000AA70 File Offset: 0x00008C70
		public EdmEntitySet(IEdmEntityContainer container, string name, IEdmEntityType elementType)
			: this(container, name, elementType, true)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			this.container = container;
			this.type = new EdmCollectionType(new EdmEntityTypeReference(elementType, false));
			this.path = new EdmPathExpression(this.container.FullName() + "." + base.Name);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000AAD2 File Offset: 0x00008CD2
		public EdmEntitySet(IEdmEntityContainer container, string name, IEdmEntityType elementType, bool includeInServiceDocument)
			: base(name, elementType)
		{
			this.includeInServiceDocument = includeInServiceDocument;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000AAEC File Offset: 0x00008CEC
		public override IEdmType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		public override IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000AAFC File Offset: 0x00008CFC
		public bool IncludeInServiceDocument
		{
			get
			{
				return this.includeInServiceDocument;
			}
		}

		// Token: 0x040000B1 RID: 177
		private readonly IEdmEntityContainer container;

		// Token: 0x040000B2 RID: 178
		private readonly IEdmCollectionType type;

		// Token: 0x040000B3 RID: 179
		private IEdmPathExpression path;

		// Token: 0x040000B4 RID: 180
		private bool includeInServiceDocument;
	}
}

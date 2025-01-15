using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018C RID: 396
	internal class CsdlSemanticsEntitySet : CsdlSemanticsNavigationSource, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement, IEdmEntityContainerElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000A94 RID: 2708 RVA: 0x0001B524 File Offset: 0x00019724
		public CsdlSemanticsEntitySet(CsdlSemanticsEntityContainer container, CsdlEntitySet entitySet)
			: base(container, entitySet)
		{
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0001C972 File Offset: 0x0001AB72
		public override IEdmType Type
		{
			get
			{
				return new EdmCollectionType(new EdmEntityTypeReference(this.typeCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeElementTypeFunc, null), false));
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x00008D76 File Offset: 0x00006F76
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0001C991 File Offset: 0x0001AB91
		public bool IncludeInServiceDocument
		{
			get
			{
				return ((CsdlEntitySet)this.navigationSource).IncludeInServiceDocument;
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		protected override IEdmEntityType ComputeElementType()
		{
			string elementType = ((CsdlEntitySet)this.navigationSource).ElementType;
			return (this.container.Context.FindType(elementType) as IEdmEntityType) ?? new UnresolvedEntityType(elementType, base.Location);
		}
	}
}

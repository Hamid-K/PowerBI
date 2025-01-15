using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019B RID: 411
	internal class CsdlSemanticsEntitySet : CsdlSemanticsNavigationSource, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmNamedElement, IEdmElement, IEdmEntityContainerElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000B57 RID: 2903 RVA: 0x0001D62C File Offset: 0x0001B82C
		public CsdlSemanticsEntitySet(CsdlSemanticsEntityContainer container, CsdlEntitySet entitySet)
			: base(container, entitySet)
		{
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0001ECB2 File Offset: 0x0001CEB2
		public override IEdmType Type
		{
			get
			{
				return new EdmCollectionType(new EdmEntityTypeReference(this.typeCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeElementTypeFunc, null), false));
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0000268E File Offset: 0x0000088E
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0001ECD1 File Offset: 0x0001CED1
		public bool IncludeInServiceDocument
		{
			get
			{
				return ((CsdlEntitySet)this.navigationSource).IncludeInServiceDocument;
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001ECE4 File Offset: 0x0001CEE4
		protected override IEdmEntityType ComputeElementType()
		{
			string elementType = ((CsdlEntitySet)this.navigationSource).ElementType;
			return (this.container.Context.FindType(elementType) as IEdmEntityType) ?? new UnresolvedEntityType(elementType, base.Location);
		}
	}
}

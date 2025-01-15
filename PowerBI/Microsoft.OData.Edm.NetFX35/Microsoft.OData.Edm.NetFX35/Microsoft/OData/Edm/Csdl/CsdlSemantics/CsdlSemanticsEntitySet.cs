using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AB RID: 427
	internal class CsdlSemanticsEntitySet : CsdlSemanticsNavigationSource, IEdmEntitySet, IEdmEntitySetBase, IEdmNavigationSource, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x00016C11 File Offset: 0x00014E11
		public CsdlSemanticsEntitySet(CsdlSemanticsEntityContainer container, CsdlEntitySet entitySet)
			: base(container, entitySet)
		{
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00016C1B File Offset: 0x00014E1B
		public override IEdmType Type
		{
			get
			{
				return new EdmCollectionType(new EdmEntityTypeReference(this.typeCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeElementTypeFunc, null), false));
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00016C3A File Offset: 0x00014E3A
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.EntitySet;
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00016C40 File Offset: 0x00014E40
		protected override IEdmEntityType ComputeElementType()
		{
			string elementType = ((CsdlEntitySet)this.navigationSource).ElementType;
			return (this.container.Context.FindType(elementType) as IEdmEntityType) ?? new UnresolvedEntityType(elementType, base.Location);
		}
	}
}

using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018D RID: 397
	internal class CsdlSemanticsSingleton : CsdlSemanticsNavigationSource, IEdmSingleton, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmNavigationSource
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001D62C File Offset: 0x0001B82C
		public CsdlSemanticsSingleton(CsdlSemanticsEntityContainer container, CsdlSingleton singleton)
			: base(container, singleton)
		{
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0001D636 File Offset: 0x0001B836
		public override IEdmType Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeElementTypeFunc, null);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x000039FB File Offset: 0x00001BFB
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.Singleton;
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001D64C File Offset: 0x0001B84C
		protected override IEdmEntityType ComputeElementType()
		{
			string type = ((CsdlSingleton)this.navigationSource).Type;
			return (this.container.Context.FindType(type) as IEdmEntityType) ?? new UnresolvedEntityType(type, base.Location);
		}
	}
}

using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000B9 RID: 185
	internal class CsdlSemanticsSingleton : CsdlSemanticsNavigationSource, IEdmSingleton, IEdmEntityContainerElement, IEdmVocabularyAnnotatable, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000325 RID: 805 RVA: 0x000073C5 File Offset: 0x000055C5
		public CsdlSemanticsSingleton(CsdlSemanticsEntityContainer container, CsdlSingleton singleton)
			: base(container, singleton)
		{
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000073CF File Offset: 0x000055CF
		public override IEdmType Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeElementTypeFunc, null);
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000073E3 File Offset: 0x000055E3
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.Singleton;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000073E8 File Offset: 0x000055E8
		protected override IEdmEntityType ComputeElementType()
		{
			string type = ((CsdlSingleton)this.navigationSource).Type;
			return (this.container.Context.FindType(type) as IEdmEntityType) ?? new UnresolvedEntityType(type, base.Location);
		}
	}
}

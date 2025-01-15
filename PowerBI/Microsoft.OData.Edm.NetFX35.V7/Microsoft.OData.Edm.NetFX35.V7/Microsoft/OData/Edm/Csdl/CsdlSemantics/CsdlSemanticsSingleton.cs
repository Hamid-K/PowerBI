using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017E RID: 382
	internal class CsdlSemanticsSingleton : CsdlSemanticsNavigationSource, IEdmSingleton, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmNavigationSource
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x0001B524 File Offset: 0x00019724
		public CsdlSemanticsSingleton(CsdlSemanticsEntityContainer container, CsdlSingleton singleton)
			: base(container, singleton)
		{
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0001B52E File Offset: 0x0001972E
		public override IEdmType Type
		{
			get
			{
				return this.typeCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeElementTypeFunc, null);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00008D57 File Offset: 0x00006F57
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.Singleton;
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001B544 File Offset: 0x00019744
		protected override IEdmEntityType ComputeElementType()
		{
			string type = ((CsdlSingleton)this.navigationSource).Type;
			return (this.container.Context.FindType(type) as IEdmEntityType) ?? new UnresolvedEntityType(type, base.Location);
		}
	}
}

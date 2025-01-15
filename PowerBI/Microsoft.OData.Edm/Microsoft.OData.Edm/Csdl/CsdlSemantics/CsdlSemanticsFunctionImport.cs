using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000174 RID: 372
	internal class CsdlSemanticsFunctionImport : CsdlSemanticsOperationImport, IEdmFunctionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x0001BE8E File Offset: 0x0001A08E
		public CsdlSemanticsFunctionImport(CsdlSemanticsEntityContainer container, CsdlFunctionImport functionImport, IEdmFunction backingfunction)
			: base(container, functionImport, backingfunction)
		{
			this.csdlSchema = container.Context;
			this.functionImport = functionImport;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0001BEAC File Offset: 0x0001A0AC
		public IEdmFunction Function
		{
			get
			{
				return (IEdmFunction)base.Operation;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0001BEB9 File Offset: 0x0001A0B9
		public bool IncludeInServiceDocument
		{
			get
			{
				return this.functionImport.IncludeInServiceDocument;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0000268B File Offset: 0x0000088B
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.FunctionImport;
			}
		}

		// Token: 0x0400060F RID: 1551
		private readonly CsdlFunctionImport functionImport;

		// Token: 0x04000610 RID: 1552
		private readonly CsdlSemanticsSchema csdlSchema;
	}
}

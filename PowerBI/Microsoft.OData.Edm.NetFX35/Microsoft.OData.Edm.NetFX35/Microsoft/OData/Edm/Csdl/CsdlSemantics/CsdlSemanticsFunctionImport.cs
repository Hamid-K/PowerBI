using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200007F RID: 127
	internal class CsdlSemanticsFunctionImport : CsdlSemanticsOperationImport, IEdmFunctionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000202 RID: 514 RVA: 0x000058A8 File Offset: 0x00003AA8
		public CsdlSemanticsFunctionImport(CsdlSemanticsEntityContainer container, CsdlFunctionImport functionImport, IEdmFunction backingfunction)
			: base(container, functionImport, backingfunction)
		{
			this.csdlSchema = container.Context;
			this.functionImport = functionImport;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000058C6 File Offset: 0x00003AC6
		public IEdmFunction Function
		{
			get
			{
				return (IEdmFunction)base.Operation;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000058D3 File Offset: 0x00003AD3
		public bool IncludeInServiceDocument
		{
			get
			{
				return this.functionImport.IncludeInServiceDocument;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000058E0 File Offset: 0x00003AE0
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.FunctionImport;
			}
		}

		// Token: 0x040000BA RID: 186
		private readonly CsdlFunctionImport functionImport;

		// Token: 0x040000BB RID: 187
		private readonly CsdlSemanticsSchema csdlSchema;
	}
}

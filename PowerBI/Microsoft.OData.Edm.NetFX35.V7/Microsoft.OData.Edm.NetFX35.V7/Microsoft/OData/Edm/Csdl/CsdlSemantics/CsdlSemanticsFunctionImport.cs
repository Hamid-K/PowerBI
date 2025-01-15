using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000165 RID: 357
	internal class CsdlSemanticsFunctionImport : CsdlSemanticsOperationImport, IEdmFunctionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x00019D8E File Offset: 0x00017F8E
		public CsdlSemanticsFunctionImport(CsdlSemanticsEntityContainer container, CsdlFunctionImport functionImport, IEdmFunction backingfunction)
			: base(container, functionImport, backingfunction)
		{
			this.csdlSchema = container.Context;
			this.functionImport = functionImport;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00019DAC File Offset: 0x00017FAC
		public IEdmFunction Function
		{
			get
			{
				return (IEdmFunction)base.Operation;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00019DB9 File Offset: 0x00017FB9
		public bool IncludeInServiceDocument
		{
			get
			{
				return this.functionImport.IncludeInServiceDocument;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00009097 File Offset: 0x00007297
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.FunctionImport;
			}
		}

		// Token: 0x04000594 RID: 1428
		private readonly CsdlFunctionImport functionImport;

		// Token: 0x04000595 RID: 1429
		private readonly CsdlSemanticsSchema csdlSchema;
	}
}

using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200006A RID: 106
	internal class CsdlSemanticsActionImport : CsdlSemanticsOperationImport, IEdmActionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000198 RID: 408 RVA: 0x000042F5 File Offset: 0x000024F5
		public CsdlSemanticsActionImport(CsdlSemanticsEntityContainer container, CsdlActionImport actionImport, IEdmAction backingAction)
			: base(container, actionImport, backingAction)
		{
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00004300 File Offset: 0x00002500
		public IEdmAction Action
		{
			get
			{
				return (IEdmAction)base.Operation;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000430D File Offset: 0x0000250D
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.ActionImport;
			}
		}
	}
}

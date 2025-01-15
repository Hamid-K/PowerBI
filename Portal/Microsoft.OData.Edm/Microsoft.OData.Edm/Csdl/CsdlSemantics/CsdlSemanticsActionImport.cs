using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016B RID: 363
	internal class CsdlSemanticsActionImport : CsdlSemanticsOperationImport, IEdmActionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x0001B6B9 File Offset: 0x000198B9
		public CsdlSemanticsActionImport(CsdlSemanticsEntityContainer container, CsdlActionImport actionImport, IEdmAction backingAction)
			: base(container, actionImport, backingAction)
		{
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0001B6C4 File Offset: 0x000198C4
		public IEdmAction Action
		{
			get
			{
				return (IEdmAction)base.Operation;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00002732 File Offset: 0x00000932
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.ActionImport;
			}
		}
	}
}

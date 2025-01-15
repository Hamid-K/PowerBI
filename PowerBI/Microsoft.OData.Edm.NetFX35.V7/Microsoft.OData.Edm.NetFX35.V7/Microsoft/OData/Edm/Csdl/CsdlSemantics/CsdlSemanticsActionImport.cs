using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200015C RID: 348
	internal class CsdlSemanticsActionImport : CsdlSemanticsOperationImport, IEdmActionImport, IEdmOperationImport, IEdmEntityContainerElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x000195B9 File Offset: 0x000177B9
		public CsdlSemanticsActionImport(CsdlSemanticsEntityContainer container, CsdlActionImport actionImport, IEdmAction backingAction)
			: base(container, actionImport, backingAction)
		{
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x000195C4 File Offset: 0x000177C4
		public IEdmAction Action
		{
			get
			{
				return (IEdmAction)base.Operation;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00008F68 File Offset: 0x00007168
		public override EdmContainerElementKind ContainerElementKind
		{
			get
			{
				return EdmContainerElementKind.ActionImport;
			}
		}
	}
}

using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000173 RID: 371
	internal class CsdlSemanticsFunction : CsdlSemanticsOperation, IEdmFunction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x0001BE70 File Offset: 0x0001A070
		public CsdlSemanticsFunction(CsdlSemanticsSchema context, CsdlFunction function)
			: base(context, function)
		{
			this.function = function;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0001BE81 File Offset: 0x0001A081
		public bool IsComposable
		{
			get
			{
				return this.function.IsComposable;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0000480B File Offset: 0x00002A0B
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}

		// Token: 0x0400060E RID: 1550
		private readonly CsdlFunction function;
	}
}

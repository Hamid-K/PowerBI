using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000164 RID: 356
	internal class CsdlSemanticsFunction : CsdlSemanticsOperation, IEdmFunction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x00019D70 File Offset: 0x00017F70
		public CsdlSemanticsFunction(CsdlSemanticsSchema context, CsdlFunction function)
			: base(context, function)
		{
			this.function = function;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00019D81 File Offset: 0x00017F81
		public bool IsComposable
		{
			get
			{
				return this.function.IsComposable;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00009215 File Offset: 0x00007415
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}

		// Token: 0x04000593 RID: 1427
		private readonly CsdlFunction function;
	}
}

using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200007D RID: 125
	internal class CsdlSemanticsFunction : CsdlSemanticsOperation, IEdmFunction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00005887 File Offset: 0x00003A87
		public CsdlSemanticsFunction(CsdlSemanticsSchema context, CsdlFunction function)
			: base(context, function)
		{
			this.function = function;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00005898 File Offset: 0x00003A98
		public bool IsComposable
		{
			get
			{
				return this.function.IsComposable;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000058A5 File Offset: 0x00003AA5
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}

		// Token: 0x040000B9 RID: 185
		private readonly CsdlFunction function;
	}
}

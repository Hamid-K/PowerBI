using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019E RID: 414
	internal abstract class CsdlSemanticsTypeDefinition : CsdlSemanticsElement, IEdmType, IEdmElement
	{
		// Token: 0x06000B4B RID: 2891 RVA: 0x0001F69A File Offset: 0x0001D89A
		protected CsdlSemanticsTypeDefinition(CsdlElement element)
			: base(element)
		{
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000B4C RID: 2892
		public abstract EdmTypeKind TypeKind { get; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		public override string ToString()
		{
			return this.ToTraceString();
		}
	}
}

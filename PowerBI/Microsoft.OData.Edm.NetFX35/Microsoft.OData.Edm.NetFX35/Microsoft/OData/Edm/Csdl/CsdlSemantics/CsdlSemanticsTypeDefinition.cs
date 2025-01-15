using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200005B RID: 91
	internal abstract class CsdlSemanticsTypeDefinition : CsdlSemanticsElement, IEdmType, IEdmElement
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00003CC2 File Offset: 0x00001EC2
		protected CsdlSemanticsTypeDefinition(CsdlElement element)
			: base(element)
		{
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600014D RID: 333
		public abstract EdmTypeKind TypeKind { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00003CCB File Offset: 0x00001ECB
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00003CCE File Offset: 0x00001ECE
		public override string ToString()
		{
			return this.ToTraceString();
		}
	}
}

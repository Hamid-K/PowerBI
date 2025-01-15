using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AF RID: 431
	internal abstract class CsdlSemanticsTypeDefinition : CsdlSemanticsElement, IEdmType, IEdmElement
	{
		// Token: 0x06000C22 RID: 3106 RVA: 0x00021E26 File Offset: 0x00020026
		protected CsdlSemanticsTypeDefinition(CsdlElement element)
			: base(element)
		{
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000C23 RID: 3107
		public abstract EdmTypeKind TypeKind { get; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0000BE23 File Offset: 0x0000A023
		public override string ToString()
		{
			return this.ToTraceString();
		}
	}
}

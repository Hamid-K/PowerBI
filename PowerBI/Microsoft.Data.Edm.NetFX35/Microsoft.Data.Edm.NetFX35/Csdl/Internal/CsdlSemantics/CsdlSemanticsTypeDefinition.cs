using System;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000063 RID: 99
	internal abstract class CsdlSemanticsTypeDefinition : CsdlSemanticsElement, IEdmType, IEdmElement
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00004FAE File Offset: 0x000031AE
		protected CsdlSemanticsTypeDefinition(CsdlElement element)
			: base(element)
		{
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000198 RID: 408
		public abstract EdmTypeKind TypeKind { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00004FB7 File Offset: 0x000031B7
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004FBA File Offset: 0x000031BA
		public override string ToString()
		{
			return this.ToTraceString();
		}
	}
}

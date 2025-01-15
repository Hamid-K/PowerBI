using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020000FF RID: 255
	public class EdmTypeDefinitionReference : EdmTypeReference, IEdmTypeDefinitionReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x0000D29C File Offset: 0x0000B49C
		public EdmTypeDefinitionReference(IEdmTypeDefinition typeDefinition, bool isNullable)
			: base(typeDefinition, isNullable)
		{
		}
	}
}

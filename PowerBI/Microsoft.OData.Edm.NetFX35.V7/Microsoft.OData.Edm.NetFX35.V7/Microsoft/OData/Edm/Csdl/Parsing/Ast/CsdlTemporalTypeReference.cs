using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001FA RID: 506
	internal class CsdlTemporalTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000D47 RID: 3399 RVA: 0x000244E7 File Offset: 0x000226E7
		public CsdlTemporalTypeReference(EdmPrimitiveTypeKind kind, int? precision, string typeName, bool isNullable, CsdlLocation location)
			: base(kind, typeName, isNullable, location)
		{
			base.Precision = precision;
		}
	}
}

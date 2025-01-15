using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000207 RID: 519
	internal class CsdlTemporalTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000DF6 RID: 3574 RVA: 0x00026617 File Offset: 0x00024817
		public CsdlTemporalTypeReference(EdmPrimitiveTypeKind kind, int? precision, string typeName, bool isNullable, CsdlLocation location)
			: base(kind, typeName, isNullable, location)
		{
			base.Precision = precision;
		}
	}
}

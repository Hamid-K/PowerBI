using System;
using System.Collections.Generic;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x0200014A RID: 330
	internal class CsdlRowType : CsdlStructuredType, ICsdlTypeExpression
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x0000F83F File Offset: 0x0000DA3F
		public CsdlRowType(IEnumerable<CsdlProperty> properties, CsdlLocation location)
			: base(properties, null, location)
		{
		}
	}
}

using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001CE RID: 462
	internal class CsdlActionImport : CsdlOperationImport
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x000259FA File Offset: 0x00023BFA
		public CsdlActionImport(string name, string schemaOperationQualifiedTypeName, string entitySet, CsdlLocation location)
			: base(name, schemaOperationQualifiedTypeName, entitySet, new CsdlOperationParameter[0], null, location)
		{
		}
	}
}

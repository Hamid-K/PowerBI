using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001BF RID: 447
	internal class CsdlActionImport : CsdlOperationImport
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x0002382B File Offset: 0x00021A2B
		public CsdlActionImport(string name, string schemaOperationQualifiedTypeName, string entitySet, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, schemaOperationQualifiedTypeName, entitySet, new CsdlOperationParameter[0], null, documentation, location)
		{
		}
	}
}

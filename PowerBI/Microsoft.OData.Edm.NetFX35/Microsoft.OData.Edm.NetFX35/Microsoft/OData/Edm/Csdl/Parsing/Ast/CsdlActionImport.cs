using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000023 RID: 35
	internal class CsdlActionImport : CsdlOperationImport
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x0000353A File Offset: 0x0000173A
		public CsdlActionImport(string name, string schemaOperationQualifiedTypeName, string entitySet, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, schemaOperationQualifiedTypeName, entitySet, new CsdlOperationParameter[0], null, documentation, location)
		{
		}
	}
}

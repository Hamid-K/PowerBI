using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C6 RID: 454
	internal class CsdlFunctionImport : CsdlOperationImport
	{
		// Token: 0x06000C88 RID: 3208 RVA: 0x00023950 File Offset: 0x00021B50
		public CsdlFunctionImport(string name, string schemaOperationQualifiedTypeName, string entitySet, bool includeInServiceDocument, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, schemaOperationQualifiedTypeName, entitySet, new CsdlOperationParameter[0], null, documentation, location)
		{
			this.IncludeInServiceDocument = includeInServiceDocument;
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0002396E File Offset: 0x00021B6E
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x00023976 File Offset: 0x00021B76
		public bool IncludeInServiceDocument { get; private set; }
	}
}

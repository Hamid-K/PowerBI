using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200002B RID: 43
	internal class CsdlFunctionImport : CsdlOperationImport
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003687 File Offset: 0x00001887
		public CsdlFunctionImport(string name, string schemaOperationQualifiedTypeName, string entitySet, bool includeInServiceDocument, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, schemaOperationQualifiedTypeName, entitySet, new CsdlOperationParameter[0], null, documentation, location)
		{
			this.IncludeInServiceDocument = includeInServiceDocument;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000036A5 File Offset: 0x000018A5
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000036AD File Offset: 0x000018AD
		public bool IncludeInServiceDocument { get; private set; }
	}
}

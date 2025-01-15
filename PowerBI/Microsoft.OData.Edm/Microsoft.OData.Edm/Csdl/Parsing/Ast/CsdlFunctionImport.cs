using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D5 RID: 469
	internal class CsdlFunctionImport : CsdlOperationImport
	{
		// Token: 0x06000D3D RID: 3389 RVA: 0x00025B17 File Offset: 0x00023D17
		public CsdlFunctionImport(string name, string schemaOperationQualifiedTypeName, string entitySet, bool includeInServiceDocument, CsdlLocation location)
			: base(name, schemaOperationQualifiedTypeName, entitySet, new CsdlOperationParameter[0], null, location)
		{
			this.IncludeInServiceDocument = includeInServiceDocument;
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00025B33 File Offset: 0x00023D33
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00025B3B File Offset: 0x00023D3B
		public bool IncludeInServiceDocument { get; private set; }
	}
}

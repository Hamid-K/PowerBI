using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E8 RID: 488
	internal abstract class CsdlOperationImport : CsdlFunctionBase
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x00023FBA File Offset: 0x000221BA
		protected CsdlOperationImport(string name, string schemaOperationQualifiedTypeName, string entitySet, IEnumerable<CsdlOperationParameter> parameters, CsdlTypeReference returnType, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, parameters, returnType, documentation, location)
		{
			this.entitySet = entitySet;
			this.SchemaOperationQualifiedTypeName = schemaOperationQualifiedTypeName;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00023FD9 File Offset: 0x000221D9
		public string EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00023FE1 File Offset: 0x000221E1
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x00023FE9 File Offset: 0x000221E9
		public string SchemaOperationQualifiedTypeName { get; private set; }

		// Token: 0x04000709 RID: 1801
		private readonly string entitySet;
	}
}

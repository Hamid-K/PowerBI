using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F5 RID: 501
	internal abstract class CsdlOperationImport : CsdlFunctionBase
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x00026103 File Offset: 0x00024303
		protected CsdlOperationImport(string name, string schemaOperationQualifiedTypeName, string entitySet, IEnumerable<CsdlOperationParameter> parameters, CsdlOperationReturn returnType, CsdlLocation location)
			: base(name, parameters, returnType, location)
		{
			this.entitySet = entitySet;
			this.SchemaOperationQualifiedTypeName = schemaOperationQualifiedTypeName;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00026120 File Offset: 0x00024320
		public string EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00026128 File Offset: 0x00024328
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x00026130 File Offset: 0x00024330
		public string SchemaOperationQualifiedTypeName { get; private set; }

		// Token: 0x0400077F RID: 1919
		private readonly string entitySet;
	}
}

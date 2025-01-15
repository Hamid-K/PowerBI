using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000022 RID: 34
	internal abstract class CsdlOperationImport : CsdlFunctionBase
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00003502 File Offset: 0x00001702
		protected CsdlOperationImport(string name, string schemaOperationQualifiedTypeName, string entitySet, IEnumerable<CsdlOperationParameter> parameters, CsdlTypeReference returnType, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, parameters, returnType, documentation, location)
		{
			this.entitySet = entitySet;
			this.SchemaOperationQualifiedTypeName = schemaOperationQualifiedTypeName;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003521 File Offset: 0x00001721
		public string EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003529 File Offset: 0x00001729
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003531 File Offset: 0x00001731
		public string SchemaOperationQualifiedTypeName { get; private set; }

		// Token: 0x04000032 RID: 50
		private readonly string entitySet;
	}
}

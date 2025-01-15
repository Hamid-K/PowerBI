using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200001F RID: 31
	internal abstract class CsdlFunctionBase : CsdlNamedElement
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000347E File Offset: 0x0000167E
		protected CsdlFunctionBase(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlTypeReference returnType, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.parameters = new List<CsdlOperationParameter>(parameters);
			this.returnType = returnType;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000349E File Offset: 0x0000169E
		public IEnumerable<CsdlOperationParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000034A6 File Offset: 0x000016A6
		public CsdlTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x0400002E RID: 46
		private readonly List<CsdlOperationParameter> parameters;

		// Token: 0x0400002F RID: 47
		private readonly CsdlTypeReference returnType;
	}
}

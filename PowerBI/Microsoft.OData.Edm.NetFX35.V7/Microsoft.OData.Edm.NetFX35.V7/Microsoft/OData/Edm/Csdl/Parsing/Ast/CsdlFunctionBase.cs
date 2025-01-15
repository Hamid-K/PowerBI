using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E7 RID: 487
	internal abstract class CsdlFunctionBase : CsdlNamedElement
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x00023F8A File Offset: 0x0002218A
		protected CsdlFunctionBase(string name, IEnumerable<CsdlOperationParameter> parameters, CsdlTypeReference returnType, CsdlDocumentation documentation, CsdlLocation location)
			: base(name, documentation, location)
		{
			this.parameters = new List<CsdlOperationParameter>(parameters);
			this.returnType = returnType;
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00023FAA File Offset: 0x000221AA
		public IEnumerable<CsdlOperationParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00023FB2 File Offset: 0x000221B2
		public CsdlTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x04000707 RID: 1799
		private readonly List<CsdlOperationParameter> parameters;

		// Token: 0x04000708 RID: 1800
		private readonly CsdlTypeReference returnType;
	}
}

using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000038 RID: 56
	internal class CsdlPropertyValue : CsdlElement
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x000038DF File Offset: 0x00001ADF
		public CsdlPropertyValue(string property, CsdlExpressionBase expression, CsdlLocation location)
			: base(location)
		{
			this.property = property;
			this.expression = expression;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000038F6 File Offset: 0x00001AF6
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000038FE File Offset: 0x00001AFE
		public CsdlExpressionBase Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x04000055 RID: 85
		private readonly CsdlExpressionBase expression;

		// Token: 0x04000056 RID: 86
		private readonly string property;
	}
}

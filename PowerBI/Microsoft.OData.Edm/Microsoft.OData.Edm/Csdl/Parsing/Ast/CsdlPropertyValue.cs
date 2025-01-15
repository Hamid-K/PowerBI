using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E1 RID: 481
	internal class CsdlPropertyValue : CsdlElement
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x00025D2C File Offset: 0x00023F2C
		public CsdlPropertyValue(string property, CsdlExpressionBase expression, CsdlLocation location)
			: base(location)
		{
			this.property = property;
			this.expression = expression;
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00025D43 File Offset: 0x00023F43
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00025D4B File Offset: 0x00023F4B
		public CsdlExpressionBase Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x04000760 RID: 1888
		private readonly CsdlExpressionBase expression;

		// Token: 0x04000761 RID: 1889
		private readonly string property;
	}
}

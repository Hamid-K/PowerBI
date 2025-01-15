using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D2 RID: 466
	internal class CsdlPropertyValue : CsdlElement
	{
		// Token: 0x06000CB0 RID: 3248 RVA: 0x00023B64 File Offset: 0x00021D64
		public CsdlPropertyValue(string property, CsdlExpressionBase expression, CsdlLocation location)
			: base(location)
		{
			this.property = property;
			this.expression = expression;
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00023B7B File Offset: 0x00021D7B
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x00023B83 File Offset: 0x00021D83
		public CsdlExpressionBase Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x040006E7 RID: 1767
		private readonly CsdlExpressionBase expression;

		// Token: 0x040006E8 RID: 1768
		private readonly string property;
	}
}

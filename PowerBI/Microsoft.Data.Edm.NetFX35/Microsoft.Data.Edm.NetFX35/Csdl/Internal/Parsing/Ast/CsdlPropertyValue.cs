using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x0200001E RID: 30
	internal class CsdlPropertyValue : CsdlElement
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002D4E File Offset: 0x00000F4E
		public CsdlPropertyValue(string property, CsdlExpressionBase expression, CsdlLocation location)
			: base(location)
		{
			this.property = property;
			this.expression = expression;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002D65 File Offset: 0x00000F65
		public string Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002D6D File Offset: 0x00000F6D
		public CsdlExpressionBase Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x0400002E RID: 46
		private readonly CsdlExpressionBase expression;

		// Token: 0x0400002F RID: 47
		private readonly string property;
	}
}

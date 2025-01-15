using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E2 RID: 482
	internal class CsdlRecordExpression : CsdlExpressionBase
	{
		// Token: 0x06000D68 RID: 3432 RVA: 0x00025D53 File Offset: 0x00023F53
		public CsdlRecordExpression(CsdlTypeReference type, IEnumerable<CsdlPropertyValue> propertyValues, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.propertyValues = new List<CsdlPropertyValue>(propertyValues);
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0001212F File Offset: 0x0001032F
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Record;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00025D6F File Offset: 0x00023F6F
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00025D77 File Offset: 0x00023F77
		public IEnumerable<CsdlPropertyValue> PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x04000762 RID: 1890
		private readonly CsdlTypeReference type;

		// Token: 0x04000763 RID: 1891
		private readonly List<CsdlPropertyValue> propertyValues;
	}
}

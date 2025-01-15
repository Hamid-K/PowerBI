using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D3 RID: 467
	internal class CsdlRecordExpression : CsdlExpressionBase
	{
		// Token: 0x06000CB3 RID: 3251 RVA: 0x00023B8B File Offset: 0x00021D8B
		public CsdlRecordExpression(CsdlTypeReference type, IEnumerable<CsdlPropertyValue> propertyValues, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.propertyValues = new List<CsdlPropertyValue>(propertyValues);
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00013C4B File Offset: 0x00011E4B
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Record;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00023BA7 File Offset: 0x00021DA7
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00023BAF File Offset: 0x00021DAF
		public IEnumerable<CsdlPropertyValue> PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x040006E9 RID: 1769
		private readonly CsdlTypeReference type;

		// Token: 0x040006EA RID: 1770
		private readonly List<CsdlPropertyValue> propertyValues;
	}
}

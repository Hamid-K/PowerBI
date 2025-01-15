using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000039 RID: 57
	internal class CsdlRecordExpression : CsdlExpressionBase
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00003906 File Offset: 0x00001B06
		public CsdlRecordExpression(CsdlTypeReference type, IEnumerable<CsdlPropertyValue> propertyValues, CsdlLocation location)
			: base(location)
		{
			this.type = type;
			this.propertyValues = new List<CsdlPropertyValue>(propertyValues);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003922 File Offset: 0x00001B22
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Record;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003926 File Offset: 0x00001B26
		public CsdlTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000392E File Offset: 0x00001B2E
		public IEnumerable<CsdlPropertyValue> PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x04000057 RID: 87
		private readonly CsdlTypeReference type;

		// Token: 0x04000058 RID: 88
		private readonly List<CsdlPropertyValue> propertyValues;
	}
}

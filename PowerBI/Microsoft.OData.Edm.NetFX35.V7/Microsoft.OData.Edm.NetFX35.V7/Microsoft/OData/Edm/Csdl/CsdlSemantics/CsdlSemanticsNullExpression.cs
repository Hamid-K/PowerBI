using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000179 RID: 377
	internal class CsdlSemanticsNullExpression : CsdlSemanticsExpression, IEdmNullExpression, IEdmExpression, IEdmElement, IEdmNullValue, IEdmValue
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x0001B2C8 File Offset: 0x000194C8
		public CsdlSemanticsNullExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0001B2D9 File Offset: 0x000194D9
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00013BAA File Offset: 0x00011DAA
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Null;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0001B2E1 File Offset: 0x000194E1
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040005F2 RID: 1522
		private readonly CsdlConstantExpression expression;
	}
}

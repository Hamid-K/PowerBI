using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000AE RID: 174
	internal class CsdlSemanticsNullExpression : CsdlSemanticsExpression, IEdmNullExpression, IEdmExpression, IEdmNullValue, IEdmValue, IEdmElement
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x00006F81 File Offset: 0x00005181
		public CsdlSemanticsNullExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00006F92 File Offset: 0x00005192
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00006F9A File Offset: 0x0000519A
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Null;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00006F9E File Offset: 0x0000519E
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00006FAB File Offset: 0x000051AB
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000138 RID: 312
		private readonly CsdlConstantExpression expression;
	}
}

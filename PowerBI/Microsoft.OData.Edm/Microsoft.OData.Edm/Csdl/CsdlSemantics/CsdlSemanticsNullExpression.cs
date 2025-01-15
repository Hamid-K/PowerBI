using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000188 RID: 392
	internal class CsdlSemanticsNullExpression : CsdlSemanticsExpression, IEdmNullExpression, IEdmExpression, IEdmElement, IEdmNullValue, IEdmValue
	{
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001D3CD File Offset: 0x0001B5CD
		public CsdlSemanticsNullExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x0001D3DE File Offset: 0x0001B5DE
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0001208E File Offset: 0x0001028E
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Null;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0001D3E6 File Offset: 0x0001B5E6
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400066E RID: 1646
		private readonly CsdlConstantExpression expression;
	}
}

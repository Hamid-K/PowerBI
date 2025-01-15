using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018F RID: 399
	internal class CsdlSemanticsStringConstantExpression : CsdlSemanticsExpression, IEdmStringConstantExpression, IEdmExpression, IEdmElement, IEdmStringValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x0001D6AC File Offset: 0x0001B8AC
		public CsdlSemanticsStringConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0001D6C8 File Offset: 0x0001B8C8
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0001D6D0 File Offset: 0x0001B8D0
		public string Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsStringConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00002715 File Offset: 0x00000915
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.StringConstant;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0001D6E4 File Offset: 0x0001B8E4
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001D6F1 File Offset: 0x0001B8F1
		private string ComputeValue()
		{
			return this.expression.Value;
		}

		// Token: 0x0400067D RID: 1661
		private readonly CsdlConstantExpression expression;

		// Token: 0x0400067E RID: 1662
		private readonly Cache<CsdlSemanticsStringConstantExpression, string> valueCache = new Cache<CsdlSemanticsStringConstantExpression, string>();

		// Token: 0x0400067F RID: 1663
		private static readonly Func<CsdlSemanticsStringConstantExpression, string> ComputeValueFunc = (CsdlSemanticsStringConstantExpression me) => me.ComputeValue();
	}
}

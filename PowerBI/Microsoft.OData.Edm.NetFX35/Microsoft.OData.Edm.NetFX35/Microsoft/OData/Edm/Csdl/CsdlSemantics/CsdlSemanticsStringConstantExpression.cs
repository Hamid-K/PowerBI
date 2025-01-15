using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000C0 RID: 192
	internal class CsdlSemanticsStringConstantExpression : CsdlSemanticsExpression, IEdmStringConstantExpression, IEdmExpression, IEdmStringValue, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x06000333 RID: 819 RVA: 0x000074AC File Offset: 0x000056AC
		public CsdlSemanticsStringConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000334 RID: 820 RVA: 0x000074C8 File Offset: 0x000056C8
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000335 RID: 821 RVA: 0x000074D0 File Offset: 0x000056D0
		public string Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsStringConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000336 RID: 822 RVA: 0x000074E4 File Offset: 0x000056E4
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.StringConstant;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000337 RID: 823 RVA: 0x000074E7 File Offset: 0x000056E7
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000338 RID: 824 RVA: 0x000074EA File Offset: 0x000056EA
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000074F7 File Offset: 0x000056F7
		private string ComputeValue()
		{
			return this.expression.Value;
		}

		// Token: 0x04000156 RID: 342
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000157 RID: 343
		private readonly Cache<CsdlSemanticsStringConstantExpression, string> valueCache = new Cache<CsdlSemanticsStringConstantExpression, string>();

		// Token: 0x04000158 RID: 344
		private static readonly Func<CsdlSemanticsStringConstantExpression, string> ComputeValueFunc = (CsdlSemanticsStringConstantExpression me) => me.ComputeValue();
	}
}

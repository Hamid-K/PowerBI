using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000180 RID: 384
	internal class CsdlSemanticsStringConstantExpression : CsdlSemanticsExpression, IEdmStringConstantExpression, IEdmExpression, IEdmElement, IEdmStringValue, IEdmPrimitiveValue, IEdmValue
	{
		// Token: 0x06000A21 RID: 2593 RVA: 0x0001B5A4 File Offset: 0x000197A4
		public CsdlSemanticsStringConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0001B5C0 File Offset: 0x000197C0
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0001B5C8 File Offset: 0x000197C8
		public string Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsStringConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0000C876 File Offset: 0x0000AA76
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.StringConstant;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0001B5DC File Offset: 0x000197DC
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001B5E9 File Offset: 0x000197E9
		private string ComputeValue()
		{
			return this.expression.Value;
		}

		// Token: 0x04000601 RID: 1537
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000602 RID: 1538
		private readonly Cache<CsdlSemanticsStringConstantExpression, string> valueCache = new Cache<CsdlSemanticsStringConstantExpression, string>();

		// Token: 0x04000603 RID: 1539
		private static readonly Func<CsdlSemanticsStringConstantExpression, string> ComputeValueFunc = (CsdlSemanticsStringConstantExpression me) => me.ComputeValue();
	}
}

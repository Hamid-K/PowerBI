using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000183 RID: 387
	internal class CsdlSemanticsDecimalConstantExpression : CsdlSemanticsExpression, IEdmDecimalConstantExpression, IEdmExpression, IEdmElement, IEdmDecimalValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x0001CE4D File Offset: 0x0001B04D
		public CsdlSemanticsDecimalConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0001CE74 File Offset: 0x0001B074
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0001CE7C File Offset: 0x0001B07C
		public decimal Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDecimalConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000039FB File Offset: 0x00001BFB
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DecimalConstant;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0001CE90 File Offset: 0x0001B090
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0001CE9D File Offset: 0x0001B09D
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDecimalConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001CEB4 File Offset: 0x0001B0B4
		private decimal ComputeValue()
		{
			decimal? num;
			if (!EdmValueParser.TryParseDecimal(this.expression.Value, out num))
			{
				return 0m;
			}
			return num.Value;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001CEE4 File Offset: 0x0001B0E4
		private IEnumerable<EdmError> ComputeErrors()
		{
			decimal? num;
			if (!EdmValueParser.TryParseDecimal(this.expression.Value, out num))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidDecimal, Strings.ValueParser_InvalidDecimal(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x04000656 RID: 1622
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000657 RID: 1623
		private readonly Cache<CsdlSemanticsDecimalConstantExpression, decimal> valueCache = new Cache<CsdlSemanticsDecimalConstantExpression, decimal>();

		// Token: 0x04000658 RID: 1624
		private static readonly Func<CsdlSemanticsDecimalConstantExpression, decimal> ComputeValueFunc = (CsdlSemanticsDecimalConstantExpression me) => me.ComputeValue();

		// Token: 0x04000659 RID: 1625
		private readonly Cache<CsdlSemanticsDecimalConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDecimalConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x0400065A RID: 1626
		private static readonly Func<CsdlSemanticsDecimalConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDecimalConstantExpression me) => me.ComputeErrors();
	}
}

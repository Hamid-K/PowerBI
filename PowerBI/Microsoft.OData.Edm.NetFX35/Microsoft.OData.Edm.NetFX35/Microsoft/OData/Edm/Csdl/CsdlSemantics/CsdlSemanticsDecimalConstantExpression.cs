using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000A4 RID: 164
	internal class CsdlSemanticsDecimalConstantExpression : CsdlSemanticsExpression, IEdmDecimalConstantExpression, IEdmExpression, IEdmDecimalValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x00006AAA File Offset: 0x00004CAA
		public CsdlSemanticsDecimalConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00006AD1 File Offset: 0x00004CD1
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00006AD9 File Offset: 0x00004CD9
		public decimal Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDecimalConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00006AED File Offset: 0x00004CED
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DecimalConstant;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00006AF3 File Offset: 0x00004CF3
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00006B00 File Offset: 0x00004D00
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDecimalConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00006B14 File Offset: 0x00004D14
		private decimal ComputeValue()
		{
			decimal? num;
			if (!EdmValueParser.TryParseDecimal(this.expression.Value, out num))
			{
				return 0m;
			}
			return num.Value;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00006B44 File Offset: 0x00004D44
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

		// Token: 0x0400011E RID: 286
		private readonly CsdlConstantExpression expression;

		// Token: 0x0400011F RID: 287
		private readonly Cache<CsdlSemanticsDecimalConstantExpression, decimal> valueCache = new Cache<CsdlSemanticsDecimalConstantExpression, decimal>();

		// Token: 0x04000120 RID: 288
		private static readonly Func<CsdlSemanticsDecimalConstantExpression, decimal> ComputeValueFunc = (CsdlSemanticsDecimalConstantExpression me) => me.ComputeValue();

		// Token: 0x04000121 RID: 289
		private readonly Cache<CsdlSemanticsDecimalConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDecimalConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000122 RID: 290
		private static readonly Func<CsdlSemanticsDecimalConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDecimalConstantExpression me) => me.ComputeErrors();
	}
}

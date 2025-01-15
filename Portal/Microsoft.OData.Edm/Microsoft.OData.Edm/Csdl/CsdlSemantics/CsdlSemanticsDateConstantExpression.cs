using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016F RID: 367
	internal class CsdlSemanticsDateConstantExpression : CsdlSemanticsExpression, IEdmDateConstantExpression, IEdmExpression, IEdmElement, IEdmDateValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x0001BBFF File Offset: 0x00019DFF
		public CsdlSemanticsDateConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001BC26 File Offset: 0x00019E26
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0001BC2E File Offset: 0x00019E2E
		public Date Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDateConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x000121BD File Offset: 0x000103BD
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateConstant;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0001BC42 File Offset: 0x00019E42
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0001BC4F File Offset: 0x00019E4F
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDateConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0001BC64 File Offset: 0x00019E64
		private Date ComputeValue()
		{
			Date? date;
			if (!EdmValueParser.TryParseDate(this.expression.Value, out date))
			{
				return Date.MinValue;
			}
			return date.Value;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0001BC94 File Offset: 0x00019E94
		private IEnumerable<EdmError> ComputeErrors()
		{
			Date? date;
			if (!EdmValueParser.TryParseDate(this.expression.Value, out date))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidDate, Strings.ValueParser_InvalidDate(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x04000603 RID: 1539
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000604 RID: 1540
		private readonly Cache<CsdlSemanticsDateConstantExpression, Date> valueCache = new Cache<CsdlSemanticsDateConstantExpression, Date>();

		// Token: 0x04000605 RID: 1541
		private static readonly Func<CsdlSemanticsDateConstantExpression, Date> ComputeValueFunc = (CsdlSemanticsDateConstantExpression me) => me.ComputeValue();

		// Token: 0x04000606 RID: 1542
		private readonly Cache<CsdlSemanticsDateConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDateConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000607 RID: 1543
		private static readonly Func<CsdlSemanticsDateConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDateConstantExpression me) => me.ComputeErrors();
	}
}

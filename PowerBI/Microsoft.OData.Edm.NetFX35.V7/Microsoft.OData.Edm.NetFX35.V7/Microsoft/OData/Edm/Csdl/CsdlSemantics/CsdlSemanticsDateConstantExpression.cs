using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000160 RID: 352
	internal class CsdlSemanticsDateConstantExpression : CsdlSemanticsExpression, IEdmDateConstantExpression, IEdmExpression, IEdmElement, IEdmDateValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x00019AFF File Offset: 0x00017CFF
		public CsdlSemanticsDateConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00019B26 File Offset: 0x00017D26
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00019B2E File Offset: 0x00017D2E
		public Date Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDateConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00013CD9 File Offset: 0x00011ED9
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateConstant;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00019B42 File Offset: 0x00017D42
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00019B4F File Offset: 0x00017D4F
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDateConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00019B64 File Offset: 0x00017D64
		private Date ComputeValue()
		{
			Date? date;
			if (!EdmValueParser.TryParseDate(this.expression.Value, out date))
			{
				return Date.MinValue;
			}
			return date.Value;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00019B94 File Offset: 0x00017D94
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

		// Token: 0x04000588 RID: 1416
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000589 RID: 1417
		private readonly Cache<CsdlSemanticsDateConstantExpression, Date> valueCache = new Cache<CsdlSemanticsDateConstantExpression, Date>();

		// Token: 0x0400058A RID: 1418
		private static readonly Func<CsdlSemanticsDateConstantExpression, Date> ComputeValueFunc = (CsdlSemanticsDateConstantExpression me) => me.ComputeValue();

		// Token: 0x0400058B RID: 1419
		private readonly Cache<CsdlSemanticsDateConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDateConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x0400058C RID: 1420
		private static readonly Func<CsdlSemanticsDateConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDateConstantExpression me) => me.ComputeErrors();
	}
}

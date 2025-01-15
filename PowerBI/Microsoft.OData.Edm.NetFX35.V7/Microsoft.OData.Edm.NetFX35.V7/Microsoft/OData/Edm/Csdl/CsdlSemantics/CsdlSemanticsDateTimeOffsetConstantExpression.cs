using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000161 RID: 353
	internal class CsdlSemanticsDateTimeOffsetConstantExpression : CsdlSemanticsExpression, IEdmDateTimeOffsetConstantExpression, IEdmExpression, IEdmElement, IEdmDateTimeOffsetValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x00019C10 File Offset: 0x00017E10
		public CsdlSemanticsDateTimeOffsetConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00019C37 File Offset: 0x00017E37
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00019C3F File Offset: 0x00017E3F
		public DateTimeOffset Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDateTimeOffsetConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00009097 File Offset: 0x00007297
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateTimeOffsetConstant;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00019C53 File Offset: 0x00017E53
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00019C60 File Offset: 0x00017E60
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDateTimeOffsetConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00019C74 File Offset: 0x00017E74
		private DateTimeOffset ComputeValue()
		{
			DateTimeOffset? dateTimeOffset;
			if (!EdmValueParser.TryParseDateTimeOffset(this.expression.Value, out dateTimeOffset))
			{
				return DateTimeOffset.MinValue;
			}
			return dateTimeOffset.Value;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00019CA4 File Offset: 0x00017EA4
		private IEnumerable<EdmError> ComputeErrors()
		{
			DateTimeOffset? dateTimeOffset;
			if (!EdmValueParser.TryParseDateTimeOffset(this.expression.Value, out dateTimeOffset))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidDateTimeOffset, Strings.ValueParser_InvalidDateTimeOffset(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x0400058D RID: 1421
		private readonly CsdlConstantExpression expression;

		// Token: 0x0400058E RID: 1422
		private readonly Cache<CsdlSemanticsDateTimeOffsetConstantExpression, DateTimeOffset> valueCache = new Cache<CsdlSemanticsDateTimeOffsetConstantExpression, DateTimeOffset>();

		// Token: 0x0400058F RID: 1423
		private static readonly Func<CsdlSemanticsDateTimeOffsetConstantExpression, DateTimeOffset> ComputeValueFunc = (CsdlSemanticsDateTimeOffsetConstantExpression me) => me.ComputeValue();

		// Token: 0x04000590 RID: 1424
		private readonly Cache<CsdlSemanticsDateTimeOffsetConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDateTimeOffsetConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000591 RID: 1425
		private static readonly Func<CsdlSemanticsDateTimeOffsetConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDateTimeOffsetConstantExpression me) => me.ComputeErrors();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000078 RID: 120
	internal class CsdlSemanticsDateTimeOffsetConstantExpression : CsdlSemanticsExpression, IEdmDateTimeOffsetConstantExpression, IEdmExpression, IEdmDateTimeOffsetValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00004A75 File Offset: 0x00002C75
		public CsdlSemanticsDateTimeOffsetConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004A9C File Offset: 0x00002C9C
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public DateTimeOffset Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDateTimeOffsetConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00004ABB File Offset: 0x00002CBB
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateTimeOffsetConstant;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00004ABE File Offset: 0x00002CBE
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00004ACB File Offset: 0x00002CCB
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDateTimeOffsetConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00004AE0 File Offset: 0x00002CE0
		private DateTimeOffset ComputeValue()
		{
			DateTimeOffset? dateTimeOffset;
			if (!EdmValueParser.TryParseDateTimeOffset(this.expression.Value, out dateTimeOffset))
			{
				return DateTimeOffset.MinValue;
			}
			return dateTimeOffset.Value;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00004B10 File Offset: 0x00002D10
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

		// Token: 0x040000A9 RID: 169
		private readonly CsdlConstantExpression expression;

		// Token: 0x040000AA RID: 170
		private readonly Cache<CsdlSemanticsDateTimeOffsetConstantExpression, DateTimeOffset> valueCache = new Cache<CsdlSemanticsDateTimeOffsetConstantExpression, DateTimeOffset>();

		// Token: 0x040000AB RID: 171
		private static readonly Func<CsdlSemanticsDateTimeOffsetConstantExpression, DateTimeOffset> ComputeValueFunc = (CsdlSemanticsDateTimeOffsetConstantExpression me) => me.ComputeValue();

		// Token: 0x040000AC RID: 172
		private readonly Cache<CsdlSemanticsDateTimeOffsetConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDateTimeOffsetConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040000AD RID: 173
		private static readonly Func<CsdlSemanticsDateTimeOffsetConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDateTimeOffsetConstantExpression me) => me.ComputeErrors();
	}
}

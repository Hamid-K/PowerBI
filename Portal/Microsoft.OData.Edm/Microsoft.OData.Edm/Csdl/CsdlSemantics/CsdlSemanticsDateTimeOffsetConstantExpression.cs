using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000170 RID: 368
	internal class CsdlSemanticsDateTimeOffsetConstantExpression : CsdlSemanticsExpression, IEdmDateTimeOffsetConstantExpression, IEdmExpression, IEdmElement, IEdmDateTimeOffsetValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x0001BD10 File Offset: 0x00019F10
		public CsdlSemanticsDateTimeOffsetConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0001BD37 File Offset: 0x00019F37
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0001BD3F File Offset: 0x00019F3F
		public DateTimeOffset Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDateTimeOffsetConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0000268B File Offset: 0x0000088B
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateTimeOffsetConstant;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0001BD53 File Offset: 0x00019F53
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0001BD60 File Offset: 0x00019F60
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDateTimeOffsetConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001BD74 File Offset: 0x00019F74
		private DateTimeOffset ComputeValue()
		{
			DateTimeOffset? dateTimeOffset;
			if (!EdmValueParser.TryParseDateTimeOffset(this.expression.Value, out dateTimeOffset))
			{
				return DateTimeOffset.MinValue;
			}
			return dateTimeOffset.Value;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001BDA4 File Offset: 0x00019FA4
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

		// Token: 0x04000608 RID: 1544
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000609 RID: 1545
		private readonly Cache<CsdlSemanticsDateTimeOffsetConstantExpression, DateTimeOffset> valueCache = new Cache<CsdlSemanticsDateTimeOffsetConstantExpression, DateTimeOffset>();

		// Token: 0x0400060A RID: 1546
		private static readonly Func<CsdlSemanticsDateTimeOffsetConstantExpression, DateTimeOffset> ComputeValueFunc = (CsdlSemanticsDateTimeOffsetConstantExpression me) => me.ComputeValue();

		// Token: 0x0400060B RID: 1547
		private readonly Cache<CsdlSemanticsDateTimeOffsetConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDateTimeOffsetConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x0400060C RID: 1548
		private static readonly Func<CsdlSemanticsDateTimeOffsetConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDateTimeOffsetConstantExpression me) => me.ComputeErrors();
	}
}

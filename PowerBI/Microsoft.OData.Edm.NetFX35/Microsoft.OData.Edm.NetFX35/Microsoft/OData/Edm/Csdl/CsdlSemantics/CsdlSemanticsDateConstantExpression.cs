using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000075 RID: 117
	internal class CsdlSemanticsDateConstantExpression : CsdlSemanticsExpression, IEdmDateConstantExpression, IEdmExpression, IEdmDateValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00004925 File Offset: 0x00002B25
		public CsdlSemanticsDateConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000494C File Offset: 0x00002B4C
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00004954 File Offset: 0x00002B54
		public Date Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDateConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00004968 File Offset: 0x00002B68
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000496B File Offset: 0x00002B6B
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DateConstant;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000496F File Offset: 0x00002B6F
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000497C File Offset: 0x00002B7C
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDateConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00004990 File Offset: 0x00002B90
		private Date ComputeValue()
		{
			Date? date;
			if (!EdmValueParser.TryParseDate(this.expression.Value, out date))
			{
				return Date.MinValue;
			}
			return date.Value;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000049C0 File Offset: 0x00002BC0
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

		// Token: 0x040000A2 RID: 162
		private readonly CsdlConstantExpression expression;

		// Token: 0x040000A3 RID: 163
		private readonly Cache<CsdlSemanticsDateConstantExpression, Date> valueCache = new Cache<CsdlSemanticsDateConstantExpression, Date>();

		// Token: 0x040000A4 RID: 164
		private static readonly Func<CsdlSemanticsDateConstantExpression, Date> ComputeValueFunc = (CsdlSemanticsDateConstantExpression me) => me.ComputeValue();

		// Token: 0x040000A5 RID: 165
		private readonly Cache<CsdlSemanticsDateConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDateConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040000A6 RID: 166
		private static readonly Func<CsdlSemanticsDateConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDateConstantExpression me) => me.ComputeErrors();
	}
}

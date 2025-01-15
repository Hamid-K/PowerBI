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
	// Token: 0x020000C3 RID: 195
	internal class CsdlSemanticsTimeOfDayConstantExpression : CsdlSemanticsExpression, IEdmTimeOfDayConstantExpression, IEdmExpression, IEdmTimeOfDayValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600033D RID: 829 RVA: 0x00007530 File Offset: 0x00005730
		public CsdlSemanticsTimeOfDayConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00007557 File Offset: 0x00005757
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000755F File Offset: 0x0000575F
		public TimeOfDay Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsTimeOfDayConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00007573 File Offset: 0x00005773
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00007576 File Offset: 0x00005776
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.TimeOfDayConstant;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000757A File Offset: 0x0000577A
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00007587 File Offset: 0x00005787
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsTimeOfDayConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000759C File Offset: 0x0000579C
		private TimeOfDay ComputeValue()
		{
			TimeOfDay? timeOfDay;
			if (!EdmValueParser.TryParseTimeOfDay(this.expression.Value, out timeOfDay))
			{
				return TimeOfDay.MinValue;
			}
			return timeOfDay.Value;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000075CC File Offset: 0x000057CC
		private IEnumerable<EdmError> ComputeErrors()
		{
			TimeOfDay? timeOfDay;
			if (!EdmValueParser.TryParseTimeOfDay(this.expression.Value, out timeOfDay))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidTimeOfDay, Strings.ValueParser_InvalidTimeOfDay(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x0400015A RID: 346
		private readonly CsdlConstantExpression expression;

		// Token: 0x0400015B RID: 347
		private readonly Cache<CsdlSemanticsTimeOfDayConstantExpression, TimeOfDay> valueCache = new Cache<CsdlSemanticsTimeOfDayConstantExpression, TimeOfDay>();

		// Token: 0x0400015C RID: 348
		private static readonly Func<CsdlSemanticsTimeOfDayConstantExpression, TimeOfDay> ComputeValueFunc = (CsdlSemanticsTimeOfDayConstantExpression me) => me.ComputeValue();

		// Token: 0x0400015D RID: 349
		private readonly Cache<CsdlSemanticsTimeOfDayConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsTimeOfDayConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x0400015E RID: 350
		private static readonly Func<CsdlSemanticsTimeOfDayConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsTimeOfDayConstantExpression me) => me.ComputeErrors();
	}
}

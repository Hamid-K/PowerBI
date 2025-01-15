using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000181 RID: 385
	internal class CsdlSemanticsTimeOfDayConstantExpression : CsdlSemanticsExpression, IEdmTimeOfDayConstantExpression, IEdmExpression, IEdmElement, IEdmTimeOfDayValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000A29 RID: 2601 RVA: 0x0001B60D File Offset: 0x0001980D
		public CsdlSemanticsTimeOfDayConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0001B634 File Offset: 0x00019834
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0001B63C File Offset: 0x0001983C
		public TimeOfDay Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsTimeOfDayConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00014010 File Offset: 0x00012210
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.TimeOfDayConstant;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0001B650 File Offset: 0x00019850
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0001B65D File Offset: 0x0001985D
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsTimeOfDayConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001B674 File Offset: 0x00019874
		private TimeOfDay ComputeValue()
		{
			TimeOfDay? timeOfDay;
			if (!EdmValueParser.TryParseTimeOfDay(this.expression.Value, out timeOfDay))
			{
				return TimeOfDay.MinValue;
			}
			return timeOfDay.Value;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001B6A4 File Offset: 0x000198A4
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

		// Token: 0x04000604 RID: 1540
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000605 RID: 1541
		private readonly Cache<CsdlSemanticsTimeOfDayConstantExpression, TimeOfDay> valueCache = new Cache<CsdlSemanticsTimeOfDayConstantExpression, TimeOfDay>();

		// Token: 0x04000606 RID: 1542
		private static readonly Func<CsdlSemanticsTimeOfDayConstantExpression, TimeOfDay> ComputeValueFunc = (CsdlSemanticsTimeOfDayConstantExpression me) => me.ComputeValue();

		// Token: 0x04000607 RID: 1543
		private readonly Cache<CsdlSemanticsTimeOfDayConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsTimeOfDayConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000608 RID: 1544
		private static readonly Func<CsdlSemanticsTimeOfDayConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsTimeOfDayConstantExpression me) => me.ComputeErrors();
	}
}

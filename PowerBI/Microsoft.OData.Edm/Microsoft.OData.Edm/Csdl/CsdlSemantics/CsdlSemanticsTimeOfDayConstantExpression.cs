using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000190 RID: 400
	internal class CsdlSemanticsTimeOfDayConstantExpression : CsdlSemanticsExpression, IEdmTimeOfDayConstantExpression, IEdmExpression, IEdmElement, IEdmTimeOfDayValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001D715 File Offset: 0x0001B915
		public CsdlSemanticsTimeOfDayConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0001D73C File Offset: 0x0001B93C
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0001D744 File Offset: 0x0001B944
		public TimeOfDay Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsTimeOfDayConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x000124F0 File Offset: 0x000106F0
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.TimeOfDayConstant;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0001D758 File Offset: 0x0001B958
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0001D765 File Offset: 0x0001B965
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsTimeOfDayConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001D77C File Offset: 0x0001B97C
		private TimeOfDay ComputeValue()
		{
			TimeOfDay? timeOfDay;
			if (!EdmValueParser.TryParseTimeOfDay(this.expression.Value, out timeOfDay))
			{
				return TimeOfDay.MinValue;
			}
			return timeOfDay.Value;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001D7AC File Offset: 0x0001B9AC
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

		// Token: 0x04000680 RID: 1664
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000681 RID: 1665
		private readonly Cache<CsdlSemanticsTimeOfDayConstantExpression, TimeOfDay> valueCache = new Cache<CsdlSemanticsTimeOfDayConstantExpression, TimeOfDay>();

		// Token: 0x04000682 RID: 1666
		private static readonly Func<CsdlSemanticsTimeOfDayConstantExpression, TimeOfDay> ComputeValueFunc = (CsdlSemanticsTimeOfDayConstantExpression me) => me.ComputeValue();

		// Token: 0x04000683 RID: 1667
		private readonly Cache<CsdlSemanticsTimeOfDayConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsTimeOfDayConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000684 RID: 1668
		private static readonly Func<CsdlSemanticsTimeOfDayConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsTimeOfDayConstantExpression me) => me.ComputeErrors();
	}
}

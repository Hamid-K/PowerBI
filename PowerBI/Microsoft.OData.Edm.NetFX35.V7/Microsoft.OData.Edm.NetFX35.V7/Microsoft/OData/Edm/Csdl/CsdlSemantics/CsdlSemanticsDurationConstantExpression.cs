using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000182 RID: 386
	internal class CsdlSemanticsDurationConstantExpression : CsdlSemanticsExpression, IEdmDurationConstantExpression, IEdmExpression, IEdmElement, IEdmDurationValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x0001B720 File Offset: 0x00019920
		public CsdlSemanticsDurationConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0001B747 File Offset: 0x00019947
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0001B74F File Offset: 0x0001994F
		public TimeSpan Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDurationConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00013D4A File Offset: 0x00011F4A
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DurationConstant;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0001B763 File Offset: 0x00019963
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0001B770 File Offset: 0x00019970
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDurationConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001B784 File Offset: 0x00019984
		private TimeSpan ComputeValue()
		{
			TimeSpan? timeSpan;
			if (!EdmValueParser.TryParseDuration(this.expression.Value, out timeSpan))
			{
				return TimeSpan.Zero;
			}
			return timeSpan.Value;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001B7B4 File Offset: 0x000199B4
		private IEnumerable<EdmError> ComputeErrors()
		{
			TimeSpan? timeSpan;
			if (!EdmValueParser.TryParseDuration(this.expression.Value, out timeSpan))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidDuration, Strings.ValueParser_InvalidDuration(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x04000609 RID: 1545
		private readonly CsdlConstantExpression expression;

		// Token: 0x0400060A RID: 1546
		private readonly Cache<CsdlSemanticsDurationConstantExpression, TimeSpan> valueCache = new Cache<CsdlSemanticsDurationConstantExpression, TimeSpan>();

		// Token: 0x0400060B RID: 1547
		private static readonly Func<CsdlSemanticsDurationConstantExpression, TimeSpan> ComputeValueFunc = (CsdlSemanticsDurationConstantExpression me) => me.ComputeValue();

		// Token: 0x0400060C RID: 1548
		private readonly Cache<CsdlSemanticsDurationConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDurationConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x0400060D RID: 1549
		private static readonly Func<CsdlSemanticsDurationConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDurationConstantExpression me) => me.ComputeErrors();
	}
}

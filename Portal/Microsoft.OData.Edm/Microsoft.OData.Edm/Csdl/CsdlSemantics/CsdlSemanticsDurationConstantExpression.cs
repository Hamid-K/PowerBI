using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000191 RID: 401
	internal class CsdlSemanticsDurationConstantExpression : CsdlSemanticsExpression, IEdmDurationConstantExpression, IEdmExpression, IEdmElement, IEdmDurationValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000AEF RID: 2799 RVA: 0x0001D828 File Offset: 0x0001BA28
		public CsdlSemanticsDurationConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0001D84F File Offset: 0x0001BA4F
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0001D857 File Offset: 0x0001BA57
		public TimeSpan Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDurationConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00002623 File Offset: 0x00000823
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DurationConstant;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0001D86B File Offset: 0x0001BA6B
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0001D878 File Offset: 0x0001BA78
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDurationConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001D88C File Offset: 0x0001BA8C
		private TimeSpan ComputeValue()
		{
			TimeSpan? timeSpan;
			if (!EdmValueParser.TryParseDuration(this.expression.Value, out timeSpan))
			{
				return TimeSpan.Zero;
			}
			return timeSpan.Value;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001D8BC File Offset: 0x0001BABC
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

		// Token: 0x04000685 RID: 1669
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000686 RID: 1670
		private readonly Cache<CsdlSemanticsDurationConstantExpression, TimeSpan> valueCache = new Cache<CsdlSemanticsDurationConstantExpression, TimeSpan>();

		// Token: 0x04000687 RID: 1671
		private static readonly Func<CsdlSemanticsDurationConstantExpression, TimeSpan> ComputeValueFunc = (CsdlSemanticsDurationConstantExpression me) => me.ComputeValue();

		// Token: 0x04000688 RID: 1672
		private readonly Cache<CsdlSemanticsDurationConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDurationConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000689 RID: 1673
		private static readonly Func<CsdlSemanticsDurationConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDurationConstantExpression me) => me.ComputeErrors();
	}
}

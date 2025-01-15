using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000C6 RID: 198
	internal class CsdlSemanticsDurationConstantExpression : CsdlSemanticsExpression, IEdmDurationConstantExpression, IEdmExpression, IEdmDurationValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600034A RID: 842 RVA: 0x00007681 File Offset: 0x00005881
		public CsdlSemanticsDurationConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600034B RID: 843 RVA: 0x000076A8 File Offset: 0x000058A8
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000076B0 File Offset: 0x000058B0
		public TimeSpan Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDurationConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000076C4 File Offset: 0x000058C4
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000076C7 File Offset: 0x000058C7
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DurationConstant;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000076CB File Offset: 0x000058CB
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000350 RID: 848 RVA: 0x000076D8 File Offset: 0x000058D8
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDurationConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000076EC File Offset: 0x000058EC
		private TimeSpan ComputeValue()
		{
			TimeSpan? timeSpan;
			if (!EdmValueParser.TryParseDuration(this.expression.Value, out timeSpan))
			{
				return TimeSpan.Zero;
			}
			return timeSpan.Value;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000771C File Offset: 0x0000591C
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

		// Token: 0x04000161 RID: 353
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000162 RID: 354
		private readonly Cache<CsdlSemanticsDurationConstantExpression, TimeSpan> valueCache = new Cache<CsdlSemanticsDurationConstantExpression, TimeSpan>();

		// Token: 0x04000163 RID: 355
		private static readonly Func<CsdlSemanticsDurationConstantExpression, TimeSpan> ComputeValueFunc = (CsdlSemanticsDurationConstantExpression me) => me.ComputeValue();

		// Token: 0x04000164 RID: 356
		private readonly Cache<CsdlSemanticsDurationConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDurationConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000165 RID: 357
		private static readonly Func<CsdlSemanticsDurationConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDurationConstantExpression me) => me.ComputeErrors();
	}
}

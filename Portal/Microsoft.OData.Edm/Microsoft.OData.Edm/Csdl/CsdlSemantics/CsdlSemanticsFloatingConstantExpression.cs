using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000185 RID: 389
	internal class CsdlSemanticsFloatingConstantExpression : CsdlSemanticsExpression, IEdmFloatingConstantExpression, IEdmExpression, IEdmElement, IEdmFloatingValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x0001D0F2 File Offset: 0x0001B2F2
		public CsdlSemanticsFloatingConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0001D119 File Offset: 0x0001B319
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0001D121 File Offset: 0x0001B321
		public double Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsFloatingConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0000480B File Offset: 0x00002A0B
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FloatingConstant;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0001D135 File Offset: 0x0001B335
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0001D142 File Offset: 0x0001B342
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsFloatingConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001D158 File Offset: 0x0001B358
		private double ComputeValue()
		{
			double? num;
			if (!EdmValueParser.TryParseFloat(this.expression.Value, out num))
			{
				return 0.0;
			}
			return num.Value;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001D18C File Offset: 0x0001B38C
		private IEnumerable<EdmError> ComputeErrors()
		{
			double? num;
			if (!EdmValueParser.TryParseFloat(this.expression.Value, out num))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidFloatingPoint, Strings.ValueParser_InvalidFloatingPoint(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x04000660 RID: 1632
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000661 RID: 1633
		private readonly Cache<CsdlSemanticsFloatingConstantExpression, double> valueCache = new Cache<CsdlSemanticsFloatingConstantExpression, double>();

		// Token: 0x04000662 RID: 1634
		private static readonly Func<CsdlSemanticsFloatingConstantExpression, double> ComputeValueFunc = (CsdlSemanticsFloatingConstantExpression me) => me.ComputeValue();

		// Token: 0x04000663 RID: 1635
		private readonly Cache<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000664 RID: 1636
		private static readonly Func<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsFloatingConstantExpression me) => me.ComputeErrors();
	}
}

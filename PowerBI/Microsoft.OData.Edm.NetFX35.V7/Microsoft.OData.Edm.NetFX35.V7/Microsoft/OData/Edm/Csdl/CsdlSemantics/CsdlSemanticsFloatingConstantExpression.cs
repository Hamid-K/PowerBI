using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000176 RID: 374
	internal class CsdlSemanticsFloatingConstantExpression : CsdlSemanticsExpression, IEdmFloatingConstantExpression, IEdmExpression, IEdmElement, IEdmFloatingValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x060009E1 RID: 2529 RVA: 0x0001AFDE File Offset: 0x000191DE
		public CsdlSemanticsFloatingConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001B005 File Offset: 0x00019205
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0001B00D File Offset: 0x0001920D
		public double Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsFloatingConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00009215 File Offset: 0x00007415
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FloatingConstant;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001B021 File Offset: 0x00019221
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0001B02E File Offset: 0x0001922E
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsFloatingConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001B044 File Offset: 0x00019244
		private double ComputeValue()
		{
			double? num;
			if (!EdmValueParser.TryParseFloat(this.expression.Value, out num))
			{
				return 0.0;
			}
			return num.Value;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0001B078 File Offset: 0x00019278
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

		// Token: 0x040005E4 RID: 1508
		private readonly CsdlConstantExpression expression;

		// Token: 0x040005E5 RID: 1509
		private readonly Cache<CsdlSemanticsFloatingConstantExpression, double> valueCache = new Cache<CsdlSemanticsFloatingConstantExpression, double>();

		// Token: 0x040005E6 RID: 1510
		private static readonly Func<CsdlSemanticsFloatingConstantExpression, double> ComputeValueFunc = (CsdlSemanticsFloatingConstantExpression me) => me.ComputeValue();

		// Token: 0x040005E7 RID: 1511
		private readonly Cache<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040005E8 RID: 1512
		private static readonly Func<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsFloatingConstantExpression me) => me.ComputeErrors();
	}
}

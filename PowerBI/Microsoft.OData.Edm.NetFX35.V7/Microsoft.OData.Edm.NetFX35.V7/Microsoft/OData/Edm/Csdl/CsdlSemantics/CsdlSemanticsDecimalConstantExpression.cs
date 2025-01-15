using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000174 RID: 372
	internal class CsdlSemanticsDecimalConstantExpression : CsdlSemanticsExpression, IEdmDecimalConstantExpression, IEdmExpression, IEdmElement, IEdmDecimalValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x060009CC RID: 2508 RVA: 0x0001AD05 File Offset: 0x00018F05
		public CsdlSemanticsDecimalConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0001AD2C File Offset: 0x00018F2C
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0001AD34 File Offset: 0x00018F34
		public decimal Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsDecimalConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00008D57 File Offset: 0x00006F57
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.DecimalConstant;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0001AD48 File Offset: 0x00018F48
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0001AD55 File Offset: 0x00018F55
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsDecimalConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001AD6C File Offset: 0x00018F6C
		private decimal ComputeValue()
		{
			decimal? num;
			if (!EdmValueParser.TryParseDecimal(this.expression.Value, out num))
			{
				return 0m;
			}
			return num.Value;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001AD9C File Offset: 0x00018F9C
		private IEnumerable<EdmError> ComputeErrors()
		{
			decimal? num;
			if (!EdmValueParser.TryParseDecimal(this.expression.Value, out num))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidDecimal, Strings.ValueParser_InvalidDecimal(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x040005DA RID: 1498
		private readonly CsdlConstantExpression expression;

		// Token: 0x040005DB RID: 1499
		private readonly Cache<CsdlSemanticsDecimalConstantExpression, decimal> valueCache = new Cache<CsdlSemanticsDecimalConstantExpression, decimal>();

		// Token: 0x040005DC RID: 1500
		private static readonly Func<CsdlSemanticsDecimalConstantExpression, decimal> ComputeValueFunc = (CsdlSemanticsDecimalConstantExpression me) => me.ComputeValue();

		// Token: 0x040005DD RID: 1501
		private readonly Cache<CsdlSemanticsDecimalConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsDecimalConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040005DE RID: 1502
		private static readonly Func<CsdlSemanticsDecimalConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsDecimalConstantExpression me) => me.ComputeErrors();
	}
}

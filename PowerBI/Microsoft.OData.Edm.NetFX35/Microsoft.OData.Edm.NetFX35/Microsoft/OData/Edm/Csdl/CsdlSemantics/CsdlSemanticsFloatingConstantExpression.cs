using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000A7 RID: 167
	internal class CsdlSemanticsFloatingConstantExpression : CsdlSemanticsExpression, IEdmFloatingConstantExpression, IEdmExpression, IEdmFloatingValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00006BF9 File Offset: 0x00004DF9
		public CsdlSemanticsFloatingConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00006C20 File Offset: 0x00004E20
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00006C28 File Offset: 0x00004E28
		public double Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsFloatingConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00006C3C File Offset: 0x00004E3C
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00006C3F File Offset: 0x00004E3F
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FloatingConstant;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00006C42 File Offset: 0x00004E42
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00006C4F File Offset: 0x00004E4F
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsFloatingConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00006C64 File Offset: 0x00004E64
		private double ComputeValue()
		{
			double? num;
			if (!EdmValueParser.TryParseFloat(this.expression.Value, out num))
			{
				return 0.0;
			}
			return num.Value;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00006C98 File Offset: 0x00004E98
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

		// Token: 0x04000125 RID: 293
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000126 RID: 294
		private readonly Cache<CsdlSemanticsFloatingConstantExpression, double> valueCache = new Cache<CsdlSemanticsFloatingConstantExpression, double>();

		// Token: 0x04000127 RID: 295
		private static readonly Func<CsdlSemanticsFloatingConstantExpression, double> ComputeValueFunc = (CsdlSemanticsFloatingConstantExpression me) => me.ComputeValue();

		// Token: 0x04000128 RID: 296
		private readonly Cache<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000129 RID: 297
		private static readonly Func<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsFloatingConstantExpression me) => me.ComputeErrors();
	}
}

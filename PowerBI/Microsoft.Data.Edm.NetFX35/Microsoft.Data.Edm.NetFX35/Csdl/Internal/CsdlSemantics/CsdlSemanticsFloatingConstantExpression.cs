using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Validation;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x0200007A RID: 122
	internal class CsdlSemanticsFloatingConstantExpression : CsdlSemanticsExpression, IEdmFloatingConstantExpression, IEdmExpression, IEdmFloatingValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x060001EE RID: 494 RVA: 0x000057A9 File Offset: 0x000039A9
		public CsdlSemanticsFloatingConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000057D0 File Offset: 0x000039D0
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000057D8 File Offset: 0x000039D8
		public double Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsFloatingConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000057EC File Offset: 0x000039EC
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000057EF File Offset: 0x000039EF
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.FloatingConstant;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000057F2 File Offset: 0x000039F2
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000057FF File Offset: 0x000039FF
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsFloatingConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005814 File Offset: 0x00003A14
		private double ComputeValue()
		{
			double? num;
			if (!EdmValueParser.TryParseFloat(this.expression.Value, out num))
			{
				return 0.0;
			}
			return num.Value;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005848 File Offset: 0x00003A48
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

		// Token: 0x040000D8 RID: 216
		private readonly CsdlConstantExpression expression;

		// Token: 0x040000D9 RID: 217
		private readonly Cache<CsdlSemanticsFloatingConstantExpression, double> valueCache = new Cache<CsdlSemanticsFloatingConstantExpression, double>();

		// Token: 0x040000DA RID: 218
		private static readonly Func<CsdlSemanticsFloatingConstantExpression, double> ComputeValueFunc = (CsdlSemanticsFloatingConstantExpression me) => me.ComputeValue();

		// Token: 0x040000DB RID: 219
		private readonly Cache<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040000DC RID: 220
		private static readonly Func<CsdlSemanticsFloatingConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsFloatingConstantExpression me) => me.ComputeErrors();
	}
}

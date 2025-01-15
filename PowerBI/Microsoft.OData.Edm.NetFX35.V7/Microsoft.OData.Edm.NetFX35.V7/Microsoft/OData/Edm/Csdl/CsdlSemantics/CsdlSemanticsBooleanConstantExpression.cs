using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000171 RID: 369
	internal class CsdlSemanticsBooleanConstantExpression : CsdlSemanticsExpression, IEdmBooleanConstantExpression, IEdmExpression, IEdmElement, IEdmBooleanValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x0001AB76 File Offset: 0x00018D76
		public CsdlSemanticsBooleanConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0001AB9D File Offset: 0x00018D9D
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0001ABA5 File Offset: 0x00018DA5
		public bool Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsBooleanConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00008F68 File Offset: 0x00007168
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BooleanConstant;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0001ABB9 File Offset: 0x00018DB9
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0001ABC6 File Offset: 0x00018DC6
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsBooleanConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001ABDC File Offset: 0x00018DDC
		private bool ComputeValue()
		{
			bool? flag;
			return EdmValueParser.TryParseBool(this.expression.Value, out flag) && flag.Value;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001AC08 File Offset: 0x00018E08
		private IEnumerable<EdmError> ComputeErrors()
		{
			bool? flag;
			if (!EdmValueParser.TryParseBool(this.expression.Value, out flag))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidBoolean, Strings.ValueParser_InvalidBoolean(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x040005D1 RID: 1489
		private readonly CsdlConstantExpression expression;

		// Token: 0x040005D2 RID: 1490
		private readonly Cache<CsdlSemanticsBooleanConstantExpression, bool> valueCache = new Cache<CsdlSemanticsBooleanConstantExpression, bool>();

		// Token: 0x040005D3 RID: 1491
		private static readonly Func<CsdlSemanticsBooleanConstantExpression, bool> ComputeValueFunc = (CsdlSemanticsBooleanConstantExpression me) => me.ComputeValue();

		// Token: 0x040005D4 RID: 1492
		private readonly Cache<CsdlSemanticsBooleanConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsBooleanConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040005D5 RID: 1493
		private static readonly Func<CsdlSemanticsBooleanConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsBooleanConstantExpression me) => me.ComputeErrors();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000180 RID: 384
	internal class CsdlSemanticsBooleanConstantExpression : CsdlSemanticsExpression, IEdmBooleanConstantExpression, IEdmExpression, IEdmElement, IEdmBooleanValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000A76 RID: 2678 RVA: 0x0001CCBE File Offset: 0x0001AEBE
		public CsdlSemanticsBooleanConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0001CCE5 File Offset: 0x0001AEE5
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0001CCED File Offset: 0x0001AEED
		public bool Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsBooleanConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00002732 File Offset: 0x00000932
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BooleanConstant;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0001CD01 File Offset: 0x0001AF01
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0001CD0E File Offset: 0x0001AF0E
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsBooleanConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001CD24 File Offset: 0x0001AF24
		private bool ComputeValue()
		{
			bool? flag;
			return EdmValueParser.TryParseBool(this.expression.Value, out flag) && flag.Value;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001CD50 File Offset: 0x0001AF50
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

		// Token: 0x0400064D RID: 1613
		private readonly CsdlConstantExpression expression;

		// Token: 0x0400064E RID: 1614
		private readonly Cache<CsdlSemanticsBooleanConstantExpression, bool> valueCache = new Cache<CsdlSemanticsBooleanConstantExpression, bool>();

		// Token: 0x0400064F RID: 1615
		private static readonly Func<CsdlSemanticsBooleanConstantExpression, bool> ComputeValueFunc = (CsdlSemanticsBooleanConstantExpression me) => me.ComputeValue();

		// Token: 0x04000650 RID: 1616
		private readonly Cache<CsdlSemanticsBooleanConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsBooleanConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000651 RID: 1617
		private static readonly Func<CsdlSemanticsBooleanConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsBooleanConstantExpression me) => me.ComputeErrors();
	}
}

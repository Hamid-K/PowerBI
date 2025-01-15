using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200009B RID: 155
	internal class CsdlSemanticsBooleanConstantExpression : CsdlSemanticsExpression, IEdmBooleanConstantExpression, IEdmExpression, IEdmBooleanValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000687E File Offset: 0x00004A7E
		public CsdlSemanticsBooleanConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000068A5 File Offset: 0x00004AA5
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000068AD File Offset: 0x00004AAD
		public bool Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsBooleanConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000068C1 File Offset: 0x00004AC1
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BooleanConstant;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000068C4 File Offset: 0x00004AC4
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x000068D1 File Offset: 0x00004AD1
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000068D4 File Offset: 0x00004AD4
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsBooleanConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000068E8 File Offset: 0x00004AE8
		private bool ComputeValue()
		{
			bool? flag;
			return EdmValueParser.TryParseBool(this.expression.Value, out flag) && flag.Value;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00006914 File Offset: 0x00004B14
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

		// Token: 0x04000110 RID: 272
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000111 RID: 273
		private readonly Cache<CsdlSemanticsBooleanConstantExpression, bool> valueCache = new Cache<CsdlSemanticsBooleanConstantExpression, bool>();

		// Token: 0x04000112 RID: 274
		private static readonly Func<CsdlSemanticsBooleanConstantExpression, bool> ComputeValueFunc = (CsdlSemanticsBooleanConstantExpression me) => me.ComputeValue();

		// Token: 0x04000113 RID: 275
		private readonly Cache<CsdlSemanticsBooleanConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsBooleanConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000114 RID: 276
		private static readonly Func<CsdlSemanticsBooleanConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsBooleanConstantExpression me) => me.ComputeErrors();
	}
}

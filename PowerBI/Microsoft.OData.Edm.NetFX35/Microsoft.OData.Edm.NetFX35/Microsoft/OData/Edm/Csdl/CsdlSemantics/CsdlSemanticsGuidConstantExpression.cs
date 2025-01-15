using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000086 RID: 134
	internal class CsdlSemanticsGuidConstantExpression : CsdlSemanticsExpression, IEdmGuidConstantExpression, IEdmExpression, IEdmGuidValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000228 RID: 552 RVA: 0x00005D04 File Offset: 0x00003F04
		public CsdlSemanticsGuidConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00005D2B File Offset: 0x00003F2B
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00005D33 File Offset: 0x00003F33
		public Guid Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsGuidConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00005D47 File Offset: 0x00003F47
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00005D4A File Offset: 0x00003F4A
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.GuidConstant;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00005D4D File Offset: 0x00003F4D
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00005D5A File Offset: 0x00003F5A
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsGuidConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00005D70 File Offset: 0x00003F70
		private Guid ComputeValue()
		{
			Guid? guid;
			if (!EdmValueParser.TryParseGuid(this.expression.Value, out guid))
			{
				return Guid.Empty;
			}
			return guid.Value;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00005DA0 File Offset: 0x00003FA0
		private IEnumerable<EdmError> ComputeErrors()
		{
			Guid? guid;
			if (!EdmValueParser.TryParseGuid(this.expression.Value, out guid))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidGuid, Strings.ValueParser_InvalidGuid(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x040000CC RID: 204
		private readonly CsdlConstantExpression expression;

		// Token: 0x040000CD RID: 205
		private readonly Cache<CsdlSemanticsGuidConstantExpression, Guid> valueCache = new Cache<CsdlSemanticsGuidConstantExpression, Guid>();

		// Token: 0x040000CE RID: 206
		private static readonly Func<CsdlSemanticsGuidConstantExpression, Guid> ComputeValueFunc = (CsdlSemanticsGuidConstantExpression me) => me.ComputeValue();

		// Token: 0x040000CF RID: 207
		private readonly Cache<CsdlSemanticsGuidConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsGuidConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040000D0 RID: 208
		private static readonly Func<CsdlSemanticsGuidConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsGuidConstantExpression me) => me.ComputeErrors();
	}
}

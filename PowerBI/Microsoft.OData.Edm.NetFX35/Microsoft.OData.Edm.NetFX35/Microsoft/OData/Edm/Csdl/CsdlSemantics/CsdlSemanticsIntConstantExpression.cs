using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000AA RID: 170
	internal class CsdlSemanticsIntConstantExpression : CsdlSemanticsExpression, IEdmIntegerConstantExpression, IEdmExpression, IEdmIntegerValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00006D4D File Offset: 0x00004F4D
		public CsdlSemanticsIntConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00006D74 File Offset: 0x00004F74
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00006D7C File Offset: 0x00004F7C
		public long Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsIntConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00006D90 File Offset: 0x00004F90
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IntegerConstant;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00006D93 File Offset: 0x00004F93
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00006DA0 File Offset: 0x00004FA0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00006DA3 File Offset: 0x00004FA3
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsIntConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00006DB8 File Offset: 0x00004FB8
		private long ComputeValue()
		{
			long? num;
			if (!EdmValueParser.TryParseLong(this.expression.Value, out num))
			{
				return 0L;
			}
			return num.Value;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00006DE4 File Offset: 0x00004FE4
		private IEnumerable<EdmError> ComputeErrors()
		{
			long? num;
			if (!EdmValueParser.TryParseLong(this.expression.Value, out num))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidInteger, Strings.ValueParser_InvalidInteger(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x0400012C RID: 300
		private readonly CsdlConstantExpression expression;

		// Token: 0x0400012D RID: 301
		private readonly Cache<CsdlSemanticsIntConstantExpression, long> valueCache = new Cache<CsdlSemanticsIntConstantExpression, long>();

		// Token: 0x0400012E RID: 302
		private static readonly Func<CsdlSemanticsIntConstantExpression, long> ComputeValueFunc = (CsdlSemanticsIntConstantExpression me) => me.ComputeValue();

		// Token: 0x0400012F RID: 303
		private readonly Cache<CsdlSemanticsIntConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsIntConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000130 RID: 304
		private static readonly Func<CsdlSemanticsIntConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsIntConstantExpression me) => me.ComputeErrors();
	}
}

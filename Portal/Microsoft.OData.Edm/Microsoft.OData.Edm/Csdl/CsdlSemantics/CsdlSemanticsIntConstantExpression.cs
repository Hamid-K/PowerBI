using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000186 RID: 390
	internal class CsdlSemanticsIntConstantExpression : CsdlSemanticsExpression, IEdmIntegerConstantExpression, IEdmExpression, IEdmElement, IEdmIntegerValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001D208 File Offset: 0x0001B408
		public CsdlSemanticsIntConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0001D22F File Offset: 0x0001B42F
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0001D237 File Offset: 0x0001B437
		public long Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsIntConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00003AFB File Offset: 0x00001CFB
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IntegerConstant;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0001D24B File Offset: 0x0001B44B
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0001D258 File Offset: 0x0001B458
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsIntConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001D26C File Offset: 0x0001B46C
		private long ComputeValue()
		{
			long? num;
			if (!EdmValueParser.TryParseLong(this.expression.Value, out num))
			{
				return 0L;
			}
			return num.Value;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001D298 File Offset: 0x0001B498
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

		// Token: 0x04000665 RID: 1637
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000666 RID: 1638
		private readonly Cache<CsdlSemanticsIntConstantExpression, long> valueCache = new Cache<CsdlSemanticsIntConstantExpression, long>();

		// Token: 0x04000667 RID: 1639
		private static readonly Func<CsdlSemanticsIntConstantExpression, long> ComputeValueFunc = (CsdlSemanticsIntConstantExpression me) => me.ComputeValue();

		// Token: 0x04000668 RID: 1640
		private readonly Cache<CsdlSemanticsIntConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsIntConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000669 RID: 1641
		private static readonly Func<CsdlSemanticsIntConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsIntConstantExpression me) => me.ComputeErrors();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000177 RID: 375
	internal class CsdlSemanticsIntConstantExpression : CsdlSemanticsExpression, IEdmIntegerConstantExpression, IEdmExpression, IEdmElement, IEdmIntegerValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x0001B0F4 File Offset: 0x000192F4
		public CsdlSemanticsIntConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0001B11B File Offset: 0x0001931B
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0001B123 File Offset: 0x00019323
		public long Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsIntConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0000C558 File Offset: 0x0000A758
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.IntegerConstant;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0001B137 File Offset: 0x00019337
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0001B144 File Offset: 0x00019344
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsIntConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001B158 File Offset: 0x00019358
		private long ComputeValue()
		{
			long? num;
			if (!EdmValueParser.TryParseLong(this.expression.Value, out num))
			{
				return 0L;
			}
			return num.Value;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0001B184 File Offset: 0x00019384
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

		// Token: 0x040005E9 RID: 1513
		private readonly CsdlConstantExpression expression;

		// Token: 0x040005EA RID: 1514
		private readonly Cache<CsdlSemanticsIntConstantExpression, long> valueCache = new Cache<CsdlSemanticsIntConstantExpression, long>();

		// Token: 0x040005EB RID: 1515
		private static readonly Func<CsdlSemanticsIntConstantExpression, long> ComputeValueFunc = (CsdlSemanticsIntConstantExpression me) => me.ComputeValue();

		// Token: 0x040005EC RID: 1516
		private readonly Cache<CsdlSemanticsIntConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsIntConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040005ED RID: 1517
		private static readonly Func<CsdlSemanticsIntConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsIntConstantExpression me) => me.ComputeErrors();
	}
}

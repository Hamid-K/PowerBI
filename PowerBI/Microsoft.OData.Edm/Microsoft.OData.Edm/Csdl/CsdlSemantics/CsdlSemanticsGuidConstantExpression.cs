using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000176 RID: 374
	internal class CsdlSemanticsGuidConstantExpression : CsdlSemanticsExpression, IEdmGuidConstantExpression, IEdmExpression, IEdmElement, IEdmGuidValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x0001C2F6 File Offset: 0x0001A4F6
		public CsdlSemanticsGuidConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0001C31D File Offset: 0x0001A51D
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0001C325 File Offset: 0x0001A525
		public Guid Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsGuidConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00003A59 File Offset: 0x00001C59
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.GuidConstant;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0001C339 File Offset: 0x0001A539
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0001C346 File Offset: 0x0001A546
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsGuidConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001C35C File Offset: 0x0001A55C
		private Guid ComputeValue()
		{
			Guid? guid;
			if (!EdmValueParser.TryParseGuid(this.expression.Value, out guid))
			{
				return Guid.Empty;
			}
			return guid.Value;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001C38C File Offset: 0x0001A58C
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

		// Token: 0x0400061A RID: 1562
		private readonly CsdlConstantExpression expression;

		// Token: 0x0400061B RID: 1563
		private readonly Cache<CsdlSemanticsGuidConstantExpression, Guid> valueCache = new Cache<CsdlSemanticsGuidConstantExpression, Guid>();

		// Token: 0x0400061C RID: 1564
		private static readonly Func<CsdlSemanticsGuidConstantExpression, Guid> ComputeValueFunc = (CsdlSemanticsGuidConstantExpression me) => me.ComputeValue();

		// Token: 0x0400061D RID: 1565
		private readonly Cache<CsdlSemanticsGuidConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsGuidConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x0400061E RID: 1566
		private static readonly Func<CsdlSemanticsGuidConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsGuidConstantExpression me) => me.ComputeErrors();
	}
}

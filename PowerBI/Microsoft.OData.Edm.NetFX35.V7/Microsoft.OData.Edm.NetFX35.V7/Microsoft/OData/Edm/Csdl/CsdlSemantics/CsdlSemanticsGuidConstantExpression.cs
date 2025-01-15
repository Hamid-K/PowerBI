using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000167 RID: 359
	internal class CsdlSemanticsGuidConstantExpression : CsdlSemanticsExpression, IEdmGuidConstantExpression, IEdmExpression, IEdmElement, IEdmGuidValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x0001A1ED File Offset: 0x000183ED
		public CsdlSemanticsGuidConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001A214 File Offset: 0x00018414
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0001A21C File Offset: 0x0001841C
		public Guid Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsGuidConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x000092ED File Offset: 0x000074ED
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.GuidConstant;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001A230 File Offset: 0x00018430
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0001A23D File Offset: 0x0001843D
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsGuidConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001A254 File Offset: 0x00018454
		private Guid ComputeValue()
		{
			Guid? guid;
			if (!EdmValueParser.TryParseGuid(this.expression.Value, out guid))
			{
				return Guid.Empty;
			}
			return guid.Value;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001A284 File Offset: 0x00018484
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

		// Token: 0x0400059F RID: 1439
		private readonly CsdlConstantExpression expression;

		// Token: 0x040005A0 RID: 1440
		private readonly Cache<CsdlSemanticsGuidConstantExpression, Guid> valueCache = new Cache<CsdlSemanticsGuidConstantExpression, Guid>();

		// Token: 0x040005A1 RID: 1441
		private static readonly Func<CsdlSemanticsGuidConstantExpression, Guid> ComputeValueFunc = (CsdlSemanticsGuidConstantExpression me) => me.ComputeValue();

		// Token: 0x040005A2 RID: 1442
		private readonly Cache<CsdlSemanticsGuidConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsGuidConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040005A3 RID: 1443
		private static readonly Func<CsdlSemanticsGuidConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsGuidConstantExpression me) => me.ComputeErrors();
	}
}

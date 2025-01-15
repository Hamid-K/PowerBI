using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000168 RID: 360
	internal class CsdlSemanticsBinaryConstantExpression : CsdlSemanticsExpression, IEdmBinaryConstantExpression, IEdmExpression, IEdmElement, IEdmBinaryValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x0001A300 File Offset: 0x00018500
		public CsdlSemanticsBinaryConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001A327 File Offset: 0x00018527
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001A32F File Offset: 0x0001852F
		public byte[] Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsBinaryConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00008D76 File Offset: 0x00006F76
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BinaryConstant;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0001A343 File Offset: 0x00018543
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0001A350 File Offset: 0x00018550
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsBinaryConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001A364 File Offset: 0x00018564
		private byte[] ComputeValue()
		{
			byte[] array;
			if (!EdmValueParser.TryParseBinary(this.expression.Value, out array))
			{
				return new byte[0];
			}
			return array;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001A390 File Offset: 0x00018590
		private IEnumerable<EdmError> ComputeErrors()
		{
			byte[] array;
			if (!EdmValueParser.TryParseBinary(this.expression.Value, out array))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidBinary, Strings.ValueParser_InvalidBinary(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x040005A4 RID: 1444
		private readonly CsdlConstantExpression expression;

		// Token: 0x040005A5 RID: 1445
		private readonly Cache<CsdlSemanticsBinaryConstantExpression, byte[]> valueCache = new Cache<CsdlSemanticsBinaryConstantExpression, byte[]>();

		// Token: 0x040005A6 RID: 1446
		private static readonly Func<CsdlSemanticsBinaryConstantExpression, byte[]> ComputeValueFunc = (CsdlSemanticsBinaryConstantExpression me) => me.ComputeValue();

		// Token: 0x040005A7 RID: 1447
		private readonly Cache<CsdlSemanticsBinaryConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsBinaryConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040005A8 RID: 1448
		private static readonly Func<CsdlSemanticsBinaryConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsBinaryConstantExpression me) => me.ComputeErrors();
	}
}

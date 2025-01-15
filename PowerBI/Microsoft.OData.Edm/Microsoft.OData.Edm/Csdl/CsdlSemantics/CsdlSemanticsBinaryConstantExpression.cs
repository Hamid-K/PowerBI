using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000177 RID: 375
	internal class CsdlSemanticsBinaryConstantExpression : CsdlSemanticsExpression, IEdmBinaryConstantExpression, IEdmExpression, IEdmElement, IEdmBinaryValue, IEdmPrimitiveValue, IEdmValue, IEdmCheckable
	{
		// Token: 0x06000A24 RID: 2596 RVA: 0x0001C408 File Offset: 0x0001A608
		public CsdlSemanticsBinaryConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0001C42F File Offset: 0x0001A62F
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0001C437 File Offset: 0x0001A637
		public byte[] Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsBinaryConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0000268E File Offset: 0x0000088E
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BinaryConstant;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0001C44B File Offset: 0x0001A64B
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0001C458 File Offset: 0x0001A658
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsBinaryConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001C46C File Offset: 0x0001A66C
		private byte[] ComputeValue()
		{
			byte[] array;
			if (!EdmValueParser.TryParseBinary(this.expression.Value, out array))
			{
				return new byte[0];
			}
			return array;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0001C498 File Offset: 0x0001A698
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

		// Token: 0x0400061F RID: 1567
		private readonly CsdlConstantExpression expression;

		// Token: 0x04000620 RID: 1568
		private readonly Cache<CsdlSemanticsBinaryConstantExpression, byte[]> valueCache = new Cache<CsdlSemanticsBinaryConstantExpression, byte[]>();

		// Token: 0x04000621 RID: 1569
		private static readonly Func<CsdlSemanticsBinaryConstantExpression, byte[]> ComputeValueFunc = (CsdlSemanticsBinaryConstantExpression me) => me.ComputeValue();

		// Token: 0x04000622 RID: 1570
		private readonly Cache<CsdlSemanticsBinaryConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsBinaryConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000623 RID: 1571
		private static readonly Func<CsdlSemanticsBinaryConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsBinaryConstantExpression me) => me.ComputeErrors();
	}
}

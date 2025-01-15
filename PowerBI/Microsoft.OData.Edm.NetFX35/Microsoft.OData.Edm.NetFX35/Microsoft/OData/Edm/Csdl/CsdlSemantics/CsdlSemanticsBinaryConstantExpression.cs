using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000089 RID: 137
	internal class CsdlSemanticsBinaryConstantExpression : CsdlSemanticsExpression, IEdmBinaryConstantExpression, IEdmExpression, IEdmBinaryValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000235 RID: 565 RVA: 0x00005E55 File Offset: 0x00004055
		public CsdlSemanticsBinaryConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00005E7C File Offset: 0x0000407C
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00005E84 File Offset: 0x00004084
		public byte[] Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsBinaryConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00005E98 File Offset: 0x00004098
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.BinaryConstant;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00005E9B File Offset: 0x0000409B
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00005EA8 File Offset: 0x000040A8
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00005EAB File Offset: 0x000040AB
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsBinaryConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00005EC0 File Offset: 0x000040C0
		private byte[] ComputeValue()
		{
			byte[] array;
			if (!EdmValueParser.TryParseBinary(this.expression.Value, out array))
			{
				return new byte[0];
			}
			return array;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00005EEC File Offset: 0x000040EC
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

		// Token: 0x040000D3 RID: 211
		private readonly CsdlConstantExpression expression;

		// Token: 0x040000D4 RID: 212
		private readonly Cache<CsdlSemanticsBinaryConstantExpression, byte[]> valueCache = new Cache<CsdlSemanticsBinaryConstantExpression, byte[]>();

		// Token: 0x040000D5 RID: 213
		private static readonly Func<CsdlSemanticsBinaryConstantExpression, byte[]> ComputeValueFunc = (CsdlSemanticsBinaryConstantExpression me) => me.ComputeValue();

		// Token: 0x040000D6 RID: 214
		private readonly Cache<CsdlSemanticsBinaryConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsBinaryConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x040000D7 RID: 215
		private static readonly Func<CsdlSemanticsBinaryConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsBinaryConstantExpression me) => me.ComputeErrors();
	}
}

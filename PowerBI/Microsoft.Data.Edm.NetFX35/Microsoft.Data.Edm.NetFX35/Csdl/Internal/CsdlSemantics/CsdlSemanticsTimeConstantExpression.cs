using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Validation;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x0200009B RID: 155
	internal class CsdlSemanticsTimeConstantExpression : CsdlSemanticsExpression, IEdmTimeConstantExpression, IEdmExpression, IEdmTimeValue, IEdmPrimitiveValue, IEdmValue, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600027A RID: 634 RVA: 0x000062A2 File Offset: 0x000044A2
		public CsdlSemanticsTimeConstantExpression(CsdlConstantExpression expression, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000062C9 File Offset: 0x000044C9
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000062D1 File Offset: 0x000044D1
		public TimeSpan Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsTimeConstantExpression.ComputeValueFunc, null);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000062E5 File Offset: 0x000044E5
		public IEdmTypeReference Type
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000062E8 File Offset: 0x000044E8
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.TimeConstant;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600027F RID: 639 RVA: 0x000062EC File Offset: 0x000044EC
		public EdmValueKind ValueKind
		{
			get
			{
				return this.expression.ValueKind;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000280 RID: 640 RVA: 0x000062F9 File Offset: 0x000044F9
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsTimeConstantExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00006310 File Offset: 0x00004510
		private TimeSpan ComputeValue()
		{
			TimeSpan? timeSpan;
			if (!EdmValueParser.TryParseTime(this.expression.Value, out timeSpan))
			{
				return TimeSpan.Zero;
			}
			return timeSpan.Value;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00006340 File Offset: 0x00004540
		private IEnumerable<EdmError> ComputeErrors()
		{
			TimeSpan? timeSpan;
			if (!EdmValueParser.TryParseTime(this.expression.Value, out timeSpan))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidTime, Strings.ValueParser_InvalidTime(this.expression.Value))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x0400011D RID: 285
		private readonly CsdlConstantExpression expression;

		// Token: 0x0400011E RID: 286
		private readonly Cache<CsdlSemanticsTimeConstantExpression, TimeSpan> valueCache = new Cache<CsdlSemanticsTimeConstantExpression, TimeSpan>();

		// Token: 0x0400011F RID: 287
		private static readonly Func<CsdlSemanticsTimeConstantExpression, TimeSpan> ComputeValueFunc = (CsdlSemanticsTimeConstantExpression me) => me.ComputeValue();

		// Token: 0x04000120 RID: 288
		private readonly Cache<CsdlSemanticsTimeConstantExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsTimeConstantExpression, IEnumerable<EdmError>>();

		// Token: 0x04000121 RID: 289
		private static readonly Func<CsdlSemanticsTimeConstantExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsTimeConstantExpression me) => me.ComputeErrors();
	}
}

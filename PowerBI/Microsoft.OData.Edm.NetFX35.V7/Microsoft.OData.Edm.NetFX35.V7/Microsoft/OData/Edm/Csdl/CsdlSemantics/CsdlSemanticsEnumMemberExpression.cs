using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000153 RID: 339
	internal class CsdlSemanticsEnumMemberExpression : CsdlSemanticsExpression, IEdmEnumMemberExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x060008E8 RID: 2280 RVA: 0x00019301 File Offset: 0x00017501
		public CsdlSemanticsEnumMemberExpression(CsdlEnumMemberExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0001932F File Offset: 0x0001752F
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00013A4F File Offset: 0x00011C4F
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMember;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00019337 File Offset: 0x00017537
		public IEnumerable<IEdmEnumMember> EnumMembers
		{
			get
			{
				return this.referencedCache.GetValue(this, CsdlSemanticsEnumMemberExpression.ComputeReferencedFunc, null);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0001934B File Offset: 0x0001754B
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsEnumMemberExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00019360 File Offset: 0x00017560
		private IEnumerable<IEdmEnumMember> ComputeReferenced()
		{
			IEnumerable<IEdmEnumMember> enumerable;
			if (!EdmEnumValueParser.TryParseEnumMember(this.expression.EnumMemberPath, base.Schema.Model, base.Location, out enumerable))
			{
				return null;
			}
			return enumerable;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00019398 File Offset: 0x00017598
		private IEnumerable<EdmError> ComputeErrors()
		{
			IEnumerable<IEdmEnumMember> enumerable;
			if (!EdmEnumValueParser.TryParseEnumMember(this.expression.EnumMemberPath, base.Schema.Model, base.Location, out enumerable))
			{
				return new EdmError[]
				{
					new EdmError(base.Location, EdmErrorCode.InvalidEnumMemberPath, Strings.CsdlParser_InvalidEnumMemberPath(this.expression.EnumMemberPath))
				};
			}
			return Enumerable.Empty<EdmError>();
		}

		// Token: 0x04000566 RID: 1382
		private readonly CsdlEnumMemberExpression expression;

		// Token: 0x04000567 RID: 1383
		private readonly IEdmEntityType bindingContext;

		// Token: 0x04000568 RID: 1384
		private readonly Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<IEdmEnumMember>> referencedCache = new Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<IEdmEnumMember>>();

		// Token: 0x04000569 RID: 1385
		private static readonly Func<CsdlSemanticsEnumMemberExpression, IEnumerable<IEdmEnumMember>> ComputeReferencedFunc = (CsdlSemanticsEnumMemberExpression me) => me.ComputeReferenced();

		// Token: 0x0400056A RID: 1386
		private readonly Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<EdmError>>();

		// Token: 0x0400056B RID: 1387
		private static readonly Func<CsdlSemanticsEnumMemberExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsEnumMemberExpression me) => me.ComputeErrors();
	}
}

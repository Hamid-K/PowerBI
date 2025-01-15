using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000162 RID: 354
	internal class CsdlSemanticsEnumMemberExpression : CsdlSemanticsExpression, IEdmEnumMemberExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x0001B3BC File Offset: 0x000195BC
		public CsdlSemanticsEnumMemberExpression(CsdlEnumMemberExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0001B3EA File Offset: 0x000195EA
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00011F33 File Offset: 0x00010133
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMember;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0001B3F2 File Offset: 0x000195F2
		public IEnumerable<IEdmEnumMember> EnumMembers
		{
			get
			{
				return this.referencedCache.GetValue(this, CsdlSemanticsEnumMemberExpression.ComputeReferencedFunc, null);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001B406 File Offset: 0x00019606
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsEnumMemberExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001B41C File Offset: 0x0001961C
		private IEnumerable<IEdmEnumMember> ComputeReferenced()
		{
			IEnumerable<IEdmEnumMember> enumerable;
			if (!EdmEnumValueParser.TryParseEnumMember(this.expression.EnumMemberPath, base.Schema.Model, base.Location, out enumerable))
			{
				return null;
			}
			return enumerable;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001B454 File Offset: 0x00019654
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

		// Token: 0x040005DF RID: 1503
		private readonly CsdlEnumMemberExpression expression;

		// Token: 0x040005E0 RID: 1504
		private readonly IEdmEntityType bindingContext;

		// Token: 0x040005E1 RID: 1505
		private readonly Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<IEdmEnumMember>> referencedCache = new Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<IEdmEnumMember>>();

		// Token: 0x040005E2 RID: 1506
		private static readonly Func<CsdlSemanticsEnumMemberExpression, IEnumerable<IEdmEnumMember>> ComputeReferencedFunc = (CsdlSemanticsEnumMemberExpression me) => me.ComputeReferenced();

		// Token: 0x040005E3 RID: 1507
		private readonly Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<EdmError>>();

		// Token: 0x040005E4 RID: 1508
		private static readonly Func<CsdlSemanticsEnumMemberExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsEnumMemberExpression me) => me.ComputeErrors();
	}
}

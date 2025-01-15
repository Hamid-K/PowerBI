using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200000C RID: 12
	internal class CsdlSemanticsEnumMemberExpression : CsdlSemanticsExpression, IEdmEnumMemberExpression, IEdmExpression, IEdmElement, IEdmCheckable
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002404 File Offset: 0x00000604
		public CsdlSemanticsEnumMemberExpression(CsdlEnumMemberExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.expression = expression;
			this.bindingContext = bindingContext;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002432 File Offset: 0x00000632
		public override CsdlElement Element
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000243A File Offset: 0x0000063A
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.EnumMember;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000243E File Offset: 0x0000063E
		public IEnumerable<IEdmEnumMember> EnumMembers
		{
			get
			{
				return this.referencedCache.GetValue(this, CsdlSemanticsEnumMemberExpression.ComputeReferencedFunc, null);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002452 File Offset: 0x00000652
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsEnumMemberExpression.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002468 File Offset: 0x00000668
		private IEnumerable<IEdmEnumMember> ComputeReferenced()
		{
			IEnumerable<IEdmEnumMember> enumerable;
			if (!EdmEnumValueParser.TryParseEnumMember(this.expression.EnumMemberPath, base.Schema.Model, base.Location, out enumerable))
			{
				return null;
			}
			return enumerable;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024A0 File Offset: 0x000006A0
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

		// Token: 0x0400000C RID: 12
		private readonly CsdlEnumMemberExpression expression;

		// Token: 0x0400000D RID: 13
		private readonly IEdmEntityType bindingContext;

		// Token: 0x0400000E RID: 14
		private readonly Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<IEdmEnumMember>> referencedCache = new Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<IEdmEnumMember>>();

		// Token: 0x0400000F RID: 15
		private static readonly Func<CsdlSemanticsEnumMemberExpression, IEnumerable<IEdmEnumMember>> ComputeReferencedFunc = (CsdlSemanticsEnumMemberExpression me) => me.ComputeReferenced();

		// Token: 0x04000010 RID: 16
		private readonly Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsEnumMemberExpression, IEnumerable<EdmError>>();

		// Token: 0x04000011 RID: 17
		private static readonly Func<CsdlSemanticsEnumMemberExpression, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsEnumMemberExpression me) => me.ComputeErrors();
	}
}

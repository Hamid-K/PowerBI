using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000059 RID: 89
	internal class CsdlSemanticsPathExpression : CsdlSemanticsExpression, IEdmPathExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00003C17 File Offset: 0x00001E17
		public CsdlSemanticsPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.Expression = expression;
			this.BindingContext = bindingContext;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00003C3A File Offset: 0x00001E3A
		public override CsdlElement Element
		{
			get
			{
				return this.Expression;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00003C42 File Offset: 0x00001E42
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00003C46 File Offset: 0x00001E46
		public IEnumerable<string> Path
		{
			get
			{
				return this.PathCache.GetValue(this, CsdlSemanticsPathExpression.ComputePathFunc, null);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00003C5C File Offset: 0x00001E5C
		private IEnumerable<string> ComputePath()
		{
			return this.Expression.Path.Split(new char[] { '/' }, 0);
		}

		// Token: 0x04000069 RID: 105
		protected readonly CsdlPathExpression Expression;

		// Token: 0x0400006A RID: 106
		protected readonly IEdmEntityType BindingContext;

		// Token: 0x0400006B RID: 107
		protected readonly Cache<CsdlSemanticsPathExpression, IEnumerable<string>> PathCache = new Cache<CsdlSemanticsPathExpression, IEnumerable<string>>();

		// Token: 0x0400006C RID: 108
		protected static readonly Func<CsdlSemanticsPathExpression, IEnumerable<string>> ComputePathFunc = (CsdlSemanticsPathExpression me) => me.ComputePath();
	}
}

using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017A RID: 378
	internal class CsdlSemanticsPathExpression : CsdlSemanticsExpression, IEdmPathExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x0001B2EE File Offset: 0x000194EE
		public CsdlSemanticsPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.Expression = expression;
			this.BindingContext = bindingContext;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0001B311 File Offset: 0x00019511
		public override CsdlElement Element
		{
			get
			{
				return this.Expression;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0000BFE1 File Offset: 0x0000A1E1
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0001B319 File Offset: 0x00019519
		public IEnumerable<string> PathSegments
		{
			get
			{
				return this.PathCache.GetValue(this, CsdlSemanticsPathExpression.ComputePathFunc, null);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0001B32D File Offset: 0x0001952D
		public string Path
		{
			get
			{
				return this.Expression.Path;
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001B33A File Offset: 0x0001953A
		private IEnumerable<string> ComputePath()
		{
			return this.Expression.Path.Split(new char[] { '/' }, 0);
		}

		// Token: 0x040005F3 RID: 1523
		protected readonly CsdlPathExpression Expression;

		// Token: 0x040005F4 RID: 1524
		protected readonly IEdmEntityType BindingContext;

		// Token: 0x040005F5 RID: 1525
		protected readonly Cache<CsdlSemanticsPathExpression, IEnumerable<string>> PathCache = new Cache<CsdlSemanticsPathExpression, IEnumerable<string>>();

		// Token: 0x040005F6 RID: 1526
		protected static readonly Func<CsdlSemanticsPathExpression, IEnumerable<string>> ComputePathFunc = (CsdlSemanticsPathExpression me) => me.ComputePath();
	}
}

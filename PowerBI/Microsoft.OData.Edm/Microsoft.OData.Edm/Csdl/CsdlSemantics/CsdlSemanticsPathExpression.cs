using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000189 RID: 393
	internal class CsdlSemanticsPathExpression : CsdlSemanticsExpression, IEdmPathExpression, IEdmExpression, IEdmElement
	{
		// Token: 0x06000ABE RID: 2750 RVA: 0x0001D3F3 File Offset: 0x0001B5F3
		public CsdlSemanticsPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(schema, expression)
		{
			this.Expression = expression;
			this.BindingContext = bindingContext;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0001D416 File Offset: 0x0001B616
		public override CsdlElement Element
		{
			get
			{
				return this.Expression;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0000462D File Offset: 0x0000282D
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.Path;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0001D41E File Offset: 0x0001B61E
		public IEnumerable<string> PathSegments
		{
			get
			{
				return this.PathCache.GetValue(this, CsdlSemanticsPathExpression.ComputePathFunc, null);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0001D432 File Offset: 0x0001B632
		public string Path
		{
			get
			{
				return this.Expression.Path;
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001D43F File Offset: 0x0001B63F
		private IEnumerable<string> ComputePath()
		{
			return this.Expression.Path.Split(new char[] { '/' }, StringSplitOptions.None);
		}

		// Token: 0x0400066F RID: 1647
		protected readonly CsdlPathExpression Expression;

		// Token: 0x04000670 RID: 1648
		protected readonly IEdmEntityType BindingContext;

		// Token: 0x04000671 RID: 1649
		protected readonly Cache<CsdlSemanticsPathExpression, IEnumerable<string>> PathCache = new Cache<CsdlSemanticsPathExpression, IEnumerable<string>>();

		// Token: 0x04000672 RID: 1650
		protected static readonly Func<CsdlSemanticsPathExpression, IEnumerable<string>> ComputePathFunc = (CsdlSemanticsPathExpression me) => me.ComputePath();
	}
}

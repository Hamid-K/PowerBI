using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000D0 RID: 208
	public interface ILetExpression : IExpression, ISyntaxNode, IDeclarator
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600032A RID: 810
		IList<VariableInitializer> Variables { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600032B RID: 811
		IExpression Expression { get; }
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000CE RID: 206
	public interface IInvocationExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000325 RID: 805
		IExpression Function { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000326 RID: 806
		IList<IExpression> Arguments { get; }
	}
}

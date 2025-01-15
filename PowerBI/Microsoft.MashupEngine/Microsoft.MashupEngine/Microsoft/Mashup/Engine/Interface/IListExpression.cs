using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000C3 RID: 195
	public interface IListExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600030F RID: 783
		IList<IExpression> Members { get; }
	}
}

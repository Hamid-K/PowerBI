using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000C1 RID: 193
	public interface IRangeListExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600030C RID: 780
		IList<IRangeExpression> Members { get; }
	}
}

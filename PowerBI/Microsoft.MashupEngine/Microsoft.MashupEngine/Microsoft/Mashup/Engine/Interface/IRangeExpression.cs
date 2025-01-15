using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000C2 RID: 194
	public interface IRangeExpression : ISyntaxNode
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600030D RID: 781
		IExpression Lower { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600030E RID: 782
		IExpression Upper { get; }
	}
}

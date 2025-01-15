using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000B6 RID: 182
	public interface ITableTypeExpression : IExpression, ISyntaxNode
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002FF RID: 767
		IExpression RowType { get; }
	}
}

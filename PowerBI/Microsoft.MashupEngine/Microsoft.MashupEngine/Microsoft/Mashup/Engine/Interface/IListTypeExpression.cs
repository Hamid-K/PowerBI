using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000B5 RID: 181
	public interface IListTypeExpression : IExpression, ISyntaxNode
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002FE RID: 766
		IExpression ItemType { get; }
	}
}

using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000C5 RID: 197
	public interface IFieldAccessExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000310 RID: 784
		IExpression Expression { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000311 RID: 785
		Identifier MemberName { get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000312 RID: 786
		bool IsOptional { get; }
	}
}

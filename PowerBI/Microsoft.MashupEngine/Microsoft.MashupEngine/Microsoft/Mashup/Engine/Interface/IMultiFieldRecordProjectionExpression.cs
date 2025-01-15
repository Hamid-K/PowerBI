using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000C6 RID: 198
	public interface IMultiFieldRecordProjectionExpression : IExpression, ISyntaxNode
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000313 RID: 787
		IExpression Expression { get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000314 RID: 788
		IList<Identifier> MemberNames { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000315 RID: 789
		bool IsOptional { get; }
	}
}

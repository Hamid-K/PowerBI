using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ResolvedDataShapeBindings
{
	// Token: 0x020000AC RID: 172
	public sealed class ResolvedDataShapeBindingAxisExpansionLevel
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x0000B738 File Offset: 0x00009938
		public ResolvedDataShapeBindingAxisExpansionLevel(IReadOnlyList<ResolvedQueryExpression> expressions, ExpansionDefaultState @default)
		{
			this.Expressions = expressions;
			this.Default = @default;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000B74E File Offset: 0x0000994E
		public IReadOnlyList<ResolvedQueryExpression> Expressions { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000B756 File Offset: 0x00009956
		public ExpansionDefaultState Default { get; }
	}
}

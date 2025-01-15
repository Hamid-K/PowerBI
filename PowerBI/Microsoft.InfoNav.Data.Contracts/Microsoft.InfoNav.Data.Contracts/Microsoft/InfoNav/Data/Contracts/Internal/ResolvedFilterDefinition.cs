using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200020C RID: 524
	[ImmutableObject(true)]
	public sealed class ResolvedFilterDefinition
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x0001D6BA File Offset: 0x0001B8BA
		internal ResolvedFilterDefinition(IReadOnlyList<ResolvedQuerySource> from, IReadOnlyList<ResolvedQueryFilter> where)
		{
			this._from = from;
			this._where = where;
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0001D6D0 File Offset: 0x0001B8D0
		public IReadOnlyList<ResolvedQuerySource> From
		{
			get
			{
				return this._from;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x0001D6D8 File Offset: 0x0001B8D8
		public IReadOnlyList<ResolvedQueryFilter> Where
		{
			get
			{
				return this._where;
			}
		}

		// Token: 0x0400071A RID: 1818
		private readonly IReadOnlyList<ResolvedQuerySource> _from;

		// Token: 0x0400071B RID: 1819
		private readonly IReadOnlyList<ResolvedQueryFilter> _where;
	}
}

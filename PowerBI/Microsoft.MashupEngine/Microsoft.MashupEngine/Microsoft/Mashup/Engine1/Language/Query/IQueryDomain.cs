using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017D5 RID: 6101
	public interface IQueryDomain
	{
		// Token: 0x06009A44 RID: 39492
		bool IsCompatibleWith(IQueryDomain domain);

		// Token: 0x170027B5 RID: 10165
		// (get) Token: 0x06009A45 RID: 39493
		bool CanIndex { get; }

		// Token: 0x06009A46 RID: 39494
		Query Optimize(Query query);
	}
}

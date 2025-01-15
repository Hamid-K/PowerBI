using System;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020000D1 RID: 209
	public interface IMigrationMetadata
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001064 RID: 4196
		string Id { get; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001065 RID: 4197
		string Source { get; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001066 RID: 4198
		string Target { get; }
	}
}

using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000286 RID: 646
	internal static class DiscoverySchemaChanges
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0003EC9B File Offset: 0x0003DC9B
		// (set) Token: 0x060016EF RID: 5871 RVA: 0x0003ECA2 File Offset: 0x0003DCA2
		internal static DiscoverySchemaChanges.SchemaChange SearchMailboxesExtendedData { get; private set; } = new DiscoverySchemaChanges.SchemaChange("15.0.730.0");

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0003ECAA File Offset: 0x0003DCAA
		// (set) Token: 0x060016F1 RID: 5873 RVA: 0x0003ECB1 File Offset: 0x0003DCB1
		internal static DiscoverySchemaChanges.SchemaChange SearchMailboxesAdditionalSearchScopes { get; private set; } = new DiscoverySchemaChanges.SchemaChange("15.0.730.0");

		// Token: 0x02000287 RID: 647
		internal sealed class SchemaChange
		{
			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0003ECB9 File Offset: 0x0003DCB9
			// (set) Token: 0x060016F3 RID: 5875 RVA: 0x0003ECC1 File Offset: 0x0003DCC1
			internal long MinimumServerVersion { get; private set; }

			// Token: 0x060016F4 RID: 5876 RVA: 0x0003ECCA File Offset: 0x0003DCCA
			internal SchemaChange(long serverVersion)
			{
				this.MinimumServerVersion = serverVersion;
			}

			// Token: 0x060016F5 RID: 5877 RVA: 0x0003ECDC File Offset: 0x0003DCDC
			internal SchemaChange(string serverBuild)
			{
				Version version = new Version(serverBuild);
				this.MinimumServerVersion = (long)((version.Build & 32767) | ((version.Minor & 63) << 16) | ((version.Major & 63) << 22) | 1879080960);
			}

			// Token: 0x060016F6 RID: 5878 RVA: 0x0003ED29 File Offset: 0x0003DD29
			internal bool IsCompatible(IDiscoveryVersionable versionable)
			{
				return versionable.ServerVersion == 0L || versionable.ServerVersion >= this.MinimumServerVersion;
			}
		}
	}
}

using System;
using System.Globalization;
using System.Linq;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000045 RID: 69
	public static class ConnectionsSettingsVersion
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00006F58 File Offset: 0x00005158
		public static int GetVersion(ConnectionsSettingsStorage storage)
		{
			int num = 1;
			if (storage.RemoteArtifacts != null && storage.RemoteArtifacts.Count > 0)
			{
				num = Math.Max(num, 3);
			}
			if (!string.IsNullOrEmpty(storage.OriginalWorkspaceObjectId))
			{
				num = Math.Max(num, 6);
			}
			if (num < 2 && storage.Connections != null && storage.Connections.Count > 0)
			{
				foreach (ConnectionPropertiesStorage connectionPropertiesStorage in storage.Connections)
				{
					if (connectionPropertiesStorage.ConnectionType == "pbiServiceLive" && string.IsNullOrEmpty(connectionPropertiesStorage.PbiServiceGroupId))
					{
						num = Math.Max(num, 2);
					}
					else if (connectionPropertiesStorage.ConnectionType == "pbiServiceXmlaStyleLive")
					{
						num = Math.Max(num, 4);
					}
					else if (connectionPropertiesStorage.ConnectionType == "atScaleDatabaseLive")
					{
						num = Math.Max(num, 5);
					}
				}
			}
			return num;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000705C File Offset: 0x0000525C
		public static void ValidateVersion(ConnectionsSettingsStorage storage)
		{
			if (storage == null)
			{
				return;
			}
			if (storage.Version > ConnectionsSettingsVersion.MaxSupportedVersion)
			{
				throw new NewerPackagePartException("connections", storage.Version.ToString(CultureInfo.InvariantCulture), ConnectionsSettingsVersion.MaxSupportedVersion.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x04000113 RID: 275
		internal const int DefaultVersion = 1;

		// Token: 0x04000114 RID: 276
		internal const int CertifiedDatasetVersion = 2;

		// Token: 0x04000115 RID: 277
		internal const int RemoteArtifactsVersion = 3;

		// Token: 0x04000116 RID: 278
		internal const int XmlaStyleLiveConnectionTypeVersion = 4;

		// Token: 0x04000117 RID: 279
		internal const int AtScaleDatabaseLiveConnectionTypeVersion = 5;

		// Token: 0x04000118 RID: 280
		internal const int OriginalWorkspaceObjectIdVersion = 6;

		// Token: 0x04000119 RID: 281
		private static readonly int[] validVersions = new int[] { 1, 1, 2, 3, 4, 5, 6 };

		// Token: 0x0400011A RID: 282
		internal static readonly int MaxSupportedVersion = ConnectionsSettingsVersion.validVersions.Max();

		// Token: 0x0400011B RID: 283
		private const string connectionsPart = "connections";
	}
}

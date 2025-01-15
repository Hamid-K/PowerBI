using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000041 RID: 65
	public sealed class ConnectionsSettings
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00006A85 File Offset: 0x00004C85
		public ConnectionsSettings(Dictionary<string, ConnectionProperties> connections = null, List<RemoteArtifactProperties> remoteArtifacts = null, string originalWorkspaceObjectId = null)
		{
			this.Connections = connections;
			this.remoteArtifacts = remoteArtifacts;
			this.OriginalWorkspaceObjectId = originalWorkspaceObjectId;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00006AA2 File Offset: 0x00004CA2
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00006AAA File Offset: 0x00004CAA
		public Dictionary<string, ConnectionProperties> Connections { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00006AB3 File Offset: 0x00004CB3
		public IReadOnlyList<RemoteArtifactProperties> RemoteArtifacts
		{
			get
			{
				return this.remoteArtifacts;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00006ABB File Offset: 0x00004CBB
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00006AC3 File Offset: 0x00004CC3
		public string OriginalWorkspaceObjectId { get; set; }

		// Token: 0x060001DE RID: 478 RVA: 0x00006ACC File Offset: 0x00004CCC
		public RemoteArtifactProperties GetRemoteArtifact()
		{
			if (this.remoteArtifacts == null || this.remoteArtifacts.Count == 0)
			{
				return null;
			}
			return this.remoteArtifacts[0];
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public bool TryAddOrUpdateRemoteArtifact(RemoteArtifactProperties artifact)
		{
			this.remoteArtifacts = this.remoteArtifacts ?? new List<RemoteArtifactProperties>();
			if (this.remoteArtifacts.Count == 0)
			{
				this.remoteArtifacts.Add(artifact);
				return true;
			}
			if (this.remoteArtifacts[0].Equals(artifact))
			{
				return false;
			}
			this.remoteArtifacts[0] = artifact;
			return true;
		}

		// Token: 0x0400010A RID: 266
		private List<RemoteArtifactProperties> remoteArtifacts;
	}
}

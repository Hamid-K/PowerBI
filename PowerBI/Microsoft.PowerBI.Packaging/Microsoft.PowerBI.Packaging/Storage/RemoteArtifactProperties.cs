using System;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x0200004E RID: 78
	public sealed class RemoteArtifactProperties : IEquatable<RemoteArtifactProperties>
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000075B6 File Offset: 0x000057B6
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000075BE File Offset: 0x000057BE
		public string DatasetId { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000075C7 File Offset: 0x000057C7
		// (set) Token: 0x0600023B RID: 571 RVA: 0x000075CF File Offset: 0x000057CF
		public string ReportId { get; set; }

		// Token: 0x0600023C RID: 572 RVA: 0x000075D8 File Offset: 0x000057D8
		public bool Equals(RemoteArtifactProperties other)
		{
			return other != null && RemoteArtifactProperties.idComparer.Equals(this.DatasetId, other.DatasetId) && RemoteArtifactProperties.idComparer.Equals(this.ReportId, other.ReportId);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000760D File Offset: 0x0000580D
		public override int GetHashCode()
		{
			return RemoteArtifactProperties.idComparer.GetHashCode(this.DatasetId) ^ RemoteArtifactProperties.idComparer.GetHashCode(this.ReportId);
		}

		// Token: 0x04000129 RID: 297
		private static readonly StringComparer idComparer = StringComparer.OrdinalIgnoreCase;
	}
}

using System;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000119 RID: 281
	public interface IAuditContext
	{
		// Token: 0x06000788 RID: 1928
		void Bind(IAuditRequest auditRequest);

		// Token: 0x06000789 RID: 1929
		bool ShouldAudit();

		// Token: 0x0600078A RID: 1930
		bool IsAuditingEnabled();

		// Token: 0x0600078B RID: 1931
		void Add(IAuditable auditable);

		// Token: 0x0600078C RID: 1932
		T ExtractAuditable<T>(AuditArtifactStatus auditArtifactStatus = AuditArtifactStatus.Required) where T : IAuditable;

		// Token: 0x0600078D RID: 1933
		string GetActivityName();

		// Token: 0x0600078E RID: 1934
		void RecordActivity(bool isActivityCompletedSuccessfully);
	}
}

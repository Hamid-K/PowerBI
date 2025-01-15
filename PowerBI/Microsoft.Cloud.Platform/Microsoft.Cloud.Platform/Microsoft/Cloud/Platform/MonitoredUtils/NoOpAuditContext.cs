using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000117 RID: 279
	public class NoOpAuditContext : IAuditContext
	{
		// Token: 0x06000780 RID: 1920 RVA: 0x0001A0C4 File Offset: 0x000182C4
		public void Add(IAuditable auditable)
		{
			TraceSourceBase<AuditingTrace>.Tracer.TraceWarning("Attempt made on GeneralAuditContext: {0} {1}", new object[]
			{
				"Add",
				(auditable != null) ? auditable.GetType().ToString() : string.Empty
			});
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool ShouldAudit()
		{
			return false;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsAuditingEnabled()
		{
			return false;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void RecordActivity(bool isActivityCompletedSuccessfully)
		{
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001A0FC File Offset: 0x000182FC
		public T ExtractAuditable<T>(AuditArtifactStatus auditArtifactStatus = AuditArtifactStatus.Required) where T : IAuditable
		{
			return default(T);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001A112 File Offset: 0x00018312
		public string GetActivityName()
		{
			return string.Empty;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001A119 File Offset: 0x00018319
		public void Bind(IAuditRequest auditRequest)
		{
			TraceSourceBase<AuditingTrace>.Tracer.TraceWarning("Attempt made on GeneralAuditContext: {0} {1}", new object[]
			{
				"Bind",
				(auditRequest != null) ? auditRequest.AuditOperation : string.Empty
			});
		}

		// Token: 0x040002B4 RID: 692
		private const string AddOperation = "Add";

		// Token: 0x040002B5 RID: 693
		private const string BindOperation = "Bind";
	}
}

using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal sealed class VersionMismatchException : ReportCatalogException
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00003B35 File Offset: 0x00001D35
		public VersionMismatchException(Guid reportID, bool isPermanentSnapshot)
			: base(ErrorCode.rsSnapshotVersionMismatch, ErrorStringsWrapper.rsSnapshotVersionMismatch, null, "version mismatch found", TraceLevel.Verbose, Array.Empty<object>())
		{
			this.m_reportID = reportID;
			this.m_isPermanentSnapshot = isPermanentSnapshot;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003B5E File Offset: 0x00001D5E
		private VersionMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00003B68 File Offset: 0x00001D68
		public Guid ReportID
		{
			get
			{
				return this.m_reportID;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003B70 File Offset: 0x00001D70
		public bool IsPermanentSnapshot
		{
			get
			{
				return this.m_isPermanentSnapshot;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003B78 File Offset: 0x00001D78
		protected override bool TraceFullException
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000010 RID: 16
		private Guid m_reportID;

		// Token: 0x04000011 RID: 17
		private bool m_isPermanentSnapshot;
	}
}

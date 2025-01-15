using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002AF RID: 687
	internal sealed class ExecuteQueryCancelableStep : CancelableLibraryStep
	{
		// Token: 0x06001910 RID: 6416 RVA: 0x00064CB4 File Offset: 0x00062EB4
		private ExecuteQueryCancelableStep(RSService service, ExternalItemPath modelName, string query, NameValueCollection parameters, int timeout, IDbConnectionPool connectionPool)
			: base(UrlFriendlyUIDGenerator.Create(), modelName, JobActionEnum.ExecuteQuery, Microsoft.ReportingServices.Diagnostics.JobType.UserJobType, service.UserContext)
		{
			this.m_service = service;
			this.m_modelName = modelName;
			this.m_query = query;
			this.m_parameters = parameters;
			this.m_timeout = timeout;
			this.m_connectionPool = connectionPool;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00064D08 File Offset: 0x00062F08
		public static RSStream ExecuteQuery(RSService rs, ExternalItemPath modelName, string query, NameValueCollection parameters, int timeout, IDbConnectionPool connectionPool)
		{
			RSStream primaryStream;
			using (ExecuteQueryCancelableStep executeQueryCancelableStep = new ExecuteQueryCancelableStep(rs, modelName, query, parameters, timeout, connectionPool))
			{
				executeQueryCancelableStep.ExecuteWrapper();
				primaryStream = executeQueryCancelableStep.PrimaryStream;
			}
			return primaryStream;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00064D50 File Offset: 0x00062F50
		protected override void Execute()
		{
			this.m_primarystream = this.m_service.ExecuteQuery(this.m_modelName, this.m_query, this.m_parameters, this.m_timeout, this.m_connectionPool);
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x00064D81 File Offset: 0x00062F81
		public RSStream PrimaryStream
		{
			get
			{
				return this.m_primarystream;
			}
		}

		// Token: 0x040008FF RID: 2303
		private RSService m_service;

		// Token: 0x04000900 RID: 2304
		private ExternalItemPath m_modelName;

		// Token: 0x04000901 RID: 2305
		private string m_query;

		// Token: 0x04000902 RID: 2306
		private NameValueCollection m_parameters;

		// Token: 0x04000903 RID: 2307
		private int m_timeout;

		// Token: 0x04000904 RID: 2308
		private RSStream m_primarystream;

		// Token: 0x04000905 RID: 2309
		private IDbConnectionPool m_connectionPool;
	}
}

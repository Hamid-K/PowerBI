using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002BA RID: 698
	internal sealed class ExecuteQueriesCancelableStep : CancelableLibraryStep
	{
		// Token: 0x06001930 RID: 6448 RVA: 0x000652DC File Offset: 0x000634DC
		internal ExecuteQueriesCancelableStep(RSService service, string itemPath, string dataSourceName, CatalogItemContext context, string sessionId, Stream inputStream, Stream outputStream, IList<string> responseFlags, bool isClientCancelable)
			: base(UrlFriendlyUIDGenerator.Create(), context.ItemPath, JobActionEnum.Render, Microsoft.ReportingServices.Diagnostics.JobType.UserJobType, service.UserContext)
		{
			this.m_context = context;
			this.m_itemPath = itemPath;
			this.m_dataSourceName = dataSourceName;
			this.m_service = service;
			this.m_sessionId = sessionId;
			this.m_inputStream = inputStream;
			this.m_outputStream = outputStream;
			this.m_responseFlags = responseFlags;
			this.m_isClientCancelable = isClientCancelable;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0006534C File Offset: 0x0006354C
		protected override void Execute()
		{
			using (ExecuteQueriesAction executeQueriesAction = new ExecuteQueriesAction(new RenderEditRequest(this.m_service.UserName, this.m_sessionId), this.m_itemPath, this.m_dataSourceName, this.m_context, this.m_inputStream, this.m_outputStream, this.m_responseFlags, this.m_service, base.JobId, this.m_isClientCancelable))
			{
				executeQueriesAction.Execute();
			}
		}

		// Token: 0x0400092E RID: 2350
		private readonly RSService m_service;

		// Token: 0x0400092F RID: 2351
		private readonly string m_itemPath;

		// Token: 0x04000930 RID: 2352
		private readonly string m_dataSourceName;

		// Token: 0x04000931 RID: 2353
		private readonly CatalogItemContext m_context;

		// Token: 0x04000932 RID: 2354
		private readonly string m_sessionId;

		// Token: 0x04000933 RID: 2355
		private readonly Stream m_inputStream;

		// Token: 0x04000934 RID: 2356
		private readonly Stream m_outputStream;

		// Token: 0x04000935 RID: 2357
		private readonly IList<string> m_responseFlags;

		// Token: 0x04000936 RID: 2358
		private readonly bool m_isClientCancelable;
	}
}

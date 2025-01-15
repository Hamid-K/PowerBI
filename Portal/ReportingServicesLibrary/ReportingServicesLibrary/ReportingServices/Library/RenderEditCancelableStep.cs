using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002B9 RID: 697
	internal sealed class RenderEditCancelableStep : CancelableLibraryStep
	{
		// Token: 0x0600192E RID: 6446 RVA: 0x00065210 File Offset: 0x00063410
		internal RenderEditCancelableStep(RSService rs, CatalogItemContext ctx, string sessionId, Stream inputStream, Stream outputStream, IList<string> responseFlags)
			: base(UrlFriendlyUIDGenerator.Create(), ctx.ItemPath, JobActionEnum.Render, Microsoft.ReportingServices.Diagnostics.JobType.UserJobType, rs.UserContext)
		{
			this.m_ctx = ctx;
			this.m_rs = rs;
			this.m_sessionId = sessionId;
			this.m_inputStream = inputStream;
			this.m_outputStream = outputStream;
			this.m_responseFlags = responseFlags;
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00065268 File Offset: 0x00063468
		protected override void Execute()
		{
			RenderEditRequest renderEditRequest = new RenderEditRequest(this.m_rs.UserName, this.m_sessionId);
			using (RenderEditAction renderEditAction = new RenderEditAction(this.m_inputStream, this.m_outputStream, this.m_responseFlags, renderEditRequest, this.m_rs, this.m_ctx, base.JobId))
			{
				renderEditAction.Execute();
			}
		}

		// Token: 0x04000928 RID: 2344
		private readonly CatalogItemContext m_ctx;

		// Token: 0x04000929 RID: 2345
		private readonly RSService m_rs;

		// Token: 0x0400092A RID: 2346
		private readonly string m_sessionId;

		// Token: 0x0400092B RID: 2347
		private readonly Stream m_inputStream;

		// Token: 0x0400092C RID: 2348
		private readonly Stream m_outputStream;

		// Token: 0x0400092D RID: 2349
		private readonly IList<string> m_responseFlags;
	}
}

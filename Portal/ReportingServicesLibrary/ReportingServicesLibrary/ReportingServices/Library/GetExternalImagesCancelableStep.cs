using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002BB RID: 699
	internal sealed class GetExternalImagesCancelableStep : CancelableLibraryStep
	{
		// Token: 0x06001932 RID: 6450 RVA: 0x000653D0 File Offset: 0x000635D0
		internal GetExternalImagesCancelableStep(string sessionId, Stream inputStream, Stream outputStream, IList<string> responseFlags, CatalogItemContext context, RSService service)
			: base(UrlFriendlyUIDGenerator.Create(), context.ItemPath, JobActionEnum.Render, Microsoft.ReportingServices.Diagnostics.JobType.UserJobType, service.UserContext)
		{
			this.m_sessionId = sessionId;
			this.m_inputStream = inputStream;
			this.m_outputStream = outputStream;
			this.m_responseFlags = responseFlags;
			this.m_service = service;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00065424 File Offset: 0x00063624
		protected override void Execute()
		{
			using (GetExternalImagesAction getExternalImagesAction = new GetExternalImagesAction(new RenderEditRequest(this.m_service.UserName, this.m_sessionId), this.m_inputStream, this.m_outputStream, this.m_responseFlags, this.m_service, base.JobId))
			{
				getExternalImagesAction.Execute();
			}
		}

		// Token: 0x04000937 RID: 2359
		private readonly string m_sessionId;

		// Token: 0x04000938 RID: 2360
		private readonly Stream m_inputStream;

		// Token: 0x04000939 RID: 2361
		private readonly Stream m_outputStream;

		// Token: 0x0400093A RID: 2362
		private readonly IList<string> m_responseFlags;

		// Token: 0x0400093B RID: 2363
		private readonly RSService m_service;
	}
}

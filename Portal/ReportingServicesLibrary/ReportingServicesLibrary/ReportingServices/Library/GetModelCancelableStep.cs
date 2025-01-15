using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002BF RID: 703
	internal sealed class GetModelCancelableStep : CancelableLibraryStep
	{
		// Token: 0x06001940 RID: 6464 RVA: 0x00065ECC File Offset: 0x000640CC
		internal GetModelCancelableStep(IRenderEditSession session, string itemPath, string dataSourceName, string modelMetadataVersion, Stream inputStream, Stream outputStream, IMetadataStore responseMetadata, IList<string> responseFlags, RSService service, CatalogItemContext itemContext)
			: base(UrlFriendlyUIDGenerator.Create(), itemContext.ItemPath, JobActionEnum.GetUserModel, Microsoft.ReportingServices.Diagnostics.JobType.UserJobType, service.UserContext)
		{
			this.m_service = service;
			this.m_itemPath = itemPath;
			this.m_dataSourceName = dataSourceName;
			this.m_context = itemContext;
			this.m_session = session;
			this.m_inputStream = inputStream;
			this.m_outputStream = outputStream;
			this.m_modelMetadataVersion = modelMetadataVersion;
			this.m_responseMetadata = responseMetadata;
			this.m_responseFlags = responseFlags;
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00065F48 File Offset: 0x00064148
		protected override void Execute()
		{
			using (GetModelAction getModelAction = new GetModelAction(this.m_session, this.m_itemPath, this.m_dataSourceName, this.m_modelMetadataVersion, this.m_inputStream, this.m_outputStream, this.m_responseMetadata, this.m_responseFlags, this.m_service, this.m_context))
			{
				getModelAction.Execute();
			}
		}

		// Token: 0x0400093C RID: 2364
		private readonly RSService m_service;

		// Token: 0x0400093D RID: 2365
		private readonly string m_itemPath;

		// Token: 0x0400093E RID: 2366
		private readonly string m_dataSourceName;

		// Token: 0x0400093F RID: 2367
		private readonly CatalogItemContext m_context;

		// Token: 0x04000940 RID: 2368
		private readonly IRenderEditSession m_session;

		// Token: 0x04000941 RID: 2369
		private readonly Stream m_inputStream;

		// Token: 0x04000942 RID: 2370
		private readonly Stream m_outputStream;

		// Token: 0x04000943 RID: 2371
		private readonly string m_modelMetadataVersion;

		// Token: 0x04000944 RID: 2372
		private readonly IMetadataStore m_responseMetadata;

		// Token: 0x04000945 RID: 2373
		private readonly IList<string> m_responseFlags;
	}
}

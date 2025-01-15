using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002B0 RID: 688
	internal sealed class GetUserModelCancelableStep : CancelableLibraryStep
	{
		// Token: 0x06001914 RID: 6420 RVA: 0x00064D89 File Offset: 0x00062F89
		private GetUserModelCancelableStep(RSService service, ExternalItemPath modelPath, string perspectiveID)
			: base(UrlFriendlyUIDGenerator.Create(), modelPath, JobActionEnum.GetUserModel, Microsoft.ReportingServices.Diagnostics.JobType.UserJobType, service.UserContext)
		{
			this.m_service = service;
			this.m_modelPath = modelPath;
			this.m_perspectiveID = perspectiveID;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00064DB8 File Offset: 0x00062FB8
		public static RSStream GetUserModel(RSService rs, ExternalItemPath modelPath, string perspectiveID)
		{
			RSStream primaryStream;
			using (GetUserModelCancelableStep getUserModelCancelableStep = new GetUserModelCancelableStep(rs, modelPath, perspectiveID))
			{
				getUserModelCancelableStep.ExecuteWrapper();
				primaryStream = getUserModelCancelableStep.PrimaryStream;
			}
			return primaryStream;
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00064DF8 File Offset: 0x00062FF8
		protected override void Execute()
		{
			this.m_primarystream = this.m_service.GetUserModelStreamable(this.m_modelPath.Value, this.m_perspectiveID);
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x00064E1C File Offset: 0x0006301C
		public RSStream PrimaryStream
		{
			get
			{
				return this.m_primarystream;
			}
		}

		// Token: 0x04000906 RID: 2310
		private RSService m_service;

		// Token: 0x04000907 RID: 2311
		private ExternalItemPath m_modelPath;

		// Token: 0x04000908 RID: 2312
		private string m_perspectiveID;

		// Token: 0x04000909 RID: 2313
		private RSStream m_primarystream;
	}
}

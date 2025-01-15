using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000657 RID: 1623
	public sealed class DataSetResult
	{
		// Token: 0x06005A5D RID: 23133 RVA: 0x001727B8 File Offset: 0x001709B8
		public DataSetResult(ParameterInfoCollection finalParameters, ProcessingMessageList warnings, UserProfileState usedUserProfileState, bool successfulCompletion)
		{
			this.m_parameters = finalParameters;
			this.m_warnings = warnings;
			this.m_usedUserProfileState = usedUserProfileState;
			this.m_successfulCompletion = successfulCompletion;
		}

		// Token: 0x17002038 RID: 8248
		// (get) Token: 0x06005A5E RID: 23134 RVA: 0x001727DD File Offset: 0x001709DD
		public ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x17002039 RID: 8249
		// (get) Token: 0x06005A5F RID: 23135 RVA: 0x001727E5 File Offset: 0x001709E5
		public ProcessingMessageList Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x1700203A RID: 8250
		// (get) Token: 0x06005A60 RID: 23136 RVA: 0x001727ED File Offset: 0x001709ED
		public UserProfileState UsedUserProfileState
		{
			get
			{
				return this.m_usedUserProfileState;
			}
		}

		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x001727F5 File Offset: 0x001709F5
		public bool SuccessfulCompletion
		{
			get
			{
				return this.m_successfulCompletion;
			}
		}

		// Token: 0x04002E8C RID: 11916
		private ParameterInfoCollection m_parameters;

		// Token: 0x04002E8D RID: 11917
		private ProcessingMessageList m_warnings;

		// Token: 0x04002E8E RID: 11918
		private UserProfileState m_usedUserProfileState;

		// Token: 0x04002E8F RID: 11919
		private bool m_successfulCompletion;
	}
}

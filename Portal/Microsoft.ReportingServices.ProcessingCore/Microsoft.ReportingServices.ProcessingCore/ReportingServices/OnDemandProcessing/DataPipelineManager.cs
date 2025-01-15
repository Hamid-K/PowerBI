using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007E8 RID: 2024
	internal abstract class DataPipelineManager
	{
		// Token: 0x06007165 RID: 29029 RVA: 0x001D7A52 File Offset: 0x001D5C52
		public DataPipelineManager(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			this.m_odpContext = odpContext;
			this.m_dataSet = dataSet;
		}

		// Token: 0x06007166 RID: 29030 RVA: 0x001D7A68 File Offset: 0x001D5C68
		public void StartProcessing()
		{
			if (!this.m_odpContext.InSubreport)
			{
				this.m_odpContext.ExecutionLogContext.StartTablixProcessingTimer();
			}
			bool isTablixProcessingMode = this.m_odpContext.IsTablixProcessingMode;
			UserProfileState? userProfileState = null;
			try
			{
				this.m_odpContext.IsTablixProcessingMode = true;
				userProfileState = new UserProfileState?(this.m_odpContext.ReportObjectModel.UserImpl.UpdateUserProfileLocationWithoutLocking(UserProfileState.InReport));
				this.InternalStartProcessing();
			}
			finally
			{
				this.m_odpContext.ReportRuntime.CurrentScope = null;
				this.m_odpContext.IsTablixProcessingMode = isTablixProcessingMode;
				if (userProfileState != null)
				{
					this.m_odpContext.ReportObjectModel.UserImpl.UpdateUserProfileLocationWithoutLocking(userProfileState.Value);
				}
				if (!this.m_odpContext.InSubreport)
				{
					this.m_odpContext.ExecutionLogContext.StopTablixProcessingTimer();
				}
			}
		}

		// Token: 0x06007167 RID: 29031
		protected abstract void InternalStartProcessing();

		// Token: 0x06007168 RID: 29032 RVA: 0x001D7B48 File Offset: 0x001D5D48
		public void StopProcessing()
		{
			this.m_dataSet.ClearDataRegionStreamingScopeInstances();
			if (this.RuntimeDataSource != null)
			{
				this.RuntimeDataSource.RecordTimeDataRetrieval();
			}
			this.InternalStopProcessing();
		}

		// Token: 0x06007169 RID: 29033
		protected abstract void InternalStopProcessing();

		// Token: 0x0600716A RID: 29034
		public abstract void Advance();

		// Token: 0x0600716B RID: 29035 RVA: 0x001D7B6E File Offset: 0x001D5D6E
		public virtual void Abort()
		{
		}

		// Token: 0x1700268F RID: 9871
		// (get) Token: 0x0600716C RID: 29036
		public abstract IOnDemandScopeInstance GroupTreeRoot { get; }

		// Token: 0x17002690 RID: 9872
		// (get) Token: 0x0600716D RID: 29037
		protected abstract RuntimeDataSource RuntimeDataSource { get; }

		// Token: 0x17002691 RID: 9873
		// (get) Token: 0x0600716E RID: 29038 RVA: 0x001D7B70 File Offset: 0x001D5D70
		public int DataSetIndex
		{
			get
			{
				return this.m_dataSet.IndexInCollection;
			}
		}

		// Token: 0x04003A6C RID: 14956
		protected readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04003A6D RID: 14957
		protected readonly Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_dataSet;
	}
}

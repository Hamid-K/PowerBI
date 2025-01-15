using System;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000811 RID: 2065
	internal class RuntimeParameterDataSet : RuntimeAtomicDataSet, ReportProcessing.IFilterOwner
	{
		// Token: 0x060072BF RID: 29375 RVA: 0x001DD655 File Offset: 0x001DB855
		public RuntimeParameterDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext processingContext, bool mustEvaluateThroughReportObjectModel, ReportParameterDataSetCache aCache)
			: base(dataSource, dataSet, dataSetInstance, processingContext, true)
		{
			this.m_parameterDataSetObj = aCache;
			this.m_mustEvaluateThroughReportObjectModel = mustEvaluateThroughReportObjectModel;
		}

		// Token: 0x060072C0 RID: 29376 RVA: 0x001DD674 File Offset: 0x001DB874
		protected override void ProcessRow(Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow row, int rowNumber)
		{
			this.m_odpContext.ReportObjectModel.FieldsImpl.NewRow();
			this.m_odpContext.ReportObjectModel.UpdateFieldValues(false, row, this.m_dataSetInstance, base.HasServerAggregateMetadata);
			bool flag = true;
			if (this.m_filters != null)
			{
				flag = this.m_filters.PassFilters(new DataFieldRow(this.m_odpContext.ReportObjectModel.FieldsImpl, false));
			}
			if (flag)
			{
				this.PostFilterNextRow();
			}
		}

		// Token: 0x060072C1 RID: 29377 RVA: 0x001DD6E9 File Offset: 0x001DB8E9
		protected override void ProcessExtendedPropertyMappings()
		{
		}

		// Token: 0x060072C2 RID: 29378 RVA: 0x001DD6EC File Offset: 0x001DB8EC
		protected override void InitializeBeforeProcessingRows(bool aReaderExtensionsSupported)
		{
			Global.Tracer.Assert(this.m_odpContext.ReportObjectModel != null && this.m_odpContext.ReportRuntime != null);
			this.m_odpContext.SetupFieldsForNewDataSet(this.m_dataSet, this.m_dataSetInstance, false, true);
			this.m_dataSet.SetFilterExprHost(this.m_odpContext.ReportObjectModel);
			this.m_dataSet.SetupRuntimeEnvironment(this.m_odpContext);
			if (this.m_dataSet.Filters != null)
			{
				this.m_filters = new Filters(Filters.FilterTypes.DataSetFilter, this, this.m_dataSet.Filters, this.m_dataSet.ObjectType, this.m_dataSet.Name, this.m_odpContext, 0);
			}
		}

		// Token: 0x060072C3 RID: 29379 RVA: 0x001DD7A3 File Offset: 0x001DB9A3
		protected override void AllRowsRead()
		{
			if (this.m_filters != null)
			{
				this.m_filters.FinishReadingRows();
			}
		}

		// Token: 0x060072C4 RID: 29380 RVA: 0x001DD7B8 File Offset: 0x001DB9B8
		protected override void FinalCleanup()
		{
			base.FinalCleanup();
			this.m_odpContext.EnsureScalabilityCleanup();
		}

		// Token: 0x060072C5 RID: 29381 RVA: 0x001DD7CB File Offset: 0x001DB9CB
		public virtual void PostFilterNextRow()
		{
			if (this.m_parameterDataSetObj != null)
			{
				this.m_parameterDataSetObj.NextRow(this.m_odpContext.ReportObjectModel.FieldsImpl.GetAndSaveFields());
			}
		}

		// Token: 0x04003ACD RID: 15053
		private ReportParameterDataSetCache m_parameterDataSetObj;

		// Token: 0x04003ACE RID: 15054
		protected bool m_mustEvaluateThroughReportObjectModel;

		// Token: 0x04003ACF RID: 15055
		private Filters m_filters;
	}
}

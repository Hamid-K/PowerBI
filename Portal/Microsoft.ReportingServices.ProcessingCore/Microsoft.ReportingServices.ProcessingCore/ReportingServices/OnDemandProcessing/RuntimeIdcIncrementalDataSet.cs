using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000839 RID: 2105
	internal sealed class RuntimeIdcIncrementalDataSet : RuntimeIncrementalDataSet
	{
		// Token: 0x060075D2 RID: 30162 RVA: 0x001E8C4C File Offset: 0x001E6E4C
		internal RuntimeIdcIncrementalDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext)
			: base(dataSource, dataSet, dataSetInstance, odpContext)
		{
		}

		// Token: 0x060075D3 RID: 30163 RVA: 0x001E8C59 File Offset: 0x001E6E59
		public Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow GetNextRow()
		{
			this.SetupNextRow();
			return this.m_currentRow;
		}

		// Token: 0x060075D4 RID: 30164 RVA: 0x001E8C68 File Offset: 0x001E6E68
		private bool SetupNextRow()
		{
			bool flag;
			try
			{
				int num;
				this.m_currentRow = base.ReadOneRow(out num);
				if (this.m_currentRow == null)
				{
					flag = false;
				}
				else
				{
					FieldsImpl fieldsImpl = this.m_odpContext.ReportObjectModel.FieldsImpl;
					fieldsImpl.NewRow();
					if (fieldsImpl.AddRowIndex)
					{
						fieldsImpl.SetRowIndex(num);
					}
					this.m_odpContext.ReportObjectModel.UpdateFieldValues(false, this.m_currentRow, this.m_dataSetInstance, base.HasServerAggregateMetadata);
					flag = true;
				}
			}
			catch (Exception)
			{
				this.CleanupForException();
				this.FinalCleanup();
				throw;
			}
			return flag;
		}

		// Token: 0x060075D5 RID: 30165 RVA: 0x001E8CFC File Offset: 0x001E6EFC
		protected override void InitializeBeforeProcessingRows(bool aReaderExtensionsSupported)
		{
			Global.Tracer.Assert(this.m_odpContext.ReportObjectModel != null && this.m_odpContext.ReportRuntime != null);
			this.m_odpContext.SetupFieldsForNewDataSet(this.m_dataSet, this.m_dataSetInstance, true, true);
			base.PopulateFieldsWithReaderFlags();
			this.m_dataSet.SetFilterExprHost(this.m_odpContext.ReportObjectModel);
		}

		// Token: 0x060075D6 RID: 30166 RVA: 0x001E8D66 File Offset: 0x001E6F66
		protected override void ProcessExtendedPropertyMappings()
		{
		}

		// Token: 0x04003BA6 RID: 15270
		private Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow m_currentRow;
	}
}

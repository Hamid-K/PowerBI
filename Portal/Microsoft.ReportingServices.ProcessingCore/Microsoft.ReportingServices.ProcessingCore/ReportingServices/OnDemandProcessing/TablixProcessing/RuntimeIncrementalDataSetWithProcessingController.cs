using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008F3 RID: 2291
	internal abstract class RuntimeIncrementalDataSetWithProcessingController : RuntimeIncrementalDataSet
	{
		// Token: 0x06007E34 RID: 32308 RVA: 0x00208BA5 File Offset: 0x00206DA5
		public RuntimeIncrementalDataSetWithProcessingController(DataSource dataSource, DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext)
			: base(dataSource, dataSet, dataSetInstance, odpContext)
		{
		}

		// Token: 0x17002912 RID: 10514
		// (get) Token: 0x06007E35 RID: 32309 RVA: 0x00208BB2 File Offset: 0x00206DB2
		internal IOnDemandScopeInstance GroupTreeRoot
		{
			get
			{
				return this.m_dataProcessingController.GroupTreeRoot;
			}
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x00208BBF File Offset: 0x00206DBF
		protected override void InitializeDataSet()
		{
			base.InitializeDataSet();
			this.m_odpContext.SetComparisonInformation(this.m_dataSet.DataSetCore);
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x00208BDD File Offset: 0x00206DDD
		protected override void TeardownDataSet()
		{
			base.TeardownDataSet();
			this.CleanupController();
		}

		// Token: 0x06007E38 RID: 32312 RVA: 0x00208BEB File Offset: 0x00206DEB
		protected override void FinalCleanup()
		{
			base.FinalCleanup();
			if (this.m_dataSetInstance != null)
			{
				this.m_odpContext.SetTablixProcessingComplete(this.m_dataSet.IndexInCollection);
			}
		}

		// Token: 0x06007E39 RID: 32313 RVA: 0x00208C11 File Offset: 0x00206E11
		private void CleanupController()
		{
			if (this.m_dataProcessingController != null)
			{
				this.m_dataProcessingController.TeardownDataProcessing();
			}
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x00208C28 File Offset: 0x00206E28
		protected override void InitializeBeforeProcessingRows(bool aReaderExtensionsSupported)
		{
			this.m_dataProcessingController = new DataProcessingController(this.m_odpContext, this.m_dataSet, this.m_dataSetInstance);
			base.PopulateFieldsWithReaderFlags();
			this.m_odpContext.ClrCompareOptions = this.m_dataSet.GetCLRCompareOptions();
			this.m_dataProcessingController.InitializeDataProcessing();
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x00208C79 File Offset: 0x00206E79
		protected override void ProcessExtendedPropertyMappings()
		{
		}

		// Token: 0x04003E21 RID: 15905
		protected DataProcessingController m_dataProcessingController;
	}
}

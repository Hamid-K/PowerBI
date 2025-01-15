using System;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008B5 RID: 2229
	internal sealed class NonStructuralIdcDataManager : LinearIdcDataManager
	{
		// Token: 0x0600798B RID: 31115 RVA: 0x001F495B File Offset: 0x001F2B5B
		public NonStructuralIdcDataManager(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet targetDataSet, IRIFReportDataScope sourceDataScope)
			: base(odpContext, targetDataSet, NonStructuralIdcDataManager.GetActiveRelationship(targetDataSet, sourceDataScope))
		{
			this.m_sourceDataScope = sourceDataScope;
		}

		// Token: 0x0600798C RID: 31116 RVA: 0x001F4974 File Offset: 0x001F2B74
		private static Relationship GetActiveRelationship(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet targetDataSet, IRIFReportDataScope sourceDataScope)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = sourceDataScope.DataScopeInfo.DataSet;
			Relationship defaultRelationship = targetDataSet.GetDefaultRelationship(dataSet);
			Global.Tracer.Assert(defaultRelationship != null, "Could not find active relationship");
			return defaultRelationship;
		}

		// Token: 0x1700282D RID: 10285
		// (get) Token: 0x0600798D RID: 31117 RVA: 0x001F49A9 File Offset: 0x001F2BA9
		internal IRIFReportDataScope SourceDataScope
		{
			get
			{
				return this.m_sourceDataScope;
			}
		}

		// Token: 0x1700282E RID: 10286
		// (get) Token: 0x0600798E RID: 31118 RVA: 0x001F49B1 File Offset: 0x001F2BB1
		internal IReference<IOnDemandScopeInstance> LastParentScopeInstance
		{
			get
			{
				return this.m_lastParentScopeInstance;
			}
		}

		// Token: 0x0600798F RID: 31119 RVA: 0x001F49B9 File Offset: 0x001F2BB9
		protected override void UpdateActiveParent(IReference<IOnDemandScopeInstance> parentScopeInstanceRef)
		{
			this.m_lastParentScopeInstance = parentScopeInstanceRef;
		}

		// Token: 0x06007990 RID: 31120 RVA: 0x001F49C4 File Offset: 0x001F2BC4
		public override void Advance()
		{
			OnDemandStateManager stateManager = this.m_odpContext.StateManager;
			ObjectModelImpl reportObjectModel = this.m_odpContext.ReportObjectModel;
			if (this.m_idcDataSet.DataSetCore.FieldsContext != null)
			{
				reportObjectModel.RestoreFields(this.m_idcDataSet.DataSetCore.FieldsContext);
			}
			if (base.SetupCorrelatedRow(true))
			{
				this.m_lastCorrelationHadMatchingRow = true;
				return;
			}
			this.m_lastCorrelationHadMatchingRow = false;
			reportObjectModel.ResetFieldValues();
		}

		// Token: 0x06007991 RID: 31121 RVA: 0x001F4A2F File Offset: 0x001F2C2F
		public override void Close()
		{
			base.Close();
			this.m_lastParentScopeInstance = null;
		}

		// Token: 0x06007992 RID: 31122 RVA: 0x001F4A3E File Offset: 0x001F2C3E
		public void SetupEnvironment()
		{
			this.m_odpContext.ReportObjectModel.RestoreFields(this.m_idcDataSet.DataSetCore.FieldsContext);
			if (!this.m_lastCorrelationHadMatchingRow)
			{
				this.m_odpContext.ReportObjectModel.ResetFieldValues();
			}
		}

		// Token: 0x04003D03 RID: 15619
		private readonly IRIFReportDataScope m_sourceDataScope;

		// Token: 0x04003D04 RID: 15620
		private bool m_lastCorrelationHadMatchingRow;

		// Token: 0x04003D05 RID: 15621
		private IReference<IOnDemandScopeInstance> m_lastParentScopeInstance;
	}
}

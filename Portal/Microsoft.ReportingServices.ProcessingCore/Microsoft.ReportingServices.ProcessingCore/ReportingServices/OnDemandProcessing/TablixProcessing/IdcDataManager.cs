using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008B3 RID: 2227
	internal sealed class IdcDataManager : LinearIdcDataManager
	{
		// Token: 0x06007981 RID: 31105 RVA: 0x001F468A File Offset: 0x001F288A
		public IdcDataManager(OnDemandProcessingContext odpContext, IRIFReportDataScope idcReportDataScope)
			: base(odpContext, idcReportDataScope.DataScopeInfo.DataSet, IdcDataManager.GetActiveRelationship(idcReportDataScope))
		{
			this.m_idcReportDataScope = idcReportDataScope;
		}

		// Token: 0x06007982 RID: 31106 RVA: 0x001F46AC File Offset: 0x001F28AC
		private static Relationship GetActiveRelationship(IRIFReportDataScope idcReportDataScope)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = idcReportDataScope.DataScopeInfo.DataSet;
			LinearJoinInfo linearJoinInfo = idcReportDataScope.DataScopeInfo.JoinInfo as LinearJoinInfo;
			Global.Tracer.Assert(linearJoinInfo != null, "Did not find expected LinearJoinInfo");
			Relationship activeRelationship = linearJoinInfo.GetActiveRelationship(dataSet);
			Global.Tracer.Assert(activeRelationship != null, "Could not find active relationship");
			return activeRelationship;
		}

		// Token: 0x06007983 RID: 31107 RVA: 0x001F4705 File Offset: 0x001F2905
		internal void SetSkippingFilter(List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> expressions, List<object> values)
		{
			this.m_skippingFilter = new RowSkippingFilter(this.m_odpContext, this.m_idcReportDataScope, expressions, values);
		}

		// Token: 0x06007984 RID: 31108 RVA: 0x001F4720 File Offset: 0x001F2920
		internal void ClearSkippingFilter()
		{
			this.m_skippingFilter = null;
		}

		// Token: 0x06007985 RID: 31109 RVA: 0x001F4729 File Offset: 0x001F2929
		protected override void UpdateActiveParent(IReference<IOnDemandScopeInstance> parentScopeInstanceRef)
		{
			this.m_lastRuntimeReceiver = parentScopeInstanceRef.Value().GetIdcReceiver(this.m_idcReportDataScope);
		}

		// Token: 0x06007986 RID: 31110 RVA: 0x001F4744 File Offset: 0x001F2944
		public override void Advance()
		{
			OnDemandStateManager stateManager = this.m_odpContext.StateManager;
			ObjectModelImpl reportObjectModel = this.m_odpContext.ReportObjectModel;
			if (this.m_idcDataSet.DataSetCore.FieldsContext != null)
			{
				reportObjectModel.RestoreFields(this.m_idcDataSet.DataSetCore.FieldsContext);
			}
			using (this.m_lastRuntimeReceiver.PinValue())
			{
				IDataCorrelation dataCorrelation = this.m_lastRuntimeReceiver.Value();
				while (base.SetupCorrelatedRow(false))
				{
					base.ApplyGroupingFieldsForServerAggregates(this.m_idcReportDataScope);
					bool flag = dataCorrelation.NextCorrelatedRow();
					if (stateManager.ShouldStopPipelineAdvance(flag))
					{
						break;
					}
				}
			}
		}

		// Token: 0x06007987 RID: 31111 RVA: 0x001F47F0 File Offset: 0x001F29F0
		public override void Close()
		{
			base.Close();
			this.m_lastRuntimeReceiver = null;
		}

		// Token: 0x04003CFD RID: 15613
		private readonly IRIFReportDataScope m_idcReportDataScope;

		// Token: 0x04003CFE RID: 15614
		private IReference<IDataCorrelation> m_lastRuntimeReceiver;
	}
}

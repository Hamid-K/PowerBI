using System;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008B2 RID: 2226
	internal abstract class LinearIdcDataManager : BaseIdcDataManager
	{
		// Token: 0x0600797C RID: 31100 RVA: 0x001F45C3 File Offset: 0x001F27C3
		public LinearIdcDataManager(OnDemandProcessingContext odpContext, DataSet idcDataSet, Relationship activeRelationship)
			: base(odpContext, idcDataSet)
		{
			this.m_activeRelationship = activeRelationship;
		}

		// Token: 0x0600797D RID: 31101 RVA: 0x001F45D4 File Offset: 0x001F27D4
		public void RegisterActiveParent(IReference<IOnDemandScopeInstance> parentScopeInstanceRef)
		{
			using (parentScopeInstanceRef.PinValue())
			{
				parentScopeInstanceRef.Value().SetupEnvironment();
				this.m_lastPrimaryKeyValues = this.m_activeRelationship.EvaluateJoinConditionKeys(true, this.m_odpContext.ReportRuntime);
				this.UpdateActiveParent(parentScopeInstanceRef);
			}
		}

		// Token: 0x0600797E RID: 31102
		protected abstract void UpdateActiveParent(IReference<IOnDemandScopeInstance> parentScopeInstanceRef);

		// Token: 0x0600797F RID: 31103 RVA: 0x001F4634 File Offset: 0x001F2834
		protected override void SetupRelationshipQueryRestart()
		{
			base.AddRelationshipRestartPosition(this.m_activeRelationship, this.m_lastPrimaryKeyValues);
		}

		// Token: 0x06007980 RID: 31104 RVA: 0x001F4648 File Offset: 0x001F2848
		protected bool SetupCorrelatedRow(bool startWithCurrentRow)
		{
			bool flag = false;
			if (!startWithCurrentRow || !base.IsDataPipelineSetup)
			{
				if (!this.ReadRowFromDataSet())
				{
					return false;
				}
				flag = true;
			}
			bool flag2 = base.Correlate(this.m_activeRelationship, this.m_lastPrimaryKeyValues, flag);
			if (!flag2)
			{
				this.PushBackLastRow();
			}
			return flag2;
		}

		// Token: 0x04003CFB RID: 15611
		private readonly Relationship m_activeRelationship;

		// Token: 0x04003CFC RID: 15612
		private VariantResult[] m_lastPrimaryKeyValues;
	}
}

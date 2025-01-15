using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000823 RID: 2083
	internal sealed class OnDemandStateManagerDefinitionOnly : OnDemandStateManager
	{
		// Token: 0x06007495 RID: 29845 RVA: 0x001E2E70 File Offset: 0x001E1070
		public OnDemandStateManagerDefinitionOnly(OnDemandProcessingContext odpContext)
			: base(odpContext)
		{
		}

		// Token: 0x17002772 RID: 10098
		// (get) Token: 0x06007496 RID: 29846 RVA: 0x001E2E79 File Offset: 0x001E1079
		internal override IReportScopeInstance LastROMInstance
		{
			get
			{
				this.FireAssert("LastROMInstance");
				return null;
			}
		}

		// Token: 0x17002773 RID: 10099
		// (get) Token: 0x06007497 RID: 29847 RVA: 0x001E2E87 File Offset: 0x001E1087
		// (set) Token: 0x06007498 RID: 29848 RVA: 0x001E2E95 File Offset: 0x001E1095
		internal override IRIFReportScope LastTablixProcessingReportScope
		{
			get
			{
				this.FireAssert("LastTablixProcessingReportScope");
				return null;
			}
			set
			{
				this.FireAssert("LastTablixProcessingReportScope");
			}
		}

		// Token: 0x17002774 RID: 10100
		// (get) Token: 0x06007499 RID: 29849 RVA: 0x001E2EA2 File Offset: 0x001E10A2
		// (set) Token: 0x0600749A RID: 29850 RVA: 0x001E2EB0 File Offset: 0x001E10B0
		internal override IInstancePath LastRIFObject
		{
			get
			{
				this.FireAssert("LastRIFObject");
				return null;
			}
			set
			{
				this.FireAssert("LastRIFObject");
			}
		}

		// Token: 0x17002775 RID: 10101
		// (get) Token: 0x0600749B RID: 29851 RVA: 0x001E2EBD File Offset: 0x001E10BD
		internal override QueryRestartInfo QueryRestartInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600749C RID: 29852 RVA: 0x001E2EC0 File Offset: 0x001E10C0
		internal override ExecutedQueryCache SetupExecutedQueryCache()
		{
			return this.ExecutedQueryCache;
		}

		// Token: 0x17002776 RID: 10102
		// (get) Token: 0x0600749D RID: 29853 RVA: 0x001E2EC8 File Offset: 0x001E10C8
		internal override ExecutedQueryCache ExecutedQueryCache
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600749E RID: 29854 RVA: 0x001E2ECB File Offset: 0x001E10CB
		internal override void ResetOnDemandState()
		{
		}

		// Token: 0x0600749F RID: 29855 RVA: 0x001E2ECD File Offset: 0x001E10CD
		internal override int RecursiveLevel(string scopeName)
		{
			this.FireAssert("RecursiveLevel");
			return -1;
		}

		// Token: 0x060074A0 RID: 29856 RVA: 0x001E2EDB File Offset: 0x001E10DB
		internal override bool InScope(string scopeName)
		{
			this.FireAssert("InScope");
			return false;
		}

		// Token: 0x060074A1 RID: 29857 RVA: 0x001E2EE9 File Offset: 0x001E10E9
		internal override Dictionary<string, object> GetCurrentSpecialGroupingValues()
		{
			this.FireAssert("GetCurrentSpecialGroupingValues");
			return null;
		}

		// Token: 0x060074A2 RID: 29858 RVA: 0x001E2EF7 File Offset: 0x001E10F7
		internal override void RestoreContext(IInstancePath originalObject)
		{
			this.FireAssert("RestoreContext");
		}

		// Token: 0x060074A3 RID: 29859 RVA: 0x001E2F04 File Offset: 0x001E1104
		internal override void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance)
		{
			this.FireAssert("SetupContext");
		}

		// Token: 0x060074A4 RID: 29860 RVA: 0x001E2F11 File Offset: 0x001E1111
		internal override void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex)
		{
			this.FireAssert("SetupContext");
		}

		// Token: 0x060074A5 RID: 29861 RVA: 0x001E2F1E File Offset: 0x001E111E
		internal override bool CalculateAggregate(string aggregateName)
		{
			this.FireAssert("CalculateAggregate");
			return false;
		}

		// Token: 0x060074A6 RID: 29862 RVA: 0x001E2F2C File Offset: 0x001E112C
		internal override bool CalculateLookup(LookupInfo lookup)
		{
			this.FireAssert("CalculateLookup");
			return false;
		}

		// Token: 0x060074A7 RID: 29863 RVA: 0x001E2F3A File Offset: 0x001E113A
		internal override bool PrepareFieldsCollectionForDirectFields()
		{
			this.FireAssert("PrepareFieldsCollectionForDirectFields");
			return false;
		}

		// Token: 0x060074A8 RID: 29864 RVA: 0x001E2F48 File Offset: 0x001E1148
		internal override void EvaluateScopedFieldReference(string scopeName, int fieldIndex, ref Microsoft.ReportingServices.RdlExpressions.VariantResult result)
		{
			this.FireAssert("EvaluateScopedFieldReference");
		}

		// Token: 0x060074A9 RID: 29865 RVA: 0x001E2F55 File Offset: 0x001E1155
		private void FireAssert(string methodOrPropertyName)
		{
			Global.Tracer.Assert(false, methodOrPropertyName + " should not be called in Definition-only mode.");
		}

		// Token: 0x060074AA RID: 29866 RVA: 0x001E2F6D File Offset: 0x001E116D
		internal override IRecordRowReader CreateSequentialDataReader(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, out Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance)
		{
			this.FireAssert("CreateSequentialDataReader");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		// Token: 0x060074AB RID: 29867 RVA: 0x001E2F84 File Offset: 0x001E1184
		internal override void BindNextMemberInstance(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex)
		{
			this.FireAssert("BindNextMemberInstance");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		// Token: 0x060074AC RID: 29868 RVA: 0x001E2F9B File Offset: 0x001E119B
		internal override bool ShouldStopPipelineAdvance(bool rowAccepted)
		{
			this.FireAssert("ShouldStopPipelineAdvance");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		// Token: 0x060074AD RID: 29869 RVA: 0x001E2FB2 File Offset: 0x001E11B2
		internal override void CreatedScopeInstance(IRIFReportDataScope scope)
		{
			this.FireAssert("CreateScopeInstance");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		// Token: 0x060074AE RID: 29870 RVA: 0x001E2FC9 File Offset: 0x001E11C9
		internal override bool ProcessOneRow(IRIFReportDataScope scope)
		{
			this.FireAssert("ProcessOneRow");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}

		// Token: 0x060074AF RID: 29871 RVA: 0x001E2FE0 File Offset: 0x001E11E0
		internal override bool CheckForPrematureServerAggregate(string aggregateName)
		{
			this.FireAssert("CheckForPrematureServerAggregate");
			throw new InvalidOperationException("This method is not valid for this StateManager type.");
		}
	}
}

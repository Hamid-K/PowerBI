using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200082F RID: 2095
	[PersistedWithinRequestOnly]
	[SkipStaticValidation]
	internal abstract class StreamingNoRowsScopeInstanceBase : IOnDemandScopeInstance, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007579 RID: 30073 RVA: 0x001E7421 File Offset: 0x001E5621
		public StreamingNoRowsScopeInstanceBase(OnDemandProcessingContext odpContext, IRIFReportDataScope dataScope)
		{
			this.m_odpContext = odpContext;
			this.m_dataScope = dataScope;
		}

		// Token: 0x0600757A RID: 30074 RVA: 0x001E7438 File Offset: 0x001E5638
		public void SetupEnvironment()
		{
			this.m_odpContext.EnsureRuntimeEnvironmentForDataSet(this.m_dataScope.DataScopeInfo.DataSet, true);
			this.m_odpContext.ReportObjectModel.ResetFieldValues();
			this.m_dataScope.ResetAggregates(this.m_odpContext.ReportObjectModel.AggregatesImpl);
		}

		// Token: 0x0600757B RID: 30075 RVA: 0x001E748C File Offset: 0x001E568C
		public IOnDemandMemberOwnerInstanceReference GetDataRegionInstance(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			return null;
		}

		// Token: 0x0600757C RID: 30076 RVA: 0x001E748F File Offset: 0x001E568F
		public IReference<IDataCorrelation> GetIdcReceiver(IRIFReportDataScope scope)
		{
			return null;
		}

		// Token: 0x17002799 RID: 10137
		// (get) Token: 0x0600757D RID: 30077 RVA: 0x001E7492 File Offset: 0x001E5692
		public bool IsNoRows
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700279A RID: 10138
		// (get) Token: 0x0600757E RID: 30078 RVA: 0x001E7495 File Offset: 0x001E5695
		public bool IsMostRecentlyCreatedScopeInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700279B RID: 10139
		// (get) Token: 0x0600757F RID: 30079 RVA: 0x001E7498 File Offset: 0x001E5698
		public bool HasUnProcessedServerAggregate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700279C RID: 10140
		// (get) Token: 0x06007580 RID: 30080 RVA: 0x001E749B File Offset: 0x001E569B
		public int Size
		{
			get
			{
				Global.Tracer.Assert(false, "Size may not be used on a no rows scope instance.");
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06007581 RID: 30081 RVA: 0x001E74B2 File Offset: 0x001E56B2
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			Global.Tracer.Assert(false, "Serialize may not be used on a no rows scope instance.");
			throw new InvalidOperationException();
		}

		// Token: 0x06007582 RID: 30082 RVA: 0x001E74C9 File Offset: 0x001E56C9
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(false, "Deserialize may not be used on a no rows scope instance.");
			throw new InvalidOperationException();
		}

		// Token: 0x06007583 RID: 30083 RVA: 0x001E74E0 File Offset: 0x001E56E0
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "ResolveReferences may not be used on a no rows scope instance.");
			throw new InvalidOperationException();
		}

		// Token: 0x06007584 RID: 30084
		public abstract Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType();

		// Token: 0x04003B94 RID: 15252
		private readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04003B95 RID: 15253
		private readonly IRIFReportDataScope m_dataScope;
	}
}

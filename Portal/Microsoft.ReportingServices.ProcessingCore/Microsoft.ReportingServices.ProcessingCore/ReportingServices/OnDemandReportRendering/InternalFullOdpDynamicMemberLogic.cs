using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002A1 RID: 673
	internal sealed class InternalFullOdpDynamicMemberLogic : InternalDynamicMemberLogic
	{
		// Token: 0x060019F2 RID: 6642 RVA: 0x00069046 File Offset: 0x00067246
		public InternalFullOdpDynamicMemberLogic(DataRegionMember memberDef, OnDemandProcessingContext odpContext)
		{
			this.m_memberDef = memberDef;
			this.m_odpContext = odpContext;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0006905C File Offset: 0x0006725C
		public override void ResetContext()
		{
			this.m_isNewContext = true;
			this.m_currentContext = -1;
			this.m_memberDef.DataRegionMemberDefinition.InstanceCount = -1;
			this.m_memberDef.DataRegionMemberDefinition.InstancePathItem.ResetContext();
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00069094 File Offset: 0x00067294
		public override bool MoveNext()
		{
			if (!this.IsContextValid(this.m_currentContext + 1))
			{
				return false;
			}
			this.m_isNewContext = true;
			this.m_currentContext++;
			this.m_memberDef.DataRegionMemberDefinition.InstancePathItem.MoveNext();
			this.m_memberDef.SetNewContext(true);
			return true;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000690EC File Offset: 0x000672EC
		public override bool SetInstanceIndex(int index)
		{
			if (index < 0)
			{
				this.ResetContext();
				return true;
			}
			if (this.IsContextValid(index))
			{
				this.m_currentContext = index;
				this.m_memberDef.DataRegionMemberDefinition.InstancePathItem.SetContext(this.m_currentContext);
				this.m_memberDef.SetNewContext(true);
				this.m_isNewContext = true;
				return true;
			}
			return false;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00069148 File Offset: 0x00067348
		private bool IsContextValid(int context)
		{
			if (this.m_memberDef.DataRegionMemberDefinition.InstanceCount < 0)
			{
				this.m_odpContext.SetupContext(this.m_memberDef.DataRegionMemberDefinition, this.m_memberDef.ReportScopeInstance, context);
			}
			return context < this.m_memberDef.DataRegionMemberDefinition.InstanceCount;
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0006919D File Offset: 0x0006739D
		internal override ScopeID GetScopeID()
		{
			throw new RenderingObjectModelException(ProcessingErrorCode.rsNotSupportedInStreamingMode, new object[] { "GetScopeID" });
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x000691B7 File Offset: 0x000673B7
		internal override ScopeID GetLastScopeID()
		{
			throw new RenderingObjectModelException(ProcessingErrorCode.rsNotSupportedInStreamingMode, new object[] { "GetLastScopeID" });
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000691D1 File Offset: 0x000673D1
		internal override void SetScopeID(ScopeID scopeID)
		{
			throw new RenderingObjectModelException(ProcessingErrorCode.rsNotSupportedInStreamingMode, new object[] { "SetScopeID" });
		}

		// Token: 0x04000CF3 RID: 3315
		private readonly DataRegionMember m_memberDef;

		// Token: 0x04000CF4 RID: 3316
		private readonly OnDemandProcessingContext m_odpContext;
	}
}

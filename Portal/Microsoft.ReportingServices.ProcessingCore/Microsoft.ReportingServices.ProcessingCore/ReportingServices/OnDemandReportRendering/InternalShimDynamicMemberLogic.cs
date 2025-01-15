using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002A4 RID: 676
	internal sealed class InternalShimDynamicMemberLogic : InternalDynamicMemberLogic
	{
		// Token: 0x06001A0B RID: 6667 RVA: 0x0006989F File Offset: 0x00067A9F
		public InternalShimDynamicMemberLogic(IShimDataRegionMember shimMember)
		{
			this.m_shimMember = shimMember;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000698AE File Offset: 0x00067AAE
		public override void ResetContext()
		{
			this.m_isNewContext = true;
			this.m_currentContext = -1;
			this.m_shimMember.ResetContext();
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x000698C9 File Offset: 0x00067AC9
		public override bool MoveNext()
		{
			if (!this.m_shimMember.SetNewContext(this.m_currentContext + 1))
			{
				return false;
			}
			this.m_currentContext++;
			return true;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000698F1 File Offset: 0x00067AF1
		public override bool SetInstanceIndex(int index)
		{
			if (index < 0)
			{
				this.ResetContext();
				return true;
			}
			if (this.m_shimMember.SetNewContext(index))
			{
				this.m_currentContext = index;
				return true;
			}
			return false;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00069917 File Offset: 0x00067B17
		internal override ScopeID GetScopeID()
		{
			throw new RenderingObjectModelException(ProcessingErrorCode.rsNotSupportedInStreamingMode, new object[] { "GetScopeID" });
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00069931 File Offset: 0x00067B31
		internal override ScopeID GetLastScopeID()
		{
			throw new RenderingObjectModelException(ProcessingErrorCode.rsNotSupportedInStreamingMode, new object[] { "GetLastScopeID" });
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0006994B File Offset: 0x00067B4B
		internal override void SetScopeID(ScopeID scopeID)
		{
			throw new RenderingObjectModelException(ProcessingErrorCode.rsNotSupportedInStreamingMode, new object[] { "SetScopeID" });
		}

		// Token: 0x04000CFC RID: 3324
		private readonly IShimDataRegionMember m_shimMember;
	}
}

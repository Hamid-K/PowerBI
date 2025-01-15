using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002A3 RID: 675
	internal sealed class InternalStreamingOdpDynamicMemberLogic : InternalStreamingOdpDynamicMemberLogicBase
	{
		// Token: 0x06001A07 RID: 6663 RVA: 0x0006976E File Offset: 0x0006796E
		public InternalStreamingOdpDynamicMemberLogic(DataRegionMember memberDef, OnDemandProcessingContext odpContext)
			: base(memberDef, odpContext)
		{
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00069778 File Offset: 0x00067978
		public override bool MoveNext()
		{
			base.ResetScopeID();
			return base.MoveNextCore(null);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00069788 File Offset: 0x00067988
		internal bool RomBasedRestart(ScopeID targetScopeID)
		{
			if (targetScopeID == null)
			{
				return false;
			}
			bool flag2;
			try
			{
				IEqualityComparer<object> processingComparer = this.m_odpContext.ProcessingComparer;
				bool flag = true;
				while (!targetScopeID.Equals(this.GetScopeID(), processingComparer) && (flag = this.MoveNext()))
				{
				}
				flag2 = flag;
			}
			catch (Exception)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsRombasedRestartFailedTypeMismatch, new object[] { this.m_memberDef.Group.Name });
			}
			return flag2;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00069804 File Offset: 0x00067A04
		internal override void SetScopeID(ScopeID scopeID)
		{
			if (this.m_grouping.IsDetail)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsDetailGroupsNotSupportedInStreamingMode, new object[] { "SetScopeID" });
			}
			if (scopeID == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidScopeID, new object[] { "SetScopeID" });
			}
			if (!this.m_odpContext.QueryRestartInfo.TryAddScopeID(scopeID, this.m_memberDef.DataRegionMemberDefinition, this))
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidScopeIDOrder, new object[]
				{
					this.m_grouping.Name,
					"SetScopeID"
				});
			}
		}
	}
}

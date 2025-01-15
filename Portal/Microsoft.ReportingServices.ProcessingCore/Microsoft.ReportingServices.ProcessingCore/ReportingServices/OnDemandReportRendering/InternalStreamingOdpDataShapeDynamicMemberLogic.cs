using System;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000A1 RID: 161
	internal sealed class InternalStreamingOdpDataShapeDynamicMemberLogic : InternalStreamingOdpDynamicMemberLogicBase
	{
		// Token: 0x06000986 RID: 2438 RVA: 0x00027611 File Offset: 0x00025811
		public InternalStreamingOdpDataShapeDynamicMemberLogic(DataShapeMember memberDef, OnDemandProcessingContext odpContext)
			: base(memberDef, odpContext)
		{
			this.m_dataShapeMemberDef = memberDef;
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00027622 File Offset: 0x00025822
		private DataShapeSegmentationManager SegmentationHelper
		{
			get
			{
				return this.m_dataShapeMemberDef.OwnerDataShape.RenderingContext.SegmentationManager;
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00027639 File Offset: 0x00025839
		public override void ResetContext()
		{
			base.ResetContext();
			if (!this.m_dataShapeMemberDef.IsColumn)
			{
				this.SegmentationHelper.ResetScopeComplete(this.m_dataShapeMemberDef);
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00027660 File Offset: 0x00025860
		public override bool MoveNext()
		{
			base.ResetScopeID();
			bool isRowMember = !this.m_dataShapeMemberDef.IsColumn;
			if (isRowMember && this.m_dataShapeMemberDef.OwnerDataShape.HasExceededRequestedPrimaryLeafCount)
			{
				if (base.MoveNextCore(null))
				{
					this.m_dataShapeMemberDef.OwnerDataShape.SetHasMoreRows();
				}
				else
				{
					this.SegmentationHelper.SetScopeComplete(this.m_dataShapeMemberDef);
				}
				return false;
			}
			bool flag = base.MoveNextCore(delegate
			{
				if (isRowMember && this.m_dataShapeMemberDef.DataRegionMemberDefinition.IsInnermostDynamicMember)
				{
					this.m_dataShapeMemberDef.OwnerDataShape.IncrementRequestedPrimaryLeafCount();
				}
			});
			if (isRowMember && !flag)
			{
				this.SegmentationHelper.SetScopeComplete(this.m_dataShapeMemberDef);
			}
			return flag;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002770C File Offset: 0x0002590C
		internal override void SetScopeID(ScopeID scopeID)
		{
			throw new RenderingObjectModelException(ProcessingErrorCode.rsNotSupportedInStreamingMode, new object[] { "SetScopeID" });
		}

		// Token: 0x0400027E RID: 638
		private readonly DataShapeMember m_dataShapeMemberDef;
	}
}

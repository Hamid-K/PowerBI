using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000088 RID: 136
	internal sealed class DataShapeSegmentationManager
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x00025A24 File Offset: 0x00023C24
		internal DataShapeSegmentationManager()
		{
			this.m_parentsWithScopeComplete = new HashSet<int>();
			this.m_membersWithStartPositionApplied = new HashSet<int>();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00025A44 File Offset: 0x00023C44
		internal bool CanRender(DataShapeMember staticMember)
		{
			RSTrace.ProcessingTracer.Assert(staticMember.IsStatic, "CanRender should only be called for static members.");
			if (staticMember.IsColumn)
			{
				return true;
			}
			switch (staticMember.Type)
			{
			case DataShapeMember.MemberType.StaticPrecedesDynamicPeer:
			{
				DataShapeMember singleDynamicMemberOrNull = staticMember.RifDataShapeMemberDefinition.SameLevelMembers.GetSingleDynamicMemberOrNull();
				RSTrace.ProcessingTracer.Assert(singleDynamicMemberOrNull != null, "Peer dynamic cannot be found.");
				return !this.m_membersWithStartPositionApplied.Contains(singleDynamicMemberOrNull.ID);
			}
			case DataShapeMember.MemberType.StaticSucceedsDynamicPeer:
			{
				DataShapeMember singleDynamicMemberOrNull2 = staticMember.RifDataShapeMemberDefinition.SameLevelMembers.GetSingleDynamicMemberOrNull();
				RSTrace.ProcessingTracer.Assert(singleDynamicMemberOrNull2 != null, "Peer dynamic cannot be found.");
				if (singleDynamicMemberOrNull2.IsLeaf && staticMember.OwnerDataShape.HasExceededRequestedPrimaryLeafCount)
				{
					staticMember.OwnerDataShape.SetHasMoreRows();
					return false;
				}
				return !staticMember.OwnerDataShape.HasMoreRows() && this.m_parentsWithScopeComplete.Contains(DataShapeSegmentationManager.GetParentID(staticMember));
			}
			case DataShapeMember.MemberType.StaticWithNoPeerDynamic:
			{
				DataShapeMember parent = staticMember.Parent;
				return parent == null || !parent.IsStatic || this.CanRender(parent);
			}
			default:
				return true;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00025B4C File Offset: 0x00023D4C
		internal void SetScopeComplete(DataShapeMember dynamicMember)
		{
			RSTrace.ProcessingTracer.Assert(!dynamicMember.IsStatic, "SetScopeComplete should only be called for dynamic members.");
			RSTrace.ProcessingTracer.Assert(!dynamicMember.IsColumn, "SetScopeComplete should only be called for row members.");
			this.m_parentsWithScopeComplete.Add(DataShapeSegmentationManager.GetParentID(dynamicMember));
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00025B9C File Offset: 0x00023D9C
		internal void ResetScopeComplete(DataShapeMember dynamicMember)
		{
			RSTrace.ProcessingTracer.Assert(!dynamicMember.IsStatic, "ResetScopeComplete should only be called for dynamic members.");
			RSTrace.ProcessingTracer.Assert(!dynamicMember.IsColumn, "ResetScopeComplete should only be called for row members.");
			this.m_parentsWithScopeComplete.Remove(DataShapeSegmentationManager.GetParentID(dynamicMember));
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00025BEC File Offset: 0x00023DEC
		internal bool TrySetStartPositionApplied(DataShapeMember dynamicMember)
		{
			RSTrace.ProcessingTracer.Assert(!dynamicMember.IsStatic, "TrySetStartPositionApplied should only be called for dynamic members.");
			RSTrace.ProcessingTracer.Assert(!dynamicMember.IsColumn, "TrySetStartPositionApplied should only be called for row members.");
			return this.m_membersWithStartPositionApplied.Add(dynamicMember.ID);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00025C3C File Offset: 0x00023E3C
		internal void ResetStartPositionApplied(DataShapeMember dynamicMember)
		{
			RSTrace.ProcessingTracer.Assert(!dynamicMember.IsStatic, "ResetStartPositionApplied should only be called for dynamic members.");
			RSTrace.ProcessingTracer.Assert(!dynamicMember.IsColumn, "ResetStartPositionApplied should only be called for row members.");
			this.m_membersWithStartPositionApplied.Remove(dynamicMember.ID);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00025C8B File Offset: 0x00023E8B
		private static int GetParentID(DataShapeMember member)
		{
			if (member.Parent != null)
			{
				return member.Parent.RifDataShapeMemberDefinition.ID;
			}
			return member.OwnerDataShape.RifDataShapeDefinition.ID;
		}

		// Token: 0x04000229 RID: 553
		private readonly HashSet<int> m_parentsWithScopeComplete;

		// Token: 0x0400022A RID: 554
		private readonly HashSet<int> m_membersWithStartPositionApplied;
	}
}

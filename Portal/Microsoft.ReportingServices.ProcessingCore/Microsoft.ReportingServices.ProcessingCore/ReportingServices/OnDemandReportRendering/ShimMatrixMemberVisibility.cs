using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200037E RID: 894
	internal sealed class ShimMatrixMemberVisibility : ShimMemberVisibility
	{
		// Token: 0x06002248 RID: 8776 RVA: 0x00083CF9 File Offset: 0x00081EF9
		public ShimMatrixMemberVisibility(ShimMatrixMember owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x00083D08 File Offset: 0x00081F08
		public override ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_startHidden == null)
				{
					this.m_startHidden = Visibility.GetStartHidden(this.m_owner.Group.CurrentShimRenderGroup.m_visibilityDef);
				}
				return this.m_startHidden;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x00083D38 File Offset: 0x00081F38
		public override string ToggleItem
		{
			get
			{
				if (this.m_owner.Group.CurrentShimRenderGroup.m_visibilityDef != null)
				{
					return this.m_owner.Group.CurrentShimRenderGroup.m_visibilityDef.Toggle;
				}
				return null;
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x00083D6D File Offset: 0x00081F6D
		public override bool RecursiveToggleReceiver
		{
			get
			{
				return this.m_owner.Group.CurrentShimRenderGroup.m_visibilityDef != null && this.m_owner.Group.CurrentShimRenderGroup.m_visibilityDef.RecursiveReceiver;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x00083DA2 File Offset: 0x00081FA2
		public override SharedHiddenState HiddenState
		{
			get
			{
				return Visibility.GetHiddenState(this.m_owner.Group.CurrentShimRenderGroup.m_visibilityDef);
			}
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x00083DBE File Offset: 0x00081FBE
		internal override bool GetInstanceHidden()
		{
			return this.m_owner.Group.CurrentShimRenderGroup.Hidden;
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x00083DD8 File Offset: 0x00081FD8
		internal override bool GetInstanceStartHidden()
		{
			if (this.m_owner.Group == null)
			{
				return false;
			}
			if (((MatrixMember)this.m_owner.Group.CurrentShimRenderGroup).InstanceInfo != null)
			{
				return ((MatrixMember)this.m_owner.Group.CurrentShimRenderGroup).InstanceInfo.StartHidden;
			}
			return this.GetInstanceHidden();
		}

		// Token: 0x040010F1 RID: 4337
		private ShimMatrixMember m_owner;
	}
}

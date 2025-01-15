using System;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200037C RID: 892
	internal sealed class ShimListMemberVisibility : ShimMemberVisibility
	{
		// Token: 0x06002239 RID: 8761 RVA: 0x000838AC File Offset: 0x00081AAC
		public ShimListMemberVisibility(ShimListMember owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x000838BC File Offset: 0x00081ABC
		public override ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_startHidden == null && this.m_owner.Group != null)
				{
					this.m_startHidden = Visibility.GetStartHidden(this.m_owner.OwnerTablix.RenderList.ReportItemDef.Visibility);
				}
				return this.m_startHidden;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x0008390C File Offset: 0x00081B0C
		public override string ToggleItem
		{
			get
			{
				if (this.m_owner.Group != null && this.m_owner.OwnerTablix.RenderList.ReportItemDef.Visibility != null)
				{
					return this.m_owner.OwnerTablix.RenderList.ReportItemDef.Visibility.Toggle;
				}
				return null;
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x00083964 File Offset: 0x00081B64
		public override bool RecursiveToggleReceiver
		{
			get
			{
				return this.m_owner.Group != null && this.m_owner.OwnerTablix.RenderList.ReportItemDef.Visibility != null && this.m_owner.OwnerTablix.RenderList.ReportItemDef.Visibility.RecursiveReceiver;
			}
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x000839BB File Offset: 0x00081BBB
		public override SharedHiddenState HiddenState
		{
			get
			{
				if (this.m_owner.Group != null)
				{
					return Visibility.GetHiddenState(this.m_owner.OwnerTablix.RenderList.ReportItemDef.Visibility);
				}
				return SharedHiddenState.Never;
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000839EB File Offset: 0x00081BEB
		internal override bool GetInstanceHidden()
		{
			return this.m_owner.Group != null && this.m_owner.Group.CurrentShimRenderGroup.Hidden;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00083A14 File Offset: 0x00081C14
		internal override bool GetInstanceStartHidden()
		{
			if (this.m_owner.Group == null)
			{
				return false;
			}
			if (((ListContent)this.m_owner.Group.CurrentShimRenderGroup).InstanceInfo != null)
			{
				return ((ListContent)this.m_owner.Group.CurrentShimRenderGroup).InstanceInfo.StartHidden;
			}
			return this.GetInstanceHidden();
		}

		// Token: 0x040010EE RID: 4334
		private ShimListMember m_owner;
	}
}

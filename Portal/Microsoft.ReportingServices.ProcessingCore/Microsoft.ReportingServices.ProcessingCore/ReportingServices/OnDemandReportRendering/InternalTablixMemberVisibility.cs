using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200037A RID: 890
	internal sealed class InternalTablixMemberVisibility : Visibility
	{
		// Token: 0x06002231 RID: 8753 RVA: 0x000837FD File Offset: 0x000819FD
		public InternalTablixMemberVisibility(InternalTablixMember owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x0008380C File Offset: 0x00081A0C
		public override ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_startHidden == null)
				{
					this.m_startHidden = Visibility.GetStartHidden(this.m_owner.MemberDefinition.Visibility);
				}
				return this.m_startHidden;
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x00083837 File Offset: 0x00081A37
		public override string ToggleItem
		{
			get
			{
				if (this.m_owner.MemberDefinition.Visibility != null)
				{
					return this.m_owner.MemberDefinition.Visibility.Toggle;
				}
				return null;
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x00083862 File Offset: 0x00081A62
		public override SharedHiddenState HiddenState
		{
			get
			{
				return Visibility.GetHiddenState(this.m_owner.MemberDefinition.Visibility);
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x00083879 File Offset: 0x00081A79
		public override bool RecursiveToggleReceiver
		{
			get
			{
				return this.m_owner.MemberDefinition.Visibility != null && this.m_owner.MemberDefinition.Visibility.RecursiveReceiver;
			}
		}

		// Token: 0x040010ED RID: 4333
		private InternalTablixMember m_owner;
	}
}

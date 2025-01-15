using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000382 RID: 898
	internal sealed class ShimMemberVisibilityInstance : VisibilityInstance
	{
		// Token: 0x06002259 RID: 8793 RVA: 0x00084022 File Offset: 0x00082222
		internal ShimMemberVisibilityInstance(ShimMemberVisibility owner)
			: base(null)
		{
			this.m_owner = owner;
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x00084032 File Offset: 0x00082232
		public override bool CurrentlyHidden
		{
			get
			{
				if (!this.m_cachedCurrentlyHidden)
				{
					this.m_cachedCurrentlyHidden = true;
					this.m_currentlyHiddenValue = this.m_owner.GetInstanceHidden();
				}
				return this.m_currentlyHiddenValue;
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x0008405A File Offset: 0x0008225A
		public override bool StartHidden
		{
			get
			{
				if (!this.m_cachedStartHidden)
				{
					this.m_cachedStartHidden = true;
					this.m_startHiddenValue = this.m_owner.GetInstanceStartHidden();
				}
				return this.m_startHiddenValue;
			}
		}

		// Token: 0x040010F8 RID: 4344
		private ShimMemberVisibility m_owner;
	}
}

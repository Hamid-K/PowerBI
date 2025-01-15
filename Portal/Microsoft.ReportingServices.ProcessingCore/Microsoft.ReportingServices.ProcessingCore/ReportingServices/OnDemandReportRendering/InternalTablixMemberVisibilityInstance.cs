using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000381 RID: 897
	internal sealed class InternalTablixMemberVisibilityInstance : VisibilityInstance
	{
		// Token: 0x06002256 RID: 8790 RVA: 0x00083F38 File Offset: 0x00082138
		internal InternalTablixMemberVisibilityInstance(InternalTablixMember owner)
			: base(owner.ReportScope)
		{
			this.m_owner = owner;
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x00083F50 File Offset: 0x00082150
		public override bool CurrentlyHidden
		{
			get
			{
				if (!this.m_cachedCurrentlyHidden)
				{
					this.m_cachedCurrentlyHidden = true;
					TablixMember memberDefinition = this.m_owner.MemberDefinition;
					ToggleCascadeDirection toggleCascadeDirection = (memberDefinition.IsColumn ? ToggleCascadeDirection.Column : ToggleCascadeDirection.Row);
					this.m_currentlyHiddenValue = memberDefinition.ComputeHidden(this.m_owner.OwnerTablix.RenderingContext, toggleCascadeDirection);
				}
				return this.m_currentlyHiddenValue;
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x00083FA8 File Offset: 0x000821A8
		public override bool StartHidden
		{
			get
			{
				if (!this.m_cachedStartHidden)
				{
					this.m_cachedStartHidden = true;
					if (this.m_owner.MemberDefinition.Visibility == null || this.m_owner.MemberDefinition.Visibility.Hidden == null)
					{
						this.m_startHiddenValue = false;
					}
					else
					{
						this.m_startHiddenValue = this.m_owner.MemberDefinition.ComputeStartHidden(this.m_owner.OwnerTablix.RenderingContext);
					}
				}
				return this.m_startHiddenValue;
			}
		}

		// Token: 0x040010F7 RID: 4343
		private InternalTablixMember m_owner;
	}
}

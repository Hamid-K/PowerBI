using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200037D RID: 893
	internal sealed class ShimTableMemberVisibility : ShimMemberVisibility
	{
		// Token: 0x06002240 RID: 8768 RVA: 0x00083A72 File Offset: 0x00081C72
		public ShimTableMemberVisibility(ShimTableMember owner, ShimTableMemberVisibility.Mode mode)
		{
			this.m_owner = owner;
			this.m_mode = mode;
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x00083A88 File Offset: 0x00081C88
		public override ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_startHidden == null)
				{
					this.m_startHidden = Visibility.GetStartHidden(this.GetVisibilityDefinition());
				}
				return this.m_startHidden;
			}
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x00083AAC File Offset: 0x00081CAC
		public override string ToggleItem
		{
			get
			{
				Visibility visibilityDefinition = this.GetVisibilityDefinition();
				if (visibilityDefinition != null)
				{
					return visibilityDefinition.Toggle;
				}
				return null;
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x00083ACC File Offset: 0x00081CCC
		public override bool RecursiveToggleReceiver
		{
			get
			{
				Visibility visibilityDefinition = this.GetVisibilityDefinition();
				return visibilityDefinition != null && visibilityDefinition.RecursiveReceiver;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x00083AEB File Offset: 0x00081CEB
		public override SharedHiddenState HiddenState
		{
			get
			{
				return Visibility.GetHiddenState(this.GetVisibilityDefinition());
			}
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x00083AF8 File Offset: 0x00081CF8
		private Visibility GetVisibilityDefinition()
		{
			switch (this.m_mode)
			{
			case ShimTableMemberVisibility.Mode.StaticColumn:
				return this.m_owner.RenderTableColumn.ColumnDefinition.Visibility;
			case ShimTableMemberVisibility.Mode.StaticRow:
				return this.m_owner.RenderTableRow.m_rowDef.Visibility;
			case ShimTableMemberVisibility.Mode.TableGroup:
				return this.m_owner.RenderTableGroup.m_visibilityDef;
			case ShimTableMemberVisibility.Mode.TableDetails:
				return this.m_owner.RenderTableDetails.DetailDefinition.Visibility;
			default:
				return null;
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x00083B78 File Offset: 0x00081D78
		internal override bool GetInstanceHidden()
		{
			switch (this.m_mode)
			{
			case ShimTableMemberVisibility.Mode.StaticColumn:
				return this.m_owner.RenderTableColumn.Hidden;
			case ShimTableMemberVisibility.Mode.StaticRow:
				return this.m_owner.RenderTableRow.Hidden;
			case ShimTableMemberVisibility.Mode.TableGroup:
				return this.m_owner.RenderTableGroup.Hidden;
			case ShimTableMemberVisibility.Mode.TableDetails:
				return this.m_owner.RenderTableDetails[this.m_owner.Group.CurrentRenderGroupIndex].Hidden;
			default:
				return false;
			}
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x00083C00 File Offset: 0x00081E00
		internal override bool GetInstanceStartHidden()
		{
			switch (this.m_mode)
			{
			case ShimTableMemberVisibility.Mode.StaticColumn:
				if (this.m_owner.RenderTableColumn.ColumnInstance == null)
				{
					return this.GetInstanceHidden();
				}
				return this.m_owner.RenderTableColumn.ColumnInstance.StartHidden;
			case ShimTableMemberVisibility.Mode.StaticRow:
				if (this.m_owner.RenderTableRow.InstanceInfo == null)
				{
					return this.GetInstanceHidden();
				}
				return this.m_owner.RenderTableRow.InstanceInfo.StartHidden;
			case ShimTableMemberVisibility.Mode.TableGroup:
				if (this.m_owner.RenderTableGroup.InstanceInfo == null)
				{
					return this.GetInstanceHidden();
				}
				return this.m_owner.RenderTableGroup.InstanceInfo.StartHidden;
			case ShimTableMemberVisibility.Mode.TableDetails:
			{
				TableDetailRowCollection tableDetailRowCollection = this.m_owner.RenderTableDetails[this.m_owner.Group.CurrentRenderGroupIndex];
				if (tableDetailRowCollection.InstanceInfo != null)
				{
					return tableDetailRowCollection.InstanceInfo.StartHidden;
				}
				return this.GetInstanceHidden();
			}
			default:
				return false;
			}
		}

		// Token: 0x040010EF RID: 4335
		private ShimTableMember m_owner;

		// Token: 0x040010F0 RID: 4336
		private ShimTableMemberVisibility.Mode m_mode;

		// Token: 0x02000952 RID: 2386
		internal enum Mode
		{
			// Token: 0x0400406E RID: 16494
			StaticColumn,
			// Token: 0x0400406F RID: 16495
			StaticRow,
			// Token: 0x04004070 RID: 16496
			TableGroup,
			// Token: 0x04004071 RID: 16497
			TableDetails
		}
	}
}

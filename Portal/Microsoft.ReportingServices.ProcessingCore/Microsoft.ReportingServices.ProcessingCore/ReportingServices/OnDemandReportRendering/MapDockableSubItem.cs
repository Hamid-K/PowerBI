using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200018E RID: 398
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapDockableSubItem : MapSubItem, IROMActionOwner
	{
		// Token: 0x06001029 RID: 4137 RVA: 0x00044E9D File Offset: 0x0004309D
		internal MapDockableSubItem(MapDockableSubItem defObject, Map map)
			: base(defObject, map)
		{
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00044EA8 File Offset: 0x000430A8
		public string UniqueName
		{
			get
			{
				return this.m_map.MapDef.UniqueName + "x" + this.MapDockableSubItemDef.ID.ToString();
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x00044EE4 File Offset: 0x000430E4
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && this.MapDockableSubItemDef.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_map.RenderingContext, this.m_map.ReportScope, this.MapDockableSubItemDef.Action, this.m_map.MapDef, this.m_map, ObjectType.Map, this.m_map.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00044F57 File Offset: 0x00043157
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00044F5C File Offset: 0x0004315C
		public ReportEnumProperty<MapPosition> Position
		{
			get
			{
				if (this.m_position == null && this.MapDockableSubItemDef.Position != null)
				{
					this.m_position = new ReportEnumProperty<MapPosition>(this.MapDockableSubItemDef.Position.IsExpression, this.MapDockableSubItemDef.Position.OriginalText, EnumTranslator.TranslateMapPosition(this.MapDockableSubItemDef.Position.StringValue, null));
				}
				return this.m_position;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00044FC5 File Offset: 0x000431C5
		public ReportBoolProperty DockOutsideViewport
		{
			get
			{
				if (this.m_dockOutsideViewport == null && this.MapDockableSubItemDef.DockOutsideViewport != null)
				{
					this.m_dockOutsideViewport = new ReportBoolProperty(this.MapDockableSubItemDef.DockOutsideViewport);
				}
				return this.m_dockOutsideViewport;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x00044FF8 File Offset: 0x000431F8
		public ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null && this.MapDockableSubItemDef.Hidden != null)
				{
					this.m_hidden = new ReportBoolProperty(this.MapDockableSubItemDef.Hidden);
				}
				return this.m_hidden;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0004502B File Offset: 0x0004322B
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && this.MapDockableSubItemDef.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.MapDockableSubItemDef.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0004505E File Offset: 0x0004325E
		internal MapDockableSubItem MapDockableSubItemDef
		{
			get
			{
				return (MapDockableSubItem)this.m_defObject;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0004506B File Offset: 0x0004326B
		internal new MapDockableSubItemInstance Instance
		{
			get
			{
				return (MapDockableSubItemInstance)this.GetInstance();
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00045078 File Offset: 0x00043278
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
		}

		// Token: 0x04000789 RID: 1929
		private ActionInfo m_actionInfo;

		// Token: 0x0400078A RID: 1930
		private ReportEnumProperty<MapPosition> m_position;

		// Token: 0x0400078B RID: 1931
		private ReportBoolProperty m_dockOutsideViewport;

		// Token: 0x0400078C RID: 1932
		private ReportBoolProperty m_hidden;

		// Token: 0x0400078D RID: 1933
		private ReportStringProperty m_toolTip;
	}
}

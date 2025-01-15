using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200010F RID: 271
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class GaugePanelItem : GaugePanelObjectCollectionItem, IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x00034855 File Offset: 0x00032A55
		internal GaugePanelItem(GaugePanelItem defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0003486C File Offset: 0x00032A6C
		string IROMActionOwner.UniqueName
		{
			get
			{
				return this.m_gaugePanel.GaugePanelDef.UniqueName + "x" + this.m_defObject.ID.ToString();
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x000348A6 File Offset: 0x00032AA6
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_gaugePanel, this.m_gaugePanel, this.m_defObject, this.m_gaugePanel.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x000348E0 File Offset: 0x00032AE0
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && this.m_defObject.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_gaugePanel.RenderingContext, this.m_gaugePanel, this.m_defObject.Action, this.m_gaugePanel.GaugePanelDef, this.m_gaugePanel, ObjectType.GaugePanel, this.m_gaugePanel.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0003494E File Offset: 0x00032B4E
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00034951 File Offset: 0x00032B51
		public string Name
		{
			get
			{
				return this.m_defObject.Name;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0003495E File Offset: 0x00032B5E
		public ReportDoubleProperty Top
		{
			get
			{
				if (this.m_top == null && this.m_defObject.Top != null)
				{
					this.m_top = new ReportDoubleProperty(this.m_defObject.Top);
				}
				return this.m_top;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00034991 File Offset: 0x00032B91
		public ReportDoubleProperty Left
		{
			get
			{
				if (this.m_left == null && this.m_defObject.Left != null)
				{
					this.m_left = new ReportDoubleProperty(this.m_defObject.Left);
				}
				return this.m_left;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x000349C4 File Offset: 0x00032BC4
		public ReportDoubleProperty Height
		{
			get
			{
				if (this.m_height == null && this.m_defObject.Height != null)
				{
					this.m_height = new ReportDoubleProperty(this.m_defObject.Height);
				}
				return this.m_height;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x000349F7 File Offset: 0x00032BF7
		public ReportDoubleProperty Width
		{
			get
			{
				if (this.m_width == null && this.m_defObject.Width != null)
				{
					this.m_width = new ReportDoubleProperty(this.m_defObject.Width);
				}
				return this.m_width;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00034A2C File Offset: 0x00032C2C
		public ReportIntProperty ZIndex
		{
			get
			{
				if (this.m_zIndex == null && this.m_defObject.ZIndex != null)
				{
					this.m_zIndex = new ReportIntProperty(this.m_defObject.ZIndex.IsExpression, this.m_defObject.ZIndex.OriginalText, this.m_defObject.ZIndex.IntValue, 0);
				}
				return this.m_zIndex;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00034A90 File Offset: 0x00032C90
		public ReportBoolProperty Hidden
		{
			get
			{
				if (this.m_hidden == null && this.m_defObject.Hidden != null)
				{
					this.m_hidden = new ReportBoolProperty(this.m_defObject.Hidden);
				}
				return this.m_hidden;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00034AC3 File Offset: 0x00032CC3
		public ReportStringProperty ToolTip
		{
			get
			{
				if (this.m_toolTip == null && this.m_defObject.ToolTip != null)
				{
					this.m_toolTip = new ReportStringProperty(this.m_defObject.ToolTip);
				}
				return this.m_toolTip;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00034AF6 File Offset: 0x00032CF6
		public string ParentItem
		{
			get
			{
				return this.m_defObject.ParentItem;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00034B03 File Offset: 0x00032D03
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x00034B0B File Offset: 0x00032D0B
		internal GaugePanelItem GaugePanelItemDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00034B13 File Offset: 0x00032D13
		public GaugePanelItemInstance Instance
		{
			get
			{
				return (GaugePanelItemInstance)this.GetInstance();
			}
		}

		// Token: 0x06000C0B RID: 3083
		internal abstract BaseInstance GetInstance();

		// Token: 0x06000C0C RID: 3084 RVA: 0x00034B20 File Offset: 0x00032D20
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
		}

		// Token: 0x0400052A RID: 1322
		internal GaugePanel m_gaugePanel;

		// Token: 0x0400052B RID: 1323
		internal GaugePanelItem m_defObject;

		// Token: 0x0400052C RID: 1324
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x0400052D RID: 1325
		private ActionInfo m_actionInfo;

		// Token: 0x0400052E RID: 1326
		private ReportDoubleProperty m_top;

		// Token: 0x0400052F RID: 1327
		private ReportDoubleProperty m_left;

		// Token: 0x04000530 RID: 1328
		private ReportDoubleProperty m_height;

		// Token: 0x04000531 RID: 1329
		private ReportDoubleProperty m_width;

		// Token: 0x04000532 RID: 1330
		private ReportIntProperty m_zIndex;

		// Token: 0x04000533 RID: 1331
		private ReportBoolProperty m_hidden;

		// Token: 0x04000534 RID: 1332
		private ReportStringProperty m_toolTip;
	}
}

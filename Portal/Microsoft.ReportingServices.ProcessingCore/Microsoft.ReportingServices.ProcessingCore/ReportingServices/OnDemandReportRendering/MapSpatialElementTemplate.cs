using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001DD RID: 477
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapSpatialElementTemplate : IROMStyleDefinitionContainer, IROMActionOwner
	{
		// Token: 0x06001231 RID: 4657 RVA: 0x0004AC7E File Offset: 0x00048E7E
		internal MapSpatialElementTemplate(MapSpatialElementTemplate defObject, MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_defObject = defObject;
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_map = map;
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0004AC9B File Offset: 0x00048E9B
		public Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.m_map, this.ReportScope, this.m_defObject, this.m_map.RenderingContext);
				}
				return this.m_style;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004ACD4 File Offset: 0x00048ED4
		public string UniqueName
		{
			get
			{
				return this.m_mapVectorLayer.ReportScope.ReportScopeInstance.UniqueName + "x" + this.m_defObject.ID.ToString();
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004AD14 File Offset: 0x00048F14
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null && this.m_defObject.Action != null)
				{
					this.m_actionInfo = new ActionInfo(this.m_map.RenderingContext, this.ReportScope, this.m_defObject.Action, this.m_map.MapDef, this.m_map, ObjectType.Map, this.m_map.Name, this);
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x0004AD82 File Offset: 0x00048F82
		public List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x0004AD85 File Offset: 0x00048F85
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

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0004ADB8 File Offset: 0x00048FB8
		public ReportDoubleProperty OffsetX
		{
			get
			{
				if (this.m_offsetX == null && this.m_defObject.OffsetX != null)
				{
					this.m_offsetX = new ReportDoubleProperty(this.m_defObject.OffsetX);
				}
				return this.m_offsetX;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0004ADEB File Offset: 0x00048FEB
		public ReportDoubleProperty OffsetY
		{
			get
			{
				if (this.m_offsetY == null && this.m_defObject.OffsetY != null)
				{
					this.m_offsetY = new ReportDoubleProperty(this.m_defObject.OffsetY);
				}
				return this.m_offsetY;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0004AE1E File Offset: 0x0004901E
		public ReportStringProperty Label
		{
			get
			{
				if (this.m_label == null && this.m_defObject.Label != null)
				{
					this.m_label = new ReportStringProperty(this.m_defObject.Label);
				}
				return this.m_label;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0004AE51 File Offset: 0x00049051
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

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0004AE84 File Offset: 0x00049084
		public ReportStringProperty DataElementLabel
		{
			get
			{
				if (this.m_dataElementLabel == null)
				{
					if (this.m_defObject.DataElementLabel == null)
					{
						return this.Label;
					}
					this.m_dataElementLabel = new ReportStringProperty(this.m_defObject.DataElementLabel);
				}
				return this.m_dataElementLabel;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x0004AEC0 File Offset: 0x000490C0
		public string DataElementName
		{
			get
			{
				return this.m_defObject.DataElementName;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0004AECD File Offset: 0x000490CD
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_defObject.DataElementOutput;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x0004AEDA File Offset: 0x000490DA
		internal IReportScope ReportScope
		{
			get
			{
				return this.m_mapVectorLayer.ReportScope;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0004AEE7 File Offset: 0x000490E7
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x0004AEEF File Offset: 0x000490EF
		internal MapSpatialElementTemplate MapSpatialElementTemplateDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x0004AEF7 File Offset: 0x000490F7
		public MapSpatialElementTemplateInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x06001242 RID: 4674
		internal abstract MapSpatialElementTemplateInstance GetInstance();

		// Token: 0x06001243 RID: 4675 RVA: 0x0004AEFF File Offset: 0x000490FF
		internal virtual void SetNewContext()
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

		// Token: 0x040008A8 RID: 2216
		protected Map m_map;

		// Token: 0x040008A9 RID: 2217
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x040008AA RID: 2218
		private MapSpatialElementTemplate m_defObject;

		// Token: 0x040008AB RID: 2219
		protected MapSpatialElementTemplateInstance m_instance;

		// Token: 0x040008AC RID: 2220
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x040008AD RID: 2221
		private ActionInfo m_actionInfo;

		// Token: 0x040008AE RID: 2222
		private ReportBoolProperty m_hidden;

		// Token: 0x040008AF RID: 2223
		private ReportDoubleProperty m_offsetX;

		// Token: 0x040008B0 RID: 2224
		private ReportDoubleProperty m_offsetY;

		// Token: 0x040008B1 RID: 2225
		private ReportStringProperty m_label;

		// Token: 0x040008B2 RID: 2226
		private ReportStringProperty m_toolTip;

		// Token: 0x040008B3 RID: 2227
		private ReportStringProperty m_dataElementLabel;
	}
}

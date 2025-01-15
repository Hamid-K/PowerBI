using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000230 RID: 560
	public sealed class ChartThreeDProperties
	{
		// Token: 0x0600157C RID: 5500 RVA: 0x00056940 File Offset: 0x00054B40
		internal ChartThreeDProperties(ChartThreeDProperties threeDPropertiesDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chart = chart;
			this.m_chartThreeDPropertiesDef = threeDPropertiesDef;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00056956 File Offset: 0x00054B56
		internal ChartThreeDProperties(ThreeDProperties renderThreeDPropertiesDef, Microsoft.ReportingServices.OnDemandReportRendering.Chart chart)
		{
			this.m_chart = chart;
			this.m_renderThreeDPropertiesDef = renderThreeDPropertiesDef;
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0005696C File Offset: 0x00054B6C
		public ReportBoolProperty Enabled
		{
			get
			{
				if (this.m_enabled == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_enabled = new ReportBoolProperty(this.m_renderThreeDPropertiesDef.Enabled);
					}
					else if (this.m_chartThreeDPropertiesDef.Enabled != null)
					{
						this.m_enabled = new ReportBoolProperty(this.m_chartThreeDPropertiesDef.Enabled);
					}
				}
				return this.m_enabled;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x000569D0 File Offset: 0x00054BD0
		public ReportEnumProperty<ChartThreeDProjectionModes> ProjectionMode
		{
			get
			{
				if (this.m_projectionMode == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_projectionMode = new ReportEnumProperty<ChartThreeDProjectionModes>(this.m_renderThreeDPropertiesDef.PerspectiveProjectionMode ? ChartThreeDProjectionModes.Oblique : ChartThreeDProjectionModes.Perspective);
					}
					else if (this.m_chartThreeDPropertiesDef.ProjectionMode != null)
					{
						this.m_projectionMode = new ReportEnumProperty<ChartThreeDProjectionModes>(this.m_chartThreeDPropertiesDef.ProjectionMode.IsExpression, this.m_chartThreeDPropertiesDef.ProjectionMode.OriginalText, EnumTranslator.TranslateChartThreeDProjectionMode(this.m_chartThreeDPropertiesDef.ProjectionMode.StringValue, null));
					}
				}
				return this.m_projectionMode;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00056A64 File Offset: 0x00054C64
		public ReportIntProperty Perspective
		{
			get
			{
				if (this.m_perspective == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_perspective = new ReportIntProperty(this.m_renderThreeDPropertiesDef.Perspective);
					}
					else if (this.m_chartThreeDPropertiesDef.Perspective != null)
					{
						this.m_perspective = new ReportIntProperty(this.m_chartThreeDPropertiesDef.Perspective);
					}
				}
				return this.m_perspective;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x00056AC8 File Offset: 0x00054CC8
		public ReportIntProperty Rotation
		{
			get
			{
				if (this.m_rotation == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_rotation = new ReportIntProperty(this.m_renderThreeDPropertiesDef.Rotation);
					}
					else if (this.m_chartThreeDPropertiesDef.Rotation != null)
					{
						this.m_rotation = new ReportIntProperty(this.m_chartThreeDPropertiesDef.Rotation);
					}
				}
				return this.m_rotation;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x00056B2C File Offset: 0x00054D2C
		public ReportIntProperty Inclination
		{
			get
			{
				if (this.m_inclination == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_inclination = new ReportIntProperty(this.m_renderThreeDPropertiesDef.Inclination);
					}
					else if (this.m_chartThreeDPropertiesDef.Inclination != null)
					{
						this.m_inclination = new ReportIntProperty(this.m_chartThreeDPropertiesDef.Inclination);
					}
				}
				return this.m_inclination;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00056B90 File Offset: 0x00054D90
		public ReportIntProperty DepthRatio
		{
			get
			{
				if (this.m_depthRatio == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_depthRatio = new ReportIntProperty(this.m_renderThreeDPropertiesDef.DepthRatio);
					}
					else if (this.m_chartThreeDPropertiesDef.DepthRatio != null)
					{
						this.m_depthRatio = new ReportIntProperty(this.m_chartThreeDPropertiesDef.DepthRatio);
					}
				}
				return this.m_depthRatio;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x00056BF4 File Offset: 0x00054DF4
		public ReportEnumProperty<ChartThreeDShadingTypes> Shading
		{
			get
			{
				if (this.m_shading == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						ChartThreeDShadingTypes chartThreeDShadingTypes = ChartThreeDShadingTypes.None;
						if (this.m_renderThreeDPropertiesDef.Shading == ThreeDProperties.ShadingTypes.Real)
						{
							chartThreeDShadingTypes = ChartThreeDShadingTypes.Real;
						}
						else if (this.m_renderThreeDPropertiesDef.Shading == ThreeDProperties.ShadingTypes.Simple)
						{
							chartThreeDShadingTypes = ChartThreeDShadingTypes.Simple;
						}
						this.m_shading = new ReportEnumProperty<ChartThreeDShadingTypes>(chartThreeDShadingTypes);
					}
					else if (this.m_chartThreeDPropertiesDef.Shading != null)
					{
						this.m_shading = new ReportEnumProperty<ChartThreeDShadingTypes>(this.m_chartThreeDPropertiesDef.Shading.IsExpression, this.m_chartThreeDPropertiesDef.Shading.OriginalText, EnumTranslator.TranslateChartThreeDShading(this.m_chartThreeDPropertiesDef.Shading.StringValue, null));
					}
				}
				return this.m_shading;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x00056CA0 File Offset: 0x00054EA0
		public ReportIntProperty GapDepth
		{
			get
			{
				if (this.m_gapDepth == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_gapDepth = new ReportIntProperty(this.m_renderThreeDPropertiesDef.GapDepth);
					}
					else if (this.m_chartThreeDPropertiesDef.GapDepth != null)
					{
						this.m_gapDepth = new ReportIntProperty(this.m_chartThreeDPropertiesDef.GapDepth);
					}
				}
				return this.m_gapDepth;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00056D04 File Offset: 0x00054F04
		public ReportIntProperty WallThickness
		{
			get
			{
				if (this.m_wallThickness == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_wallThickness = new ReportIntProperty(this.m_renderThreeDPropertiesDef.WallThickness);
					}
					else if (this.m_chartThreeDPropertiesDef.WallThickness != null)
					{
						this.m_wallThickness = new ReportIntProperty(this.m_chartThreeDPropertiesDef.WallThickness);
					}
				}
				return this.m_wallThickness;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x00056D68 File Offset: 0x00054F68
		public ReportBoolProperty Clustered
		{
			get
			{
				if (this.m_clustered == null)
				{
					if (this.m_chart.IsOldSnapshot)
					{
						this.m_clustered = new ReportBoolProperty(this.m_renderThreeDPropertiesDef.Clustered);
					}
					else if (this.m_chartThreeDPropertiesDef.Clustered != null)
					{
						this.m_clustered = new ReportBoolProperty(this.m_chartThreeDPropertiesDef.Clustered);
					}
				}
				return this.m_clustered;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x00056DCB File Offset: 0x00054FCB
		internal Microsoft.ReportingServices.OnDemandReportRendering.Chart ChartDef
		{
			get
			{
				return this.m_chart;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x00056DD3 File Offset: 0x00054FD3
		internal ChartThreeDProperties ChartThreeDPropertiesDef
		{
			get
			{
				return this.m_chartThreeDPropertiesDef;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x00056DDB File Offset: 0x00054FDB
		public ChartThreeDPropertiesInstance Instance
		{
			get
			{
				if (this.m_chart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ChartThreeDPropertiesInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00056E0B File Offset: 0x0005500B
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000A24 RID: 2596
		private Microsoft.ReportingServices.OnDemandReportRendering.Chart m_chart;

		// Token: 0x04000A25 RID: 2597
		private ThreeDProperties m_renderThreeDPropertiesDef;

		// Token: 0x04000A26 RID: 2598
		private ChartThreeDProperties m_chartThreeDPropertiesDef;

		// Token: 0x04000A27 RID: 2599
		private ChartThreeDPropertiesInstance m_instance;

		// Token: 0x04000A28 RID: 2600
		private ReportBoolProperty m_clustered;

		// Token: 0x04000A29 RID: 2601
		private ReportIntProperty m_wallThickness;

		// Token: 0x04000A2A RID: 2602
		private ReportIntProperty m_gapDepth;

		// Token: 0x04000A2B RID: 2603
		private ReportEnumProperty<ChartThreeDShadingTypes> m_shading;

		// Token: 0x04000A2C RID: 2604
		private ReportIntProperty m_depthRatio;

		// Token: 0x04000A2D RID: 2605
		private ReportIntProperty m_rotation;

		// Token: 0x04000A2E RID: 2606
		private ReportIntProperty m_inclination;

		// Token: 0x04000A2F RID: 2607
		private ReportIntProperty m_perspective;

		// Token: 0x04000A30 RID: 2608
		private ReportEnumProperty<ChartThreeDProjectionModes> m_projectionMode;

		// Token: 0x04000A31 RID: 2609
		private ReportBoolProperty m_enabled;
	}
}

using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000253 RID: 595
	public sealed class ChartThreeDPropertiesInstance : BaseInstance
	{
		// Token: 0x06001726 RID: 5926 RVA: 0x0005DAD3 File Offset: 0x0005BCD3
		internal ChartThreeDPropertiesInstance(ChartThreeDProperties chartThreeDPropertiesDef)
			: base(chartThreeDPropertiesDef.ChartDef)
		{
			this.m_chartThreeDPropertiesDef = chartThreeDPropertiesDef;
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0005DAE8 File Offset: 0x0005BCE8
		public bool Enabled
		{
			get
			{
				if (this.m_enabled == null)
				{
					if (this.m_chartThreeDPropertiesDef.ChartDef.IsOldSnapshot)
					{
						this.m_enabled = new bool?(this.m_chartThreeDPropertiesDef.Enabled.Value);
					}
					else
					{
						this.m_enabled = new bool?(this.m_chartThreeDPropertiesDef.ChartThreeDPropertiesDef.EvaluateEnabled(this.ReportScopeInstance, this.m_chartThreeDPropertiesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_enabled.Value;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0005DB74 File Offset: 0x0005BD74
		public ChartThreeDProjectionModes ProjectionMode
		{
			get
			{
				if (this.m_projectionMode == null)
				{
					if (this.m_chartThreeDPropertiesDef.ChartDef.IsOldSnapshot)
					{
						this.m_projectionMode = new ChartThreeDProjectionModes?(this.m_chartThreeDPropertiesDef.ProjectionMode.Value);
					}
					else
					{
						this.m_projectionMode = new ChartThreeDProjectionModes?(this.m_chartThreeDPropertiesDef.ChartThreeDPropertiesDef.EvaluateProjectionMode(this.ReportScopeInstance, this.m_chartThreeDPropertiesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_projectionMode.Value;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06001729 RID: 5929 RVA: 0x0005DC00 File Offset: 0x0005BE00
		public int Perspective
		{
			get
			{
				if (this.m_perspective == null)
				{
					if (this.m_chartThreeDPropertiesDef.ChartDef.IsOldSnapshot)
					{
						this.m_perspective = new int?(this.m_chartThreeDPropertiesDef.Perspective.Value);
					}
					else
					{
						this.m_perspective = new int?(this.m_chartThreeDPropertiesDef.ChartThreeDPropertiesDef.EvaluatePerspective(this.ReportScopeInstance, this.m_chartThreeDPropertiesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_perspective.Value;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0005DC8C File Offset: 0x0005BE8C
		public int Rotation
		{
			get
			{
				if (this.m_rotation == null)
				{
					if (this.m_chartThreeDPropertiesDef.ChartDef.IsOldSnapshot)
					{
						this.m_rotation = new int?(this.m_chartThreeDPropertiesDef.Rotation.Value);
					}
					else
					{
						this.m_rotation = new int?(this.m_chartThreeDPropertiesDef.ChartThreeDPropertiesDef.EvaluateRotation(this.ReportScopeInstance, this.m_chartThreeDPropertiesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_rotation.Value;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x0005DD18 File Offset: 0x0005BF18
		public int Inclination
		{
			get
			{
				if (this.m_inclination == null)
				{
					if (this.m_chartThreeDPropertiesDef.ChartDef.IsOldSnapshot)
					{
						this.m_inclination = new int?(this.m_chartThreeDPropertiesDef.Inclination.Value);
					}
					else
					{
						this.m_inclination = new int?(this.m_chartThreeDPropertiesDef.ChartThreeDPropertiesDef.EvaluateInclination(this.ReportScopeInstance, this.m_chartThreeDPropertiesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_inclination.Value;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0005DDA4 File Offset: 0x0005BFA4
		public int DepthRatio
		{
			get
			{
				if (this.m_depthRatio == null)
				{
					if (this.m_chartThreeDPropertiesDef.ChartDef.IsOldSnapshot)
					{
						this.m_depthRatio = new int?(this.m_chartThreeDPropertiesDef.DepthRatio.Value);
					}
					else
					{
						this.m_depthRatio = new int?(this.m_chartThreeDPropertiesDef.ChartThreeDPropertiesDef.EvaluateDepthRatio(this.ReportScopeInstance, this.m_chartThreeDPropertiesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_depthRatio.Value;
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0005DE30 File Offset: 0x0005C030
		public ChartThreeDShadingTypes Shading
		{
			get
			{
				if (this.m_shading == null)
				{
					if (this.m_chartThreeDPropertiesDef.ChartDef.IsOldSnapshot)
					{
						this.m_shading = new ChartThreeDShadingTypes?(this.m_chartThreeDPropertiesDef.Shading.Value);
					}
					else
					{
						this.m_shading = new ChartThreeDShadingTypes?(this.m_chartThreeDPropertiesDef.ChartThreeDPropertiesDef.EvaluateShading(this.ReportScopeInstance, this.m_chartThreeDPropertiesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_shading.Value;
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0005DEBC File Offset: 0x0005C0BC
		public int GapDepth
		{
			get
			{
				if (this.m_gapDepth == null)
				{
					if (this.m_chartThreeDPropertiesDef.ChartDef.IsOldSnapshot)
					{
						this.m_gapDepth = new int?(this.m_chartThreeDPropertiesDef.GapDepth.Value);
					}
					else
					{
						this.m_gapDepth = new int?(this.m_chartThreeDPropertiesDef.ChartThreeDPropertiesDef.EvaluateGapDepth(this.ReportScopeInstance, this.m_chartThreeDPropertiesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_gapDepth.Value;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0005DF48 File Offset: 0x0005C148
		public int WallThickness
		{
			get
			{
				if (this.m_wallThickness == null)
				{
					if (this.m_chartThreeDPropertiesDef.ChartDef.IsOldSnapshot)
					{
						this.m_wallThickness = new int?(this.m_chartThreeDPropertiesDef.WallThickness.Value);
					}
					else
					{
						this.m_wallThickness = new int?(this.m_chartThreeDPropertiesDef.ChartThreeDPropertiesDef.EvaluateWallThickness(this.ReportScopeInstance, this.m_chartThreeDPropertiesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_wallThickness.Value;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x0005DFD4 File Offset: 0x0005C1D4
		public bool Clustered
		{
			get
			{
				if (this.m_clustered == null)
				{
					if (this.m_chartThreeDPropertiesDef.ChartDef.IsOldSnapshot)
					{
						this.m_clustered = new bool?(this.m_chartThreeDPropertiesDef.Clustered.Value);
					}
					else
					{
						this.m_clustered = new bool?(this.m_chartThreeDPropertiesDef.ChartThreeDPropertiesDef.EvaluateClustered(this.ReportScopeInstance, this.m_chartThreeDPropertiesDef.ChartDef.RenderingContext.OdpContext));
					}
				}
				return this.m_clustered.Value;
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0005E060 File Offset: 0x0005C260
		protected override void ResetInstanceCache()
		{
			this.m_enabled = null;
			this.m_projectionMode = null;
			this.m_perspective = null;
			this.m_rotation = null;
			this.m_inclination = null;
			this.m_depthRatio = null;
			this.m_shading = null;
			this.m_gapDepth = null;
			this.m_wallThickness = null;
			this.m_clustered = null;
		}

		// Token: 0x04000B62 RID: 2914
		private ChartThreeDProperties m_chartThreeDPropertiesDef;

		// Token: 0x04000B63 RID: 2915
		private bool? m_enabled;

		// Token: 0x04000B64 RID: 2916
		private ChartThreeDProjectionModes? m_projectionMode;

		// Token: 0x04000B65 RID: 2917
		private int? m_perspective;

		// Token: 0x04000B66 RID: 2918
		private int? m_rotation;

		// Token: 0x04000B67 RID: 2919
		private int? m_inclination;

		// Token: 0x04000B68 RID: 2920
		private int? m_depthRatio;

		// Token: 0x04000B69 RID: 2921
		private ChartThreeDShadingTypes? m_shading;

		// Token: 0x04000B6A RID: 2922
		private int? m_gapDepth;

		// Token: 0x04000B6B RID: 2923
		private int? m_wallThickness;

		// Token: 0x04000B6C RID: 2924
		private bool? m_clustered;
	}
}

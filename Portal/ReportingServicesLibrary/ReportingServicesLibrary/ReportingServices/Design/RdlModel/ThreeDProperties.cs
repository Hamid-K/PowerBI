using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003B5 RID: 949
	public class ThreeDProperties
	{
		// Token: 0x06001EC5 RID: 7877 RVA: 0x0007D9CC File Offset: 0x0007BBCC
		public ThreeDProperties()
		{
			this.Enabled = false;
			this.ProjectionMode = ThreeDProperties.ProjectionModes.Perspective;
			this.Rotation = 0;
			this.Inclination = 0;
			this.Perspective = 0;
			this.Shading = ThreeDProperties.Shadings.None;
			this.WallThickness = 0;
			this.DrawingStyle = ThreeDProperties.DrawingStyles.Cube;
			this.Clustered = false;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0007DA20 File Offset: 0x0007BC20
		public ThreeDProperties(bool enabled, ThreeDProperties.ProjectionModes projectionMode, int rotation, int inclination, int perspective, ThreeDProperties.Shadings shading, int wallThickness, ThreeDProperties.DrawingStyles drawingStyle, bool clustered)
		{
			this.Enabled = enabled;
			this.ProjectionMode = projectionMode;
			this.Rotation = rotation;
			this.Inclination = inclination;
			this.Perspective = perspective;
			this.Shading = shading;
			this.WallThickness = wallThickness;
			this.DrawingStyle = drawingStyle;
			this.Clustered = clustered;
		}

		// Token: 0x04000D3E RID: 3390
		[DefaultValue(false)]
		public bool Enabled;

		// Token: 0x04000D3F RID: 3391
		[DefaultValue(ThreeDProperties.ProjectionModes.Perspective)]
		public ThreeDProperties.ProjectionModes ProjectionMode;

		// Token: 0x04000D40 RID: 3392
		[DefaultValue(0)]
		public int Rotation;

		// Token: 0x04000D41 RID: 3393
		[DefaultValue(0)]
		public int Inclination;

		// Token: 0x04000D42 RID: 3394
		[DefaultValue(0)]
		public int Perspective;

		// Token: 0x04000D43 RID: 3395
		[DefaultValue(ThreeDProperties.Shadings.None)]
		public ThreeDProperties.Shadings Shading;

		// Token: 0x04000D44 RID: 3396
		[DefaultValue(0)]
		public int GapDepth;

		// Token: 0x04000D45 RID: 3397
		[DefaultValue(0)]
		public int WallThickness;

		// Token: 0x04000D46 RID: 3398
		[DefaultValue(ThreeDProperties.DrawingStyles.Cube)]
		public ThreeDProperties.DrawingStyles DrawingStyle;

		// Token: 0x04000D47 RID: 3399
		[DefaultValue(false)]
		public bool Clustered;

		// Token: 0x02000510 RID: 1296
		public enum ProjectionModes
		{
			// Token: 0x04001252 RID: 4690
			Perspective,
			// Token: 0x04001253 RID: 4691
			Orthographic
		}

		// Token: 0x02000511 RID: 1297
		public enum Shadings
		{
			// Token: 0x04001255 RID: 4693
			None,
			// Token: 0x04001256 RID: 4694
			Simple,
			// Token: 0x04001257 RID: 4695
			Real
		}

		// Token: 0x02000512 RID: 1298
		public enum DrawingStyles
		{
			// Token: 0x04001259 RID: 4697
			Cube,
			// Token: 0x0400125A RID: 4698
			Cylinder
		}
	}
}

using System;
using System.Collections.ObjectModel;
using System.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200005B RID: 91
	internal class GeometryLineStringImplementation : GeometryLineString
	{
		// Token: 0x06000253 RID: 595 RVA: 0x000068E4 File Offset: 0x00004AE4
		internal GeometryLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeometryPoint[0];
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000068FF File Offset: 0x00004AFF
		internal GeometryLineStringImplementation(SpatialImplementation creator, params GeometryPoint[] points)
			: this(CoordinateSystem.DefaultGeometry, creator, points)
		{
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000690E File Offset: 0x00004B0E
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000691B File Offset: 0x00004B1B
		public override ReadOnlyCollection<GeometryPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeometryPoint>(this.points);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00006928 File Offset: 0x00004B28
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.LineString);
			this.SendFigure(pipeline);
			pipeline.EndGeometry();
		}

		// Token: 0x0400006F RID: 111
		private GeometryPoint[] points;
	}
}

using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000058 RID: 88
	internal class GeometryLineStringImplementation : GeometryLineString
	{
		// Token: 0x0600027A RID: 634 RVA: 0x000061C8 File Offset: 0x000043C8
		internal GeometryLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeometryPoint[0];
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000061E3 File Offset: 0x000043E3
		internal GeometryLineStringImplementation(SpatialImplementation creator, params GeometryPoint[] points)
			: this(CoordinateSystem.DefaultGeometry, creator, points)
		{
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000061F2 File Offset: 0x000043F2
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600027D RID: 637 RVA: 0x000061FE File Offset: 0x000043FE
		public override ReadOnlyCollection<GeometryPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeometryPoint>(this.points);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000620B File Offset: 0x0000440B
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.LineString);
			this.SendFigure(pipeline);
			pipeline.EndGeometry();
		}

		// Token: 0x04000068 RID: 104
		private GeometryPoint[] points;
	}
}

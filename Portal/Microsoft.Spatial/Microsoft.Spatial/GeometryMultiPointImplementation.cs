using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200005A RID: 90
	internal class GeometryMultiPointImplementation : GeometryMultiPoint
	{
		// Token: 0x06000285 RID: 645 RVA: 0x000062BA File Offset: 0x000044BA
		internal GeometryMultiPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeometryPoint[0];
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000062D5 File Offset: 0x000044D5
		internal GeometryMultiPointImplementation(SpatialImplementation creator, params GeometryPoint[] points)
			: this(CoordinateSystem.DefaultGeometry, creator, points)
		{
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000062E4 File Offset: 0x000044E4
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000288 RID: 648 RVA: 0x000062F0 File Offset: 0x000044F0
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.points);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000062FD File Offset: 0x000044FD
		public override ReadOnlyCollection<GeometryPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeometryPoint>(this.points);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000630C File Offset: 0x0000450C
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.MultiPoint);
			for (int i = 0; i < this.points.Length; i++)
			{
				this.points[i].SendTo(pipeline);
			}
			pipeline.EndGeometry();
		}

		// Token: 0x0400006A RID: 106
		private GeometryPoint[] points;
	}
}

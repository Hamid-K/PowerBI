using System;
using System.Collections.ObjectModel;
using System.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200005D RID: 93
	internal class GeometryMultiPointImplementation : GeometryMultiPoint
	{
		// Token: 0x0600025E RID: 606 RVA: 0x000069DA File Offset: 0x00004BDA
		internal GeometryMultiPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPoint[] points)
			: base(coordinateSystem, creator)
		{
			this.points = points ?? new GeometryPoint[0];
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000069F5 File Offset: 0x00004BF5
		internal GeometryMultiPointImplementation(SpatialImplementation creator, params GeometryPoint[] points)
			: this(CoordinateSystem.DefaultGeometry, creator, points)
		{
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00006A04 File Offset: 0x00004C04
		public override bool IsEmpty
		{
			get
			{
				return this.points.Length == 0;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00006A11 File Offset: 0x00004C11
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.points);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00006A1E File Offset: 0x00004C1E
		public override ReadOnlyCollection<GeometryPoint> Points
		{
			get
			{
				return new ReadOnlyCollection<GeometryPoint>(this.points);
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00006A2C File Offset: 0x00004C2C
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

		// Token: 0x04000071 RID: 113
		private GeometryPoint[] points;
	}
}

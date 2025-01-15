using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200005F RID: 95
	internal class GeometryMultiPolygonImplementation : GeometryMultiPolygon
	{
		// Token: 0x0600026E RID: 622 RVA: 0x000069DE File Offset: 0x00004BDE
		internal GeometryMultiPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPolygon[] polygons)
			: base(coordinateSystem, creator)
		{
			this.polygons = polygons;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000069EF File Offset: 0x00004BEF
		internal GeometryMultiPolygonImplementation(SpatialImplementation creator, params GeometryPolygon[] polygons)
			: this(CoordinateSystem.DefaultGeometry, creator, polygons)
		{
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000069FE File Offset: 0x00004BFE
		public override bool IsEmpty
		{
			get
			{
				return this.polygons.Length == 0;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00006A0B File Offset: 0x00004C0B
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.polygons);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00006A18 File Offset: 0x00004C18
		public override ReadOnlyCollection<GeometryPolygon> Polygons
		{
			get
			{
				return new ReadOnlyCollection<GeometryPolygon>(this.polygons);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00006A28 File Offset: 0x00004C28
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeometryPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeometry(SpatialType.MultiPolygon);
			for (int i = 0; i < this.polygons.Length; i++)
			{
				this.polygons[i].SendTo(pipeline);
			}
			pipeline.EndGeometry();
		}

		// Token: 0x04000074 RID: 116
		private GeometryPolygon[] polygons;
	}
}

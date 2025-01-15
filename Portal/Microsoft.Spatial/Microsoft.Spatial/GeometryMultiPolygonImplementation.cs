using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200005B RID: 91
	internal class GeometryMultiPolygonImplementation : GeometryMultiPolygon
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000634E File Offset: 0x0000454E
		internal GeometryMultiPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPolygon[] polygons)
			: base(coordinateSystem, creator)
		{
			this.polygons = polygons;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000635F File Offset: 0x0000455F
		internal GeometryMultiPolygonImplementation(SpatialImplementation creator, params GeometryPolygon[] polygons)
			: this(CoordinateSystem.DefaultGeometry, creator, polygons)
		{
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000636E File Offset: 0x0000456E
		public override bool IsEmpty
		{
			get
			{
				return this.polygons.Length == 0;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000637A File Offset: 0x0000457A
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.polygons);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00006387 File Offset: 0x00004587
		public override ReadOnlyCollection<GeometryPolygon> Polygons
		{
			get
			{
				return new ReadOnlyCollection<GeometryPolygon>(this.polygons);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00006394 File Offset: 0x00004594
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

		// Token: 0x0400006B RID: 107
		private GeometryPolygon[] polygons;
	}
}

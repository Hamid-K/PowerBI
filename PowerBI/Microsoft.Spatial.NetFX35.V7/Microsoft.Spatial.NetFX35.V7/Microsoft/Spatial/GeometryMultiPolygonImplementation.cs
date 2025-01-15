using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000056 RID: 86
	internal class GeometryMultiPolygonImplementation : GeometryMultiPolygon
	{
		// Token: 0x06000215 RID: 533 RVA: 0x00005686 File Offset: 0x00003886
		internal GeometryMultiPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryPolygon[] polygons)
			: base(coordinateSystem, creator)
		{
			this.polygons = polygons;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00005697 File Offset: 0x00003897
		internal GeometryMultiPolygonImplementation(SpatialImplementation creator, params GeometryPolygon[] polygons)
			: this(CoordinateSystem.DefaultGeometry, creator, polygons)
		{
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000056A6 File Offset: 0x000038A6
		public override bool IsEmpty
		{
			get
			{
				return this.polygons.Length == 0;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000056B2 File Offset: 0x000038B2
		public override ReadOnlyCollection<Geometry> Geometries
		{
			get
			{
				return new ReadOnlyCollection<Geometry>(this.polygons);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000056BF File Offset: 0x000038BF
		public override ReadOnlyCollection<GeometryPolygon> Polygons
		{
			get
			{
				return new ReadOnlyCollection<GeometryPolygon>(this.polygons);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000056CC File Offset: 0x000038CC
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

		// Token: 0x0400005E RID: 94
		private GeometryPolygon[] polygons;
	}
}

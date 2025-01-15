using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000056 RID: 86
	internal class GeographyMultiPolygonImplementation : GeographyMultiPolygon
	{
		// Token: 0x06000240 RID: 576 RVA: 0x0000647E File Offset: 0x0000467E
		internal GeographyMultiPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyPolygon[] polygons)
			: base(coordinateSystem, creator)
		{
			this.polygons = polygons;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000648F File Offset: 0x0000468F
		internal GeographyMultiPolygonImplementation(SpatialImplementation creator, params GeographyPolygon[] polygons)
			: this(CoordinateSystem.DefaultGeography, creator, polygons)
		{
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000649E File Offset: 0x0000469E
		public override bool IsEmpty
		{
			get
			{
				return this.polygons.Length == 0;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000064AB File Offset: 0x000046AB
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.polygons);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000064B8 File Offset: 0x000046B8
		public override ReadOnlyCollection<GeographyPolygon> Polygons
		{
			get
			{
				return new ReadOnlyCollection<GeographyPolygon>(this.polygons);
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000064C8 File Offset: 0x000046C8
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.MultiPolygon);
			for (int i = 0; i < this.polygons.Length; i++)
			{
				this.polygons[i].SendTo(pipeline);
			}
			pipeline.EndGeography();
		}

		// Token: 0x0400006A RID: 106
		private GeographyPolygon[] polygons;
	}
}

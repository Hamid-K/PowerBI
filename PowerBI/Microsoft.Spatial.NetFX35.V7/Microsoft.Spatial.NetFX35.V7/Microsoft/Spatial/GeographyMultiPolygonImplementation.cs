using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200004D RID: 77
	internal class GeographyMultiPolygonImplementation : GeographyMultiPolygon
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0000512E File Offset: 0x0000332E
		internal GeographyMultiPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyPolygon[] polygons)
			: base(coordinateSystem, creator)
		{
			this.polygons = polygons;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000513F File Offset: 0x0000333F
		internal GeographyMultiPolygonImplementation(SpatialImplementation creator, params GeographyPolygon[] polygons)
			: this(CoordinateSystem.DefaultGeography, creator, polygons)
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000514E File Offset: 0x0000334E
		public override bool IsEmpty
		{
			get
			{
				return this.polygons.Length == 0;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000515A File Offset: 0x0000335A
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.polygons);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005167 File Offset: 0x00003367
		public override ReadOnlyCollection<GeographyPolygon> Polygons
		{
			get
			{
				return new ReadOnlyCollection<GeographyPolygon>(this.polygons);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005174 File Offset: 0x00003374
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

		// Token: 0x04000054 RID: 84
		private GeographyPolygon[] polygons;
	}
}

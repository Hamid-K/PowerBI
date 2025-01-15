using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000052 RID: 82
	internal class GeographyMultiPolygonImplementation : GeographyMultiPolygon
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00005DF6 File Offset: 0x00003FF6
		internal GeographyMultiPolygonImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeographyPolygon[] polygons)
			: base(coordinateSystem, creator)
		{
			this.polygons = polygons;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00005E07 File Offset: 0x00004007
		internal GeographyMultiPolygonImplementation(SpatialImplementation creator, params GeographyPolygon[] polygons)
			: this(CoordinateSystem.DefaultGeography, creator, polygons)
		{
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00005E16 File Offset: 0x00004016
		public override bool IsEmpty
		{
			get
			{
				return this.polygons.Length == 0;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00005E22 File Offset: 0x00004022
		public override ReadOnlyCollection<Geography> Geographies
		{
			get
			{
				return new ReadOnlyCollection<Geography>(this.polygons);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00005E2F File Offset: 0x0000402F
		public override ReadOnlyCollection<GeographyPolygon> Polygons
		{
			get
			{
				return new ReadOnlyCollection<GeographyPolygon>(this.polygons);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00005E3C File Offset: 0x0000403C
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

		// Token: 0x04000061 RID: 97
		private GeographyPolygon[] polygons;
	}
}

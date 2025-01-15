using System;
using System.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000056 RID: 86
	internal class GeographyPointImplementation : GeographyPoint
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000659C File Offset: 0x0000479C
		internal GeographyPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, double latitude, double longitude, double? zvalue, double? mvalue)
			: base(coordinateSystem, creator)
		{
			if (double.IsNaN(latitude) || double.IsInfinity(latitude))
			{
				throw new ArgumentException(Strings.InvalidPointCoordinate(latitude, "latitude"));
			}
			if (double.IsNaN(longitude) || double.IsInfinity(longitude))
			{
				throw new ArgumentException(Strings.InvalidPointCoordinate(longitude, "longitude"));
			}
			this.latitude = latitude;
			this.longitude = longitude;
			this.z = zvalue;
			this.m = mvalue;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000661F File Offset: 0x0000481F
		internal GeographyPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
			this.latitude = double.NaN;
			this.longitude = double.NaN;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00006647 File Offset: 0x00004847
		public override double Latitude
		{
			get
			{
				if (this.IsEmpty)
				{
					throw new NotSupportedException(Strings.Point_AccessCoordinateWhenEmpty);
				}
				return this.latitude;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00006662 File Offset: 0x00004862
		public override double Longitude
		{
			get
			{
				if (this.IsEmpty)
				{
					throw new NotSupportedException(Strings.Point_AccessCoordinateWhenEmpty);
				}
				return this.longitude;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000667D File Offset: 0x0000487D
		public override bool IsEmpty
		{
			get
			{
				return double.IsNaN(this.latitude);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000668A File Offset: 0x0000488A
		public override double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00006692 File Offset: 0x00004892
		public override double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000669C File Offset: 0x0000489C
		public override void SendTo(GeographyPipeline pipeline)
		{
			base.SendTo(pipeline);
			pipeline.BeginGeography(SpatialType.Point);
			if (!this.IsEmpty)
			{
				pipeline.BeginFigure(new GeographyPosition(this.latitude, this.longitude, this.z, this.m));
				pipeline.EndFigure();
			}
			pipeline.EndGeography();
		}

		// Token: 0x04000069 RID: 105
		private double latitude;

		// Token: 0x0400006A RID: 106
		private double longitude;

		// Token: 0x0400006B RID: 107
		private double? z;

		// Token: 0x0400006C RID: 108
		private double? m;
	}
}

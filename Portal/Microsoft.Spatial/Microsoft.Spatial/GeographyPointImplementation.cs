using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000053 RID: 83
	internal class GeographyPointImplementation : GeographyPoint
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00005E80 File Offset: 0x00004080
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "zvalue and mvalue are spelled correctly")]
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

		// Token: 0x06000264 RID: 612 RVA: 0x00005F03 File Offset: 0x00004103
		internal GeographyPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
			this.latitude = double.NaN;
			this.longitude = double.NaN;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00005F2B File Offset: 0x0000412B
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00005F46 File Offset: 0x00004146
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00005F61 File Offset: 0x00004161
		public override bool IsEmpty
		{
			get
			{
				return double.IsNaN(this.latitude);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00005F6E File Offset: 0x0000416E
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00005F76 File Offset: 0x00004176
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00005F80 File Offset: 0x00004180
		[SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
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

		// Token: 0x04000062 RID: 98
		private double latitude;

		// Token: 0x04000063 RID: 99
		private double longitude;

		// Token: 0x04000064 RID: 100
		private double? z;

		// Token: 0x04000065 RID: 101
		private double? m;
	}
}

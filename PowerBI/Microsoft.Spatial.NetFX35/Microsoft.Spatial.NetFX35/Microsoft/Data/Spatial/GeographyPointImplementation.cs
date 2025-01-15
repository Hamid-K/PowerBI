using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000057 RID: 87
	internal class GeographyPointImplementation : GeographyPoint
	{
		// Token: 0x06000246 RID: 582 RVA: 0x0000650C File Offset: 0x0000470C
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

		// Token: 0x06000247 RID: 583 RVA: 0x0000658F File Offset: 0x0000478F
		internal GeographyPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
			this.latitude = double.NaN;
			this.longitude = double.NaN;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000065B7 File Offset: 0x000047B7
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000065D2 File Offset: 0x000047D2
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600024A RID: 586 RVA: 0x000065ED File Offset: 0x000047ED
		public override bool IsEmpty
		{
			get
			{
				return double.IsNaN(this.latitude);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000065FA File Offset: 0x000047FA
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00006602 File Offset: 0x00004802
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000660C File Offset: 0x0000480C
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

		// Token: 0x0400006B RID: 107
		private double latitude;

		// Token: 0x0400006C RID: 108
		private double longitude;

		// Token: 0x0400006D RID: 109
		private double? z;

		// Token: 0x0400006E RID: 110
		private double? m;
	}
}

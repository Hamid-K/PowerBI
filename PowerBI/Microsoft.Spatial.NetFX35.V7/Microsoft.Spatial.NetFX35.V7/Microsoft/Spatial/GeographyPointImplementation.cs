using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x0200004E RID: 78
	internal class GeographyPointImplementation : GeographyPoint
	{
		// Token: 0x060001ED RID: 493 RVA: 0x000051B8 File Offset: 0x000033B8
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

		// Token: 0x060001EE RID: 494 RVA: 0x0000523B File Offset: 0x0000343B
		internal GeographyPointImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator)
			: base(coordinateSystem, creator)
		{
			this.latitude = double.NaN;
			this.longitude = double.NaN;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00005263 File Offset: 0x00003463
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000527E File Offset: 0x0000347E
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00005299 File Offset: 0x00003499
		public override bool IsEmpty
		{
			get
			{
				return double.IsNaN(this.latitude);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000052A6 File Offset: 0x000034A6
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000052AE File Offset: 0x000034AE
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
		public override double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000052B8 File Offset: 0x000034B8
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

		// Token: 0x04000055 RID: 85
		private double latitude;

		// Token: 0x04000056 RID: 86
		private double longitude;

		// Token: 0x04000057 RID: 87
		private double? z;

		// Token: 0x04000058 RID: 88
		private double? m;
	}
}

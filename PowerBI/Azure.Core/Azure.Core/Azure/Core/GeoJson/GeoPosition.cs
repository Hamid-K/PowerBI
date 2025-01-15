using System;
using System.Runtime.CompilerServices;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000B8 RID: 184
	public readonly struct GeoPosition : IEquatable<GeoPosition>
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00011C2C File Offset: 0x0000FE2C
		public double? Altitude { get; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00011C34 File Offset: 0x0000FE34
		public double Longitude { get; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x00011C3C File Offset: 0x0000FE3C
		public double Latitude { get; }

		// Token: 0x060005B6 RID: 1462 RVA: 0x00011C44 File Offset: 0x0000FE44
		public GeoPosition(double longitude, double latitude)
		{
			this = new GeoPosition(longitude, latitude, null);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00011C62 File Offset: 0x0000FE62
		public GeoPosition(double longitude, double latitude, double? altitude)
		{
			this.Longitude = longitude;
			this.Latitude = latitude;
			this.Altitude = altitude;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00011C7C File Offset: 0x0000FE7C
		public bool Equals(GeoPosition other)
		{
			return Nullable.Equals<double>(this.Altitude, other.Altitude) && this.Longitude.Equals(other.Longitude) && this.Latitude.Equals(other.Latitude);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00011CCC File Offset: 0x0000FECC
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is GeoPosition)
			{
				GeoPosition geoPosition = (GeoPosition)obj;
				return this.Equals(geoPosition);
			}
			return false;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00011CF1 File Offset: 0x0000FEF1
		public override int GetHashCode()
		{
			return HashCodeBuilder.Combine<double, double, double?>(this.Longitude, this.Latitude, this.Altitude);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00011D0A File Offset: 0x0000FF0A
		public static bool operator ==(GeoPosition left, GeoPosition right)
		{
			return left.Equals(right);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00011D14 File Offset: 0x0000FF14
		public static bool operator !=(GeoPosition left, GeoPosition right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00011D24 File Offset: 0x0000FF24
		[NullableContext(1)]
		public override string ToString()
		{
			if (this.Altitude == null)
			{
				return string.Format("[{0:G17}, {1:G17}]", this.Longitude, this.Latitude);
			}
			return string.Format("[{0:G17}, {1:G17}, {2:G17}]", this.Longitude, this.Latitude, this.Altitude.Value);
		}

		// Token: 0x17000190 RID: 400
		public double this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.Longitude;
				case 1:
					return this.Latitude;
				case 2:
					if (this.Altitude != null)
					{
						return this.Altitude.Value;
					}
					break;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00011DF8 File Offset: 0x0000FFF8
		public int Count
		{
			get
			{
				if (this.Altitude != null)
				{
					return 3;
				}
				return 2;
			}
		}
	}
}

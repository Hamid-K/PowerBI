using System;
using System.Globalization;

namespace System.Spatial
{
	// Token: 0x0200003C RID: 60
	public class GeographyPosition : IEquatable<GeographyPosition>
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00004807 File Offset: 0x00002A07
		public GeographyPosition(double latitude, double longitude, double? z, double? m)
		{
			this.latitude = latitude;
			this.longitude = longitude;
			this.z = z;
			this.m = m;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000482C File Offset: 0x00002A2C
		public GeographyPosition(double latitude, double longitude)
			: this(latitude, longitude, default(double?), default(double?))
		{
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00004853 File Offset: 0x00002A53
		public double Latitude
		{
			get
			{
				return this.latitude;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000485B File Offset: 0x00002A5B
		public double Longitude
		{
			get
			{
				return this.longitude;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00004863 File Offset: 0x00002A63
		public double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000486B File Offset: 0x00002A6B
		public double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00004873 File Offset: 0x00002A73
		public static bool operator ==(GeographyPosition left, GeographyPosition right)
		{
			if (object.ReferenceEquals(left, null))
			{
				return object.ReferenceEquals(right, null);
			}
			return !object.ReferenceEquals(right, null) && left.Equals(right);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00004898 File Offset: 0x00002A98
		public static bool operator !=(GeographyPosition left, GeographyPosition right)
		{
			return !(left == right);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000048A4 File Offset: 0x00002AA4
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && obj.GetType() == typeof(GeographyPosition) && this.Equals((GeographyPosition)obj);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000048D4 File Offset: 0x00002AD4
		public bool Equals(GeographyPosition other)
		{
			return other != null && other.latitude.Equals(this.latitude) && other.longitude.Equals(this.longitude) && other.z.Equals(this.z) && other.m.Equals(this.m);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00004958 File Offset: 0x00002B58
		public override int GetHashCode()
		{
			int num = this.latitude.GetHashCode();
			num = (num * 397) ^ this.longitude.GetHashCode();
			num = (num * 397) ^ ((this.z != null) ? this.z.Value.GetHashCode() : 0);
			return (num * 397) ^ ((this.m != null) ? this.m.Value.GetHashCode() : 0);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000049F8 File Offset: 0x00002BF8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "GeographyPosition(latitude:{0}, longitude:{1}, z:{2}, m:{3})", new object[]
			{
				this.latitude,
				this.longitude,
				(this.z != null) ? this.z.ToString() : "null",
				(this.m != null) ? this.m.ToString() : "null"
			});
		}

		// Token: 0x04000034 RID: 52
		private readonly double latitude;

		// Token: 0x04000035 RID: 53
		private readonly double longitude;

		// Token: 0x04000036 RID: 54
		private readonly double? m;

		// Token: 0x04000037 RID: 55
		private readonly double? z;
	}
}

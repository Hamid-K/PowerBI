using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x0200003E RID: 62
	public class GeographyPosition : IEquatable<GeographyPosition>
	{
		// Token: 0x06000195 RID: 405 RVA: 0x0000491F File Offset: 0x00002B1F
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public GeographyPosition(double latitude, double longitude, double? z, double? m)
		{
			this.latitude = latitude;
			this.longitude = longitude;
			this.z = z;
			this.m = m;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004944 File Offset: 0x00002B44
		public GeographyPosition(double latitude, double longitude)
			: this(latitude, longitude, default(double?), default(double?))
		{
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000496B File Offset: 0x00002B6B
		public double Latitude
		{
			get
			{
				return this.latitude;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00004973 File Offset: 0x00002B73
		public double Longitude
		{
			get
			{
				return this.longitude;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000497B File Offset: 0x00002B7B
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00004983 File Offset: 0x00002B83
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000498B File Offset: 0x00002B8B
		public static bool operator ==(GeographyPosition left, GeographyPosition right)
		{
			if (object.ReferenceEquals(left, null))
			{
				return object.ReferenceEquals(right, null);
			}
			return !object.ReferenceEquals(right, null) && left.Equals(right);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000049B0 File Offset: 0x00002BB0
		public static bool operator !=(GeographyPosition left, GeographyPosition right)
		{
			return !(left == right);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000049BC File Offset: 0x00002BBC
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && obj.GetType() == typeof(GeographyPosition) && this.Equals((GeographyPosition)obj);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000049EC File Offset: 0x00002BEC
		public bool Equals(GeographyPosition other)
		{
			return other != null && other.latitude.Equals(this.latitude) && other.longitude.Equals(this.longitude) && other.z.Equals(this.z) && other.m.Equals(this.m);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004A70 File Offset: 0x00002C70
		public override int GetHashCode()
		{
			int num = this.latitude.GetHashCode();
			num = (num * 397) ^ this.longitude.GetHashCode();
			num = (num * 397) ^ ((this.z != null) ? this.z.Value.GetHashCode() : 0);
			return (num * 397) ^ ((this.m != null) ? this.m.Value.GetHashCode() : 0);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00004B10 File Offset: 0x00002D10
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

		// Token: 0x04000038 RID: 56
		private readonly double latitude;

		// Token: 0x04000039 RID: 57
		private readonly double longitude;

		// Token: 0x0400003A RID: 58
		private readonly double? m;

		// Token: 0x0400003B RID: 59
		private readonly double? z;
	}
}

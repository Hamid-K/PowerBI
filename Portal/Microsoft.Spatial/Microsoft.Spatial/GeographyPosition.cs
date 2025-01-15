using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x0200003D RID: 61
	public class GeographyPosition : IEquatable<GeographyPosition>
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00004B7F File Offset: 0x00002D7F
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public GeographyPosition(double latitude, double longitude, double? z, double? m)
		{
			this.latitude = latitude;
			this.longitude = longitude;
			this.z = z;
			this.m = m;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public GeographyPosition(double latitude, double longitude)
			: this(latitude, longitude, null, null)
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004BCB File Offset: 0x00002DCB
		public double Latitude
		{
			get
			{
				return this.latitude;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00004BD3 File Offset: 0x00002DD3
		public double Longitude
		{
			get
			{
				return this.longitude;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00004BDB File Offset: 0x00002DDB
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00004BE3 File Offset: 0x00002DE3
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00004BEB File Offset: 0x00002DEB
		public static bool operator ==(GeographyPosition left, GeographyPosition right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Equals(right);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00004C01 File Offset: 0x00002E01
		public static bool operator !=(GeographyPosition left, GeographyPosition right)
		{
			return !(left == right);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00004C0D File Offset: 0x00002E0D
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == typeof(GeographyPosition) && this.Equals((GeographyPosition)obj);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00004C34 File Offset: 0x00002E34
		public bool Equals(GeographyPosition other)
		{
			return other != null && other.latitude.Equals(this.latitude) && other.longitude.Equals(this.longitude) && other.z.Equals(this.z) && other.m.Equals(this.m);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00004CB8 File Offset: 0x00002EB8
		public override int GetHashCode()
		{
			int num = this.latitude.GetHashCode();
			num = (num * 397) ^ this.longitude.GetHashCode();
			num = (num * 397) ^ ((this.z != null) ? this.z.Value.GetHashCode() : 0);
			return (num * 397) ^ ((this.m != null) ? this.m.Value.GetHashCode() : 0);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00004D50 File Offset: 0x00002F50
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

		// Token: 0x0400003E RID: 62
		private readonly double latitude;

		// Token: 0x0400003F RID: 63
		private readonly double longitude;

		// Token: 0x04000040 RID: 64
		private readonly double? m;

		// Token: 0x04000041 RID: 65
		private readonly double? z;
	}
}

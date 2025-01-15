using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x02000038 RID: 56
	public class GeographyPosition : IEquatable<GeographyPosition>
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00003EAB File Offset: 0x000020AB
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public GeographyPosition(double latitude, double longitude, double? z, double? m)
		{
			this.latitude = latitude;
			this.longitude = longitude;
			this.z = z;
			this.m = m;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003ED0 File Offset: 0x000020D0
		public GeographyPosition(double latitude, double longitude)
			: this(latitude, longitude, default(double?), default(double?))
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00003EF7 File Offset: 0x000020F7
		public double Latitude
		{
			get
			{
				return this.latitude;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00003EFF File Offset: 0x000020FF
		public double Longitude
		{
			get
			{
				return this.longitude;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003F07 File Offset: 0x00002107
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00003F0F File Offset: 0x0000210F
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00003F17 File Offset: 0x00002117
		public static bool operator ==(GeographyPosition left, GeographyPosition right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Equals(right);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003F2D File Offset: 0x0000212D
		public static bool operator !=(GeographyPosition left, GeographyPosition right)
		{
			return !(left == right);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00003F39 File Offset: 0x00002139
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == typeof(GeographyPosition) && this.Equals((GeographyPosition)obj);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00003F60 File Offset: 0x00002160
		public bool Equals(GeographyPosition other)
		{
			return other != null && other.latitude.Equals(this.latitude) && other.longitude.Equals(this.longitude) && other.z.Equals(this.z) && other.m.Equals(this.m);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003FE4 File Offset: 0x000021E4
		public override int GetHashCode()
		{
			int num = this.latitude.GetHashCode();
			num = (num * 397) ^ this.longitude.GetHashCode();
			num = (num * 397) ^ ((this.z != null) ? this.z.Value.GetHashCode() : 0);
			return (num * 397) ^ ((this.m != null) ? this.m.Value.GetHashCode() : 0);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000407C File Offset: 0x0000227C
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

		// Token: 0x04000031 RID: 49
		private readonly double latitude;

		// Token: 0x04000032 RID: 50
		private readonly double longitude;

		// Token: 0x04000033 RID: 51
		private readonly double? m;

		// Token: 0x04000034 RID: 52
		private readonly double? z;
	}
}

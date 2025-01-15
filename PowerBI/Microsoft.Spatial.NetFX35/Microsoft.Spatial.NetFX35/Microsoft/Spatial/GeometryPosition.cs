using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x0200003F RID: 63
	public class GeometryPosition : IEquatable<GeometryPosition>
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00004BAD File Offset: 0x00002DAD
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public GeometryPosition(double x, double y, double? z, double? m)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.m = m;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004BD4 File Offset: 0x00002DD4
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y make sense in context")]
		public GeometryPosition(double x, double y)
			: this(x, y, default(double?), default(double?))
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00004BFB File Offset: 0x00002DFB
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? M
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00004C03 File Offset: 0x00002E03
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00004C0B File Offset: 0x00002E0B
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00004C13 File Offset: 0x00002E13
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m make sense in context")]
		public double? Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00004C1B File Offset: 0x00002E1B
		public static bool operator ==(GeometryPosition left, GeometryPosition right)
		{
			if (object.ReferenceEquals(left, null))
			{
				return object.ReferenceEquals(right, null);
			}
			return !object.ReferenceEquals(right, null) && left.Equals(right);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00004C40 File Offset: 0x00002E40
		public static bool operator !=(GeometryPosition left, GeometryPosition right)
		{
			return !(left == right);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00004C4C File Offset: 0x00002E4C
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && obj.GetType() == typeof(GeometryPosition) && this.Equals((GeometryPosition)obj);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00004C7C File Offset: 0x00002E7C
		public bool Equals(GeometryPosition other)
		{
			return other != null && other.x.Equals(this.x) && other.y.Equals(this.y) && other.z.Equals(this.z) && other.m.Equals(this.m);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00004D00 File Offset: 0x00002F00
		public override int GetHashCode()
		{
			int num = this.x.GetHashCode();
			num = (num * 397) ^ this.y.GetHashCode();
			num = (num * 397) ^ ((this.z != null) ? this.z.Value.GetHashCode() : 0);
			return (num * 397) ^ ((this.m != null) ? this.m.Value.GetHashCode() : 0);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "GeometryPosition({0}, {1}, {2}, {3})", new object[]
			{
				this.x,
				this.y,
				(this.z != null) ? this.z.ToString() : "null",
				(this.m != null) ? this.m.ToString() : "null"
			});
		}

		// Token: 0x0400003C RID: 60
		private readonly double? m;

		// Token: 0x0400003D RID: 61
		private readonly double x;

		// Token: 0x0400003E RID: 62
		private readonly double y;

		// Token: 0x0400003F RID: 63
		private readonly double? z;
	}
}
